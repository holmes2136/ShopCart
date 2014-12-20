using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain.Products;

public partial class Components_ProductBestSelling : Vevo.WebUI.Products.BaseProductUserControl
{
    #region Private

    private string CultureID
    {
        get
        {
            return CultureUtilities.StoreCultureID;
        }
    }

    private void PopulateControls()
    {
        IList<Product> productBestSellingList = DataAccessContext.ProductRepository.GetAllByBestSelling(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetIntValue( "ProductBestSellingShow" ),
            DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ),
            DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ),
            new StoreRetriever().GetCurrentStoreID(),
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ) );

        if (productBestSellingList.Count != 0)
        {
            uxBestSellingList.DataSource = productBestSellingList;
            uxBestSellingList.DataBind();
        }
        else
        {
            this.Visible = false;
        }
    }

    private void ProductBestSelling_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControls();
    }

    private void ProductBestSelling_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    private bool IsPhysicalGiftCertificate( Product product )
    {
        return product.IsGiftCertificate &&
            !((GiftCertificateProduct) product).IsElectronic;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ProductBestSelling_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( ProductBestSelling_StoreCurrencyChanged );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!DataAccessContext.Configurations.GetBoolValue( "BestsellersModuleDisplay" ))
        {
            this.Visible = false;
        }
        else
        {
            if (!IsPostBack)
                PopulateControls();
        }
    }

    #endregion

}
