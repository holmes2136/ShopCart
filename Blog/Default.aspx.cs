using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Blog_Default : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void PopulateControl()
    {
        try
        {
            Control userConttrol = LoadControl( String.Format( "Themes/{0}/LayoutControls/Default.ascx", StyleSheetTheme ) );
            userConttrol.ID = "uxDefaultLayout";
            uxDefaultPanel.Controls.Add( userConttrol );
        }
        catch (Exception ex)
        {
            SaveLogFile.SaveLog( ex );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        string title = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        if (String.IsNullOrEmpty( title ))
            title = DataAccessContext.Configurations.GetValue( CultureUtilities.BaseCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        Page.Title = title;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", StoreContext.CurrentStore ))
            Response.Redirect( "~/Default.aspx" );
        PopulateControl();
    }
}
