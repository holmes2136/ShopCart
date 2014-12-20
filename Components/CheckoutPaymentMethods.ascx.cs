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
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;
using System.Collections.Generic;

public partial class Components_CheckoutPaymentMethods : BaseLanguageUserControl
{

    #region Private

    protected bool IsRecurring( object name )
    {
        if (StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, name.ToString() );

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
                StoreContext.Culture, uxPaymentList.DataKeys[i].ToString() );

            if (paymentOption.CanUseRecurring)
                return (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
        }
        return null;
    }

    private void SetAutoPostBackRadioButton()
    {
        bool isHasPO = false;
        IList<PaymentOption> options = DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            StoreContext.Culture, BoolFilter.ShowTrue );
        {
            foreach (PaymentOption paymentOption in options)
            {
                if (paymentOption.Name == "Purchase Order")
                {
                    isHasPO = true;
                    break;
                }
            }

            if (isHasPO)
            {
                for (int i = 0; i < uxPaymentList.Items.Count; i++)
                {
                    RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
                    radio.AutoPostBack = true;
                }
            }
        }
    }

    private void PopulateLicenseAgreement()
    {
        if (IsPolicyAgreementEnabled() && (uxPaymentList.Items.Count > 0))
            uxPolicyAgreementDiv.Visible = true;

        string result = String.Empty;
        if (EmailTemplates.ReadTemplate( "PolicyAgreement.txt", out result ))
        {
            uxLicenseDiv.InnerHtml = result;
        }
    }

    private bool IsShowInThisStore( PaymentOption option )
    {
        IList<PaymentOptionStore> poStore = DataAccessContext.PaymentOptionRepository.GetAllPaymentOptionStoreByName( option.Name );
        foreach (PaymentOptionStore po in poStore)
        {
            if ((po.StoreID == StoreContext.CurrentStore.StoreID) && !po.IsEnabled)
                return false;
        }
        return true;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPOpanel.Visible = false;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateLicenseAgreement();

        if (!IsPostBack)
        {
            PopulateControls();
            SetAutoPostBackRadioButton();
            RestorePaymentMethod();
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

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "Purchase Order" );
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

    #region public

    public void RestorePaymentMethod()
    {
        PaymentOption paymentOption = StoreContext.CheckoutDetails.PaymentMethod.PaymentOption;
        if (paymentOption == null) return;
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            HiddenField nameHidden = (HiddenField) uxPaymentList.Items[i].FindControl( "uxPaymentNameHidden" );
            if (nameHidden.Value == paymentOption.Name)
            {
                RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
                radio.Checked = true;

                if (PaymentOption.IsCustomPayment( paymentOption.Name ))
                {
                    DropDownList drop = (DropDownList) uxPaymentList.Items[i].FindControl("uxDrop");
                    drop.SelectedValue = StoreContext.CheckoutDetails.PaymentMethod.SecondaryName;
                }
                else if (paymentOption.Name == "Purchase Order")
                {
                    uxPOpanel.Visible = true;
                    uxPONumberText.Text = StoreContext.CheckoutDetails.PaymentMethod.PONumber;
                }
                break;
            }
        }
    }

    public void PopulateControls()
    {
        IList<PaymentOption> options = DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            StoreContext.Culture, BoolFilter.ShowTrue );

        IList<PaymentOption> options_display = new List<PaymentOption>();
        int i = 0;
        foreach (PaymentOption option in options)
        {
            if (IsShowInThisStore( option ))
            {
                PaymentOption option_new = (PaymentOption) option.Clone();
                option_new.PaymentImage = "../" + option.PaymentImage;
                options_display.Add( option_new );
                i++;
            }
        }

        uxPaymentList.DataSource = options_display;
        uxPaymentList.DataBind();
    }

    public bool IsAnyPaymentSelected()
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

    public bool CheckNoPaymentOption()
    {
        if (uxPaymentList.Items.Count <= 0)
        {
            uxPOpanel.Visible = false;
            uxPolicyAgreementDiv.Visible = false;
            uxCheckoutInnerTitle.Visible = false;
            return true;
        }
        else
            return false;
    }

    public void DisplayError( string errorMessage )
    {
        uxMessage.Text = errorMessage;
        uxPaymentValidatorDiv.Visible = true;
    }
    public void DisplayPolicyAgreementError( string errorMessage )
    {
        uxPolicyAgreementMessage.Text = errorMessage;
        uxPolicyAgreementValidatorDiv.Visible = true;
    }
    public void DisplayPOError(string errorMessage)
    {
        uxPOMessage.Text = errorMessage;
        uxPOValidatorDiv.Visible = true;
    }
    public bool IsAgreeChecked()
    {
        return uxAgreeChecked.Checked;
    }

    public bool IsPolicyAgreementEnabled()
    {
        return DataAccessContext.Configurations.GetBoolValue( "IsPolicyAgreementEnabled" );
    }

    public string GetSelectedPaymentName()
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

    public bool IsPONumberEmpty( out string poNumber )
    {
        poNumber = String.Empty;
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "Purchase Order" );
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

    public string GetSecondaryPaymentName()
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

    public bool HasOnlyOneRecurringPayment()
    {
        int count = 0;

        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                StoreContext.Culture, uxPaymentList.DataKeys[i].ToString() );

            if (paymentOption.CanUseRecurring)
            {
                count++;
                if (count > 1)
                    return false;
            }
        }

        return count == 1;
    }

    public void SetRecurringPaymentMethod()
    {
        FindRecurringRadioButton().Checked = true;
    }

    public bool IsOnlyOnePaymentSelectable()
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

    public bool IsPONumberPayment()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "Purchase Order" );
        for (int i = 0; i < uxPaymentList.Items.Count; i++)
        {
            RadioButton radio = (RadioButton) uxPaymentList.Items[i].FindControl( "uxRadio" );
            if (radio.Checked)
            {
                HiddenField hiddenName = (HiddenField) uxPaymentList.Items[i].FindControl( "uxPaymentNameHidden" );
                if (hiddenName.Value.Equals( paymentOption.Name ))
                {
                    uxPOpanel.Visible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public DataList PaymentList
    {
        get
        {
            return uxPaymentList;
        }
    }

    #endregion
}
