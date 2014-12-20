using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Reports;
using Vevo.Reports.Exporters;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_MainControls_ReportCustomer : AdminAdvancedBaseUserControl
{
    #region Private
    DataTable _table;
    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private static bool _errorFlag;

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "RegisterPeriod" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private GridViewHelper GridHelper1
    {
        get
        {
            if (ViewState["GridHelper1"] == null)
                ViewState["GridHelper1"] = new GridViewHelper( uxTopCustomerGrid, "Total" );

            return (GridViewHelper) ViewState["GridHelper1"];
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

        uxReportDrop.DataSource = ReportFilterUtilities.GetCustomerReportList();
        uxReportDrop.DataBind();

        uxNumberItemsDrop.DataSource = ReportFilterUtilities.GetNumberOfItemsList();
        uxNumberItemsDrop.DataBind();

        uxPeriodDrop.Attributes.Add( "onchange", "GetCustomDate(this.value,'" + uxSetDate.ClientID + "')" );
        uxReportDrop.Attributes.Add( "onchange", "GetCustomerRegister(this.value,'" + uxSetItems.ClientID + "')" );
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

        if (ReportFilterUtilities.ConvertToCustomerReportType( uxReportDrop.SelectedValue ) != CustomerReportType.UserRegistration)
        {
            uxSetItems.Style.Add( "display", "block" );
        }
        else
        {
            uxSetItems.Style.Add( "display", "none" );
        }

    }

    private void setTitle()
    {
        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
        {
            uxTitleLabel.Text = uxReportDrop.SelectedItem.ToString() + " ( From " +
                uxStartDateCalendarPopup.SelectedDate.ToShortDateString() + " To " +
                uxEndDateCalendarPopUp.SelectedDate.ToShortDateString() + ")";
        }
        else
        {
            uxTitleLabel.Text = uxReportDrop.SelectedItem.ToString() + " (" +
                uxPeriodDrop.SelectedItem.Text + ")";
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
        CustomerReportType customerReportType = ReportFilterUtilities.ConvertToCustomerReportType( uxReportDrop.SelectedValue );
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

        
        CustomerReportBuilder customerReportBuilder = new CustomerReportBuilder();
        if (customerReportType != CustomerReportType.UserRegistration)
        {
            _table = customerReportBuilder.GetTopCustomerReportData(
                GridHelper1.GetFullSortText(),
                period,
                customerReportType,
                uxNumberItemsDrop.SelectedItem.ToString(),
                _startOrderDate,
                _endOrderDate,
                uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );

            uxTopCustomerGrid.DataSource = _table;
            uxTopCustomerGrid.DataBind();
            uxTopCustomerGrid.Visible = true;
            uxGrid.Visible = false;
            uxPadding.Visible = false;

            if (_table.Rows.Count > 0)
            {
                uxTitleLabel.Visible = true;
                setTitle();
            }
            else
            {
                uxTitleLabel.Visible = false;
            }
        }
        else
        {
            _table = customerReportBuilder.GetCustomerReportData(
                GridHelper.GetFullSortText(),
                period,
                _startOrderDate,
                _endOrderDate,
                uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );

            uxGrid.DataSource = _table;
            uxGrid.DataBind();
            uxGrid.Visible = true;
            uxTopCustomerGrid.Visible = false;
            uxPadding.Visible = true;
            uxTitleLabel.Visible = false;
        }

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        setCustomDateDisplay();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
        if (uxReportDrop.SelectedItem.ToString() == "Customer Registration")
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
            if (_errorFlag == false)
            {
                if (uxReportDrop.SelectedItem.ToString() == "Customer Registration")
                {
                    string script = "<script language='javascript'>CreateCustomerChart(document.getElementById('" +
                        uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value," +
                    "'null','null','" + browserName + "')</script>";
                    ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
                }
            }
        }
        setCustomDateDisplay();
    }

    protected void uxRefreshButton_Click( object sender, EventArgs e )
    {
        if (uxReportDrop.SelectedItem.ToString() == "Top Buyer by Quantity")
        {
            ViewState["GridHelper1"] = new GridViewHelper( uxTopCustomerGrid, "NumberOfOrder", GridViewHelper.Direction.DESC );
        }
        else if (uxReportDrop.SelectedItem.ToString() == "Top Buyer by Price")
        {
            ViewState["GridHelper1"] = new GridViewHelper( uxTopCustomerGrid, "Total", GridViewHelper.Direction.DESC );
        }
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
        uxFileNameLink.Text = "";
        uxFileNameLink.NavigateUrl = "";
        setCustomDateDisplay();
        if (_errorFlag == false && uxReportDrop.SelectedItem.ToString() == "Customer Registration")
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

    protected void uxTopCustomer_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper1.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    private void CreateChart()
    {
        HttpBrowserCapabilities browser = Request.Browser;
        string browserName = browser.Browser;

        if (ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ) == PeriodType.Custom)
        {
            string script = "<script language='javascript'>CreateCustomerChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value, '" +
                _startOrderDate.ToShortDateString() + "','" +
                _endOrderDate.ToShortDateString() + "','" +
                browserName +
                "')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
        else
        {
            string script = "<script language='javascript'>CreateCustomerChart(document.getElementById('" +
                uxPeriodDrop.ClientID + "').value,document.getElementById('" + uxReportDrop.ClientID + "').value, " +
                "'null','null','" + browserName + "')</script>";
            ScriptManager.RegisterStartupScript( this, GetType(), "startScript", script, false );
        }
    }

    protected void uxExportButton_Click( object sender, EventArgs e )
    {
        string message, message1, filePhysicalPathName, fileNameLinkText, fileNameLinkURL;
        CustomerReportType customerReportType = ReportFilterUtilities.ConvertToCustomerReportType( uxReportDrop.SelectedValue );

        filePhysicalPathName = Server.MapPath( "../" );

        if (customerReportType == CustomerReportType.UserRegistration)
        {
            CustomerReportExporter exporter = new CustomerReportExporter();
            fileNameLinkURL = exporter.ExportCustomerReportData(
                customerReportType,
                ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
                filePhysicalPathName,
                _startOrderDate,
                _endOrderDate,
                out message,
                out message1,
                out fileNameLinkText );
        }

        else
        {
            CustomerReportExporter exporter = new CustomerReportExporter();
            fileNameLinkURL = exporter.ExportCustomerReportData(
                ReportFilterUtilities.ConvertToCustomerReportType( uxReportDrop.SelectedValue ),
                ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
                uxNumberItemsDrop.SelectedItem.ToString(),
                filePhysicalPathName,
                _startOrderDate,
                _endOrderDate,
                out message,
                out message1,
                out fileNameLinkText );
        }

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

        if (uxReportDrop.SelectedItem.ToString() == "Customer Registration")
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
                int Register = 0;

                for (int x = 0; x < _table.Rows.Count; x++)
                {
                    Register += ConvertUtilities.ToInt32( _table.Rows[x]["RegisterCustomer"].ToString() );
                }

                TableCellCollection cells = e.Row.Cells;

                cells[0].Text = " Total ";
                cells[0].HorizontalAlign = HorizontalAlign.Center;
                cells[0].Font.Bold = true;

                cells[1].CssClass = "TotalQuantity";
                cells[1].Text = "" + Register;
            }
        }

    }
    #endregion
}
