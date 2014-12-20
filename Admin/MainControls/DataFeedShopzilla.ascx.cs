using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;

public partial class AdminAdvanced_MainControls_DataFeedShopzilla : AdminAdvancedBaseUserControl
{
    private const string _fileName = "Shopzilla";
    private const string _fileExtension = ".txt";
    private ExportUtility _export = new ExportUtility();

    private Store CurrentStore
    {
        get { return uxDataFeedDetails.GetCurrentStore(); }
    }

    private bool IncludeOutOfStock( object stock, object useInventory )
    {
        return _export.IncludeOutOfStock( uxDataFeedDetails.GetIncludeOutOfStock(), stock, useInventory );
    }

    private string GetFilePathName()
    {
        return uxDataFeedDetails.GetDirectory() + uxDataFeedDetails.GetFileName() + _fileExtension;
    }

    private string GenerateHeader()
    {
        return "Category\tManufacturer\tTitle\tDescription\tLink\tImage\tSKU\tQuantity on Hand\tCondition\t" +
            "Shipping Weight\tShipping Cost\tBid\tPromo Text\tUPC\tPrice";
    }

    private string GenerateDetail( Product product, string categoryID, object stock )
    {
        string productID, categoryName, manufacturer, name, shortDescription, urlName, imageUrl, sku, weight, shippingCost, upc, price, sumStock;
        ProductPrice productPrice = product.GetProductPrice( CurrentStore.StoreID );

        string storefrontUrl;
        if (KeyUtilities.IsMultistoreLicense())
            storefrontUrl = "http://" + CurrentStore.UrlName;
        else
            storefrontUrl = UrlPath.StorefrontUrl;

        productID = product.ProductID;
        categoryName = _export.GetCategoryName( categoryID );
        manufacturer = product.Manufacturer;
        name = _export.ReplaceNonAlphaAndDigit( product.Name );
        shortDescription = product.ShortDescription;
        urlName = _export.GetProductUrl( productID, product.UrlName, storefrontUrl );
        imageUrl = _export.GetImageUrl( product, storefrontUrl );
        sku = product.Sku;
        weight = _export.FormatNumber( product.Weight );
        shippingCost = _export.GetShippingCost( product.FixedShippingCost, productID,
            uxDataFeedDetails.GetShippingMethod(), productPrice.Price );
        upc = product.Upc;
        price = _export.GetPrice( productPrice.Price );
        sumStock = product.SumStock.ToString();

        return categoryName + "\t" +
            manufacturer + "\t" +
            name + "\t" +
            shortDescription + "\t" +
            urlName + "\t" +
            imageUrl + "\t" +
            sku + "\t" +
            sumStock + "\t" +
            string.Empty + "\t" +
            weight + "\t" +
            shippingCost + "\t" +
            string.Empty + "\t" +
            string.Empty + "\t" +
            upc + "\t" +
            price + "\t";

    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        uxDataFeedDetails.SetFileExtension( _fileExtension );
        uxDataFeedDetails.SetInvisibleProductCondition();
        uxDataFeedDetails.SetInvisibleStockDescription();
        uxDataFeedDetails.SetInvisibleMedium();
        if (!IsAdminModifiable())
        {
            uxGenerateButton.Visible = false;
        }
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        ArrayList items = uxDataFeedDetails.GetSelectedCategory();
        string filePathName = "";
        if (items.Count > 0)
        {
            filePathName = GetFilePathName();
            string filePhysicalPathName = Server.MapPath( "../" + filePathName );

            StreamWriter writer = new StreamWriter( filePhysicalPathName, false );
            writer.WriteLine( GenerateHeader() );

            string details;

            for (int i = 0; i < items.Count; i++)
            {
                IList<Product> productList = DataAccessContext.ProductRepository.GetByCategoryID(
                    uxLanguageControl.CurrentCulture,
                    items[i].ToString(),
                    "ProductID",
                    BoolFilter.ShowTrue,
                    CurrentStore.StoreID );

                foreach (Product product in productList)
                {
                    if (IncludeOutOfStock( product.SumStock, product.UseInventory ))
                    {
                        details = GenerateDetail( product, items[i].ToString(), product.SumStock );
                        writer.WriteLine( details );
                    }
                }
            }
            writer.Dispose();
        }

        if (items.Count == 0)
        {
            uxMessage.DisplayError( "Please select catagory that you want to export." );
            uxFileNameLink.Text = "";
            uxFileNameLink.NavigateUrl = "";
        }
        else if (!String.IsNullOrEmpty( filePathName ))
        {
            uxMessage.DisplayMessage( "Export data successfully. Please click the following link to view the file." );
            uxFileNameLink.Text = Path.GetFileName( filePathName );
            uxFileNameLink.NavigateUrl = "../DownloadFile.aspx?FilePath=" + "../" + filePathName;
            uxFileNameLink.Target = "_blank";
        }
    }
	
	public void PopulateControl()
    {
        uxLanguageControl.PopulateControls();
        uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
        uxDataFeedDetails.PopulateControl();
    }
}
