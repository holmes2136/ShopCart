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
using Vevo.Domain;
using Vevo.Reports;
using Vevo.Reports.Exporters;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ReportBestSeller : AdminAdvancedBaseUserControl
{
    #region Private

    private static DateTime _startOrderDate;
    private static DateTime _endOrderDate;
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ProductPrice", GridViewHelper.Direction.DESC );

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

        uxReportDrop.DataSource = ReportFilterUtilities.GetBestSellingReportList();
        uxReportDrop.DataBind();

        uxNumberItemsDrop.DataSource = ReportFilterUtilities.GetNumberOfItemsList();
        uxNumberItemsDrop.DataBind();

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

        if (_endOrderDate < _startOrderDate)
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
                return;
            }
        }
        else
        {
            ReportFilterUtilities.GetOrderDateRange( period, out _startOrderDate, out _endOrderDate );
        }

        BestSellingReportBuilder bestSellingReportBuilder = new BestSellingReportBuilder();
        DataTable table = bestSellingReportBuilder.GetBestSellingReportData(
            GridHelper.GetFullSortText(),
            StoreContext.Culture.CultureID,
            period,
            ReportFilterUtilities.ConvertToBestSellReportType( uxReportDrop.SelectedValue ),
            uxNumberItemsDrop.SelectedItem.ToString(),
            StoreContext.CurrentStore.StoreID,
            _startOrderDate,
            _endOrderDate,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (table.Rows.Count > 0)
        {
            uxTitleLabel.Visible = true;
            setTitle();
        }
        else
        {
            uxTitleLabel.Visible = false;
        }

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = table;
        uxGrid.DataBind();
        setCustomDateDisplay();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPagingControl.BubbleEvent += new EventHandler( uxGrid_RefreshHandler );

        if (!MainContext.IsPostBack)
        {
            PopulateControls();
            RefreshGrid();
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
        if (uxReportDrop.SelectedItem.ToString() == "Number Of Product")
        {
            ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ProductQuantity", GridViewHelper.Direction.DESC );
        }
        else
        {
            ViewState["GridHelper"] = new GridViewHelper( uxGrid, "ProductPrice", GridViewHelper.Direction.DESC );
        }
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
        uxFileNameLink.Text = "";
        uxFileNameLink.NavigateUrl = "";
        setCustomDateDisplay();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxExportButton_Click( object sender, EventArgs e )
    {
        string message, message1, filePhysicalPathName, fileNameLinkText, fileNameLinkURL;

        filePhysicalPathName = Server.MapPath( "../" );

        BestSellerReportExporter exporter = new BestSellerReportExporter(
            CurrencyUtilities.BaseCurrencySymbol, StoreContext.Culture.CultureID );

        fileNameLinkURL = exporter.ExportBestSellerReportData(
            ReportFilterUtilities.ConvertToBestSellReportType( uxReportDrop.SelectedValue ),
            ReportFilterUtilities.ConvertToPeriodType( uxPeriodDrop.SelectedValue ),
            uxNumberItemsDrop.SelectedItem.ToString(),
            filePhysicalPathName,
            StoreContext.CurrentStore.StoreID,
            _startOrderDate,
            _endOrderDate,
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

        setCustomDateDisplay();
    }

    #endregion
}
