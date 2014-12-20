using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Blog_BlogList : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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

    private string ConvertNumberToMonth( string month )
    {
        DateTime date = new DateTime( 2000, ConvertUtilities.ToInt32( month ), 1 );
        return date.ToString( "MMMM" );
    }

    private void RefreshTitle()
    {
        string title = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        if (String.IsNullOrEmpty( title ))
            title = DataAccessContext.Configurations.GetValue( CultureUtilities.BaseCultureID, "BlogPageTitle", StoreContext.CurrentStore );

        this.Page.Title = title;

        string[] archiveDate = CurrentBlogListName.Split( '_' );
        uxDefaultTitle.Text = "[$BlogListHeader] " + ConvertNumberToMonth( archiveDate[0] ) + " " + archiveDate[1];
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
