using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.WebUI.Marketing;

public partial class Mobile_Themes_Mobile : Vevo.WebUI.BaseControls.BaseMobileMasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "RestrictAccessToShop" ))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                if (IsRestrictedAccessPage())
                {
                    Response.Redirect( "Login.aspx" );
                }
            }
        }

        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                uxPriceRequireLoginPanel.Visible = true;
            }
        }

        string applicationPath = HttpContext.Current.Request.ApplicationPath;
        string localPath = HttpContext.Current.Request.Url.LocalPath;
        string uri = HttpContext.Current.Request.Url.AbsoluteUri;

        int storefrontPath = uri.Length - (localPath.Length - applicationPath.Length);

        uxFullSiteHyperLink.NavigateUrl = uri.Substring( 0, storefrontPath ) + "?IsViewMobile=false";

        AffiliateHelper.SetAffiliateCookie( AffiliateCode );
    }
}
