using System;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class DirectPaymentSale : BaseProcessCheckoutPage
{


    protected void Page_Load( object sender, EventArgs e )
    {
        PaymentMethod paymentMethod = StoreContext.CheckoutDetails.PaymentMethod;
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, paymentMethod.Name );

        if (!paymentOption.CreditCardRequired)
        {
            uxPaymentInfo.SetCheckoutBillingAddress();

            if (!IsAnonymousCheckout())
                Response.Redirect( "OrderSummary.aspx" );
            else
                Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
        }


        if (!IsPostBack)
        {
            uxPaymentInfo.PopulateControls( Master.Controls );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected bool IsAnonymousCheckout()
    {
        if (Request.QueryString["skiplogin"] == "true")
            return true;
        else
            return false;
    }
}
