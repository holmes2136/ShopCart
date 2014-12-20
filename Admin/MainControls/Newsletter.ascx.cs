using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.Marketing;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Domain.EmailTemplates;

public partial class AdminAdvanced_MainControls_Newsletter : AdminAdvancedBaseUserControl
{
    private string returnPath = "Newsletter.ascx";

    #region private

    private string ReplaceEmail( string emailFromTextBox, string email, string emailHash, string storeID )
    {
        return EmailTemplateTextVariable.ReplaceNewsLetterText(emailFromTextBox, email, emailHash, storeID );
    }

    private int GetSubscribersCountFromStoreList()
    {
        string storeID = uxStoreList.CurrentSelected;
        IList<NewsLetter> newsLetterList;
        if (storeID == null | storeID == string.Empty)
        {
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAll( "Email" );
        }
        else
        {
            Store store = DataAccessContext.StoreRepository.GetOne( storeID );
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAllByStore( store );
        }
        return newsLetterList.Count;
    }

    private string SelectedStoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
                return uxStoreList.CurrentSelected;
            else
                return Store.RegularStoreID;
        }
    }

    #endregion

    #region protected

    protected void Page_Load( object sender, EventArgs e )
    {
        IList<NewsLetter> newsLetterList;

        if (KeyUtilities.IsMultistoreLicense())
        {
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAll( "Email" );
        }
        else
        {
            Store store = DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID );
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAllByStore( store );
            uxStoreListLabel.Visible = false;
        }


        uxSubscribersLabel.Text = newsLetterList.Count.ToString();
        uxStoreSubscribersLabel.Text = newsLetterList.Count.ToString();

        if (IsAdminModifiable())
        {
            uxSendButton.Visible = true;
            uxTestSendButton.Visible = true;
        }
        else
        {
            uxSendButton.Visible = false;
            uxTestSendButton.Visible = false;
        }

        if (!MainContext.IsPostBack)
        {
            uxFromText.Text = DataAccessContext.Configurations.GetValue( "CompanyEmail" );

            uxMailStartText.Text = "1";
            uxMailEndText.Text = newsLetterList.Count.ToString();
            if (newsLetterList.Count == 0)
            {
                uxMailStartText.Text = "0";
                uxMailEndText.Text = "0";
            }
            SetEmailBody();
        }
    }

    protected void uxSendButton_Click( object sender, EventArgs e )
    {
        string storeID = SelectedStoreID;
        IList<NewsLetter> newsLetterList;
        if (storeID == null || storeID == string.Empty)
        {
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAll( "Email" );
        }
        else
        {
            Store store = DataAccessContext.StoreRepository.GetOne( storeID );
            newsLetterList = DataAccessContext.NewsLetterRepository.GetAllByStore( store );
        }

        if (uxEmailBodyText.Text == string.Empty)
        {
            uxMessage.DisplayError( Resources.NewsletterMessage.EmptyContentError );
            return;
        }

        if (newsLetterList.Count == 0)
        {
            uxMessage.DisplayError( Resources.NewsletterMessage.EmptySubscriber );
            return;
        }

        int mailStart = int.Parse( uxMailStartText.Text );
        int mailEnd = int.Parse( uxMailEndText.Text );

        if (mailStart == 0)
            mailStart = 1;
        if (mailEnd == 0)
            mailEnd = 1;

        if (mailEnd > newsLetterList.Count)
        {
            AdminMessageMainControl.RedirectToMessagePage( MainContext, Resources.NewsletterMessage.SendSuccessHead, String.Format( Resources.NewsletterMessage.NotValidMaxmail, newsLetterList.Count ), returnPath );

        }

        try
        {
            int mailSendCount = 0;
            for (int i = mailStart - 1; i < mailEnd; i++)
            {
                try
                {
                    string emailHash = newsLetterList[i].EmailHash;
                    string email = newsLetterList[i].Email;
                    string emailBody = ReplaceEmail( uxEmailBodyText.Text, email, emailHash, newsLetterList[i].StoreID );

                    Store store = DataAccessContext.StoreRepository.GetOne( newsLetterList[i].StoreID );
                    WebUtilities.SendHtmlMail( uxFromText.Text, email, uxSubjectText.Text, emailBody, store );
                    mailSendCount++;
                }
                catch (Exception ex)
                {
                    //uxMessage.DisplayException( ex );
                    //return;
                    if (mailSendCount == 0)
                    {
                        AdminMessageMainControl.RedirectToMessagePage( MainContext, Resources.NewsletterMessage.SendUnSuccessHead, string.Format( Resources.NewsletterMessage.SendZeroSuccessBody, ex, mailSendCount ), returnPath );
                    }
                    else
                    {
                        AdminMessageMainControl.RedirectToMessagePage( MainContext, Resources.NewsletterMessage.SendUnSuccessHead, string.Format( Resources.NewsletterMessage.SendUnSuccessBody, ex,
                                mailSendCount, mailEnd - (mailStart - 1), mailStart, mailStart + (mailSendCount - 1) ), returnPath );
                    }
                }
            }

            AdminMessageMainControl.RedirectToMessagePage( MainContext, Resources.NewsletterMessage.SendSuccessHead, String.Format( Resources.NewsletterMessage.SendSuccessBody, mailSendCount, mailStart, mailStart + (mailSendCount - 1) ), returnPath );
        }
        catch (Exception ex)// in case of an error
        {
            uxMessage.DisplayError( "Error : " + ex );
            //uxMessage.DisplayException( ex );
        }
    }

    protected void SetEmailBody()
    {
        string emailID = DataAccessContext.EmailTemplateDetailRepository.GetIDByNameAndStoreID(
                            StoreContext.Culture,
                            "Newsletter Layout",
                            new StoreRetriever().GetCurrentStoreID() );

        EmailTemplateDetail emailDetail = DataAccessContext.EmailTemplateDetailRepository.GetOne( StoreContext.Culture, emailID );

        uxEmailBodyText.Text = emailDetail.Body;
    }

    protected void uxPreviewButton_Click( object sender, EventArgs e )
    {
        string emailBody = string.Empty;

        string emailHash = string.Empty;
        string email = string.Empty;

        emailHash = "PreviewKey";
        email = "Preview@Email.Com";

        emailBody = ReplaceEmail( uxEmailBodyText.Text, email, emailHash, new StoreRetriever().GetCurrentStoreID() );

        uxPreviewFromLabel.Text = uxFromText.Text;
        uxPreviewEmailBodyLabel.Text = emailBody;
        uxPreviewSubjectLabel.Text = uxSubjectText.Text;

        uxPreviewButtonModalPopup.Show();
    }

    protected void uxTestSendButton_Click( object sender, EventArgs e )
    {
        uxTestSendButtonModalPopup.Show();

        string emailBody = string.Empty;
        string emailHash = string.Empty;
        string email = string.Empty;

        emailHash = "TestKey";
        email = "TestSend@Email.Com";

        emailBody = ReplaceEmail( uxEmailBodyText.Text, email, emailHash, new StoreRetriever().GetCurrentStoreID() );

        uxTestSendFromText.Text = uxFromText.Text;
        uxTestSendEmailBodyText.Text = emailBody;
        uxTestSendSubjectText.Text = uxSubjectText.Text;
    }

    protected void uxTestSendSendButton_Click( object sender, EventArgs e )
    {
        string[] receiver = uxTestSendToText.Text.ToString().Split( char.Parse( "," ) );
        int count = receiver.Length;

        if (uxTestSendToText.Text == string.Empty)
        {
            uxTestSendMessage.DisplayError( Resources.NewsletterMessage.TestSendEmptyReceiverEmail );
            uxTestSendButtonModalPopup.Show();
            return;
        }
        try
        {
            for (int i = 0; i <= receiver.Length - 1; i++)
            {
                WebUtilities.SendHtmlMail(
                    uxTestSendFromText.Text,
                    receiver[i].ToString(),
                    uxTestSendSubjectText.Text,
                    ReplaceEmail( uxTestSendEmailBodyText.Text, receiver[i].ToString(), "TestKey", new StoreRetriever().GetCurrentStoreID() ) );

            }
            uxTestSendMessage.DisplayMessage( Resources.NewsletterMessage.TestSendSuccess );
        }
        catch (Exception ex)
        {
            uxTestSendMessage.DisplayError( Resources.NewsletterMessage.TestSendUnSuccess, ex );
            uxTestSendButtonModalPopup.Show();
        }
        uxTestSendButtonModalPopup.Show();
    }

    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        int subscribersCount = GetSubscribersCountFromStoreList();
        uxStoreSubscribersLabel.Text = subscribersCount.ToString();

        uxMailStartText.Text = "1";
        uxMailEndText.Text = subscribersCount.ToString();
        if (subscribersCount == 0)
        {
            uxMailStartText.Text = "0";
            uxMailEndText.Text = "0";
        }
    }

    #endregion
}
