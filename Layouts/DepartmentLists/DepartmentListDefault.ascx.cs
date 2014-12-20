using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Layouts_DepartmentLists_DepartmentListDefault : BaseDepartmentListControl
{
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

    private string CurrentDepartmentID
    {
        get
        {
            string id = Request.QueryString["DepartmentID"];
            if (id != null)
            {
                return id;
            }
            else
            {
                return DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() );
            }
        }
    }

    private string ItemPerPage
    {
        get
        {
            if (ViewState["ItemPerPage"] == null)
                ViewState["ItemPerPage"] = uxItemsPerPageControl.DefaultValue;

            return (string) ViewState["ItemPerPage"];
        }
        set
        {
            ViewState["ItemPerPage"] = value;
        }
    }

    private int NoOfDepartmentColumn
    {
        get
        {
            return DataAccessContext.Configurations.GetIntValue( "NumberOfDepartmentColumn" );
        }
    }

    private IList<Department> GetDepartmentList( int itemsPerPage, out int totalItems )
    {
        if (!String.IsNullOrEmpty( CurrentDepartmentName ))
        {
            return DataAccessContext.DepartmentRepository.GetByParentUrlName(
                StoreContext.Culture,
                CurrentDepartmentName,
                "SortOrder",
                BoolFilter.ShowTrue,
                (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );
        }
        else
        {
            return DataAccessContext.DepartmentRepository.GetByParentID(
                StoreContext.Culture,
                CurrentDepartmentID,
                "SortOrder",
                BoolFilter.ShowTrue,
                (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );
        }
    }

    private void PopulateDepartmentControls()
    {
        uxItemsPerPageControl.SelectValue( ItemPerPage );

        int totalItems;
        int selectedValue;

        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        uxList.DataSource = GetDepartmentList( selectedValue, out totalItems );
        uxList.DataBind();

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / selectedValue );
    }

    private void PopulateProductControl()
    {
        BaseProductListControl productList = (BaseProductListControl)
            uxCatalogControlPanel.FindControl( "uxProductList" );

        if (productList != null) return;

        Department department = new Department();
        if (!String.IsNullOrEmpty( CurrentDepartmentName ))
        {
            department = DataAccessContext.DepartmentRepository.GetOneByUrlName( StoreContext.Culture, CurrentDepartmentName );

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
                    DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) ) as BaseProductListControl;

            productListControl.ID = "uxProductList";
            productListControl.IsSearchResult = true;
            productListControl.UserDefinedParameters = new object[]{
            department.DepartmentID};
            productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            uxCatalogControlPanel.Controls.Add( productListControl );
        }
        else
            uxCatalogControlPanel.Visible = false;
    }

    private BaseProductListControl GetProductList()
    {
        return (BaseProductListControl) uxCatalogControlPanel.FindControl( "uxProductList" );
    }

    private void DepartmentList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( DepartmentList_StoreCultureChanged );
    }

    private ScriptManager GetScriptManager()
    {
        return AjaxUtilities.GetScriptManager( this );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "DeptPage", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "DeptItemPerPage", uxItemsPerPageControl.SelectedValue );
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControl.SelectedValue;
        uxPagingControl.CurrentPage = 1;
        AddHistoryPoint();
        Refresh();
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if (!string.IsNullOrEmpty( e.State["DeptItemPerPage"] ))
        {
            ItemPerPage = e.State["DeptItemPerPage"].ToString();
        }
        else
        {
            ItemPerPage = uxItemsPerPageControl.DefaultValue;
        }

        int totalItems;
        int selectedValue;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        GetDepartmentList( selectedValue, out totalItems );

        uxPagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / selectedValue );

        if (!string.IsNullOrEmpty( e.State["DeptPage"] ))
        {
            args = e.State["DeptPage"];
            uxPagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxPagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );
        AjaxUtilities.ScrollToTop( uxGoToTopLink );

        uxList.RepeatColumns = NoOfDepartmentColumn;
        uxList.RepeatDirection = RepeatDirection.Horizontal;

        if (!IsPostBack)
        {
            ItemPerPage = CatalogUtilities.DepartmentItemsPerPage;
        }

        Refresh();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Refresh();

        CatalogUtilities.DepartmentItemsPerPage = ItemPerPage;
    }

    public void Refresh()
    {
        PopulateDepartmentControls();

        if (DataAccessContext.Configurations.GetBoolValue( "DepartmentShowProductList", new StoreRetriever().GetStore() ))
            PopulateProductControl();
    }

    public static IList<Product> GetProductList(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        string departmentID = userDefined[0].ToString();

        IList<string> list = new List<string>();

        IList<string> departmentIDs = DataAccessContext.DepartmentRepository.GetLeafFromDepartmentID( departmentID, list );

        List<string> departmentCollection = new List<string>();
        foreach (string departmentItem in departmentIDs)
        {
            departmentCollection.Add( departmentItem );
        }

        return DataAccessContext.ProductRepository.GetByDepartmentID(
            culture,
            departmentCollection.ToArray(),
            sortBy,
            startIndex,
            endIndex,
            BoolFilter.ShowTrue,
            out howManyItems,
            new StoreRetriever().GetCurrentStoreID() );
    }
}
