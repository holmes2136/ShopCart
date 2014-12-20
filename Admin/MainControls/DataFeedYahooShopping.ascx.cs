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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;

public partial class AdminAdvanced_MainControls_DataFeedYahooShopping : AdminAdvancedBaseUserControl
{
    private const string _fileName = "data";
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
        return "code\tname\tdescription\tprice\tproduct-url\tmerchant-site-category\tmedium\timage-url\tupc\tmanufacturer\t" +
               "manufacturer-part-no\tmsrp\tin-stock\tshipping-price\tshipping-weight";
    }

    private string GenerateDetail( Product product, string categoryID, object stock )
    {
        string sku, manufacturer, manufacturerPartnambers, name, categoryName, productID,
            urlName, shortDescription, price, upc, shippingCost, weight, medium, msrp, imageUrl;
        ProductPrice productPrice = product.GetProductPrice( CurrentStore.StoreID );
       
        string storefrontUrl;
        if (KeyUtilities.IsMultistoreLicense())
            storefrontUrl = "http://" + CurrentStore.UrlName;
        else
            storefrontUrl = UrlPath.StorefrontUrl;

        productID = product.ProductID;
        sku = product.Sku;
        name = product.Name;
        shortDescription = product.ShortDescription;
        price = _export.GetPrice( productPrice.Price );
        urlName = _export.GetProductUrl( productID, product.UrlName, storefrontUrl );
        categoryName = _export.GetCategoryName( categoryID );
        medium = uxDataFeedDetails.GetMedium();
        upc = product.Upc;
        manufacturer = product.Manufacturer;
        manufacturerPartnambers = product.ManufacturerPartNumber;
        msrp = _export.FormatNumber( productPrice.RetailPrice );
        shippingCost = _export.GetShippingCost( product.FixedShippingCost, productID,
            uxDataFeedDetails.GetShippingMethod(), productPrice.Price );
        weight = _export.FormatNumber( product.Weight );
        imageUrl = _export.GetImageUrl( product, storefrontUrl );

        return sku + "\t" +
            name + "\t" +
            shortDescription + "\t" +
            price + "\t" +
            urlName + "\t" +
            categoryName + "\t" +
            medium + "\t" +
            imageUrl + "\t" +
            upc + "\t" +
            manufacturer + "\t" +
            manufacturerPartnambers + "\t" +
            msrp + "\t" +
            (_export.GetStockAvailable( stock, productID, CurrentStore.StoreID ) ? "N" : "Y") + "\t" +
            shippingCost + "\t" +
            weight;
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
        uxDataFeedDetails.NotRenameFilename();
        uxDataFeedDetails.SetInvisibleProductCondition();
        uxDataFeedDetails.SetInvisibleStockDescription();
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
