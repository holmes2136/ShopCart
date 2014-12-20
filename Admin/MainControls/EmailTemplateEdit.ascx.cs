using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Domain.EmailTemplates;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_EmailTemplateEdit : AdminAdvancedBaseUserControl
{

    protected string Name
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["Name"] ))
                return MainContext.QueryString["Name"];
            else
                return "";
        }
    }

    private string EmailTemplateID
    {
        get
        {
            return DataAccessContext.EmailTemplateDetailRepository.GetIDByNameAndStoreID( uxLanguageControl.CurrentCulture, Name, uxStoreList.CurrentSelected );
        }
    }

    private void PopulateControl()
    {
        PopulateDropdown();

        EmailTemplateDetail email = DataAccessContext.EmailTemplateDetailRepository.GetOne( uxLanguageControl.CurrentCulture, EmailTemplateID );

        if (String.IsNullOrEmpty( email.Subject ))
        {
            uxSubjectTextTR.Visible = false;
        }
        else
        {
            uxSubjectTextTR.Visible = true;
            uxSubjectText.Text = email.Subject;
        }

        uxContentText.Text = email.Body;
    }

    private void Language_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControl();
    }


    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControl();
    }

    protected void PopulateDropdown()
    {
        uxEmailKeywordDrop.Items.Clear();

        DataTable emailVariableTable = EmailTemplateTextVariable.GetVariableList( Name );

        DataView dataView = new DataView( emailVariableTable );
        dataView.Sort = "Keyword";

        uxEmailKeywordDrop.DataSource = dataView;
        uxEmailKeywordDrop.DataTextField = "Keyword";
        uxEmailKeywordDrop.DataValueField = "Value";
        uxEmailKeywordDrop.DataBind();
        uxEmailKeywordDrop.Items.Insert( 0, new ListItem( "Email Keyword...", "" ) );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Language_RefreshHandler );

        if (string.IsNullOrEmpty( EmailTemplateID ))
            MainContext.RedirectMainControl( "EmailTemplateList.ascx", "" );

        if (!MainContext.IsPostBack)
            PopulateControl();

        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    protected void uxInsertKeywordButton_Click( object sender, EventArgs e )
    {
        if (uxEmailKeywordDrop.SelectedIndex != 0)
        {
            uxContentText.Text = uxContentText.Text + uxEmailKeywordDrop.SelectedValue;
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if ((String.IsNullOrEmpty( uxSubjectText.Text ) ^ uxSubjectTextTR.Visible) &&
            !String.IsNullOrEmpty( uxContentText.Text ))
        {
            EmailTemplateDetail email = DataAccessContext.EmailTemplateDetailRepository.GetOne(
                uxLanguageControl.CurrentCulture, EmailTemplateID );
            email.Subject = uxSubjectText.Text;
            email.Body = uxContentText.Text;
            email = DataAccessContext.EmailTemplateDetailRepository.Save( email );

            uxMessage.DisplayMessage( "Updated Successfully." );
            PopulateControl();
        }
    }

    protected void uxEmailKeywordDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        //uxKeywordDescription.Text = uxEmailKeywordDrop.SelectedValue;
    }

    protected void uxPreviewButton_Click( object sender, EventArgs e )
    {
        string emailBody = string.Empty;

        uxPreviewSubjectLabel.Text = uxSubjectText.Text;
        uxPreviewEmailBodyLabel.Text = uxContentText.Text;

        uxPreviewButtonModalPopup.Show();
    }

    protected void uxTestSendButton_Click( object sender, EventArgs e )
    {
        uxTestSendFromText.Text = "SenderEmail@127.0.0.1";
        uxTestSendToText.Text = "ReceiverEmail@127.0.0.1";
        uxTestSendEmailBodyText.Text = uxContentText.Text;
        uxTestSendSubjectText.Text = uxSubjectText.Text;

        uxTestSendButtonModalPopup.Show();
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
                    uxTestSendEmailBodyText.Text );
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
}
