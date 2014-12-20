using System;
using Vevo;
using Vevo.Domain;

public partial class Mobile_MyAccount : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ))
            uxWishListDiv.Visible = false;
    }

    protected void uxLoginStatus_LoggedOut( object sender, EventArgs e )
    {
    }
}
