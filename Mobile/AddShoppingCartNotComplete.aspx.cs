using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Deluxe.Domain.Products;

public partial class Mobile_AddShoppingCartNotComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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

    protected void Page_Load( object sender, EventArgs e )
    {
        if (ProductID != "0")
        {
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID );
            ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );
            if (subscriptionItem.IsSubscriptionProduct())
            {
                uxInvalidSubscriptionPanel.Visible = true;
            }
            else
            {
                uxInvalidSubscriptionPanel.Visible = false;
                if (StoreContext.ShoppingCart.ContainsFreeShippingCostProduct() || CartItemPromotion.ContainsFreeShippingCostProductInBundlePromotion( StoreContext.ShoppingCart ))
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
        }
        else
        {
            uxProductNonFreeShippingCostPanel.Visible = false;
            uxProductFreeShippingCostPanel.Visible = false;
        }

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }
}