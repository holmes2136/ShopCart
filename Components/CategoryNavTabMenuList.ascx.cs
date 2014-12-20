using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_CategoryNavTabMenuList : BaseLanguageUserControl
{
    private void PopulateControls()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", StoreContext.CurrentStore );

        uxList.DataSource = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
                                StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );
        uxList.DataBind();
    }

    private void Components_CategoryNavTabMenuList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_CategoryNavTabMenuList_StoreCultureChanged );

        PopulateControls();
    }

    protected bool IsLeaf( object dataItem )
    {
        Category category = (Category) dataItem;
        return category.IsLeafCategory();
    }

    protected string GetURL( object dataItem )
    {
        Category category = (Category) dataItem;
        return UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
    }
}
