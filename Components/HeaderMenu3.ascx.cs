using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Components_HeaderMenu3 : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateMenu()
    {
        HtmlGenericControl ul = new HtmlGenericControl( "ul" );

        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );


        foreach (Category category in categoryList)
        {
            HtmlGenericControl li = new HtmlGenericControl( "li" );
            HtmlGenericControl div1 = new HtmlGenericControl( "div" );
            HtmlGenericControl div2 = new HtmlGenericControl( "div" );

            div1.Attributes.Add( "class", "HeaderMenuNavItemLeft" );
            div2.Attributes.Add( "class", "HeaderMenuNavItemRight" );

            if (DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( category.CategoryID ))
            {
                Menu CategoryDropDownMenu = new Menu();
                int MaxNode = DataAccessContext.Configurations.GetIntValue( "CategoryDynamicDropDownLevel" );
                CategoryDropDownMenu.CssClass = "ContentMenuNavMenuList";
                CategoryDropDownMenu.StaticHoverStyle.CssClass = "ContentMenuNavMenuListStaticHover";
                CategoryDropDownMenu.StaticMenuItemStyle.CssClass = "ContentMenuNavListStaticMenuItem";
                CategoryDropDownMenu.StaticSelectedStyle.CssClass = "ContentMenuNavMenuListStaticSelectItem";
                CategoryDropDownMenu.StaticMenuStyle.CssClass = "ContentMenuNavMenuListStaticMenuStyle";
                CategoryDropDownMenu.DynamicHoverStyle.CssClass = "ContentMenuNavMenuListDynamicHover";
                CategoryDropDownMenu.DynamicMenuItemStyle.CssClass = "ContentMenuNavMenuListDynamicMenuItem";
                CategoryDropDownMenu.DynamicSelectedStyle.CssClass = "ContentMenuNavMenuListDynamicSelectItem";
                CategoryDropDownMenu.DynamicMenuStyle.CssClass = "ContentMenuNavMenuListDynamicMenuStyle";
                CategoryDropDownMenu.StaticEnableDefaultPopOutImage = false;
                CategoryDropDownMenu.Orientation = Orientation.Horizontal;
                CategoryDropDownMenu.MaximumDynamicDisplayLevels = MaxNode;

                MenuItem rootMenu = new MenuItem();
                rootMenu.Text = category.Name;
                rootMenu.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );

                IList<Category> list = DataAccessContext.CategoryRepository.GetByParentID(
                    StoreContext.Culture,
                    category.CategoryID,
                    "SortOrder",
                    BoolFilter.ShowTrue );
                foreach (Category subcategory in list)
                {
                    rootMenu.ChildItems.Add( CreateMenuItemWithChildren( subcategory ) );
                }

                CategoryDropDownMenu.Items.Add( rootMenu );

                div2.Controls.Add( CategoryDropDownMenu );
            }
            else
            {
                HyperLink hl = new HyperLink();

                hl.CssClass = "HyperLink";
                hl.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
                hl.Text = category.Name;

                div2.Controls.Add( hl );
                
            }

            div1.Controls.Add( div2 );
            li.Controls.Add( div1 );
            ul.Controls.Add( li );

        }
        uxHeaderMenu3PlaceHolder.Controls.Add( ul );
    }

    private void PopulateCategories( MenuItem item, string parentCategoryID )
    {
        IList<Category> list = DataAccessContext.CategoryRepository.GetByParentID(
            StoreContext.Culture,
            parentCategoryID,
            "SortOrder",
            BoolFilter.ShowTrue );

        foreach (Category category in list)
        {
            item.ChildItems.Add( CreateMenuItemWithChildren( category ) );
        }
    }

    protected MenuItem CreateMenuItemWithChildren( Category category )
    {
        MenuItem newItem = new MenuItem();

        if (DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( category.CategoryID ))
        {
            newItem.Text = category.Name;
            newItem.Value = category.CategoryID;
            newItem.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );

            PopulateCategories( newItem, category.CategoryID );
        }
        else
        {
            newItem.Text = category.Name;
            newItem.Value = category.CategoryID;
            newItem.NavigateUrl = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
        }

        return newItem;
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
