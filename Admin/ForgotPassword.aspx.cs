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
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class AdminAdvance_ForgotPassword : AdminAdvancedBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebUtilities.TieButton( this.Page, uxUserNameText, uxSubmitButton );
    }

    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        try
        {
            UrlPath urlPath = new UrlPath( Request.Url.AbsoluteUri );
            string currentURL = UrlPath.StorefrontUrl + urlPath.ExtractFirstApplicationSubfolder() + "/";

            UserUtilities.SendResetPasswordConfirmationEmail( uxUserNameText.Text, currentURL );

            uxMessage.DisplayMessage( "Please check your email for the password reset confirmation link." );
            //Response.Redirect( "PasswordRecoveryFinished.aspx" );
        }
        catch (VevoException ex)
        {
            uxMessage.DisplayError( "Error: " + ex.Message );
        }
    }

}
