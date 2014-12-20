using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Products;
using Vevo.Deluxe.Domain.Users;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;
using Vevo.WebUI.Products;
using Vevo;

public partial class Layouts_ProductDetails_ProductDetailsResponsive : BaseProductDetails
{
    #region Private

    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxQuantityText, uxAddToCartImageButton );
    }

    private void VerifyQuantityText()
    {
        foreach (ProductOptionGroup productOptionGroup in CurrentProduct.ProductOptionGroups)
        {
            if (productOptionGroup.OptionGroup.Type == OptionGroup.OptionGroupType.InputList)
            {
                uxQuantityText.Visible = false;
                uxQuantitySpan.Visible = false;
                return;
            }
        }
    }

    private void DisplayOutOfStockError( int currentStock, string name )
    {
        int amount = currentStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );

        if (amount < 0)
            amount = 0;

        string result = name + " " + amount;

        if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
        {
            uxStockLabel.Text = String.Format(
                GetLanguageText( "CheckStockMesssage1" ) + " {0} " + GetLanguageText( "CheckStockMesssage2" ), result.Trim() );
            uxMessageStockDiv.Visible = true;
        }
        else
        {
            uxStockLabel.Text = String.Format( GetLanguageText( "CheckStockMessage" ) );
            uxMessageStockDiv.Visible = true;
        }
    }

    private bool VerifyValidInput( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (Page.IsValid && uxOptionGroupDetails.IsValidInput)
        {
            int result = 0;
            if (!int.TryParse( uxQuantityText.Text, out result ) ||
                result < 0 ||
                (uxQuantityText.Text == "" && uxQuantityText.Visible))
            {
                errorMessage = GetLanguageText( "InvalidQuantity" );
            }
            else
            {
                if (uxProductKitGroupDetails.IsValidInput( result ))
                    return true;
            }
        }

        return false;
    }

    private CartItemGiftDetails CreateGiftDetails()
    {
        return uxGiftCertificateDetails.GetCartItemGiftDetails();
    }

    private bool IsMatchQuantity()
    {
        bool isOK = false;
        int quantity = ConvertUtilities.ToInt32( uxQuantityText.Text );
        int minQuantity = CurrentProduct.MinQuantity;
        int maxQuantity = CurrentProduct.MaxQuantity;
        uxMinQuantityLabel.Text = GetLanguageText( "MinQuantity" ) + minQuantity;
        uxMaxQuantityLabel.Text = GetLanguageText( "MaxQuantity" ) + maxQuantity;

        if (minQuantity > quantity)
        {
            uxMessageMinQuantityDiv.Visible = true;
            uxMessageMaxQuantityDiv.Visible = false;
        }
        else if (maxQuantity != 0 && maxQuantity < quantity)
        {
            uxMessageMinQuantityDiv.Visible = false;
            uxMessageMaxQuantityDiv.Visible = true;
        }
        else
        {
            uxMessageMinQuantityDiv.Visible = false;
            uxMessageMaxQuantityDiv.Visible = false;
            isOK = true;
        }

        return isOK;
    }

    private void AddItemToShoppingCart()
    {
        OptionItemValueCollection selectedOptions = uxOptionGroupDetails.GetSelectedOptions();
        ProductKitItemValueCollection selectedKits = uxProductKitGroupDetails.GetSelectedProductKitItems();
        CartItemGiftDetails giftDetails = CreateGiftDetails();

        decimal customPrice = decimal.Parse( uxEnterAmountText.Text );
        customPrice = decimal.Divide( customPrice, Convert.ToDecimal( StoreContext.Currency.ConversionRate ) );

        CartAddItemService addToCartService = new CartAddItemService(
            StoreContext.Culture, StoreContext.ShoppingCart );
        int currentStock;
        string errorOptionName;
        bool stockOK = addToCartService.AddToCart(
            CurrentProduct,
            selectedOptions,
            selectedKits,
            ConvertUtilities.ToInt32( uxQuantityText.Text ),
            giftDetails,
            customPrice,
            out errorOptionName,
            out currentStock );


        if (stockOK)
        {
            bool enableNotification = ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", StoreContext.CurrentStore ) );
            if (UrlManager.IsMobileDevice(Request))
            {
                enableNotification = false;
            }
            if (enableNotification)
            {
                uxAddToCartNotification.Show( CurrentProduct, ConvertUtilities.ToInt32( uxQuantityText.Text ), customPrice, giftDetails, selectedOptions, selectedKits );
            }
            else
            {
                Response.Redirect( "ShoppingCart.aspx" );
            }
        }
        else
        {
            DisplayOutOfStockError( currentStock, errorOptionName );
        }
    }

    private void AddItemToWishListCart( object sender, EventArgs e )
    {
        string errorMessage;
        if (VerifyValidInput( out errorMessage ))
        {
            OptionItemValueCollection selectedOptions = uxOptionGroupDetails.GetSelectedOptions();
            ProductKitItemValueCollection selectedKits = uxProductKitGroupDetails.GetSelectedProductKitItems();

            if (CurrentProduct.MinQuantity <= ConvertUtilities.ToInt32( uxQuantityText.Text ))
            {
                uxAddtoWishListButton.AddItemToWishListCart( CurrentProduct.ProductID, selectedOptions, uxQuantityText.Text );
            }
            else
            {
                uxAddtoWishListButton.AddItemToWishListCart( CurrentProduct.ProductID, selectedOptions, CurrentProduct.MinQuantity.ToString() );
            }

            Response.Redirect( "WishList.aspx" );
        }
        else
        {
            uxMessage.Text = errorMessage;
        }
    }

    private void AddItemToCompareList( object sender, EventArgs e )
    {
        uxAddtoCompareListButton.AddItemToCompareListCart( CurrentProduct.ProductID );
        Response.Redirect( "ComparisonList.aspx" );
    }

    private void SetupRatingAndReview()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "MerchantRating" ) && !DataAccessContext.Configurations.GetBoolValue( "CustomerRating" )
            && !DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ))
        {
            Control control;

            control = WebUtilities.FindControlRecursive( this, "uxRatingCustomerDIV" );
            if (control != null)
            {
                ((HtmlGenericControl) control).Style.Add( HtmlTextWriterStyle.Display, "none" );
            }

            control = WebUtilities.FindControlRecursive( this, "uxWriteReviewDIV" );
            if (control != null)
                ((HtmlGenericControl) control).Style.Add( HtmlTextWriterStyle.Display, "none" );

            control = WebUtilities.FindControlRecursive( this, "uxRatingTabDIV" );
            if (control != null)
                ((HtmlGenericControl) control).Style.Add( HtmlTextWriterStyle.Display, "none" );
        }
        else
        {
            if (!DataAccessContext.Configurations.GetBoolValue( "MerchantRating" ))
            {
                uxTabPanelAllRating.Visible = false;
            }

            if (!DataAccessContext.Configurations.GetBoolValue( "CustomerRating" ) && !DataAccessContext.Configurations.GetBoolValue( "CustomerReview" ))
            {
                Control control;
                control = WebUtilities.FindControlRecursive( this, "uxRatingCustomerDIV" );
                if (control != null)
                    ((HtmlGenericControl) control).Style.Add( HtmlTextWriterStyle.Display, "none" );

                control = WebUtilities.FindControlRecursive( this, "uxWriteReviewDIV" );
                if (control != null)
                    ((HtmlGenericControl) control).Style.Add( HtmlTextWriterStyle.Display, "none" );

                uxTabPanelCustomerRating.Visible = false;
            }
        }
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

    #endregion

    #region Protected
    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterSubmitButton();

        uxAddtoWishListButton.BubbleEvent += new EventHandler( AddItemToWishListCart );
        uxAddtoCompareListButton.BubbleEvent += new EventHandler( AddItemToCompareList );

        uxMessageStockDiv.Visible = false;
        uxMessageDiv.Visible = false;
        uxMessageMinQuantityDiv.Visible = false;
        uxMessageMaxQuantityDiv.Visible = false;

        AddRecentlyViewedProduct();
        Response.Cache.SetNoStore();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxCustomerReviews.ProductID = CurrentProduct.ProductID;
        uxRelatedProducts.ProductID = CurrentProduct.ProductID;

        if (!IsPostBack)
        {
            VerifyQuantityText();
        }
        PopulateThumbnail();

        if (!DataAccessContext.Configurations.GetBoolValue( "TellAFriendEnabled" ))
        {
            uxTellFriend.Visible = false;
        }
        if (!uxOptionGroupDetails.IsShowStock)
        {
            uxRemainingQuantity.Visible = false;
        }

        SetupRatingAndReview();

        if (CurrentProduct.ProductSpecifications.Count == 0)
            uxProductSpecificationPanel.Visible = false;

        uxProductKitPanel.Visible = CurrentProduct.IsProductKit;
    }

    private Control FindControlRecursive( Control Root, string Id )
    {
        if (Root.ID == Id)
            return Root;
        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive( Ctl, Id );
            if (FoundCtl != null)
                return FoundCtl;
        }
        return null;
    }

    protected void uxAddToCartImageButton_Click( object sender, EventArgs e )
    {
        ProductSubscription subscriptionItem = new ProductSubscription( CurrentProduct.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (StoreContext.Customer.IsNull)
            {
                string returnUrl = "AddtoCart.aspx?ProductID=" + CurrentProduct.ProductID;
                Response.Redirect( "~/UserLogin.aspx?ReturnUrl=" + returnUrl );
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
            string errorMessage = String.Empty;
            if (VerifyValidInput( out errorMessage ))
            {
                if (IsMatchQuantity())
                {
                    AddItemToShoppingCart();
                }
            }
            else
            {
                if (errorMessage != String.Empty)
                {
                    uxMessage.Text = errorMessage;
                    uxMessageDiv.Visible = true;
                }
            }
        }
    }

    protected void PopulateThumbnail()
    {
        if (CurrentProduct.ProductImages.Count > 1)
        {
            uxThumbnailDataList.DataSource = CurrentProduct.ProductImages;
            uxThumbnailDataList.DataBind();
        }
    }

    protected void uxThumbBtn_Click( object sender, EventArgs e )
    {
        LinkButton linkBtn = (LinkButton) sender;
        Image img = (Image) linkBtn.FindControl( "uxThumbImage" );

        uxCatalogImage.ImageUrl = img.ImageUrl.Replace( "Thumbnail/", "" );
        SmallImage = img.ImageUrl.Replace( "Thumbnail/", "" );
    }

    protected void uxAddReviewButton_Command( object sender, CommandEventArgs e )
    {
        if (Page.User.Identity.IsAuthenticated &&
            (!Roles.IsUserInRole( Page.User.Identity.Name, "Customers" )))
            FormsAuthentication.SignOut();

        string productID = e.CommandArgument.ToString();
        Response.Redirect( productID );
    }
    protected string GetAmountText( string price )
    {
        int currencyLength = StoreContext.Currency.CurrencySymbol.Length;
        if (StoreContext.Currency.CurrencyPosition == "Before")
            return price.Remove( 0, currencyLength );
        else
            return price.Remove( price.Length - currencyLength, currencyLength );
    }
    protected string GetFormattedPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID.ToString(), StoreContext.CurrentStore.StoreID );

        decimal price = product.GetDisplayedPrice( StoreContext.WholesaleStatus );
        return StoreContext.Currency.FormatPrice( price );
    }
    #endregion
}