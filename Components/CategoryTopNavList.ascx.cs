using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_CategoryTopNavList : BaseLanguageUserControl
{
    private string _categoryID = "0";
    private bool _hasSubCategory = false;

    private void Components_CategoryTopNavList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID(
            StoreContext.Culture, CategoryID, "SortOrder", BoolFilter.ShowTrue );

        uxListSubCategory.DataSource = categoryList;
        uxListSubCategory.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_CategoryTopNavList_StoreCultureChanged );

        PopulateControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( !_hasSubCategory )
        {
            uxListSubCategory.RepeatDirection = RepeatDirection.Vertical;
            uxListSubCategory.RepeatColumns = 1;
            uxListSubCategory.CssClass = "LeafSubCategoryDataList";
        }
        else
        {
            uxListSubCategory.RepeatDirection = RepeatDirection.Horizontal;
            uxListSubCategory.RepeatColumns = DataAccessContext.Configurations.GetIntValue( "NumberOfSubCategoryMenuColumn", StoreContext.CurrentStore );
            uxListSubCategory.CssClass = "SubCategoryDataList";
        }
    }

    protected void uxListSubCategory_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        Components_CategorySubTopNavList subCategoryControl = ( Components_CategorySubTopNavList ) e.Item.FindControl( "uxList" );
        HiddenField categoryHidden = ( HiddenField ) e.Item.FindControl( "uxSubCategoryHidden" );
        subCategoryControl.CategoryID = categoryHidden.Value;
        subCategoryControl.PopulateControl();
    }

    protected string GetURL( object dataItem )
    {
        Category category = ( Category ) dataItem;
        return UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
    }

    protected string GetName( object dataItem )
    {
        Category category = ( Category ) dataItem;
        return category.Name;
    }

    protected bool IsSubCategory( object dataItem )
    {
        Category category = ( Category ) dataItem;
        bool isLeaf = category.IsLeafCategory();

        if ( !isLeaf )
            _hasSubCategory = true;

        return !isLeaf;
    }

    #region Public
    public string CategoryID
    {
        get { return _categoryID; }
        set { _categoryID = value; }
    }

    public bool VisiblePanel
    {
        get { return uxPanelSubCategory.Visible; }
        set { uxPanelSubCategory.Visible = value; }
    }

    #endregion
}