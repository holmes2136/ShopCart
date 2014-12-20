using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo.Domain.Stores;

public partial class Components_CategoryDynamicDropDownMenu : BaseCategoryMenuNavListUserControl
{
    private string _rootMenuName;

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

    private void Components_CategoryDynamicDropDownMenu_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        MaxNode = DataAccessContext.Configurations.GetIntValue( "CategoryDynamicDropDownLevel" );
        uxCategoryDropDownMenu.Items.Clear();

        MenuItem rootMenu = new MenuItem();
        rootMenu.Text = RootMenuName;
        rootMenu.NavigateUrl = "~/Catalog.aspx";

        uxCategoryDropDownMenu.MaximumDynamicDisplayLevels = MaxNode;

        string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentIDAndRootID(
           StoreContext.Culture, rootID, rootID, "SortOrder", BoolFilter.ShowTrue );

        CategoryNavMenuBuilder menuBuilder = new CategoryNavMenuBuilder(
            StoreContext.Culture, UrlManager.GetCategoryUrl, MaxNode );

        foreach (Category category in categoryList)
        {
            rootMenu.ChildItems.Add( menuBuilder.CreateMenuItemTree( 0, category ) );
        }
        uxCategoryDropDownMenu.Items.Add( rootMenu );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler(
            Components_CategoryDynamicDropDownMenu_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public string RootMenuName
    {
        get { return _rootMenuName; }
        set { _rootMenuName = value; }
    }
}
