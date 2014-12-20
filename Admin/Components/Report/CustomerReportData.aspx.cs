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

public partial class AdminAdvanced_DataFile_CustomerReportData : AdminAdvancedBasePage
{
    private const int InitialMaxYAxisRange = 5;
    private const int InitailYAxisInterval = 5;

    private List<double> _data1 = new List<double>();
    private List<string> _label1 = new List<string>();
    private string _xLegend = String.Empty;
    private string _yLegend = String.Empty;
    private string _value = String.Empty;

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

    private CustomerReportType CustomerReport
    {
        get
        {
            if (Request.QueryString["report"] == null)
                return CustomerReportType.Unknow;
            else
                return ReportFilterUtilities.ConvertToCustomerReportType( Request.QueryString["report"] );
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

    private void SetUpChart( OpenFlashChart.OpenFlashChart chart, DateTime startOrderDate, DateTime endOrderDate )
    {
        if (Period == PeriodType.Custom)
        {
            chart.Title = new Title( ReportFilterUtilities.ConvertToCustomerReportText( CustomerReport ) +
                " ( From " + startOrderDate.ToShortDateString()
                            + " To " + endOrderDate.ToShortDateString() + ")" );
        }
        else
        {
            chart.Title = new Title( ReportFilterUtilities.ConvertToCustomerReportText( CustomerReport ) +
                " (" + ReportFilterUtilities.ConvertToPeriodText( Period ) + ")" );
        }
        _yLegend = "Register";
        _value = "RegisterCustomer";
    }

    private LineDot CreateLine( List<double> data )
    {
        LineDot line1 = new LineDot();
        line1.Values = data;
        line1.HaloSize = 1;
        line1.Width = 1;
        line1.DotSize = 4;
        line1.Colour = "#1f95ed";
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
                    DateTime orderDay = ConvertUtilities.ToDateTime( table.Rows[j]["RegisterPeriod"] );
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
                    int orderHour = ConvertUtilities.ToInt32( table.Rows[j]["RegisterPeriod"] );
                    DateTime orderDate = ConvertUtilities.ToDateTime( table.Rows[j]["RegisterDate"] );
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
        CustomerReportBuilder customerReportBuilder = new CustomerReportBuilder();

        if (Period == PeriodType.LastMonth)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddMonths( -1 );
            numDay = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddDays( -1 ).Day;
            table = customerReportBuilder.GetCustomerReport( startOrderDate, endOrderDate );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Last30Days)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day ).AddDays( -30 );
            numDay = 30;
            table = customerReportBuilder.GetCustomerReport( startOrderDate, endOrderDate );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Last7Days)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day ).AddDays( -7 );
            numDay = 7;
            table = customerReportBuilder.GetCustomerReport( startOrderDate, endOrderDate );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.Today)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0 );
            table = customerReportBuilder.GetOneDayCustomerReport( startOrderDate, endOrderDate );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.Yesterday)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0 ).AddDays( -1 );
            table = customerReportBuilder.GetOneDayCustomerReport( startOrderDate, endOrderDate );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.Last24Hours)
        {
            day = DateTime.Now.AddHours( -23 );
            table = customerReportBuilder.Get24HoursCustomerReport( startOrderDate, endOrderDate );
            CreateDayData( table, day );
        }
        else if (Period == PeriodType.ThisMonth)
        {
            day = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 );
            numDay = new DateTime( DateTime.Now.Year, DateTime.Now.Month, 1 ).AddMonths( 1 ).AddSeconds( -1 ).Day;
            table = customerReportBuilder.GetCustomerReport( startOrderDate, endOrderDate );
            CreateMonthData( table, day, numDay );
        }
        else if (Period == PeriodType.ThisYear)
        {
            day = new DateTime( DateTime.Now.Year, 1, 1 );
            int numMonth = 12;
            table = customerReportBuilder.GetYearCustomerReport( startOrderDate, endOrderDate );
            CreateYearData( table, day, numMonth );
        }
        else if (Period == PeriodType.Last3Years)
        {
            day = new DateTime( DateTime.Now.Year, 1, 1 ).AddYears( -2 );
            int numMonth = 36;
            table = customerReportBuilder.GetYearCustomerReport( startOrderDate, endOrderDate );
            CreateYearData( table, day, numMonth );
        }
        else if (Period == PeriodType.Custom)
        {
            PeriodType customPeriod = ReportFilterUtilities.ResolveCustomPeriodType( startOrderDate, endOrderDate );
            if (customPeriod == PeriodType.ThisYear)
            {
                day = new DateTime( startOrderDate.Year, startOrderDate.Month, startOrderDate.Day );
                int yearDiff = endOrderDate.Year - startOrderDate.Year;
                int monthDiff = endOrderDate.Month - startOrderDate.Month;
                yearDiff = yearDiff * 12;
                int numMonth = yearDiff + monthDiff;
                table = customerReportBuilder.GetYearCustomerReport( startOrderDate, endOrderDate );
                CreateYearData( table, day, numMonth + 1 );
            }
            else if (customPeriod == PeriodType.Today)
            {
                table = customerReportBuilder.GetOneDayCustomerReport( startOrderDate, endOrderDate );
                CreateDayData( table, startOrderDate );
            }
            else
            {
                TimeSpan dateDifference = endOrderDate.Subtract( startOrderDate );
                int days = dateDifference.Days;
                table = customerReportBuilder.GetCustomerReport( startOrderDate, endOrderDate );
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
                    maxRange = val;
                    if (val < InitialMaxYAxisRange)
                    {
                        maxRange = InitialMaxYAxisRange;
                    }
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
