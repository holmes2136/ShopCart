using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_CurrencySelector : AdminAdvancedBaseUserControl
{
    private string LastSelectedCurrency
    {
        get
        {
            if (ViewState["LastSelectedCurrency"] == null)
                ViewState["LastSelectedCurrency"] = DataAccessContext.Configurations.GetValue( "PaymentCurrency" );

            return (string) ViewState["LastSelectedCurrency"];
        }
        set
        {
            ViewState["LastSelectedCurrency"] = value;
        }
    }

    private void SetListToNone()
    {
        uxCurrencyDrop.Items.Clear();
        uxCurrencyDrop.Items.Add( new ListItem( "(None)", "" ) );
    }

    private IList<PaymentOption> GetPaymentMethodList()
    {
        IList<PaymentOption> paymentMethodList = DataAccessContext.PaymentOptionRepository.GetAll(
            AdminUtilities.CurrentCulture, BoolFilter.ShowTrue );

        IList<PaymentOption> list = ListUtilities<PaymentOption>.CopyList( paymentMethodList );

        // *** away add current payment method object although it's not enable 
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( AdminUtilities.CurrentCulture, CurrentPaymentName );

        if (!paymentOption.IsEnabled)
            list.Add( paymentOption );

        return list;
    }

    private GatewayCurrency GetValidCurrencies()
    {
        GatewayCurrency gateways = new GatewayCurrency( Server.MapPath( "~/Gateway/GatewayCurrencyList.xml" ) );

        IList<PaymentOption> paymentMethodList = GetPaymentMethodList();

        for (int index = 0; index < paymentMethodList.Count; index++)
        {
            if (paymentMethodList[index].Name == CurrentPaymentName)
            {
                if (IsPaymentEnabled)
                {
                    string[] currencies = paymentMethodList[index].Currencies;

                    if (currencies.Length > 0)
                        gateways.AddGateway( paymentMethodList[index].Name, currencies );
                }
            }
            else
            {
                string[] currencies = paymentMethodList[index].Currencies;

                if (currencies.Length > 0)
                    gateways.AddGateway( paymentMethodList[index].Name, currencies );
            }
        }

        return gateways;
    }

    private void RefreshWarning()
    {
        uxWarningLabel.Visible = CurrencyUtilities.IsWarningPaymentCurrency( LastSelectedCurrency );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshWarning();
    }

    protected void uxCurrencyDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        LastSelectedCurrency = uxCurrencyDrop.SelectedValue;
        RefreshDropDownList();
        
        // Send event to parent controls
        OnBubbleEvent( e );
    }


    public string CurrentPaymentName
    {
        get
        {
            if (ViewState["CurrentPaymentName"] == null)
                return "";
            else
                return ViewState["CurrentPaymentName"].ToString();
        }
        set { ViewState["CurrentPaymentName"] = value; }
    }

    public string SelectedValue
    {
        get
        {
            return uxCurrencyDrop.SelectedValue;
        }
        set
        {
            uxCurrencyDrop.SelectedValue = value;
        }
    }

    public bool IsPaymentEnabled
    {
        get
        {
            if (ViewState["IsPaymentEnabled"] == null)
                return false;
            else
                return Convert.ToBoolean( ViewState["IsPaymentEnabled"] );
        }
        set { ViewState["IsPaymentEnabled"] = value; }
    }

    public void RefreshDropDownList()
    {
        GatewayCurrency gateways = GetValidCurrencies();

        IList<Vevo.Domain.Currency> currencyList = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );

        Vevo.Domain.Payments.PaymentCurrency[] currencies = gateways.GetValidCurrencies();
        if (currencies.Length > 0)
        {
            uxCurrencyDrop.Items.Clear();
            foreach (Vevo.Domain.Payments.PaymentCurrency item in currencies)
            {
                ListItem customListItem = new ListItem( item.DisplayName, item.CurrencyCode );
                string attributes = String.Empty;
                if (item.CurrencyCode == DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ))
                    attributes += "font-weight:bold;";

                if (!CurrencyInList( currencyList, item.CurrencyCode ))
                    attributes += "color:#E6E6E6;";

                customListItem.Attributes.Add( "style", attributes );
                uxCurrencyDrop.Items.Add( customListItem );
            }

            if (uxCurrencyDrop.Items.FindByValue( LastSelectedCurrency ) != null)
                uxCurrencyDrop.SelectedValue = LastSelectedCurrency;
        }
        else
        {
            SetListToNone();
        }
    }

    private bool CurrencyInList( IList<Vevo.Domain.Currency> currencyList, String currencyCode )
    {
        for (int i = 0; i < currencyList.Count; i++)
        {
            if (currencyList[i].CurrencyCode == currencyCode)
                return true;
        }
        return false;
    }
}
