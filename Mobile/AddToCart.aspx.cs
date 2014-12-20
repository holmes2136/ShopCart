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
using Vevo.Domain.Stores;
using Vevo;
using Vevo.WebUI;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Mobile_AddToCart : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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
        if (product.ProductOptionGroups.Count > 0)
        {
            Response.Redirect( UrlManager.GetMobileProductUrl( product.ProductID, product.UrlName ) );
        }
        else
        {
            AddItemToShoppingCartWithoutOption( product );
        }
    }

    private void AddItemToShoppingCartWithoutOption( Product product )
    {
        if (product.RequiresUserInput() || product.IsCustomPrice)
        {
            Response.Redirect( UrlManager.GetMobileProductUrl( product.ProductID, product.UrlName ) );
        }
        else
        {
            StoreContext.ShoppingCart.AddItem( product, product.MinQuantity );

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
