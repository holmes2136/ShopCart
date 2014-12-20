using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Shared.DataAccess;

public partial class Mobile_Themes_MobileTheme_LayoutControls_Default : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        string applicationPath = HttpContext.Current.Request.ApplicationPath;
        string localPath = HttpContext.Current.Request.Url.LocalPath;
        string uri = HttpContext.Current.Request.Url.AbsoluteUri;

        int storefrontPath = uri.Length - (localPath.Length - applicationPath.Length);

        uxPromotionLink.NavigateUrl = "~/" + Vevo.UrlManager.MobileFolder + "/Promotion.aspx";
        liPromotionLink.Visible = Vevo.Domain.DataAccessContext.Configurations.GetBoolValue( "EnableBundlePromo", Vevo.WebUI.StoreContext.CurrentStore ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
    }
}
