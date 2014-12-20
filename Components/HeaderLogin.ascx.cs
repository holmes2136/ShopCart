using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI.BaseControls;
using System.Web;

public partial class Components_HeaderLogin : BaseLayoutControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        HyperLink uxMyAccountLink = (HyperLink) uxLoginView.FindControl( "uxMyAccount1" );

        if (uxMyAccountLink != null)
        {
            if (Roles.IsUserInRole( "Affiliates" ))
                uxMyAccountLink.NavigateUrl = "~/affiliatedashboard.aspx";
            else
                uxMyAccountLink.NavigateUrl = "~/accountdashboard.aspx";
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (!Roles.IsUserInRole( "Affiliates" ))
            {
                Panel uxWishlistPanel2 = (Panel) uxLoginView.FindControl( "uxWishlistLoggedInDiv" );
                uxWishlistPanel2.Visible = IsWishListLinkVisible();
            }
        }
        else
        {
            Panel uxWishlistPanel1 = (Panel) uxLoginView.FindControl( "uxWishlistAnonymousDiv" );
            uxWishlistPanel1.Visible = IsWishListLinkVisible();
        }

    }

    protected bool IsWishListLinkVisible()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ))
            return true;
        else
            return false;

    }
}
