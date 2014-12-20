using System;
using System.Web.UI;
using Vevo.Deluxe.WebUI.Base;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Blog_CategoryList : BaseLicenseLanguagePage
{
    private string CurrentCategory
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
            {
                return String.Empty;
            }
            else
            {
                string categoryURL = Request.QueryString["CategoryName"].ToString();
                return DataAccessContext.BlogCategoryRepository.GetBlogCategoryNameFromBlogCategoryURL(categoryURL);
            }
                
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
        if (!DataAccessContext.Configurations.GetBoolValue( "BlogEnabled", StoreContext.CurrentStore ))
            Response.Redirect( "~/Default.aspx" );

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxDefaultTitle.Text += " : " + CurrentCategory;
    }
}