using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.GoogleFeed;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Tax;
using Vevo.Shared.WebUI;

public partial class AdminAdvanced_MainControls_DataFeedGoogle : AdminAdvancedBaseUserControl
{
    private const string _fileName = "GoogleXMLFile";
    private const string _fileExtension = ".xml";
    private ExportUtility _export = new ExportUtility();
    private Dictionary<String, String> currencyList = new Dictionary<String, String>();

    private string GetFilePathName()
    {
        return uxDataFeedDetails.GetDirectory() + uxDataFeedDetails.GetFileName() + _fileExtension;
    }

    private Store CurrentStore
    {
        get { return uxDataFeedDetails.GetCurrentStore(); }
    }

    private string[] StrSplit( string str )
    {
        char[] delimiter = new char[] { ',' };
        string[] result = str.Split( delimiter );
        return result;
    }

    private bool IncludeOutOfStock( object stock, object useInventory )
    {
        return _export.IncludeOutOfStock( uxDataFeedDetails.GetIncludeOutOfStock(), stock, useInventory );
    }

    private void GenerateCurrencyCode()
    {
        currencyList.Clear();
        currencyList.Add( "AU", "AUD" );
        currencyList.Add( "BR", "BRL" );
        currencyList.Add( "CN", "CNY" );
        currencyList.Add( "CZ", "CZK" );
        currencyList.Add( "FR", "EUR" );
        currencyList.Add( "DE", "EUR" );
        currencyList.Add( "IT", "EUR" );
        currencyList.Add( "JP", "JPY" );
        currencyList.Add( "NL", "EUR" );
        currencyList.Add( "ES", "EUR" );
        currencyList.Add( "CH", "CHF" );
        currencyList.Add( "GB", "GBP" );
        currencyList.Add( "US", "USD" );
    }

    private void WriteProductDetails( Product product, Culture culture, XmlWriter writer, ArrayList optionMapping, ArrayList optionItems, ArrayList optionItemIDs, string itemGroupID )
    {
        //description 
        //expiration_date 
        //id 
        //image_link 
        //link 
        //title 
        writer.WriteStartElement( "item" );

        //--------------- Basic Information --------------------------
        //<title>Patio set</title>
        writer.WriteElementString( "title", Server.HtmlEncode( product.Name ) );//maximum 80 characters

        //<description>The Veracruz Oval Table.</description>
        writer.WriteElementString( "description", Server.HtmlEncode( product.ShortDescription ) );

        //<g:google_product_category>Apparel &amp; Accessories &gt; Clothing &gt; Dresses</g:google_product_category>
        foreach ( string categoryID in product.CategoryIDs )
        {
            writer.WriteElementString( "g", "product_type", null, Server.HtmlEncode( uxDataFeedDetails.GetProductBreadcrumb( categoryID ) ) );
        }

        //<g:product_type>Home &amp; Garden &gt; Kitchen &amp; Dining &gt; Appliances &gt; Refrigerators</g:product_type>
        if ( uxDataFeedDetails.GetIsUseGoogleCategory() )
            writer.WriteElementString( "g", "google_product_category", null, Server.HtmlEncode( uxDataFeedDetails.GetGoogleCategory() ) );

        //<link>http://www.example.com/asp/sp.asp?cat=12&amp;id=1030</link>
        // Do not return https:// for storefront prouduct & image links
        UrlPath productUrl = new UrlPath( UrlManager.GetProductUrl( product.ProductID, product.UrlName ) );
        writer.WriteElementString( "link", productUrl.CreateAbsoluteUri( "http://" + CurrentStore.UrlName ).Replace( "https://", "http://" ) );

        //<g:image_link>http://www.example.com/image1.jpg</g:image_link>
        ProductImage productImage = product.GetPrimaryProductImage();
        if ( String.IsNullOrEmpty( productImage.RegularImage ) )
        {
            writer.WriteElementString( "g", "image_link", null, "http://" + CurrentStore.UrlName );
        }
        else
        {
            // Do not return https:// for storefront prouduct & image links
            writer.WriteElementString( "g", "image_link", null, new Uri( "http://" + CurrentStore.UrlName + "/" + productImage.RegularImage ).AbsoluteUri );
        }

        //<g:condition>refurbished</g:condition>
        writer.WriteElementString( "g", "condition", null, Server.HtmlEncode( uxDataFeedDetails.GetGoogleProductCondition() ) );

        //-------------- Prices and Availability -------------------
        //<g:availability>in stock</g:availability>
        if ( product.IsOutOfStock() )
        {
            writer.WriteElementString( "g", "availability", null, "out of stock" );
        }
        else
        {
            writer.WriteElementString( "g", "availability", null, "available for order" );
        }

        //<g:price>25.00</g:price>
        writer.WriteElementString( "g", "price", null,
            Currency.ConvertPriceToSelectedCurrency( product.GetProductPrice( CurrentStore.StoreID ).Price, currencyList[ uxDataFeedDetails.GetGoogleCountryCode() ] ) );

        //------------ Unique Product Identifiers ---------------------
        //<g:brand>Acme</g:brand>
        if ( ( product.Brand != null ) && ( product.Brand != String.Empty ) )
        {
            writer.WriteElementString( "g", "brand", null, product.Brand );
        }

        //<g:gtin>12345678</g:gtin>
        if ( ( product.Upc != null ) && ( product.Upc != String.Empty ) )
        {
            writer.WriteElementString( "g", "gtin", null, product.Upc );
        }

        //'<g:mpn>GO1234568OOGLE</g:mpn>
        if ( ( product.ManufacturerPartNumber != null ) && ( product.ManufacturerPartNumber != String.Empty ) )
        {
            writer.WriteElementString( "g", "mpn", null, product.ManufacturerPartNumber );
        }

        //'<g:item_group_id>FB3030</g:item_group_id>
        if ( itemGroupID != null )
        {
            writer.WriteElementString( "g", "item_group_id", null, itemGroupID );
        }

        //------------ For Apperal Product -----------------------
        if ( ( product.ProductSpecifications != null ) && ( product.ProductSpecifications.Count > 0 ) && ( uxDataFeedDetails.GetGoogleCategory().Contains( "Apparel" ) ) )
        {
            foreach ( ProductSpecification spec in product.ProductSpecifications )
            {
                // Check for Adult
                GoogleSpecMapping specAdultMap = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( culture, "4", spec.Value );
                if ( ( specAdultMap != null ) && ( !specAdultMap.IsNull ) )
                {
                    writer.WriteElementString( "g", "age_group", null, "adult" );
                    continue;
                }
                // Check For Kids
                else
                {
                    GoogleSpecMapping specKidsMap = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( culture, "5", spec.Value );
                    if ( ( specKidsMap != null ) && ( !specKidsMap.IsNull ) )
                    {
                        writer.WriteElementString( "g", "age_group", null, "kids" );
                        continue;
                    }
                }

                // Check for Male
                GoogleSpecMapping specMaleMap = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( culture, "1", spec.Value );
                if ( ( specMaleMap != null ) && ( !specMaleMap.IsNull ) )
                {
                    writer.WriteElementString( "g", "gender", null, "male" );
                    continue;
                }
                else
                {
                    // Check for Female
                    GoogleSpecMapping specFemaleMap = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( culture, "2", spec.Value );
                    if ( ( specFemaleMap != null ) && ( !specFemaleMap.IsNull ) )
                    {
                        writer.WriteElementString( "g", "gender", null, "female" );
                        continue;
                    }
                    else
                    {
                        // Check for Unisex
                        GoogleSpecMapping specUnisexMap = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleSpecMapping( culture, "3", spec.Value );
                        if ( ( specUnisexMap != null ) && ( !specUnisexMap.IsNull ) )
                        {
                            writer.WriteElementString( "g", "gender", null, "unisex" );
                            continue;
                        }
                    }
                }
            }
        }

        //----------- For Variant product  ----------
        if ( ( optionMapping != null ) && ( optionItems != null ) )
        {
            for ( int i = 0; i < optionMapping.Count; i++ )
            {
                GoogleOptionMapping optionMap = ( GoogleOptionMapping ) optionMapping[ i ];
                writer.WriteElementString( "g", optionMap.GoogleFeedTagName.ToLower(), null, optionItems[ i ].ToString() );
            }
            string optionItemIDText = String.Join( "-", ( string[] ) optionItemIDs.ToArray( typeof( string ) ) );
            writer.WriteElementString( "g", "id", null, product.Sku + "-" + optionItemIDText );
        }
        else
        {
            //<g:id>01flx</g:id> 
            writer.WriteElementString( "g", "id", null, product.Sku );
        }

        //----------- For Tax (US Only) ----------
        //<g:tax>
        //    <g:country>US</g:country>
        //    <g:region>CA</g:region>
        //    <g:rate>8.25</g:rate>
        //    <g:tax_ship>y</g:tax_ship>
        //</g:tax>
        if ( uxDataFeedDetails.GetGoogleCountryCode().Equals( "US" ) )
        {
            if ( product.TaxClassID == "0" ) //No Tax
            {
                writer.WriteStartElement( "g", "tax", null ); //<g:tax>

                writer.WriteElementString( "g", "country", null, uxDataFeedDetails.GetGoogleCountryCode() );
                writer.WriteElementString( "g", "rate", null, "0" );

                writer.WriteEndElement(); //</g:tax>
            }
            else
            {
                TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( product.TaxClassID );
                string worldTax = "0";
                bool hasUSATax = false;

                foreach ( TaxClassRule rule in taxClass.TaxClassRule )
                {
                    //For Everywhere rule if no USA rule
                    if ( rule.IsDefaultCountry )
                    {
                        worldTax = rule.TaxRate.ToString();
                    }

                    //For US and Everywhere rule only
                    if ( rule.CountryCode.Equals( "US" ) )
                    {
                        writer.WriteStartElement( "g", "tax", null ); //<g:tax>
                        writer.WriteElementString( "g", "country", null, uxDataFeedDetails.GetGoogleCountryCode() );

                        if ( !String.IsNullOrEmpty( rule.ZipCode ) )
                        {
                            writer.WriteElementString( "g", "region", null, rule.ZipCode );
                        }
                        else if ( !String.IsNullOrEmpty( rule.StateCode ) )
                        {
                            writer.WriteElementString( "g", "region", null, rule.StateCode );
                        }

                        writer.WriteElementString( "g", "rate", null, rule.TaxRate.ToString() );

                        writer.WriteEndElement(); //</g:tax>
                        hasUSATax = true;
                    }
                }

                if ( !hasUSATax )
                {
                    writer.WriteStartElement( "g", "tax", null ); //<g:tax>

                    writer.WriteElementString( "g", "country", null, uxDataFeedDetails.GetGoogleCountryCode() );
                    writer.WriteElementString( "g", "rate", null, worldTax );

                    writer.WriteEndElement(); //</g:tax>
                }
            }
        }

        //----------- For Shipping ----------
        //<g:shipping>
        //    <g:country>US</g:country>
        //    <g:region>MA</g:region>
        //    <g:service>Ground</g:service>
        //    <g:price>5.95 USD</g:price>
        //</g:shipping>
        if ( product.FreeShippingCost ) // Free Shipping Cost
        {
            writer.WriteStartElement( "g", "shipping", null ); //<g:shipping>

            writer.WriteElementString( "g", "country", null, uxDataFeedDetails.GetGoogleCountryCode() );
            writer.WriteElementString( "g", "price", null,
                Currency.ConvertPriceToSelectedCurrency( Convert.ToDecimal( 0 ), currencyList[ uxDataFeedDetails.GetGoogleCountryCode() ] ) );

            writer.WriteEndElement(); //</g:shipping>
        }
        else if ( product.FixedShippingCost ) // Override Shipping Cost
        {
            foreach ( ProductShippingCost cost in product.ProductShippingCosts )
            {
                writer.WriteStartElement( "g", "shipping", null ); //<g:shipping>

                writer.WriteElementString( "g", "country", null, uxDataFeedDetails.GetGoogleCountryCode() );
                writer.WriteElementString( "g", "price", null,
                    Currency.ConvertPriceToSelectedCurrency( cost.FixedShippingCost, currencyList[ uxDataFeedDetails.GetGoogleCountryCode() ] ) );

                writer.WriteEndElement(); //</g:shipping>
            }
        }
        else
        {
            //----------- For Shipping Weight ----------
            //<g:shipping_weight>3 kg</g:shipping_weight>
            writer.WriteElementString( "g", "shipping_weight", null,
                string.Format( "{0:n2}", product.Weight ) + " " + DataAccessContext.Configurations.GetValue( "WeightUnit" ).ToLower() );
        }

        writer.WriteEndElement(); //</item>
    }

    private string generateRandomString( int length )
    {
        Random random = new Random();
        string randomString = "";
        int randNumber;

        for ( int i = 0; i < length; i++ )
        {
            if ( random.Next( 1, 3 ) == 1 )
                randNumber = random.Next( 97, 123 ); //char {a-z}
            else
                randNumber = random.Next( 48, 58 ); //int {0-9}

            randomString = randomString + ( char ) randNumber;
        }
        return randomString;
    }

    private void Generate()
    {
        //* description
        //* expiration_date
        //* id
        //* image_link
        //* link
        //* title
        uxFileNameLink.Text = "";
        uxFileNameLink.NavigateUrl = "";
        uxMessage.Clear();

        ArrayList items = uxDataFeedDetails.GetSelectedCategory();
        if ( items.Count > 0 )
        {
            if ( uxDataFeedDetails.GetFileName().Trim() == string.Empty )
            {
                uxMessage.DisplayError( Resources.MarketingMessages.EmptyUploadField );
                return;
            }

            if ( String.IsNullOrEmpty( Currency.ConvertPriceToSelectedCurrency( Convert.ToDecimal( 0 ),
                currencyList[ uxDataFeedDetails.GetGoogleCountryCode() ] ) ) )
            {
                uxMessage.DisplayError( String.Format( Resources.MarketingMessages.CurrencyError,
                    currencyList[ uxDataFeedDetails.GetGoogleCountryCode() ] ) );
                return;
            }

            try
            {
                string saveFileName = GetFilePathName();
                string xmlFilename = Server.MapPath( "../" + saveFileName );

                bool exportStatus = false;

                XmlWriterSettings xmlSetting = new XmlWriterSettings();
                xmlSetting.Encoding = System.Text.Encoding.UTF8;
                xmlSetting.Indent = true;

                XmlWriter writer = XmlWriter.Create( xmlFilename, xmlSetting );

                writer.WriteStartDocument();
                writer.WriteStartElement( "rss" );//<rss>
                writer.WriteAttributeString( "version", "2.0" );
                writer.WriteAttributeString( "xmlns", "g", null, "http://base.google.com/ns/1.0" );

                writer.WriteStartElement( "channel" );//<channel>
                writer.WriteElementString( "title", uxDataFeedDetails.GetDataFeedTitle() );
                // Do not return https:// for storefront link
                writer.WriteElementString( "link", "http://" + CurrentStore.UrlName );
                writer.WriteElementString( "description", uxDataFeedDetails.GetDataFeedDescription() );

                ArrayList errorDupMapping = new ArrayList();
                try
                {
                    exportStatus = true;

                    List<string> SkuList = new List<string>();
                    for ( int i = 0; i < items.Count; i++ )
                    {
                        IList<Product> productList = DataAccessContext.ProductRepository.GetByCategoryID(
                            uxLanguageControl.CurrentCulture,
                            items[ i ].ToString(),
                            "ProductID",
                            BoolFilter.ShowTrue,
                            CurrentStore.StoreID
                            );
                        foreach ( Product product in productList )
                        {
                            bool isAlreadyGen = false;
                            if ( IncludeOutOfStock( product.SumStock, product.UseInventory ) &&
                                 !SkuList.Contains( product.Sku ) )
                            {
                                SkuList.Add( product.Sku );
                                if ( ( product.ProductOptionGroups == null ) || ( product.ProductOptionGroups.Count == 0 ) )
                                {
                                    WriteProductDetails( product, uxLanguageControl.CurrentCulture, writer, null, null, null, null );
                                    isAlreadyGen = true;
                                }
                                else
                                {
                                    ArrayList variantMap = new ArrayList();
                                    foreach ( ProductOptionGroup optionGroup in product.ProductOptionGroups )
                                    {
                                        for ( int ui = 3; ui <= 6; ui++ )
                                        {
                                            GoogleOptionMapping optionMapping = DataAccessContext.GoogleFeedMappingRepository.GetOneGoogleOptionMapping( uxLanguageControl.CurrentCulture, ui.ToString(), optionGroup.OptionGroupID );
                                            if ( ( optionMapping != null ) && ( !optionMapping.IsNull ) )
                                            {
                                                variantMap.Add( optionMapping );
                                            }
                                        }
                                    }

                                    // Checking duplicate mapping option
                                    bool isDupMapping = false;
                                    for ( int j = 0; j < variantMap.Count; j++ )
                                    {
                                        for ( int k = 0; k < variantMap.Count; k++ )
                                        {
                                            if ( isDupMapping == true ) break;
                                            if ( j == k ) continue;
                                            GoogleOptionMapping mapA = ( GoogleOptionMapping ) variantMap[ j ];
                                            GoogleOptionMapping mapB = ( GoogleOptionMapping ) variantMap[ k ];
                                            if ( mapA.GoogleFeedTagID == mapB.GoogleFeedTagID )
                                            {
                                                isDupMapping = true;
                                                break;
                                            }
                                        }
                                    }
                                    if ( isDupMapping == true )
                                    {
                                        exportStatus = false;
                                        errorDupMapping.Add( product.ProductID );
                                        continue;
                                    }

                                    // Write Details by product Option
                                    ArrayList optionGroups = new ArrayList();
                                    string itemGroupID = generateRandomString( 7 );
                                    foreach ( GoogleOptionMapping optionMap in variantMap )
                                    {
                                        optionGroups.Add( optionMap.OptionGroupID );
                                    }
                                    DataTable dt = DataAccessContext.ProductRepository.GetStockOptionLine( uxLanguageControl.CurrentCulture, ( string[] ) optionGroups.ToArray( typeof( string ) ) );
                                    if ( dt != null )
                                    {
                                        foreach ( DataRow row in dt.Rows )
                                        {
                                            ArrayList optionItems = new ArrayList();
                                            ArrayList optionItemIDs = new ArrayList();
                                            for ( int im = 1; im < dt.Columns.Count - 1; im += 2 )
                                            {
                                                optionItems.Add( row[ im ].ToString() );
                                            }
                                            for ( int jm = 0; jm < dt.Columns.Count - 1; jm += 2 )
                                            {
                                                optionItemIDs.Add( row[ jm ].ToString() );
                                            }
                                            WriteProductDetails( product, uxLanguageControl.CurrentCulture, writer, variantMap, optionItems, optionItemIDs, itemGroupID );
                                            isAlreadyGen = true;
                                        }
                                    }
                                }

                                if ( !isAlreadyGen )
                                {
                                    WriteProductDetails( product, uxLanguageControl.CurrentCulture, writer, null, null, null, null );
                                }

                            }
                        }
                    }

                }
                catch ( Exception ex )
                {
                    SaveLogFile.SaveLog( ex );
                }
                finally
                {
                    writer.WriteEndDocument();//</rss>
                    writer.Close();
                }
                uxFileNameLink.Text = Path.GetFileName( saveFileName );
                uxFileNameLink.NavigateUrl = "../DownloadFile.aspx?FilePath=../" + saveFileName;
                uxFileNameLink.Target = "_blank";
                if ( exportStatus )
                    uxMessage.DisplayMessage( Resources.MarketingMessages.UploadSuccess );
                else
                {
                    if ( errorDupMapping.Count > 0 )
                    {
                        uxMessage.DisplayError( "Variant mapping rules are dulicated. ProductID: " + String.Join( ",", ( string[] ) errorDupMapping.ToArray( typeof( string ) ) ) );
                    }
                    else
                    {
                        uxMessage.DisplayError( "Have problem to export." );
                    }
                }
            }
            catch
            {
                uxMessage.DisplayError( Resources.MarketingMessages.UploadError );
            }
        }
        else
        {
            uxMessage.DisplayError( "Please select catagory that you want to export." );
        }
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
        uxDataFeedDetails.SetInvisibleShippingMethod();
        uxDataFeedDetails.SetInvisibleStockDescription();
        uxDataFeedDetails.SetVisibleGoogleProductConditionDropDown();
        uxDataFeedDetails.SetVisibleGoogleProductCategory();
        if ( !IsAdminModifiable() )
        {
            uxGenerateButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( !MainContext.IsPostBack )
            uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        GenerateCurrencyCode();
        Generate();
    }

    public void PopulateControl()
    {
        uxDataFeedDetails.SetFileName( _fileName, uxLanguageControl.CurrentCultureID );
        uxDataFeedDetails.PopulateControl();
    }
}