using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_MainControls_OrdersList : AdminAdvancedBaseListControl
{
    #region Private

    private const int OrderIDIndex = 1;
    private OrderCreateExtraFilter _orderExtraFilter = new OrderCreateExtraFilter();

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.OrderRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "StoreID" );
    }

    private void LoadProcessedDropFromQuery()
    {
        if (MainContext.QueryString["Processed"] != null)
        {
            uxProcessedDrop.SelectedValue = MainContext.QueryString["Processed"];
        }
    }

    private void LoadPaymentDropFromQuery()
    {
        if (MainContext.QueryString["Payment"] != null)
        {
            uxPaymentDrop.SelectedValue = MainContext.QueryString["Payment"];
        }
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxProcessedButton.Visible = false;
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void AdminAdvanced_MainControls_OrdersList_BrowseHistoryAdding(
        object sender, BrowseHistoryAddEventArgs e )
    {
        e.BrowseHistoryQuery.AddQuery( "Processed", uxProcessedDrop.SelectedValue );
        e.BrowseHistoryQuery.AddQuery( "Payment", uxPaymentDrop.SelectedValue );
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.OrderItemsPerPage;
            SetUpSearchFilter();
            LoadProcessedDropFromQuery();
            LoadPaymentDropFromQuery();
        }

        RegisterGridView( uxGrid, "OrderID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterStoreFilterDrop( uxStoreFilterDrop );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );

        BrowseHistoryAdding += new BrowseHistoryAddEventHandler(
            AdminAdvanced_MainControls_OrdersList_BrowseHistoryAdding );
    }

    #endregion

    #region Protected

    protected string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return uxStoreFilterDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxStoreFilterDrop.Visible = false;
        }

        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    protected void uxProcessedDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();

        AddBrowseHistory();
    }

    protected void uxPaymentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();

        AddBrowseHistory();
    }

    protected void uxProcessedButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool processed = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox check = (CheckBox) row.FindControl( "uxCheck" );
                if (check.Checked)
                {
                    string id = row.Cells[1].Text.Trim();
                    Order order = DataAccessContext.OrderRepository.GetOne( id );
                    order.Processed = true;
                    DataAccessContext.OrderRepository.Save( order );
                    processed = true;
                }
            }

            if (processed)
                uxMessage.DisplayMessage( Resources.OrdersMessages.ProcessedSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[OrderIDIndex].Text.Trim();
                    DataAccessContext.OrderRepository.Delete( id );
                    DataAccessContextDeluxe.CustomerRewardPointRepository.UpdateDeletedCustomerRewardPointDetails( id );
                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.OrdersMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        GridView grid = (GridView) sender;
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        _orderExtraFilter = new OrderCreateExtraFilter( uxPaymentDrop.SelectedValue, uxProcessedDrop.SelectedValue );
        IList<Order> list = DataAccessContext.OrderRepository.SearchOrder(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            _orderExtraFilter,
            StoreID,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = list;
        uxGrid.DataBind();
    }

    protected string GetAvsImage( string orderID )
    {
        Order order = DataAccessContext.OrderRepository.GetOne( orderID );
        if (order.AvsAddrStatus == "Fail" || order.AvsZipStatus == "Fail")
            return "CssSymbolFail";

        if (order.AvsAddrStatus == "Unavailable" || order.AvsZipStatus == "Unavailable")
            return "CssSymbolUnavailable";

        if (order.AvsAddrStatus == "Pass" || order.AvsZipStatus == "Pass")
            return "CssSymbolPass";
        else
            return "";
    }

    protected bool IsAvsStatusNA( string orderID )
    {
        if (GetAvsImage( orderID ) == "CssSymbolUnavailable")
            return true;
        else
            return false;
    }

    protected bool IsCvvStatusNA( string orderID )
    {
        if (GetCvvImage( orderID ) == "CssSymbolUnavailable")
            return true;
        else
            return false;
    }

    protected string GetCvvImage( string orderID )
    {
        Order order = DataAccessContext.OrderRepository.GetOne( orderID );
        if (order.CvvStatus == "Fail")
            return "CssSymbolFail";

        if (order.CvvStatus == "Unavailable")
            return "CssSymbolUnavailable";

        if (order.CvvStatus == "Pass")
            return "CssSymbolPass";
        else
            return "";
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "OrderCreateSelectingCustomer.ascx" );
    }

    #endregion

    #region Public Methods

    public decimal TotalPrice()
    {
        _orderExtraFilter = new OrderCreateExtraFilter( uxPaymentDrop.SelectedValue, uxProcessedDrop.SelectedValue );
        return DataAccessContext.OrderRepository.SumOrder( uxSearchFilter.SearchFilterObj, _orderExtraFilter, StoreID );
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells.RemoveAt( 0 );
            cells.RemoveAt( 0 );
            cells[0].ColumnSpan = 3;
            if (uxSearchFilter.SearchFilterObj.FilterType != SearchFilter.SearchFilterType.None)
                if (uxSearchFilter.SearchFilterObj.Value2 != "")
                    if (uxSearchFilter.SearchFilterObj.FilterType == SearchFilter.SearchFilterType.Date)
                    {
                        cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName + "<br> from " +
                            DateTime.Parse( uxSearchFilter.SearchFilterObj.Value1 ).ToString( "MMMM d, yyyy" ) +
                            " to " + DateTime.Parse( uxSearchFilter.SearchFilterObj.Value2 ).ToString( "MMMM d, yyyy" );
                    }
                    else
                    {
                        cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName +
                            "<br> from " + uxSearchFilter.SearchFilterObj.Value1 +
                            " to " + uxSearchFilter.SearchFilterObj.Value2;
                    }
                else
                    cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName + " from " + uxSearchFilter.SearchFilterObj.Value1;
            else
                cells[0].Text = " Show All ";
            cells[0].CssClass = "pdl10";
            cells[1].Text = "Total";
            cells[1].HorizontalAlign = HorizontalAlign.Center;
            cells[1].Font.Bold = true;
            cells[2].Text = AdminUtilities.FormatPrice( TotalPrice() );
            cells[2].CssClass = "OrderListTotalPrice";
        }

    }





    #endregion

}
