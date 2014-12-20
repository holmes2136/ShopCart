using System;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Blog_Blog : Vevo.WebUI.BaseControls.BaseBlogMasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }
}
