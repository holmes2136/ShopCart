using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Shared.WebUI;

public partial class AdminAdvanced_MainControls_DataFeedShoppingDotCom : AdminAdvancedBaseUserControl
{
    private const string _fileName = "ShoppingDotComCsv";
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

    private string GetShippingCost( bool useProductShippingCost, string productID, string shippingID, object price )
    {
        string shippingCost = _export.GetShippingCost( useProductShippingCost, productID, shippingID, price );
        if (ConvertUtilities.ToDecimal( shippingCost ) == 0)
            shippingCost = "FREE";
        return shippingCost;
    }

    private string GenerateHeader()
    {
        return "MPN,Manufacturer Name,UPC,Product Name,Product Description,  Product Price  ,Product URL," +
               "Image URL ,Shopping.com Categorization,Stock Availability,Stock Description, Ground Shipping  ," +
               " Weight ,Zip Code";
    }

    private string GenerateDetail( Product product, string categoryID, object stock )
    {
        string manufacturerPartnambers, manufacturer, upc, name, shortDescription, price, urlName, categoryName, productID,
            stockDescription, shippingCost, weight, imageUrl;
        string storefrontUrl;
        if (KeyUtilities.IsMultistoreLicense())
            storefrontUrl = "http://" + CurrentStore.UrlName;
        else
            storefrontUrl = UrlPath.StorefrontUrl;

        productID = product.ProductID;
        manufacturerPartnambers = _export.ReplaceString( product.ManufacturerPartNumber );
        manufacturer = _export.ReplaceString( product.Manufacturer );
        upc = _export.ReplaceString( product.Upc );
        name = _export.ReplaceString( product.Name );
        shortDescription = _export.ReplaceString( product.ShortDescription );
        price = _export.ReplaceString( _export.GetPrice( product.GetProductPrice( CurrentStore.StoreID ).Price ) );
        urlName = _export.ReplaceString( _export.GetProductUrl( productID, product.UrlName, storefrontUrl ) );
        categoryName = _export.ReplaceString( _export.GetCategoryName( categoryID ) );
        stockDescription = _export.ReplaceString( uxDataFeedDetails.GetStockDescription() );
        shippingCost = _export.ReplaceString( GetShippingCost( product.FixedShippingCost, productID,
            uxDataFeedDetails.GetShippingMethod(), product.GetProductPrice( CurrentStore.StoreID ).Price ) );
        weight = _export.ReplaceString( _export.FormatNumber( product.Weight ) );
        imageUrl = _export.ReplaceString( _export.GetImageUrl( product, storefrontUrl ) );

        return manufacturerPartnambers + "," +
             manufacturer + "," +
             upc + "," +
             name + "," +
             shortDescription + "," +
             price + "," +
             urlName + "," +
             imageUrl + "," +
             categoryName + "," +
             _export.ReplaceString( _export.GetStockAvailable( stock, productID, CurrentStore.StoreID ) ? "N" : "Y" ) + "," +
             stockDescription + "," +
             shippingCost + "," + weight + ",\"\"";
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
        uxDataFeedDetails.SetInvisibleProductCondition();
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
