using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.Domain.Orders;
using Vevo.Base.Domain;

public partial class Mobile_OrderHistory : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    int ItemsPerPage = 20;

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxHistoryGrid, "OrderID", GridViewHelper.Direction.DESC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.OrderRepository.GetTableOrderHistorySchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private string CreateExtraField()
    {
        return " PaymentComplete = " + DataAccess.CreateLiteralBool( true ) + " AND Username = @Username ";
    }

    private void RefreshGrid()
    {
        string username = this.User.Identity.Name;
        int totalItems;
        IList<Order> orderList = new List<Order>();

        orderList = DataAccessContext.OrderRepository.SearchOrder(
              GridHelper.GetFullSortText(),
              uxSearchFilter.SearchFilterObj,
              CreateExtraField(),
              new StoreRetriever().GetCurrentStoreID(),
              (uxPagingControl.CurrentPage - 1) * ItemsPerPage,
              (uxPagingControl.CurrentPage * ItemsPerPage) - 1,
              out totalItems, DataAccess.CreateParameterString( username ) );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / ItemsPerPage );

        if (orderList.Count > 0)
            GridViewHelper.ShowGridAlways( uxHistoryGrid, orderList, String.Empty );
        else
        {
            uxHistoryGrid.DataSource = orderList;
            uxHistoryGrid.DataBind();
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );

        if (!IsPostBack)
        {
            SetUpSearchFilter();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxHistoryGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }
}
