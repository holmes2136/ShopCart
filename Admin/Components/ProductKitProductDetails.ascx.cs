using System;
using System.Drawing;
using System.IO;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Admin_Components_ProductKitProductDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadProduct = "Images/Products/";
    private const int MaxSmallProductImageWidth = 170;

    private void PopulateProductImage( string imageSecondary )
    {
        string mapUrl;
        mapUrl = "../" + imageSecondary;

        if (File.Exists( Server.MapPath( mapUrl ) ))
        {
            if (mapUrl != "")
            {
                Bitmap mypic;
                mypic = new Bitmap( Server.MapPath( mapUrl ) );

                if (mypic.Width < MaxSmallProductImageWidth)
                    uxProductImage.Width = mypic.Width;
                else
                    uxProductImage.Width = MaxSmallProductImageWidth;
                mypic.Dispose();
            }
            uxProductImage.Visible = true;
        }
        else
        {
            uxProductImage.Visible = false;
        }
    }

    private decimal GetProductPrice( Product product )
    {
        if (!product.IsCustomPrice)
            return product.GetDisplayedPrice( StoreContext.WholesaleStatus, StoreID );
        else
            return product.ProductCustomPrice.DefaultPrice;

    }
    public void PopulateControls()
    {
        uxCustomPriceNote.Visible = false;
        uxSpecialTrialPanel.Visible = false;

        uxProductNameLabel.Text = CurrentProduct.Name;
        uxProductImage.ImageUrl = AdminConfig.UrlFront + CurrentProduct.ImageSecondary;
        PopulateProductImage( CurrentProduct.ImageSecondary );

        Currency currency = DataAccessContext.CurrencyRepository.GetOne( CurrencyCode );


        if (CurrentProduct.IsRecurring)
        {
            uxSpecialTrialPanel.Visible = IsShowRecurringPeriod();
            uxRecurringPeriodTR.Visible = IsShowRecurringPeriod();
            uxRecurringCyclesLabel.Text = ShowRecurringCycles();
            uxTrialPeriodMoreTR.Visible = IsShowTrialPeriodMore();
            uxTrialPeriodTR.Visible = IsShowTrialPeriod();
            uxFreeTrialPeriodTR.Visible = IsShowFreeTrialPeriod();
            uxFreeTrialPeriodMoreTR.Visible = IsShowFreeTrialPeriodMore();
            uxRecurringTrialMoreAmountLabel.Text = currency.FormatPrice( CurrentProduct.ProductRecurring.RecurringTrialAmount );
            uxRecurringTrialAmountLabel.Text = currency.FormatPrice( CurrentProduct.ProductRecurring.RecurringTrialAmount );
            uxRecurringMoreNumberOfTrialCyclesLabel.Text = CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles.ToString();
            uxRecurringNumberOfTrialCyclesLabel.Text = CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles.ToString();
        }

        if (!CurrentProduct.IsCustomPrice)
        {
            uxEnterAmountLabel.Text = currency.FormatPriceWithOutSymbol( GetProductPrice( CurrentProduct ) );
        }
        else
        {
            uxEnterAmountLabel.Text = currency.FormatPriceWithOutSymbol( CurrentProduct.ProductCustomPrice.DefaultPrice );
            uxMinPriceLabel.Text = String.Format( "Minimum Price: {0}", currency.FormatPrice( CurrentProduct.ProductCustomPrice.MinimumPrice ) );
            uxCustomPriceNote.Visible = true;
        }

        uxQuantityText.Text = "1";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    private bool CheckUseInventory( Product product )
    {
        return product.UseInventory;
    }

    public void DisplayError( string message )
    {
        uxMessage.DisplayError( message );
    }

    public void ClearCheckBox()
    {
        uxIsDefaultCheck.Checked = false;
        uxIsUserDefinedQuantityCheck.Checked = false;
    }

    public bool IsShowRecurringPeriod()
    {
        return (ConvertUtilities.ToBoolean( CurrentProduct.IsRecurring )
               && !CatalogUtilities.IsOutOfStock( CurrentProduct.SumStock, CurrentProduct.UseInventory ));
    }

    public bool IsShowTrialPeriodMore()
    {
        return IsShowRecurringPeriod() &&
            (ConvertUtilities.ToInt32( CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles ) > 1) &&
            (ConvertUtilities.ToDecimal( CurrentProduct.ProductRecurring.RecurringTrialAmount ) > 0m);
    }

    public bool IsShowTrialPeriod()
    {
        return IsShowRecurringPeriod() &&
            (ConvertUtilities.ToInt32( CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles ) == 1) &&
            (ConvertUtilities.ToDecimal( CurrentProduct.ProductRecurring.RecurringTrialAmount ) > 0m);
    }

    public bool IsShowFreeTrialPeriod()
    {
        return IsShowRecurringPeriod() &&
            (ConvertUtilities.ToInt32( CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles ) > 1) &&
            (ConvertUtilities.ToDecimal( CurrentProduct.ProductRecurring.RecurringTrialAmount ) == 0m);
    }

    public bool IsShowFreeTrialPeriodMore()
    {
        return IsShowRecurringPeriod() &&
            (ConvertUtilities.ToInt32( CurrentProduct.ProductRecurring.RecurringNumberOfTrialCycles ) == 1) &&
            (ConvertUtilities.ToDecimal( CurrentProduct.ProductRecurring.RecurringTrialAmount ) == 0m);
    }

    public string ShowRecurringCycles()
    {
        if (CurrentProduct.IsRecurring)
            return String.Format(
                "{0} {1}s",
                ConvertUtilities.ToInt32( CurrentProduct.ProductRecurring.RecurringInterval ),
                CurrentProduct.ProductRecurring.RecurringIntervalUnit.ToString().ToLower() );
        else
            return String.Empty;
    }

    public bool HasOptions()
    {
        if (CurrentProduct.ProductOptionGroups.Count > 0)
            return true;
        else
            return false;
    }

    public Product CurrentProduct
    {
        get
        {
            return DataAccessContext.ProductRepository.GetOne(
                DataAccessContext.CultureRepository.GetOne( CultureID ), ProductID, StoreID );
        }
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                return "0";
            else
                return (string) ViewState["ProductID"];
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public string CartItemID
    {
        get
        {
            if (ViewState["CartItemID"] == null)
                return "0";
            else
                return (string) ViewState["CartItemID"];
        }
        set
        {
            ViewState["CartItemID"] = value;
        }
    }

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return CultureUtilities.StoreCultureID;
            else
                return (string) ViewState["CultureID"];
        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    public string StoreID
    {
        get
        {
            if (ViewState["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return (string) ViewState["StoreID"];
        }
        set
        {
            ViewState["StoreID"] = value;
        }
    }

    public string CurrencyCode
    {
        get
        {
            if (ViewState["CurrencyCode"] == null)
                return DataAccessContext.CurrencyRepository.GetOne(
                    DataAccessContext.Configurations.GetValueNoThrow( "DefaultDisplayCurrencyCode",
                    DataAccessContext.StoreRepository.GetOne( StoreID ) ) ).CurrencyCode;
            else
                return (string) ViewState["CurrencyCode"];
        }
        set
        {
            ViewState["CurrencyCode"] = value;
        }
    }

    public bool IsRecurringProduct()
    {
        if (CurrentProduct.IsRecurring)
        {
            return true;
        }
        else
            return false;
    }

    public bool IsDownloadbleProduct()
    {
        if (CurrentProduct.IsDownloadable)
        {
            return true;
        }
        else
            return false;
    }

    public int Quantity { get { return ConvertUtilities.ToInt32( uxQuantityText.Text ); } }
    public bool IsDefault { get { return uxIsDefaultCheck.Checked; } }
    public bool IsUserDefinedQuantity { get { return uxIsUserDefinedQuantityCheck.Checked; } }
}
