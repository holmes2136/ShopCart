using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo.Domain.Payments;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class Admin_MainControls_OrderCreatePaymentDetails : AdminAdvancedBaseUserControl
{
    private string SelectedStoreID
    {
        get
        {
            if (MainContext.QueryString["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return MainContext.QueryString["StoreID"];
        }
    }

    public string CurrencyCode
    {
        get
        {
            if (MainContext.QueryString["CurrencyCode"] == null)
                return DataAccessContext.CurrencyRepository.GetOne(
                    DataAccessContext.Configurations.GetValueNoThrow(
                    "DefaultDisplayCurrencyCode", DataAccessContext.StoreRepository.GetOne( SelectedStoreID ) ) ).CurrencyCode;
            else
                return MainContext.QueryString["CurrencyCode"];
        }
    }

    private bool IsPaymentFailure
    {
        get
        {
            if (MainContext.QueryString["PaymentFailure"] == null)
                return false;
            else
                return ConvertUtilities.ToBoolean( MainContext.QueryString["PaymentFailure"] );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsPaymentFailure)
        {
            uxPaymentFailDetailsPanel.Visible = true;
            uxTitle.Text = CheckoutNotCompletePage.TitlePage;
            uxMsgLabel.Text = WebUtilities.ReplaceNewLine( CheckoutNotCompletePage.DescriptionPage );
            uxGotoPageLink.Text = CheckoutNotCompletePage.LinkName;
            uxGotoPageLink.PageName = CheckoutNotCompletePage.PageName;
            uxGotoPageLink.PageQueryString = CheckoutNotCompletePage.QueryString;
            uxPaymentDetails.Visible = false;
        }

        PaymentMethod paymentMethod = StoreContext.CheckoutDetails.PaymentMethod;
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, paymentMethod.Name );

        if (!paymentOption.CreditCardRequired)
        {
            MainContext.RedirectMainControl( "OrderCreateCheckOutSummary.ascx", 
                String.Format( "StoreID={0}&CurrencyCode={1}", SelectedStoreID, CurrencyCode ) );
            return;
        }

      
    }
}
