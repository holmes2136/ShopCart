using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI.International;

public partial class Components_Newsletter : BaseLanguageUserControl
{
    private void RegisterScript()
    {
        uxEmailSubscribeText.Text = "[$JoinNewsletter]";
        uxEmailSubscribeText.Attributes.Add( "onblur", "javascript:if(this.value=='') this.value='"+"[$JoinNewsletter]"+"';" );
        uxEmailSubscribeText.Attributes.Add( "onfocus", "javascript:if(this.value=='" + "[$JoinNewsletter]" + "') this.value='';" );
    }

    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxEmailSubscribeText, uxEmailImageButton );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterSubmitButton();
        if ( DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ) && !Page.User.Identity.IsAuthenticated )
        {
            uxEmailSubscribeText.Enabled = false;
            uxEmailImageButton.Enabled = false;
        }
        if ( !IsPostBack )
        {
            RegisterScript();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "NewsletterModuleDisplay" ) )
            this.Visible = false;
    }

    protected void uxEmailImageButton_Click( object sender, EventArgs e )
    {
        if ( !uxEmailSubscribeText.Text.Contains( "Join our newsletter" ) || String.IsNullOrEmpty( uxEmailSubscribeText.Text.Trim() ) )
            Response.Redirect( "Newsletter.aspx?Email=" + HttpUtility.UrlEncode( EncryptEmail( uxEmailSubscribeText.Text ) ) + "&Act=register" );
        else
            Response.Redirect( "Newsletter.aspx" );
    }

    private string EncryptEmail( string email )
    {
        string originalKey = ConfigurationManager.AppSettings[ "SecretKey" ];
        return SymmetricEncryption.Encrypt( email, originalKey );
    }
}
