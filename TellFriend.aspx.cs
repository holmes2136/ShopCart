using System;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class TellFriend : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentProductID
    {
        get
        {
            if (Request.QueryString["ProductID"] != null)
                return Request.QueryString["ProductID"].ToString();
            else if (Request.QueryString["ProductName"] != null)
                return DataAccessContext.ProductRepository.GetProductIDFromUrlName(
                        Request.QueryString["ProductName"] );
            else
                return String.Empty;
        }
    }

    public string PromotionGroupID
    {
        get
        {
            if (Request.QueryString["PromotionID"] != null)
                return Request.QueryString["PromotionID"].ToString();
            else if (Request.QueryString["ProductName"] != null)
                return DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupIDFromUrlName(
                        Request.QueryString["ProductName"] );
            else
                return String.Empty;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack && !String.IsNullOrEmpty( CurrentProductID ))
        {
            ClearData();
            uxFromText.Text = string.Empty;

            MembershipUser user = Membership.GetUser();
            if (user != null &&
                Roles.IsUserInRole( user.UserName, "Customers" ))
            {
                string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( user.UserName );
                Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
                uxFromText.Text = customer.Email;
            }

            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, CurrentProductID, new StoreRetriever().GetCurrentStoreID() );

            SetUpEmail( product );
        }
        else if (!IsPostBack && !String.IsNullOrEmpty( PromotionGroupID ))
        {
            ClearData();
            uxFromText.Text = string.Empty;

            MembershipUser user = Membership.GetUser();
            if (user != null &&
                Roles.IsUserInRole( user.UserName, "Customers" ))
            {
                string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( user.UserName );
                Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
                uxFromText.Text = customer.Email;
            }

            PromotionGroup group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );

            SetUpEmailPromotion( group );
        }
    }

    private string GetFormatTemplate( string FileName )
    {
        string fileDir = Server.MapPath( "ContentTemplates/" );
        string sFileName = FileName;

        FileInfo finfo = new FileInfo( fileDir + sFileName );
        if (!finfo.Exists)
            return "";

        StreamReader vevoReadText = new StreamReader( fileDir + sFileName, System.Text.Encoding.Default );
        string format = vevoReadText.ReadToEnd();
        vevoReadText.Close();

        return format;
    }

    protected void SetUpEmail( Product product )
    {
        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceTellFriendText(
            product,
            out subjectText,
            out bodyText );

        uxSubjectText.Text = subjectText;
        uxEmailBodyHidden.Value = bodyText;
    }

    protected void SetUpEmailPromotion( PromotionGroup group )
    {
        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceTellFriendPromotionText(
            group,
            out subjectText,
            out bodyText );

        uxSubjectText.Text = subjectText;
        uxEmailBodyHidden.Value = bodyText;
    }
    private void ClearData()
    {
        uxFromText.Text = string.Empty;
        uxToText.Text = string.Empty;
        uxSubjectText.Text = string.Empty;
        uxEmailBodyText.Text = string.Empty;
        uxErrorMessageDiv.Visible = false;
    }

    private string AddHeaderText( string emailBody )
    {
        return String.Format(
            "A message from your friend!<br/>" +
            "Sender Email address: {0}<br/>" +
            "---------------------------------<br/><br/>" +
            "{1}",
            uxFromText.Text,
            emailBody );
    }

    protected void uxSubmit_Click( object sender, EventArgs e )
    {
        if (!uxCaptchaControl.UserValidated)
            return;

        string emailBody = uxEmailBodyHidden.Value + uxEmailBodyText.Text;

        emailBody = AddHeaderText( emailBody );

        MailAddress mailAddr = new MailAddress( uxFromText.Text.Trim() );

        try
        {
            WebUtilities.SendHtmlMail(
                DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                uxToText.Text.Trim(),
                uxSubjectText.Text.Trim(),
                emailBody
             );
        }
        catch (Exception)
        {
            uxErrorMessage.DisplayErrorNoNewLine( "[$SentErrorMessage]" );
            uxErrorMessageDiv.Visible = true;
            return;
        }

        Response.Redirect( "TellFriendFinished.aspx" );
    }
}
