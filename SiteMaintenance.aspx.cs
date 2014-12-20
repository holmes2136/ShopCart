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
using Vevo.Domain;
using Vevo;

public partial class SiteMaintenance : System.Web.UI.Page
{
    protected void Page_PreRender( object sender, EventArgs e )
    {
        string title = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "Title" );

        if (String.IsNullOrEmpty( title ))
            title = DataAccessContext.Configurations.GetValue( CultureUtilities.BaseCultureID, "Title" );

        Page.Title = title;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        
    }

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }
}
