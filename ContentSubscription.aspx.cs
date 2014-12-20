using System;
using System.Collections;
using System.Web.UI.WebControls;
using Vevo.WebUI.International;
using Vevo.Shared.Utilities;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Contents;
using Vevo.Domain.DataInterfaces;
using System.Collections.Generic;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Contents;

public partial class ContentSubscription : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "EndDate", GridViewHelper.Direction.ASC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionTableSchemas();
        uxSearchFilter.SetUpSchema( list, "CustomerID" );
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

        if (uxItemsPerPageDrop.SelectedValue == "All")
        {
            uxGrid.DataSource = DataAccessContextDeluxe.CustomerSubscriptionRepository.SearchCustomerSubscription(
                StoreContext.Customer.CustomerID,
                GridHelper.GetFullSortText(),
                uxSearchFilter.SearchFilterObj,
                out totalItems );

            uxPagingControl.NumberOfPages = 1;
            uxPagingControl.CurrentPage = 1;
        }
        else
        {
            int itemsPerPage = ConvertUtilities.ToInt32( uxItemsPerPageDrop.SelectedValue );

            uxGrid.DataSource = DataAccessContextDeluxe.CustomerSubscriptionRepository.SearchCustomerSubscription(
                StoreContext.Customer.CustomerID,
                GridHelper.GetFullSortText(),
                uxSearchFilter.SearchFilterObj,
                (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );

            uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / itemsPerPage );
        }

        uxGrid.DataBind();

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

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected string GetSubscriptionLevel( object subscriptionID )
    {
        if (subscriptionID.ToString() != "0")
        {
            SubscriptionLevel level = DataAccessContextDeluxe.SubscriptionLevelRepository.GetOne( subscriptionID.ToString() );
            return level.Level.ToString();
        }
        else
        {
            return "0";
        }

    }

    protected string GetDisplayDate( object date )
    {
        return String.Format( "{0:dd} {0:MMM} {0:yyyy}", (DateTime) date );
    }
}
