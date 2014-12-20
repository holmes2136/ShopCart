using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using System.Web.UI;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;

public partial class Components_CategoryNavList : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else
            {
                Category category = DataAccessContext.CategoryRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentCategoryName );
                return category.CategoryID;
            }
        }
    }

    private string CurrentCategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"];
        }
    }

    private Category CurrentCategory
    {
        get
        {
            return DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, CurrentCategoryID );
        }
    }

    private bool IsCategoryList()
    {
        if (Request.Url.AbsoluteUri.ToLower().Contains( "catalog.aspx" ) && ConvertUtilities.ToInt32( CurrentCategoryID ) > 0)
            return true;
        else
            return false;
    }

    private bool IsFacetedVisible()
    {
        if (IsCategoryList())
        {
            return DataAccessContext.Configurations.GetBoolValue( "FacetedSearchEnabled", StoreContext.CurrentStore ) && CurrentCategory.IsAnchor;
        }
        else
            return false;
    }

    private bool IsParentOfOtherCategories( string parentID )
    {
        return DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( parentID );
    }

    private void Components_CategoryNavList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void Refresh()
    {
        PopulateControls();
        switch (DataAccessContext.Configurations.GetValue( "CategoryMenuType" ))
        {
            case "cascade":
                uxMenuList.Refresh();
                break;

            case "treeview":
                uxTreeList.Refresh();
                break;

            default:
                uxNormalList.Refresh();
                break;
        }

    }

    private void PopulateControls()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "CategoryListModuleDisplay" ) || IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            switch (DataAccessContext.Configurations.GetValue( "CategoryMenuType" ))
            {
                case "cascade":
                    uxMenuList.MaxNode = DataAccessContext.Configurations.GetIntValue( "CategoryMenuLevel" );
                    uxMenuList.Visible = true;
                    break;

                case "treeview":
                    uxTreeList.MaxNode = DataAccessContext.Configurations.GetIntValue( "CategoryMenuLevel" );
                    uxTreeList.Visible = true;
                    break;

                default:
                    uxNormalList.Visible = true;
                    break;
            }

        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_CategoryNavList_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }
}
