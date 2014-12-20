using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_Components_SiteConfig_DisplayConfig : System.Web.UI.UserControl
{
    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return string.Empty;
            else
                return (string)ViewState["ValidationGroup"];
        }

        set
        {
            ViewState["ValidationGroup"] = value;
        }
    }

    public void PopulateControls()
    {
        uxRetailPriceModeDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue("RetailPriceMode").ToString();
        uxShowSkuModeDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue("ShowSkuMode").ToString();
        uxDisplayOptionDrop.SelectedValue = DataAccessContext.Configurations.GetValue("IsDisplayOptionInProductList");
        uxIsDiscountAllCustomerDrop.SelectedValue = DataAccessContext.Configurations.GetValue("IsDiscountAllCustomer");

        uxSiteMapEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue("SiteMapEnabled");
        uxLogoInvoiceImageText.Text = DataAccessContext.Configurations.GetValue("LogoInvoiceImage");
        uxShippingAddDrop.SelectedValue = DataAccessContext.Configurations.GetValue("ShippingAddressMode").ToString();
        uxIgnoreProductFixedShippingDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue("RTShippingIgnoreFixedShippingCost");
        
        uxQuantityDiscountDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue("QuantityDiscountSeparateOption").ToString();

        uxHandlingFeeEnabledDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue("HandlingFeeEnabled");


        uxLanguageMenuDisplayDrop.SelectedValue = DataAccessContext.Configurations.GetValue("LanguageMenuDisplayMode");
        uxOutOfStockValueText.Text = DataAccessContext.Configurations.GetValue("OutOfStockValue");
        uxModeCatalogDrop.SelectedValue = DataAccessContext.Configurations.GetValue("IsCatalogMode");
        uxUseInventoryControlDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue("UseStockControl").ToString();
        uxDisplayRemainingQuantityDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue("ShowQuantity").ToString();

        PopulateSiteMapConfig();

        uxVevoPayConfigPanel.Visible = ConvertUtilities.ToBoolean(DataAccessContext.Configurations.GetValue("VevoPayPADSSMode"));
        uxPaymentSSLEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue("PaymentSSLEnabled");
        uxVevoPayPADSSModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue("VevoPayPADSSMode");
        uxWeightUnitDrop.SelectedValue = DataAccessContext.Configurations.GetValue("WeightUnit");
        uxAdminSSLDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue("EnableAdminSSL").ToString();
        uxCouponEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue("CouponEnabled");
        uxFaceBookLikeButtonDrop.SelectedValue = DataAccessContext.Configurations.GetValue("FBLikeButton");
        uxCustomerAutoApproveDrop.SelectedValue = DataAccessContext.Configurations.GetValue("CustomerAutoApprove");
        
        uxPaymentAppText.PopulateControls();
        uxPolicyAgreementControl.PopulateControls();
        uxCanonicalizationControl.PopulateControls();

    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LanguageMenuDisplayMode"],
            uxLanguageMenuDisplayDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UseStockControl"],
            uxUseInventoryControlDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ShowQuantity"],
            uxDisplayRemainingQuantityDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["OutOfStockValue"],
            uxOutOfStockValueText.Text);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsCatalogMode"],
            uxModeCatalogDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RetailPriceMode"],
            uxRetailPriceModeDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ShowSkuMode"],
            uxShowSkuModeDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ShippingAddressMode"],
            uxShippingAddDrop.SelectedValue.ToString());

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsDiscountAllCustomer"],
            uxIsDiscountAllCustomerDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsDisplayOptionInProductList"],
            uxDisplayOptionDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SiteMapEnabled"],
            uxSiteMapEnabledDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["SiteMapDisplayType"],
           uxSiteMapTypeDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LogoInvoiceImage"],
            uxLogoInvoiceImageText.Text);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["VevoPayPADSSMode"],
            uxVevoPayPADSSModeDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PaymentSSLEnabled"],
            uxPaymentSSLEnabledDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableAdminSSL"],
            uxAdminSSLDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["WeightUnit"],
            uxWeightUnitDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RTShippingIgnoreFixedShippingCost"],
            uxIgnoreProductFixedShippingDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["QuantityDiscountSeparateOption"],
            uxQuantityDiscountDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["HandlingFeeEnabled"],
            uxHandlingFeeEnabledDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CouponEnabled"],
            uxCouponEnabledDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FBLikeButton"],
            uxFaceBookLikeButtonDrop.SelectedValue);

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CustomerAutoApprove"],
            uxCustomerAutoApproveDrop.SelectedValue);

        uxPaymentAppText.Update();
        uxPolicyAgreementControl.Update();
        uxCanonicalizationControl.Update();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!WebConfiguration.AdminSSLDisabled)
        {
            uxAdminSSLAlertTR.Visible = false;
        }
        else
        {
            uxAdminSSLAlertTR.Visible = true;
        }
    }

    private void PopulateSiteMapConfig()
    {
        uxSiteMapTypeTR.Visible = ConvertUtilities.ToBoolean(uxSiteMapEnabledDrop.SelectedValue);
        uxSiteMapTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue("SiteMapDisplayType");
    }

    protected void uxSiteMapEnabledDrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateSiteMapConfig();
    }

    protected void uxVevoPayPADSSModeDrop_SelectedIndexChanged(object sender, EventArgs e)
    {        
        uxVevoPayConfigPanel.Visible = ConvertUtilities.ToBoolean(uxVevoPayPADSSModeDrop.SelectedValue);
    }
    public bool CouponEnable
    {
        get
        {
            return ConvertUtilities.ToBoolean(uxCouponEnabledDrop.SelectedValue);
        }
    }
}
