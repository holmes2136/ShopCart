using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Deluxe.Domain.Products;

public partial class AddShoppingCartNotComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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

    private string FreeShipping
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["FreeShipping"] ))
                return "0";
            else
                return Request.QueryString["FreeShipping"];
        }
    }

    private string PromotionStock
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["PromotionStock"] ))
                return "0";
            else
                return Request.QueryString["PromotionStock"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxContinueLink.NavigateUrl =
            "GiftRegistryItem.aspx?GiftRegistryID=" + StoreContext.CheckoutDetails.GiftRegistryID;

        uxContinueLink.Visible = false;
        giftRegistryMessagePanel.Visible = false;
        if (ProductID != "0")
        {
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID );
            ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );
            if (subscriptionItem.IsSubscriptionProduct())
            {
                uxMessage.DisplayError( "Cannot add product to the cart.<br/><br/>You already have higher product subscription level." );
            }
            else
            {
                if (StoreContext.ShoppingCart.ContainsFreeShippingCostProduct() || CartItemPromotion.ContainsFreeShippingCostProductInBundlePromotion(StoreContext.ShoppingCart))
                {
                    uxMessage.DisplayError( "Cannot add product to the cart.<br/><br/>The shopping cart already has one or more product " +
                            "that is a free shipping product." );
                    uxProductNonFreeShippingCostPanel.Visible = false;

                }
                else
                {
                    uxMessage.DisplayError( "Cannot add product to the cart.<br/><br/>The shopping cart already has one or more product " +
                            "that is not a free shipping." );
                    uxProductFreeShippingCostPanel.Visible = false;
                }
            }
        }
        else
        {
            if (FreeShipping != "0")
            {
                if (StoreContext.ShoppingCart.ContainsFreeShippingCostProduct() || CartItemPromotion.ContainsFreeShippingCostProductInBundlePromotion(StoreContext.ShoppingCart))
                {
                    uxProductFreeShippingCostPanel.Visible = true;
                    uxProductNonFreeShippingCostPanel.Visible = false;
                }
                else
                {
                    uxProductNonFreeShippingCostPanel.Visible = true;
                    uxProductFreeShippingCostPanel.Visible = false;
                }
            }
            else if (PromotionStock != "0")
            {
                uxMessage.DisplayError( "Cannot add Promotional product to the cart because it is out of stock." );
            }
            else
            {
                uxContinueLink.Visible = true;
                giftRegistryMessagePanel.Visible = true;
                uxMessage.DisplayError( "Failed adding product to the cart.<br/><br/>The shopping cart already has one or more gift registry item(s).<br/><br/>" +
                            "Please add more items using gift registry item list menu." );
            }
        }
    }
}
