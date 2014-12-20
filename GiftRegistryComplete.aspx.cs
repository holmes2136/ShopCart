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

public partial class GiftRegistryComplete : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void uxContinueLinkButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "Catalog.aspx" );
    }
}
