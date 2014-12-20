using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.ImportExport;
using Vevo.Domain.Stores;
using Vevo.Shared.SystemServices;
using Vevo.WebAppLib;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_MainControls_ProductBulkImport : AdminAdvancedBaseUserControl
{
    private const string NewLineString = "<br />";
    private const int TimeOut = 36000;

    private string CurrentStoreID
    {
        get
        {
            if ( KeyUtilities.IsMultistoreLicense() )
                return uxStoreDrop.SelectedValue;
            else
                return Store.Null.StoreID;
        }
    }

    private void PopulateControls()
    {
        if ( !IsAdminModifiable() )
        {
            uxImportButton.Visible = false;
            uxFileNameUploadLinkButton.Visible = false;
        }

        uxImageProcessCheck.Checked = true;
        uxLanguageDrop.SelectedValue = CultureUtilities.StoreCultureID;

        WebUtilities.TieButton( this.Page, uxFileNameText, uxImportButton );
    }

    private void AddErrorMessageHeader( ProductImportStatus status )
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = "<b>Import finished with errors.</b><br />" +
            status.ImportedCount + " row(s) imported<br />" +
            status.ErrorCount + " error(s)" + NewLineString;
        uxMessagePanel.Controls.Add( label );
    }

    private void AddRelatedProductError( ProductImportStatus status )
    {
        Label relatedLabel = new Label();
        relatedLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        relatedLabel.Text = status.RelatedProductError + NewLineString;
        if ( relatedLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( relatedLabel );
    }

    private void AddSKUImportedError( ProductImportStatus status )
    {
        Label skuLabel = new Label();
        skuLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        skuLabel.Text = status.SKUImportedError + NewLineString;
        if ( skuLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( skuLabel );
    }

    private void AddProductNameError( ProductImportStatus status )
    {
        Label nameLabel = new Label();
        nameLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        nameLabel.Text = status.ProductNameError + NewLineString;
        if ( nameLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( nameLabel );
    }

    private void AddCategoryError( ProductImportStatus status )
    {
        Label categoryLabel = new Label();
        categoryLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        categoryLabel.Text = status.CategoryError + NewLineString;
        if ( categoryLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( categoryLabel );
    }

    private void AddDepartmentError( ProductImportStatus status )
    {
        Label departmentLabel = new Label();
        departmentLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        departmentLabel.Text = status.DepartmentError + NewLineString;
        if ( departmentLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( departmentLabel );
    }

    private void AddImageError( ProductImportStatus status )
    {
        Label imageUploadLabel = new Label();
        imageUploadLabel.ForeColor = Color.Red;
        imageUploadLabel.Text = status.ImageUploadError + NewLineString;
        if ( imageUploadLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( imageUploadLabel );
    }

    private void AddOtherError( ProductImportStatus status )
    {
        Label otherErrorLabel = new Label();
        otherErrorLabel.ForeColor = Color.Red;
        otherErrorLabel.Text = status.OtherError + NewLineString;
        if ( otherErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( otherErrorLabel );
    }

    private void AddUseOptionError( ProductImportStatus status )
    {
        Label useOptionErrorLabel = new Label();
        useOptionErrorLabel.ForeColor = Color.Red;
        useOptionErrorLabel.Text = status.UseOptionError + NewLineString;
        if ( useOptionErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( useOptionErrorLabel );
    }

    private void AddImageProcessError( ProductImportStatus status )
    {
        Label useImageProcessErrorLabel = new Label();
        useImageProcessErrorLabel.ForeColor = Color.Red;
        useImageProcessErrorLabel.Text = status.ImageProcessError + NewLineString;
        if ( useImageProcessErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( useImageProcessErrorLabel );
    }

    private void AddRecurringProcessError( ProductImportStatus status )
    {
        Label recurringGiftDownloadErrorLabel = new Label();
        recurringGiftDownloadErrorLabel.ForeColor = Color.Red;
        recurringGiftDownloadErrorLabel.Text = status.RecurringGiftDownloadError + NewLineString;
        if ( recurringGiftDownloadErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( recurringGiftDownloadErrorLabel );

        Label recurringIntervalCyclesErrorLabel = new Label();
        recurringIntervalCyclesErrorLabel.ForeColor = Color.Red;
        recurringIntervalCyclesErrorLabel.Text = status.RecurringIntervalCyclesError + NewLineString;
        if ( recurringIntervalCyclesErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( recurringIntervalCyclesErrorLabel );

        Label recurringIntervalUnitErrorLabel = new Label();
        recurringIntervalUnitErrorLabel.ForeColor = Color.Red;
        recurringIntervalUnitErrorLabel.Text = status.RecurringIntervalUnitError + NewLineString;
        if ( recurringIntervalUnitErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( recurringIntervalUnitErrorLabel );
    }

    private void AddTaxClassError( ProductImportStatus status )
    {
        Label taxClassLabel = new Label();
        taxClassLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        taxClassLabel.Text = status.TaxClassError + NewLineString;
        if ( taxClassLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( taxClassLabel );
    }

    private void AddProductKitGroupError( ProductImportStatus status )
    {
        Label productKitGroupErrorLabel = new Label();
        productKitGroupErrorLabel.ForeColor = Color.Red;
        productKitGroupErrorLabel.Text = status.ProductKitGroupError + NewLineString;
        if ( productKitGroupErrorLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( productKitGroupErrorLabel );
    }

    private void AddProductSkuError( ProductImportStatus status )
    {
        Label productSkuErrorLabel = new Label();
        productSkuErrorLabel.ForeColor = Color.Red;
        productSkuErrorLabel.Text = status.ProductSKUError + NewLineString;
        if ( productSkuErrorLabel.Text.Trim() != NewLineString )
            uxImportSpecificationMessagePanel.Controls.Add( productSkuErrorLabel );
    }

    private void AddSpecificationItemError( ProductImportStatus status )
    {
        Label specificationErrorLabel = new Label();
        specificationErrorLabel.ForeColor = Color.Red;
        specificationErrorLabel.Text = status.SpecificationItemError + NewLineString;
        if ( specificationErrorLabel.Text.Trim() != NewLineString )
            uxImportSpecificationMessagePanel.Controls.Add( specificationErrorLabel );
    }

    private void AddProductKitGroupItemError( ProductImportStatus status )
    {
        Label productKitGroupItemErrorLabel = new Label();
        productKitGroupItemErrorLabel.ForeColor = Color.Red;
        productKitGroupItemErrorLabel.Text = status.ProductKitGroupError + NewLineString;
        if ( productKitGroupItemErrorLabel.Text.Trim() != NewLineString )
            uxImportProductKitItemMessagePanel.Controls.Add( productKitGroupItemErrorLabel );
    }

    private void AddProductKitSkuError( ProductImportStatus status )
    {
        Label productSkuErrorLabel = new Label();
        productSkuErrorLabel.ForeColor = Color.Red;
        productSkuErrorLabel.Text = status.ProductSKUError + NewLineString;
        if ( productSkuErrorLabel.Text.Trim() != NewLineString )
            uxImportProductKitItemMessagePanel.Controls.Add( productSkuErrorLabel );
    }

    private void AddProductKitQuantityError( ProductImportStatus status )
    {
        Label productQTYErrorLabel = new Label();
        productQTYErrorLabel.ForeColor = Color.Red;
        productQTYErrorLabel.Text = status.ProductKitQuantityError + NewLineString;
        if (productQTYErrorLabel.Text.Trim() != NewLineString)
            uxImportProductKitItemMessagePanel.Controls.Add( productQTYErrorLabel );
    }

    private void AddProductKitOtherError( ProductImportStatus status )
    {
        Label otherErrorLabel = new Label();
        otherErrorLabel.ForeColor = Color.Red;
        otherErrorLabel.Text = status.OtherError + NewLineString;
        if ( otherErrorLabel.Text.Trim() != NewLineString )
            uxImportProductKitItemMessagePanel.Controls.Add( otherErrorLabel );
    }

    private void AddInventoryAndStockError( ProductImportStatus status )
    {
        Label stockLabel = new Label();
        stockLabel.ForeColor = Color.FromArgb( 153, 0, 255 );
        stockLabel.Text = status.InventoryAndStockError + NewLineString;
        if ( stockLabel.Text.Trim() != NewLineString )
            uxMessagePanel.Controls.Add( stockLabel );
    }

    private void AddImportErrorMessages( ProductImportStatus status )
    {
        AddErrorMessageHeader( status );
        AddSKUImportedError( status );
        AddProductNameError( status );
        AddRelatedProductError( status );
        AddCategoryError( status );
        AddDepartmentError( status );
        AddImageError( status );
        AddUseOptionError( status );
        AddOtherError( status );
        AddImageProcessError( status );
        AddRecurringProcessError( status );
        AddTaxClassError( status );
        AddProductKitGroupError( status );
        AddInventoryAndStockError( status );

        uxMessagePanel.CssClass = "mgl30";
    }

    private void AddSpecificationImportErrorMessages( ProductImportStatus status )
    {
        AddProductSkuError( status );
        AddSpecificationItemError( status );

        uxImportSpecificationMessagePanel.CssClass = "mgl30";
    }

    private void AddProductKitItemImportErrorMessages( ProductImportStatus status )
    {
        AddProductKitSkuError( status );
        AddProductKitGroupItemError( status );
        AddProductKitQuantityError( status );
        AddProductKitOtherError( status );

        uxImportProductKitItemMessagePanel.CssClass = "mgl30";
    }

    private void UpdateMessage( ProductImportStatus status )
    {
        switch ( status.Status )
        {
            case ProductImportStatus.ErrorStatus.Success:
                Label successlabel = new Label();
                successlabel.ForeColor = Color.Blue;
                successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.ErrorCount + " error(s)<br />";
                uxMessagePanel.Controls.Add( successlabel );
                break;

            case ProductImportStatus.ErrorStatus.DoneWithErrors:
                AddImportErrorMessages( status );
                break;

            case ProductImportStatus.ErrorStatus.Fatal:
                Label fatalLabel = new Label();
                fatalLabel.ForeColor = Color.Red;
                fatalLabel.Text = "<b>Import Failed!<br/> Error Message:</b><br/>" +
                    WebUtilities.ReplaceNewLine( status.ErrorMessage ) + "<br/>" +
                    status.ImportedCount + " row(s) imported<br/>";
                uxMessagePanel.Controls.Add( fatalLabel );

                AddImportErrorMessages( status );
                break;
        }
    }

    private void UpdateSpecificationImportMessage( ProductImportStatus status )
    {
        switch ( status.Status )
        {
            case ProductImportStatus.ErrorStatus.Success:
                Label successlabel = new Label();
                successlabel.ForeColor = Color.Blue;
                successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.ErrorCount + " error(s)<br />";
                uxImportSpecificationMessagePanel.Controls.Add( successlabel );
                break;

            case ProductImportStatus.ErrorStatus.DoneWithErrors:
                AddSpecificationImportErrorMessages( status );
                break;

            case ProductImportStatus.ErrorStatus.Fatal:
                Label fatalLabel = new Label();
                fatalLabel.ForeColor = Color.Red;
                fatalLabel.Text = "<b>Import Failed!<br/> Error Message:</b><br/>" +
                    WebUtilities.ReplaceNewLine( status.ErrorMessage ) + "<br/>" +
                    status.ImportedCount + " row(s) imported<br/>";
                uxImportSpecificationMessagePanel.Controls.Add( fatalLabel );

                AddSpecificationImportErrorMessages( status );
                break;
        }
    }

    private void UpdateProductKitItemImportMessage( ProductImportStatus status )
    {
        switch ( status.Status )
        {
            case ProductImportStatus.ErrorStatus.Success:
                Label successlabel = new Label();
                successlabel.ForeColor = Color.Blue;
                successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.ErrorCount + " error(s)<br />";
                uxImportProductKitItemMessagePanel.Controls.Add( successlabel );
                break;

            case ProductImportStatus.ErrorStatus.DoneWithErrors:
                AddProductKitItemImportErrorMessages( status );
                break;

            case ProductImportStatus.ErrorStatus.Fatal:
                Label fatalLabel = new Label();
                fatalLabel.ForeColor = Color.Red;
                fatalLabel.Text = "<b>Import Failed!<br/> Error Message:</b><br/>" +
                    WebUtilities.ReplaceNewLine( status.ErrorMessage ) + "<br/>" +
                    status.ImportedCount + " row(s) imported<br/>";
                uxImportProductKitItemMessagePanel.Controls.Add( fatalLabel );

                AddProductKitItemImportErrorMessages( status );
                break;
        }
    }

    private void UpdateTimeMessage( TimeSpan timeDiff, Panel uxMessagePanel )
    {
        Label timelabel = new Label();
        timelabel.ForeColor = Color.Brown;
        timelabel.Font.Bold = true;
        timelabel.Text = String.Format( "<br/>Used time : {0}:{1}:{2}:{3}", timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds, timeDiff.Milliseconds );
        uxMessagePanel.Controls.Add( timelabel );
    }

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        uxStoreDrop.Items.Add( new ListItem( "Default Value", Store.Null.StoreID ) );
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for ( int index = 0; index < storeList.Count; index++ )
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[ index ].StoreName, storeList[ index ].StoreID ) );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        ScriptManager uxScriptManager = ( ScriptManager ) Page.FindControl( "uxScriptManager" );
        uxScriptManager.AsyncPostBackTimeout = TimeOut;

        if ( !MainContext.IsPostBack )
        {
            uxFileUpload.ReturnTextControlClientID = uxFileNameText.ClientID;

            InsertStoreInDropDownList();
            PopulateControls();

            uxSpecificationCsvFileUpload.ReturnTextControlClientID = uxSpecificationCsvFileNameText.ClientID;
            uxProductKitItemCsvFileUpload.ReturnTextControlClientID = uxProductKitItemCsvFileNameText.ClientID;
        }

        if ( !KeyUtilities.IsMultistoreLicense() )
            uxStorePanel.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxFileUpload.ShowControl = true;
    }

    protected void uxSpecificationCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxSpecificationCsvFileUpload.ShowControl = true;
    }

    protected void uxImageProcessCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        uxSkipImageProcessCheck.Enabled = uxImageProcessCheck.Checked;
    }

    protected void uxImportButton_Click( object sender, EventArgs e )
    {
        uxMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxFileNameText.Text.Trim() );
        if ( !File.Exists( localFilePath ) )
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxMessagePanel.Controls.Add( label );
            return;
        }

        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue );
        ProductImporter importer = new ProductImporter( culture, new FileManager() );
        ProductImportStatus status;
        TimeSpan timeDiff;
        string storeID = CurrentStoreID;
        switch ( uxModeRadioList.SelectedValue )
        {
            case "Purge":
                DataAccessContextDeluxe.PromotionProductRepository.DeleteAll();
                DataAccessContextDeluxe.ProductSubscriptionRepository.DeleteAll();
                status = importer.ImportProductPurgeAll(
                    "~/" + uxFileNameText.Text.Trim(), uxImageProcessCheck.Checked, uxSkipImageProcessCheck.Checked, out timeDiff, storeID );
                UpdateMessage( status );
                UpdateTimeMessage( timeDiff, uxMessagePanel );
                break;

            case "Overwrite":
                status = importer.ImportProductOverwrite(
                    "~/" + uxFileNameText.Text.Trim(), uxImageProcessCheck.Checked, uxSkipImageProcessCheck.Checked, out timeDiff, storeID );
                UpdateMessage( status );
                UpdateTimeMessage( timeDiff, uxMessagePanel );
                break;
        }

        AdminUtilities.ClearAllCache();
    }

    protected void uxSpecificationImportButton_Click( object sender, EventArgs e )
    {
        uxImportSpecificationMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxSpecificationCsvFileNameText.Text.Trim() );
        if ( !File.Exists( localFilePath ) )
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportSpecificationMessagePanel.Controls.Add( label );
            return;
        }

        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue );
        ProductSpecificationImporter importer = new ProductSpecificationImporter( culture, new FileManager() );
        ProductImportStatus status;
        TimeSpan timeDiff;
        string storeID = CurrentStoreID;
        switch ( uxSpecificationImportModeRadioList.SelectedValue )
        {
            case "Purge":
                status = importer.ImportSpecificationPurgeAll(
                    "~/" + uxSpecificationCsvFileNameText.Text.Trim(), out timeDiff, storeID );
                UpdateSpecificationImportMessage( status );
                UpdateTimeMessage( timeDiff, uxImportSpecificationMessagePanel );
                break;

            case "Overwrite":
                status = importer.ImportSpecificationOverwrite(
                    "~/" + uxSpecificationCsvFileNameText.Text.Trim(), out timeDiff, storeID );
                UpdateSpecificationImportMessage( status );
                UpdateTimeMessage( timeDiff, uxImportSpecificationMessagePanel );
                break;
        }

        AdminUtilities.ClearAllCache();
    }

    protected void uxProductKitItemCsvFileNameUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxProductKitItemCsvFileUpload.ShowControl = true;
    }

    protected void uxProductKitItemImportButton_Click( object sender, EventArgs e )
    {
        uxImportProductKitItemMessagePanel.Controls.Clear();
        string localFilePath = Server.MapPath( "~/" + uxProductKitItemCsvFileNameText.Text.Trim() );
        if ( !File.Exists( localFilePath ) )
        {
            Label label = new Label();
            label.ForeColor = Color.Red;
            label.Text = "File not found.";
            uxImportProductKitItemMessagePanel.Controls.Add( label );
            return;
        }

        Culture culture = DataAccessContext.CultureRepository.GetOne( uxLanguageDrop.SelectedValue );
        ProductKitGroupItemImporter importer = new ProductKitGroupItemImporter( culture, new FileManager() );
        ProductImportStatus status;
        TimeSpan timeDiff;
        string storeID = CurrentStoreID;
        switch ( uxProductKitItemImportModeRadioList.SelectedValue )
        {
            case "Purge":
                status = importer.ImportProductKitItemPurgeAll(
                    "~/" + uxProductKitItemCsvFileNameText.Text.Trim(), out timeDiff, storeID );
                UpdateProductKitItemImportMessage( status );
                UpdateTimeMessage( timeDiff, uxImportProductKitItemMessagePanel );
                break;

            case "Overwrite":
                status = importer.ImportProductKitItemOverwrite(
                    "~/" + uxProductKitItemCsvFileNameText.Text.Trim(), out timeDiff, storeID );
                UpdateProductKitItemImportMessage( status );
                UpdateTimeMessage( timeDiff, uxImportProductKitItemMessagePanel );
                break;
        }

        AdminUtilities.ClearAllCache();
    }
}
