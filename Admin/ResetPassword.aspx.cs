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
using Vevo.WebAppLib;

public partial class AdminAdvanced_ResetPassword : AdminAdvancedBasePage
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }
    protected void uxResetPasswordButton_Click( object sender, EventArgs e )
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
