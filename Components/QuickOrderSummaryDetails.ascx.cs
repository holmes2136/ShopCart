using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Deluxe.Domain.BundlePromotion;

public partial class Components_QuickOrderSummaryDetails : BaseLayoutControl
{
    private decimal GetShoppingCartTotal()
    {
        OrderCalculator orderCalculator = new OrderCalculator();

        return orderCalculator.GetShoppingCartTotal(
            StoreContext.Customer,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.CheckoutDetails.Coupon );
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxCheckOutButton_Click( object sender, EventArgs e )
    {
        if ( Page.User.Identity.IsAuthenticated &&
             !Roles.IsUserInRole( Page.User.Identity.Name, "Customers" ) )
        {
            FormsAuthentication.SignOut();
        }

        Response.Redirect( "~/CheckOut.aspx" );
    }

    protected void uxViewCartButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/ShoppingCart.aspx" );
    }


    protected string GetItemImage( object cartItem )
    {
        ICartItem baseCartItem = ( ICartItem ) cartItem;

        if ( baseCartItem.IsPromotion )
        {
            CartItemPromotion cartItemPromotion = (CartItemPromotion) baseCartItem;
            PromotionGroup promotion = cartItemPromotion.PromotionSelected.GetPromotionGroup();

            if ( String.IsNullOrEmpty( promotion.ImageFile ) )
            {
                return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
            }
            else
            {
                return "~/" + promotion.ImageFile;
            }
        }
        else
        {
            ProductImage details = baseCartItem.Product.GetPrimaryProductImage();

            if ( String.IsNullOrEmpty( details.ThumbnailImage ) )
            {
                return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
            }
            else
            {
                return "~/" + details.ThumbnailImage;
            }
        }
    }

    protected string GetName( object cartItem )
    {
        return ( ( ICartItem ) cartItem ).GetName( StoreContext.Culture, StoreContext.Currency );
    }

    protected string GetLink( object cartItem )
    {
        ICartItem baseCartItem = ( ICartItem ) cartItem;
        if ( baseCartItem.IsPromotion )
        {
            CartItemPromotion cartItemPromotion = (CartItemPromotion) baseCartItem;
            PromotionGroup promotion = cartItemPromotion.PromotionSelected.GetPromotionGroup();
            return UrlManager.GetPromotionUrl( promotion.PromotionGroupID, promotion.Locales[ StoreContext.Culture ].UrlName );
        }
        else
        {
            Product product = baseCartItem.Product;
            return UrlManager.GetProductUrl( product.ProductID, product.Locales[ StoreContext.Culture ].UrlName );
        }
    }

    protected string GetQuantityAndPrice( object cartItem )
    {
        return ( ( ICartItem ) cartItem ).Quantity.ToString() + " x " +
            StoreContext.Currency.FormatPrice( ( ( ICartItem ) cartItem ).GetShoppingCartUnitPrice( StoreContext.WholesaleStatus ) );
    }
}
