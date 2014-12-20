using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_CategorySubTopNavList : BaseLanguageUserControl
{
    private string _categoryID = "0";

    protected void Page_Load( object sender, EventArgs e )
    {

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

    protected string GetMoreURL()
    {
        Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, CategoryID );

        return UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
    }

    public void PopulateControl()
    {
        int howManyItem = 0;
        int categoryItem = DataAccessContext.Configurations.GetIntValue( "NumberOfSubCategoryMenuItem", StoreContext.CurrentStore );

        IList<Category> _subCatalog = DataAccessContext.CategoryRepository.GetByParentID( StoreContext.Culture, _categoryID, "SortOrder",
            BoolFilter.ShowTrue, 0, categoryItem - 1, out howManyItem );

        if ( howManyItem > categoryItem )
            uxViewMorePanel.Visible = true;
        else
            uxViewMorePanel.Visible = false;

        uxSubCategoryList.DataSource = _subCatalog;
        uxSubCategoryList.DataBind();
    }

    public string CategoryID
    {
        get { return _categoryID; }
        set { _categoryID = value; }
    }
}