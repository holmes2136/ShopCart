using System;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;

public partial class Admin_Components_StoreConfig_StoreLayoutConfig : AdminAdvancedBaseUserControl
{
    private const string _pathUpload = "Images/Configuration/";

    private void SetImageTextBoxesWithUpload()
    {
        uxSpecialOfferUpload.ReturnTextControlClientID = uxSpecialOfferImageText.ClientID;
        uxPaymentLogoUpload.ReturnTextControlClientID = uxPaymentLogoImageText.ClientID;
        uxFeaturedMerchantUpload1.ReturnTextControlClientID = uxFeaturedMerchantImage1Text.ClientID;
        uxFeaturedMerchantUpload2.ReturnTextControlClientID = uxFeaturedMerchantImage2Text.ClientID;
        uxFeaturedMerchantUpload3.ReturnTextControlClientID = uxFeaturedMerchantImage3Text.ClientID;
        uxFreeShippingUpload.ReturnTextControlClientID = uxFreeShippingImageText.ClientID;
        uxSecureUpload.ReturnTextControlClientID = uxSecureImageText.ClientID;
    }

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private bool CouponEnable
    {
        get
        {
            return DataAccessContext.Configurations.GetBoolValue( "CouponEnabled" );
        }
    }

    private void SetupStoreBannerModuleDisplayDrop()
    {
        if (uxStoreBannerModuleDisplayDrop.SelectedValue == "True")
        {
            uxStoreBannerEffectModeTR.Visible = true;
            uxStoreBannerSlideSpeedTR.Visible = true;
            uxStoreBannerEffectPeriodTR.Visible = true;
        }
        else
        {
            uxStoreBannerEffectModeTR.Visible = false;
            uxStoreBannerSlideSpeedTR.Visible = false;
            uxStoreBannerEffectPeriodTR.Visible = false;
        }
    }

    private void SetupStoreBannerEffectModeDrop()
    {
        if (uxStoreBannerEffectModeDrop.SelectedValue == "none")
        {
            uxStoreBannerSlideSpeedTR.Visible = false;
            uxStoreBannerEffectPeriodTR.Visible = false;
        }
        else
        {
            uxStoreBannerSlideSpeedTR.Visible = true;
            uxStoreBannerEffectPeriodTR.Visible = true;
        }
    }

    private void SetUpTodaySpecialModuleDisplayDrop()
    {
        if (uxTodaySpecialModuleDisplayDrop.SelectedValue == "True")
        {
            uxTodaySpecialModuleEffectTR.Visible = true;
            uxTodaySpecialModuleSpeedTR.Visible = true;
            uxTodaySpecialModuleWaitTR.Visible = true;
        }
        else
        {
            uxTodaySpecialModuleEffectTR.Visible = false;
            uxTodaySpecialModuleSpeedTR.Visible = false;
            uxTodaySpecialModuleWaitTR.Visible = false;
        }
    }

    private void PopulateBannerEffectDropdown()
    {
        uxStoreBannerEffectModeDrop.Items.Clear();

        string[] effectName = ("None|Random|Simple Fade|Curtain Top Left|Curtain Top Right|Curtain Bottom Left|Curtain Bottom Right|Curtain Slice Left|CurtainSlice Right|Blind Curtain Top Left|Blind Curtain Top Right|Blind Curtain Bottom Left|Blind Curtain Bottom Right|Blind Curtain Slice Bottom|Blind Curtain Slice Top|Stampede|Mosaic|Mosaic Reverse|Mosaic Random|Mosaic Spiral|Mosaic Spiral Reverse|Top Left Bottom Right|Bottom Right Top Left|Bottom Left Top Right|Bottom Left Top Right|Scroll Left|Scroll Right|Scroll Horz|Scroll Bottom|Scroll Top").Split( '|' );
        string[] effectValue = ("none|random|simpleFade|curtainTopLeft|curtainTopRight|curtainBottomLeft|curtainBottomRight|curtainSliceLeft|curtainSliceRight|blindCurtainTopLeft|blindCurtainTopRight|blindCurtainBottomLeft|blindCurtainBottomRight|blindCurtainSliceBottom|blindCurtainSliceTop|stampede|mosaic|mosaicReverse|mosaicRandom|mosaicSpiral|mosaicSpiralReverse|topLeftBottomRight|bottomRightTopLeft|bottomLeftTopRight|bottomLeftTopRight|scrollLeft|scrollRight|scrollHorz|scrollBottom|scrollTop").Split( '|' );

        for (int i = 0; i < effectName.Length; i++)
        {
            uxStoreBannerEffectModeDrop.Items.Add( new ListItem( effectName[i], effectValue[i] ) );
        }
    }

    private void PopulateCurrencyDropdown()
    {
        string currencyCode = DataAccessContext.Configurations.GetValue( "DefaultDisplayCurrencyCode", CurrentStore );

        uxDisplayCurrencyCodeDrop.DataSource = DataAccessContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
        uxDisplayCurrencyCodeDrop.DataBind();

        uxDisplayCurrencyCodeDrop.SelectedValue = currencyCode;
    }

    private void SetupEnableNewArrivalProductDrop()
    {
        if (uxEnableNewArrivalProductDrop.SelectedValue == "True")
        {
            uxEnableNewArrivalProductPanel.Visible = true;
        }
        else
        {
            uxEnableNewArrivalProductPanel.Visible = false;
        }
    }

    private void SetUpFacetedSearchVisibility()
    {
        if (uxFacetedSearchEnabledDrop.SelectedValue == "True")
            uxFacetedSearchDetailsPanel.Visible = true;
        else
            uxFacetedSearchDetailsPanel.Visible = false;
    }

    private void SetUpQuickSearchVisibility()
    {
        if (uxSearchModuleDisplayDrop.SelectedValue == "True")
            uxSearchModuleOptionPanel.Visible = true;
        else
            uxSearchModuleOptionPanel.Visible = false;
    }

    public void PopulateCouponDisPlay()
    {
        if (CouponEnable)
        {
            if (!MainContext.IsPostBack)
            {
                uxCouponModuleDisplayDrop.SelectedValue =
                    DataAccessContext.Configurations.GetValue( "CouponModuleDisplay", CurrentStore );
            }
            uxCouponModuleDisplayDrop.Enabled = true;
        }
        else
        {
            uxCouponModuleDisplayDrop.SelectedValue = "False";
            uxCouponModuleDisplayDrop.Enabled = false;
        }
    }

    public void PopulateControls()
    {
        PopulateCouponDisPlay();
        PopulateCurrencyDropdown();
        PopulateBannerEffectDropdown();

        uxStoreBannerModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "StoreBannerModuleDisplay", CurrentStore ).ToString();

        uxStoreBannerEffectModeDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue( "StoreBannerEffectMode", CurrentStore );

        uxStoreBannerSlideSpeedText.Text =
            DataAccessContext.Configurations.GetValue( "StoreBannerSlideSpeed", CurrentStore );

        uxStoreBannerEffectPeriodText.Text =
            DataAccessContext.Configurations.GetValue( "StoreBannerEffectPeriod", CurrentStore );

        SetupStoreBannerModuleDisplayDrop();
        SetupStoreBannerEffectModeDrop();

        uxCategoryListModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "CategoryListModuleDisplay", CurrentStore ).ToString();

        uxDepartmentListModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay", CurrentStore ).ToString();

        uxDepartmentHeaderMenuDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "DepartmentHeaderMenuDisplay", CurrentStore ).ToString();

        uxEnableManufacturerDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "EnableManufacturer", CurrentStore ).ToString();

        uxManufacturerHeaderMenuDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "ManufacturerHeaderMenuDisplay", CurrentStore ).ToString();

        uxHeaderMenuStyleDrop.SelectedValue =
           DataAccessContext.Configurations.GetValue( "HeaderMenuStyle", CurrentStore ).ToString();

        uxTodaySpecialModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "TodaySpecialModuleDisplay", CurrentStore ).ToString();
        uxTodaySpecialModuleEffectDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue( "ProductSpecialEffectMode", CurrentStore );
        uxTodaySpecialModuleSpeedText.Text =
            DataAccessContext.Configurations.GetValue( "ProductSpecialTransitionSpeed", CurrentStore );
        uxTodaySpecialModuleWaitText.Text =
            DataAccessContext.Configurations.GetValue( "ProductSpecialEffectWaitTime", CurrentStore );

        uxCurrencyModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "CurrencyModuleDisplay", CurrentStore ).ToString();
        uxNewsletterModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "NewsletterModuleDisplay", CurrentStore ).ToString();
        uxSpecialOfferModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "SpecialOfferModuleDisplay", CurrentStore ).ToString();
        uxSpecialOfferImageText.Text =
            DataAccessContext.Configurations.GetValue( "SpecialOfferImage", CurrentStore );
        uxPaymentLogoModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "PaymentLogoModuleDisplay", CurrentStore ).ToString();
        uxPaymentLogoImageText.Text =
            DataAccessContext.Configurations.GetValue( "PaymentLogoImage", CurrentStore );
        uxSearchModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "SearchModuleDisplay", CurrentStore ).ToString();
        uxDisplayCategoryInQuickSearchDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "DisplayCategoryInQuickSearch", CurrentStore ).ToString();
        uxMiniCartModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "MiniCartModuleDisplay", CurrentStore ).ToString();

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxGiftRegistryTR.Visible = false;
            uxGiftRegistryDisplayDrop.SelectedValue = "False";
            uxGiftRegistryDisplayDrop.Enabled = false;
        }
        if (!MainContext.IsPostBack)
        {
            uxGiftRegistryDisplayDrop.SelectedValue =
                DataAccessContext.Configurations.GetValue( "GiftRegistryModuleDisplay", CurrentStore ).ToString();
        }

        uxFeaturedMerchantModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "FeaturedMerchantModuleDisplay", CurrentStore ).ToString();
        uxFeaturedMerchantCountDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantCount", CurrentStore );
        uxFeaturedMerchantImage1Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage1", CurrentStore );
        uxFeaturedMerchantImage2Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage2", CurrentStore );
        uxFeaturedMerchantImage3Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantImage3", CurrentStore );
        uxFeaturedMerchantUrl1Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl1", CurrentStore );
        uxFeaturedMerchantUrl2Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl2", CurrentStore );
        uxFeaturedMerchantUrl3Text.Text =
            DataAccessContext.Configurations.GetValue( "FeaturedMerchantUrl3", CurrentStore );

        uxFreeShippingModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "FreeShippingModuleDisplay", CurrentStore ).ToString();
        uxFreeShippingImageText.Text =
            DataAccessContext.Configurations.GetValue( "FreeShippingImage", CurrentStore );

        uxSecureModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "SecureModuleDisplay", CurrentStore ).ToString();
        uxSecureImageText.Text =
            DataAccessContext.Configurations.GetValue( "SecureImage", CurrentStore );

        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxBundlePromotionDisplayTR.Visible = false;
            uxBundlePromotionDisplayDrop.SelectedValue = "False";
            uxBundlePromotionDisplayDrop.Enabled = false;
        }
        else
        {
            uxBundlePromotionDisplayDrop.SelectedValue =
                DataAccessContext.Configurations.GetBoolValue( "EnableBundlePromo", CurrentStore ).ToString();
        }
        uxBestsellersModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "BestsellersModuleDisplay", CurrentStore ).ToString();
        uxFeaturedProductModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "FeaturedProductModuleDisplay", CurrentStore ).ToString();
        uxNewsModuleDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "NewsModuleDisplay", CurrentStore ).ToString();

        uxWishListDisplayDrop.SelectedValue =
            DataAccessContext.Configurations.GetValue( "WishListEnabled", CurrentStore );

        uxTellAFriendDisplayDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "TellAFriendEnabled", CurrentStore );


        SetUpTodaySpecialModuleDisplayDrop();

        uxRecentlyViewedProductDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "RecentlyViewedProductDisplay", CurrentStore );

        uxCompareListDisplayDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "CompareListEnabled", CurrentStore );

        uxFacetedSearchEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "FacetedSearchEnabled", CurrentStore ).ToString();
        uxPriceNavigationStepTextbox.Text = DataAccessContext.Configurations.GetValue( "PriceNavigationStep", CurrentStore );
        uxMaximunIntervalTextbox.Text = DataAccessContext.Configurations.GetValue( "MaximunInterval", CurrentStore );
        uxEnableNewArrivalProductDrop.SelectedValue = DataAccessContext.Configurations.GetBoolValue( "EnableNewArrivalProduct", CurrentStore ).ToString();
        uxProductNewArrivalNumberText.Text = DataAccessContext.Configurations.GetValue( "ProductNewArrivalNumber", CurrentStore );
        uxMaximumDisplayProductNewArrivalText.Text = DataAccessContext.Configurations.GetValue( "MaximumDisplayProductNewArrival", CurrentStore );
        SetupEnableNewArrivalProductDrop();

    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["StoreBannerModuleDisplay"],
            uxStoreBannerModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["StoreBannerEffectMode"],
            uxStoreBannerEffectModeDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["StoreBannerSlideSpeed"],
            uxStoreBannerSlideSpeedText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["StoreBannerEffectPeriod"],
            uxStoreBannerEffectPeriodText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CategoryListModuleDisplay"],
            uxCategoryListModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DepartmentListModuleDisplay"],
            uxDepartmentListModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DepartmentHeaderMenuDisplay"],
            uxDepartmentHeaderMenuDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           DataAccessContext.Configurations["EnableManufacturer"],
           uxEnableManufacturerDrop.SelectedValue,
           CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ManufacturerHeaderMenuDisplay"],
            uxManufacturerHeaderMenuDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["HeaderMenuStyle"],
            uxHeaderMenuStyleDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["TodaySpecialModuleDisplay"],
            uxTodaySpecialModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProductSpecialEffectMode"],
            uxTodaySpecialModuleEffectDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProductSpecialTransitionSpeed"],
            uxTodaySpecialModuleSpeedText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProductSpecialEffectWaitTime"],
            uxTodaySpecialModuleWaitText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CurrencyModuleDisplay"],
            uxCurrencyModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NewsletterModuleDisplay"],
            uxNewsletterModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SpecialOfferModuleDisplay"],
            uxSpecialOfferModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SpecialOfferImage"],
            uxSpecialOfferImageText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PaymentLogoModuleDisplay"],
            uxPaymentLogoModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PaymentLogoImage"],
            uxPaymentLogoImageText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SearchModuleDisplay"],
            uxSearchModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DisplayCategoryInQuickSearch"],
            uxDisplayCategoryInQuickSearchDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MiniCartModuleDisplay"],
            uxMiniCartModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CouponModuleDisplay"],
            uxCouponModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantModuleDisplay"],
            uxFeaturedMerchantModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantCount"],
            uxFeaturedMerchantCountDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantImage1"],
            uxFeaturedMerchantImage1Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantImage2"],
            uxFeaturedMerchantImage2Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantImage3"],
            uxFeaturedMerchantImage3Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantUrl1"],
            uxFeaturedMerchantUrl1Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantUrl2"],
            uxFeaturedMerchantUrl2Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedMerchantUrl3"],
            uxFeaturedMerchantUrl3Text.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FreeShippingModuleDisplay"],
            uxFreeShippingModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FreeShippingImage"],
            uxFreeShippingImageText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SecureModuleDisplay"],
            uxSecureModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SecureImage"],
            uxSecureImageText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableBundlePromo"],
            uxBundlePromotionDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["BestsellersModuleDisplay"],
            uxBestsellersModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FeaturedProductModuleDisplay"],
            uxFeaturedProductModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NewsModuleDisplay"],
            uxNewsModuleDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["WishListEnabled"],
            uxWishListDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["TellAFriendEnabled"],
            uxTellAFriendDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GiftRegistryModuleDisplay"],
            uxGiftRegistryDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["DefaultDisplayCurrencyCode"],
            uxDisplayCurrencyCodeDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["RecentlyViewedProductDisplay"],
            uxRecentlyViewedProductDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["CompareListEnabled"],
            uxCompareListDisplayDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FacetedSearchEnabled"],
            uxFacetedSearchEnabledDrop.SelectedValue,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PriceNavigationStep"],
            uxPriceNavigationStepTextbox.Text, CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MaximunInterval"],
            uxMaximunIntervalTextbox.Text, CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["EnableNewArrivalProduct"],
            uxEnableNewArrivalProductDrop.SelectedValue, CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProductNewArrivalNumber"],
            uxProductNewArrivalNumberText.Text, CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MaximumDisplayProductNewArrival"],
            uxMaximumDisplayProductNewArrivalText.Text, CurrentStore );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            Vevo.Domain.Configurations.Configuration config =
                DataAccessContext.Configurations["ProductSpecialEffectMode"];
            string[] fieldValue = config.SelectionValues.Split( '|' );
            string[] fieldName = config.SelectionNames.Split( '|' );
            if (fieldName.Length == fieldValue.Length)
            {
                uxTodaySpecialModuleEffectDrop.Items.Clear();
                for (int i = 0; i < fieldName.Length; i++)
                {
                    uxTodaySpecialModuleEffectDrop.Items.Add( new ListItem( fieldName[i], fieldValue[i] ) );
                }
            }
        }

        uxSpecialOfferUpload.PathDestination = _pathUpload;
        uxPaymentLogoUpload.PathDestination = _pathUpload;
        uxFeaturedMerchantUpload1.PathDestination = _pathUpload;
        uxFeaturedMerchantUpload2.PathDestination = _pathUpload;
        uxFeaturedMerchantUpload3.PathDestination = _pathUpload;
        uxFreeShippingUpload.PathDestination = _pathUpload;
        uxSecureUpload.PathDestination = _pathUpload;

        if (!IsAdminModifiable())
        {
            uxSpecialOfferUploadLinkButton.Visible = false;
            uxPaymentLogoUploadLinkButton.Visible = false;
            uxFeaturedMerchantUpload1LinkButton.Visible = false;
            uxFeaturedMerchantUpload2LinkButton.Visible = false;
            uxFeaturedMerchantUpload3LinkButton.Visible = false;
            uxFreeShippingUploadLinkButton.Visible = false;
            uxSecureUploadLinkButton.Visible = false;
        }
        SetImageTextBoxesWithUpload();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpFacetedSearchVisibility();
    }

    protected void uxSpecialOfferUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxSpecialOfferUpload.ShowControl = true;
        uxSpecialOfferUploadLinkButton.Visible = false;
    }

    protected void uxPaymentLogoUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxPaymentLogoUpload.ShowControl = true;
        uxPaymentLogoUploadLinkButton.Visible = false;
    }

    protected void uxFeaturedMerchantUpload1LinkButton_Click( object sender, EventArgs e )
    {
        uxFeaturedMerchantUpload1.ShowControl = true;
        uxFeaturedMerchantUpload1LinkButton.Visible = false;
    }

    protected void uxFeaturedMerchantUpload2LinkButton_Click( object sender, EventArgs e )
    {
        uxFeaturedMerchantUpload2.ShowControl = true;
        uxFeaturedMerchantUpload2LinkButton.Visible = false;
    }

    protected void uxFeaturedMerchantUpload3LinkButton_Click( object sender, EventArgs e )
    {
        uxFeaturedMerchantUpload3.ShowControl = true;
        uxFeaturedMerchantUpload3LinkButton.Visible = false;
    }

    protected void uxFreeShippingUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxFreeShippingUpload.ShowControl = true;
        uxFreeShippingUploadLinkButton.Visible = false;
    }

    protected void uxSecureUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxSecureUpload.ShowControl = true;
        uxSecureUploadLinkButton.Visible = false;
    }

    protected void uxStoreBannerModuleDisplayDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetupStoreBannerModuleDisplayDrop();
    }

    protected void uxStoreBannerEffectModeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetupStoreBannerEffectModeDrop();
    }

    protected void uxTodaySpecialModuleDisplayDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetUpTodaySpecialModuleDisplayDrop();
    }

    protected void uxEnableNewArrivalProductDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetupEnableNewArrivalProductDrop();
    }

    protected void uxFacetedSearchEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetUpFacetedSearchVisibility();
    }

    protected void uxSearchModuleDisplayDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetUpQuickSearchVisibility();
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
