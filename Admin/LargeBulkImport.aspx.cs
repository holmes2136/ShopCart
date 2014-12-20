using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Widget;
using System.Collections.Generic;
using Vevo.Domain.Stores;
using System.Drawing;
using Vevo.Domain.ImportExport;
using Vevo.Shared.SystemServices;
using Vevo.Deluxe.Domain;

public partial class AdminAdvanced_LargeBulkImport : System.Web.UI.Page
{
    private const string NewLineString = "<br />";

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

    private void DisplatError(string message)
    {
        uxMessageLabel.ForeColor = System.Drawing.Color.Red;
        uxMessageLabel.Text = "<strong>Error : </storng>" + message;
    }

    private void PopulateControls()
    {
        uxImageProcessCheck.Checked = true;
        uxLanguageDrop.SelectedValue = CultureUtilities.StoreCultureID;
    }

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        uxStoreDrop.Items.Add(new ListItem("Default Value", Store.Null.StoreID));
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll("StoreID");

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add(new ListItem(storeList[index].StoreName, storeList[index].StoreID));
        }
    }

    private void UpdateTimeMessage(TimeSpan timeDiff)
    {
        Label timelabel = new Label();
        timelabel.ForeColor = Color.Brown;
        timelabel.Font.Bold = true;
        timelabel.Text = String.Format("<br/>Used time : {0}:{1}:{2}:{3}", timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds, timeDiff.Milliseconds);
        uxTimeLabel.Text += timelabel.Text;
    }

    private void UpdateMessage(ProductImportStatus status)
    {
        switch (status.Status)
        {
            case ProductImportStatus.ErrorStatus.Success:
                Label successlabel = new Label();
                successlabel.ForeColor = Color.Blue;
                successlabel.Text = "<b>Import finished successfully!</b><br />" +
                    status.ImportedCount + " row(s) imported<br />" +
                    status.ErrorCount + " error(s)<br />";
                uxMessageLabel.Text += successlabel.Text;
                break;

            case ProductImportStatus.ErrorStatus.DoneWithErrors:
                AddImportErrorMessages(status);
                break;

            case ProductImportStatus.ErrorStatus.Fatal:
                Label fatalLabel = new Label();
                fatalLabel.ForeColor = Color.Red;
                fatalLabel.Text = "<b>Import Failed!<br/> Error Message:</b><br/>" +
                    WebUtilities.ReplaceNewLine(status.ErrorMessage) + "<br/>" +
                    status.ImportedCount + " row(s) imported<br/>";
                uxMessageLabel.Text += fatalLabel.Text;
                AddImportErrorMessages(status);
                break;
        }
    }

    private void AddImportErrorMessages(ProductImportStatus status)
    {
        AddErrorMessageHeader(status);
        AddRelatedProductError(status);
        AddCategoryError(status);
        AddImageError(status);
        AddUseOptionError(status);
        AddOtherError(status);
        AddImageProcessError(status);
        AddRecurringProcessError(status);
        AddTaxClassError(status);
        AddDepartmentError(status);
        //   uxMessagePanel.CssClass = "mgl30";
    }


    private void AddErrorMessageHeader(ProductImportStatus status)
    {
        Label label = new Label();
        label.ForeColor = Color.Red;
        label.Text = "<b>Import finished with errors.</b><br />" +
            status.ImportedCount + " row(s) imported<br />" +
            status.ErrorCount + " error(s)" + NewLineString;
        uxMessageLabel.Text += label.Text;
    }

    private void AddRelatedProductError(ProductImportStatus status)
    {
        Label relatedLabel = new Label();
        relatedLabel.ForeColor = Color.FromArgb(153, 0, 255);
        relatedLabel.Text = status.RelatedProductError + NewLineString;
        if (relatedLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += relatedLabel.Text;
    }

    private void AddCategoryError(ProductImportStatus status)
    {
        Label categoryLabel = new Label();
        categoryLabel.ForeColor = Color.FromArgb(153, 0, 255);
        categoryLabel.Text = status.CategoryError + NewLineString;
        if (categoryLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += categoryLabel.Text;
    }

    private void AddDepartmentError(ProductImportStatus status)
    {
        Label departmentLabel = new Label();
        departmentLabel.ForeColor = Color.FromArgb(153, 0, 255);
        departmentLabel.Text = status.DepartmentError + NewLineString;
        if (departmentLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += departmentLabel.Text;
    }
    private void AddImageError(ProductImportStatus status)
    {
        Label imageUploadLabel = new Label();
        imageUploadLabel.ForeColor = Color.Red;
        imageUploadLabel.Text = status.ImageUploadError + NewLineString;
        if (imageUploadLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += imageUploadLabel.Text;
    }

    private void AddOtherError(ProductImportStatus status)
    {
        Label otherErrorLabel = new Label();
        otherErrorLabel.ForeColor = Color.Red;
        otherErrorLabel.Text = status.OtherError + NewLineString;
        if (otherErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += otherErrorLabel.Text;
    }

    private void AddUseOptionError(ProductImportStatus status)
    {
        Label useOptionErrorLabel = new Label();
        useOptionErrorLabel.ForeColor = Color.Red;
        useOptionErrorLabel.Text = status.UseOptionError + NewLineString;
        if (useOptionErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += useOptionErrorLabel.Text;
    }

    private void AddImageProcessError(ProductImportStatus status)
    {
        Label useImageProcessErrorLabel = new Label();
        useImageProcessErrorLabel.ForeColor = Color.Red;
        useImageProcessErrorLabel.Text = status.ImageProcessError + NewLineString;
        if (useImageProcessErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += useImageProcessErrorLabel.Text;
    }

    private void AddRecurringProcessError(ProductImportStatus status)
    {
        Label recurringGiftDownloadErrorLabel = new Label();
        recurringGiftDownloadErrorLabel.ForeColor = Color.Red;
        recurringGiftDownloadErrorLabel.Text = status.RecurringGiftDownloadError + NewLineString;
        if (recurringGiftDownloadErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += recurringGiftDownloadErrorLabel.Text;

        Label recurringIntervalCyclesErrorLabel = new Label();
        recurringIntervalCyclesErrorLabel.ForeColor = Color.Red;
        recurringIntervalCyclesErrorLabel.Text = status.RecurringIntervalCyclesError + NewLineString;
        if (recurringIntervalCyclesErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += recurringIntervalCyclesErrorLabel.Text;

        Label recurringIntervalUnitErrorLabel = new Label();
        recurringIntervalUnitErrorLabel.ForeColor = Color.Red;
        recurringIntervalUnitErrorLabel.Text = status.RecurringIntervalUnitError + NewLineString;
        if (recurringIntervalUnitErrorLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += recurringIntervalUnitErrorLabel.Text;
    }

    private void AddTaxClassError(ProductImportStatus status)
    {
        Label taxClassLabel = new Label();
        taxClassLabel.ForeColor = Color.FromArgb(153, 0, 255);
        taxClassLabel.Text = status.TaxClassError + NewLineString;
        if (taxClassLabel.Text.Trim() != NewLineString)
            uxMessageLabel.Text += taxClassLabel.Text;
    }


    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = String.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        uxMessageLabel.Text = String.Empty;

        if (!Page.IsPostBack)
        {
            InsertStoreInDropDownList();
            PopulateControls();
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStorePanel.Visible = false;
    }

    private Dictionary<string, string> GetProductIDAndSku()
    {
        Dictionary<string, string> skuProductID = new Dictionary<string, string>();

        DataTable table = DataAccess.ExecuteSelect("Select ProductID, Sku from Product");
        foreach (DataRow row in table.Rows)
        {
            if (!skuProductID.ContainsKey(row["Sku"].ToString()))
            {
                skuProductID.Add(row["Sku"].ToString(), row["ProductID"].ToString());
            }
        }
        return skuProductID;
    }

    protected void uxExecuteButton_Click(object sender, EventArgs e)
    {
        string pathFile = uxFileNameText.Text;

        Culture culture = DataAccessContext.CultureRepository.GetOne(uxLanguageDrop.SelectedValue);
        // ProductImporter importer = new ProductImporter(culture, new FileManager());
        ProductImportStatus status;
        TimeSpan timeDiff;
        string storeID = CurrentStoreID;
        LargeImporter importer = new LargeImporter(culture, new FileManager());

        switch (uxModeRadioList.SelectedValue)
        {
            case "Purge":
                DataAccessContextDeluxe.PromotionProductRepository.DeleteAll();
                DataAccessContextDeluxe.ProductSubscriptionRepository.DeleteAll();
                status = importer.ImportProductPurgeAll(
                    "~/" + uxFileNameText.Text.Trim(), 
                    uxImageProcessCheck.Checked, 
                    uxSkipImageProcessingCheck.Checked, 
                    out timeDiff, 
                    storeID);

                UpdateMessage(status);
                UpdateTimeMessage(timeDiff);

                break;

            case "Overwrite":
                status = importer.ImportProductOverwrite(
                    "~/" + uxFileNameText.Text.Trim(), 
                    uxImageProcessCheck.Checked, 
                    uxSkipImageProcessingCheck.Checked, 
                    out timeDiff, storeID, 
                    GetProductIDAndSku());

                UpdateMessage(status);
                UpdateTimeMessage(timeDiff);

                break;
        }

        AdminUtilities.ClearAllCache();
    }

    protected void uxImageProcessCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        uxSkipImageProcessingCheck.Enabled = uxImageProcessCheck.Checked;
    }

    protected void uxDelButton_Click(object sender, EventArgs e)
    {

        DataAccessContext.ProductRepository.DeleteAll();
        AdminUtilities.ClearAllCache();
        uxMessageLabel.Text = "Delete all completed";
    }



}
