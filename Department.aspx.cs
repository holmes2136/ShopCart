using System;
using System.Collections.Generic;
using System.Web;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Products;

public partial class DepartmentPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string DepartmentID
    {
        get
        {
            if (ViewState["DepartmentID"] == null)
                return "0";
            else
                return (string) ViewState["DepartmentID"];
        }
        set
        {
            ViewState["DepartmentID"] = value;
        }
    }

    private string CurrentDepartmentID
    {
        get
        {
            string id = Request.QueryString["DepartmentID"];
            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else
            {
                Department department = DataAccessContext.DepartmentRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentDepartmentName );

                return department.DepartmentID;
            }
        }
    }

    private string CurrentDepartmentName
    {
        get
        {
            if (Request.QueryString["DepartmentName"] == null)
                return String.Empty;
            else
                return Request.QueryString["DepartmentName"];
        }
    }

    private bool IsParentOfOtherDepartments()
    {
        if (!String.IsNullOrEmpty( CurrentDepartmentName ))
        {
            return DataAccessContext.DepartmentRepository.IsUrlNameNotLeaf( CurrentDepartmentName );
        }
        else if (!String.IsNullOrEmpty( CurrentDepartmentID ))
        {
            return DataAccessContext.DepartmentRepository.IsDepartmentIDNotLeaf( CurrentDepartmentID );
        }
        else
        {
            return false;
        }
    }

    private void PopulateTitleAndMeta( DynamicPageElement element )
    {
        Department department = DataAccessContext.DepartmentRepository.GetOne(
            StoreContext.Culture, CurrentDepartmentID );

        string title = SeoVariable.ReplaceDepartmentVariable(
              department,
              StoreContext.Culture,
              DataAccessContext.Configurations.GetValue( StoreContext.Culture.CultureID, "DefaultDepartmentPageTitle", StoreContext.CurrentStore ) );

        element.SetUpTitleAndMetaTags(
            department.GetPageTitle( StoreContext.Culture, title ),
            department.GetMetaDescription( StoreContext.Culture ),
            department.GetMetaKeyword( StoreContext.Culture ) );
    }

    private void PopulateControls()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        if (CurrentDepartmentID == "0")
        {
            if (CurrentDepartmentName == "")
                element.SetUpTitleAndMetaTags( "[$Title] - " + NamedConfig.SiteName, NamedConfig.SiteDescription );
            else
                Response.Redirect( "~/Error404.aspx" );
        }
        else
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne(
            StoreContext.Culture, CurrentDepartmentID );

            if (department.IsCategoryAvailableStore( StoreContext.CurrentStore.StoreID ) && (department.IsParentsEnable()))
                PopulateTitleAndMeta( element );
            else
                Response.Redirect( "~/Error404.aspx" );
        }
    }

    private void PopulateUserControl()
    {
        uxProductControlPanel.Visible = false;
        uxDepartmentControlPanel.Visible = false;
        Department department = DataAccessContext.DepartmentRepository.GetOne(
            StoreContext.Culture, CurrentDepartmentID );

        uxDepartmentNameLabel.Text = department.Name;

        if (IsParentOfOtherDepartments())
        {
            uxDepartmentControlPanel.Visible = true;
            BaseDepartmentListControl departmentControl = new BaseDepartmentListControl();
            if (!String.IsNullOrEmpty( department.DepartmentListLayoutPath ))
                departmentControl = LoadControl( String.Format( "{0}{1}",
                        SystemConst.LayoutDepartmentListPath, department.DepartmentListLayoutPath ) )
                    as BaseDepartmentListControl;
            else
                departmentControl = LoadControl( String.Format( "{0}{1}",
                        SystemConst.LayoutDepartmentListPath,
                        DataAccessContext.Configurations.GetValue( "DefaultDepartmentListLayout" ) ) )
                    as BaseDepartmentListControl;

            uxDepartmentControlPanel.Controls.Add( departmentControl );
        }
        else
        {
            uxProductControlPanel.Visible = true;
            BaseProductListControl productListControl = new BaseProductListControl();
            if (!String.IsNullOrEmpty( department.ProductListLayoutPath ))
                productListControl = LoadControl( String.Format(
                    "{0}{1}",
                    SystemConst.LayoutProductListPath,
                    department.ProductListLayoutPath ) ) as BaseProductListControl;
            else
                productListControl = LoadControl( String.Format(
                    "{0}{1}",
                    SystemConst.LayoutProductListPath,
                    DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) )
                        as BaseProductListControl;

            productListControl.ID = "uxProductList";
            productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            productListControl.UserDefinedParameters = new object[] { CurrentDepartmentID };
            uxProductControlPanel.Controls.Add( productListControl );
        }
    }

    private void SetUpBreadcrumb()
    {
        if (CurrentDepartmentID == "0" && CurrentDepartmentName == String.Empty)
        {
            uxCatalogBreadcrumb.Visible = false;
            uxDepartmentNameLabel.CssClass = "CatalogName CatalogRoot";
        }
        else
        {
            uxCatalogBreadcrumb.SetupDepartmentSitemap( CurrentDepartmentID );
            uxCatalogBreadcrumb.Refresh();
        }
    }

    private void Department_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        if (CurrentDepartmentName == "")
        {
            Response.Redirect( "~/Department.aspx" );
        }
        else
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne(
                    StoreContext.Culture, DepartmentID );

            if (!String.IsNullOrEmpty( department.UrlName ))
            {
                Response.Redirect( UrlManager.GetDepartmentUrl( DepartmentID, department.UrlName ) );
            }
            else
            {
                Response.Redirect( "~/Error404.aspx" );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        Response.Cache.SetCacheability( HttpCacheability.NoCache );
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader( "Pragma", "no-cache" );

        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Department_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
            DepartmentID = CurrentDepartmentID;
        }

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpBreadcrumb();
    }

    public static IList<Product> GetProductList(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        return DataAccessContext.ProductRepository.GetByDepartmentID(
            culture,
            userDefined[0].ToString(),
            sortBy,
            startIndex,
            endIndex,
            BoolFilter.ShowTrue,
            out howManyItems,
            new StoreRetriever().GetCurrentStoreID()
            );
    }
}
