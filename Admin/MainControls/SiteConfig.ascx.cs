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
using System.IO;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.Domain.Configurations;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_SiteConfig : AdminAdvancedBaseUserControl
{
    private const string _pathUpload = "Images/Configuration/";

    protected class ConfigList
    {
        private string _configName;
        private string _displayName;

        public string ConfigName { get { return _configName; } set { _configName = value; } }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }

        public ConfigList( string configNameParam, string displayNameParam )
        {
            ConfigName = configNameParam;
            DisplayName = displayNameParam;
        }
    }

    private ConfigList[] _layoutConfigList = new ConfigList[]
        {
            new ConfigList( "Test", "Testtest" ),
            new ConfigList( "Test", "Testtest2" ),
            new ConfigList( "Test", "Testtest3" )
        };

    private void PopulateStoreConfig()
    {
    }

    private void PopulateWebServiceConfig()
    {
        uxWebServiceAdminUserText.Text = DataAccessContext.Configurations.GetValue( "WebServiceAdminUser" );
    }

    private void PopulateCustomTimeZone()
    {
        uxUseTimeZoneDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "UseCustomTimeZone" );
        uxCustomTimeZoneDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CustomTimeZone" );
    }

    private void PopulateControls()
    {
        uxTaxConfig.PopulateControls();
        uxProductImageSizeConfig.PopulateControls();
        uxDisplay.PopulateControls();
        uxRatingReview.PopulateControls();
        uxGiftCertificate.PopulateControls();
        uxWholesale.PopulateControls();
        uxEmailNotification.PopulateControls();
        uxUploadConfig.PopulateControls();
        uxSeo.PopulateControls();
        uxSystemConfig.PopulateControls();
        uxAffiliateConfig.PopulateControls();
        uxShippingTracking.PopulateControls();
        uxDownloadCount.PopulateControls();
        /*-------- Original Code -------------*/
        PopulateStoreConfig();
        PopulateCustomTimeZone();
        PopulateWebServiceConfig();

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxAffiliateTab.Visible = false;
            uxeBayConfigTab.Visible = false;

            uxWebServiceSettingLabel.Visible = false;
            uxWebServiceSettingTR.Visible = false;
        }
    }

    private void SetUpUrlCultureID()
    {
        DataAccessHelper.UrlCultureID = CultureUtilities.UrlCultureID;
    }

    private void PopulateSymbolDropdown()
    {
        CurrencyUtilities.BaseCurrencyCode = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );

        uxSymbolDrop.DataSource = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
        uxSymbolDrop.DataBind();

        uxSymbolDrop.SelectedValue = CurrencyUtilities.BaseCurrencyCode;
    }

    private void PopulateDropdownControl()
    {
        uxMerchantCountryList.CurrentSelected = DataAccessContext.Configurations.GetValue( "ShippingMerchantCountry" );
        PopulateSymbolDropdown();

        uxShippingIncludeDiscountDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ShippingIncludeDiscount" );
    }

    private void SetDefaultCurrency()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["BaseWebsiteCurrency"],
            uxSymbolDrop.SelectedValue );

        CurrencyUtilities.BaseCurrencyCode = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );
        Currency currency = DataAccessContext.CurrencyRepository.GetOne( uxSymbolDrop.SelectedValue );
        currency.ConversionRate = 1;
        DataAccessContext.CurrencyRepository.Save( currency, uxSymbolDrop.SelectedValue );

    }

    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxSearchText, uxSearchButton );
    }

    private string FindSearchText()
    {
        return uxSearchText.Text;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterSubmitButton();

        if (!MainContext.IsPostBack)
            PopulateDropdownControl();

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            uxDeleteConfirmButton.TargetControlID = "uxDeleteFileTempButton";
            uxConfirmModalPopup.TargetControlID = "uxDeleteFileTempButton";
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyDeleteFileTempButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyDeleteFileTempButton";
        }
        if (!IsAdminModifiable())
        {
            uxSiteMaintenancePanel.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
            uxSearchText.Text = "";
            uxDownloadExpirePeriodText.Text = DataAccessContext.Configurations.GetValue( "DownloadExpirePeriod" );
            uxAllowAnonymousCheckoutDropDown.SelectedValue = DataAccessContext.Configurations.GetValue( "AnonymousCheckoutAllowed" );
        }

        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            uxTaxConfig.Update();
            uxProductImageSizeConfig.Update();
            uxDisplay.Update();
            uxRatingReview.Update();
            uxGiftCertificate.Update();
            uxWholesale.Update();
            uxEmailNotification.Update();
            uxUploadConfig.Update();
            uxSeo.Update();
            uxSystemConfig.Update();
            uxAffiliateConfig.Update();
            uxShippingTracking.Update();
            uxDownloadCount.Update();
            uxeBayConfig.Update();

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["UseCustomTimeZone"],
                uxUseTimeZoneDrop.SelectedValue );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["CustomTimeZone"],
                uxCustomTimeZoneDrop.SelectedValue );


            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["WebServiceAdminUser"],
                uxWebServiceAdminUserText.Text );

            AdminUtilities.LoadSystemConfig();
            AdminUtilities.ClearCurrencyCache();

            uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );

            SiteMapManager.ClearCache();
            PopulateControls();

            SetUpUrlCultureID();

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["DownloadExpirePeriod"],
                uxDownloadExpirePeriodText.Text );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["AnonymousCheckoutAllowed"],
                uxAllowAnonymousCheckoutDropDown.SelectedValue );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["ShippingMerchantCountry"],
                uxMerchantCountryList.CurrentSelected );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["ShippingIncludeDiscount"],
                uxShippingIncludeDiscountDrop.SelectedValue );

            SetDefaultCurrency();
            SystemConfig.Load();

            UpdatePanel headerPanel = (UpdatePanel) WebUtilities.FindControlRecursive( this.Page, "uxHeaderUpdatePanel" );
            if (headerPanel != null)
            {
                Control paymentLink = (Control) WebUtilities.FindControlRecursive( headerPanel, "PaymentLink" );
                if (paymentLink != null)
                {
                    paymentLink.Visible = DataAccessContext.Configurations.GetBoolValue( "VevoPayPADSSMode" );
                    headerPanel.Update();
                }
            }

        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxDeleteFileTempButton_Click( object sender, EventArgs e )
    {
        string[] files = Directory.GetFiles( Server.MapPath( "../" + SystemConst.OptionFileTempPath ) );
        try
        {
            foreach (string filetmp in files)
                File.Delete( filetmp );
            uxMessage.DisplayMessage( Resources.SiteConfigMessages.DeleteAllTempFileSuccess );
        }
        catch
        {
            uxMessage.DisplayError( Resources.SiteConfigMessages.DeleteAllTempFileError );
        }
    }

    protected void uxDeleteSearchButton_Click( object sender, EventArgs e )
    {
        try
        {
            SearchLogAccess.DeleteAll();
            uxMessage.DisplayMessage( Resources.SiteConfigMessages.DeleteAllSearchSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( Resources.SiteConfigMessages.DeleteAllSearchError + " " + ex.Message );
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        if (FindSearchText() != "")
            MainContext.RedirectMainControl( "SearchConfigurationResult.ascx",
                String.Format( "Search={0}&Store={1}&RedirectURL={2}", FindSearchText(), "0", MainContext.LastControl ) );
    }
}
