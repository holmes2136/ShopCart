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
using System.Xml;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;

public partial class AdminAdvanced_MainControls_DataFeedPriceGrabber : AdminAdvancedBaseUserControl
{
    private const string _fileName = "PriceGrabberCsv";
    private const string _fileExtension = ".csv";
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
        return "\"Unique Retailer SKU (RETSKU)\",\"Manufacturer Name\",\"Manufacturer Part Number (MPN)\"," +
               "\"Product Title\",\"Categorization\",\"Product URL\",\"Image URL\",\"Detailed Description\",\"Selling Price\"," +
               "\"Product Condition\",\"Availability\",\"UPC\",\"Shipping Costs\",\"Weight\"";
    }

    private string GetProductCondition()
    {
        if (uxDataFeedDetails.GetProductCondition().ToLower() == "none")
            return "New";
        else
            return uxDataFeedDetails.GetProductCondition();
    }

    private string GenerateDetail( Product product, string categoryID, object stock )
    {
        string sku, manufacturer, manufacturerPartnambers, name, categoryName, productID,
            urlName, shortDescription, price, productCondition, upc, shippingCost, weight, imageUrl;
        ProductPrice productPrice = product.GetProductPrice( CurrentStore.StoreID );
       
        string storefrontUrl;
        if (KeyUtilities.IsMultistoreLicense())
            storefrontUrl = "http://" + CurrentStore.UrlName;
        else
            storefrontUrl = UrlPath.StorefrontUrl;

        sku = _export.ReplaceString( product.Sku );
        manufacturer = _export.ReplaceString( product.Manufacturer );
        manufacturerPartnambers = _export.ReplaceString( product.ManufacturerPartNumber );
        name = _export.ReplaceString( _export.ReplaceNonAlphaAndDigit( product.Name ) );
        categoryName = _export.ReplaceString( _export.GetCategoryName( categoryID ) );
        productID = product.ProductID;
        urlName = _export.ReplaceString( _export.GetProductUrl( productID, product.UrlName, storefrontUrl ) );
        shortDescription = _export.ReplaceString( product.ShortDescription );
        price = _export.ReplaceString( _export.GetPrice( productPrice.Price ) );
        productCondition = _export.ReplaceString( GetProductCondition() );
        upc = _export.ReplaceString( product.Upc );
        shippingCost = _export.ReplaceString( _export.GetShippingCost( product.FixedShippingCost, productID,
            uxDataFeedDetails.GetShippingMethod(), productPrice.Price ) );
        weight = _export.ReplaceString( _export.FormatNumber( product.Weight ) );
        imageUrl = _export.ReplaceString( _export.GetImageUrl( product, storefrontUrl ) );

        return sku + "," +
             manufacturer + "," +
             manufacturerPartnambers + "," +
             name + "," +
             categoryName + "," +
             urlName + "," +
             imageUrl + "," +
             shortDescription + "," +
             price + "," +
             productCondition + "," +
             _export.ReplaceString( _export.GetStockAvailable( stock, productID, CurrentStore.StoreID ) ? "No" : "Yes" ) + "," +
             upc + "," +
             shippingCost + "," + weight;
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
        uxDataFeedDetails.SetFileExtension( _fileExtension );
        uxDataFeedDetails.SetInvisibleMedium();
        uxDataFeedDetails.SetInvisibleStockDescription();
        if (!IsAdminModifiable())
        {
            uxGenerateButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
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
                    uxLanguageControl.CurrentCulture, items[i].ToString(), "ProductID", BoolFilter.ShowTrue, CurrentStore.StoreID );
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
