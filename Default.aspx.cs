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
using Vevo.Domain;

public partial class _Default : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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
        string title = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "Title" );
        
        if(String.IsNullOrEmpty(title))
            title = DataAccessContext.Configurations.GetValue( CultureUtilities.BaseCultureID, "Title" );
        
        Page.Title = title;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControl();
    }
}

