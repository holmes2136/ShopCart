using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Shared.Utilities;
using Vevo.Domain.Products;

public partial class Components_DepartmentNavList : Vevo.WebUI.International.BaseLanguageUserControl
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
        return DataAccessContext.DepartmentRepository.IsDepartmentIDNotLeaf( parentID );
    }

    private void Components_DepartmentNavList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void Refresh()
    {
        PopulateControls();

        switch (DataAccessContext.Configurations.GetValue( "DepartmentMenuType" ))
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
        if (!DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay" ) || IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            switch (DataAccessContext.Configurations.GetValue( "DepartmentMenuType" ))
            {
                case "cascade":
                    uxMenuList.MaxNode = DataAccessContext.Configurations.GetIntValue( "DepartmentMenuLevel" );
                    uxMenuList.Visible = true;
                    break;

                case "treeview":
                    uxTreeList.MaxNode = DataAccessContext.Configurations.GetIntValue( "DepartmentMenuLevel" );
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
            new StorefrontEvents.CultureEventHandler( Components_DepartmentNavList_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }
}
