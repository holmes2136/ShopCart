using System;
using System.Web.Security;
using System.Web.UI;
using PayPal.Payments.DataObjects;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Payments.PayPalProExpress;
using Vevo.Domain.Shipping;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class ShippingPage : BaseProcessCheckoutPage
{
    private bool IsAnonymousCheckout()
    {
        if ( Request.QueryString[ "skiplogin" ] == "true" && !Roles.IsUserInRole( "Customers" ) )
        {
            return true;
        }
        else
        {
            return false;
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

    private void HideButtons()
    {
        uxNextImageButton.Visible = false;
        uxCancelImageButton.Visible = false;
    }

    private void PopulateControls()
    {
        if(uxShippingDetails.ShippingOptionList.Items.Count == 0)
            uxShippingDetails.PopulateControls();
        
        DisplayEmptyShoppingCart();
        DisplayNoShippingOption();
    }

    private void DisplayEmptyShoppingCart()
    {
        if ( StoreContext.ShoppingCart.GetCartItems().Length <= 0 )
        {
            uxMessage.DisplayError( uxShippingDetails.DisplayEmptyShoppingCart() );
            HideButtons();
        }
    }

    private void DisplayNoShippingOption()
    {
        if ( uxShippingDetails.ShippingOptionList.Items.Count == 0 )
        {
            uxMessage.DisplayError( uxShippingDetails.DisplayNoShippingOption() );
            HideButtons();
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

    private void SetShippingAndRedirect()
    {
        string shippingID;
        decimal shippingCost, handlingFee;
        ExtractListItemValue( uxShippingDetails.ShippingOptionList.SelectedValue,
            out shippingID, out shippingCost, out handlingFee );

        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
            StoreContext.Culture, shippingID );

        if ( shippingOption.ShippingOptionType.IsRealTime
            && StoreContext.ShoppingCart.ContainsRecurringProduct() )
        {
            uxShippingDetails.DisplayRecurringWarningMessage();
            return;
        }

        ShippingChoice shippingChoice = new ShippingChoice(
            ExtractNameFromListItemText( uxShippingDetails.ShippingOptionList.SelectedItem.Text ), shippingCost, handlingFee );

        ShippingMethod shippingMethod = shippingOption.CreateShippingMethod( shippingChoice );
        StoreContext.CheckoutDetails.ShippingMethod = shippingMethod;

        if ( !( Request.QueryString[ "skiplogin" ] == "true" ) )
            Response.Redirect( "Payment.aspx" );
        else
            Response.Redirect( "Payment.aspx?skiplogin=true" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if ( Request.QueryString[ "Token" ] != null )
        {
            PayPalProExpressPaymentMethod payment = ( PayPalProExpressPaymentMethod )
                StoreContext.CheckoutDetails.PaymentMethod;

            if ( !payment.ProcessPostLoginDetails( Request.QueryString[ "Token" ], StoreContext.CheckoutDetails ) )
            {
                CheckoutNotCompletePage.RedirectToPage(
                "Error Message",
                payment.GetErrorMessage(),
                "ShoppingCart.aspx",
                "Return To Shopping" );
            }
        }
        else
        {
            if (DataAccessContext.Configurations.GetValue( "CheckoutMode" ).ToString() != "Normal" )
            {
                if ( !IsAnonymousCheckout() )
                    Response.Redirect( "OnePageCheckout.aspx" );
                else
                    Response.Redirect( "OnePageCheckout.aspx?skiplogin=true" );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( ( StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem() ) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ) )
        {
            StoreContext.CheckoutDetails.SetShipping( ShippingMethod.Null );
            if ( !( Request.QueryString[ "skiplogin" ] == "true" ) )
                Response.Redirect( "Payment.aspx" );
            else
                Response.Redirect( "Payment.aspx?skiplogin=true" );
        }

        PopulateControls();
    }

    protected void uxNextImageButton_Click( object sender, EventArgs e )
    {
        SetShippingAndRedirect();
    }

    protected void uxCancelImageButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "ShoppingCart.aspx" );
    }
}
