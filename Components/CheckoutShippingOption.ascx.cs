using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Shipping;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Domain.Shipping.Custom;

public partial class Components_CheckoutShippingOption : BaseLanguageUserControl
{
    private const string RecurringWarring = "Some shipping methods (e.g. UPS, FedEx) are disabled due to recurring products";

    #region private
    private string CreateHandlingFeeText( decimal handlingFee )
    {
        if (handlingFee > 0)
            return " + [$HandlingFee] " + StoreContext.Currency.FormatPrice( handlingFee );
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

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if (index > 0)
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    #endregion

    #region protected
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
            RestoreSelectedShippingOption();
        }
    }
    #endregion

    #region public

    public void RestoreSelectedShippingOption()
    {
        ShippingMethod shippingMethod = StoreContext.CheckoutDetails.ShippingMethod;
        if (shippingMethod == null) return;
        for (int i = 0; i < uxShippingRadioList.Items.Count; i++)
        {
            string[] values = uxShippingRadioList.Items[i].Value.Split( "-".ToCharArray() );
            if (values[0] == shippingMethod.ShippingID)
            {
                uxShippingRadioList.SelectedIndex = i;
                break;
            }
        }
    }

    public void DisplayRealTimeErrorMessage( string errorMessage, string restriction )
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

    public string DisplayEmptyShoppingCart()
    {
        return "[$ShoppingCartEmpty]";
    }

    public string DisplayNoShippingOption()
    {
        return "[$NoShippingOption]";
    }

    public bool RealTimeHasError()
    {
        return uxRealTimeMessageLabel.Text.Length != 0 || uxRealTimeMessagePanel.Visible;
    }

    public void PopulateControls()
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxShippingRadioList.Items.Clear();

            string errorMessage, restriction;

            PopulateRealTimeShipping( uxShippingRadioList.Items, out errorMessage, out restriction );
            PopulateNonRealTimeShipping( uxShippingRadioList.Items );

            uxShippingRadioList.DataBind();

            string currentEstimated = StoreContext.CheckoutDetails.EstimatedShippingSelect;
            if (!String.IsNullOrEmpty( currentEstimated ))
            {
                for (int i = 0; i < uxShippingRadioList.Items.Count; i++)
                {
                    ListItem item = uxShippingRadioList.Items[i];
                    if ((ExtractNameFromListItemText( item.Text )) == (ExtractNameFromListItemText( currentEstimated )))
                    {
                        uxShippingRadioList.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (uxShippingRadioList.Items.Count <= 0)
                uxIntro.Visible = false;

            DisplayRealTimeErrorMessage( errorMessage, restriction );
        }
    }

    public void DisplayRecurringWarningMessage()
    {
        uxRecurringWarringLabel.Text = RecurringWarring;
    }

    public RadioButtonList ShippingOptionList
    {
        get
        {
            return uxShippingRadioList;
        }
    }
    #endregion
}
