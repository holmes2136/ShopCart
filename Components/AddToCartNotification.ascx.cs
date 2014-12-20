using System;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Marketing;
using Vevo.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class Components_AddToCartNotification : Vevo.WebUI.International.BaseLanguageUserControl,
    Vevo.WebUI.Products.IAddToCartNotificationControl
{
    private void SignOut()
    {
        if (Page.User.Identity.IsAuthenticated &&
           (!Roles.IsUserInRole( Page.User.Identity.Name, "Customers" )))
            FormsAuthentication.SignOut();
    }

    private string GenerateOptionName(
           Culture culture,
           Currency currency,
           PromotionSelectedItem item )
    {
        if (item.ProductOptionIDs.Count <= 0)
            return String.Empty;

        StringBuilder sb = new StringBuilder();
        foreach (string optionID in item.ProductOptionIDs)
        {
            OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne( culture, optionID );
            sb.Append( "," );
            sb.Append( optionItem.Name );
        }
        string outText = sb.ToString();
        outText = "(" + outText.TrimStart( ',' ) + ")";
        return outText;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void Show( Product product, int quantity )
    {
        Show( product, quantity, 0, CartItemGiftDetails.Null, new OptionItemValueCollection(), new ProductKitItemValueCollection() );
    }

    public void Show( PromotionSelected promotion, int quantity )
    {
        PromotionGroup promotionGroupSelected = promotion.GetPromotionGroup();
        Culture culture = StoreContext.Culture;
        string productListText = "";
        foreach (PromotionSelectedItem item in promotion.PromotionSelectedItems)
        {
            productListText += "&nbsp;&nbsp;&ndash;&nbsp;&nbsp;" + item.Product.Locales[culture].Name + GenerateOptionName( culture, StoreContext.Currency, item ) + " x " + item.GetPromotionProduct.Quantity.ToString() + "<BR>";
        }
        string name = promotionGroupSelected.Locales[culture].Name + "<BR>" + productListText;

        uxProductImage.ImageUrl = "~/" + promotionGroupSelected.ImageFile;
        uxProductNameLink.NavigateUrl = UrlManager.GetPromotionUrl( promotionGroupSelected.PromotionGroupID, promotionGroupSelected.Locales[StoreContext.Culture].UrlName );
        uxProductNameLink.Text = "<div class='ProductName'>" + name + "</div>";
        uxQuantityLabel.Text = quantity.ToString();
        uxPriceLabel.Text = StoreContext.Currency.FormatPrice( promotionGroupSelected.Price );
        uxMessage.Text = promotionGroupSelected.Name + "[$AddSuccess]";
        uxAddToCartPopup.Show();
    }

    public void Show( Product product, int quantity, decimal customPrice, CartItemGiftDetails giftDetails, OptionItemValueCollection selectedOptions, ProductKitItemValueCollection productKitItemCollection )
    {
        ProductImage image = product.GetPrimaryProductImage();
        uxProductImage.ImageUrl = "~/" + image.RegularImage;
        uxProductNameLink.NavigateUrl = UrlManager.GetProductUrl( product.ProductID, product.Locales[StoreContext.Culture].UrlName );
        uxProductNameLink.Text = "<div class='ProductName'>" + product.Name + "</div>";
        uxQuantityLabel.Text = quantity.ToString();
        decimal productPrice = product.GetDisplayedPrice(StoreContext.WholesaleStatus);


        if (product.IsCustomPrice)
        {
            productPrice = customPrice;
        }

        if (product.IsGiftCertificate && !product.IsFixedPrice)
        {
            productPrice = giftDetails.GiftValue;
        }

        if (productKitItemCollection.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (OptionItemValue optionItemValue in selectedOptions)
            {
                sb.Append( "<div class='OptionName'>" );
                sb.Append( optionItemValue.GetDisplayedName( StoreContext.Culture, StoreContext.Currency ) );
                sb.Append( "</div>" );
            }

            if ((!productKitItemCollection.IsNull) && (productKitItemCollection.Count > 0))
            {
                sb.Append( "<div class='OptionName'>" );
                sb.Append( "Items:" );
                sb.Append( "</div>" );
            }

            foreach (ProductKitItemValue value in productKitItemCollection)
            {
                sb.Append( "<div class='OptionName'>" );
                sb.Append( "- " + value.GetGroupName( StoreContext.Culture, StoreContext.CurrentStore.StoreID ) + ":" + value.GetDisplayedName( StoreContext.Culture, StoreContext.CurrentStore.StoreID ) );
                sb.Append( "</div>" );
            }

            uxProductNameLink.Text = uxProductNameLink.Text + sb.ToString();
        }
        else
        {
            if (selectedOptions.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (OptionItemValue optionItemValue in selectedOptions)
                {
                    sb.Append( "<div class='OptionName'>" );
                    sb.Append( optionItemValue.GetDisplayedName( StoreContext.Culture, StoreContext.Currency ) );
                    sb.Append( "</div>" );

                    productPrice += optionItemValue.OptionItem.PriceToAdd;
                }

                uxProductNameLink.Text = uxProductNameLink.Text + sb.ToString();
            }
        }

        uxPriceLabel.Text = StoreContext.Currency.FormatPrice( productPrice );
        uxMessage.Text = product.Name + "[$AddSuccess]";
        uxAddToCartPopup.Show();
    }

    protected void uxCheckoutImageButton_Click( object sender, EventArgs e )
    {
        SignOut();
        Response.Redirect( "Checkout.aspx" );
    }

    protected void uxContinueShoppingButton_Click( object sender, EventArgs e )
    {
        uxAddToCartPopup.Hide();
    }
}
