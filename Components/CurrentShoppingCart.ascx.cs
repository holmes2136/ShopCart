using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.WebAppLib;
using Vevo.WebUI;
using System.Collections.Generic;
using Vevo.Domain.Payments;

public partial class Components_CurrentShoppingCart : Vevo.WebUI.BaseControls.BaseLayoutControl
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

    private void PopulateControls()
    {
        IList<PaymentOption> paymentsWithoutButton = GetPaymentsWithoutButton();
        if (paymentsWithoutButton.Count == 0)
        {
            uxCheckOutButton.Visible = false;
        }

        uxAmountLabel.Text = StoreContext.Currency.FormatPrice(
            GetShoppingCartTotal() );

        uxQuantityLabel.Text = StoreContext.ShoppingCart.GetCurrentQuantity().ToString();

        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            OrderCalculator orderCalculator = new OrderCalculator();
            decimal discount = orderCalculator.GetPreCheckoutDiscount(
                StoreContext.Customer,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.CheckoutDetails.Coupon );

            uxDiscountTR.Visible = true;
            uxDiscountLabel.Text = StoreContext.Currency.FormatPrice( discount * -1 );
        }
        else
            uxDiscountTR.Visible = false;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
      
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "MiniCartModuleDisplay" ))
        {
            PopulateControls();
        }
        else
        {
            this.Visible = false;
        }

    }

    protected void uxCheckOutButton_Click( object sender, EventArgs e )
    {
        if (Page.User.Identity.IsAuthenticated &&
            !Roles.IsUserInRole( Page.User.Identity.Name, "Customers" ))
            FormsAuthentication.SignOut();

        Response.Redirect( "~/CheckOut.aspx" );

    }
}