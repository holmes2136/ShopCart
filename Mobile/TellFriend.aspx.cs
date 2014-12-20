using System;
using System.Net.Mail;
using System.Web.Security;
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
        if (!IsPostBack)
        {
            ClearData();
            uxSenderText.Text = string.Empty;

            MembershipUser user = Membership.GetUser();
            if (user != null &&
                Roles.IsUserInRole( user.UserName, "Customers" ))
            {
                string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( user.UserName );
                Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
                uxSenderText.Text = customer.Email;
            }
        }
    }

    protected void SetUpEmail( Product product, out string subject, out string contentBody )
    {
        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceTellFriendText(
            product,
            out subjectText,
            out bodyText );

        subject = subjectText;
        contentBody = bodyText + "<br/>";
    }

    protected void SetUpEmailPromotion( PromotionGroup group, out string subject, out string contentBody )
    {
        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceTellFriendPromotionText(
            group,
            out subjectText,
            out bodyText );

        subject = subjectText;
        contentBody = bodyText + "<br/><br/>";
    }
    private void ClearData()
    {
        uxSenderText.Text = string.Empty;
        uxRecipientText.Text = string.Empty;
        uxMessage.Text = string.Empty;
    }

    private string AddHeaderText( string emailBody )
    {
        return String.Format(
            "A message from your friend!<br/>" +
            "Sender Email address: {0}<br/>" +
            "---------------------------------<br/><br/>" +
            "{1}",
            uxSenderText.Text,
            emailBody );
    }

    protected void uxSubmit_Click( object sender, EventArgs e )
    {
        if (!uxCaptchaControl.UserValidated)
            return;

        string subject = string.Empty;
        string contentBody = string.Empty;

        if (!String.IsNullOrEmpty( CurrentProductID ))
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, CurrentProductID, new StoreRetriever().GetCurrentStoreID() );

            SetUpEmail( product, out subject, out contentBody );
        }
        else if (!String.IsNullOrEmpty( PromotionGroupID ))
        {
            PromotionGroup group = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( StoreContext.Culture, StoreContext.CurrentStore.StoreID, PromotionGroupID, BoolFilter.ShowTrue );

            SetUpEmailPromotion( group, out subject, out contentBody );
        }

        string emailBody = contentBody + uxMessage.Text;

        emailBody = AddHeaderText( emailBody );

        try
        {
            MailAddress mailAddr = new MailAddress( uxSenderText.Text.Trim() );
            WebUtilities.SendHtmlMail(
                DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                uxRecipientText.Text.Trim(),
                subject,
                emailBody
             );
            Response.Redirect( "TellFriendFinished.aspx" );
        }
        catch (Exception)
        {
            uxErrorMessage.DisplayError( "[$SentErrorMessage]" );
        }
    }
}
