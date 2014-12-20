using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;

public partial class AdminAdvanced_Components_AffiliateDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string AffiliateCode
    {
        get
        {
            return MainContext.QueryString["AffiliateCode"];
        }
    }

    private void uxState_RefreshHandler( object sender, EventArgs e )
    {
        uxStateList.CountryCode = uxCountryList.CurrentSelected;
        uxStateList.Refresh();
    }

    private void PopulateLink()
    {
        if (IsEditMode())
        {
            uxLinkTR.Visible = true;
            GetPaymentListLink();
            GetCommissionListLink();
        }
        else
            uxLinkTR.Visible = false;
    }

    private void GetPaymentListLink()
    {
        uxPaymentListLink.PageName = "AffiliatePaymentList.ascx";
        uxPaymentListLink.PageQueryString = "AffiliateCode=" + AffiliateCode;
    }

    private void GetCommissionListLink()
    {
        uxCommissionListLink.PageName = "AffiliateCommissionList.ascx";
        uxCommissionListLink.PageQueryString = "AffiliateCode=" + AffiliateCode;
    }

    private void DisplaySendEmailButtonControl()
    {
        if (IsEditMode())
        {
            uxAddSendMailButton.Visible = false;

            if (IsAdminModifiable())
            {
                if (uxIsEnabledCheck.Checked)
                    uxUpdateSendMailButton.Visible = true;
                else
                    uxUpdateSendMailButton.Visible = false;
            }
            else
            {
                uxUpdateSendMailButton.Visible = false;
            }
        }
        else
        {
            uxUpdateSendMailButton.Visible = false;
            if (uxIsEnabledCheck.Checked)
                uxAddSendMailButton.Visible = true;
            else
                uxAddSendMailButton.Visible = false;
        }


    }

    private void PopulateControls()
    {
        if (!String.IsNullOrEmpty( AffiliateCode ))
        {
            Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
            uxUserName.Text = affiliate.UserName;
            uxFirstName.Text = affiliate.ContactAddress.FirstName;
            uxLastName.Text = affiliate.ContactAddress.LastName;
            uxCompany.Text = affiliate.ContactAddress.Company;
            uxAddress1.Text = affiliate.ContactAddress.Address1;
            uxAddress2.Text = affiliate.ContactAddress.Address2;
            uxCity.Text = affiliate.ContactAddress.City;
            uxStateList.CountryCode = affiliate.ContactAddress.Country;
            uxStateList.CurrentSelected = affiliate.ContactAddress.State;
            uxZip.Text = affiliate.ContactAddress.Zip;
            uxCountryList.CurrentSelected = affiliate.ContactAddress.Country;
            uxPhone.Text = affiliate.ContactAddress.Phone;
            uxFax.Text = affiliate.ContactAddress.Fax;
            uxEmail.Text = affiliate.Email;
            uxWebSite.Text = affiliate.Website;
            uxCalendarPopup.SelectedDate = Convert.ToDateTime( affiliate.RegisterDate );
            uxCommissionText.Text = affiliate.CommissionRate.ToString();
            uxIsEnabledCheck.Checked = affiliate.IsEnabled;
        }
    }

    private void ClearInputFields()
    {
        uxUserName.Text = "";
        uxPasswordText.Text = "";
        uxTextBoxConfrim.Text = "";
        uxFirstName.Text = "";
        uxLastName.Text = "";
        uxWebSite.Text = "";
        uxCompany.Text = "";
        uxAddress1.Text = "";
        uxAddress2.Text = "";
        uxCity.Text = "";
        uxCountryList.CurrentSelected = "";
        uxStateList.CountryCode = "";
        uxStateList.CurrentSelected = "";
        uxZip.Text = "";
        uxPhone.Text = "";
        uxFax.Text = "";
        uxEmail.Text = "";
        uxIsEnabledCheck.Checked = true;

        uxCommissionText.Text = DataAccessContext.Configurations.GetValue( "AffiliateCommissionRate" );

        DisplaySendEmailButtonControl();
    }

    private void SendMail()
    {
        string subjectMail;
        string bodyMail;

        EmailTemplateTextVariable.ReplaceAffiliateApproveText(
            uxUserName.Text, uxEmail.Text, out subjectMail, out bodyMail );

        WebUtilities.SendHtmlMail(
            NamedConfig.CompanyEmail,
            uxEmail.Text,
            subjectMail,
            bodyMail );
        uxMessage.DisplayMessage( Resources.AffiliateMessages.SendApproveMailSuccess );
    }

    private Affiliate SetUpAffiliate( Affiliate affiliate )
    {
        affiliate.RegisterDate = uxCalendarPopup.SelectedDate;
        affiliate.UserName = uxUserName.Text;
        affiliate.ContactAddress = new Address( uxFirstName.Text, uxLastName.Text, uxCompany.Text, uxAddress1.Text, uxAddress2.Text,
            uxCity.Text, uxStateList.CurrentSelected, uxZip.Text, uxCountryList.CurrentSelected, uxPhone.Text, uxFax.Text );
        affiliate.Email = uxEmail.Text;
        affiliate.Website = uxWebSite.Text;
        affiliate.CommissionRate = ConvertUtilities.ToDecimal( uxCommissionText.Text );
        affiliate.IsEnabled = uxIsEnabledCheck.Checked;
        return affiliate;
    }

    private Affiliate AddNewAffiliate()
    {
        if (Page.IsValid)
        {
            MembershipUser user = Membership.GetUser( uxUserName.Text );
            if (user != null)
            {
                uxMessage.DisplayError( Resources.AffiliateMessages.UserNameExistedError );
                return Affiliate.Null;
            }

            Membership.CreateUser( uxUserName.Text, uxPasswordText.Text, uxEmail.Text.Trim() );
            Roles.AddUserToRole( uxUserName.Text, "Affiliates" );

            Affiliate affiliate = new Affiliate();
            affiliate = SetUpAffiliate( affiliate );
            affiliate = DataAccessContextDeluxe.AffiliateRepository.Save( affiliate );
            return affiliate;
        }
        return Affiliate.Null;
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid)
            {
                Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( AffiliateCode );
                affiliate = SetUpAffiliate( affiliate );
                DataAccessContextDeluxe.AffiliateRepository.Save( affiliate );

                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCountryList.BubbleEvent += new EventHandler( uxState_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
                PasswordTR.Visible = false;
                uxConfirmPasswordTR.Visible = false;
                uxUserName.Enabled = false;
                uxAddButton.Visible = false;

                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }
            }
            else
            {
                //uxUpdateSendMailButton.Visible = false;
                uxCommissionText.Text = DataAccessContext.Configurations.GetValue( "AffiliateCommissionRate" );
                uxCalendarPopup.SelectedDate = DateTime.Today;
                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxUpdateButton.Visible = false;
                }
                else
                {
                    Response.Redirect( "AffiliateList.aspx" );
                }
            }
            DisplaySendEmailButtonControl();
        }

        PopulateLink();
    }

    protected void uxPasswordText_PreRender( object sender, EventArgs e )
    {
        // Retain password value across postback
        uxPasswordText.Attributes["value"] = uxPasswordText.Text;
    }

    protected void uxTextBoxConfrim_PreRender( object sender, EventArgs e )
    {
        // Retain password value across postback
        uxTextBoxConfrim.Attributes["value"] = uxTextBoxConfrim.Text;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            //  AddNewAffiliate();
            //uxMessage.DisplayMessage( Resources.AffiliateMessages.AddSuccess );

            //ClearInputFields();

            if (AddNewAffiliate() != Affiliate.Null)
            {
                uxMessage.DisplayMessage( Resources.AffiliateMessages.AddSuccess );

                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            Update();
            uxMessage.DisplayMessage( Resources.AffiliateMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddSendMailButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (AddNewAffiliate() != Affiliate.Null)
            {
                AddNewAffiliate();
                SendMail();
                uxMessage.DisplayMessage( Resources.AffiliateMessages.AddSendApproveMailSuccess );

                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateSendMailButton_Click( object sender, EventArgs e )
    {
        try
        {
            Update();
            SendMail();
            uxMessage.DisplayMessage( Resources.AffiliateMessages.UpdateSendApproveMailSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxIsEnabledCheck_CheckedChanged( object sender, EventArgs e )
    {
        DisplaySendEmailButtonControl();
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
}
