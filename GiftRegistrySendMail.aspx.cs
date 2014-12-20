using System;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Users;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;

public partial class GiftRegistrySendMail : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private string GiftRegistryID
    {
        get
        {
            return Request.QueryString["GiftRegistryID"];
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

    private void PopulateControls()
    {
        string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName );
        Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );

        GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID );
        string giftRegistryLink = UrlPath.StorefrontUrl + "GiftRegistryItem.aspx?GiftRegistryID=" + GiftRegistryID;

        string subjectText;
        string bodyText;

        EmailTemplateTextVariable.ReplaceGiftRegistryInvitationText( giftRegistry, giftRegistryLink, out subjectText, out bodyText );

        uxSubjectText.Text = subjectText;
        uxFromText.Text = customer.Email;
        uxEmailBodyHidden.Value = bodyText;
    }

    private void SendMail()
    {
        try
        {
            string toText = uxToText.Text.Trim();
            string[] emailList = null;

            if (toText.Length > 0)
            {
                if (toText.Contains( "," ) || toText.Contains( ";" ))
                {
                    GetEmailList( toText, out emailList );
                }

                if (emailList != null)
                {
                    for (int i = 0; i <= emailList.Length - 1; i++)
                    {
                        sendingMail( emailList[i] );
                    }
                }
                else
                {
                    sendingMail( toText );
                }
            }

            Response.Redirect( "GiftRegistrySendMailFinished.aspx" );
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            uxMessage.DisplayError( "[$ErrorSendMail]" );
        }
    }

    private void sendingMail( string email )
    {
        string emailBody = uxEmailBodyHidden.Value + uxEmailBodyText.Text;
        string subjectText = uxSubjectText.Text.Trim();
        MailAddress mailAddr = new MailAddress( uxFromText.Text.Trim() );

        WebUtilities.SendHtmlMail(
            mailAddr,
            email,
            subjectText,
            emailBody
        );
    }

    private void GetEmailList( string email, out string[] listEmail )
    {
        listEmail = email.Split( char.Parse( ";" ), char.Parse( "," ) );
    }

    private bool IsValidUserName( string userName )
    {
        return Page.User.Identity.IsAuthenticated &&
            String.Compare( userName, Page.User.Identity.Name, true ) == 0;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        string userName = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID ).UserName;
        if (!IsValidUserName( userName ))
        {
            uxErrorLiteral.Visible = true;
            uxGiftRegistrySendMailPanel.Visible = false;
        }
        else
        {
            if (!IsPostBack)
                PopulateControls();
        }
    }

    protected void uxSubmit_Click( object sender, EventArgs e )
    {
        if (uxCaptchaControl.UserValidated)
        {
            SendMail();
        }
    }
}
