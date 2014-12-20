using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.WebUI;
using Vevo.Domain.Shipping;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebAppLib;
using Vevo.Domain.Payments.PayPalProExpress;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Domain.Shipping.Custom;

public partial class Mobile_Shipping : Vevo.WebUI.Orders.BaseProcessCheckoutPage
{
    private const string RecurringWarring = "Some shipping methods (e.g. UPS, FedEx) are disabled due to recurring products";


    private string CreateHandlingFeeText( decimal handlingFee )
    {
        if (handlingFee > 0)
            return " + Handling Fee " + StoreContext.Currency.FormatPrice( handlingFee );
        else
            return String.Empty;
    }

    private void FormatForListItem(
        string shippingID,
        string name,
        decimal shippingCost,
        decimal handlingFee,
        out string text,
        out string value )
    {
        text = String.Format( "{0} ({1}{2})",
            Server.HtmlDecode( name ),
            StoreContext.Currency.FormatPrice( shippingCost ),
            CreateHandlingFeeText( handlingFee ) );

        value = String.Format( "{0}-{1}-{2}",
            shippingID, shippingCost, handlingFee );
    }


    private void PopulateRealTimeShipping(
        ListItemCollection radioItems,
        out string errorMessage,
        out string restriction )
    {
        uxRecurringWarringLabel.Text = "";
        IList<ShippingOption> realTimeShippingOptions = DataAccessContext.ShippingOptionRepository.GetShipping(
            StoreContext.Culture, BoolFilter.ShowTrue );

        errorMessage = String.Empty;
        restriction = String.Empty;

        foreach (ShippingOption shippingOption in realTimeShippingOptions)
        {
            string tempError, tempRestriction;

            IList<ShippingChoice> shippingChoices = shippingOption.RequestRealTimeShippingChoices(
                StoreContext.CheckoutDetails.ShippingAddress,
                StoreContext.ShoppingCart,
                StoreContext.ShoppingCart.GetSubtotal( StoreContext.WholesaleStatus ),
                StoreContext.ShoppingCart.ContainsFreeShippingCostProduct() || CartItemPromotion.ContainsFreeShippingCostProductInBundlePromotion( StoreContext.ShoppingCart ),
                StoreContext.CheckoutDetails.Coupon,
                out tempError,
                out tempRestriction );

            if (!String.IsNullOrEmpty( tempError ))
            {
                errorMessage += shippingOption.ShippingOptionType.DisplayName + " Error: "
                    + tempError + "<br/><br/>";
            }

            if (!String.IsNullOrEmpty( tempRestriction ))
            {
                restriction += "<strong>" + shippingOption.ShippingOptionType.DisplayName
                    + " Restrictions:</strong> " + tempRestriction + "<br/><br/>";
            }

            foreach (ShippingChoice shippingChoice in shippingChoices)
            {
                string text, value;
                decimal shippingCost;

                if (StoreContext.CheckoutDetails.Coupon.DiscountType == Vevo.Domain.Discounts.Coupon.DiscountTypeEnum.FreeShipping)
                    shippingCost = 0.0m;
                else
                    shippingCost = shippingChoice.ShippingCost;

                FormatForListItem(
                    shippingOption.ShippingID,
                    shippingChoice.Name,
                    shippingCost,
                    shippingChoice.HandlingFee,
                    out text,
                    out value );

                radioItems.Add( new ListItem( text, value ) );
            }
        }
    }

    private void CalculateShippingCost(
        ShippingOption shippingOption,
        out decimal shippingCost,
        out decimal handlingFee )
    {
        OrderCalculator orderCalculator = new OrderCalculator();

        ShippingMethod shippingMethod = shippingOption.CreateNonRealTimeShippingMethod();

        if (StoreContext.CheckoutDetails.Coupon.DiscountType == Vevo.Domain.Discounts.Coupon.DiscountTypeEnum.FreeShipping)
            shippingCost = 0;
        else
        {
            decimal shippingCostProduct = orderCalculator.GetShippingCost(
                shippingMethod,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.WholesaleStatus,
                StoreContext.GetOrderAmount().Discount );
            decimal shippingCostPromotion = CartItemPromotion.GetShippingCostFromPromotion( shippingMethod,
                        StoreContext.ShoppingCart.SeparateCartItemGroups(),
                        StoreContext.WholesaleStatus,
                        StoreContext.GetOrderAmount().Discount );
            if (shippingMethod.GetType().IsAssignableFrom( typeof( FixedShippingMethod ) ))
            {
                if (shippingCostProduct > shippingCostPromotion)
                    shippingCost = shippingCostProduct;
                else
                    shippingCost = shippingCostPromotion;
            }
            else
            {
                shippingCost = shippingCostProduct + shippingCostPromotion;
            }
        }


        handlingFee = orderCalculator.GetHandlingFee(
            shippingMethod,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.WholesaleStatus );
    }

    private void PopulateNonRealTimeShipping( ListItemCollection radioItems )
    {
        IList<ShippingOption> shippingList = DataAccessContext.ShippingOptionRepository.GetShipping(
            StoreContext.Culture, BoolFilter.ShowFalse );

        foreach (ShippingOption shippingOption in shippingList)
        {
            if (shippingOption.CanShipTo( StoreContext.CheckoutDetails.ShippingAddress ))
            {
                decimal shippingCost, handlingFee;
                CalculateShippingCost( shippingOption, out shippingCost, out handlingFee );

                string text, value;
                FormatForListItem(
                    shippingOption.ShippingID,
                    shippingOption.ShippingName,
                    shippingCost,
                    handlingFee,
                    out text,
                    out value );

                radioItems.Add( new ListItem( text, value ) );
            }
        }
    }

    private void PopulateControls()
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxShippingRadioList.Items.Clear();

            string errorMessage, restriction;

            PopulateRealTimeShipping( uxShippingRadioList.Items, out errorMessage, out restriction );
            PopulateNonRealTimeShipping( uxShippingRadioList.Items );

            //PopulateShippingRadioButtons( uxShippingRadioList.Items );

            uxShippingRadioList.DataBind();

            DisplayRealTimeErrorMessage( errorMessage, restriction );

            if (uxShippingRadioList.Items.Count == 0)
            {
                DisplayNoShippingOption();
            }
        }
        else
        {
            DisplayEmptyShoppingCart();
        }

    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Shipping_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( Shipping_CurrencyChanged );
    }

    private void Shipping_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void Shipping_CurrencyChanged( object send, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    private void DisplayRealTimeErrorMessage( string errorMessage, string restriction )
    {
        if (!String.IsNullOrEmpty( errorMessage ))
        {
            uxRealTimeMessageLabel.Text = errorMessage;
            uxRealTimeMessagePanel.Visible = true;
        }

        if (!String.IsNullOrEmpty( restriction ))
        {
            uxRestrictionsLiteral.Text = WebUtilities.ReplaceNewLine( restriction );
        }
    }

    private void HideButtons()
    {
        uxNextButton.Visible = false;
        uxCancelButton.Visible = false;
    }

    private void DisplayEmptyShoppingCart()
    {
        uxMessage.DisplayMessage( "<br/>[$ShoppingCartEmpty]" );
        HideButtons();
    }

    private void DisplayNoShippingOption()
    {
        uxMessage.DisplayMessage( "<br/>[$NoShippingOption]" );
        HideButtons();
    }

    private void ExtractListItemValue(
        string listItemValue,
        out string shippingID,
        out decimal shippingCost,
        out decimal handlingFee )
    {
        string[] selectItem = listItemValue.Split( '-' );

        shippingID = selectItem[0];
        shippingCost = ConvertUtilities.ToDecimal( selectItem[1] );
        handlingFee = ConvertUtilities.ToDecimal( selectItem[2] );
    }

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if (index > 0)
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    private void SetShippingAndRedirect()
    {
        string shippingID;
        decimal shippingCost, handlingFee;
        ExtractListItemValue( uxShippingRadioList.SelectedValue,
            out shippingID, out shippingCost, out handlingFee );

        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
            StoreContext.Culture, shippingID );

        if (shippingOption.ShippingOptionType.IsRealTime
            && StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            uxRecurringWarringLabel.Text = RecurringWarring;
            return;
        }

        ShippingChoice shippingChoice = new ShippingChoice(
            ExtractNameFromListItemText( uxShippingRadioList.SelectedItem.Text ), shippingCost, handlingFee );

        ShippingMethod shippingMethod = shippingOption.CreateShippingMethod( shippingChoice );
        StoreContext.CheckoutDetails.ShippingMethod = shippingMethod;

        if (!(Request.QueryString["skiplogin"] == "true"))
            Response.Redirect( "Payment.aspx" );
        else
            Response.Redirect( "Payment.aspx?skiplogin=true" );
    }

    private bool RealTimeHasError()
    {
        return uxRealTimeMessageLabel.Text.Length != 0 || uxRealTimeMessagePanel.Visible;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if (Request.QueryString["Token"] != null)
        {
            PayPalProExpressPaymentMethod payment = (PayPalProExpressPaymentMethod)
                StoreContext.CheckoutDetails.PaymentMethod;
            //if (!payment.ProcessPostLoginDetails( Request.QueryString["Token"], StoreContext.CheckoutSession ))
            //{
            //    payment.RedirectToErrorPage();
            //}            

            if (!payment.ProcessPostLoginDetails( Request.QueryString["Token"], StoreContext.CheckoutDetails ))
            {
                //payment.RedirectToErrorPage();
                CheckoutNotCompletePage.RedirectToPage(
                "Error Message",
                payment.GetErrorMessage(),
                "ShoppingCart.aspx",
                "Return To Shopping" );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ((StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem()) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            StoreContext.CheckoutDetails.SetShipping( ShippingMethod.Null );
            if (!(Request.QueryString["skiplogin"] == "true"))
                Response.Redirect( "Payment.aspx" );
            else
                Response.Redirect( "Payment.aspx?skiplogin=true" );
        }

        if (!IsPostBack)
        {
            PopulateControls();
        }

        if (uxShippingRadioList.Items.Count == 1)
        {
            uxShippingRadioList.Items[0].Selected = true;
            if (!RealTimeHasError())
            {
                SetShippingAndRedirect();
            }
        }
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        SetShippingAndRedirect();
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "ShoppingCart.aspx" );
    }

}
