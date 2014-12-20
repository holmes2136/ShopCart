using System;
using System.Drawing;
using System.IO;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Admin_Components_Order_ProductItemDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadProduct = "Images/Products/";
    private const int MaxSmallProductImageWidth = 170;
    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    public OptionItemValueCollection GetSelectedOptions()
    {
        return uxProductOptionGroupDetails.GetSelectedOptions();
    }

    public int GetQuantity()
    {
        return ConvertUtilities.ToInt32( uxQuantityText.Text );
    }

    public bool IsProductKit( string productID )
    {
        return DataAccessContext.ProductRepository.CheckIsProductKitByProductID( productID );
    }


    public bool VerifyValidInput( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (CurrentProduct.IsCustomPrice)
        {
            if (ConvertUtilities.ToDecimal( uxEnterAmountText.Text ) < CurrentProduct.ProductCustomPrice.MinimumPrice)
            {
                errorMessage = "Error: Price is invalid.";
                return false;
            }
        }

        if (uxProductOptionGroupDetails.IsValidInput)
        {
            int result = 0;
            if (!int.TryParse( uxQuantityText.Text, out result ) ||
                result < 0 || (uxQuantityText.Text == "" && uxQuantityText.Visible))
            {
                errorMessage += "Error: Quantity is invalid.";
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    protected bool IsMatchQuantity()
    {
        bool isOK = false;
        int quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        int minQuantity = CurrentProduct.MinQuantity;
        int maxQuantity = CurrentProduct.MaxQuantity;
        uxMinQuantityLabel.Text = "Minimum quantity is " + minQuantity;
        uxMaxQuantityLabel.Text = "Maximum quantity is " + maxQuantity;

        if (minQuantity > quantity)
        {
            uxMinQuantityLabel.Visible = true;
            uxMaxQuantityLabel.Visible = false;
        }
        else if (maxQuantity != 0 && maxQuantity < quantity)
        {
            uxMinQuantityLabel.Visible = false;
            uxMaxQuantityLabel.Visible = true;
        }
        else
        {
            uxMinQuantityLabel.Visible = false;
            uxMaxQuantityLabel.Visible = false;
            isOK = true;
        }

        return isOK;
    }

    private CartItemGiftDetails CreateGiftDetails()
    {
        return uxGiftCertificateDetails.GetCartItemGiftDetails();
    }

    public bool UpdateCartItem( bool isSubscriptionable, out string errMsg )
    {
        errMsg = String.Empty;
        if (!VerifyValidInput( out errMsg ))
        {
            if (!String.IsNullOrEmpty( errMsg ))
            {
                DisplayErrorMessage( errMsg );
                return false;
            }
        }

        //string errMsg;
        if (!VerifyQuantity( out errMsg ))
        {
            DisplayErrorMessage( errMsg );
            return false;
        }
        if (!IsMatchQuantity())
        {
            return false;
        }

        ICartItem item = StoreContext.ShoppingCart.FindCartItemByID( CartItemID );
        OptionItemValueCollection selectedOptions = uxProductOptionGroupDetails.GetSelectedOptions();
        CartItemGiftDetails giftDetails = CreateGiftDetails();

        Currency currency = DataAccessContext.CurrencyRepository.GetOne( CurrencyCode );
        decimal enterPrice = ConvertUtilities.ToDecimal( currency.FormatPriceWithOutSymbolInvert( ConvertUtilities.ToDecimal( uxEnterAmountText.Text ) ) );
        if (enterPrice == item.Product.GetProductPrice( StoreID ).Price)
            enterPrice = 0;

        decimal customPrice = 0;
        if (item.Product.IsCustomPrice)
            customPrice = ConvertUtilities.ToDecimal( currency.FormatPriceWithOutSymbolInvert( ConvertUtilities.ToDecimal( uxEnterAmountText.Text ) ) );

        int currentStock;
        string errorOptionName;

        StoreContext.ShoppingCart.DeleteItem( CartItemID );

        CartAddItemService addToCartService = new CartAddItemService(
            StoreContext.Culture, StoreContext.ShoppingCart );
        bool stockOK = addToCartService.AddToCartByAdmin(
            item.Product,
            selectedOptions,
            ConvertUtilities.ToInt32( uxQuantityText.Text ),
            giftDetails,
            customPrice,
            enterPrice,
            StoreID,
            out errorOptionName,
            out currentStock,
            isSubscriptionable );

        if (stockOK)
        {
            return true;
        }
        else
        {
            errMsg = DisplayOutOfStockError( currentStock, errorOptionName );

            return false;
        }
    }

    public bool AddItemToShoppingCart( bool isSubscriptionable, out string errMsg )
    {
        errMsg = String.Empty;

        if (!VerifyValidInput( out errMsg ))
        {
            DisplayErrorMessage( errMsg );
            return false;
        }

        if (!IsMatchQuantity())
        {
            return false;
        }

        OptionItemValueCollection selectedOptions = uxProductOptionGroupDetails.GetSelectedOptions();
        CartItemGiftDetails giftDetails = CreateGiftDetails();

        Currency currency = DataAccessContext.CurrencyRepository.GetOne( CurrencyCode );
        decimal enterPrice = ConvertUtilities.ToDecimal( currency.FormatPriceWithOutSymbolInvert( ConvertUtilities.ToDecimal( uxEnterAmountText.Text ) ) );
        if (enterPrice == CurrentProduct.GetProductPrice( StoreID ).Price)
            enterPrice = 0;

        decimal customPrice = 0;
        if (CurrentProduct.IsCustomPrice)
            customPrice = ConvertUtilities.ToDecimal( currency.FormatPriceWithOutSymbolInvert( ConvertUtilities.ToDecimal( uxEnterAmountText.Text ) ) );


        CartAddItemService addToCartService = new CartAddItemService( 
            StoreContext.Culture, StoreContext.ShoppingCart );

        int currentStock;
        string errorOptionName;
        bool stockOK = addToCartService.AddToCartByAdmin(
            CurrentProduct,
            selectedOptions,
            ConvertUtilities.ToInt32( uxQuantityText.Text ),
            giftDetails,
            customPrice,
            enterPrice,
            StoreID,
            out errorOptionName,
            out currentStock,
            isSubscriptionable );


        if (stockOK)
        {
            return true;
        }
        else
        {
            errMsg = DisplayOutOfStockError( currentStock, errorOptionName );
            uxMessage.DisplayError( errMsg );
            return false;
        }
    }

    private string DisplayOutOfStockError( int currentStock, string name )
    {
        int amount = currentStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );

        if (amount < 0)
            amount = 0;

        string result = name + " " + amount;
        if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
        {
            return String.Format(
                "Sorry, we only have {0} in stock", result.Trim() );
        }
        else
        {
            return String.Format( "Sorry, we do not have enough items." );
        }
    }

    public void DisplayErrorMessage( string message )
    {
        uxMessage.DisplayError( message );
    }

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
        uxEnterAmountTextRequired.ValidationGroup = SetValidGroup;
        uxEnterAmountTextCompare.ValidationGroup = SetValidGroup;
        uxQuantityRequiredValidator.ValidationGroup = SetValidGroup;
        uxQuantityCompare.ValidationGroup = SetValidGroup;

        if (IsProductKit( ProductID ))
        {
            uxMessageLabel.Visible = true;
            uxMessageLabel.Text = "Product Kit cannot be added.";
            uxMessageLabel.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            uxMessageLabel.Visible = false;
        }

        uxMinQuantityLabel.Visible = false;
        uxMaxQuantityLabel.Visible = false;
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

        if (!IsEditMode())
        {
            if (!CurrentProduct.IsCustomPrice)
            {
                uxEnterAmountText.Text = currency.FormatPriceWithOutSymbol( GetProductPrice( CurrentProduct ) );
                uxEnterAmountTextCompare.ValueToCompare = "0";
            }
            else
            {
                uxEnterAmountText.Text = currency.FormatPriceWithOutSymbol( CurrentProduct.ProductCustomPrice.DefaultPrice );
                uxEnterAmountTextCompare.ValueToCompare = Convert.ToDecimal( CurrentProduct.ProductCustomPrice.MinimumPrice ).ToString();
                uxMinPriceLabel.Text = String.Format( "Minimum Price: {0}", currency.FormatPrice( CurrentProduct.ProductCustomPrice.MinimumPrice ) );
                uxCustomPriceNote.Visible = true;
            }


            uxQuantityText.Text = CurrentProduct.MinQuantity.ToString();

            uxQuantityDiscount.DiscountGroupID = CurrentProduct.DiscountGroupID;
            uxQuantityDiscount.PopulateControls();
            if (CurrentProduct.DiscountGroupID == "0")
                uxQuantityDiscountPanel.Visible = false;
            else
                uxQuantityDiscountPanel.Visible = true;

            uxProductOptionGroupDetails.ProductID = ProductID;
            uxProductOptionGroupDetails.CultureID = CultureID;
            uxProductOptionGroupDetails.StoreID = StoreID;
            uxProductOptionGroupDetails.CurrencyCode = CurrencyCode;
            uxProductOptionGroupDetails.ValidGroup = SetValidGroup;
            uxProductOptionGroupDetails.PopulateControls();
            if (CurrentProduct.ProductOptionGroups.Count <= 0)
                uxProductOptionGroupPanel.Visible = false;
            else
                uxProductOptionGroupPanel.Visible = true;

            uxGiftCertificateDetails.ProductID = ProductID;
            uxGiftCertificateDetails.CultureID = CultureID;
            uxGiftCertificateDetails.StoreID = StoreID;
            uxGiftCertificateDetails.PopulateControls();

            if (!CurrentProduct.IsGiftCertificate)
            {
                uxGiftPanel.Visible = false;
                uxCustomPriceDiv.Visible = true;
            }
            else
            {
                uxCustomPriceDiv.Visible = false;
                uxGiftPanel.Visible = true;
            }


        }
        else
        {
            ICartItem cartItem = StoreContext.ShoppingCart.FindCartItemByID( CartItemID );
            uxQuantityText.Text = cartItem.Quantity.ToString();


            if (cartItem.Product.IsCustomPrice)
            {
                uxEnterAmountText.Text = currency.FormatPriceWithOutSymbol( cartItem.ProductCustomPrice );
                uxEnterAmountTextCompare.ValueToCompare = Convert.ToDecimal( cartItem.Product.ProductCustomPrice.MinimumPrice ).ToString();
                uxMinPriceLabel.Text = String.Format( "Minimum Price: {0}", currency.FormatPrice( cartItem.Product.ProductCustomPrice.MinimumPrice ) );

            }
            else if (cartItem.IsEnterPrice)
            {
                uxEnterAmountText.Text = currency.FormatPriceWithOutSymbol( cartItem.EnteredPrice );
                uxEnterAmountTextCompare.ValueToCompare = "0";
            }
            else
            {
                uxEnterAmountText.Text = currency.FormatPriceWithOutSymbol( GetProductPrice( cartItem.Product ) );
                uxEnterAmountTextCompare.ValueToCompare = "0";
            }

            uxProductOptionGroupDetails.ProductID = cartItem.Product.ProductID;
            uxProductOptionGroupDetails.CultureID = CultureID;
            uxProductOptionGroupDetails.StoreID = StoreID;
            uxProductOptionGroupDetails.CurrencyCode = CurrencyCode;
            uxProductOptionGroupDetails.ValidGroup = SetValidGroup;
            uxProductOptionGroupDetails.PopulateControls( cartItem.Options );

            if (CurrentProduct.ProductOptionGroups.Count <= 0)
                uxProductOptionGroupPanel.Visible = false;
            else
            {
                uxProductOptionGroupPanel.Visible = true;
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    private bool VerifyQuantity( out string errMsg )
    {
        string stockMessage = String.Empty;
        string minMaxQuantityMessage = String.Empty;
        errMsg = String.Empty;

        if (!IsEnoughStock( out stockMessage ))
        {
            return false;
        }

        return true;
    }

    private bool CheckUseInventory( Product product )
    {
        return product.UseInventory;
    }

    private bool IsEnoughStock( out string errMsg )
    {
        errMsg = String.Empty;
        ICartItem cartItem = StoreContext.ShoppingCart.FindCartItemByID( CartItemID );
        int quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        OptionItemValueCollection options = uxProductOptionGroupDetails.GetSelectedOptions();
        int productStock = cartItem.Product.GetStock( options.GetUseStockOptionItemIDs() );
        int currentStock = productStock - quantity;

        if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
        {
            if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( cartItem.Product ) ))
            {
                errMsg += cartItem.Product.Name;
                if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
                {
                    int displayStock = productStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );
                    if (displayStock < 0)
                    {
                        displayStock = 0;
                    }
                    errMsg += " ( available " + displayStock + " items )";
                }
            }
        }
        if (!String.IsNullOrEmpty( errMsg ))
        {
            errMsg = "Stock Error : " + errMsg;
            return false;
        }
        else
        {
            return true;
        }

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

    public Product CurrentProduct
    {
        get
        {
            if (!IsEditMode())
            {
                return DataAccessContext.ProductRepository.GetOne(
                    DataAccessContext.CultureRepository.GetOne( CultureID ), ProductID, StoreID );
            }
            else
            {
                ICartItem cartItem = StoreContext.ShoppingCart.FindCartItemByID( CartItemID );
                return cartItem.Product;
            }
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

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    public string SetValidGroup
    {
        get
        {
            if (ViewState["ValidGroup"] == null)
                return "0";
            else
                return (string) ViewState["ValidGroup"];
        }
        set
        {
            ViewState["ValidGroup"] = value;
        }
    }
}
