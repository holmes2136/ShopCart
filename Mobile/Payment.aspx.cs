using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain.Payments;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Orders;

public partial class Mobile_Payment : Vevo.WebUI.Orders.BaseProcessCheckoutPage
{
    #region Private

    //private bool IsAnyPaymentSelected()
    //{
    //    string paymentName = GetSelectedPaymentName();
    //    if (String.IsNullOrEmpty( paymentName ))
    //        return false;
    //    else
    //    {
    //        if (PaymentOption.IsCustomPayment( paymentName ))
    //        {
    //            //string secondaryName = GetSecondaryPaymentName();
    //            if (String.IsNullOrEmpty( secondaryName ))
    //                return false;
    //            else
    //                return true;
    //        }
    //        else
    //            return true;
    //    }
    //}

    private bool IsPolicyAgreementEnabled()
    {
        return DataAccessContext.Configurations.GetBoolValue( "IsPolicyAgreementEnabled" );
    }

    //protected bool IsRecurring( object name )
    //{
    //    if (StoreContext.ShoppingCart.ContainsRecurringProduct())
    //    {
    //        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, name.ToString() );

    //        return paymentOption.CanUseRecurring;
    //    }
    //    return true;
    //}

    //private DropDownList GetCustomDropDown( int paymentIndex )
    //{
    //    //return (DropDownList) uxPaymentList.Items[paymentIndex].FindControl( "uxDrop" );
    //}

    //private string GetDropDownValue( int paymentIndex )
    //{
    //    DropDownList dropDown = GetCustomDropDown( paymentIndex );
    //    return dropDown.SelectedValue;
    //}

    //private string GetSelectedPaymentName()
    //{
    //    for (int i = 0; i < uxPaymentList.Items.Count; i++)
    //    {
    //        RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
    //        if (radio.Checked)
    //        {
    //            string name = uxPaymentList.DataKeys[i].ToString();
    //            return name;
    //        }
    //    }

    //    return String.Empty;
    //}

    //private string GetSecondaryPaymentName()
    //{
    //    for (int i = 0; i < uxPaymentList.Items.Count; i++)
    //    {
    //        RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
    //        if (radio.Checked)
    //        {
    //            string name = uxPaymentList.DataKeys[i].ToString();

    //            if (PaymentOption.IsCustomPayment( name ))
    //            {
    //                string dropDownValue = GetDropDownValue( i );
    //                if (!String.IsNullOrEmpty( dropDownValue ))
    //                    return dropDownValue;
    //                else
    //                    return String.Empty;
    //            }
    //        }
    //    }

    //    return String.Empty;
    //}

    private void SetPaymentAndRedirect()
    {
        //if (!IsAnyPaymentSelected())
        //{
        //    uxValidateMessage.DisplayError( "[$ErrorNoPaymentSelected]" );
        //    return;
        //}
        //else if (!uxAgreeChecked.Checked && IsPolicyAgreementEnabled())
        //{
        //    uxValidateMessage.DisplayError( "[$ErrorNotCheckPolicyAgreement]" );
        //    return;
        //}

        //string paymentMethodName = GetSelectedPaymentName();
        //PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
        //    StoreContext.Culture, paymentMethodName );
        //PaymentMethod paymentMethod = paymentOption.CreatePaymentMethod();
        //paymentMethod.SecondaryName = GetSecondaryPaymentName();

        //StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

        if (!(Request.QueryString["skiplogin"] == "true"))
            Response.Redirect( "OrderSummary.aspx" );
        else
            Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
    }

    //private void PopulateCustomDropDown( DropDownList dropDown )
    //{
    //    dropDown.Visible = true;

    //    dropDown.Items.Clear();
    //    dropDown.Items.Add( new ListItem( "-- Select --", String.Empty ) );

    //    string[] customList = DataAccessContext.Configurations.GetValueList( "PaymentByCustomList" );
    //    foreach (string key in customList)
    //        dropDown.Items.Add( key.Trim() );
    //}

    //private RadioButton FindRecurringRadioButton()
    //{
    //    for (int i = 0; i < uxPaymentList.Items.Count; i++)
    //    {
    //        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
    //            StoreContext.Culture, uxPaymentList.DataKeys[i].ToString() );

    //        if (paymentOption.CanUseRecurring)
    //            return (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
    //    }
    //    return null;
    //}

    //private bool HasOnlyOneRecurringPayment()
    //{
    //    int count = 0;

    //    for (int i = 0; i < uxPaymentList.Items.Count; i++)
    //    {
    //        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
    //            StoreContext.Culture, uxPaymentList.DataKeys[i].ToString() );

    //        if (paymentOption.CanUseRecurring)
    //        {
    //            count++;
    //            if (count > 1)
    //                return false;
    //        }
    //    }

    //    return count == 1;
    //}

    private void SetRecurringPaymentMethod()
    {
        //FindRecurringRadioButton().Checked = true;
    }

    //private bool IsOnlyOnePaymentSelectable()
    //{
    //    if (uxPaymentList.Items.Count == 1)
    //    {
    //        DropDownList customDrop = GetCustomDropDown( 0 );
    //        if (customDrop.Visible &&
    //            customDrop.Items.Count != 2)
    //            return false;
    //        else
    //            return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private bool HasCoupon()
    {
        return !String.IsNullOrEmpty( StoreContext.CheckoutDetails.Coupon.CouponID );
    }

    private bool HasGiftCertificate()
    {
        return !StoreContext.CheckoutDetails.GiftCertificate.IsNull;
    }

    //private void PopulateControls()
    //{
    //    uxPaymentList.DataSource = DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
    //        StoreContext.Culture, BoolFilter.ShowTrue );
    //    uxPaymentList.DataBind();
    //}

    private void Control_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        //PopulateControls();
    }

    private void RegisterStoreEvents()
    {
        //GetStorefrontEvents().StoreCultureChanged +=
        //    new StorefrontEvents.CultureEventHandler( Control_StoreCultureChanged );
    }

    private void PopulateLicenseAgreement()
    {
        if (IsPolicyAgreementEnabled())
            uxPolicyAgreementDiv.Visible = true;

        string result = String.Empty;
        if (EmailTemplates.ReadTemplate( "PolicyAgreement.txt", out result ))
        {
            uxLicenseDiv.InnerHtml = result;
        }
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        //RegisterStoreEvents();

        if (!IsPostBack)
        {
            PopulateLicenseAgreement();
        }

        ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
        if (cart.Length == 0)
            Response.Redirect( "Default.aspx" );

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, StoreContext.CheckoutDetails.PaymentMethod.Name );

        if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            if (!paymentOption.PaymentMethodSelectionAllowed)
            {
                if (!(Request.QueryString["skiplogin"] == "true"))
                    Response.Redirect( "OrderSummary.aspx" );
                else
                    Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
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
                    StoreContext.Culture, paymentMethodName );
                PaymentMethod paymentMethod = zeroPaymentOption.CreatePaymentMethod();
                paymentMethod.SecondaryName = secondaryName;
                StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

                if (!(Request.QueryString["skiplogin"] == "true"))
                    Response.Redirect( "OrderSummary.aspx" );
                else
                    Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
            }

            this.ClientScript.RegisterClientScriptInclude( "JSControls", "ClientScripts/controls.js" );
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        //if (!IsPostBack)
        //    PopulateControls();

        //if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        //{
        //    //if (IsOnlyOnePaymentSelectable())
        //    //{
        //    //    //DropDownList customDrop = (DropDownList) uxPaymentList.Items[0].FindControl( "uxDrop" );
        //    //    //RadioButton uxRadio = (RadioButton) uxPaymentList.Items[0].FindControl( "uxRadio" );
        //    //    //uxRadio.Checked = true;
        //    //    //if (customDrop.Visible &&
        //    //    //    customDrop.Items.Count == 2)
        //    //    //{
        //    //    //    customDrop.Items[1].Selected = true;
        //    //    //}

        //    //    if (!IsPolicyAgreementEnabled())
        //    //    {
        //    //        SetPaymentAndRedirect();
        //    //    }
        //    //}
        //}
        //else
        //{
        //    //if (HasOnlyOneRecurringPayment())
        //    //{
        //    //    SetRecurringPaymentMethod();

        //    //    if (!IsPolicyAgreementEnabled())
        //    //    {
        //    //        SetPaymentAndRedirect();
        //    //    }
        //    //}
        //}

        //if (StoreContext.ShoppingCart.ContainsRecurringProduct())
        //{
        //    if (uxPaymentList.Items.Count > 1)
        //    {
        //        uxMessage.FontBold = false;
        //        uxMessage.DisplayError( "[$RecurringPaymentError]" );
        //    }
        //}
    }

    //protected void uxRadio_DataBinding( object sender, EventArgs e )
    //{
    //    RadioButton radio = (RadioButton) sender;

    //    string script = "SetUniqueRadioButton('.*uxPaymentList.*PaymentGroup',this)";

    //    radio.Attributes.Add( "onclick", script );
    //}

    protected void uxPaymentImageButton_Click( object sender, ImageClickEventArgs e )
    {
        SetPaymentAndRedirect();
    }

    //protected bool IsStringEmpty( object text )
    //{
    //    return !String.IsNullOrEmpty( text.ToString().Trim() );
    //}

    //protected void uxPaymentList_ItemDataBound( object sender, DataListItemEventArgs e )
    //{
    //    string paymentName = DataBinder.Eval( e.Item.DataItem, "Name" ).ToString();

    //    if (PaymentOption.IsCustomPayment( paymentName )
    //        && !StoreContext.ShoppingCart.ContainsRecurringProduct())
    //    {
    //        DropDownList dropDown = (DropDownList) e.Item.FindControl( "uxDrop" );

    //        PopulateCustomDropDown( dropDown );
    //    }
    //}

    #endregion
}
