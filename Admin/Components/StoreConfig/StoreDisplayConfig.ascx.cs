using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;

public partial class Admin_Components_StoreConfig_StoreDisplayConfig : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private string LanguageID
    {
        get
        {
            return AdminConfig.CurrentContentCultureID;
        }
    }

    private void SetUpRootCategoryDropDown()
    {
        uxRootCategoryDrop.Items.Clear();

        Culture culture = DataAccessContext.CultureRepository.GetOne( LanguageID );
        IList<Category> rootCategoryList =
            DataAccessContext.CategoryRepository.GetRootCategory( culture, "CategoryID", BoolFilter.ShowAll );

        foreach (Category rootCategory in rootCategoryList)
        {
            uxRootCategoryDrop.Items.Add( new ListItem( rootCategory.Name, rootCategory.CategoryID ) );
        }

        uxRootCategoryDrop.SelectedValue = DataAccessContext.Configurations.GetValueNoThrow( "RootCategory", CurrentStore );
    }

    private void SetUpRootDepartmentDropDown()
    {
        uxRootDepartmentDrop.Items.Clear();

        Culture culture = DataAccessContext.CultureRepository.GetOne( LanguageID );
        IList<Department> rootDepartmentList =
            DataAccessContext.DepartmentRepository.GetRootDepartment( culture, "DepartmentID", BoolFilter.ShowAll );

        if (rootDepartmentList.Count > 0)
        {
            foreach (Department rootDepartment in rootDepartmentList)
            {
                uxRootDepartmentDrop.Items.Add( new ListItem( rootDepartment.Name, rootDepartment.DepartmentID ) );
            }
            uxRootDepartmentDrop.SelectedValue = DataAccessContext.Configurations.GetValueNoThrow( "RootDepartment", CurrentStore );
        }

        else
        {
            if (KeyUtilities.IsMultistoreLicense())
                uxRootDepartmentDrop.Items.Add( new ListItem( "None", "0" ) );
            else
                uxRootDepartmentDrop.Items.Add( new ListItem( "None", "1" ) );

            uxRootDepartmentDrop.Enabled = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (MainContext.IsPostBack)
        {
            uxThemeSelect.PopulateControls();
            uxMobileThemeSelect.PopulateControls();
            uxCategorySelect.PopulateControls();
            uxDepartmentSelect.PopulateControls();
            uxProductListSelect.PopulateControls();
            uxProductDetailsSelect.PopulateControls();
            uxGoogleAnalyticsConfig.PopulateControls(CurrentStore);
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControls();
    }

    public void PopulateControls()
    {
        uxSiteName.CultureID = LanguageID;
        uxSiteName.PopulateControl();
        uxLogoImage.PopulateControls();
        uxDefaultwebsiteLanguage.PopulateControls();

        uxStoreDefaultCountryDropDown.CurrentSelected
            = DataAccessContext.Configurations.GetValue( "StoreDefaultCountry", CurrentStore ).ToString();
        uxSearchModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "SearchMode", CurrentStore ).ToString();

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxBundlePromotionShowTR.Visible = false;
            uxBundlePromotionShowText.Text = "0";
            uxBundlePromotionShowText.Enabled = false;
        }
        else
        {
            uxBundlePromotionShowText.Text = DataAccessContext.Configurations.GetValue( "BundlePromotionShow", CurrentStore ).ToString();
        }
        uxRandomNumberText.Text = DataAccessContext.Configurations.GetValue( "RandomProductShow", CurrentStore );
        uxNumberBestSelling.Text
            = DataAccessContext.Configurations.GetValue( "ProductBestSellingShow", CurrentStore );

        SetUpRootCategoryDropDown();
        SetUpRootDepartmentDropDown();

        uxCategoryMenuTypeDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "CategoryMenuType", CurrentStore ).ToString();
        uxDepartmentMenuTypeDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "DepartmentMenuType", CurrentStore ).ToString();

        uxCategoryMenuLevelText.Text
            = DataAccessContext.Configurations.GetValue( "CategoryMenuLevel", CurrentStore );
        uxDepartmentMenuLevelText.Text
            = DataAccessContext.Configurations.GetValue( "DepartmentMenuLevel", CurrentStore );

        uxCategoryDynamicDropDownDisplayDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "CategoryDynamicDropDownDisplay", CurrentStore );
        uxDepartmentDynamicDropDownDisplayDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "DepartmentDynamicDropDownDisplay", CurrentStore );
        uxManufacturerDynamicDropDownDisplayDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "ManufacturerDynamicDropDownDisplay", CurrentStore );

        uxCategoryDynamicDropDownLevelText.Text
            = DataAccessContext.Configurations.GetValue( "CategoryDynamicDropDownLevel", CurrentStore );
        uxDepartmentDynamicDropDownLevelText.Text
            = DataAccessContext.Configurations.GetValue( "DepartmentDynamicDropDownLevel", CurrentStore );

        uxCategoryShowProductListDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "CategoryShowProductList", CurrentStore ).ToString();
        uxDepartmentShowProductListDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "DepartmentShowProductList", CurrentStore ).ToString();

        uxThemeSelect.PopulateControls( CurrentStore );
        uxMobileThemeSelect.PopulateControls( CurrentStore );
        uxCategorySelect.PopulateControls( CurrentStore );
        uxDepartmentSelect.PopulateControls( CurrentStore );
        uxProductListSelect.PopulateControls( CurrentStore );
        uxProductDetailsSelect.PopulateControls( CurrentStore );
        //uxDefaultMobileThemeSelect.PopulateControls( CurrentStore );

        uxNumberOfProduct.Text
            = DataAccessContext.Configurations.GetValue( "ProductItemsPerPage", CurrentStore );
        uxNumberOfCategory.Text
            = DataAccessContext.Configurations.GetValue( "CategoryItemsPerPage", CurrentStore );
        uxNumberOfDepartment.Text
            = DataAccessContext.Configurations.GetValue( "DepartmentItemsPerPage", CurrentStore );

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxBundlePromotionDisplayTR.Visible = false;
            uxBundlePromotionDisplayText.Text = "0";
            uxBundlePromotionDisplayText.Enabled = false;
        }
        else
        {
            uxBundlePromotionDisplayText.Text
                = DataAccessContext.Configurations.GetValue( "BundlePromotionDisplay", CurrentStore );
        }


        uxNumberOfProductColumnText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfProductColumn", CurrentStore );
        uxNumberOfCategoryColumnText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfCategoryColumn", CurrentStore );
        uxNumberOfDepartmentColumnText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfDepartmentColumn", CurrentStore );

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxBundlePromotionColumnTR.Visible = false;
            uxBundlePromotionColumnText.Text = "0";
            uxBundlePromotionColumnText.Enabled = false;
        }
        else
        {
            uxBundlePromotionColumnText.Text
                       = DataAccessContext.Configurations.GetValue( "BundlePromotionColumn", CurrentStore );
        }
        
        uxNumberRecentlyViewed.Text
            = DataAccessContext.Configurations.GetValue( "RecentlyViewedProductShow", CurrentStore );
        uxNumberCompareProduct.Text
            = DataAccessContext.Configurations.GetValue( "CompareProductShow", CurrentStore );
        uxTopCategoryMenuColumnText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfSubCategoryMenuColumn", CurrentStore );
        uxTopCategoryMenuItemText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfSubCategoryMenuItem", CurrentStore );
        uxNumberOfManufacturer.Text
            = DataAccessContext.Configurations.GetValue( "ManufacturerItemsPerPage", CurrentStore );
        uxRestrictAccessToShopDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "RestrictAccessToShop", CurrentStore );
        uxPriceRequireLoginDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "PriceRequireLogin", CurrentStore );

        uxRmaDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "EnableRMA", CurrentStore ).ToString();
        uxSSLDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "EnableSSL", CurrentStore );
        uxAddToCartDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", CurrentStore );
        uxQuickViewDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "EnableQuickView", CurrentStore );
        uxSaleTaxExemptDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "SaleTaxExempt", CurrentStore );

        uxMobileViewDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "MobileView", CurrentStore );

        uxManufacturerMenuTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ManufacturerMenuType", CurrentStore );
        uxAdvancedSearchModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "AdvancedSearchMode", CurrentStore );
        uxReviewPerCultureDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "EnableReviewPerCulture", CurrentStore ).ToString();
        uxCheckoutModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "CheckoutMode", CurrentStore );

        if (!KeyUtilities.IsMultistoreLicense())
        {
            uxRootCategorySettingPanel.Visible = false;
            uxRootDepartmentSettiongPanel.Visible = false;
        }

        uxWidgetAddThisConfig.PopulateControls( CurrentStore );
        uxWidgetLivePersonConfig.PopulateControls( CurrentStore );
        uxGoogleAnalyticsConfig.PopulateControls( CurrentStore );
    }

    public void Update()
    {
        uxSiteName.Update();
        uxLogoImage.Update();
        uxDefaultwebsiteLanguage.Update();

        DataAccessContext.ConfigurationRepository.UpdateValue(
          DataAccessContext.Configurations["StoreDefaultCountry"],
          uxStoreDefaultCountryDropDown.CurrentSelected,
          CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
          DataAccessContext.Configurations["SearchMode"],
          uxSearchModeDrop.SelectedValue,
          CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BundlePromotionShow"],
            uxBundlePromotionShowText.Text.Trim(),
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
          DataAccessContext.Configurations["RandomProductShow"],
          uxRandomNumberText.Text.Trim(),
          CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["ProductBestSellingShow"],
           uxNumberBestSelling.Text.Trim(),
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RootCategory"],
            uxRootCategoryDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RootDepartment"],
            uxRootDepartmentDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["CategoryMenuType"],
           uxCategoryMenuTypeDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["DepartmentMenuType"],
           uxDepartmentMenuTypeDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CategoryMenuLevel"],
            uxCategoryMenuLevelText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DepartmentMenuLevel"],
            uxDepartmentMenuLevelText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["CategoryDynamicDropDownDisplay"],
           uxCategoryDynamicDropDownDisplayDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["DepartmentDynamicDropDownDisplay"],
           uxDepartmentDynamicDropDownDisplayDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["ManufacturerDynamicDropDownDisplay"],
           uxManufacturerDynamicDropDownDisplayDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["EnableRMA"],
           uxRmaDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
          DataAccessContext.Configurations["CategoryDynamicDropDownLevel"],
          uxCategoryDynamicDropDownLevelText.Text,
          CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
          DataAccessContext.Configurations["DepartmentDynamicDropDownLevel"],
          uxDepartmentDynamicDropDownLevelText.Text,
          CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CategoryShowProductList"],
            uxCategoryShowProductListDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["DepartmentShowProductList"],
           uxDepartmentShowProductListDrop.SelectedValue,
          CurrentStore );

        uxThemeSelect.Update( CurrentStore );
        uxMobileThemeSelect.Update( CurrentStore );
        uxCategorySelect.Update( CurrentStore );
        uxDepartmentSelect.Update( CurrentStore );
        uxProductListSelect.Update( CurrentStore );
        uxProductDetailsSelect.Update( CurrentStore );
        //uxDefaultMobileThemeSelect.Update( CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfCategoryColumn"],
            uxNumberOfCategoryColumnText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfDepartmentColumn"],
            uxNumberOfDepartmentColumnText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BundlePromotionColumn"],
            uxBundlePromotionColumnText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfProductColumn"],
            uxNumberOfProductColumnText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CategoryItemsPerPage"],
            uxNumberOfCategory.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DepartmentItemsPerPage"],
            uxNumberOfDepartment.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BundlePromotionDisplay"],
            uxBundlePromotionDisplayText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfSubCategoryMenuColumn"],
            uxTopCategoryMenuColumnText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfSubCategoryMenuItem"],
            uxTopCategoryMenuItemText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProductItemsPerPage"],
            uxNumberOfProduct.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["RecentlyViewedProductShow"],
           uxNumberRecentlyViewed.Text,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompareProductShow"],
            uxNumberCompareProduct.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["EnableSSL"],
           uxSSLDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableAddToCartNotification"],
            uxAddToCartDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableQuickView"],
            uxQuickViewDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["SaleTaxExempt"],
           uxSaleTaxExemptDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["MobileView"],
           uxMobileViewDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["ManufacturerMenuType"],
           uxManufacturerMenuTypeDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ManufacturerItemsPerPage"],
            uxNumberOfManufacturer.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AdvancedSearchMode"],
            uxAdvancedSearchModeDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableReviewPerCulture"],
            uxReviewPerCultureDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CheckoutMode"],
            uxCheckoutModeDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["RestrictAccessToShop"],
           uxRestrictAccessToShopDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PriceRequireLogin"],
            uxPriceRequireLoginDrop.SelectedValue,
            CurrentStore );

        uxWidgetAddThisConfig.Update( CurrentStore );
        uxWidgetLivePersonConfig.Update( CurrentStore );
        uxGoogleAnalyticsConfig.Update( CurrentStore );

        PopulateControls();

    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }
}
