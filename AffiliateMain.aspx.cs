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

public partial class AffiliateMain : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Response.Redirect("~/AffiliateDashboard.aspx");
    }

    protected void uxLoginStatus_LoggedOut( object sender, EventArgs e )
    {
    
    }
}
