using System;

public partial class AffiliateDetailsPage : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxAffiliateDetails.SetEditMode();
    }
    protected void Page_PreRender( object sender, EventArgs e )
    {
        string message = uxAffiliateDetails.GetMessage();
        if (!String.IsNullOrEmpty( message ))
        {
            if (message.Contains( "UpdateComplete" ))
                uxMessage.DisplayMessage( message );
            else
                uxMessage.DisplayError( message );
        }
    }
}
