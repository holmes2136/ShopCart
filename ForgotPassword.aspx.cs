using System;
using Vevo;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class ForgotPassword : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieButton( this.Page, uxUserNameText, uxSubmitButton );
    }

    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        try
        {
            UserUtilities.SendResetPasswordConfirmationEmail( uxUserNameText.Text, UrlPath.StorefrontUrl );
            uxMessage.DisplayMessage( "[$PasswordForgetoCheckMail]" );
        }
        catch (VevoException ex)
        {
            uxMessage.DisplayError( "Error: " + ex.Message );
        }
    }
}

