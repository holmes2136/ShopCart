using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Marketing;

public partial class Components_AffiliateDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string AffiliateCode
    {
        get
        {
            return DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( HttpContext.Current.User.Identity.Name );
        }
    }

    private string EditMode
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["EditMode"] ))
                return Request.QueryString["EditMode"];
            else
                return "";
        }
    }

    private bool IsEnabled
    {
        get
        {
            if (ViewState["CurrentIsEnabled"] == null)
                return true;
            else
                return (bool) ViewState["CurrentIsEnabled"];
        }

        set { ViewState["CurrentIsEnabled"] = value; }
    }

    private string ErrorMessage
    {
        get
        {
            if (ViewState["ErrorMessage"] == null)
                return String.Empty;
            return ViewState["ErrorMessage"].ToString();
        }
        set
        {
            ViewState["ErrorMessage"] = value;
        }
    }

    private void PopulateBlockAccept()
    {
        string bodyText = String.Empty;

        EmailTemplateTextVariable.ReplaceAffiliateAgreementText( out bodyText );
        uxAgreementDIV.InnerHtml = bodyText;

    }

    private void PopulateControls()
    {
        if (!string.IsNullOrEmpty( AffiliateCode ))
        {
            Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
            uxFirstName.Text = affiliate.ContactAddress.FirstName;
            uxLastName.Text = affiliate.ContactAddress.LastName;
            uxUserName.Text = affiliate.UserName;
            uxCompany.Text = affiliate.ContactAddress.Company;
            uxAddress1.Text = affiliate.ContactAddress.Address1;
            uxAddress2.Text = affiliate.ContactAddress.Address2;
            uxCity.Text = affiliate.ContactAddress.City;
            uxCountryAndState.CurrentCountry = affiliate.ContactAddress.Country;
            uxCountryAndState.CurrentState = affiliate.ContactAddress.State;
            uxZip.Text = affiliate.ContactAddress.Zip;
            uxPhone.Text = affiliate.ContactAddress.Phone;
            uxFax.Text = affiliate.ContactAddress.Fax;
            uxEmail.Text = affiliate.Email;
            uxWebSite.Text = affiliate.Website;
            uxCommissionRateLabel.Text = affiliate.CommissionRate.ToString();
            IsEnabled = affiliate.IsEnabled;
        }
    }

    private void SendMailToAffiliate( string affiliateEmail, string userName )
    {
        string body, subjectMail;
        if (DataAccessContext.Configurations.GetBoolValue( "AffiliateAutoApprove" ))
        {
            EmailTemplateTextVariable.ReplaceAffiliateApproveText( userName, affiliateEmail, out subjectMail, out body );
        }
        else
        {
            EmailTemplateTextVariable.ReplaceNewAffiliateRegistrationText( userName, affiliateEmail, out subjectMail, out body );
        }

        WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail,
                affiliateEmail,
                subjectMail,
                body );

    }

    private void SendMailToMerchant( string affiliateCode )
    {
        string subjectMail;
        string body;
        string affiliateViewUrl = UrlPath.StorefrontUrl + DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ) +
            "/Default.aspx#AffiliateEdit,AffiliateCode=" + affiliateCode;

        EmailTemplateTextVariable.ReplaceAffiliateSubscribeText(
            affiliateViewUrl,
            uxUserName.Text.Trim(),
            uxEmail.Text.Trim(),
            out subjectMail,
            out body );

        WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail,
                NamedConfig.CompanyEmail,
                subjectMail,
                body );
    }

    private Affiliate SetUpAffiliate( Affiliate affiliate )
    {
        affiliate.UserName = uxUserName.Text.Trim();
        affiliate.ContactAddress = new Address( uxFirstName.Text, uxLastName.Text, uxCompany.Text, uxAddress1.Text,
            uxAddress2.Text, uxCity.Text, uxCountryAndState.CurrentState, uxZip.Text, uxCountryAndState.CurrentCountry,
            uxPhone.Text, uxFax.Text );
        affiliate.Email = uxEmail.Text.Trim();
        affiliate.Website = uxWebSite.Text.Trim();
        if (!DataAccessContext.Configurations.GetBoolValue( "AffiliateAutoApprove" ))
            affiliate.IsEnabled = false;
        else
            affiliate.IsEnabled = true;

        return affiliate;
    }

    private void AddAffiliate()
    {
        if (Page.IsValid)
        {
            if (uxCountryAndState.IsRequiredCountry && !uxCountryAndState.VerifyCountryIsValid)
            {
                uxCountryStateDiv.Visible = true;
                uxCountryStateMessage.Text = "Required Country.";
                return;
            }

            if (uxCountryAndState.IsRequiredState && !uxCountryAndState.VerifyStateIsValid)
            {
                uxCountryStateDiv.Visible = true;
                uxCountryStateMessage.Text = "Required State.";
                return;
            }

            MembershipUser user = Membership.GetUser( uxUserName.Text.Trim() );
            if (user == null)
            {
                Affiliate affiliate = new Affiliate();
                affiliate = SetUpAffiliate( affiliate );
                affiliate.RegisterDate = DateTime.Today;
                affiliate.CommissionRate = DataAccessContext.Configurations.GetDecimalValue( "AffiliateCommissionRate" );
                affiliate = DataAccessContextDeluxe.AffiliateRepository.Save( affiliate );

                Membership.CreateUser( uxUserName.Text.Trim(), uxPassword.Text, uxEmail.Text.Trim() );
                Roles.AddUserToRole( uxUserName.Text, "Affiliates" );

                if (DataAccessContext.Configurations.GetBoolValue( "AffiliateAutoApprove" ))
                    FormsAuthentication.SetAuthCookie( uxUserName.Text, false );

                try
                {
                    SendMailToAffiliate( uxEmail.Text, uxUserName.Text );
                    SendMailToMerchant( affiliate.AffiliateCode );
                    Response.Redirect( "AffiliateRegisterFinish.aspx" );
                }
                catch (Exception)
                {
                    uxUsernameValidDIV.Attributes.Add( "display", "none" );
                    ErrorMessage = "[$SentErrorMessage]";
                    ClearData();
                }
            }
            else
            {
                ErrorMessage = "[$User Existed]";
            }
        }
    }

    private void Update()
    {
        Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
        affiliate = SetUpAffiliate( affiliate );
        affiliate.CommissionRate = ConvertUtilities.ToDecimal( uxCommissionRateLabel.Text );
        DataAccessContextDeluxe.AffiliateRepository.Save( affiliate );

        ErrorMessage = "[$UpdateComplete]";
    }

    private void ClearData()
    {
        uxUserName.Text = String.Empty;
        uxEmail.Text = String.Empty;
        uxPassword.Text = String.Empty;
        uxTextBoxConfrim.Text = String.Empty;
        uxFirstName.Text = String.Empty;
        uxLastName.Text = String.Empty;
        uxWebSite.Text = String.Empty;
        uxCompany.Text = String.Empty;
        uxAddress1.Text = String.Empty;
        uxAddress2.Text = String.Empty;
        uxCity.Text = String.Empty;
        uxZip.Text = String.Empty;
        uxPhone.Text = String.Empty;
        uxFax.Text = String.Empty;
        uxAcceptCheck.Checked = false;
        uxCountryAndState.CurrentState = "";
        Country country = DataAccessContext.CountryRepository.GetOne( DataAccessContext.Configurations.GetValue( "StoreDefaultCountry", StoreContext.CurrentStore ).ToString() );
        uxCountryAndState.SetCountryByName( country.CommonName );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCountryStateDiv.Visible = false;

        if (!IsEditMode())
        {
            if (IsPostBack)
            {
                uxUsernameValidDIV.Attributes.Add( "display", "" );
                uxUserName.Attributes.Add( "onchange", "var uxMessage = document.getElementById('" + uxUsernameValidDIV.ClientID + "');uxMessage.innerHTML = '';" );
            }
            else
            {
                uxUsernameValidDIV.Attributes.Add( "display", "none" );
            }
        }

        if (!IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
                uxPasswordTR.Visible = false;
                uxConfirmPasswordTR.Visible = false;
                uxAddButton.Visible = false;
                uxAgreementPanel.Visible = false;
                uxUserName.Enabled = false;

                if (EditMode == "account")
                    uxRegisAddressPanel.Style["display"] = "none";
                else if (EditMode == "address")
                    uxRegisterPanel.Style["display"] = "none";
            }
            else
            {
                PopulateBlockAccept();
                uxCommissionRateTR.Visible = false;
                uxUpdateButton.Visible = false;
                uxAgreementPanel.Visible = true;
                uxUserName.Enabled = true;
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (!uxAcceptCheck.Checked)
        {
            uxPolicyAgreementMessage.Text = "[$ErrorPolicyCheck]";
            uxPolicyAgreementValidatorDiv.Visible = true;
        }
        else
        {
            uxPolicyAgreementValidatorDiv.Visible = false;
            AddAffiliate();
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (uxCountryAndState.IsRequiredCountry && !uxCountryAndState.VerifyCountryIsValid)
        {
            uxCountryStateDiv.Visible = true;
            uxCountryStateMessage.Text = "Required Country.";
            return;
        }

        if (uxCountryAndState.IsRequiredState && !uxCountryAndState.VerifyStateIsValid)
        {
            uxCountryStateDiv.Visible = true;
            uxCountryStateMessage.Text = "Required State.";
            return;
        }

        Update();
    }
    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    public string GetMessage()
    {
        return ErrorMessage;
    }
}
