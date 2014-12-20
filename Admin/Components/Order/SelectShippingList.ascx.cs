using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.Domain.Orders;

public partial class Admin_Components_Order_SelectShippingList : AdminAdvancedBaseUserControl
{
    private const string RecurringWarring = "Some shipping methods (e.g. UPS, FedEx) are disabled due to recurring products";

    private string CreateHandlingFeeText( decimal handlingFee )
    {
        if (handlingFee > 0)
            return " + Handling Fee " + CurrentCurrency.FormatPrice( handlingFee );
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
            CurrentCurrency.FormatPrice( shippingCost ),
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
            CurrentCulture, BoolFilter.ShowTrue );

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

                FormatForListItem(
                    shippingOption.ShippingID,
                    shippingChoice.Name,
                    shippingChoice.ShippingCost,
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
            shippingCost = orderCalculator.GetShippingCost(
                shippingMethod,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.WholesaleStatus,
                StoreContext.GetOrderAmount().Discount )
                + CartItemPromotion.GetShippingCostFromPromotion( shippingMethod,
                    StoreContext.ShoppingCart.SeparateCartItemGroups(),
                    StoreContext.WholesaleStatus,
                    StoreContext.GetOrderAmount().Discount );

        handlingFee = orderCalculator.GetHandlingFee(
            shippingMethod,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.WholesaleStatus );
    }

    private void PopulateNonRealTimeShipping( ListItemCollection radioItems )
    {
        IList<ShippingOption> shippingList = DataAccessContext.ShippingOptionRepository.GetShipping(
            CurrentCulture, BoolFilter.ShowFalse );

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

    public void PopulateControls()
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
        uxRealTimeMessagePanel.Visible = false;
        uxRestrictionsLiteral.Visible = false;
        if (!String.IsNullOrEmpty( errorMessage ))
        {
            uxRealTimeMessageLabel.Text = errorMessage;
            uxRealTimeMessagePanel.Visible = true;
        }

        if (!String.IsNullOrEmpty( restriction ))
        {
            uxRestrictionsLiteral.Text = WebUtilities.ReplaceNewLine( restriction );
            uxRestrictionsLiteral.Visible = true;
        }
    }

    private void HideButtons()
    {
        //uxNextImageButton.Visible = false;
        //uxCancelImageButton.Visible = false;
    }

    private void DisplayEmptyShoppingCart()
    {
        uxMessage.DisplayMessage( "Shopping cart is empty." );
        HideButtons();
    }

    private void DisplayNoShippingOption()
    {
        uxMessage.DisplayError( "No shipping method available." );
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

    public ShippingMethod GetSelectedShippingMethod()
    {
        string shippingID;
        decimal shippingCost, handlingFee;

        if (uxShippingRadioList.SelectedValue == "")
            return ShippingMethod.Null;

        ExtractListItemValue( uxShippingRadioList.SelectedValue,
            out shippingID, out shippingCost, out handlingFee );

        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
            CurrentCulture, shippingID );

        if (shippingOption.ShippingOptionType.IsRealTime
            && StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            uxRecurringWarringLabel.Text = RecurringWarring;
            //return;
        }

        ShippingChoice shippingChoice = new ShippingChoice(
            ExtractNameFromListItemText( uxShippingRadioList.SelectedItem.Text ), shippingCost, handlingFee );

        ShippingMethod shippingMethod = shippingOption.CreateShippingMethod( shippingChoice );
        //StoreContext.CheckoutDetails.ShippingMethod = shippingMethod;

        return shippingMethod;
    }

    private bool RealTimeHasError()
    {
        return uxRealTimeMessageLabel.Text.Length != 0 || uxRealTimeMessagePanel.Visible;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ((StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem()) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            StoreContext.CheckoutDetails.SetShipping( ShippingMethod.Null );
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

    private Currency CurrentCurrency
    {
        get { return DataAccessContext.CurrencyRepository.GetOne( CurrencyCode ); }
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

    private Culture CurrentCulture
    {
        get { return DataAccessContext.CultureRepository.GetOne( CultureID ); }
    }
}
