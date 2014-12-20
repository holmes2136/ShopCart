using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.Orders;

public partial class Components_ShoppingCartDetails : BaseLayoutControl
{
    private bool PanelVisible
    {
        get
        {
            if ( Session[ "ShoppingCartDetailsPanelVisible" ] == null )
                Session[ "ShoppingCartDetailsPanelVisible" ] = true;

            return ( bool ) Session[ "ShoppingCartDetailsPanelVisible" ];
        }
        set
        {
            Session[ "ShoppingCartDetailsPanelVisible" ] = value;
        }
    }

    private decimal GetShoppingCartTotal()
    {
        OrderCalculator orderCalculator = new OrderCalculator();

        return orderCalculator.GetShoppingCartTotal(
            StoreContext.Customer,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.CheckoutDetails.Coupon );
    }

    private void PopulateControls()
    {
        ICartItem[] itemList = StoreContext.ShoppingCart.GetCartItems();
        List<ICartItem> newItem = new List<ICartItem>();

        if ( itemList.Length > 0 )
        {
            for ( int i = itemList.Length - 1; i > -1; i-- )
            {
                newItem.Add( itemList[ i ] );

                if ( newItem.Count == 3 )
                    i = -1;
            }
            MiniShoppingCartDiv.Attributes.Add( "style", "display: block;" );
        }
        else
        {
            MiniShoppingCartDiv.Attributes.Add( "style", "display: none;" );
        }

        uxRecentlyGrid.DataSource = newItem;
        uxRecentlyGrid.DataBind();
    }

    private void ShowHidePanel()
    {
        if ( uxRecentlyPanel.Visible )
        {
            uxRecentlyPanel.Visible = false;
            uxShowHideImage.ImageUrl = "~/Images/Design/Icon/CartHidePanel.png";
        }
        else
        {
            uxRecentlyPanel.Visible = true;
            uxShowHideImage.ImageUrl = "~/Images/Design/Icon/CartShowPanel.png";
        }

        PanelVisible = uxRecentlyPanel.Visible;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxCartDetailLabel.Text = string.Format(
            GetLanguageText( "CartDetail" ),
            StoreContext.ShoppingCart.GetCurrentQuantity().ToString() );
        uxCartSubTotalLabel.Text = string.Format(
            GetLanguageText( "SubTotal" ) + "<span class='MiniShoppingCartSubTotal'>{0}</span>",
            StoreContext.Currency.FormatPrice( GetShoppingCartTotal() ) );

        PopulateControls();
        uxRecentlyPanel.Visible = PanelVisible;
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

    protected void uxShowHideButton_Click( object sender, EventArgs e )
    {
        ShowHidePanel();
    }

    protected void uxRecentlyGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        StoreContext.ShoppingCart.DeleteItem( uxRecentlyGrid.DataKeys[ e.RowIndex ][ "CartItemID" ].ToString() );
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
