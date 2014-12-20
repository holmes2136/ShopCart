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

public partial class AdminAdvanced_DataFile_ShippingReportData : AdminAdvancedBasePage
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
            chart.Title = new Title( "Shipping Report" + " ( From " + _startOrderDate.ToShortDateString()
                            + " To " + _endOrderDate.ToShortDateString() + ")" );
        }
        else
        {
            chart.Title = new Title( "Shipping Report" + " (" + ReportFilterUtilities.ConvertToPeriodText( Period ) + ")" );
        }
        chart.Bgcolor = "#ffffff";
        chart.Title.Style = "{font-size: 17px; font-family: Times New Roman; font-weight: bold; " +
            " color: #9bbde0; text-align: center; wmode: transparent; padding-bottom: 30px;}";
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
        pie.StartAngle = 45;
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

        ShippingReportBuilder shippingReportBuilder = new ShippingReportBuilder();
        DataTable table = shippingReportBuilder.GetShippingReportData(
            Period,
            _startOrderDate,
            _endOrderDate );

        for (int i = 0; i < table.Rows.Count; i++)
        {
            string shippingMethod;
            if (String.IsNullOrEmpty( table.Rows[i]["ShippingMethod"].ToString() ))
            {
                shippingMethod = "Non-shipping Order";
            }
            else
            {
                shippingMethod = table.Rows[i]["ShippingMethod"].ToString();
            }
            _values.Add( new PieValue( Convert.ToDouble( table.Rows[i]["SumShippingMethod"] ), shippingMethod ) );
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
