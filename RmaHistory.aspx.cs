using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Returns;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Base.Domain;

public partial class RmaHistory : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private GridViewHelper GridHelper
    {
        get
        {
            if ( ViewState[ "GridHelper" ] == null )
                ViewState[ "GridHelper" ] = new GridViewHelper( uxRmaHistoryGrid, "RmaID", GridViewHelper.Direction.DESC );

            return ( GridViewHelper ) ViewState[ "GridHelper" ];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.RmaRepository.GetRmaHistoryTableSchema();
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

    private void RefreshGrid()
    {
        int totalItems;
        IList<Rma> rmaList = new List<Rma>();

        if ( uxItemsPerPageDrop.SelectedValue == "All" )
        {
            rmaList = DataAccessContext.RmaRepository.SearchRma(
                  GridHelper.GetFullSortText(),
                  uxSearchFilter.SearchFilterObj,
                  StoreContext.CurrentStore.StoreID,
                  StoreContext.Customer.CustomerID,
                  out totalItems );

            uxPagingControl.NumberOfPages = 1;
            uxPagingControl.CurrentPage = 1;
        }
        else
        {
            int itemsPerPage = ConvertUtilities.ToInt32( uxItemsPerPageDrop.SelectedValue );

            rmaList = DataAccessContext.RmaRepository.SearchRma(
                  GridHelper.GetFullSortText(),
                  uxSearchFilter.SearchFilterObj,
                  ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
                  ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
                  StoreContext.CurrentStore.StoreID,
                  StoreContext.Customer.CustomerID,
                  out totalItems );

            uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / itemsPerPage );
        }

        uxRmaHistoryGrid.DataSource = rmaList;
        uxRmaHistoryGrid.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageDrop.BubbleEvent += new EventHandler( uxItemsPerPageDrop_BubbleEvent );

        if ( !IsPostBack )
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
