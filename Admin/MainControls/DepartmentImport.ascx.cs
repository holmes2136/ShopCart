using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.ImportExport;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;

public partial class Admin_MainControls_DepartmentImport : AdminAdvancedBaseUserControl
{
    private const int TimeOut = 36000;

    private string CurrentStoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
                return uxStoreDrop.SelectedValue;
            else
                return Store.Null.StoreID;
        }
    }

    private void PopulateControls()
    {
        uxLanguageDrop.SelectedValue = CultureUtilities.StoreCultureID;

        WebUtilities.TieButton( this.Page, uxDepartmentCsvFileNameText, uxDepartmentImportButton );
    }

    private void AddDepartmentMessageHeader( CategoryImportHelperStatus status, Panel uxMessagePanel )
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

    private void AddFatalErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = status.ErrorMessage + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddOtherErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.OtherError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddDepartmentFieldErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.FieldError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddRootDepartmentErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.RootError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddParentContainProductErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.ParentContainProductError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddRootChangedErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.RootChangedError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel, bool isImportCustomer )
    {
        AddDepartmentMessageHeader( status, uxMessagePanel );
        AddOtherErrorMessage( status, uxMessagePanel );
        AddDepartmentFieldErrorMessage( status, uxMessagePanel );
        AddRootDepartmentErrorMessage( status, uxMessagePanel );
        AddParentContainProductErrorMessage( status, uxMessagePanel );
        AddRootChangedErrorMessage( status, uxMessagePanel );
    }

    private void UpdateImportMessage( CategoryImportHelperStatus status, Panel uxMessagePanel, bool isImportCustomer )
    {
        switch (status.Status)
        {
            case CategoryImportHelperStatus.ErrorStatus.Success:
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

            case CategoryImportHelperStatus.ErrorStatus.DoneWithErrors:
                AddErrorMessage( status, uxMessagePanel, isImportCustomer );
                break;

            case CategoryImportHelperStatus.ErrorStatus.Fatal:
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

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        uxStoreDrop.Items.Add( new ListItem( "Default Value", Store.Null.StoreID ) );
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[index].StoreName, storeList[index].StoreID ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        ScriptManager uxScriptManager = (ScriptManager) Page.FindControl( "uxScriptManager" );
        uxScriptManager.AsyncPostBackTimeout = TimeOut;

        if (!MainContext.IsPostBack)
        {
            InsertStoreInDropDownList();
            PopulateControls();

            uxDepartmentCsvFileUpload.ReturnTextControlClientID = uxDepartmentCsvFileNameText.ClientID;

            uxDepartmentImportModeRadioList.SelectedIndex = 0;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxDepartmentCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxDepartmentCsvFileUpload.ShowControl = true;
    }

    protected void uxDepartmentImportButton_Click( object sender, EventArgs e )
    {
        uxImportDepartmentMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxDepartmentCsvFileNameText.Text.Trim() );
        if (!File.Exists( localFilePath ))
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportDepartmentMessagePanel.Controls.Add( label );
            return;
        }

        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue );
        CategoryImportHelper importer = new CategoryImportHelper( culture, "Department" );
        CategoryImportHelperStatus status = new CategoryImportHelperStatus( "Department" );
        TimeSpan timeDiff;

        switch (uxDepartmentImportModeRadioList.SelectedValue)
        {
            case "Overwrite":
                status = importer.ImportDepartmentOverWrite(
                    "~/" + uxDepartmentCsvFileNameText.Text.Trim(),
                    CurrentStoreID,
                    out timeDiff );
                UpdateImportMessage( status, uxImportDepartmentMessagePanel, true );
                UpdateTimeMessage( timeDiff, uxImportDepartmentMessagePanel );
                break;

            default: break;
        }

        AdminUtilities.ClearAllCache();
    }
}
