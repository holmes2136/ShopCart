using System;
using System.Collections;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo;

public partial class Components_CategoryNavMenuList : BaseCategoryMenuNavListUserControl
{
    private string CategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (String.IsNullOrEmpty( id ))
                return "0";
            else
                return id;
        }
    }

    private string CategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"];
        }
    }
    
    private void PopulateControls()
    {
        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        uxCategoryNavListMenu.Items.Clear();

        uxCategoryNavListMenu.MaximumDynamicDisplayLevels = DataAccessContext.Configurations.GetIntValue( "CategoryMenuLevel" );

        CategoryNavMenuBuilder menuBuilder = new CategoryNavMenuBuilder(
            StoreContext.Culture, UrlManager.GetCategoryUrl, MaxNode );

        foreach (Category category in categoryList)
        {
            uxCategoryNavListMenu.Items.Add( menuBuilder.CreateMenuItemTree( 0, category ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public void Refresh()
    {
        PopulateControls();
    }
}
