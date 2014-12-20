using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class Themes_ResponsiveGreen_Components_RandomProduct : BaseProductListItemUserControl
{
    //private void uxAddtoWishListButton_BubbleEvent(object sender, EventArgs e)
    //{
    //    ProcessAddToWishListButton((Vevo.WebUI.Orders.IAddToWishListControl)sender, );
    //}

    //private void uxAddtoCompareListButton_BubbleEvent(object sender, EventArgs e)
    //{
    //    uxAddtoCompareListButton.AddItemToCompareListCart(uxAddtoCompareListButton.ProductID);
    //    Response.Redirect("ComparisonList.aspx");
    //}
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

        //for (int i = 0; i < uxRandomList.Items.Count; i++)
        //{
        //    ASP.components_addtowishlistbutton_ascx uxAddtoWishListButton = 
        //        (ASP.components_addtowishlistbutton_ascx)uxRandomList.Items[i].FindControl("uxAddtoWishListButton");

        //    uxAddtoWishListButton.BubbleEvent += new EventHandler(uxAddtoWishListButton_BubbleEvent);
        //}
    }

    private void RandomProduct_StoreCultureChanged(object sender, CultureEventArgs e)
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
        //uxAddtoWishListButton.BubbleEvent += new EventHandler(uxAddtoWishListButton_BubbleEvent);
        //uxAddtoCompareListButton.BubbleEvent += new EventHandler(uxAddtoCompareListButton_BubbleEvent);
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
