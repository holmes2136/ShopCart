using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.WebUI.Products;
using System.Collections.Generic;
using Vevo.Deluxe.Domain.Products;

public partial class Components_RandomProduct : BaseProductUserControl
{
    #region Private
    private void PopulateControls()
    {
        IList<Product> productList = DataAccessContext.ProductRepository.GetAllByRandom( 
            StoreContext.Culture,
            DataAccessContext.Configurations.GetIntValue( "RandomProductShow" ),
            DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ),
            DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ),
            StoreContext.CurrentStore.StoreID,
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ) );

        uxRandomList.DataSource = productList;
        uxRandomList.DataBind();
    }

    private void RandomProduct_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void RandomProduct_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    private bool IsPhysicalGiftCertificate( Product product )
    {
        if (product.IsGiftCertificate)
        {
            GiftCertificateProduct giftProduct = (GiftCertificateProduct) product;
            if (!giftProduct.IsElectronic)
                return true;
        }
        return false;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( RandomProduct_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( RandomProduct_StoreCurrencyChanged );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "FeaturedProductModuleDisplay" ))
        {
            if (!IsPostBack)
                PopulateControls();
        }
        else
        {
            this.Visible = false;
        }
    }

    protected void uxAddToCartImageButton_Command( object sender, CommandEventArgs e )
    {
        string productID = e.CommandArgument.ToString();
        string urlName = e.CommandName.ToString();

        Product product = DataAccessContext.ProductRepository.GetOne(
                   StoreContext.Culture,
                   productID, new StoreRetriever().GetCurrentStoreID() );

        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (StoreContext.Customer.IsNull)
            {
                string returnUrl = "AddtoCart.aspx?ProductID=" + product.ProductID;
                Response.Redirect( "~/UserLogin.aspx?ReturnUrl=" + returnUrl );
            }
        }

        if (!StoreContext.ShoppingCart.CheckCanAddItemToCart( product))
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx?ProductID=" + productID );
        }

        if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            Response.Redirect( "AddShoppingCartNotComplete.aspx" );
        }
        else
        {

            if ((product.RequiresUserInput() || product.IsCustomPrice || product.IsProductKit))
            {
                Response.Redirect( UrlManager.GetProductUrl( productID, urlName ) );
            }
            else
            {
                StoreContext.ShoppingCart.AddItem( product, product.MinQuantity );

                bool enableNotification = ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "EnableAddToCartNotification", StoreContext.CurrentStore ) );
                if (UrlManager.IsMobileDevice(Request))
                {
                    enableNotification = false;
                }
                if (enableNotification)
                {
                    uxAddToCartNotification.Show( product, product.MinQuantity );
                }
                else
                {
                    Response.Redirect( "ShoppingCart.aspx" );
                }
            }
        }
    }

    protected bool QuantityDiscountVisible( object productDiscountGroupID, object categoryIDList )
    {
        return CatalogUtilities.GetQuantityDiscountByProductID( productDiscountGroupID, categoryIDList );
    }
    
    #endregion


    #region Public Properties

    public string CultureID
    {
        get
        {
            return CultureUtilities.StoreCultureID;
        }
    }

    #endregion
}
