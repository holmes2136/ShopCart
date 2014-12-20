using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Payments;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Shared.DataAccess;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.Orders;

public partial class ShoppingCartPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private IList<PaymentOption> GetPaymentsWithoutButton()
    {
        return DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            StoreContext.Culture, BoolFilter.ShowTrue );
    }

    private IList<PaymentOption> GetPaymentsWithButton()
    {
        return DataAccessContext.PaymentOptionRepository.GetButtonList(
            StoreContext.Culture, BoolFilter.ShowTrue );
    }

    private void PopulatePaymentButtons()
    {
        IList<PaymentOption> paymentsWithoutButton = GetPaymentsWithoutButton();
        if (paymentsWithoutButton.Count == 0)
        {
            uxCheckoutImageButton.Visible = false;
        }

        IList<PaymentOption> paymentsWithButton = GetPaymentsWithButton();
        if (paymentsWithButton.Count > 0)
        {
            if (!StoreContext.ShoppingCart.ContainsSubscriptionProduct())
            {
                if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
                {
                    uxOrTR.Visible = true;
                    uxExpressPaymentButtonsTR.Visible = true;

                    for (int i = 0; i < paymentsWithButton.Count; i++)
                    {
                        BaseButtonPaymentMethod control =
                            (BaseButtonPaymentMethod) LoadControl( "~/" + paymentsWithButton[i].ButtonUserControl );

                        control.AddConfrimBox();
                        Panel panel = new Panel();
                        panel.CssClass = "ExpressPaymentButton";
                        panel.Controls.Add( control );

                        uxButtonPlaceHolder.Controls.Add( panel );
                    }
                }
                else
                {
                    uxOrTR.Visible = true;
                    uxRecurringWarning.Visible = true;
                }
            }
        }
    }

    private decimal ExtractCostFromShippingMethodText( string listItemName )
    {
        if (String.IsNullOrEmpty( listItemName ))
            return 0;
        int index = listItemName.LastIndexOf( "(" ) + 2;
        int length = listItemName.Length - 1;
        string number = listItemName.Substring( index, length - index );
        if (index > 0)
            return decimal.Parse( number.Trim() );
        else
            return 0;
    }

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if (index > 0)
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    private void PopulateControls()
    {
        decimal shippingTax = 0;
        decimal handlingTax = 0;
        bool isTaxIncluded = DataAccessContext.Configurations.GetBoolValue( "TaxIncludedInPrice" );

        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxPanel.Visible = true;

            OrderAmount orderAmount = StoreContext.GetOrderAmount();

            if (orderAmount.ShippingCost > 0)
            {
                if (!DataAccessContext.Configurations.GetBoolValue( "IsTaxableShippingCost" ))
                {
                    shippingTax = 0;
                }
                else
                {
                    shippingTax = orderAmount.ShippingCost * DataAccessContext.Configurations.GetDecimalValue( "TaxPercentageIncludedInPrice" ) / 100;
                }
            }

            if (orderAmount.HandlingFee > 0)
            {
                if (!DataAccessContext.Configurations.GetBoolValue( "IsTaxableShippingCost" ))
                {
                    handlingTax = 0;
                }
                else
                {
                    handlingTax = orderAmount.HandlingFee * DataAccessContext.Configurations.GetDecimalValue( "TaxPercentageIncludedInPrice" ) / 100;
                }
            }

            if (isTaxIncluded)
            {
                uxTotalAmountLabel.Text = StoreContext.Currency.FormatPrice( StoreContext.ShoppingCart.GetSubtotalIncludedTax( StoreContext.WholesaleStatus )
                                            + (orderAmount.ShippingCost + shippingTax)
                                            + (orderAmount.HandlingFee + handlingTax)
                                            + (orderAmount.Discount * -1)
                                            + (orderAmount.PointDiscount * -1)
                                            + (orderAmount.GiftCertificate * -1) );
            }
            else
            {
                uxTotalAmountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Total );
            }

            if (isTaxIncluded)
            {
                uxSubTotalLabel.Text = StoreContext.Currency.FormatPrice( StoreContext.ShoppingCart.GetSubtotalIncludedTax( StoreContext.WholesaleStatus ) );
            }
            else
            {
                uxSubTotalLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Subtotal );
            }

            if (orderAmount.Discount > 0)
            {
                uxDiscountTR.Visible = true;
                uxDiscountAmountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Discount * -1 );
            }
            else
                uxDiscountTR.Visible = false;

            if (orderAmount.PointDiscount > 0)
            {
                uxRewardDiscountTR.Visible = true;
                uxRewardDiscountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.PointDiscount * -1 );
            }
            else
                uxRewardDiscountTR.Visible = false;

            if (orderAmount.GiftCertificate > 0)
            {
                uxGiftDiscountTR.Visible = true;
                uxGiftDiscountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.GiftCertificate * -1 );
            }
            else
                uxGiftDiscountTR.Visible = false;

            if (orderAmount.Tax > 0 && !isTaxIncluded)
            {
                uxTaxTR.Visible = true;
                uxTaxAmountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Tax );
            }
            else
            {
                uxTaxTR.Visible = false;
            }

            if ( orderAmount.ShippingCost > 0 )
            {
                uxShippingTR.Visible = true;
                if (isTaxIncluded)
                {
                    uxShippingAmountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.ShippingCost + shippingTax );
                }
                else
                {
                    uxShippingAmountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.ShippingCost );
                }
            }
            else
                uxShippingTR.Visible = false;

            if (orderAmount.HandlingFee > 0)
            {
                uxHandlingFeeTR.Visible = true;
                if (isTaxIncluded)
                {
                    uxHandlingFeeLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.HandlingFee + handlingTax );
                }
                else
                {
                    uxHandlingFeeLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.HandlingFee );
                }
            }
            else
                uxHandlingFeeTR.Visible = false;

            uxGrid.DataSource = StoreContext.ShoppingCart.GetCartItems();
            uxGrid.DataBind();
        }
        else
        {
            uxPanel.Visible = false;
            uxCartEmptyDiv.Visible = true;
            uxCartEmptyLabel.Text = "[$Your shopping cart is empty]";

            uxBackHomeLink.Visible = true;
            StoreContext.CheckoutDetails.Clear();
        }
        if (uxTaxIncludePanel.Visible)
        {
            uxTaxIncludeMessageLabel.Text =
                String.Format( "[$TaxIncludeMessage1] {0} [$TaxIncludeMessage2]",
                DataAccessContext.Configurations.GetValue( "TaxPercentageIncludedInPrice" ).ToString() );
        }

        PopulateContinueUrl();
        PopulateAddToGiftRegistryButton();
    }

    private bool VerifyQuantity()
    {
        string stockMessage = String.Empty;
        string giftRegistryMessage = String.Empty;
        string minMaxQuantityMessage = String.Empty;

        if (!IsEnoughStock( out stockMessage ))
        {
            uxMessageLiteral.DisplayError(stockMessage);
            return false;
        }

        if (!IsEnoughGiftRegistyWantQuantity( out giftRegistryMessage ))
        {
            uxMessageLiteral.DisplayError(giftRegistryMessage);
            return false;
        }

        if (!IsAcceptQuantity( out minMaxQuantityMessage ))
        {
            uxMessageLiteral.DisplayError(minMaxQuantityMessage);
            return false;
        }

        return true;
    }

    private bool UpdateQuantity()
    {
        bool result = true;

        try
        {
            if (!VerifyQuantity())
                return false;

            int rowIndex;
            for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
            {
                GridViewRow row = uxGrid.Rows[rowIndex];
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string quantityText = ((TextBox) row.FindControl( "uxQuantityText" )).Text;
                    int quantity = 1;

                    if (int.TryParse( quantityText, out quantity ))
                    {
                        string cartItemID = uxGrid.DataKeys[rowIndex]["CartItemID"].ToString();
                        StoreContext.ShoppingCart.UpdateQuantity(
                            cartItemID,
                            quantity );
                    }
                }
            }
        }
        catch
        {
            uxMessage.DisplayError( "[$UpdateFailed]" );
            result = false;
        }

        uxCartStatusHidden.Value = "Updated";
        return result;
    }

    private void PopulateContinueUrl()
    {
        LinkButton link = (LinkButton) uxPanel.FindControl( "uxContinueLink" );
        if (!StoreContext.CheckoutDetails.ContainsGiftRegistry())
            link.PostBackUrl = "~/Catalog.aspx";
        else
            link.PostBackUrl = "GiftRegistryItem.aspx?GiftRegistryID=" + StoreContext.CheckoutDetails.GiftRegistryID;
    }

    private bool HasGiftRegistryByUser( string userName )
    {
        IList<GiftRegistry> list = DataAccessContextDeluxe.GiftRegistryRepository.GetAllByUserName(
            userName, DataAccessContext.StoreRetriever.GetCurrentStoreID() );
        return (list.Count > 0);
    }

    private void PopulateAddToGiftRegistryButton()
    {
        MembershipUser user = Membership.GetUser();

        if (!StoreContext.CheckoutDetails.ContainsGiftRegistry() &&
            user != null &&
            DataAccessContext.Configurations.GetBoolValue( "GiftRegistryModuleDisplay" )
            && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName )
            )
        {
            if (Roles.GetRolesForUser( user.UserName )[0].ToLower() == "customers"
                && HasGiftRegistryByUser( user.UserName ))
                uxAddToGiftRegistryImageButton.Visible = true;
            else
                uxAddToGiftRegistryImageButton.Visible = false;
        }
        else
            uxAddToGiftRegistryImageButton.Visible = false;
    }

    private bool CheckUseInventory( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );
        return product.UseInventory;
    }

    private bool IsEnoughStock( out string message )
    {
        message = String.Empty;

        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                ICartItem cartItem = (ICartItem) StoreContext.ShoppingCart.FindCartItemByID( (string) uxGrid.DataKeys[rowIndex]["CartItemID"] );
                bool isPromotion = (bool) uxGrid.DataKeys[rowIndex]["IsPromotion"];
                bool isProductKit = (bool) uxGrid.DataKeys[rowIndex]["IsProductKit"];
                string productID = uxGrid.DataKeys[rowIndex]["ProductID"].ToString();
                string productName = ((HyperLink) row.FindControl( "uxItemNameLink" )).Text;
                int quantity = ConvertUtilities.ToInt32( ((TextBox) row.FindControl( "uxQuantityText" )).Text );
                if (!isPromotion)
                {
                    Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );

                    OptionItemValueCollection options = (OptionItemValueCollection) uxGrid.DataKeys[rowIndex]["Options"];
                    int productStock = product.GetStock( options.GetUseStockOptionItemIDs() );
                    int currentStock = productStock - quantity;
                    if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
                    {
                        if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( productID ) ))
                        {
                            message += "<li>" + productName;
                            if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
                            {
                                int displayStock = productStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );
                                if (displayStock < 0)
                                {
                                    displayStock = 0;
                                }
                                message += " ( available " + displayStock + " items )";
                            }
                            else
                            {
                                message += "</li>";
                            }
                        }
                    }

                    if (isProductKit)
                    {
                        ProductKitItemValueCollection valueCollection = cartItem.ProductKits;
                        foreach (ProductKitItemValue value in valueCollection)
                        {
                            product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, value.ProductID, new StoreRetriever().GetCurrentStoreID() );
                            productStock = product.GetStock( new string[0] );
                            currentStock = productStock - quantity;
                            if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
                            {
                                if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( product.ProductID ) ))
                                {
                                    message += "<li>" + product.Name + " of " + productName;
                                    if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
                                    {
                                        int displayStock = productStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );
                                        if (displayStock < 0)
                                        {
                                            displayStock = 0;
                                        }
                                        message += " ( available " + displayStock + " items )";
                                    }
                                    else
                                    {
                                        message += "</li>";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    CartItemPromotion promotionCartItem = (CartItemPromotion) cartItem;
                    PromotionSelected promotionSelected = promotionCartItem.PromotionSelected;
                    foreach (PromotionSelectedItem item in promotionSelected.PromotionSelectedItems)
                    {
                        Product product = item.Product;
                        IList<ProductOptionGroup> groups = DataAccessContext.ProductRepository.GetProductOptionGroups( StoreContext.Culture, product.ProductID );
                        string[] optionsUseStock = item.GetUseStockOptionItems().ToArray( typeof( string ) ) as string[];
                        int productStock = product.GetStock( optionsUseStock );
                        int currentStock = productStock - quantity;
                        if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
                        {
                            if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( product.ProductID ) ))
                            {
                                message += "<li>" + productName;
                                message += "</li>";
                            }
                        }
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">[$StockError]</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool CheckBunblePromotionBeforeAddToGiftRegistry( out string message )
    {
        message = String.Empty;

        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string productName = ((HyperLink) row.FindControl( "uxItemNameLink" )).Text;
                bool isPromotion = (bool) uxGrid.DataKeys[rowIndex]["IsPromotion"];
                if (isPromotion)
                {
                    message += "<li>" + productName + "</li>";
                }
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">[$GiftRegistryErrorBundlePromotion]</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsEnoughGiftRegistyWantQuantity( out string message )
    {
        CheckoutDetails checkout = StoreContext.CheckoutDetails;
        message = String.Empty;
        if (!String.IsNullOrEmpty( checkout.GiftRegistryID ) && checkout.GiftRegistryID != "0")
        {
            int rowIndex;
            for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
            {
                GridViewRow row = uxGrid.Rows[rowIndex];
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string cartItemID = uxGrid.DataKeys[rowIndex]["CartItemID"].ToString();
                    string giftRegistryItemID =
                        StoreContext.CheckoutDetails.CartItemIDToGiftRegistryIDMap[cartItemID];

                    GiftRegistryItem giftRegistryItem = DataAccessContextDeluxe.GiftRegistryItemRepository.GetOne(
                        giftRegistryItemID );

                    int wantQuantity = (int) (giftRegistryItem.WantQuantity - giftRegistryItem.HasQuantity);

                    string productName = ((HyperLink) row.FindControl( "uxItemNameLink" )).Text;
                    int quantity = ConvertUtilities.ToInt32( ((TextBox) row.FindControl( "uxQuantityText" )).Text );

                    if (quantity > wantQuantity)
                    {
                        message += "<li>" + productName + "</li>";
                    }
                }
            }

            if (!String.IsNullOrEmpty( message ))
            {
                message =
                    "<p class=\"ErrorHeader\">[$GiftRegistryError]</p>" +
                    "<ul class=\"ErrorBody\">" + message + "</ul>";

                return false;
            }
            else
            {
                return true;
            }
        }
        else
            return true;
    }

    private bool IsAcceptQuantity( out string message )
    {
        message = String.Empty;

        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                if ((bool) uxGrid.DataKeys[rowIndex]["IsPromotion"])
                    continue;
                string productID = uxGrid.DataKeys[rowIndex]["ProductID"].ToString();
                string productName = ((HyperLink) row.FindControl( "uxItemNameLink" )).Text;
                int quantity = ConvertUtilities.ToInt32( ((TextBox) row.FindControl( "uxQuantityText" )).Text );

                Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );
                int minQuantity = product.MinQuantity;
                int maxQuantity = product.MaxQuantity;

                if (quantity < minQuantity)
                {
                    message += "<li>" + productName;
                    message += " ( minimum quantity " + minQuantity + " items )";
                    message += "</li>";
                }

                if (maxQuantity != 0 && quantity > maxQuantity)
                {
                    message += "<li>" + productName;
                    message += " ( maximum quantity " + maxQuantity + " items )";
                    message += "</li>";
                }
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">[$QuantityError]</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool TaxIncludeVisibility()
    {
        return DataAccessContext.Configurations.GetBoolValue( "TaxIncludedInPrice" );
    }

    private void SignOut()
    {
        if (Page.User.Identity.IsAuthenticated &&
           (!Roles.IsUserInRole( Page.User.Identity.Name, "Customers" )))
            FormsAuthentication.SignOut();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulatePaymentButtons();

        if (CatalogUtilities.IsCatalogMode())
        {
            //************ do something *****************
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxTaxIncludePanel.Visible = TaxIncludeVisibility();
        PopulateControls();
    }

    protected void uxGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        StoreContext.ShoppingCart.DeleteItem(
            uxGrid.DataKeys[e.RowIndex]["CartItemID"].ToString() );

        uxCartStatusHidden.Value = "Deleted";
    }

    protected void uxUpdateQuantityImageButton_Click( object sender, EventArgs e )
    {
        UpdateQuantity();
    }

    protected void uxClearCartButton_Click( object sender, EventArgs e )
    {
        StoreContext.ClearCheckoutSession();
    }

    protected void uxCheckoutImageButton_Click( object sender, EventArgs e )
    {
        if (UpdateQuantity())
        {
            SignOut();
            Response.Redirect( "Checkout.aspx" );
        }
    }

    protected void uxAddToGiftRegistryImageButton_Click( object sender, EventArgs e )
    {
        if (UpdateQuantity())
        {
            string errorMessage;
            if (CheckBunblePromotionBeforeAddToGiftRegistry( out errorMessage ))
            {
                SignOut();
                Response.Redirect( "GiftRegistrySelect.aspx" );
            }
            else
            {
                uxMessageLiteral.DisplayError( errorMessage );
            }
        }
    }

    protected void uxGrid_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        TextBox quantity = (TextBox) e.Row.FindControl( "uxQuantityText" );
        WebUtilities.TieButton( this.Page, quantity, uxUpdateQuantityImageButton );
    }

    protected string GetName( object cartItem )
    {
        return ((ICartItem) cartItem).GetName( StoreContext.Culture, StoreContext.Currency );
    }

    protected string GetMainText( object cartItem )
    {
        string mainText;
        ((ICartItem) cartItem).GetPreCheckoutTooltip(
            StoreContext.Currency,
            StoreContext.WholesaleStatus,
            out mainText );
        return mainText;
    }

    protected string GetTaxTooltipText()
    {
        return String.Format( "[$TaxIncludeMessage1] {0} [$TaxIncludeMessage2]",
                DataAccessContext.Configurations.GetValue( "TaxPercentageIncludedInPrice" ).ToString() );
    }

    protected string GetTooltipText( object cartItem )
    {
        string mainText;
        return ((ICartItem) cartItem).GetPreCheckoutTooltip(
            StoreContext.Currency,
            StoreContext.WholesaleStatus,
            out mainText );
    }

    protected string GetUnitPriceText( object cartItem )
    {
        return StoreContext.Currency.FormatPrice( ((ICartItem) cartItem).GetShoppingCartUnitPrice(
            StoreContext.WholesaleStatus ) );
    }

    protected string GetSubtotalText( object cartItem )
    {
        ICartItem item = ((ICartItem) cartItem);

        return StoreContext.Currency.FormatPrice(
            item.GetShoppingCartUnitPrice( StoreContext.WholesaleStatus )
            * item.Quantity );
    }
    protected string GetItemImage( object cartItem )
    {
        ICartItem baseCartItem = (ICartItem) cartItem;

        if (baseCartItem.IsPromotion)
        {
            CartItemPromotion cartItemPromotion = (CartItemPromotion) baseCartItem;
            PromotionGroup promotion = cartItemPromotion.PromotionSelected.GetPromotionGroup();

            if (String.IsNullOrEmpty( promotion.ImageFile ))
            {
                return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
            }
            else
            {
                return "~/" + promotion.ImageFile;
            }
        }
        else
        {
            ProductImage details = baseCartItem.Product.GetPrimaryProductImage();

            if (String.IsNullOrEmpty( details.ThumbnailImage ))
            {
                return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
            }
            else
            {
                return "~/" + details.ThumbnailImage;
            }
        }
    }

    protected string GetLink( object cartItem )
    {
        ICartItem baseCartItem = (ICartItem) cartItem;
        if (baseCartItem.IsPromotion)
        {
            CartItemPromotion cartItemPromotion = (CartItemPromotion) baseCartItem;
            PromotionGroup promotion = cartItemPromotion.PromotionSelected.GetPromotionGroup();
            return UrlManager.GetPromotionUrl( promotion.PromotionGroupID, promotion.Locales[StoreContext.Culture].UrlName );
        }
        else
        {
            Product product = baseCartItem.Product;
            return UrlManager.GetProductUrl( product.ProductID, product.Locales[StoreContext.Culture].UrlName );
        }
    }
}
