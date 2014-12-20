using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.WebUI;

public partial class Admin_Components_Order_SelectPaymentList : AdminAdvancedBaseUserControl
{
    #region Private
    private Culture CurrentCulture
    {
        get { return DataAccessContext.CultureRepository.GetOne( CultureID ); }
    }

    private bool IsAnyPaymentSelected()
    {
        string paymentName = GetSelectedPaymentName();
        if (String.IsNullOrEmpty( paymentName ))
            return false;
        else
        {
            if (PaymentOption.IsCustomPayment( paymentName ))
            {
                string secondaryName = GetSecondaryPaymentName();
                if (String.IsNullOrEmpty( secondaryName ))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }
    }

    private bool IsPolicyAgreementEnabled()
    {
        return DataAccessContext.Configurations.GetBoolValue( "IsPolicyAgreementEnabled" );
    }

    protected bool IsRecurring( object name )
    {
        if (StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( CurrentCulture, name.ToString() );

            return paymentOption.CanUseRecurring;
        }
        return true;
    }

    private DropDownList GetCustomDropDown( int paymentIndex )
    {
        return (DropDownList) uxPaymentList.Items[paymentIndex].FindControl( "uxDrop" );
    }

    private string GetDropDownValue( int paymentIndex )
    {
        DropDownList dropDown = GetCustomDropDown( paymentIndex );
        return dropDown.SelectedValue;
    }

    private string GetSelectedPaymentName()
    {
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
            if (radio.Checked)
            {
                string name = uxPaymentList.DataKeys[i].ToString();
                return name;
            }
        }

        return String.Empty;
    }

    private string GetSecondaryPaymentName()
    {
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
            if (radio.Checked)
            {
                string name = uxPaymentList.DataKeys[i].ToString();

                if (PaymentOption.IsCustomPayment( name ))
                {
                    string dropDownValue = GetDropDownValue( i );
                    if (!String.IsNullOrEmpty( dropDownValue ))
                        return dropDownValue;
                    else
                        return String.Empty;
                }
            }
        }

        return String.Empty;
    }

    public PaymentMethod GetSelectedPaymentMethod()
    {
        string paymentMethodName = GetSelectedPaymentName();
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            CurrentCulture, paymentMethodName );

        PaymentMethod paymentMethod = paymentOption.CreatePaymentMethod();
        paymentMethod.SecondaryName = GetSecondaryPaymentName();

        string poNumber = String.Empty;

        if (IsPONumberEmpty( out poNumber ))
        {
            uxPaymentMethodMessageDiv.Visible = true;
            uxPaymentMethodMessageLabel.Text = "[$ErrorPONumberRequired]";
        }

        paymentMethod.PONumber = poNumber;

        return paymentMethod;
    }

    public bool SetPayment( out string errMsg )
    {
        errMsg = String.Empty;
        string paymentMethodName;
        PaymentMethod paymentMethod;
        if (StoreContext.GetOrderAmount().Total <= 0)
        {
            paymentMethodName = PaymentOption.NoPaymentName;
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
            paymentMethod = zeroPaymentOption.CreatePaymentMethod();
            paymentMethod.SecondaryName = secondaryName;
            StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

            return true;
        }


        if (!IsAnyPaymentSelected())
        {
            errMsg = "Please correct the following errors :";
            uxPaymentMethodMessageLabel.Text = "Please select payment method.";
            uxPaymentMethodMessageDiv.Visible = true;
            return false;
        }

        paymentMethodName = GetSelectedPaymentName();
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            CurrentCulture, paymentMethodName );

        paymentMethod = paymentOption.CreatePaymentMethod();
        paymentMethod.SecondaryName = GetSecondaryPaymentName();

        string poNumber = String.Empty;

        if (IsPONumberEmpty( out poNumber ))
        {
            errMsg = "Please input purchase order number.";
            uxPaymentMethodMessageLabel.Text = errMsg;
            uxPaymentMethodMessageDiv.Visible = true;
            return false;
        }

        paymentMethod.PONumber = poNumber;
        StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

        return true;
    }

    private void PopulateCustomDropDown( DropDownList dropDown )
    {
        dropDown.Visible = true;

        dropDown.Items.Clear();
        dropDown.Items.Add( new ListItem( "-- Select --", String.Empty ) );

        string[] customList = DataAccessContext.Configurations.GetValueList( "PaymentByCustomList" );
        foreach (string key in customList)
            dropDown.Items.Add( key.Trim() );
    }

    private RadioButton FindRecurringRadioButton()
    {
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                CurrentCulture, uxPaymentList.DataKeys[i].ToString() );

            if (paymentOption.CanUseRecurring)
                return (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
        }
        return null;
    }

    private bool HasOnlyOneRecurringPayment()
    {
        int count = 0;

        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                CurrentCulture, uxPaymentList.DataKeys[i].ToString() );

            if (paymentOption.CanUseRecurring)
            {
                count++;
                if (count > 1)
                    return false;
            }
        }

        return count == 1;
    }

    private void SetRecurringPaymentMethod()
    {
        FindRecurringRadioButton().Checked = true;
    }

    private bool IsOnlyOnePaymentSelectable()
    {
        if (uxPaymentList.Items.Count == 1)
        {
            DropDownList customDrop = GetCustomDropDown( 0 );
            if (customDrop.Visible &&
                customDrop.Items.Count != 2)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }

    private bool HasCoupon()
    {
        return !String.IsNullOrEmpty( StoreContext.CheckoutDetails.Coupon.CouponID );
    }

    private bool HasGiftCertificate()
    {
        return !StoreContext.CheckoutDetails.GiftCertificate.IsNull;
    }

    public void PopulateControls()
    {
        uxPaymentList.DataSource = DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            CurrentCulture, BoolFilter.ShowTrue );
        uxPaymentList.DataBind();
    }

    private void Control_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private bool IsPONumberPayment()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "Purchase Order" );
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
            if (radio.Checked)
            {
                uxPOpanel.Visible = true;
                return true;
            }
        }
        return false;
    }

    private bool IsPONumberEmpty( out string poNumber )
    {
        poNumber = String.Empty;
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( CurrentCulture, "Purchase Order" );
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
            if (radio.Checked)
            {
                if (uxPaymentList.DataKeys[i].ToString().Equals( paymentOption.Name ))
                {
                    uxPOpanel.Visible = true;
                    if (String.IsNullOrEmpty( uxPONumberText.Text ))
                    {
                        return true;
                    }
                    else
                    {
                        poNumber = uxPONumberText.Text;
                        return false;
                    }
                }
            }
        }
        return false;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPOpanel.Visible = false;
        uxPaymentMethodMessageDiv.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            if (!HasOnlyOneRecurringPayment())
            {
                uxMessage.DisplayError( "Some payment methods are disabled because they cannot " +
                    "be used with recurring payments of the product(s) in your shopping cart." );
            }
        }

        ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
        if (cart.Length == 0)
            MainContext.RedirectMainControl( "OrderList.ascx" );

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            CurrentCulture, StoreContext.CheckoutDetails.PaymentMethod.Name );

        if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
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
                    CurrentCulture, paymentMethodName );
                PaymentMethod paymentMethod = zeroPaymentOption.CreatePaymentMethod();
                paymentMethod.SecondaryName = secondaryName;
                StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

            }

        }
    }

    protected void uxRadio_DataBinding( object sender, EventArgs e )
    {
        RadioButton radio = (RadioButton) sender;

        string script = "SetUniqueRadioButton('.*uxPaymentList.*PaymentGroup',this)";

        radio.Attributes.Add( "onclick", script );
    }

    protected void uxRadio_CheckedChanged( object sender, EventArgs e )
    {
        uxPOpanel.Visible = false;

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( CurrentCulture, "Purchase Order" );
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );

            if (radio.Checked)
            {
                HiddenField hiddenName = (HiddenField) uxPaymentList.Items[i].FindControl( "uxPaymentNameHidden" );
                if (hiddenName.Value.Equals( paymentOption.Name ))
                {
                    uxPOpanel.Visible = true;
                }
            }
        }
    }


    protected bool IsStringEmpty( object text )
    {
        return !String.IsNullOrEmpty( text.ToString().Trim() );
    }

    protected void uxPaymentList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        string paymentName = DataBinder.Eval( e.Item.DataItem, "Name" ).ToString();

        if (PaymentOption.IsCustomPayment( paymentName )
            && !StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            DropDownList dropDown = (DropDownList) e.Item.FindControl( "uxDrop" );

            PopulateCustomDropDown( dropDown );
        }
    }

    #endregion


    #region Public

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
    #endregion
}
