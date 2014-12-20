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

public partial class AdminAdvanced_Components_Snapshot_GraphicView : AdminAdvancedBaseUserControl
{
    #region Private

    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private string _storeID = "";

    private void PopulateControls()
    {
        ListItemCollection list = new ListItemCollection();

        list.Add( new ListItem( "Today", PeriodType.Today.ToString() ) );
        list.Add( new ListItem( "Last 7 days", PeriodType.Last7Days.ToString() ) );
        list.Add( new ListItem( "This month", PeriodType.ThisMonth.ToString() ) );
        list.Add( new ListItem( "Last month", PeriodType.LastMonth.ToString() ) );
        list.Add( new ListItem( "Year to date", PeriodType.YearToDate.ToString() ) );

        uxPeriodGraphicDrop.DataSource = list;
        uxPeriodGraphicDrop.DataBind();

        uxPeriodGraphicDrop.SelectedIndex = 0;
    }

    private void CreateChart()
    {
        HttpBrowserCapabilities browser = Request.Browser;
        string browserName = browser.Browser;

        string script = "<script language='javascript'>CreateChart(document.getElementById('" +
            uxPeriodGraphicDrop.ClientID + "').value,'" + SaleReportType.OrderTotals.ToString() + "', " +
            "'null','null','" + browserName + "','false'," + StoreID + ")</script>";
        ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
    }

    private void RefreshGrid()
    {
        PeriodType period = ReportFilterUtilities.ConvertToPeriodType( uxPeriodGraphicDrop.SelectedValue );
        ReportFilterUtilities.GetOrderDateRange( period, out _startOrderDate, out _endOrderDate );

        decimal revenue = 0;
        int sumQuantity = 0;
        decimal sumTax = 0;
        decimal sumShipping = 0;
        SaleReportBuilder reportBuilder = new SaleReportBuilder();
        DataTable table = reportBuilder.GetReportData(
            period,
            _startOrderDate,
            _endOrderDate,
            StoreID
           );
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

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
        CreateChart();
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        HttpBrowserCapabilities browser = Request.Browser;
        string browserName = browser.Browser;
        PopulateControls();
        RefreshGrid();

        string script = "<script language='javascript'>if ( document.getElementById('" +
            uxPeriodGraphicDrop.ClientID + "') != null) { CreateChart(document.getElementById('" +
            uxPeriodGraphicDrop.ClientID + "').value,'" + SaleReportType.OrderTotals.ToString() + "'," +
            "'null','null','" + browserName + "','false','" + StoreID + "') }</script>";
        ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
    }

    protected void uxPeriodGraphicDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        CreateChart();
        RefreshGrid();
    }

    protected string GetDateTimeText( object registerPeriod )
    {
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;

        return RepositoryDisplayFormatter.GetDateTimeText(
            registerPeriod,
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodGraphicDrop.SelectedValue ),
            startDate,
            endDate );
    }

    #endregion

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
            CreateChart();
        }
    }
}
