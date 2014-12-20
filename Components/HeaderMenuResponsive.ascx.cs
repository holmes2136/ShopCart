using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_HeaderMenuResponsive : BaseLanguageUserControl
{
    private void PopulateMenu()
    {
        HtmlGenericControl ul = new HtmlGenericControl( "ul" );

        ul.Attributes.Add( "class", "left" );

        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        foreach (Category category in categoryList)
        {
            HtmlGenericControl li = new HtmlGenericControl( "li" );

            if (DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( category.CategoryID ))
            {
                li.Attributes.Add( "class", "has-dropdown" );

                StringBuilder sb = new StringBuilder();

                string categoryUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName ).Split( '/' )[1];

                sb.Append( "<a href=\"" + categoryUrl + "\">" + category.Name + "</a>" );
                sb.Append( "    <ul class=\"dropdown\">" );

                IList<Category> list = DataAccessContext.CategoryRepository.GetByParentID(
                    StoreContext.Culture,
                    category.CategoryID,
                    "SortOrder",
                    BoolFilter.ShowTrue );
                foreach (Category subcategory in list)
                {
                    sb.Append( CreateItemWithChildren( subcategory ) );
                }

                sb.Append( "    </ul>" );

                li.InnerHtml = sb.ToString();
            }
            else
            {
                HyperLink hl = new HyperLink();

                hl.CssClass = "HyperLink";
                hl.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
                hl.Text = category.Name;

                li.Controls.Add( hl );
            }

            ul.Controls.Add( li );

        }
        uxMenuPanel.Controls.Add( ul );
    }
    protected string CreateItemWithChildren( Category category )
    {
        StringBuilder sb = new StringBuilder();
        string categoryUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName ).Split( '/' )[1];

        if (DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( category.CategoryID ))
        {
            IList<Category> list = DataAccessContext.CategoryRepository.GetByParentID(
            StoreContext.Culture,
            category.CategoryID,
            "SortOrder",
            BoolFilter.ShowTrue );

            sb.Append( "<li class=\"has-dropdown\">" );
            sb.Append( "    <a href=\"" + categoryUrl + "\">" + category.Name + "</a>" );
            sb.Append( "    <ul class=\"dropdown\">" );

            foreach (Category subCategory in list)
            {
                sb.Append( CreateItemWithChildren( subCategory ) );
            }
            sb.Append( "    </ul>" );
            sb.Append( "</li>" );
        }
        else
        {
            sb.Append( "<li>" );
            sb.Append( "    <a href=\"" + categoryUrl + "\">" + category.Name + "</a>" );
            sb.Append( "</li>" );
        }

        return sb.ToString();
    }
    private bool IsTopContentMenuDisplay()
    {
        string id = DataAccessContext.Configurations.GetValue( "TopContentMenu" );

        ContentMenu contentMenu = DataAccessContext.ContentMenuRepository.GetOne( id );

        return contentMenu.IsEnabled;

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateMenu();
    }
}