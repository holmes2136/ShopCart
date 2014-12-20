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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;

public partial class AffiliateCommission : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "OrderID", GridViewHelper.Direction.DESC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string AffiliateCode
    {
        get
        {
            return DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( Page.User.Identity.Name );
        }
    }

    private String StartOrderID
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderID")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartOrderID"];
        }
    }

    private string EndOrderID
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderID")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndOrderID"];
        }
    }

    private string StartAmount
    {
        get
        {
            if (uxSearchFilter.FieldValue == "ProductCost")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartAmount"];
        }
    }

    private string EndAmount
    {
        get
        {
            if (uxSearchFilter.FieldValue == "ProductCost")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndAmount"];
        }
    }

    private string StartCommission
    {
        get
        {
            if (uxSearchFilter.FieldValue == "Commission")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartCommission"];
        }
    }

    private string EndCommission
    {
        get
        {
            if (uxSearchFilter.FieldValue == "Commission")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndCommission"];
        }
    }

    private string StartOrderDate
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderDate")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["StartOrderDate"];
        }
    }

    private string EndOrderDate
    {
        get
        {
            if (uxSearchFilter.FieldValue == "OrderDate")
                return uxSearchFilter.Value2;
            else
                return Request.QueryString["EndOrderDate"];
        }
    }

    private string PaymentStatus
    {
        get
        {
            if (uxSearchFilter.FieldValue == "PaymentStatus")
                return uxSearchFilter.Value1;
            else
                return Request.QueryString["PaymentStatus"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateOrderRepository.GetCommissionSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateCommission( out int totalItems )
    {
        int itemsPerPage = 0;

        IList<AffiliateOrder> affiliateOrderList;
        if (uxItemsPerPageDrop.SelectedValue == "All")
        {
            affiliateOrderList = DataAccessContextDeluxe.AffiliateOrderRepository.SearchCommission(
                GridHelper.GetFullSortText(),
                AffiliateCode,
                StartOrderID,
                EndOrderID,
                StartAmount,
                EndAmount,
                StartCommission,
                EndCommission,
                StartOrderDate,
                EndOrderDate,
                PaymentStatus,
                out totalItems );
            uxPagingControl.NumberOfPages = 1;
            uxPagingControl.CurrentPage = 1;
        }
        else
        {
            itemsPerPage = ConvertUtilities.ToInt32( uxItemsPerPageDrop.SelectedValue );

            affiliateOrderList = DataAccessContextDeluxe.AffiliateOrderRepository.SearchCommission(
                GridHelper.GetFullSortText(),
                AffiliateCode,
                StartOrderID,
                EndOrderID,
                StartAmount,
                EndAmount,
                StartCommission,
                EndCommission,
                StartOrderDate,
                EndOrderDate,
                PaymentStatus,
                (uxPagingControl.CurrentPage - 1) * itemsPerPage,
                (uxPagingControl.CurrentPage * itemsPerPage) - 1,
                out totalItems );
            uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / itemsPerPage );
        }

        uxGrid.DataSource = affiliateOrderList;
        uxGrid.DataBind();


        if (uxGrid.Rows.Count == 0)
            uxListPanel.Visible = false;
        else
            uxListPanel.Visible = true;
    }

    private void PopulateText( int totalItems )
    {
        if (totalItems <= 0)
            uxNoResultPanel.Visible = true;
        else
            uxNoResultPanel.Visible = false;
    }

    private void PopulateLink( int totalItems )
    {
        if (totalItems > 0)
            uxBackLink.Visible = false;
        else
        {
            if (uxSearchFilter.SearchType == SearchFilterType.None)
                uxBackLink.Visible = true;
            else
                uxBackLink.Visible = false;
        }
    }

    private void PopulateControls()
    {
        int totalItems;
        PopulateCommission( out totalItems );

        if (totalItems == 0)
            uxListPanel.Visible = false;

        PopulateText( totalItems );
        PopulateLink( totalItems );
    }

    private string GetTotal()
    {
        return DataAccessContextDeluxe.AffiliateOrderRepository.GetTotalCommission(
            AffiliateCode,
            StartOrderID,
            EndOrderID,
            StartAmount,
            EndAmount,
            StartCommission,
            EndCommission,
            StartOrderDate,
            EndOrderDate,
            PaymentStatus
        );
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells[0].CssClass = "AffiliateCommissionFooterFirstColumn";
            cells[1].Text = "Total";
            cells[1].CssClass = "AffiliateCommissionFooterTotalColumn";
            cells[2].Text = AdminUtilities.FormatPrice( Convert.ToDecimal( GetTotal() ) );
            cells[2].CssClass = "AffiliateCommissionFooterTotalAmountColumn";
            cells[3].CssClass = "AffiliateCommissionFooterEndColumn";
        }
    }

    private void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void uxItemsPerPageDrop_BubbleEvent( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void uxSearchFilter_BubbleEvent( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageDrop.BubbleEvent += new EventHandler( uxItemsPerPageDrop_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        if (!IsPostBack)
            SetUpSearchFilter();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
            PopulateControls();
    }

    protected string GetPaymentStatus( object affiliatePaymentID )
    {
        if (affiliatePaymentID.ToString() == "0")
            return "No";
        else
            return "Yes";
    }

    protected string ShowOnlyDate( object orderDate )
    {
        DateTime date = (DateTime) orderDate;
        return date.ToShortDateString();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        PopulateControls();
    }
}
