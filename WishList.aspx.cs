using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class WishListPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private

    private Cart _wishListCart;


    private Cart CurrentWishListCart
    {
        get
        {
            if (_wishListCart == null)
            {
                string wishListID = DataAccessContext.WishListRepository.GetWishListIDFromCustomerID(
                    StoreContext.Customer.CustomerID );
                WishList wishList = DataAccessContext.WishListRepository.GetOne( wishListID );

                _wishListCart = wishList.Cart;
            }
            return _wishListCart;
        }
    }

    private bool IsExitsInCurrentStore( Product product )
    {
        string rootID = DataAccessContext.Configurations.GetValue(
                "RootCategory",
                DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() ) );

        return product.IsAvailable( rootID );
    }

    private void RefreshGrid()
    {
        IList<ICartItem> itemList = CurrentWishListCart.CartItems;

        for (int i = 0; i < itemList.Count; i++)
        {
            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture, itemList[i].ProductID, DataAccessContext.StoreRetriever.GetCurrentStoreID() );

            if (!IsExitsInCurrentStore( product ))
                itemList.RemoveAt( i );
        }

        uxWishListGrid.DataSource = itemList;
        uxWishListGrid.DataBind();

        if (itemList.Count == 0)
            uxButtonDiv.Visible = false;
    }

    private string GetCartItemIDFromGrid( int rowIndex )
    {
        return uxWishListGrid.DataKeys[rowIndex]["CartItemID"].ToString();
    }

    private IList<ICartItem> GetInvalidWishListCartItems()
    {
        IList<ICartItem> invalidCartItems = new List<ICartItem>();

        foreach (ICartItem cartItem in CurrentWishListCart.CartItems)
        {
            if (!cartItem.ValidateReferredObjects())
                invalidCartItems.Add( cartItem );
        }

        return invalidCartItems;
    }

    private void DeleteWishListCartItems( IList<ICartItem> cartItems )
    {
        foreach (ICartItem cartItem in cartItems)
        {
            CurrentWishListCart.DeleteItem( cartItem.CartItemID );
        }
    }

    #endregion

    #region protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ))
        {
            if (!IsPostBack)
            {
                IList<ICartItem> invalidCartItems = GetInvalidWishListCartItems();
                if (invalidCartItems.Count > 0)
                {
                    DeleteWishListCartItems( invalidCartItems );
                    uxMessage.DisplayError( "[$WishList Message]" );
                }
            }
        }
        else
        {
            uxWishListPlaceHolder.Visible = false;
            uxMessage.DisplayError( "[$WishListDisabled]" );
        }

        RefreshGrid();
    }

    protected void uxWishListGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        CurrentWishListCart.DeleteItem( GetCartItemIDFromGrid( e.RowIndex ) );
        DataAccessContext.CartRepository.UpdateWhole( CurrentWishListCart );

        RefreshGrid();

        uxStatusHidden.Value = "Deleted";
    }

    protected string ConvertOptionCollectionToString( object obj )
    {
        if (obj != null)
        {
            OptionItemValueCollection collection = (OptionItemValueCollection) obj;
            return collection.ToString();
        }
        else
            return String.Empty;
    }

    protected void uxWishListGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        string cartItemID = GetCartItemIDFromGrid( e.RowIndex );
        ICartItem cartItem = CurrentWishListCart.FindCartItemByID( cartItemID );

        StoreContext.ShoppingCart.AddItem( cartItem );

        CurrentWishListCart.DeleteItem( cartItemID );
        DataAccessContext.CartRepository.UpdateWhole( CurrentWishListCart );

        RefreshGrid();

        uxStatusHidden.Value = "AddedToCart";
    }

    protected void uxViewShoppingCartButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "ShoppingCart.aspx" );
    }

    protected void uxContinueButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( "Catalog.aspx" );
    }

    protected string GetName( object cartItem )
    {
        return ((ICartItem) cartItem).GetName( StoreContext.Culture, StoreContext.Currency );
    }

    protected string GetURL( object cartItem )
    {
        Product product = ((ICartItem) cartItem).Product;

        return UrlManager.GetProductUrl( product.ProductID, product.Locales[StoreContext.Culture].UrlName );
    }

    protected string GetUnitPriceText( object cartItem )
    {
        decimal unitPrice = ((CartItem) cartItem).GetShoppingCartUnitPrice( StoreContext.WholesaleStatus );
        return StoreContext.Currency.FormatPrice( unitPrice );
    }

    protected string GetItemImage( object cartItem )
    {
        ICartItem baseCartItem = (ICartItem) cartItem;

        ProductImage details = baseCartItem.Product.GetPrimaryProductImage();

        if (String.IsNullOrEmpty( details.ThumbnailImage ))
        {
            return "~/Images/Products/Thumbnail/DefaultNoImage.gif";
        }
        else
        {
            return "~/" + details.ThumbnailImage;
        }
    }

    #endregion

    #region Public Methods

    public bool CheckWebsiteMode()
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "IsCatalogMode" ))
            return true;
        else
            return false;
    }

    #endregion
}
