using System;
using System.Collections.Generic;
using System.Web.UI;
using Vevo;
using Vevo.Deluxe.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;
using Vevo.WebUI.Products;

public partial class Mobile_Components_ProductDetails : BaseProductDetails
{
    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxQuantityText, uxAddToCartButton );
    }

    private void DisplayOutOfStockError( int currentStock, string name )
    {
        int amount = currentStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );

        if (amount < 0)
            amount = 0;

        string result = name + " " + amount;
        uxStockLabel.ForeColor = System.Drawing.Color.Red;
        if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
        {
            uxStockLabel.Text = String.Format(
                "[$CheckStockMesssage1] {0} [$CheckStockMesssage2]", result.Trim() );
            uxStockLabel.Visible = true;
        }
        else
        {
            uxStockLabel.Text = String.Format( "[$CheckStockMessage]" );
            uxStockLabel.Visible = true;
        }
    }

    private CartItemGiftDetails CreateGiftDetails()
    {
        return uxGiftCertificateDetails.GetCartItemGiftDetails();
    }

    private void AddItemToShoppingCart()
    {
        OptionItemValueCollection selectedOptions = uxOptionGroupDetails.GetSelectedOptions();
        ProductKitItemValueCollection selectedKits = uxProductKitGroupDetails.GetSelectedProductKitItems();
        CartItemGiftDetails giftDetails = CreateGiftDetails();

        decimal customPrice = decimal.Parse( uxEnterAmountText.Text );
        customPrice = decimal.Divide( customPrice, Convert.ToDecimal( StoreContext.Currency.ConversionRate ) );


        CartAddItemService addToCartService = new CartAddItemService(
            StoreContext.Culture, StoreContext.ShoppingCart );
        int currentStock;
        string errorOptionName;
        bool stockOK = addToCartService.AddToCart(
            CurrentProduct,
            selectedOptions,
            selectedKits,
            ConvertUtilities.ToInt32( uxQuantityText.Text ),
            giftDetails,
            customPrice,
            out errorOptionName,
            out currentStock );


        if (stockOK)
        {
            Response.Redirect( "ShoppingCart.aspx" );
        }
        else
        {
            DisplayOutOfStockError( currentStock, errorOptionName );
        }
    }

    private bool IsMatchQuantity()
    {
        bool isOK = false;
        int quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        int minQuantity = CurrentProduct.MinQuantity;
        int maxQuantity = CurrentProduct.MaxQuantity;
        uxMinQuantityLabel.Text = "[$MinQuantity]" + minQuantity;
        uxMaxQuantityLabel.Text = "[$MaxQuantity]" + maxQuantity;

        if (minQuantity > quantity)
        {
            uxMessageMinQuantityDiv.Visible = true;
            uxMessageMaxQuantityDiv.Visible = false;
        }
        else if (maxQuantity != 0 && maxQuantity < quantity)
        {
            uxMessageMinQuantityDiv.Visible = false;
            uxMessageMaxQuantityDiv.Visible = true;
        }
        else
        {
            uxMessageMinQuantityDiv.Visible = false;
            uxMessageMaxQuantityDiv.Visible = false;
            isOK = true;
        }

        return isOK;
    }

    private bool VerifyValidInput( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (Page.IsValid && uxOptionGroupDetails.IsValidInput)
        {
            int result = 0;
            if (!int.TryParse( uxQuantityText.Text, out result ) ||
                result < 0 ||
                (uxQuantityText.Text == "" && uxQuantityText.Visible))
            {
                errorMessage = "[$InvalidQuantity]";
            }
            else
            {
                if (uxProductKitGroupDetails.IsValidInput( result ))
                    return true;
            }
        }

        return false;
    }

    private void AddToWishList( string productID, OptionItemValueCollection selectedOptions )
    {
        AddToWishList( productID, selectedOptions, "1" );
    }

    private void AddToWishList( string productID, OptionItemValueCollection selectedOptions, string quantity )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture,
            productID, new StoreRetriever().GetCurrentStoreID() );

        if (selectedOptions == null || selectedOptions.Count == 0)
        {
            StoreContext.WishList.Cart.AddItem( product, int.Parse( quantity ) );
        }
        else
        {
            OptionItemValueCollection inputLists = selectedOptions.GetOptionItemsByGroupType(
                OptionGroup.OptionGroupType.InputList );
            if (inputLists.Count == 0)
            {
                StoreContext.WishList.Cart.AddItem( product, int.Parse( quantity ), selectedOptions );
            }
            else
            {
                OptionItemValueCollection optionsWithoutInputList =
                    selectedOptions.GetOptionItemsNotInGroupType( OptionGroup.OptionGroupType.InputList );
                //add product is Loop.
                foreach (OptionItemValue optionInput in inputLists)
                {
                    OptionItemValueCollection options = optionsWithoutInputList.Clone();
                    options.Add( optionInput );

                    StoreContext.WishList.Cart.AddItem( product, int.Parse( optionInput.Details ), options );
                }
            }
        }

        DataAccessContext.CartRepository.UpdateWhole( StoreContext.WishList.Cart );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageDiv.Visible = false;
        uxMessageMinQuantityDiv.Visible = false;
        uxMessageMaxQuantityDiv.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxRelatedProducts.ProductID = CurrentProduct.ProductID;
    }

    protected void uxTellFriendLinkButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( UrlManager.GetTellFriendMobileUrl( CurrentProduct.ProductID, CurrentProduct.UrlName ) );
    }

    protected void uxAddToWishList_Click( object sender, EventArgs e )
    {
        OptionItemValueCollection selectedOptions = uxOptionGroupDetails.GetSelectedOptions();

        if (CurrentProduct.MinQuantity <= ConvertUtilities.ToInt32( uxQuantityText.Text ))
        {
            AddToWishList( CurrentProduct.ProductID, selectedOptions, uxQuantityText.Text );
        }
        else
        {
            AddToWishList( CurrentProduct.ProductID, selectedOptions, CurrentProduct.MinQuantity.ToString() );
        }

        Response.Redirect( "WishList.aspx" );
    }

    protected void uxAddToCartButton_Click( object sender, EventArgs e )
    {
        ProductSubscription subscriptionItem = new ProductSubscription( CurrentProduct.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (StoreContext.Customer.IsNull)
            {
                string returnUrl = "AddtoCart.aspx?ProductID=" + CurrentProduct.ProductID;
                Response.Redirect( "Login.aspx?ReturnUrl=" + returnUrl );
            }
        }

        if (!StoreContext.ShoppingCart.CheckCanAddItemToCart( CurrentProduct ))
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + CurrentProduct.ProductID );
        }

        if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx" );
        }
        else
        {
            string errorMessage = String.Empty;
            if (VerifyValidInput( out errorMessage ))
            {
                if (IsMatchQuantity())
                {
                    AddItemToShoppingCart();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty( errorMessage ))
                {
                    uxMessage.Text = errorMessage;
                    uxMessageDiv.Visible = true;
                }
            }
        }
    }

    protected bool IsShowCustomPrice( object isCustomPrice, object isCallForPrice )
    {
        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (ConvertUtilities.ToBoolean( isCustomPrice ))
            return true;

        return false;
    }

    protected bool IsExpressCheckoutEnabled()
    {
        IList<PaymentOption> paymentList = DataAccessContext.PaymentOptionRepository.GetAll( StoreContext.Culture, BoolFilter.ShowTrue );

        foreach (PaymentOption payment in paymentList)
        {
            if (payment.AnonymousCheckoutAllowed)
                return true;
        }

        return false;
    }

    protected bool IsHaveOption()
    {
        IList<ProductOptionGroup> OptionGroup = CurrentProduct.ProductOptionGroups;
        if (OptionGroup.Count <= 0)
            return false;
        else
            return true;
    }
}
