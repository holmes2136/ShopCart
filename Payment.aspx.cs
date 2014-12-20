using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Payment : BaseProcessCheckoutPage
{
    #region Private
    private bool SetPaymentAndRedirect()
    {
        if (!uxPaymentMehods.IsAnyPaymentSelected())
        {
            uxPaymentMehods.DisplayError("[$ErrorNoPaymentSelected]");
            return false;
        }
        else if (uxPaymentMehods.IsPolicyAgreementEnabled() && !uxPaymentMehods.IsAgreeChecked())
        {
            uxPaymentMehods.DisplayPolicyAgreementError("[$ErrorNotCheckPolicyAgreement]");
            return false;
        }

        string paymentMethodName = uxPaymentMehods.GetSelectedPaymentName();
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, paymentMethodName);

        PaymentMethod paymentMethod = paymentOption.CreatePaymentMethod();
        paymentMethod.SecondaryName = uxPaymentMehods.GetSecondaryPaymentName();

        string poNumber = String.Empty;

        if (uxPaymentMehods.IsPONumberEmpty(out poNumber))
        {
            uxPaymentMehods.DisplayPOError("[$ErrorPONumberRequired]");
            return false;
        }

        paymentMethod.PONumber = poNumber;
        StoreContext.CheckoutDetails.SetPaymentMethod(paymentMethod);

        return true;
    }

    private bool HasCoupon()
    {
        return !String.IsNullOrEmpty(StoreContext.CheckoutDetails.Coupon.CouponID);
    }

    private bool HasGiftCertificate()
    {
        return !StoreContext.CheckoutDetails.GiftCertificate.IsNull;
    }

    private void Control_StoreCultureChanged(object sender, CultureEventArgs e)
    {
        uxPaymentMehods.PopulateControls();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler(Control_StoreCultureChanged);
    }

    #endregion


    #region Protected

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterStoreEvents();

        ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
        if (cart.Length == 0)
            Response.Redirect("Default.aspx");

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, StoreContext.CheckoutDetails.PaymentMethod.Name);

        if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            if (!paymentOption.PaymentMethodSelectionAllowed)
            {
                if (!(Request.QueryString["skiplogin"] == "true"))
                    Response.Redirect("OrderSummary.aspx");
                else
                    Response.Redirect("OrderSummary.aspx?skiplogin=true");
            }

            if (StoreContext.GetOrderAmount().Total <= 0)
            {
                string paymentMethodName = PaymentOption.NoPaymentName;
                string secondaryName = String.Empty;

                if (HasCoupon() || HasGiftCertificate())
                {
                    if (HasCoupon())
                        secondaryName += "Coupon";

                    if (HasGiftCertificate())
                    {
                        if (HasCoupon())
                            secondaryName += " / ";

                        secondaryName += "Gift Certificate";
                    }
                }
                PaymentOption zeroPaymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                    StoreContext.Culture, paymentMethodName);
                PaymentMethod paymentMethod = zeroPaymentOption.CreatePaymentMethod();
                paymentMethod.SecondaryName = secondaryName;
                StoreContext.CheckoutDetails.SetPaymentMethod(paymentMethod);

                if (!(Request.QueryString["skiplogin"] == "true"))
                    Response.Redirect("OrderSummary.aspx");
                else
                    Response.Redirect("OrderSummary.aspx?skiplogin=true");
            }

            this.ClientScript.RegisterClientScriptInclude("JSControls", "ClientScripts/controls.js");
        }

        if (!IsPostBack) 
        { 
            uxPaymentMehods.PopulateControls();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (uxPaymentMehods.CheckNoPaymentOption())
        {
            uxMessage.DisplayError( "[$NoPaymentOption]" );
            uxPaymentImageButton.Visible = false;
            return;
        }

        if (!uxPaymentMehods.IsAnyPaymentSelected())
        {
            return;
        }

        if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            if (uxPaymentMehods.IsOnlyOnePaymentSelectable())
            {
                DropDownList customDrop = (DropDownList)uxPaymentMehods.PaymentList.Items[0].FindControl("uxDrop");
                RadioButton uxRadio = (RadioButton)uxPaymentMehods.PaymentList.Items[0].FindControl("uxRadio");
                uxRadio.Checked = true;
                if (customDrop.Visible &&
                    customDrop.Items.Count == 2)
                {
                    customDrop.Items[1].Selected = true;
                }

                if (!uxPaymentMehods.IsPolicyAgreementEnabled() && !uxPaymentMehods.IsPONumberPayment())
                {
                    SetPaymentAndRedirect();
                }
            }
        }
        else
        {
            if (uxPaymentMehods.HasOnlyOneRecurringPayment())
            {
                uxPaymentMehods.SetRecurringPaymentMethod();

                if (!uxPaymentMehods.IsPolicyAgreementEnabled())
                {
                    SetPaymentAndRedirect();
                }
            }
        }

        if (StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            if (uxPaymentMehods.PaymentList.Items.Count > 1)
            {
                uxPaymentMehods.DisplayError("[$RecurringPaymentError]");
            }
        }
    }

    protected void uxPaymentImageButton_Click(object sender, EventArgs e)
    {
        bool isValid = SetPaymentAndRedirect();

        if (isValid)
        {
            if (!(Request.QueryString["skiplogin"] == "true"))
                Response.Redirect("DirectPaymentSale.aspx");
            else
                Response.Redirect("DirectPaymentSale.aspx?skiplogin=true");
        }
    }

    #endregion
}
