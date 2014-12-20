using System;

public partial class AffiliateRegister : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( uxAffiliateDetails.GetMessage() ))
            uxMessage.DisplayError( uxAffiliateDetails.GetMessage() );
    }
}
