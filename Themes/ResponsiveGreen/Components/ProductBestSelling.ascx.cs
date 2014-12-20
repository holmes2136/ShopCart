using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain.Products;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Themes_ResponsiveGreen_Components_ProductBestSelling : Vevo.WebUI.Products.BaseProductUserControl
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


    protected string GetDisplayString(object message, int length)
    {
        string displayMessage = ConvertUtilities.ToString(message);
        if (displayMessage.Length > length)
        {
            return displayMessage.Substring(0, length) + "  ...";
        }

        return displayMessage;
    }
    #endregion

}
