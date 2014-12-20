using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.Products;
using Vevo.Deluxe.Domain.Users;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Orders;
using Vevo.WebUI.Products;

public partial class Components_ProductQuickView : BaseProductUserControl
{
    private string _productID;
    private Product _product = Product.Null;

    public string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty(_productID))
            {
                _productID = uxProductHidden.Value;
            }

            return _productID;
        }
        set
        {
            _productID = value;
        }
    }

    private Product CurrentProduct
    {
        get
        {
            if (_product == Product.Null)
            {
                Product product = DataAccessContext.ProductRepository.GetOne(
                    StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID);

                _product = product;
            }

            return _product;
        }
    }

    protected string CurrentProductImageID
    {
        get
        {
            if (ViewState["CurrentProductImageID_" + this.ClientID] == null)
                ViewState["CurrentProductImageID_" + this.ClientID] = CurrentProduct.GetPrimaryProductImage().ProductImageID;
            return ViewState["CurrentProductImageID_" + this.ClientID].ToString();
        }
        set
        {
            ViewState["CurrentProductImageID_" + this.ClientID] = value;
        }
    }

    private void PopulateThumbnail()
    {
        if (CurrentProduct.ProductImages.Count > 1)
        {
            uxThumbnailDataList.DataSource = CurrentProduct.ProductImages;
            uxThumbnailDataList.DataBind();
        }
    }

    private void RegisterScript()
    {
        ((Panel)this.Parent).Attributes.Add("onmouseover", "SetupQuickView(" +
            "'" + this.Parent.ClientID + "'," +
            "'" + uxQuickViewButtonBorder.ClientID + "'," +
            "'" + uxQuickViewButtonPanel.ClientID + "'," +
            "'" + uxQuickViewButton.ClientID + "');");
        ((Panel)this.Parent).Attributes.Add("onmouseout", "document.getElementById('" + uxQuickViewButtonPanel.ClientID + "').style.display='none'");

        uxQuickViewButton.Attributes.Add("onclick", "return InitialQuickView('" + uxQuickViewPanel.ClientID + "');");
        uxCloseButton.Attributes.Add("onclick", "return CloseQuickView('" + uxQuickViewPanel.ClientID + "');");

        String csname = "SetupQuickView";
        ClientScriptManager cs = Page.ClientScript;


        StringBuilder sb = new StringBuilder();

        sb.AppendLine("function InitialQuickView(QuickViewPanel) {");
        sb.AppendLine("    var popupPanel = document.getElementById(QuickViewPanel);");
        sb.AppendLine("    if( popupPanel != null && popupPanel.style.display == 'none' ) {");
        sb.AppendLine("        popupPanel.style.display='block';");
        sb.AppendLine("        return false; ");
        sb.AppendLine("    } ");
        sb.AppendLine(" } ");


        sb.AppendLine("function SetupQuickView(ParentWidth,QuickViewButtonBorder,QuickViewButtonPanel,QuickViewButton) {");
        sb.AppendLine("     if (!/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {");
        sb.AppendLine("         document.getElementById(QuickViewButtonBorder).style.width = document.getElementById(ParentWidth).offsetWidth + 'px';");
        sb.AppendLine("         document.getElementById(QuickViewButtonBorder).style.height = document.getElementById(ParentWidth).offsetHeight + 'px';");
        sb.AppendLine("         document.getElementById(QuickViewButtonPanel).style.height = document.getElementById(ParentWidth).offsetHeight + 'px';");
        sb.AppendLine("         document.getElementById(QuickViewButtonBorder).style.display = 'block';");
        sb.AppendLine("         document.getElementById(QuickViewButtonPanel).style.display = 'block';");
        sb.AppendLine("         document.getElementById(QuickViewButton).style.display='block';");
        sb.AppendLine("     } ");
        sb.AppendLine(" } ");

        sb.AppendLine("function CloseQuickView(QuickViewPanel) {");
        sb.AppendLine("    document.getElementById(QuickViewPanel).style.display = 'none';");
        sb.AppendLine("    return false;");
        sb.AppendLine(" } ");

        sb.AppendLine("function DisplayQuantityDiscountDetail(ShowLink,HideLink,DetailPanel) {");
        sb.AppendLine("    var ShowLinkId = document.getElementById(ShowLink);");
        sb.AppendLine("    var HideLinkId = document.getElementById(HideLink);");
        sb.AppendLine("    var DetailPanelId = document.getElementById(DetailPanel);");
        sb.AppendLine("    if( ShowLinkId.style.display == 'block' ) {");
        sb.AppendLine("        ShowLinkId.style.display = 'none';");
        sb.AppendLine("        HideLinkId.style.display = 'block';");
        sb.AppendLine("        DetailPanelId.style.display = 'block';");
        sb.AppendLine("        return false; ");
        sb.AppendLine("    } ");
        sb.AppendLine("    else {");
        sb.AppendLine("        ShowLinkId.style.display = 'block';");
        sb.AppendLine("        HideLinkId.style.display = 'none';");
        sb.AppendLine("        DetailPanelId.style.display = 'none';");
        sb.AppendLine("        return false; ");
        sb.AppendLine("    } ");
        sb.AppendLine(" } ");
        
        if (!cs.IsClientScriptBlockRegistered(this.GetType(), csname))
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    private void GetInfoTextFromImage( string imageURL, ref string altTag, ref string titleTag )
    {
        foreach (ProductImage image in CurrentProduct.ProductImages)
        {
            if (imageURL.Contains( image.RegularImage ))
            {
                altTag = image.Locales[StoreContext.Culture].AltTag;
                titleTag = image.Locales[StoreContext.Culture].TitleTag;
                return;
            }
        }
    }

    private bool VerifyValidInput( DataListItem item, out string errorMessage )
    {
        errorMessage = String.Empty;
        TextBox uxQuantityText = (TextBox) item.FindControl( "uxQuantityText" );
        Components_OptionGroupDetails uxOptionGroupDetails =
            (Components_OptionGroupDetails) item.FindControl( "uxOptionGroupDetails" );

        if (Page.IsValid)
        {
            if (!uxOptionGroupDetails.IsValidInput)
            {
                errorMessage = GetLanguageText( "InvalidOption" );
                return false;
            }

            int result = 0;
            if (!int.TryParse( uxQuantityText.Text, out result ) || result < 0 || (uxQuantityText.Text == "" && uxQuantityText.Visible))
            {
                errorMessage = GetLanguageText( "InvalidQuantity" );
                return false;
            }
            else
                return true;
        }

        return false;
    }

    private bool IsMatchQuantity( DataListItem item, out string errorMessage )
    {
        errorMessage = String.Empty;
        TextBox uxQuantityText = (TextBox) item.FindControl( "uxQuantityText" );

        int quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        int minQuantity = CurrentProduct.MinQuantity;
        int maxQuantity = CurrentProduct.MaxQuantity;

        if (minQuantity > quantity)
        {
            errorMessage = GetLanguageText( "MinQuantity" ) + minQuantity;
            return false;
        }

        if (maxQuantity != 0 && maxQuantity < quantity)
        {
            errorMessage = GetLanguageText( "MaxQuantity" ) + maxQuantity;
            return false;
        }

        return true;
    }

    private void AddRecentlyViewedProduct()
    {
        if (StoreContext.RecentltViewedProduct.Contains( CurrentProduct.ProductID ))
        {
            StoreContext.RecentltViewedProduct.Remove( CurrentProduct.ProductID );
            StoreContext.RecentltViewedProduct.Insert( 0, CurrentProduct.ProductID );
            return;
        }

        int recentViewedNumber = DataAccessContext.Configurations.GetIntValue( "RecentlyViewedProductShow" );

        if (StoreContext.RecentltViewedProduct.Count < recentViewedNumber)
            StoreContext.RecentltViewedProduct.Insert( 0, CurrentProduct.ProductID );
        else
        {
            StoreContext.RecentltViewedProduct.RemoveAt( StoreContext.RecentltViewedProduct.Count - 1 );
            StoreContext.RecentltViewedProduct.Insert( 0, CurrentProduct.ProductID );
        }
    }

    private void AddItemToShoppingCart( DataListItem item, out string errorMessage )
    {
        Components_OptionGroupDetails uxOptionGroupDetails =
            (Components_OptionGroupDetails) item.FindControl( "uxOptionGroupDetails" );
        Components_GiftCertificateDetails uxGiftCertificateDetails =
            (Components_GiftCertificateDetails) item.FindControl( "uxGiftCertificateDetails" );

        TextBox uxEnterAmountText = (TextBox) item.FindControl( "uxEnterAmountText" );
        TextBox uxQuantityText = (TextBox) item.FindControl( "uxQuantityText" );
        decimal customPrice = decimal.Divide( decimal.Parse( uxEnterAmountText.Text ), Convert.ToDecimal( StoreContext.Currency.ConversionRate ) );


        CartAddItemService addToCartService = new CartAddItemService(
            StoreContext.Culture, StoreContext.ShoppingCart );

        int currentStock;
        string errorOptionName;
        bool stockOK = addToCartService.AddToCart(
            CurrentProduct,
            uxOptionGroupDetails.GetSelectedOptions(),
            ProductKitItemValueCollection.Null,
            ConvertUtilities.ToInt32( uxQuantityText.Text ),
            uxGiftCertificateDetails.GetCartItemGiftDetails(),
            customPrice,
            out errorOptionName,
            out currentStock );


        if (stockOK)
        {
            errorMessage = String.Empty;

            bool enableNotification = ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", StoreContext.CurrentStore ) );
            if (UrlManager.IsMobileDevice(Request))
            {
                enableNotification = false;
            }
            if (enableNotification)
            {
                uxAddToCartNotification.Show( CurrentProduct, ConvertUtilities.ToInt32( uxQuantityText.Text ), customPrice, uxGiftCertificateDetails.GetCartItemGiftDetails(), uxOptionGroupDetails.GetSelectedOptions(), ProductKitItemValueCollection.Null );
                uxAddtoCartUpdate.Update();
            }
            else
            {
                Response.Redirect( "ShoppingCart.aspx" );
            }
        }
        else
        {
            DisplayOutOfStockError( currentStock, errorOptionName, out errorMessage );
        }
    }

    private void DisplayOutOfStockError( int currentStock, string name, out string errorMessage )
    {
        Label uxStockLabel = (Label) uxList.Items[0].FindControl( "uxStockLabel" );

        int amount = currentStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );

        if (amount < 0)
            amount = 0;

        string result = name + " " + amount;

        if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
        {
            errorMessage = String.Format(
                GetLanguageText( "CheckStockMesssage1" ) + " {0} " + GetLanguageText( "CheckStockMesssage2" ), result.Trim() );
        }
        else
        {
            errorMessage = GetLanguageText( "CheckStockMessage" );
        }
    }

    private void PopulateControls()
    {
        ProductImage details;

        IList<Product> productList = new List<Product>();
        productList.Add( CurrentProduct );

        uxList.DataSource = productList;
        uxList.DataBind();

        if (String.IsNullOrEmpty( CurrentProductImageID ))
        {
            details = CurrentProduct.GetPrimaryProductImage();
            CurrentProductImageID = details.ProductImageID;
        }
        else
        {
            details = CurrentProduct.GetProductImage( CurrentProductImageID );
        }

        uxProductHidden.Value = CurrentProduct.ProductID;

        string smallImage = details.RegularImage;
        string altTag = string.Empty;
        string titleTag = string.Empty;
        GetInfoTextFromImage( smallImage, ref altTag, ref titleTag );

        PopulateThumbnail();

        uxImageHidden.Value = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConvertUtilities.ToBoolean(DataAccessContext.Configurations.GetValue("EnableQuickView", StoreContext.CurrentStore)))
            RegisterScript();

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (uxQuickViewButton.CommandArgument.Equals( "hide" ))
        {
            uxQuickViewPanel.Visible = false;
            uxProductHidden.Value = ProductID;
            return;
        }

        uxQuickViewPanel.Visible = true;
        PopulateControls();
        uxQuickViewButton.CommandArgument = "hide";
        uxUpdatePanel.Update();
    }

    protected void uxQuickViewButton_Click( object sender, EventArgs e )
    {
        AddRecentlyViewedProduct();
        uxQuickViewButton.CommandArgument = "show";
    }

    protected void uxViewDetailButton_Click( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( ProductID ))
        {
            ProductID = uxProductHidden.Value;
        }

        Response.Redirect( UrlManager.GetProductUrl( CurrentProduct.ProductID, CurrentProduct.UrlName ) );
    }

    protected void uxAddToCartImageButton_Click( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( ProductID ))
        {
            ProductID = uxProductHidden.Value;
        }

        ProductSubscription subscriptionItem = new ProductSubscription( CurrentProduct.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (StoreContext.Customer.IsNull)
            {
                Response.Redirect( "~/UserLogin.aspx?ReturnUrl=AddtoCart.aspx?ProductID=" + CurrentProduct.ProductID );
            }

            if (CustomerSubscription.IsContainsProductSubscriptionHigherLevel(
                subscriptionItem.ProductSubscriptions[0].SubscriptionLevelID,
                DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( StoreContext.Customer.CustomerID ) ))
            {
                Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + CurrentProduct.ProductID );
            }
        }

        if (!StoreContext.ShoppingCart.CheckCanAddItemToCart( CurrentProduct ))
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + CurrentProduct.ProductID );
        }

        if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx" );
        }
        else
        {
            DataListItem item = uxList.Items[0];

            string errorMessage = String.Empty;
            if (VerifyValidInput( item, out errorMessage ) &&
                 IsMatchQuantity( item, out errorMessage ))
            {
                AddItemToShoppingCart( item, out errorMessage );
            }

            if (!String.IsNullOrEmpty( errorMessage ))
            {
                uxMessage.DisplayError( errorMessage );
                uxQuickViewButton.CommandArgument = "show";
            }
            else
            {
                uxMessage.ClearMessage();
                uxQuickViewButton.CommandArgument = "hide";
                uxUpdatePanel.Update();
            }
        }
    }

    protected void uxThumbBtn_Click( object sender, EventArgs e )
    {
        LinkButton linkBtn = (LinkButton) sender;
        Image img = (Image) linkBtn.FindControl( "uxThumbImage" );

        uxQuickViewButton.CommandArgument = "show";
        uxCatalogImage.ImageUrl = img.ImageUrl.Replace( "Thumbnail/", "" );
    }

    protected string GetProductName()
    {
        return CurrentProduct.Name;
    }

    protected string GetAmountText( string price )
    {
        int currencyLength = StoreContext.Currency.CurrencySymbol.Length;
        if (StoreContext.Currency.CurrencyPosition == "Before")
            return price.Remove( 0, currencyLength );
        else
            return price.Remove( price.Length - currencyLength, currencyLength );
    }

    protected string QuickViewImageTitleText( string productImageID )
    {
        if (CurrentProduct == null) return string.Empty;
        foreach (ProductImage image in CurrentProduct.ProductImages)
        {
            if (image.ProductImageID == productImageID)
                return image.Locales[StoreContext.Culture].TitleTag;
        }
        return string.Empty;
    }

    protected string QuickViewImageAlternateText( string productImageID )
    {
        if (CurrentProduct == null) return string.Empty;
        foreach (ProductImage image in CurrentProduct.ProductImages)
        {
            if (image.ProductImageID == productImageID)
                return image.Locales[StoreContext.Culture].AltTag;
        }
        return string.Empty;
    }

    protected bool VisibleQuantity( int sumStock, bool useInventory )
    {
        if (!IsAuthorizedToViewPrice())
            return false;

        if (!CatalogUtilities.IsCatalogMode() && CatalogUtilities.IsOutOfStock( sumStock, useInventory ))
            return false;
        else
            return true;
    }

    protected string GetFormattedPrice()
    {
        decimal price = CurrentProduct.GetDisplayedPrice( StoreContext.WholesaleStatus );
        return StoreContext.Currency.FormatPrice( price );
    }

    protected bool ProductSpecificationVisible()
    {
        if (CurrentProduct.ProductSpecifications.Count == 0)
        {
            return false;
        }
        else
            return true;
    }

    protected string CheckValidStock()
    {
        int numberItemInSession = StoreContext.ShoppingCart.GetNumberOfItems( ProductID ) +
            PromotionSelectedItem.FindSubProductAmountInPromotion( StoreContext.ShoppingCart, ProductID );
        int currentStock = 0;

        currentStock = CurrentProduct.GetStockForQuickReview() - numberItemInSession;

        bool isOutStock = CatalogUtilities.IsOutOfStock( Convert.ToInt32( currentStock ), CurrentProduct.UseInventory );

        if (CurrentProduct.isProductOptionStock())
            return "";

        if (isOutStock)
        {
            return GetLanguageText( "OutStock" );
        }
        else
        {
            return GetLanguageText( "InStock" ) + " (" + RemainingStock( currentStock ) + ")";
        }
    }

    protected string GetDisplayString( object message, int length )
    {
        string displayMessage = ConvertUtilities.ToString( message );
        if (displayMessage.Length > length)
        {
            return displayMessage.Substring(0, length) + "  ...";
        }

        return displayMessage;
    }

    protected string GetValidGroup()
    {
        return "ValidGroup_" + this.ClientID;
    }

    protected bool HasUploadOrKit()
    {
        if (CurrentProduct.IsProductKit)
            return true;

        foreach (ProductOptionGroup productOptionGroup in CurrentProduct.ProductOptionGroups)
        {
            if (productOptionGroup.OptionGroup.Type == OptionGroup.OptionGroupType.Upload ||
                productOptionGroup.OptionGroup.Type == OptionGroup.OptionGroupType.UploadRequired)
                return true;
        }

        return false;
    }
}
