using System;
using Vevo.Domain;
using Vevo.WebUI.International;

public partial class RegisterFinish : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void PopulateMessage()
    {
        string message = String.Empty;
        if (DataAccessContext.Configurations.GetBoolValue( "CustomerAutoApprove" ))
        {
            Response.Redirect( "Default.aspx" );
        }
        else
        {
            message = "[$FinishWithManualApprove]";
        }
        uxMessage.DisplayMessage( message );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateMessage();
    }
}
