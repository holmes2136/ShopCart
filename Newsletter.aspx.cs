using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Newsletter : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    public string Email
    {
        get
        {
            return DecryptEmail( Request.QueryString["Email"] );
        }
    }

    public string Key
    {
        get
        {
            return Request.QueryString["Key"];
        }
    }

    public string EmailID
    {
        get
        {
            return Request.QueryString["EmailID"];
        }
    }

    public string ActionCode
    {
        get
        {
            return Request.QueryString["Act"];
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        StoreRetriever storeRetriever = new StoreRetriever();
        Store store = storeRetriever.GetStore();
        if (ActionCode == null || ActionCode == string.Empty)
        {
            uxNewsletterSubScribeTR.Visible = true;
            uxNewsletterMessageTR.Visible = false;
        }
        else
        {
            uxNewsletterSubScribeTR.Visible = false;
            uxNewsletterMessageTR.Visible = true;
            switch (ActionCode.ToLower())
            {
                case "register":
                    SendConfirmationEmail();
                    break;
                case "registerbox":
                    uxNewsletterSubScribeTR.Visible = true;
                    SendConfirmationEmail();
                    break;
                case "unsubscribe":
                    if (DataAccessContext.NewsLetterRepository.DeleteEmail( Key, store ))
                        uxSubscribeLabel.Text = "[$UnsubscribeSuccess]";
                    else
                        uxSubscribeLabel.Text = "[$UnsubscribeFail]";
                    uxEmailLabel.Text = Email;
                    break;
                case "confirm":
                    RegisterEmail();
                    break;
                default:
                    uxSubscribeLabel.Text = "";
                    break;
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieButton( this.Page, uxEmailSubscribeText, uxNewsletterImageButton );
        
    }

    protected void uxNewsletterImageButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (String.IsNullOrEmpty( uxEmailSubscribeText.Text ))
                uxMessage.DisplayMessage( "[$Required Email Address]" );
            else
                Response.Redirect( "Newsletter.aspx?Email=" + HttpUtility.UrlEncode( EncryptEmail( uxEmailSubscribeText.Text ) ) + "&Act=registerbox" );
        }
        catch (VevoException ex)
        {
            uxMessage.DisplayError( "Error: " + ex.Message );
        }
    }

    private void RegisterEmail()
    {
        StoreRetriever storeRetriever = new StoreRetriever();
        Store store = storeRetriever.GetStore();
        if (Email == null | Email == string.Empty)
        {
            EmailDiv.Visible = false;
            uxSubscribeLabel.Text = "[$RegisterEmpty]";
            return;
        }
        Regex emailregex = new Regex( @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" );
        Match m = emailregex.Match( Email );

        if (m.Success)
        {
            string emailHash =
                SecurityUtilities.HashMD5( Email + WebConfiguration.SecretKey );

            NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( Email, store );
            if (newsLetter.IsNull)
            {
                newsLetter.Email = Email;
                newsLetter.EmailHash = emailHash;
                newsLetter.JoinDate = DateTime.Now;
                newsLetter.StoreID = store.StoreID;
                DataAccessContext.NewsLetterRepository.Create( newsLetter );
                uxSubscribeLabel.Text = "[$RegisterSuccess]";
                uxNewsletterSubScribeTR.Visible = false;
            }
            else
                uxSubscribeLabel.Text = "[$RegisterAlready]";

        }
        else
        {
            uxNewsletterSubScribeTR.Visible = true;
            uxSubscribeLabel.ForeColor = System.Drawing.Color.Red;
            uxSubscribeLabel.Text = "[$RegisterInvalidEmail]";
        }
        uxEmailLabel.Text = Email;
    }

    private void SendConfirmationEmail()
    {
        uxEmailLabel.Text = Email;
        uxSubscribeLabel.Text = "[$ConfirmSubscribeEMail]";

        string subjectText;
        string bodyText;

        string confirmLink = UrlPath.StorefrontUrl + "SubscribeConfirm.aspx?Email=" + HttpUtility.UrlEncode( EncryptEmail( Email ) );

        EmailTemplateTextVariable.ReplaceConfirmationSubscribeText( Email, confirmLink, out subjectText, out bodyText );

        try
        {
            WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail,
                Email,
                subjectText,
                bodyText );
        }
        catch (Exception)
        {
            uxMessage.DisplayError( "[$SentErrorMessage]" );
            uxNewsletterMessageTR.Visible = false;
        }
    }

    private string EncryptEmail( string email )
    {
        string originalKey = ConfigurationManager.AppSettings["SecretKey"];
        return SymmetricEncryption.Encrypt( email, originalKey );
    }

    private string DecryptEmail( string emailHash )
    {
        string originalKey = ConfigurationManager.AppSettings["SecretKey"];
        return SymmetricEncryption.Decrypt( emailHash, originalKey );
    }
}
