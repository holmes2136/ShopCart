using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using System.Data;
using Vevo.Reports;
using Vevo.Reports.Exporters;
using Vevo.Domain;

public partial class Admin_MainControls_ReportStock : AdminAdvancedBaseUserControl
{
    #region Private
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "Stock" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void PopulateControls()
    {
        if (!IsAdminModifiable())
        {
            uxExportButton.Visible = false;
        }
    }

    private void RefreshGrid()
    {
        if (!MainContext.IsPostBack)
            uxPagingControl.ItemsPerPages = AdminConfig.OrderItemsPerPage;
        int totalItems;

        string cultureID = CultureUtilities.DefaultCultureID;
        StockReportBuilder stockReportBuilder = new StockReportBuilder();

        DataTable table;
        if (uxReportDrop.SelectedIndex == 0)
            table = stockReportBuilder.GetLowStockReport(
                cultureID,
                uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );
        else
            table = stockReportBuilder.GetAllStockReport(
                cultureID,
                uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );

        table.DefaultView.Sort = GridHelper.GetFullSortText();

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = table;
        uxGrid.DataBind();
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
    }

    protected void uxReportDrop_SelectIndexedChange( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
        uxFileNameLink.Text = "";
        uxFileNameLink.NavigateUrl = "";
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

        string cultureID = CultureUtilities.DefaultCultureID;
        StockReportExporter exporter = new StockReportExporter();
        if (uxReportDrop.SelectedIndex == 0)
        {
            fileNameLinkURL = exporter.ExportLowStockReportData(
                filePhysicalPathName,
                GridHelper.GetFullSortText(),
                cultureID,
                out message,
                out message1,
                out fileNameLinkText );
        }
        else
        {
            fileNameLinkURL = exporter.ExportAllStockReportData(
                filePhysicalPathName,
                GridHelper.GetFullSortText(),
                cultureID,
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
    }

    #endregion
}
