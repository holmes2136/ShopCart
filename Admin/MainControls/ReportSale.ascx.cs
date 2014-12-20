using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Reports;
using Vevo.Reports.Exporters;
using Vevo.Shared.Utilities;
public partial class AdminAdvanced_MainControls_ReportSale : AdminAdvancedBaseUserControl
{
    #region Private

    private DataTable _table;
    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private static bool _errorFlag;
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "Period" );

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

        uxReportDrop.DataSource = ReportFilterUtilities.GetSaleReportList();
        uxReportDrop.DataBind();

        uxPeriodDrop.Attributes.Add( "onchange", "GetCustomDate(this.value,'" + uxSetDate.ClientID + "')" );
    }

    private void setCustomDateDisplay()
    {
        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
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

        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
        {
            string script = "<script language='javascript'>CreateChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value, '" +
                _startOrderDate.ToShortDateString() + "','" +
                _endOrderDate.ToShortDateString() + "','" +
                browserName +
                "','true','0')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
        else
        {
            string script = "<script language='javascript'>CreateChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value, " +
                "'null','null','" + browserName + "','true','0')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
    }

    private bool VerifyCustomInput( out string errorMessage )
    {
        if (String.IsNullOrEmpty( uxStartDateCalendarPopup.SelectedDateText ) &&
            String.IsNullOrEmpty( uxEndDateCalendarPopUp.SelectedDateText ))
        {
            errorMessage = "Please selected Date";
            return false;
        }
        else if (_endOrderDate < _startOrderDate)
        {
            errorMessage = "Start date cannot greater than End date";
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
        SaleReportBuilder reportBuilder = new SaleReportBuilder();
        _table = reportBuilder.GetReportData(
            GridHelper.GetFullSortText(),
            period,
            _startOrderDate,
            _endOrderDate,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            "0",
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = _table;

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

            string script = "<script language='javascript'>CreateChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value," +
                "'null','null','" + browserName + "','true','0')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );

        }
        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
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

        SaleReportExporter exporter = new SaleReportExporter( CurrencyUtilities.BaseCurrencySymbol );
        fileNameLinkURL = exporter.ExportSellReportData(
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
            filePhysicalPathName,
            _startOrderDate,
            _endOrderDate,
            "0",
            out message,
            out message1,
            out fileNameLinkText
            );

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
        RefreshGrid();
        CreateChart();
        setCustomDateDisplay();
    }

    protected string GetDateTimeText( object registerPeriod )
    {
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
        {
            startDate = uxStartDateCalendarPopup.SelectedDate;
            endDate = uxEndDateCalendarPopUp.SelectedDate;
        }
        return RepositoryDisplayFormatter.GetDateTimeText(
            registerPeriod,
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
            startDate,
            endDate );
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (_table != null)
            {
                decimal total = 0;
                decimal numberOfOrder = 0;
                decimal quantity = 0;

                for (int x = 0; x < _table.Rows.Count; x++)
                {
                    total += ConvertUtilities.ToDecimal( _table.Rows[x]["Total"].ToString() );
                    numberOfOrder += ConvertUtilities.ToInt32( _table.Rows[x]["NumberOfOrder"].ToString() );
                    quantity += ConvertUtilities.ToInt32( _table.Rows[x]["Quantity"].ToString() );
                }

                TableCellCollection cells = e.Row.Cells;

                cells[0].Text = " Total ";
                cells[0].HorizontalAlign = HorizontalAlign.Center;
                cells[0].Font.Bold = true;

                cells[1].CssClass = "TotalReport";
                cells[1].Text = AdminUtilities.FormatPrice( total );

                cells[2].CssClass = "TotalQuantity";
                cells[2].Text = "" + numberOfOrder;

                cells[3].CssClass = "TotalReport";
                cells[3].Text = AdminUtilities.FormatPrice( total / numberOfOrder );

                cells[4].CssClass = "TotalQuantity";
                cells[4].Text = "" + quantity;

                cells[5].CssClass = "TotalQuantity";
                cells[5].Text = "" + (quantity / numberOfOrder);
            }
        }

    }
    #endregion
}
