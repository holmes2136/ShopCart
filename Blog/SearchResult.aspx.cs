using System;
using System.Web.UI;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Blog_SearchResult : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentSearch
    {
        get
        {
            if ( Request.QueryString[ "Search" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "Search" ].ToString();
        }
    }
    
    private void PopulateUserControl()
    {
        UserControl blogListControl = new UserControl();

        blogListControl = LoadControl( String.Format( "{0}{1}",
                    SystemConst.LayoutBlogListPath,
                    DataAccessContext.Configurations.GetValue( "DefaultBlogListLayout" ), StoreContext.CurrentStore ) )
                as UserControl;

        uxBlogListPanel.Controls.Add( blogListControl );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", StoreContext.CurrentStore ) )
            Response.Redirect( "~/Default.aspx" );

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxDefaultTitle.Text += " : " + CurrentSearch;
    }
}
