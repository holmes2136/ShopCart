using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Blog_Themes_BlogTheme_LayoutControls_Default : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string CurrentBlogListName
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["BlogListName"] ))
            {
                return String.Empty;
            }
            else
            {
                return Request.QueryString["BlogListName"];
            }
        }
    }

    private void PopulateControls()
    {

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

    private void RefreshTitle()
    {
        string title = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        if (String.IsNullOrEmpty( title ))
            title = DataAccessContext.Configurations.GetValue( CultureUtilities.BaseCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        this.Page.Title = title;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", StoreContext.CurrentStore ))
            Response.Redirect( "~/Default.aspx" );

        if (!IsPostBack)
        {
            PopulateControls();
        }

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshTitle();
    }
}
