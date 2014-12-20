using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Deluxe.Domain.QuickBooks;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_QuickBooksLog : AdminAdvancedBaseListControl
{
    private string qbLogID;
    
    private void PopulateSumary()
    {
        IList<QuickBooksLogSummary> table = DataAccessContextDeluxe.QuickBooksLogRepository.GetQBLogSummaryByQBLogID( qbLogID );

        uxGridDetail.DataSource = table;
        uxGridDetail.DataBind();
    }

    private void PopulateItem()
    {
        IList<QuickBooksLogItem> table = DataAccessContextDeluxe.QuickBooksLogRepository.GetQBLogItemByQBLogID( qbLogID );

        uxItemGrid.DataSource = table;
        uxItemGrid.DataBind();
    }

    private void PopulaControls()
    {
        IList<QuickBooksLog> table = DataAccessContextDeluxe.QuickBooksLogRepository.GetAll( String.Empty );
        ListItem item;
        uxQBLogDrop.Items.Clear();
        
        if (table.Count == 0)
        {
            qbLogID = "0";
            uxQBLogDrop.Items.Add( "Today" );
            uxDeleteButton.Visible = false;
        }
        else
        {
            qbLogID = DataAccessContextDeluxe.QuickBooksLogRepository.GetLastQBLogID();
            uxDeleteButton.Visible = true;
        }

        PopulateSumary();
        PopulateItem();

        foreach (QuickBooksLog log in table)
        {
            item = new ListItem();
            item.Text = log.QBExportDate.ToString();
            item.Value = log.QBLogID;
            uxQBLogDrop.Items.Add( item );
        }

        if (!qbLogID.Equals( "0" ))
        {
            uxQBLogDrop.SelectedValue = qbLogID;
        }

        if (IsAdminModifiable())
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteButton.Attributes.Add(
                    "onclick",
                    "if (confirm('Are you sure to delete selected item(s)?')) {} else {return false}" );
            }
        }
        else
        {
            uxDeleteButton.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        PopulaControls();
    }

    protected override void RefreshGrid()
    {
        PopulaControls();
    }

    protected void uxItemGrid_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (!qbLogID.Equals( "0" ))
        {
            string statusSeverity = e.Row.Cells[3].Text.Trim();
            if (statusSeverity.ToLower() == "error")
            {
                e.Row.Cells[3].Style.Add( "color", "#ff0000" );
            }
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        DataAccessContextDeluxe.QuickBooksLogRepository.Delete( uxQBLogDrop.SelectedValue );
        PopulaControls();
        uxMessage.DisplayMessage( Resources.ContentMessages.DeleteSuccess );
    }

    protected void uxQBLogDrop_OnSelectedIndexChanged( object sender, EventArgs e )
    {
        qbLogID = uxQBLogDrop.SelectedValue;

        PopulateSumary();
        PopulateItem();
    }
}
