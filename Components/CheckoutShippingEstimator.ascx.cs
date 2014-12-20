using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Shipping;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Domain.Shipping.Custom;

public partial class Components_CheckoutShippingEstimator : BaseLanguageUserControl
{
    #region private
    private ShippingAddress _shippingAddress;

    private string CreateHandlingFeeText( decimal handlingFee )
    {
        if ( handlingFee > 0 )
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
        IList<ShippingOption> realTimeShippingOptions = DataAccessContext.ShippingOptionRepository.GetShipping(
            StoreContext.Culture, BoolFilter.ShowTrue );

        errorMessage = String.Empty;
        restriction = String.Empty;

        foreach ( ShippingOption shippingOption in realTimeShippingOptions )
        {
            string tempError, tempRestriction;

            IList<ShippingChoice> shippingChoices = shippingOption.RequestRealTimeShippingChoices(
                _shippingAddress,
                StoreContext.ShoppingCart,
                StoreContext.ShoppingCart.GetSubtotal( StoreContext.WholesaleStatus ),
                StoreContext.ShoppingCart.ContainsFreeShippingCostProduct() || CartItemPromotion.ContainsFreeShippingCostProductInBundlePromotion(StoreContext.ShoppingCart),
                StoreContext.CheckoutDetails.Coupon,
                out tempError,
                out tempRestriction );

            foreach ( ShippingChoice shippingChoice in shippingChoices )
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

        if ( StoreContext.CheckoutDetails.Coupon.DiscountType == Vevo.Domain.Discounts.Coupon.DiscountTypeEnum.FreeShipping )
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

        foreach ( ShippingOption shippingOption in shippingList )
        {
            if ( shippingOption.CanShipTo( _shippingAddress ) )
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
        if ( StoreContext.ShoppingCart.GetCartItems().Length > 0 )
        {
            uxShippingRadioList.Items.Clear();

            string errorMessage, restriction;

            PopulateRealTimeShipping( uxShippingRadioList.Items, out errorMessage, out restriction );
            PopulateNonRealTimeShipping( uxShippingRadioList.Items );

            uxShippingRadioList.DataBind();

            if ( uxShippingRadioList.Items.Count > 0 )
            {
                uxShippingMethodListDiv.Visible = true;
                uxSelectShippingButton.Visible = true;
            }
            else
            {
                uxMessage.DisplayError( "[$NoShippingMethod]" );
                uxMessageDiv.Visible = true;
            }
        }
    }

    private void ExtractListItemValue(
        string listItemValue,
        out string shippingID,
        out decimal shippingCost,
        out decimal handlingFee )
    {
        string[] selectItem = listItemValue.Split( '-' );

        shippingID = selectItem[ 0 ];
        shippingCost = ConvertUtilities.ToDecimal( selectItem[ 1 ] );
        handlingFee = ConvertUtilities.ToDecimal( selectItem[ 2 ] );
    }

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if ( index > 0 )
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    private void SetShipping()
    {
        string shippingID;
        decimal shippingCost, handlingFee;

        ExtractListItemValue( uxShippingRadioList.SelectedValue, out shippingID, out shippingCost, out handlingFee );

        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
            StoreContext.Culture, shippingID );

        if ( shippingOption.ShippingOptionType.IsRealTime
            && StoreContext.ShoppingCart.ContainsRecurringProduct() )
        {
            //uxShippingDetails.DisplayRecurringWarningMessage();
            return;
        }

        ShippingChoice shippingChoice = new ShippingChoice(
            ExtractNameFromListItemText( uxShippingRadioList.SelectedItem.Text ), shippingCost, handlingFee );

        ShippingMethod shippingMethod = shippingOption.CreateShippingMethod( shippingChoice );
        StoreContext.CheckoutDetails.ShippingMethod = shippingMethod;
        StoreContext.CheckoutDetails.EstimatedShippingSelect = uxShippingRadioList.SelectedItem.Text;
    }

    #endregion

    #region protected
    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageNotSelectDiv.Visible = false;
        uxMessageDiv.Visible = false;
        uxShippingMethodListDiv.Visible = false;

        if ( !IsPostBack )
        {
            StoreContext.CheckoutDetails.EstimatedShippingSelect = String.Empty;
        }
    }

    protected void uxEstimateButton_Click( object sender, EventArgs e )
    {
        Address ads = new Address( "First Name", "Last Name", "None", "Address1", "Address1", "", uxCountryAndState.CurrentState, uxZip.Text, uxCountryAndState.CurrentCountry, "1234567", "1234567" );
        _shippingAddress = new ShippingAddress( ads, true );
        PopulateControls();
    }

    protected void uxSelectShippingButton_Click( object sender, EventArgs e )
    {
        if ( uxShippingRadioList.SelectedIndex >= 0 )
        {
            SetShipping();

            uxSelectShippingButton.Visible = false;
            uxShippingRadioList.Items.Clear();
        }
        else
        {
            uxNotSelectShippingMessage.Text = "[$NoSelectShipping]";
            uxMessageNotSelectDiv.Visible = true;
            uxShippingMethodListDiv.Visible = true;
        }
    }

    #endregion

    #region public

    public string GetEstimatedShippingOption()
    {
        if ( uxShippingRadioList.SelectedIndex == -1 )
            return String.Empty;
        else
            return uxShippingRadioList.SelectedItem.Text;
    }
    #endregion
}
