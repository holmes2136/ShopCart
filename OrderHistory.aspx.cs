using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class OrderHistory : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private int _rmaColumnIndex = 5;

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

    private void uxItemsPerPageDrop_BubbleEvent( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private string CreateExtraField()
    {
        return " PaymentComplete = " + DataAccess.CreateLiteralBool( true ) + " AND Username = @Username ";
    }

    private void SetRMAColumnVisibility()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "EnableRMA", StoreContext.CurrentStore ))
            uxHistoryGrid.Columns[_rmaColumnIndex].Visible = false;
    }

    private void RefreshGrid()
    {
        string username = this.User.Identity.Name;
        int totalItems;
        IList<Order> orderList = new List<Order>();

        if (uxItemsPerPageDrop.SelectedValue == "All")
        {
            orderList = DataAccessContext.OrderRepository.SearchOrder(
                  GridHelper.GetFullSortText(),
                  uxSearchFilter.SearchFilterObj,
                  CreateExtraField(),
                  new StoreRetriever().GetCurrentStoreID(),
                  out totalItems, DataAccess.CreateParameterString( username ) );
            uxPagingControl.NumberOfPages = 1;
            uxPagingControl.CurrentPage = 1;
        }
        else
        {
            int itemsPerPage = ConvertUtilities.ToInt32( uxItemsPerPageDrop.SelectedValue );

            orderList = DataAccessContext.OrderRepository.SearchOrder(
                  GridHelper.GetFullSortText(),
                  uxSearchFilter.SearchFilterObj,
                  CreateExtraField(),
                  new StoreRetriever().GetCurrentStoreID(),
                  (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                  (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                  out totalItems, DataAccess.CreateParameterString( username ) );

            uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / itemsPerPage );
        }

        if (orderList.Count > 0)
            GridViewHelper.ShowGridAlways( uxHistoryGrid, orderList, String.Empty );
        else
        {
            uxHistoryGrid.DataSource = orderList;
            uxHistoryGrid.DataBind();
        }

        SetRMAColumnVisibility();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageDrop.BubbleEvent += new EventHandler( uxItemsPerPageDrop_BubbleEvent );

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

    protected bool IsRmaVisible()
    {
        return DataAccessContext.Configurations.GetBoolValue( "EnableRMA" );
    }
}
