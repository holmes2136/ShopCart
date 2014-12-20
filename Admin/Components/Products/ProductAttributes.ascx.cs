using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;
using Vevo.Domain.Tax;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_Components_Products_ProductAttributes : AdminAdvancedBaseUserControl
{
    #region Private

    private const string _pathUploadProduct = "Images/Products/";
    private const int MaxSmallProductImageWidth = 170;
    private const string ProductDetailsSessionName = "AdminProductDetails";
    private bool _isEditMode;
    private bool _isEnterPrice = false;

    private bool IsRestoreSession
    {
        get
        {
            return !MainContext.IsPostBack &&
                ConvertUtilities.ToBoolean( MainContext.QueryString["Restore"] ) &&
                Session[ProductDetailsSessionName] != null;
        }
    }

    private string _pathUploadProductFile
    {
        get
        {
            return WebConfiguration.ProductDownloadPath + "/";
        }
    }

    private string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }
    private bool isCustomPrice
    {
        get
        {
            _isEnterPrice = uxUseCustomPriceCheck.Checked;
            return _isEnterPrice;
        }
    }
    private string FormatNumber( string number )
    {
        return String.Format( "{0:f2}", number );
    }

    private string CreateNonEmptyNumber( string number )
    {
        if (String.IsNullOrEmpty( number.Trim() ))
            return "0";
        else
            return number;
    }

    private string[] OriginalOptionGroup
    {
        get { return (string[]) ViewState["OriginalOptionGroup"]; }
        set { ViewState["OriginalOptionGroup"] = value; }
    }

    private double ConvertProductRatingForDatabase()
    {
        if (DataAccessContext.Configurations.GetIntValue( "StarRatingAmount" ) > 0)
        {
            return ConvertUtilities.ToDouble( uxProductRating.Text.Trim() ) /
            DataAccessContext.Configurations.GetIntValue( "StarRatingAmount" );
        }
        else
            return 0;
    }

    private void PopulateSpecificationItem( Product product )
    {
        foreach (ProductSpecification item in product.ProductSpecifications)
        {
            SpecificationItem specificationItem = DataAccessContext.SpecificationItemRepository.GetOne(
                CurrentCulture, item.SpecificationItemID );

            Panel rowPanel = (Panel) uxSpecificationItemTR.FindControl( "RowPanelSpecificationGroup" + specificationItem.SpecificationGroupID );

            switch (specificationItem.Type)
            {
                case SpecificationItemControlType.Textbox:
                    TextBox text = (TextBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                    text.Text = item.Value;
                    break;
                case SpecificationItemControlType.DropDownList:
                    DropDownList dropDown = (DropDownList) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                    dropDown.SelectedValue = item.Value;
                    break;
                case SpecificationItemControlType.MultiSelect:
                    ListBox multiSelect = (ListBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                    foreach (ListItem listItem in multiSelect.Items)
                    {
                        if (listItem.Value == item.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
                    break;
                default:
                    TextBox textDefault = (TextBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                    textDefault.Text = item.Value;
                    break;
            }
        }
    }

    private void PopulateShippingCost( Product product )
    {
        IList<ShippingOption> shippingList =
            DataAccessContext.ShippingOptionRepository.GetShipping( StoreContext.Culture, BoolFilter.ShowFalse );

        for (int i = 0; i < shippingList.Count; i++)
        {
            TextBox txt = (TextBox) uxShippingCostTR.FindControl( shippingList[i].ShippingID );

            txt.Text = String.Format(
                "{0:f2}",
                product.GetOverriddenShippingCost( shippingList[i].ShippingID ) );
        }
    }

    private void PopulateProductImage( string imageSecondary )
    {
        string mapUrl;
        mapUrl = "../" + imageSecondary;

        if (File.Exists( Server.MapPath( mapUrl ) ))
        {
            if (mapUrl != "")
            {
                Bitmap mypic;
                mypic = new Bitmap( Server.MapPath( mapUrl ) );

                if (mypic.Width < MaxSmallProductImageWidth)
                    uxProductImage.Width = mypic.Width;
                else
                    uxProductImage.Width = MaxSmallProductImageWidth;
                mypic.Dispose();
            }
            uxProductImage.Visible = true;
        }
        else
        {
            uxProductImage.Visible = false;
        }
    }

    private void PopulateDefaultCheckboxVisible( string storeID )
    {
        if (storeID == "0")
        {
            uxPriceCheck.Visible = false;
            uxUseDefaultPriceLabel.Visible = false;
            uxRetailPriceCheck.Visible = false;
            uxUseDefaultRetailLabel.Visible = false;
            uxWholesaleCheck.Visible = false;
            uxUseDefaultWholesaleLabel.Visible = false;
            uxWholesale2Check.Visible = false;
            uxUseDefaultWholesale2Label.Visible = false;
            uxWholesale3Check.Visible = false;
            uxUseDefaultWholesale3Label.Visible = false;
        }
        else
        {
            uxPriceCheck.Visible = true;
            uxUseDefaultPriceLabel.Visible = true;
            uxRetailPriceCheck.Visible = true;
            uxUseDefaultRetailLabel.Visible = true;
            uxWholesaleCheck.Visible = true;
            uxUseDefaultWholesaleLabel.Visible = true;
            uxWholesale2Check.Visible = true;
            uxUseDefaultWholesale2Label.Visible = true;
            uxWholesale3Check.Visible = true;
            uxUseDefaultWholesale3Label.Visible = true;
        }
    }

    private void PopulateProductPrices( Product product, string storeID )
    {
        ProductPrice defaultProductPrice = product.GetProductPrice( "0" );
        ProductPrice productPrice = product.GetProductPrice( storeID );

        PopulateDefaultCheckboxVisible( storeID );

        uxPriceCheck.Checked = productPrice.UseDefaultPrice;
        uxRetailPriceCheck.Checked = productPrice.UseDefaultRetailPrice;
        uxWholesaleCheck.Checked = productPrice.UseDefaultWholeSalePrice;
        uxWholesale2Check.Checked = productPrice.UseDefaultWholeSalePrice2;
        uxWholesale3Check.Checked = productPrice.UseDefaultWholeSalePrice3;

        if (uxPriceCheck.Checked && storeID != "0")
        {
            uxPriceText.Enabled = false;
            uxPriceText.Text = String.Format( "{0:f2}", defaultProductPrice.Price );
        }
        else
        {
            uxPriceText.Enabled = true;
            uxPriceText.Text = String.Format( "{0:f2}", productPrice.Price );
        }

        if (uxRetailPriceCheck.Checked && storeID != "0")
        {
            uxRetailPriceText.Enabled = false;
            uxRetailPriceText.Text = String.Format( "{0:f2}", defaultProductPrice.RetailPrice );
        }
        else
        {
            uxRetailPriceText.Enabled = true;
            uxRetailPriceText.Text = String.Format( "{0:f2}", productPrice.RetailPrice );
        }

        if (uxWholesaleCheck.Checked && storeID != "0")
        {
            uxWholesalePriceText.Enabled = false;
            uxWholesalePriceText.Text = String.Format( "{0:f2}", defaultProductPrice.WholesalePrice );
        }
        else
        {
            uxWholesalePriceText.Enabled = true;
            uxWholesalePriceText.Text = String.Format( "{0:f2}", productPrice.WholesalePrice );
        }

        if (uxWholesale2Check.Checked && storeID != "0")
        {
            uxWholesalePrice2Text.Enabled = false;
            uxWholesalePrice2Text.Text = String.Format( "{0:f2}", defaultProductPrice.WholesalePrice2 );
        }
        else
        {
            uxWholesalePrice2Text.Enabled = true;
            uxWholesalePrice2Text.Text = String.Format( "{0:f2}", productPrice.WholesalePrice2 );
        }

        if (uxWholesale3Check.Checked && storeID != "0")
        {
            uxWholesalePrice3Text.Enabled = false;
            uxWholesalePrice3Text.Text = String.Format( "{0:f2}", defaultProductPrice.WholesalePrice3 );
        }
        else
        {
            uxWholesalePrice3Text.Enabled = true;
            uxWholesalePrice3Text.Text = String.Format( "{0:f2}", productPrice.WholesalePrice3 );
        }
    }

    private void SetPrice( Product product, string storeID, string storeViewID, out decimal price, out bool useDefaultPrice )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        price = VerifyPrice(
                             storeID,
                             storeViewID,
                             ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxPriceText.Text ) ),
                             product.GetProductPrice( "0" ).Price,
                             productPrice.Price,
                             productPrice.UseDefaultPrice,
                             uxPriceCheck.Checked,
                             out useDefaultPrice );
    }

    private void SetRetailPrice( Product product, string storeID, string storeViewID, out decimal retailPrice, out bool useDefaultRetailPrice )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        retailPrice = VerifyPrice(
                             storeID,
                             storeViewID,
                             ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxRetailPriceText.Text ) ),
                             product.GetProductPrice( "0" ).RetailPrice,
                             productPrice.RetailPrice,
                             productPrice.UseDefaultRetailPrice,
                             uxRetailPriceCheck.Checked,
                             out useDefaultRetailPrice );
    }

    private void SetWholesalePrice( Product product, string storeID, string storeViewID, out decimal wholesalePrice, out bool useDefaultWholesalePrice )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        wholesalePrice = VerifyPrice(
                             storeID,
                             storeViewID,
                             ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxWholesalePriceText.Text ) ),
                             product.GetProductPrice( "0" ).WholesalePrice,
                             productPrice.WholesalePrice,
                             productPrice.UseDefaultWholeSalePrice,
                             uxWholesaleCheck.Checked,
                             out useDefaultWholesalePrice );
    }

    private void SetWholesalePrice2( Product product, string storeID, string storeViewID, out decimal wholesalePrice2, out bool useDefaultWholesalePrice2 )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        wholesalePrice2 = VerifyPrice(
                             storeID,
                             storeViewID,
                             ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxWholesalePrice2Text.Text ) ),
                             product.GetProductPrice( "0" ).WholesalePrice2,
                             productPrice.WholesalePrice2,
                             productPrice.UseDefaultWholeSalePrice2,
                             uxWholesale2Check.Checked,
                             out useDefaultWholesalePrice2 );
    }

    private void SetWholesalePrice3( Product product, string storeID, string storeViewID, out decimal wholesalePrice3, out bool useDefaultWholesalePrice3 )
    {
        ProductPrice productPrice = product.GetProductPrice( storeID );

        wholesalePrice3 = VerifyPrice(
                             storeID,
                             storeViewID,
                             ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxWholesalePrice3Text.Text ) ),
                             product.GetProductPrice( "0" ).WholesalePrice3,
                             productPrice.WholesalePrice3,
                             productPrice.UseDefaultWholeSalePrice3,
                             uxWholesale3Check.Checked,
                             out useDefaultWholesalePrice3 );
    }

    private decimal VerifyPrice(
        string storeID,
        string storeViewID,
        decimal enteredPrice,
        decimal defaultPrice,
        decimal price,
        bool useDefaultPrice,
        bool enteredUseDefaultPrice,
        out bool isUseDefaultPrice )
    {
        isUseDefaultPrice = true;

        if (_isEditMode)
        {
            return ProductPrice.VerifyPrice(
                storeID, storeViewID, enteredPrice, defaultPrice, price, useDefaultPrice, enteredUseDefaultPrice, out isUseDefaultPrice );
        }
        else
        {
            return enteredPrice;
        }
    }


    private void SetProductPrice( Product product, string storeID, string storeViewID )
    {
        decimal price;
        bool useDefaultPrice;
        decimal retailPrice;
        bool useDefalutRetailPrice;
        decimal wholesalePrice;
        bool useDefaultWholesalePrice;
        decimal wholesalePrice2;
        bool useDefaultWholesalePrice2;
        decimal wholesalePrice3;
        bool useDefaultWholesalePrice3;


        SetPrice( product, storeID, storeViewID, out price, out useDefaultPrice );

        if (RetailPriceMode)
        {
            SetRetailPrice( product, storeID, storeViewID, out retailPrice, out useDefalutRetailPrice );
        }
        else
        {
            SetPrice( product, storeID, storeViewID, out retailPrice, out useDefalutRetailPrice );
        }

        if (WholesaleMode == 1)
        {
            SetWholesalePrice( product, storeID, storeViewID, out wholesalePrice, out useDefaultWholesalePrice );
        }
        else
        {
            SetPrice( product, storeID, storeViewID, out wholesalePrice, out useDefaultWholesalePrice );
        }

        SetWholesalePrice2( product, storeID, storeViewID, out wholesalePrice2, out useDefaultWholesalePrice2 );
        SetWholesalePrice3( product, storeID, storeViewID, out wholesalePrice3, out useDefaultWholesalePrice3 );

        product.SetPrice( storeID,
                    price,
                    retailPrice,
                    wholesalePrice,
                    wholesalePrice2,
                    wholesalePrice3,
                    useDefaultPrice,
                    useDefalutRetailPrice,
                    useDefaultWholesalePrice,
                    useDefaultWholesalePrice2,
                    useDefaultWholesalePrice3 );
    }

    private void SetupProductPrices( Product product, string storeID )
    {
        if (storeID != "0")
        {
            SetProductPrice( product, storeID, storeID );
        }
        else
        {
            SetProductPrice( product, "0", "0" );

            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

            foreach (Store store in storeList)
            {
                SetProductPrice( product, store.StoreID, "0" );
            }
        }
    }
    private Control GetAjaxTabControl( Control control )
    {
        if (control.GetType().ToString() == "AjaxControlToolkit.TabContainer")
            return control;
        if (control.Parent == null)
            return null;
        return GetAjaxTabControl( control.Parent );
    }

    private void InitSpecificationGroup()
    {
        uxSpecificationGroupDrop.DataSource = DataAccessContext.SpecificationGroupRepository.GetAll( CurrentCulture );
        uxSpecificationGroupDrop.DataTextField = "Name";
        uxSpecificationGroupDrop.DataValueField = "SpecificationGroupID";
        uxSpecificationGroupDrop.DataBind();
        uxSpecificationGroupDrop.Items.Insert( 0, new ListItem( "None", "0" ) );
    }

    private void InitManufacturerList()
    {
        IList<Manufacturer> manufacturerList = DataAccessContext.ManufacturerRepository.GetAll( CurrentCulture, BoolFilter.ShowAll, "SortOrder" );
        uxManufacturerDrop.Items.Add( new ListItem( "None", "" ) );
        foreach (Manufacturer manufacturer in manufacturerList)
        {
            if (manufacturer.IsEnabled)
                uxManufacturerDrop.Items.Add( new ListItem( manufacturer.Name, manufacturer.ManufacturerID ) );
        }
    }

    #endregion

    #region Protected

    protected void uxDownloadPathLinkButton_Click( object sender, EventArgs e )
    {
        uxDownloadPathUpload.ShowControl = true;
    }

    protected void uxOptionalUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxOptionalUpload.ShowControl = true;
    }

    protected void QuantityValidator( object source, ServerValidateEventArgs args )
    {
        if (String.IsNullOrEmpty( uxMinimumQuantityText.Text ) && String.IsNullOrEmpty( uxMaximumQuantityText.Text ))
        {
            args.IsValid = true;
            return;
        }

        int minQuantity = ConvertUtilities.ToInt32( uxMinimumQuantityText.Text );
        int maxQuantity = ConvertUtilities.ToInt32( uxMaximumQuantityText.Text );

        if (maxQuantity != 0 && minQuantity > maxQuantity)
        {
            args.IsValid = false;
            return;
        }

        args.IsValid = true;
    }

    protected void CustomPriceValidator( object source, ServerValidateEventArgs args )
    {
        decimal defaultPrice = ConvertUtilities.ToDecimal( uxDefaultPriceText.Text );
        decimal minimumPrice = ConvertUtilities.ToDecimal( uxMinimumPriceText.Text );

        if (minimumPrice > defaultPrice)
        {
            args.IsValid = false;
            return;
        }

        args.IsValid = true;
    }

    protected void uxAddImagesButton_Click( object sender, EventArgs e )
    {
        Control control = GetAjaxTabControl( this );

        AjaxControlToolkit.TabContainer tabControl = (AjaxControlToolkit.TabContainer) control;
        tabControl.ActiveTab = tabControl.Tabs[1];
    }

    protected void uxSpecificationGroupDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SetVisibleSpecificationControls();
    }

    protected void uxPriceCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxPriceCheck.Checked)
        {
            uxPriceText.Enabled = false;
        }
        else
        {
            uxPriceText.Enabled = true;
        }
    }

    protected void uxRetailPriceCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxRetailPriceCheck.Checked)
        {
            uxRetailPriceText.Enabled = false;
        }
        else
        {
            uxRetailPriceText.Enabled = true;
        }
    }

    protected void uxWholesaleCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxWholesaleCheck.Checked)
        {
            uxWholesalePriceText.Enabled = false;
        }
        else
        {
            uxWholesalePriceText.Enabled = true;
        }
    }

    protected void uxWholesaleCheck2_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxWholesale2Check.Checked)
        {
            uxWholesalePrice2Text.Enabled = false;
        }
        else
        {
            uxWholesalePrice2Text.Enabled = true;
        }
    }

    protected void uxWholesaleCheck3_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxWholesale3Check.Checked)
        {
            uxWholesalePrice3Text.Enabled = false;
        }
        else
        {
            uxWholesalePrice3Text.Enabled = true;
        }
    }

    protected void uxUseCustomPriceCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxUseCustomPriceCheck.Checked)
        {
            uxUseCustomPriceTR.Visible = true;
            RelatedPricePanelHide();
            uxIsDynamicProductKitPriceCheck.Enabled = false;
            uxCallForPricePanel.Visible = false;
            uxIsCallForPriceCheck.Checked = false;
        }
        else
        {
            uxUseCustomPriceTR.Visible = false;
            RelatedPricePanelShow();
            uxIsDynamicProductKitPriceCheck.Enabled = true;
            uxCallForPricePanel.Visible = true;
        }
    }

    protected void uxIsCallForPriceCheck_OnCheckedChanged( object sender, EventArgs e )
    {
        if (uxIsCallForPriceCheck.Checked)
        {
            uxUseCustomPriceTR.Visible = false;
            uxUseCustomPriceCheck.Checked = false;
        }
        else
        {
            uxUseCustomPriceTR.Visible = true;
        }
    }

    protected void uxIncludeVatLabel_PreRender( object sender, EventArgs e )
    {
        uxIncludeVatLabel.Visible = DataAccessContext.Configurations.GetBoolValue( "TaxIncludedInPrice" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxProductDetailsLayoutPathPanel.Style["display"] = "none";
        uxOverrideProductLayoutDrop.Attributes.Add( "onchange", String.Format( "ShowHideObject('{0}')", uxProductDetailsLayoutPathPanel.ClientID ) );

        if (!MainContext.IsPostBack)
        {
            InitSpecificationGroup();
            InitManufacturerList();
            uxOptionalUpload.PathDestination = _pathUploadProduct;
            uxDownloadPathUpload.PathDestination = _pathUploadProductFile;
            uxOptionalUpload.ReturnTextControlClientID = uxImageSecondaryText.ClientID;
            uxDownloadPathUpload.ReturnTextControlClientID = uxDownloadPathText.ClientID;

            if (_isEditMode)
            {
                AddImagesTR.Visible = false;
                uxProductSeo.Visible = false;
                uxCreateDatePanel.Visible = true;
                uxViewCountPanel.Visible = true;
            }
            else
            {
                uxPriceCheck.Visible = false;
                uxUseDefaultPriceLabel.Visible = false;
                uxRetailPriceCheck.Visible = false;
                uxUseDefaultRetailLabel.Visible = false;
                uxWholesaleCheck.Visible = false;
                uxUseDefaultWholesaleLabel.Visible = false;
                uxWholesale2Check.Visible = false;
                uxUseDefaultWholesale2Label.Visible = false;
                uxWholesale3Check.Visible = false;
                uxUseDefaultWholesale3Label.Visible = false;
                uxMinimumQuantityText.Text = "1";
                uxMaximumQuantityText.Text = "0";
                uxRmaText.Text = "0";
                uxCreateDatePanel.Visible = false;
                uxViewCountPanel.Visible = false;
            }
        }
    }

    #endregion

    #region Public Methods

    public string Price { get { return FormatNumber( CreateNonEmptyNumber( uxPriceText.Text ) ); } }
    public string RetailPrice { get { return FormatNumber( CreateNonEmptyNumber( uxRetailPriceText.Text ) ); } }
    public string WholeSalePrice { get { return FormatNumber( CreateNonEmptyNumber( uxWholesalePriceText.Text ) ); } }
    public string WholeSalePrice2 { get { return FormatNumber( CreateNonEmptyNumber( uxWholesalePrice2Text.Text ) ); } }
    public string WholeSalePrice3 { get { return FormatNumber( CreateNonEmptyNumber( uxWholesalePrice3Text.Text ) ); } }
    public string ImageSecondary { get { return FormatNumber( CreateNonEmptyNumber( uxImageSecondaryText.Text ) ); } }
    public int WholesaleMode { get { return int.Parse( DataAccessContext.Configurations.GetValue( "WholesaleMode" ) ); } }
    public int WholesaleLevel { get { return int.Parse( DataAccessContext.Configurations.GetValue( "WholesaleLevel" ) ); } }
    public bool RetailPriceMode { get { return DataAccessContext.Configurations.GetBoolValue( "RetailPriceMode" ); } }

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return null;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
            uxProductSeo.CurrentCulture = value;
            uxInventoryAndOption.CurrentCulture = value;
        }
    }

    public void PopulateStockOptionControl()
    {
        uxInventoryAndOption.PopulateStockOptionControl();
    }

    public void PopulateStockVisibility()
    {
        uxInventoryAndOption.PopulateStockVisibility();
    }

    public void SetEditMode( bool isEditMode )
    {
        _isEditMode = isEditMode;
        uxInventoryAndOption.IsEditMode = isEditMode;
    }

    public void ClearInputFields()
    {
        uxInventoryAndOption.ClearInputFields();
        uxProductSeo.ClearInputFields();
        uxSkuText.Text = "";
        uxBrandText.Text = "";
        uxModelText.Text = "";
        uxImageSecondaryText.Text = "";

        uxWeightText.Text = "";
        uxFixedShippingCostDrop.SelectedValue = "False";
        uxIsFreeShippingCostCheck.Checked = false;
        uxWholesalePriceText.Text = "";
        uxWholesalePrice2Text.Text = "";
        uxWholesalePrice3Text.Text = "";
        uxPriceText.Text = "";
        uxRetailPriceText.Text = "";
        uxKeywordsText.Text = "";
        uxIsTodaySpecialCheck.Checked = false;
        uxDownloadableCheck.Checked = false;
        uxDownloadPathText.Text = "";
        uxIsEnabledCheck.Checked = true;
        uxQuantityDiscount.Clear();
        uxMinimumQuantityText.Text = "1";
        uxMaximumQuantityText.Text = "0";
        uxRelatedProducts.Text = "";

        uxImageSecondaryText.Text = "";
        uxProductImage.ImageUrl = "";
        uxProductImage.Visible = false;
        uxManufacturerPartNumberText.Text = "";
        uxUpcText.Text = "";
        uxProductRating.Text = "";

        uxTaxClassDrop.SelectedValue = TaxClass.NonTaxClassID;
        uxOtherOneText.Text = "";
        uxOtherTwoText.Text = "";
        uxOtherThreeText.Text = "";
        uxOtherFourText.Text = "";
        uxOtherFiveText.Text = "";
        uxViewCount.Text = "";
        uxCreateDateTime.Text = "";
        uxIsAffiliate.Checked = false;

        uxOverrideProductLayoutDrop.SelectedValue = "False";
        uxProductDetailsLayoutPathDrop.SelectedValue = "";
        uxProductDetailsLayoutPathPanel.Style["display"] = "none";

        uxUseCustomPriceCheck.Checked = false;
        uxDefaultPriceText.Text = "";
        uxMinimumPriceText.Text = "";

        uxIsCallForPriceCheck.Checked = false;
        uxSpecificationGroupDrop.SelectedIndex = 0;

        uxRmaText.Text = "0";

        uxIsDynamicProductKitPriceCheck.Checked = false;
        uxIsDynamicProductKitWeightCheck.Checked = false;
        uxUseCustomPriceCheck.Enabled = true;
        uxWeightText.Enabled = true;
        uxWholesalePriceText.Enabled = true;
        uxWholesalePrice2Text.Enabled = true;
        uxWholesalePrice3Text.Enabled = true;
        uxPriceText.Enabled = true;
        uxRetailPriceText.Enabled = true;

        uxManufacturerDrop.SelectedIndex = 0;

        uxLengthText.Text = "";
        uxWidthText.Text = "";
        uxHeightText.Text = "";
    }

    private string GetProductSpecificationGroupID( Product product )
    {
        foreach (ProductSpecification item in product.ProductSpecifications)
        {
            SpecificationItem specificationItem = DataAccessContext.SpecificationItemRepository.GetOne(
               CurrentCulture, item.SpecificationItemID );

            return specificationItem.SpecificationGroupID;
        }

        return "0";
    }

    public void PopulateControls( Product product, string storeID )
    {
        PopulateShippingCost( product );
        PopulateProductPrices( product, storeID );
        uxSpecificationGroupDrop.SelectedValue = GetProductSpecificationGroupID( product );
        PopulateSpecificationItem( product );
        SetVisibleSpecificationControls();
        SetVisibleRmaControl();
        uxInventoryAndOption.PopulateControls( product );

        uxSkuText.Text = product.Sku;
        uxBrandText.Text = product.Brand;
        uxModelText.Text = product.Model;
        uxImageSecondaryText.Text = product.ImageSecondary;

        uxWeightText.Text = product.Weight.ToString();
        uxFixedShippingCostDrop.SelectedValue = product.FixedShippingCost.ToString();
        uxIsFreeShippingCostCheck.Checked = product.FreeShippingCost;

        uxKeywordsText.Text = product.Keywords;
        uxProductImage.ImageUrl = AdminConfig.UrlFront + product.ImageSecondary;
        uxProductRating.Text =
            Convert.ToString( Convert.ToDouble( product.MerchantRating ) *
            Convert.ToInt32( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) ) );

        PopulateProductImage( product.ImageSecondary );

        uxIsTodaySpecialCheck.Checked = product.IsTodaySpecial;

        uxDownloadableCheck.Checked = product.IsDownloadable;
        uxDownloadPathText.Text = product.DownloadPath;
        uxIsEnabledCheck.Checked = product.IsEnabled;
        uxMinimumQuantityText.Text = product.MinQuantity.ToString();
        uxMaximumQuantityText.Text = product.MaxQuantity.ToString();
        uxRelatedProducts.Text = product.RelatedProducts;
        uxQuantityDiscount.Refresh( product.DiscountGroupID );

        uxManufacturerPartNumberText.Text = product.ManufacturerPartNumber;
        uxUpcText.Text = product.Upc;
        uxTaxClassDrop.SelectedValue = product.TaxClassID;

        uxOtherOneText.Text = product.Other1;
        uxOtherTwoText.Text = product.Other2;
        uxOtherThreeText.Text = product.Other3;
        uxOtherFourText.Text = product.Other4;
        uxOtherFiveText.Text = product.Other5;
        uxViewCount.Text = product.ViewCount.ToString();
        uxCrateDateCalendar.SelectedDateText = product.CreateDate.ToShortDateString();
        uxCrateDateCalendar.Visible = true;
        uxCreateDateTime.Visible = false;
        uxIsAffiliate.Checked = product.IsAffiliate;

        uxIsCallForPriceCheck.Checked = product.IsCallForPrice;
        if (uxIsCallForPriceCheck.Checked)
            uxUseCustomPriceTR.Visible = false;

        uxUseCustomPriceCheck.Checked = product.IsCustomPrice;
        if (uxUseCustomPriceCheck.Checked)
            uxCallForPricePanel.Visible = false;

        uxDefaultPriceText.Text = String.Format( "{0:f2}", product.ProductCustomPrice.DefaultPrice );
        uxMinimumPriceText.Text = String.Format( "{0:f2}", product.ProductCustomPrice.MinimumPrice );
        uxRmaText.Text = product.ReturnTime.ToString();

        if (!String.IsNullOrEmpty( product.ProductDetailsLayoutPath ))
        {
            uxOverrideProductLayoutDrop.SelectedValue = "True";
            uxProductDetailsLayoutPathDrop.SelectedValue = product.ProductDetailsLayoutPath;
            uxProductDetailsLayoutPathPanel.Style["display"] = "";
        }
        else
        {
            uxOverrideProductLayoutDrop.SelectedValue = "False";
            uxProductDetailsLayoutPathPanel.Style["display"] = "none";
            uxProductDetailsLayoutPathDrop.SelectedValue = "";
        }

        uxIsDynamicProductKitPriceCheck.Checked = product.IsDynamicProductKitPrice;
        uxIsDynamicProductKitWeightCheck.Checked = product.IsDynamicProductKitWeight;

        if (uxManufacturerDrop.Items.FindByValue( product.Manufacturer ) != null)
            uxManufacturerDrop.SelectedValue = product.Manufacturer;
        else
            uxManufacturerDrop.SelectedValue = "";

        uxLengthText.Text = product.Length.ToString();
        uxWidthText.Text = product.Width.ToString();
        uxHeightText.Text = product.Height.ToString();
    }

    public void SetDisplayControls()
    {
        uxInventoryAndOption.SetUpDisplay();
        uxDownloadPathTR.Visible = true;
        uxIsDownloadableTR.Visible = true;
        uxRelateProductTR.Visible = true;
        uxWeightTR.Visible = true;
        uxIsEnabledTR.Visible = true;
    }

    public Product Setup( Product product, string storeID )
    {
        if (!_isEditMode)
        {
            product = uxProductSeo.Setup( product, storeID );
        }

        SetupProductPrices( product, storeID );

        product.Sku = uxSkuText.Text;
        product.Brand = uxBrandText.Text;
        product.Model = uxModelText.Text;
        product.ImageSecondary = ImageSecondary;
        product.Weight = ConvertUtilities.ToDouble( CreateNonEmptyNumber( uxWeightText.Text ) );

        product.FixedShippingCost = bool.Parse( uxFixedShippingCostDrop.SelectedValue );
        product.FreeShippingCost = uxIsFreeShippingCostCheck.Checked;

        product.Manufacturer = uxManufacturerDrop.SelectedValue;
        product.Keywords = uxKeywordsText.Text;
        product.Other1 = uxOtherOneText.Text;
        product.Other2 = uxOtherTwoText.Text;
        product.Other3 = uxOtherThreeText.Text;
        product.Other4 = uxOtherFourText.Text;
        product.Other5 = uxOtherFiveText.Text;

        if (uxIsAffiliate.Visible)
        {
            product.IsAffiliate = uxIsAffiliate.Checked;
        }

        product.IsTodaySpecial = uxIsTodaySpecialCheck.Checked;
        product.IsCustomPrice = uxUseCustomPriceCheck.Checked;

        if (!String.IsNullOrEmpty( uxMinimumQuantityText.Text ) && !String.IsNullOrEmpty( uxMaximumQuantityText.Text ))
        {
            product.MinQuantity = ConvertUtilities.ToInt32( uxMinimumQuantityText.Text );
            product.MaxQuantity = ConvertUtilities.ToInt32( uxMaximumQuantityText.Text );
        }

        product.IsDownloadable = uxDownloadableCheck.Checked;
        product.DownloadPath = uxDownloadPathText.Text;
        product.IsEnabled = uxIsEnabledCheck.Checked;
        product.RelatedProducts = uxRelatedProducts.Text.Trim().Replace( " ", "" );
        product.MerchantRating = ConvertProductRatingForDatabase();
        product.DiscountGroupID = uxQuantityDiscount.DiscountGounpID;

        product.ManufacturerPartNumber = uxManufacturerPartNumberText.Text;
        product.Upc = uxUpcText.Text;
        product.IsCallForPrice = uxIsCallForPriceCheck.Checked;
        if (uxCustomPriceTR.Visible)
        {
            product.IsCustomPrice = uxUseCustomPriceCheck.Checked;
            /* Save Product Custom Price */
            ProductCustomPrice productCustomPrice = new ProductCustomPrice();
            productCustomPrice.DefaultPrice = ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxDefaultPriceText.Text ) );
            productCustomPrice.MinimumPrice = ConvertUtilities.ToDecimal( CreateNonEmptyNumber( uxMinimumPriceText.Text ) );
            product.ProductCustomPrice = productCustomPrice;
        }
        else
        {
            product.IsCustomPrice = false;
        }

        product.TaxClassID = uxTaxClassDrop.SelectedValue;

        product.UseInventory = uxInventoryAndOption.IsUseInventory;

        if (ConvertUtilities.ToBoolean( uxOverrideProductLayoutDrop.SelectedValue ))
        {
            product.ProductDetailsLayoutPath = uxProductDetailsLayoutPathDrop.SelectedValue;
            uxProductDetailsLayoutPathPanel.Style["display"] = "";
        }
        else
        {
            product.ProductDetailsLayoutPath = String.Empty;
            uxProductDetailsLayoutPathPanel.Style["display"] = "none";
        }

        if (uxRmaTR.Visible)
        {
            product.ReturnTime = ConvertUtilities.ToInt32( uxRmaText.Text );
        }
        else
        {
            product.ReturnTime = 0;
        }

        product.IsDynamicProductKitPrice = uxIsDynamicProductKitPriceCheck.Checked;
        product.IsDynamicProductKitWeight = uxIsDynamicProductKitWeightCheck.Checked;
        if (uxCrateDateCalendar.Visible)
        {
            product.CreateDate = uxCrateDateCalendar.SelectedDate;
        }

        product.Length = ConvertUtilities.ToDouble( CreateNonEmptyNumber( uxLengthText.Text ) );
        product.Width = ConvertUtilities.ToDouble( CreateNonEmptyNumber( uxWidthText.Text ) );
        product.Height = ConvertUtilities.ToDouble( CreateNonEmptyNumber( uxHeightText.Text ) );

        return product;
    }

    public void IsDownloadableEnabled( bool isEnabled )
    {
        if (!isEnabled)
        {
            uxDownloadableCheck.Checked = false;
            uxDownloadableCheck.Enabled = false;
        }
        else
        {
            uxDownloadableCheck.Enabled = true;
        }
    }

    public void SetEnabledControlsForProductSubscription( bool isEnabled )
    {
        if (!isEnabled)
        {
            uxInventoryAndOption.IsProductOptionVisible( false );
            uxInventoryAndOption.IsStockOptionVisible( false );
            uxIsFreeShippingCostCheck.Checked = false;
            uxIsFreeShippingCostCheck.Enabled = false;
            uxFixedShippingCostDrop.SelectedValue = "False";
            uxFixedShippingCostDrop.Enabled = false;

        }
        else
        {
            uxInventoryAndOption.IsProductOptionVisible( true );
            uxInventoryAndOption.IsStockOptionVisible( true );
            uxIsFreeShippingCostCheck.Enabled = true;
            uxFixedShippingCostDrop.Enabled = true;
        }
    }

    public void SetEnabledWeightText( bool enable )
    {
        uxWeightText.Enabled = enable;
    }

    public void SetEnabledMinMaxQTY( bool enable )
    {
        uxMinimumQuantityText.Enabled = enable;
        uxMaximumQuantityText.Enabled = enable;
    }

    public void SetMaxQuantity( int max )
    {
        uxMaximumQuantityText.Text = max.ToString();
    }

    public void SetMinQuantity( int min )
    {
        uxMinimumQuantityText.Text = min.ToString();
    }

    public void SetEnabledQuantityDiscount( bool enabled )
    {
        uxQuantityDiscount.IsQuantityDiscountEnabled( enabled );
    }

    public void SetVisibleSpecificationControls()
    {
        if (uxSpecificationGroupDrop.SelectedValue != "0")
        {
            uxSpecificationItemTR.Visible = true;
            IList<SpecificationGroup> groups = DataAccessContext.SpecificationGroupRepository.GetAll( CurrentCulture );
            foreach (SpecificationGroup group in groups)
            {
                Panel rowPanel = (Panel) uxSpecificationItemTR.FindControl( "RowPanelSpecificationGroup" + group.SpecificationGroupID );
                if (group.SpecificationGroupID == uxSpecificationGroupDrop.SelectedValue)
                    rowPanel.Visible = true;
                else
                    rowPanel.Visible = false;
            }

        }
        else
            uxSpecificationItemTR.Visible = false;
    }

    public void SetVisibleRmaControl()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "EnableRMA" ))
        {
            uxRmaTR.Visible = true;
        }
        else
        {
            uxRmaTR.Visible = false;
        }
    }

    public void SetFixedShippingCostVisibility( bool isGiftCertificate )
    {
        if (uxDownloadableCheck.Checked || isGiftCertificate)
        {
            uxFixedShippingCostTR.Visible = false;
            uxShippingCostTR.Visible = false;
            uxIsFreeShippingCostTR.Visible = false;
        }
        else
        {
            uxIsFreeShippingCostTR.Visible = true;
            uxFixedShippingCostTR.Visible = true;
            uxShippingCostTR.Visible = true;

            if (uxIsFreeShippingCostCheck.Checked)
            {
                uxFixedShippingCostTR.Visible = false;
                uxShippingCostTR.Visible = false;
            }
            else
            {
                if (ConvertUtilities.ToBoolean( uxFixedShippingCostDrop.SelectedValue ))
                    uxShippingCostTR.Visible = true;
                else
                    uxShippingCostTR.Visible = false;
            }
        }

    }

    public bool IsFixPrice( bool isFixedPrice, bool isGiftCertificate, bool isRecurring, bool isCallForPrice )
    {
        bool isFixPrice = true;
        //check donation display with gift certificate
        if (isGiftCertificate || isRecurring)
        {
            IsProductCustomPrice( isGiftCertificate, isRecurring, isCallForPrice );
            if (isFixedPrice)
            {
                if (isCustomPrice)
                {
                    RelatedPricePanelShow();
                }
            }
            else
            {
                RelatedPricePanelHide();
                isFixPrice = false;
            }

        }
        else
        {
            IsProductCustomPrice( isGiftCertificate, isRecurring, isCallForPrice );
            if (isCustomPrice)
            {
                RelatedPricePanelHide();
            }
            else
            {
                RelatedPricePanelShow();
            }
        }
        return isFixPrice;

    }

    public bool IsProductSkuExist()
    {
        string sku = uxSkuText.Text;
        string productID = DataAccessContext.ProductRepository.GetProductIDBySku( sku );

        if (String.IsNullOrEmpty( productID ))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // check donation display with gift certificate
    private void IsProductCustomPrice( bool isGiftCertificate, bool isRecurring, bool isCallForPrice )
    {
        if (isGiftCertificate || isRecurring || isCallForPrice)
        {
            uxUseCustomPriceTR.Visible = false;
            uxCustomPriceTR.Visible = false;
        }
        else
        {
            uxUseCustomPriceTR.Visible = true;
            uxCustomPriceTR.Visible = isCustomPrice;
            uxRetailPriceRequiredValidator.Enabled = false;
            uxPriceRequiredValidator.Enabled = false;
        }
    }
    private void RelatedPricePanelHide()
    {
        uxPriceTR.Visible = false;
        uxRetailPriceTR.Style.Add( "display", "none" );
        uxWholesalePriceTR.Style.Add( "display", "none" );
        uxWholesalePrice2TR.Style.Add( "display", "none" );
        uxWholesalePrice3TR.Style.Add( "display", "none" );
    }
    private void RelatedPricePanelShow()
    {
        uxPriceTR.Visible = true;
        uxRetailPriceTR.Style.Add( "display", "" );
        uxWholesalePriceTR.Style.Add( "display", "" );
        uxWholesalePrice2TR.Style.Add( "display", "" );
        uxWholesalePrice3TR.Style.Add( "display", "" );
    }
    // End Donation

    private TextBox PopulateTextbox( string specItemID )
    {
        TextBox txtBox = new TextBox();
        txtBox.ID = "SpecificationItem" + specItemID;
        txtBox.CssClass = "TextBox";
        return txtBox;
    }

    private DropDownList PopulateDropDownList( string specItemID )
    {
        DropDownList dropList = new DropDownList();
        dropList.ID = "SpecificationItem" + specItemID;
        dropList.CssClass = "DropDown";

        dropList.DataSource = DataAccessContext.SpecificationItemValueRepository.GetBySpecificationItemID( CurrentCulture, specItemID );
        dropList.DataTextField = "DisplayValue";
        dropList.DataValueField = "Value";
        dropList.DataBind();

        dropList.Items.Insert( 0, new ListItem( "None", "" ) );

        return dropList;
    }

    private ListBox PopulateMultiSelect( string specItemID )
    {
        ListBox listBox = new ListBox();
        listBox.SelectionMode = ListSelectionMode.Multiple;
        listBox.ID = "SpecificationItem" + specItemID;
        listBox.CssClass = "MultiCatalogInnerListBox";

        listBox.DataSource = DataAccessContext.SpecificationItemValueRepository.GetBySpecificationItemID( CurrentCulture, specItemID );
        listBox.DataTextField = "DisplayValue";
        listBox.DataValueField = "Value";
        listBox.DataBind();

        return listBox;
    }

    public void PopulateShippingCostControl()
    {
        int i = 0;

        IList<ShippingOption> nonRealTimeShippings = DataAccessContext.ShippingOptionRepository.GetShipping(
            CurrentCulture, BoolFilter.ShowFalse );

        for (int rowIndex = 0; rowIndex < nonRealTimeShippings.Count; rowIndex++)
        {
            ShippingOption shippingOption = nonRealTimeShippings[rowIndex];
            Panel rowPanel = new Panel();
            rowPanel.ID = "RowPanel" + shippingOption.ShippingID;
            rowPanel.CssClass = "CommonRowStyle";

            uxShippingCostTR.Controls.Add( rowPanel );

            Panel panel = new Panel();
            panel.ID = "NamePanel" + shippingOption.ShippingID;
            panel.CssClass = "Label";

            Label label = new Label();
            label.ID = "NameLabel" + shippingOption.ShippingID;
            label.Text = "  " + shippingOption.ShippingName;
            label.CssClass = " BulletLabel";

            panel.Controls.Add( label );

            rowPanel.Controls.Add( panel );

            TextBox txtBox = new TextBox();
            txtBox.ID = shippingOption.ShippingID;
            txtBox.CssClass = "TextBox";
            rowPanel.Controls.Add( txtBox );

            panel = new Panel();
            panel.CssClass = "Clear";
            rowPanel.Controls.Add( panel );

            label = new Label();
            label.ID = "Label" + shippingOption.ShippingID; ;
            label.Text += "";
            rowPanel.Controls.Add( label );
            i++;
        }
    }

    public void PopulateSpecificationItemControls()
    {
        uxSpecificationItemTR.Controls.Clear();
        IList<SpecificationGroup> groups = DataAccessContext.SpecificationGroupRepository.GetAll( CurrentCulture );
        foreach (SpecificationGroup group in groups)
        {
            IList<SpecificationItem> items = DataAccessContext.SpecificationItemRepository.GetBySpecificationGroupID(
                CurrentCulture, group.SpecificationGroupID );

            Panel rowPanel = new Panel();
            rowPanel.ID = "RowPanelSpecificationGroup" + group.SpecificationGroupID;
            rowPanel.CssClass = "CommonRowStyle";

            foreach (SpecificationItem item in items)
            {
                Label label = new Label();
                label.ID = "SpecificationNameLabel" + item.SpecificationItemID;
                label.Text = "  " + item.Name;
                label.CssClass = " BulletLabel";

                rowPanel.Controls.Add( label );

                Control specItemControl = new Control();

                switch (item.Type)
                {
                    case SpecificationItemControlType.Textbox:
                        specItemControl = (Control) PopulateTextbox( item.SpecificationItemID );
                        break;
                    case SpecificationItemControlType.DropDownList:
                        specItemControl = (Control) PopulateDropDownList( item.SpecificationItemID );
                        break;
                    case SpecificationItemControlType.MultiSelect:
                        specItemControl = (Control) PopulateMultiSelect( item.SpecificationItemID );
                        break;
                    default:
                        specItemControl = (Control) PopulateTextbox( item.SpecificationItemID );
                        break;
                }

                rowPanel.Controls.Add( specItemControl );

                Panel panel = new Panel();
                panel.CssClass = "Clear";
                rowPanel.Controls.Add( panel );
            }

            uxSpecificationItemTR.Controls.Add( rowPanel );
        }

        SetVisibleSpecificationControls();
    }

    public void RestoreSessionData()
    {
        if (IsRestoreSession)
        {
            InputControlRecorder inputRecorder = (InputControlRecorder) Session[ProductDetailsSessionName];
            inputRecorder.Restore( this );

            PopulateStockOptionControl();

            inputRecorder.Restore( this );

            Session[ProductDetailsSessionName] = null;

            if (!_isEditMode)
            {
                if (!String.IsNullOrEmpty( ProductImageData.SecondaryImagePath ))
                    uxImageSecondaryText.Text = ProductImageData.SecondaryImagePath;
            }
            else
            {
                uxImageSecondaryText.Text = DataAccessContext.ProductRepository.GetOne( CurrentCulture, CurrentID, new StoreRetriever().GetCurrentStoreID() ).ImageSecondary;
            }

            uxProductImage.ImageUrl = AdminConfig.UrlFront + uxImageSecondaryText.Text +
                "?State=" + RandomUtilities.RandomNumberNDigits( 3 );
        }
    }

    public void SetOptionList()
    {
        uxInventoryAndOption.SetOptionList();
    }

    public void SelectOptionList( Product product )
    {
        uxInventoryAndOption.SelectOptionList( product );
    }

    public bool VerifyInputListOption()
    {
        return uxInventoryAndOption.VerifyInputListOption();
    }

    public void AddOptionGroup( Product product )
    {
        uxInventoryAndOption.AddOptionGroup( product );
    }

    public void CreateStockOption( Product product )
    {
        uxInventoryAndOption.CreateStockOption( product );
    }

    public void UpdateProductShippingCost( Product product )
    {
        IList<ShippingOption> shippingList =
            DataAccessContext.ShippingOptionRepository.GetShipping( CurrentCulture, BoolFilter.ShowFalse );

        for (int i = 0; i < shippingList.Count; i++)
        {
            TextBox txt = (TextBox) uxShippingCostTR.FindControl( shippingList[i].ShippingID );

            ProductShippingCost productShippingCost = new ProductShippingCost();
            productShippingCost.ShippingID = shippingList[i].ShippingID;
            productShippingCost.FixedShippingCost = ConvertUtilities.ToDecimal( txt.Text );

            product.ProductShippingCosts.Add( productShippingCost );
        }
    }

    public void HideUploadButton()
    {
        uxOptionalUploadLinkButton.Visible = false;
        uxDownloadPathLinkButton.Visible = false;
    }

    public void PopulateDropdown()
    {
        string[] fileList = Directory.GetFiles( Server.MapPath( SystemConst.LayoutProductDetailsPath ) );
        uxProductDetailsLayoutPathDrop.Items.Clear();
        uxProductDetailsLayoutPathDrop.Items.Add( new ListItem( "Plese Select...", "" ) );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxProductDetailsLayoutPathDrop.Items.Add( new ListItem( itemName, itemName ) );
            }
        }
        uxProductDetailsLayoutPathDrop.SelectedValue = DataAccessContext.Configurations.GetValueNoThrow( "DefaultProductDetailsLayout" );
    }

    public void HideStockOption()
    {
        uxInventoryAndOption.HideStockOption();
    }

    public void SetWholesaleVisible( bool isFixedPrice, bool isGiftCertificate, bool isRecurring )
    {
        if (WholesaleMode == 1 && IsFixPrice( isFixedPrice, isGiftCertificate, isRecurring, IsCallForPrice ))
        {
            if (WholesaleLevel >= 1)
                uxWholesalePriceTR.Visible = true;
            else
                uxWholesalePriceTR.Visible = false;
            if (WholesaleLevel >= 2)
                uxWholesalePrice2TR.Visible = true;
            else
                uxWholesalePrice2TR.Visible = false;
            if (WholesaleLevel >= 3)
                uxWholesalePrice3TR.Visible = true;
            else
                uxWholesalePrice3TR.Visible = false;

        }
        else
        {
            uxWholesalePriceTR.Visible = false;
            uxWholesalePrice2TR.Visible = false;
            uxWholesalePrice3TR.Visible = false;
        }
    }

    public void SetRetailPriceVisible( bool isFixedPrice, bool isGiftCertificate, bool isRecurring )
    {
        if (RetailPriceMode && IsFixPrice( isFixedPrice, isGiftCertificate, isRecurring, IsCallForPrice ))
            uxRetailPriceTR.Visible = true;
        else
            uxRetailPriceTR.Visible = false;
    }

    public void SetTaxClassVisible( bool isVisible )
    {
        uxTaxClassTR.Visible = isVisible;
    }

    public void SetProductRatingVisible( bool isGiftCertificate )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "MerchantRating" )
            && !isGiftCertificate)
        {
            uxProductRatingRow.Visible = true;
        }
        else
        {
            uxProductRatingRow.Visible = false;
            uxProductRating.Text = String.Empty;
        }
    }

    public void SetProductKitControlVisible( bool isProductKit )
    {
        uxIsDynamicProductKitPricePanel.Visible = isProductKit;
        uxIsDynamicProductKitWeightPanel.Visible = isProductKit;

        if (!isProductKit)
        {
            uxIsDynamicProductKitPriceCheck.Checked = false;
            uxIsDynamicProductKitWeightCheck.Checked = false;
        }
    }

    public void SetStockWhenAdd()
    {
        uxInventoryAndOption.SetStockWhenAdd();
    }

    public void InitDiscountDrop()
    {
        uxQuantityDiscountTR.Visible = true;
    }

    public void InitTaxClassDrop()
    {
        uxTaxClassDrop.DataSource = DataAccessContext.TaxClassRepository.GetAll( "TaxClassID" );
        uxTaxClassDrop.DataTextField = "TaxClassName";
        uxTaxClassDrop.DataValueField = "TaxClassID";
        uxTaxClassDrop.DataBind();
        uxTaxClassDrop.Items.Insert( 0, new ListItem( "None", "0" ) );
    }

    public void SetProductSpecifications( Product product )
    {
        product.ProductSpecifications = new List<ProductSpecification>();
        if (uxSpecificationGroupDrop.SelectedValue != "0")
        {
            IList<SpecificationItem> items = DataAccessContext.SpecificationItemRepository.GetBySpecificationGroupID(
                CurrentCulture, uxSpecificationGroupDrop.SelectedValue );

            Panel rowPanel = (Panel) uxSpecificationItemTR.FindControl( "RowPanelSpecificationGroup" + uxSpecificationGroupDrop.SelectedValue );

            foreach (SpecificationItem item in items)
            {
                ProductSpecification productSpecification = new ProductSpecification();
                productSpecification.ProductID = CurrentID;
                productSpecification.SpecificationItemID = item.SpecificationItemID;

                switch (item.Type)
                {
                    case SpecificationItemControlType.Textbox:
                        TextBox text = (TextBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                        productSpecification.Value = text.Text;
                        product.ProductSpecifications.Add( productSpecification );
                        break;
                    case SpecificationItemControlType.DropDownList:
                        DropDownList dropDown = (DropDownList) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                        productSpecification.Value = dropDown.SelectedValue;
                        product.ProductSpecifications.Add( productSpecification );
                        break;
                    case SpecificationItemControlType.MultiSelect:
                        ListBox multiSelect = (ListBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                        string selectedItem = string.Empty;
                        IList<ProductSpecification> productSpecList = new List<ProductSpecification>();
                        foreach (int index in multiSelect.GetSelectedIndices())
                        {
                            productSpecification = new ProductSpecification();
                            productSpecification.ProductID = CurrentID;
                            productSpecification.SpecificationItemID = item.SpecificationItemID;
                            productSpecification.Value = multiSelect.Items[index].Value;
                            productSpecList.Add( productSpecification );
                        }
                        foreach (ProductSpecification spec in productSpecList)
                        {
                            product.ProductSpecifications.Add( spec );
                        }
                        break;
                    default:
                        TextBox textDefault = (TextBox) rowPanel.FindControl( "SpecificationItem" + item.SpecificationItemID );
                        productSpecification.Value = textDefault.Text;
                        product.ProductSpecifications.Add( productSpecification );
                        break;
                }
            }
        }
    }

    public bool IsCallForPrice
    {
        get { return uxIsCallForPriceCheck.Checked; }
    }
    #endregion

}
