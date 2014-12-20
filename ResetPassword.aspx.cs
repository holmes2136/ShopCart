using System;
using Vevo;

public partial class ResetPassword : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }
    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        try
        {
            UserUtilities.ResetPasswordAndSendEmail( Request.QueryString["username"] );
            Response.Redirect( "PasswordRecoveryFinished.aspx" );
        }
        catch (VevoException ex)
        {
            uxMessage.DisplayError( "Error: " + ex.Message );
        }
    }
}
