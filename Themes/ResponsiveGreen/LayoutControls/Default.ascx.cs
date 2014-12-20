using System;
using System.Web;
using System.Web.UI;

public partial class Themes_ResponsiveGreen_LayoutControls_Default : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability( HttpCacheability.NoCache );
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader( "Pragma", "no-cache" );
    }
}
