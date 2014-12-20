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

public partial class Admin_MainControls_CategoryImport : AdminAdvancedBaseUserControl
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

        WebUtilities.TieButton( this.Page, uxCategoryCsvFileNameText, uxCategoryImportButton );
    }

    private void AddCategoryMessageHeader( CategoryImportHelperStatus status, Panel uxMessagePanel )
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

    private void AddCategoryFieldErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
    {
        Label label = new Label();
        label.ForeColor = Color.FromArgb( 153, 0, 255 );
        label.Text = status.FieldError + "<br />";
        if (label.Text.Trim() != "<br />")
            uxMessagePanel.Controls.Add( label );
    }

    private void AddRootCategoryErrorMessage( CategoryImportHelperStatus status, Panel uxMessagePanel )
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
        AddCategoryMessageHeader( status, uxMessagePanel );
        AddOtherErrorMessage( status, uxMessagePanel );
        AddCategoryFieldErrorMessage( status, uxMessagePanel );
        AddRootCategoryErrorMessage( status, uxMessagePanel );
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

            uxCategoryCsvFileUpload.ReturnTextControlClientID = uxCategoryCsvFileNameText.ClientID;

            uxCategoryImportModeRadioList.SelectedIndex = 0;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxCategoryCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxCategoryCsvFileUpload.ShowControl = true;
    }

    protected void uxCategoryImportButton_Click( object sender, EventArgs e )
    {
        uxImportCategoryMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxCategoryCsvFileNameText.Text.Trim() );
        if (!File.Exists( localFilePath ))
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportCategoryMessagePanel.Controls.Add( label );
            return;
        }

        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue );
        CategoryImportHelper importer = new CategoryImportHelper( culture, "Category" );
        CategoryImportHelperStatus status = new CategoryImportHelperStatus( "Category" );
        TimeSpan timeDiff;

        switch (uxCategoryImportModeRadioList.SelectedValue)
        {
            case "Overwrite":
                status = importer.ImportCategoryOverWrite(
                    "~/" + uxCategoryCsvFileNameText.Text.Trim(),
                    CurrentStoreID,
                    out timeDiff );
                UpdateImportMessage( status, uxImportCategoryMessagePanel, true );
                UpdateTimeMessage( timeDiff, uxImportCategoryMessagePanel );
                break;

            default: break;
        }

        AdminUtilities.ClearAllCache();
    }
}
