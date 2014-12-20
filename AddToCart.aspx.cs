using System;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain.Products;
using Vevo.Deluxe.Domain.Users;
using Vevo.Deluxe.Domain;

public partial class AddToCart : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["ProductID"] ))
                return "0";
            else
                return Request.QueryString["ProductID"];
        }
    }

    private string StoreID
    {
        get
        {
            return new StoreRetriever().GetCurrentStoreID();
        }
    }

    private void AddItemToShoppingCart( Product product )
    {
        if ((product.ProductOptionGroups.Count > 0) || product.IsProductKit)
        {
            Response.Redirect( UrlManager.GetProductUrl( product.ProductID, product.UrlName ) );
        }
        else
        {
            AddItemToShoppingCartWithoutOption( product );
        }
    }

    private void AddItemToShoppingCartWithoutOption( Product product )
    {
        if (product.RequiresUserInput() || product.IsCustomPrice || product.IsProductKit)
        {
            Response.Redirect( UrlManager.GetProductUrl( product.ProductID, product.UrlName ) );
        }
        else
        {
            ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );
            StoreContext.ShoppingCart.AddItem( product, product.MinQuantity, subscriptionItem.IsSubscriptionProduct() );

            Response.Redirect( "ShoppingCart.aspx" );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (ProductID != "0")
        {
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, StoreID );

            if (product == NullProduct.Null || !product.IsEnabled)
            {
                uxProductNotAvailableMessagePanel.Visible = true;
                return;
            }

            ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

            if (subscriptionItem.IsSubscriptionProduct())
            {
                if (StoreContext.Customer.IsNull)
                {
                    Response.Redirect( "~/UserLogin.aspx?ReturnUrl=AddtoCart.aspx?ProductID=" + product.ProductID );
                }

                if (CustomerSubscription.IsContainsProductSubscriptionHigherLevel(
                    subscriptionItem.ProductSubscriptions[0].SubscriptionLevelID,
                    DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( StoreContext.Customer.CustomerID ) ))
                {
                    Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + product.ProductID );
                }
            }

            if (!StoreContext.ShoppingCart.CheckCanAddItemToCart( product ))
            {
                Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + product.ProductID );
            }

            if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
            {
                Response.Redirect( "AddShoppingCartNotComplete.aspx" );
            }
            else
            {
                AddItemToShoppingCart( product );
            }
        }
        else
        {
            uxProductNotExistMessagePanel.Visible = true;
        }
    }
}
