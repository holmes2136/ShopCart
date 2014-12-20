using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;
using Vevo;
using Vevo.Reports;
using Vevo.Reports.Exporters;
using Vevo.WebUI;
using Vevo.Domain;
using System.Globalization;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Snapshot_TabularView : AdminAdvancedBaseUserControl
{
    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private static bool _errorFlag;
    private string _storeID = "";

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "Period" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }
    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
            RefreshGrid();
        }

    }
    private void PopulateControls()
    {
        ListItemCollection list = new ListItemCollection();

        list.Add( new ListItem( "Today", PeriodType.Today.ToString() ) );
        list.Add( new ListItem( "Last 7 days", PeriodType.Last7Days.ToString() ) );
        list.Add( new ListItem( "This month", PeriodType.ThisMonth.ToString() ) );
        list.Add( new ListItem( "Last month", PeriodType.LastMonth.ToString() ) );
        list.Add( new ListItem( "Year to date", PeriodType.YearToDate.ToString()) );

        uxPeriodDrop.DataSource = list;
        uxPeriodDrop.DataBind();

        uxPeriodDrop.SelectedIndex = 0;
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        if (!MainContext.IsPostBack)
            uxPagingControl.ItemsPerPages = AdminConfig.OrderItemsPerPage;

        int totalItems;
        _errorFlag = false;

        PeriodType period = ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue );

        ReportFilterUtilities.GetOrderDateRange( period, out _startOrderDate, out _endOrderDate );

        SaleReportBuilder reportBuilder = new SaleReportBuilder();
        DataTable table = reportBuilder.GetReportData(
            GridHelper.GetFullSortText(),
            period,
            _startOrderDate,
            _endOrderDate,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            StoreID,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = table;
        uxGrid.DataBind();

        decimal revenue = 0;
        int sumQuantity = 0;
        decimal sumTax = 0;
        decimal sumShipping = 0;

        foreach (DataRow row in table.Rows)
        {
            revenue += Convert.ToDecimal( row["Total"] );
            sumQuantity += Convert.ToInt32( row["Quantity"] );
            sumTax += Convert.ToDecimal( row["TotalTax"] );
            sumShipping += Convert.ToDecimal( row["TotalShippingCost"] );
        }
        uxRevenueValueLabel.Text = revenue.ToString( "F", CultureInfo.InvariantCulture );
        uxQuantityValueLabel.Text = sumQuantity.ToString( "F", CultureInfo.InvariantCulture );
        uxTaxValueLabel.Text = sumTax.ToString( "F", CultureInfo.InvariantCulture );
        uxShippingValueLabel.Text = sumShipping.ToString( "F", CultureInfo.InvariantCulture );
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxPeriodDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (_errorFlag == false)
        {
            RefreshGrid();
        }
    }

    protected string GetDateTimeText( object registerPeriod )
    {
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;

        return RepositoryDisplayFormatter.GetDateTimeText(
            registerPeriod,
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
            startDate,
            endDate );
    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                if (_storeID != "")
                    return _storeID;
                else
                    return "0";
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
        set 
        {
            _storeID = value;
            RefreshGrid();
        }
    }
}
