using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using System.Drawing;
using Vevo.Domain;
using System.IO;
using Vevo.Domain.ImportExport;

public partial class AdminAdvanced_MainControls_ImportCustomerList : AdminAdvancedBaseUserControl
{
    private const int TimeOut = 36000;

    private void PopulateControls()
    {
        WebUtilities.TieButton( this.Page, uxCustomerCsvFileNameText, uxCustomerImportButton );
        WebUtilities.TieButton( this.Page, uxShippingAddressCsvFileNameText, uxShippingAddressImportButton );
    }

    private void AddCustomerMessageHeader( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = "<b>Import finished with errors.</b><br />" +
            status.ImportedCount + " row(s) imported<br />" +
            status.UpdatedCount + " row(s) updated<br />" +
            status.ErrorCount + " error(s)" + "<br /><br />";
        uxMessagePanel.Controls.Add( label );
        uxMessagePanel.CssClass = "mgl30";
    }

    private void AddShippingMessageHeader( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = "<b>Import finished with errors.</b><br />" +
            status.ImportedCount + " row(s) imported<br />" +
            status.SkippedCount + " row(s) skipped<br />" +
            status.ErrorCount + " error(s)" + "<br /><br />";
        uxMessagePanel.Controls.Add( label );
        uxMessagePanel.CssClass = "mgl30";
    }

    private void AddFatalErrorMessage( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = status.ErrorMessage + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddCustomerNotFoundError( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.CustomerNotFoundError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddOtherErrorMessage( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.OtherError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddSkippedErrorMessage( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.SkippedCustomerMessage + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddCustomerFieldErrorMessage( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.FieldError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddShippingFieldErrorMessage( CustomerImportStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.ShippingFieldError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddErrorMessage( CustomerImportStatus status, Panel uxMessagePanel, bool isImportCustomer )
    {
        if (isImportCustomer)
        {
            AddCustomerMessageHeader( status, uxMessagePanel );
            AddOtherErrorMessage( status, uxMessagePanel );
            AddCustomerFieldErrorMessage( status, uxMessagePanel );
        }
        else
        {
            AddShippingMessageHeader( status, uxMessagePanel );
            AddOtherErrorMessage( status, uxMessagePanel );
            AddSkippedErrorMessage( status, uxMessagePanel );
            AddCustomerNotFoundError( status, uxMessagePanel );
            AddShippingFieldErrorMessage( status, uxMessagePanel );
        }
    }

    private void UpdateImportMessage( CustomerImportStatus status, Panel uxMessagePanel, bool isImportCustomer )
    {
        switch (status.Status)
        {
            case CustomerImportStatus.ErrorStatus.Success:
                Label successlabel = new Label();
                successlabel.ForeColor = Color.Blue;
                if (isImportCustomer)
                {
                    successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.UpdatedCount + " row(s) updated<br />" +
                    status.ErrorCount + " error(s)<br />";
                }
                else
                {
                    successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.UpdatedCount + " row(s) updated<br />" +
                    status.ErrorCount + " error(s)<br />";
                }
                uxMessagePanel.Controls.Add( successlabel );
                uxMessagePanel.CssClass = "mgl30";
                break;

            case CustomerImportStatus.ErrorStatus.DoneWithErrors:
                AddErrorMessage( status, uxMessagePanel, isImportCustomer );
                break;

            case CustomerImportStatus.ErrorStatus.Fatal:
                AddFatalErrorMessage( status, uxMessagePanel );
                break;
        }
    }

    private void UpdateTimeMessage( TimeSpan timeDiff, Panel uxMessagePanel )
    {
        Label timelabel = new Label();
        timelabel.ForeColor = Color.Brown;
        timelabel.Font.Bold = true;
        timelabel.Text = String.Format( "<br />Used time : {0}:{1}:{2}:{3}", timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds, timeDiff.Milliseconds );
        uxMessagePanel.Controls.Add( timelabel );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        ScriptManager uxScriptManager = (ScriptManager) Page.FindControl( "uxScriptManager" );
        uxScriptManager.AsyncPostBackTimeout = TimeOut;

        if (!MainContext.IsPostBack)
        {
            PopulateControls();

            uxCustomerCsvFileUpload.ReturnTextControlClientID = uxCustomerCsvFileNameText.ClientID;
            uxShippingAddressCsvFileUpload.ReturnTextControlClientID = uxShippingAddressCsvFileNameText.ClientID;

            uxCustomerImportModeRadioList.SelectedIndex = 0;
            uxShippingAddressImportModeRadioList.SelectedIndex = 0;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxCustomerCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxCustomerCsvFileUpload.ShowControl = true;
    }

    protected void uxShippingAddressCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxShippingAddressCsvFileUpload.ShowControl = true;
    }

    protected void uxCustomerImportButton_Click( object sender, EventArgs e )
    {
        uxImportCustomerMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxCustomerCsvFileNameText.Text.Trim() );
        if (!File.Exists( localFilePath ))
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportCustomerMessagePanel.Controls.Add( label );
            return;
        }

        CustomerImporter importer = new CustomerImporter();
        CustomerImportStatus status = new CustomerImportStatus();
        TimeSpan timeDiff;

        switch (uxCustomerImportModeRadioList.SelectedValue)
        {
            case "Overwrite":
                status = importer.ImportCustomerOverWrite(
                    "~/" + uxCustomerCsvFileNameText.Text.Trim(), out timeDiff );
                UpdateImportMessage( status, uxImportCustomerMessagePanel, true );
                UpdateTimeMessage( timeDiff, uxImportCustomerMessagePanel );
                break;

            default: break;
        }

        AdminUtilities.ClearAllCache();
    }

    protected void uxShippingAddressImportButton_Click( object sender, EventArgs e )
    {
        uxImportShippingAddressMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxShippingAddressCsvFileNameText.Text.Trim() );
        if (!File.Exists( localFilePath ))
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportShippingAddressMessagePanel.Controls.Add( label );
            return;
        }

        CustomerImporter importer = new CustomerImporter();
        CustomerImportStatus status = new CustomerImportStatus();
        TimeSpan timeDiff;

        switch (uxShippingAddressImportModeRadioList.SelectedValue)
        {
            case "Purge":
                status = importer.ImportShippingAddressPurgeAll(
                    "~/" + uxShippingAddressCsvFileNameText.Text.Trim(), out timeDiff );
                UpdateImportMessage( status, uxImportShippingAddressMessagePanel, false );
                UpdateTimeMessage( timeDiff, uxImportShippingAddressMessagePanel );
                break;

            case "Overwrite":
                status = importer.ImportShippingAddressOverWrite(
                    "~/" + uxShippingAddressCsvFileNameText.Text.Trim(), out timeDiff );
                UpdateImportMessage( status, uxImportShippingAddressMessagePanel, false );
                UpdateTimeMessage( timeDiff, uxImportShippingAddressMessagePanel );
                break;

            default: break;
        }

        AdminUtilities.ClearAllCache();
    }
}
