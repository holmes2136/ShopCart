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
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_AffiliatePayCommissionList : AdminAdvancedBaseUserControl
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "AffiliateCode" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.AffiliateRepository.GetTableSchema();

        uxSearchFilter.SetUpSchema( list );
    }

    private void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContextDeluxe.AffiliateRepository.SearchAffiliateHaveBalance(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            DataAccessContext.Configurations.GetDecimalValue( "AffiliateDefaultPaidBalance" ),
            GetStartDate(),
            GetEndDate(),
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
    }

    private string GetStartDate()
    {
        if (uxPeriodDrop.SelectedValue == "Custom")
        {
            return uxStartDate.SelectedDateText;
        }
        else
        {
            return null;
        }
    }

    private string GetEndDate()
    {
        DateTime today = DateTime.Today;
        string endDate;
        if (uxPeriodDrop.SelectedValue == "LastMonth")
        {
            endDate = DateTimeUtilities.GetLastDayOfTheMonth( today.AddMonths( -1 ) ).ToLongDateString();
        }
        else if (uxPeriodDrop.SelectedValue == "ThisMonth")
        {
            endDate = DateTimeUtilities.GetLastDayOfTheMonth( today ).ToLongDateString();
        }
        else
        {
            endDate = uxEndDate.SelectedDateText;
        }
        return endDate;
    }

    private void PopulateNote()
    {
        uxLastMonthNoteLiteral.Visible = false;
        uxTodayNoteLiteral.Visible = false;
        uxCustomNoteLiteral.Visible = false;

        if (uxPeriodDrop.SelectedValue == "LastMonth")
            uxLastMonthNoteLiteral.Visible = true;
        else if (uxPeriodDrop.SelectedValue == "ThisMonth")
            uxTodayNoteLiteral.Visible = true;
        else
            uxCustomNoteLiteral.Visible = true;
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGrid.Rows.Count > 0)
        {
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }

        PopulateNote();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void uxGrid_ResetPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    public string TotalPrice()
    {
        return DataAccessContextDeluxe.AffiliateRepository.GetTotalBalance(
            uxSearchFilter.SearchFilterObj,
            GetStartDate(),
            GetEndDate(),
            DataAccessContext.Configurations.GetDecimalValue( "AffiliateDefaultPaidBalance" )
            );
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells.RemoveAt( 0 );
            cells[0].ColumnSpan = 2;
            if (uxSearchFilter.SearchFilterObj.FilterType != SearchFilter.SearchFilterType.None)
                if (uxSearchFilter.SearchFilterObj.Value2 != "")
                    if (uxSearchFilter.SearchFilterObj.FilterType == SearchFilter.SearchFilterType.Date)
                    {
                        cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName +
                            "<br> from " + DateTime.Parse( uxSearchFilter.SearchFilterObj.Value1 ).ToString( "MMMM d, yyyy" ) +
                            " to " + DateTime.Parse( uxSearchFilter.SearchFilterObj.Value2 ).ToString( "MMMM d, yyyy" );
                    }
                    else
                    {
                        cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName + "<br> from " +
                            uxSearchFilter.SearchFilterObj.Value1 + " to " + uxSearchFilter.SearchFilterObj.Value2;
                    }
                else
                    cells[0].Text = uxSearchFilter.SearchFilterObj.FieldName + " from " + uxSearchFilter.SearchFilterObj.Value1;
            else
                cells[0].Text = " Show All ";
            cells[0].CssClass = "pdl10";
            cells[1].Text = "Total";
            cells[1].HorizontalAlign = HorizontalAlign.Center;
            cells[1].Font.Bold = true;
            cells[2].Text = AdminUtilities.FormatPrice( Convert.ToDecimal( TotalPrice() ) );
            cells[2].HorizontalAlign = HorizontalAlign.Right;
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        uxSearchFilter.BubbleEvent += new EventHandler( uxGrid_ResetPageHandler );
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AffiliatePaymentPerPage;
            SetUpSearchFilter();
            uxNoteLiteral.Text = uxNoteLiteral.Text + " "
                + StoreContext.Currency.FormatPrice(
                    DataAccessContext.Configurations.GetDecimalValue( "AffiliateDefaultPaidBalance" ) );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxPeriodDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxPeriodDrop.SelectedValue == "Custom")
            uxCustomDatePanel.Visible = true;
        else
        {
            uxCustomDatePanel.Visible = false;
            RefreshGrid();
        }
    }

    protected void uxDateRangeButton_Click( object sender, EventArgs e )
    {
        if (uxPeriodDrop.SelectedValue == "Custom" &&
            String.IsNullOrEmpty( uxStartDate.SelectedDateText ) &&
            String.IsNullOrEmpty( uxEndDate.SelectedDateText ))
            return;

        RefreshGrid();
    }
}
