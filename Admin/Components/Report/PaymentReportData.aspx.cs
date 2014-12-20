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

public partial class AdminAdvanced_DataFile_PaymentReportData : AdminAdvancedBasePage
{
    List<PieValue> _values = new List<PieValue>();
    DateTime _startOrderDate;
    DateTime _endOrderDate;

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

    private void SetUpChart( OpenFlashChart.OpenFlashChart chart )
    {
        if (Period == PeriodType.Custom)
        {
            chart.Title = new Title( "Payment Report" + " ( From " + _startOrderDate.ToShortDateString()
                            + " To " + _endOrderDate.ToShortDateString() + ")" );
            chart.Title.Style = "{font-size: 17px; color: #9bbde0; text-align: center; wmode: transparent; " +
                " padding-bottom: 30px;}";
        }
        else
        {
            chart.Title = new Title( "Payment Report" + " (" + ReportFilterUtilities.ConvertToPeriodText( Period ) + ")" );
            chart.Title.Style = "{font-size: 17px; color: #9bbde0; text-align: center; wmode: transparent; " +
                " padding-bottom: 30px;}";
        }
        chart.Bgcolor = "#ffffff";

    }

    private void SetUpPie( OpenFlashChart.Pie pie )
    {
        pie.Values = _values;
        pie.Colours = new string[] { 
            "#e03c3c", 
            "#533ce0", 
            "#17d724", 
            "#f140eb", 
            "#ff9000", 
            "#b400ff" };
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        OpenFlashChart.Pie pie = new OpenFlashChart.Pie();

        if (Period == PeriodType.Custom)
        {
            _startOrderDate = Convert.ToDateTime( StartDate );
            _endOrderDate = Convert.ToDateTime( EndDate );
        }
        else
        {
            ReportFilterUtilities.GetOrderDateRange( Period, out _startOrderDate, out _endOrderDate );
        }

        PaymentReportBuilder paymentReportBuilder = new PaymentReportBuilder();
        DataTable table = paymentReportBuilder.GetPaymentReportData(
            Period,
            _startOrderDate,
            _endOrderDate );

        for (int i = 0; i < table.Rows.Count; i++)
        {
            _values.Add( new PieValue( Convert.ToDouble( table.Rows[i]["SumPaymentMethod"] ), table.Rows[i]["PaymentMethod"].ToString() ) );
        }

        SetUpChart( chart );

        SetUpPie( pie );

        chart.AddElement( pie );

        Response.Clear();
        //Response.CacheControl = "no-cache";
        Response.Write( chart.ToString() );
        Response.End();
    }
}
