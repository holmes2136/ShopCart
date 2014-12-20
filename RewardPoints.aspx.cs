using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.CustomerReward;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class RewardPoints : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxRewardPointsGrid, "RewardPointID", GridViewHelper.Direction.ASC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string CustomerID
    {
        get
        {
            string username = this.User.Identity.Name;
            return DataAccessContext.CustomerRepository.GetIDFromUserName( username );
        }
    }

    private void RefreshGrid()
    {
        int totalItems;
        IList<CustomerRewardPoint> rewardPointList = new List<CustomerRewardPoint>();
        if (uxItemsPerPageDrop.SelectedValue == "All")
        {
            rewardPointList = DataAccessContextDeluxe.CustomerRewardPointRepository.SearchCustomerRewardPointByCustomerIDAndStoreID(
                CustomerID,
                StoreContext.CurrentStore.StoreID,
                "ReferenceDate" );

            uxPagingControl.NumberOfPages = 1;
            uxPagingControl.CurrentPage = 1;
        }
        else
        {
            int itemsPerPage = ConvertUtilities.ToInt32( uxItemsPerPageDrop.SelectedValue );

            rewardPointList = DataAccessContextDeluxe.CustomerRewardPointRepository.SearchCustomerRewardPointByCustomerIDAndStoreID(
                CustomerID,
                StoreContext.CurrentStore.StoreID,
                "ReferenceDate",
                (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );

            uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / itemsPerPage );
        }

        uxRewardPointsGrid.DataSource = rewardPointList;
        uxRewardPointsGrid.DataBind();
    }

    private decimal TotalPoint()
    {
        return DataAccessContextDeluxe.CustomerRewardPointRepository.SumCustomerIDAndStoreID( CustomerID, StoreContext.CurrentStore.StoreID );
    }

    private void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void uxItemsPerPageDrop_BubbleEvent( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ))
            Response.Redirect( "Error404.aspx" );

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageDrop.BubbleEvent += new EventHandler( uxItemsPerPageDrop_BubbleEvent );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected bool IsHasLink( object orderID )
    {
        if (ConvertUtilities.ToInt32( orderID ) > 0)
        {
            return true;
        }
        return false;
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;

            cells[0].Text = "Total Points";
            cells[0].CssClass = "RewardPointGridTotalFooterStyle";

            cells[1].Text = ConvertUtilities.ToString( TotalPoint() );
            cells[1].CssClass = "RewardPointGridPointFooterStyle";
        }
    }
}
