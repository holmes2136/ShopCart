using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Reports;
using Vevo.Reports.Exporters;

public partial class AdminAdvanced_MainControls_ReportShipping : AdminAdvancedBaseUserControl
{
    #region Private

    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private static bool _errorFlag;
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ShippingMethod" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void PopulateControls()
    {
        if (!IsAdminModifiable())
        {
            uxExportButton.Visible = false;
        }

        uxPeriodDrop.DataSource = ReportFilterUtilities.GetPeriodList();
        uxPeriodDrop.DataBind();
        uxPeriodDrop.SelectedIndex = 5;

        uxPeriodDrop.Attributes.Add( "onchange", "GetCustomDate(this.value,'" + uxSetDate.ClientID + "')" );
    }

    private void setCustomDateDisplay()
    {
        if (uxPeriodDrop.SelectedItem.ToString() == "Custom")
        {
            uxSetDate.Style.Add( "display", "block" );
        }
        else
        {
            uxSetDate.Style.Add( "display", "none" );
        }
    }

    private void CreateChart()
    {
        HttpBrowserCapabilities browser = Request.Browser;
        string browserName = browser.Browser;

        if (uxPeriodDrop.SelectedItem.ToString() == "Custom")
        {
            string script = "<script language='javascript'>CreateShippingChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value, '" +
                _startOrderDate.ToShortDateString() + "','" +
                _endOrderDate.ToShortDateString() + "','" +
                browserName +
                "')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
        else
        {
            string script = "<script language='javascript'>CreateShippingChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value, " +
                "'null','null','" + browserName + "')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
    }

    private string GenerateHeader()
    {
        return "\"Shipping Method\",\"Use Count\",\"Percent\"";
    }

    private string GenerateDetail( DataRow row )
    {
        string name, useCount, percentage;
        name = row["ShippingMethod"].ToString();
        useCount = row["SumShippingMethod"].ToString();
        percentage = String.Format( "{0:n2} %", row["Percentage"].ToString() );

        return name + "," + useCount + "," + percentage;
    }

    private bool VerifyCustomInput( out string errorMessage )
    {
        if (String.IsNullOrEmpty( uxStartDateCalendarPopup.SelectedDateText ) &&
            String.IsNullOrEmpty( uxEndDateCalendarPopUp.SelectedDateText ))
        {
            errorMessage = "Please selected Date";
            uxGrid.DataSource = null;
            uxGrid.DataBind();
            return false;
        }
        else if (uxEndDateCalendarPopUp.SelectedDate < uxStartDateCalendarPopup.SelectedDate)
        {
            errorMessage = "Start date cannot greater than End date";
            uxGrid.DataSource = null;
            uxGrid.DataBind();
            return false;
        }

        errorMessage = String.Empty;
        return true;
    }

    private void LoadCustomInput()
    {
        if (String.IsNullOrEmpty( uxStartDateCalendarPopup.SelectedDateText ) &&
            !String.IsNullOrEmpty( uxEndDateCalendarPopUp.SelectedDateText ))
        {
            _endOrderDate = uxEndDateCalendarPopUp.SelectedDate;
            _startOrderDate = _endOrderDate;
        }
        else if (!String.IsNullOrEmpty( uxStartDateCalendarPopup.SelectedDateText ) &&
            String.IsNullOrEmpty( uxEndDateCalendarPopUp.SelectedDateText ))
        {
            _startOrderDate = uxStartDateCalendarPopup.SelectedDate;
            _endOrderDate = _startOrderDate;
        }
        else
        {
            _startOrderDate = uxStartDateCalendarPopup.SelectedDate;
            _endOrderDate = uxEndDateCalendarPopUp.SelectedDate;
        }
    }

    private void RefreshGrid()
    {
        if (!MainContext.IsPostBack)
            uxPagingControl.ItemsPerPages = AdminConfig.OrderItemsPerPage;
        int totalItems;
        _errorFlag = false;

        PeriodType period = ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue );
        if (period == PeriodType.Custom)
        {
            string errorMessage;
            if (VerifyCustomInput( out errorMessage ))
            {
                LoadCustomInput();
            }
            else
            {
                uxMessage.DisplayError( errorMessage );
                uxMessage1.Visible = false;
                _errorFlag = true;
                return;
            }
        }
        else
        {
            ReportFilterUtilities.GetOrderDateRange( period, out _startOrderDate, out _endOrderDate );
        }
        ShippingReportBuilder shippingReportBuilder = new ShippingReportBuilder();
        DataTable table = shippingReportBuilder.GetShippingReportData(
            GridHelper.GetFullSortText(),
            period,
            _startOrderDate,
            _endOrderDate,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (table.Rows.Count == 0)
        {
            uxDisplayChart.Style.Add( "Display", "none" );
        }
        else
        {
            uxDisplayChart.Style.Add( "Display", "" );
        }

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = table;
        uxGrid.DataBind();
        setCustomDateDisplay();
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
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            HttpBrowserCapabilities browser = Request.Browser;
            string browserName = browser.Browser;

            PopulateControls();
            RefreshGrid();

            string script = "<script language='javascript'>CreateShippingChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value," +
                "'null','null','" + browserName + "')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
        if (uxPeriodDrop.SelectedItem.ToString() == "Custom")
        {
            uxSetDate.Style.Add( "display", "block" );
        }
        else
        {
            uxSetDate.Style.Add( "display", "none" );
        }
    }

    protected void uxRefreshButton_Click( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
        uxFileNameLink.Text = "";
        uxFileNameLink.NavigateUrl = "";
        setCustomDateDisplay();
        if (_errorFlag == false)
        {
            CreateChart();
        }
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
        CreateChart();
    }

    protected void uxExportButton_Click( object sender, EventArgs e )
    {
        string message, message1, filePhysicalPathName, fileNameLinkText, fileNameLinkURL;

        filePhysicalPathName = Server.MapPath( "../" );

        ShippingReportExporter exporter = new ShippingReportExporter();
        fileNameLinkURL = exporter.ExportShippingReportData(
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
            filePhysicalPathName,
            _startOrderDate,
            _endOrderDate,
            out message,
            out message1,
            out fileNameLinkText );

        if (String.IsNullOrEmpty( fileNameLinkURL ))
        {
            uxMessage.DisplayError( message );
            uxMessage1.Visible = false;
            uxFileNameLink.Text = fileNameLinkText;
            uxFileNameLink.NavigateUrl = fileNameLinkURL;
        }
        else
        {
            uxMessage.DisplayMessage( message );
            uxMessage1.Visible = true;
            uxMessage1.DisplayMessageNoNewLIne( message1 );
            uxFileNameLink.Text = fileNameLinkText;
            uxFileNameLink.NavigateUrl = fileNameLinkURL;
            uxFileNameLink.Target = "_blank";
        }

        CreateChart();
        setCustomDateDisplay();
    }

    protected string GetShippingMethod( string shippingMethod )
    {
        if (String.IsNullOrEmpty( shippingMethod ))
        {
            return "Non-Shipping Order";
        }
        else
        {
            return shippingMethod;
        }
    }

    #endregion
}
