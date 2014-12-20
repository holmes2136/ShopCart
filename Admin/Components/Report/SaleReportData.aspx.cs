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
using OpenFlashChart;
using Vevo;
using Vevo.Reports;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_DataFile_SaleReportData : AdminAdvancedBasePage
{
    private const int InitialMaxYAxisRange = 10;
    private const int InitailYAxisInterval = 5;

    List<double> _data1 = new List<double>();
    List<string> _label1 = new List<string>();
    string _xLegend = String.Empty;
    string _yLegend = String.Empty;
    string _value = String.Empty;

    private string StoreID
    {
        get
        {
            if (Request.QueryString["storeID"] == null)
                return "0";
            else
                return Request.QueryString["storeID"].ToString();
        }
    }

    private PeriodType Period
    {
        get
        {
            if (Request.QueryString["period"] == null)
                return PeriodType.Unknow;
            else
                return ReportFilterUtilities.ConvertToPeriodType( Request.QueryString["period"] );
        }
    }

    private SaleReportType SaleReport
    {
        get
        {
            if (Request.QueryString["report"] == null)
                return SaleReportType.Unknow;
            else
                return ReportFilterUtilities.ConvertToSaleReportType( Request.QueryString["report"] );
        }
    }

    private string StartDate
    {
        get
        {
            if (Request.QueryString["startdate"] == "null" || Request.QueryString["startdate"] == null)
                return "No Parameter";
            else
                return Request.QueryString["startdate"];
        }
    }

    private string EndDate
    {
        get
        {
            if (Request.QueryString["enddate"] == "null" || Request.QueryString["enddate"] == null)
                return "No Parameter";
            else
                return Request.QueryString["enddate"];
        }
    }

    private string IsDisplayTitle
    {
        get
        {
            if (Request.QueryString["IsDisplayTitle"] == "null" || Request.QueryString["IsDisplayTitle"] == null)
                return "true";
            else
                return Request.QueryString["IsDisplayTitle"];
        }
    }

    private void SetUpChart( OpenFlashChart.OpenFlashChart chart, DateTime startOrderDate, DateTime endOrderDate )
    {
        if (IsDisplayTitle == "true")
        {
            if (Period == PeriodType.Custom)
            {
                chart.Title = new Title( ReportFilterUtilities.ConvertToSaleReportText( SaleReport ) +
                       " ( From " + startOrderDate.ToShortDateString() + " To " +
                       endOrderDate.ToShortDateString() + ")" );
            }
            else
            {
                chart.Title = new Title( ReportFilterUtilities.ConvertToSaleReportText( SaleReport ) +
                    " (" + ReportFilterUtilities.ConvertToPeriodText( Period ) + ")" );
            }
        }
        else
        {
            chart.Title = new Title( "" );
        }

        if (SaleReport == SaleReportType.OrderTotals)
        {
            _yLegend = "Amount (" + CurrencyUtilities.BaseCurrencySymbol + ")";
            _value = "Total";
        }
        else if (SaleReport == SaleReportType.AverageOrderTotals)
        {
            _yLegend = "Amount (" + CurrencyUtilities.BaseCurrencySymbol + ")";
            _value = "Average";
        }
        else if (SaleReport == SaleReportType.NumberOfOrder)
        {
            _yLegend = "Orders";
            _value = "NumberOfOrder";
        }
        else if (SaleReport == SaleReportType.NumberOfItemsSold)
        {
            _yLegend = "Items";
            _value = "Quantity";
        }
        else if (SaleReport == SaleReportType.AverageItemPerOrder)
        {
            _yLegend = "Items";
            _value = "AvgQuantity";
        }
    }

    private LineDot CreateLine( List<double> data )
    {
        LineDot line1 = new LineDot();
        line1.Values = data;
        line1.HaloSize = 1;
        line1.Width = 1;
        line1.DotSize = 4;
        line1.Colour = "#5ac8d3";
        return line1;
    }

    private void SetChartStyle( OpenFlashChart.OpenFlashChart chart )
    {
        chart.Bgcolor = "#ffffff";
        chart.Title.Style = "{font-size: 17px; color: #9bbde0; text-align: center; wmode: transparent;}";

    }

    private void SetRangeScale( OpenFlashChart.OpenFlashChart chart, int maxRange )
    {
        int step;
        step = (int) maxRange / InitailYAxisInterval;

        chart.Y_Axis.SetRange( 0, maxRange, step );
    }

    private void SetYLengend( OpenFlashChart.OpenFlashChart chart, string legend )
    {
        Legend ylegend = new Legend( legend );
        ylegend.Style = "{font-size: 12px; font-family: Times New Roman; font-weight: bold; color: #A2ACBA; text-align: center;}";
        chart.Y_Legend = ylegend;
        chart.Y_Axis.Colour = "#000000";
        chart.Y_Axis.GridColour = "#cccccc";
    }

    private void SetXLengend( OpenFlashChart.OpenFlashChart chart, string legend )
    {
        Legend xLegend = new Legend( legend );
        xLegend.Style = "{font-size: 12px; font-family: Times New Roman; font-weight: bold; color: #A2ACBA; text-align: center;}";
        chart.X_Legend = xLegend;
    }

    private void SetXAxis( OpenFlashChart.OpenFlashChart chart, List<string> labels )
    {
        XAxisLabels xLabels = new XAxisLabels();
        xLabels.Values = labels;
        xLabels.Vertical = true;
        xLabels.Color = "#000000";

        XAxis x = new XAxis();
        x.Colour = "#000000";
        x.Labels = xLabels;
        x.GridColour = "#ffffff";

        chart.X_Axis = x;
    }

    private void CreateYearData( DataTable table, DateTime day, int numMonth )
    {
        DateTime newDay;
        for (int i = 0; i < numMonth; i++)
        {
            bool existMonth = false;
            if (table.Rows.Count != 0)
            {
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    int orderMonth = ConvertUtilities.ToInt32( table.Rows[j]["MonthNumber"] );
                    int orderYear = ConvertUtilities.ToInt32( table.Rows[j]["Year"] );
                    newDay = day.AddMonths( i );
                    if (orderMonth == newDay.Month && orderYear == newDay.Year)
                    {
                        _data1.Add( ConvertUtilities.ToDouble( table.Rows[j][_value] ) );
                        existMonth = true;
                        break;
                    }
                }
            }
            if (existMonth == false)
            {
                _data1.Add( 0 );
            }
            newDay = day.AddMonths( i );
            _label1.Add( newDay.ToString( "MMM, yy" ) );
        }
        _xLegend = "Month";
    }

    private void CreateMonthData( DataTable table, DateTime day, int numDay )
    {
        DateTime newDay;
        for (int i = 0; i < numDay; i++)
        {
            bool existDay = false;
            if (table.Rows.Count != 0)
            {
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    DateTime orderDay = ConvertUtilities.ToDateTime( table.Rows[j]["Period"] );
                    newDay = day;
                    if (orderDay.Day == newDay.AddDays( i ).Day && orderDay.Month == newDay.AddDays( i ).Month)
                    {
                        _data1.Add( ConvertUtilities.ToDouble( table.Rows[j][_value] ) );
                        existDay = true;
                        break;
                    }
                }
            }
            if (existDay == false)
            {
                _data1.Add( 0 );
            }
            newDay = new DateTime( day.Year, day.Month, day.Day ).AddDays( i );
            _label1.Add( newDay.ToString( "MMM d, yy" ) );
        }
        _xLegend = "Date";
    }

    private void CreateDayData( DataTable table, DateTime day )
    {
        DateTime newDay;
        for (int i = 0; i < 24; i++)
        {
            bool existHour = false;
            if (table.Rows.Count != 0)
            {
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    newDay = new DateTime( day.Year, day.Month, day.Day, day.Hour, day.Minute, day.Second ).AddHours( i );
                    int orderHour = ConvertUtilities.ToInt32( table.Rows[j]["Period"] );
                    DateTime orderDate = ConvertUtilities.ToDateTime( table.Rows[j]["OrderDate"] );
                    if (orderHour == newDay.Hour && orderDate.Day == newDay.Day)
                    {
                        _data1.Add( ConvertUtilities.ToDouble( table.Rows[j][_value] ) );
                        existHour = true;
                        break;
                    }
                }
            }
            if (existHour == false)
            {
                _data1.Add( 0 );
            }
            newDay = day.AddHours( i );
            _label1.Add( newDay.Hour.ToString() );
        }
        _xLegend = "Hours";
    }

    private DataTable CreateDataAndLabel( DateTime startOrderDate, DateTime endOrderDate )
    {
        DateTime day;
        int numDay;
        DataTable table = new DataTable();
        SaleReportBuilder reportBuilder = new SaleReportBuilder();

        if (Period == PeriodType.LastMonth)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddMonths( -1 );
            numDay = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddDays( -1 ).Day;
            table = reportBuilder.GetSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Last30Days)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day ).AddDays( -30 );
            numDay = 30;
            table = reportBuilder.GetSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Last7Days)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day ).AddDays( -7 );
            numDay = 7;
            table = reportBuilder.GetSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Today)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0 );
            table = reportBuilder.GetOneDaySaleReport( startOrderDate, endOrderDate, StoreID );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.Yesterday)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0 ).AddDays( -1 );
            table = reportBuilder.GetOneDaySaleReport( startOrderDate, endOrderDate, StoreID );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.Last24Hours)
        {
            day = DateTime.Now.AddHours( -23 );
            table = reportBuilder.Get24HoursSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.ThisMonth)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 );
            numDay = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddMonths( 1 ).AddSeconds( -1 ).Day;
            table = reportBuilder.GetSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.ThisYear)
        {
            day = new DateTime( DateTime.Now.Year, 1, 1 );
            int numMonth = 12;
            table = reportBuilder.GetYearSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateYearData( table, day, numMonth );
        }
        else if (Period == PeriodType.Last3Years)
        {
            day = new DateTime( DateTime.Now.Year, 1, 1 ).AddYears( -2 );
            int numMonth = 36;
            table = reportBuilder.GetYearSaleReport( startOrderDate, endOrderDate, StoreID );
            CreateYearData( table, day, numMonth );
        }
        else if ((Period == PeriodType.Custom) || (Period == PeriodType.YearToDate))
        {
            PeriodType customPeriod = ReportFilterUtilities.ResolveCustomPeriodType( startOrderDate, endOrderDate );
            if (customPeriod == PeriodType.ThisYear)
            {
                day = new DateTime( startOrderDate.Year, startOrderDate.Month, startOrderDate.Day );
                int yearDiff = endOrderDate.Year - startOrderDate.Year;
                int monthDiff = endOrderDate.Month - startOrderDate.Month;
                yearDiff = yearDiff * 12;
                int numMonth = yearDiff + monthDiff;
                table = reportBuilder.GetYearSaleReport( startOrderDate, endOrderDate, StoreID );
                CreateYearData( table, day, numMonth + 1 );
            }
            else if (customPeriod == PeriodType.Today)
            {
                table = reportBuilder.GetOneDaySaleReport( startOrderDate, endOrderDate, StoreID );
                CreateDayData( table, startOrderDate );
            }
            else
            {
                TimeSpan dateDifference = endOrderDate.Subtract( startOrderDate );
                int days = dateDifference.Days;
                table = reportBuilder.GetSaleReport( startOrderDate, endOrderDate, StoreID );
                CreateMonthData( table, startOrderDate, days + 1 );
            }
        }
        return table;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        DateTime startOrderDate;
        DateTime endOrderDate;

        if (Period == PeriodType.Custom)
        {
            startOrderDate = Convert.ToDateTime( StartDate );
            endOrderDate = Convert.ToDateTime( EndDate );
        }
        else
        {
            ReportFilterUtilities.GetOrderDateRange( Period, out startOrderDate, out endOrderDate );
        }

        SetUpChart( chart, startOrderDate, endOrderDate );

        double maxRange = 0;
        DataTable table = CreateDataAndLabel( startOrderDate, endOrderDate );
        if (table.Rows.Count < 1)
        {
            maxRange = InitialMaxYAxisRange;
        }
        else
        {
            foreach (DataRow row in table.Rows)
            {

                double val = ConvertUtilities.ToDouble( row[_value] );

                if (val > maxRange)
                {
                    int mod = 10;
                    double result;
                    do
                    {
                        result = val / mod;
                        mod = mod * 10;
                    }
                    while (result > 1);
                    string resultformat = String.Format( "{0:0.0}", result );
                    maxRange = Convert.ToDouble( resultformat ) * (mod / 10);
                    if (val < InitialMaxYAxisRange)
                    {
                        maxRange = InitialMaxYAxisRange;
                    }

                    do
                    {
                        maxRange = maxRange + (mod / 100) / 2;
                    }
                    while (maxRange < val);
                }
            }
        }

        chart.AddElement( CreateLine( _data1 ) );

        SetChartStyle( chart );

        SetRangeScale( chart, (int) maxRange );

        SetYLengend( chart, _yLegend );

        SetXLengend( chart, _xLegend );

        SetXAxis( chart, _label1 );

        Response.Clear();
        //Response.CacheControl = "no-cache";
        Response.Write( chart.ToString() );
        Response.End();
    }
}
