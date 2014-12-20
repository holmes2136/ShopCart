using System;
using System.Data;
using System.Web.UI;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Base;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_ProductDetailsEdit : AdminAdvancedBaseUserControl
{
    #region Private

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Edit;

    private string Action
    {
        get
        {
            string value = MainContext.QueryString["action"];
            if (!String.IsNullOrEmpty( value ))
            {
                return value;
            }
            else
            {
                return "0";
            }
        }
    }

    private int TabId
    {
        get
        {
            string value = MainContext.QueryString["TabName"];
            if (!String.IsNullOrEmpty( value ))
            {
                switch (value)
                {
                    case "Attribute": return 1;
                    case "Image": return 2;
                    case "Seo": return 3;
                    case "Other": return 4;
                    default: return 0;
                }
            }
            else
                return 0;
        }
    }

    private void ClearInputFields()
    {
        uxProductInfo.ClearInputFields();
        uxGiftCertificate.ClearInputFields();
        uxProductLink.ClearInputFields();
        uxProductAttributes.ClearInputFields();
        uxDepartmentInfo.ClearInputFields();
        uxProductSeo.ClearInputFields();

        uxGiftCertificate.SetGiftCertificateControlsVisibility( IsEditMode() );

        //Recurring Edit
        uxRecurring.ClearInputFields();
        uxProductSubscription.ClearInputFields();
        uxProductKit.ClearInputFields();
    }

    private void PopulateControls()
    {
        PopulateLanguageSection();

        if (ConvertUtilities.ToInt32( CurrentID ) > 0)
        {
            Product product =
                DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID, uxStoreList.CurrentSelected );

            uxProductAttributes.PopulateControls( product, uxStoreList.CurrentSelected );
            uxProductSeo.PopulateControls( product, uxStoreList.CurrentSelected );

            uxGiftCertificate.PopulateControls( product );
            if (product.IsGiftCertificate)
            {
                uxGiftCertificate.PopulateGiftData( (GiftCertificateProduct) product );
                uxGiftCertificate.SetGiftCertificateControlsVisibility( IsEditMode() );
            }

            uxRecurring.PopulateControls( product );
            if (KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            {
                uxProductSubscription.PopulateControls( product );
            }
            uxProductAttributes.IsFixPrice(
                uxGiftCertificate.IsFixedPrice, uxGiftCertificate.IsGiftCertificate, uxRecurring.IsRecurring, uxProductAttributes.IsCallForPrice );
            uxProductKit.PopulateControls( product );
        }
    }

    private void CopyVisible( bool value )
    {
        uxCopyButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {

                uxCopyConfirmButton.TargetControlID = "uxCopyButton";
                uxConfirmModalPopup.TargetControlID = "uxCopyButton";
            }
            else
            {
                uxCopyConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxCopyConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private void DisplayControlsByVersion()
    {
        uxProductAttributes.SetDisplayControls();
        uxGiftCertificate.SetDisplayControl( DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" ) );
        if (IsEditMode())
        {
            CopyVisible( true );
        }
    }

    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void Language_RefreshHandler( object sender, EventArgs e )
    {
        PopulateLanguageSection();
    }

    private string AddNew()
    {
        Product product = new Product( uxLanguageControl.CurrentCulture );
        product = uxGiftCertificate.Setup( uxLanguageControl.CurrentCulture );

        product = SetUpProduct( product );

        ProductSubscription subscriptionItem = uxProductSubscription.Setup( product );

        product = DataAccessContext.ProductRepository.Save( product );
        DataAccessContextDeluxe.ProductSubscriptionRepository.SaveAll( product.ProductID, subscriptionItem.ProductSubscriptions );

        uxMessage.DisplayMessage( Resources.ProductMessages.AddSuccess );

        return product.ProductID;
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxProductInfo.ConvertToCategoryIDs().Length <= 0)
                {
                    uxMessage.DisplayError( Resources.ProductMessages.AddErrorCategoryEmpty );
                    return;
                }
                if (uxProductKit.IsProductKit)
                {
                    if (uxProductKit.GetSelectedGroupID().Length <= 0)
                    {
                        uxMessage.DisplayError( Resources.ProductMessages.AddErrorProductKitEmpty );
                        return;
                    }
                }
                if (!uxProductAttributes.VerifyInputListOption())
                {
                    DisplayErrorOption();
                    return;
                }

                string price;
                string retailPrice;
                string wholeSalePrice;
                string wholeSalePrice2;
                string wholeSalePrice3;
                decimal giftAmount;
                if (uxProductAttributes.IsFixPrice(
                    uxGiftCertificate.IsFixedPrice, uxGiftCertificate.IsGiftCertificate, uxRecurring.IsRecurring, uxProductAttributes.IsCallForPrice ))
                {

                    price = uxProductAttributes.Price;
                    retailPrice = uxProductAttributes.RetailPrice;
                    wholeSalePrice = uxProductAttributes.WholeSalePrice;
                    wholeSalePrice2 = uxProductAttributes.WholeSalePrice2;
                    wholeSalePrice3 = uxProductAttributes.WholeSalePrice3;

                    giftAmount = ConvertUtilities.ToDecimal( uxGiftCertificate.GiftAmount );
                }
                else
                {
                    price = "0";
                    retailPrice = "0";
                    wholeSalePrice = "0";
                    wholeSalePrice2 = "0";
                    wholeSalePrice3 = "0";
                    giftAmount = 0m;
                }
                string storeID = new StoreRetriever().GetCurrentStoreID();
                Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID, storeID );
                product = SetUpProduct( product );
                product = uxProductImageList.Update( product );

                ProductSubscription subscriptionItem = uxProductSubscription.Setup( product );

                if (product.ProductOptionGroups.Count > 0)
                {
                    DataAccessContext.ProductKitGroupRepository.DeleteProductKitItemByProductID( product.ProductID );
                }
                product = DataAccessContext.ProductRepository.Save( product );
                DataAccessContextDeluxe.ProductSubscriptionRepository.SaveAll( product.ProductID, subscriptionItem.ProductSubscriptions );

                uxMessage.DisplayMessage( Resources.ProductMessages.UpdateSuccess );

                AdminUtilities.ClearSiteMapCache();

                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private DataTable CurrentStockOption
    {
        get
        {
            if (ViewState["CurrentStockOption"] == null)
                return null;
            else
                return (DataTable) ViewState["CurrentStockOption"];
        }
        set
        {
            ViewState["CurrentStockOption"] = value;
        }
    }

    private void DisplayErrorOption()
    {
        uxMessage.DisplayError( Resources.ProductOptionMessages.InvalidInputList );
    }

    private void EnforcePermission()
    {
        if (!MainContext.IsPostBack &&
            !IsAdminModifiable())
        {
            if (IsEditMode())
            {
                uxEditButton.Visible = false;
                CopyVisible( false );
                uxProductAttributes.HideUploadButton();
                uxDeleteButton.Visible = false;
                uxProductImageList.HideUploadImageButton();
            }
        }
    }
    private void PopulateLink()
    {
        uxReviewLink.Visible = true;
        GetReviewLink();
    }

    private void GetReviewLink()
    {
        uxReviewLink.PageName = "ProductReviewList.ascx";
        uxReviewLink.PageQueryString = "ProductID=" + CurrentID;
    }

    private void CopyRemainingProductLocales( Product originalProduct, string newProductID )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product newProduct = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, newProductID, storeID );
        newProduct.ImageSecondary = String.Empty;
        foreach (ILocale locale in originalProduct.GetLocales())
        {
            if (locale.CultureID != newProduct.Locales[uxLanguageControl.CurrentCulture].CultureID)
            {
                ProductLocale newLocale = new ProductLocale();
                newLocale.CultureID = originalProduct.Locales[locale.CultureID].CultureID;
                newLocale.Name = originalProduct.Locales[locale.CultureID].Name;
                newLocale.ShortDescription = originalProduct.Locales[locale.CultureID].ShortDescription;
                newLocale.LongDescription = originalProduct.Locales[locale.CultureID].LongDescription;
                newProduct.Locales.Add( newLocale );
            }
        }

        DataAccessContext.ProductRepository.Save( newProduct );
    }

    private void CopyMetaInformation( Product originalProduct, string newProductID )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product newProduct = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, newProductID, storeID );
        foreach (ProductMetaInformation meta in originalProduct.GetMetaInformations())
        {
            Culture culture = DataAccessContext.CultureRepository.GetOne( meta.CultureID );
            newProduct.SetMetaKeyword( culture, meta.StoreID, meta.MetaKeyword );
            newProduct.SetMetaDescription( culture, meta.StoreID, meta.MetaDescription );
            newProduct.SetUseDefaultValueMetaKeyword( culture, meta.StoreID, meta.UseDefaultMetaKeyword );
            newProduct.SetUseDefaultValueMetaDescription( culture, meta.StoreID, meta.UseDefaultMetaDescription );
        }
        DataAccessContext.ProductRepository.Save( newProduct );
    }

    private void CopyProductPrice( Product originalProduct, string newProductID )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();

        Product newProduct = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, newProductID, storeID );

        newProduct.ProductPrices = originalProduct.ProductPrices;

        DataAccessContext.ProductRepository.Save( newProduct );
    }

    private Product SetUpProduct( Product product )
    {
        product = uxProductInfo.Setup( product );
        product = uxDepartmentInfo.Setup( product );
        product = uxProductSeo.Setup( product, uxStoreList.CurrentSelected );
        product = uxRecurring.Setup( product );
        product = uxProductAttributes.Setup( product, uxStoreList.CurrentSelected );
        product = uxProductKit.Setup( product );

        product.ShippingCost = ConvertUtilities.ToDecimal( "0" );
        product.IsGiftCertificate = uxGiftCertificate.IsGiftCertificate;
        if (product.IsGiftCertificate)
            product = uxGiftCertificate.Update( product );
        product.IsFixedPrice = uxProductAttributes.IsFixPrice(
            uxGiftCertificate.IsFixedPrice, uxGiftCertificate.IsGiftCertificate, uxRecurring.IsRecurring, uxProductAttributes.IsCallForPrice );
        product.ImageSecondary = uxProductImageList.SecondaryImage();

        product.ProductStocks.Clear();
        product.ProductOptionGroups.Clear();
        product.ProductShippingCosts.Clear();

        uxProductAttributes.AddOptionGroup( product );
        uxProductAttributes.CreateStockOption( product );
        uxProductAttributes.UpdateProductShippingCost( product );
        uxProductAttributes.SetProductSpecifications( product );
        return product;
    }

    private void ProductSubscriptionCondition()
    {
        if (uxProductSubscription.IsProductSubscription)
        {
            //set disable control
            uxProductKit.DisableProductKitControl();
            uxProductSubscription.ShowProductSubscription();
            uxProductAttributes.SetEnabledControlsForProductSubscription( false );
        }
        else
        {
            //set enable control
            uxProductKit.EnableProductKitControl();
            uxProductSubscription.HideProductSubscription();
            uxProductAttributes.SetEnabledControlsForProductSubscription( true );
        }
    }

    private void RecurringCondition()
    {
        //Add code for Recurring.        
        if (uxRecurring.IsRecurring)
        {
            uxRecurring.ShowRecurring();
        }
        else
        {
            uxRecurring.HideRecurring();
        }
    }

    private void GiftCertificateCondition()
    {
        if (uxGiftCertificate.IsGiftCertificate)
        {
            uxProductKit.DisableProductKitControl();
            uxProductSubscription.DisableProductScuscriptionControl();
            uxRecurring.DisableRecurring();
        }
        else
        {
            uxRecurring.EnableRecurring();
        }
    }

    private void ProductKitCondition()
    {
        if (uxProductKit.IsProductKit)
        {
            uxProductKit.SetIsProductKit( true );
            uxProductAttributes.SetProductKitControlVisible( true );
            uxProductSubscription.DisableProductScuscriptionControl();
            uxProductAttributes.IsDownloadableEnabled( false );
        }
        else
        {
            uxProductKit.SetIsProductKit( false );
            uxProductAttributes.SetProductKitControlVisible( false );
            uxProductSubscription.EnableProductScuscriptionControl();
            uxProductAttributes.IsDownloadableEnabled( true );
        }
    }

    private void ShowHideQuantityDiscount()
    {
        if (uxProductKit.IsProductKit || uxProductSubscription.IsProductSubscription)
        {
            uxProductAttributes.SetEnabledQuantityDiscount( false );
        }
        else
        {
            uxProductAttributes.SetEnabledQuantityDiscount( true );
        }
    }

    private void ShowHideMinMaxQTY()
    {
        if (uxProductSubscription.IsProductSubscription)
        {
            uxProductAttributes.SetEnabledMinMaxQTY( false );
            uxProductAttributes.SetMinQuantity( 1 );
            uxProductAttributes.SetMaxQuantity( 1 );
        }
        else
        {
            uxProductAttributes.SetEnabledMinMaxQTY( true );
        }
    }

    private void ShowHideDownloadable()
    {
        if (uxProductKit.IsProductKit || uxRecurring.IsRecurring)
        {
            uxProductAttributes.IsDownloadableEnabled( false );
        }
        else
        {
            uxProductAttributes.IsDownloadableEnabled( true );
        }
    }

    #endregion


    #region Protected

    protected string CurrentID
    {
        get
        {
            if (IsEditMode())
                return MainContext.QueryString["ProductID"];
            else
                return "0";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Language_RefreshHandler );

        uxProductInfo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxDepartmentInfo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductSeo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductSeo.StoreID = uxStoreList.CurrentSelected;
        uxProductAttributes.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductAttributes.SetEditMode( IsEditMode() );

        uxProductAttributes.PopulateShippingCostControl();
        uxProductAttributes.PopulateSpecificationItemControls();

        if (!MainContext.IsPostBack)
        {
            if (!KeyUtilities.IsMultistoreLicense())
            {
                uxStoreList.Visible = false;
                uxStoreViewLabel.Visible = false;
            }
            uxProductAttributes.InitTaxClassDrop();

            uxProductAttributes.InitDiscountDrop();
            PopulateLink();
            uxProductAttributes.PopulateDropdown();
            uxProductSubscription.InitDropDown();
        }
        uxTabContainer.ActiveTabIndex = TabId;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            ucProductSubscriptionTR.Visible = false;
        }

        if (Action == "copy" && !MainContext.IsPostBack)
        {
            uxMessage.DisplayMessage( Resources.ProductMessages.CopySuccess );
        }

        if (!MainContext.IsPostBack)
        {
            uxProductAttributes.SetOptionList();
        }

        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();

                if (CurrentID != null &&
                    int.Parse( CurrentID ) >= 0)
                {
                    Product product = DataAccessContext.ProductRepository.GetOne(
                        uxLanguageControl.CurrentCulture, CurrentID, uxStoreList.CurrentSelected );

                    uxProductAttributes.SelectOptionList( product );
                    uxProductAttributes.PopulateStockOptionControl();
                }
            }
        }
        else
        {
            if (!MainContext.IsPostBack)
            {
                uxEditButton.Visible = false;
                CopyVisible( false );
                PopulateControls();

                uxProductAttributes.HideStockOption();
                uxProductIDTR.Visible = false;
                uxProductAttributes.PopulateStockVisibility();

                //Add code for Recurring.                
                uxRecurring.HideRecurring();
            }
        }

        uxProductAttributes.RestoreSessionData();

        uxProductAttributes.SetWholesaleVisible( uxGiftCertificate.IsFixedPrice, uxGiftCertificate.IsGiftCertificate, uxRecurring.IsRecurring );

        uxProductAttributes.SetRetailPriceVisible( uxGiftCertificate.IsFixedPrice, uxGiftCertificate.IsGiftCertificate, uxRecurring.IsRecurring );

        uxProductAttributes.SetProductRatingVisible( uxGiftCertificate.IsGiftCertificate );

        uxGiftCertificate.SetGiftCertificateControlsVisibility( IsEditMode() );

        ProductSubscriptionCondition();
        RecurringCondition();
        ProductKitCondition();
        GiftCertificateCondition();

        ShowHideQuantityDiscount();
        ShowHideDownloadable();
        ShowHideMinMaxQTY();


        uxProductAttributes.SetFixedShippingCostVisibility( uxGiftCertificate.IsGiftCertificate );

        DisplayControlsByVersion();
        EnforcePermission();
    }

    protected void uxSaveAndViewImageLinkButton_Click( object sender, EventArgs e )
    {
        Update();
        MainContext.RedirectMainControl( "ProductImageList.ascx", "ProductID=" + CurrentID );
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        Update();
    }

    protected void uxCopyButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                if (!uxProductAttributes.VerifyInputListOption())
                {
                    DisplayErrorOption();
                    return;
                }

                string newProductID = AddNew();
                Product originalProduct = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID, new StoreRetriever().GetCurrentStoreID() );
                CopyRemainingProductLocales( originalProduct, newProductID );
                CopyMetaInformation( originalProduct, newProductID );
                CopyProductPrice( originalProduct, newProductID );

                ClearInputFields();
                uxProductAttributes.PopulateStockOptionControl();
                AdminUtilities.ClearSiteMapCache();

                MainContext.RedirectMainControl( "ProductEdit.ascx", String.Format( "ProductID={0}&action=copy", newProductID ) );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxIsFixedPriceDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxStatusHidden.Value = "Refresh";
    }

    protected void uxReviewLink_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductReviewList.ascx", String.Format( "ProductID={0}", CurrentID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            DataAccessContext.ProductRepository.Delete( CurrentID );
            DataAccessContextDeluxe.PromotionProductRepository.DeleteByProductID( CurrentID );
            DataAccessContextDeluxe.ProductSubscriptionRepository.DeleteByProductID( CurrentID );
            deleted = true;

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductMessages.DeleteSuccess );
                MainContext.RedirectMainControl( "ProductList.ascx" );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }


    #endregion


    #region Public Methods

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    public int GetNumberOfCategories()
    {
        return DataAccessContext.CategoryRepository.GetAll( uxLanguageControl.CurrentCulture, "CategoryID" ).Count;
    }

    private void PopulateLanguageSection()
    {

        if (CurrentID != null &&
            int.Parse( CurrentID ) > 0)
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID, new StoreRetriever().GetCurrentStoreID() );
        }

        uxProductInfo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductInfo.PopulateControls();
        uxDepartmentInfo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductAttributes.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductSeo.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductSeo.StoreID = uxStoreList.CurrentSelected;
        uxProductSeo.PopulateControls();
        uxProductKit.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductImageList.CurrentCulture = uxLanguageControl.CurrentCulture;
        uxProductImageList.PopulateControls();
    }

    #endregion

}



