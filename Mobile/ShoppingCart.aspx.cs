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
using Vevo.WebUI.Payments;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.Orders;

public partial class Mobile_ShoppingCart : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private decimal GetShoppingCartTotal()
    {
        OrderCalculator orderCalculator = new OrderCalculator();
        return orderCalculator.GetShoppingCartTotal(
            StoreContext.Customer,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.CheckoutDetails.Coupon );
    }

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
            else
            {
                uxOrTR.Visible = true;
                uxSubscriptionWarning.Visible = true;
            }

        }
    }

    private void PopulateControls()
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxPanel.Visible = true;
            uxTotalAmountLabel.Text = StoreContext.Currency.FormatPrice(
                GetShoppingCartTotal() );

            OrderCalculator orderCalculator = new OrderCalculator();
            decimal discount = orderCalculator.GetPreCheckoutDiscount(
                StoreContext.Customer,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.CheckoutDetails.Coupon );

            if (discount > 0)
            {
                uxDiscountTR.Visible = true;
                uxDiscountAmountLabel.Text = StoreContext.Currency.FormatPrice( discount * -1 );
            }
            else
                uxDiscountTR.Visible = false;

            uxGrid.DataSource = StoreContext.ShoppingCart.GetCartItems();
            uxGrid.DataBind();
        }
        else
        {
            uxPanel.Visible = false;
            uxShoppingCartDiv.Visible = true;
            uxMessageLabel.Text = "[$Your shopping cart is empty]";
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
    }

    private bool VerifyQuantity()
    {
        string stockMessage = String.Empty;
        string giftRegistryMessage = String.Empty;
        string minMaxQuantityMessage = String.Empty;

        if (!IsEnoughStock( out stockMessage ))
        {
            uxMessage.DisplayError( stockMessage );
            return false;
        }

        if (!IsEnoughGiftRegistyWantQuantity( out giftRegistryMessage ))
        {
            uxMessage.DisplayError( giftRegistryMessage );
            return false;
        }

        if (!IsAcceptQuantity( out minMaxQuantityMessage ))
        {
            uxMessage.DisplayError( minMaxQuantityMessage );
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
        if (!StoreContext.CheckoutDetails.ContainsGiftRegistry())
            uxContinueLink.PostBackUrl = "Catalog.aspx";
        else
            uxContinueLink.PostBackUrl =
                "GiftRegistryItem.aspx?GiftRegistryID=" + StoreContext.CheckoutDetails.GiftRegistryID;
    }

    private bool HasGiftRegistryByUser( string userName )
    {
        IList<GiftRegistry> list = DataAccessContextDeluxe.GiftRegistryRepository.GetAllByUserName(
            userName, DataAccessContext.StoreRetriever.GetCurrentStoreID() );
        return (list.Count > 0);
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
                string productID = uxGrid.DataKeys[rowIndex]["ProductID"].ToString();
                string productName = ((Label) row.FindControl( "uxNameLabel" )).Text;
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
                }
                else
                {
                    CartItemPromotion cartItemPromotion = (CartItemPromotion) cartItem;
                    PromotionSelected promotionSelected = cartItemPromotion.PromotionSelected;
                    foreach (PromotionSelectedItem item in promotionSelected.PromotionSelectedItems)
                    {
                        Product product = item.Product;
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

                    string productName = ((Label) row.FindControl( "uxNameLabel" )).Text;
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
                string productName = ((Label) row.FindControl( "uxNameLabel" )).Text;
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

        PopulateControls();
    }

    protected void uxUpdateQuantityButton_Click( object sender, EventArgs e )
    {
        UpdateQuantity();
        PopulateControls();
    }

    protected void uxContinueButton_Click( object sender, EventArgs e )
    {
        PopulateContinueUrl();
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
}
