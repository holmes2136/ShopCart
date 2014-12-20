using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Configuration;
using System.Web.Security;
using Vevo.Base.Domain;
using Vevo.DataAccessLib;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.Marketing;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Domain.Contents;
using Vevo.Domain.EmailTemplates;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Domain.Products.BackOffice;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;
using Vevo.Domain.Tax;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Widget;

namespace Vevo
{
    /// <summary>
    /// Summary description for DatabaseConverter
    /// </summary>
    public partial class DatabaseConverter
    {
        #region Private

        private DataTable _configTable;

        private bool _flagBeforeGenerate = false;
        private bool _flagAfterGenerate = true;

        private bool _flagBeforeGenerateUpgrade303 = true;
        private bool _flagAfterGenerateUpgrade303 = false;

        private bool _flagBeforeGenerateUpgrade400 = false;
        private bool _flagAfterGenerateUpgrade400 = true;

        private bool _flagBeforeGenerateUpgrade410 = false;
        private bool _flagAfterGenerateUpgrade410 = true;

        private bool _flagBeforeGenerateUpgrade420 = false;
        private bool _flagAfterGenerateUpgrade420 = true;

        private bool _flagBeforeGenerateUpgrade421 = false;
        private bool _flagAfterGenerateUpgrade421 = true;

        private bool _flagBeforeGenerateUpgrade430 = false;
        private bool _flagAfterGenerateUpgrade430 = true;

        private bool _flagBeforeGenerateUpgrade440 = false;
        private bool _flagAfterGenerateUpgrade440 = true;

        private bool _flagBeforeGenerateUpgrade500 = false;
        private bool _flagAfterGenerateUpgrade500 = true;

        private bool _flagBeforeGenerateUpgrade520 = false;
        private bool _flagAfterGenerateUpgrade520 = true;

        private bool _flagBeforeGenerateUpgrade530 = false;
        private bool _flagAfterGenerateUpgrade530 = true;

        private bool _flagBeforeGenerateUpgrade540 = false;
        private bool _flagAfterGenerateUpgrade540 = true;

        private bool _flagBeforeGenerateUpgrade600 = false;
        private bool _flagAfterGenerateUpgrade600 = true;

        private bool _flagBeforeGenerateUpgrade601 = false;
        private bool _flagAfterGenerateUpgrade601 = true;

        private bool _flagBeforeGenerateUpgrade610 = false;
        private bool _flagAfterGenerateUpgrade610 = true;

        private bool _eWayUseCVN = false;
        private DataTable _metaInformationTable = new DataTable();
        private ArrayList _titleLanguageList = new ArrayList();
        private ArrayList _cultureList = new ArrayList();

        private IDictionary<string, string> _bannerDic = new Dictionary<string, string>();

        private const string SQLSelectProductString =
        "SELECT Product.ProductID, Name, ShortDescription, LongDescription, Sku, Model, " +
        "    ImageSecondary, SumStock, Weight, FixedShippingCost, " +
        "    ShippingCost, Manufacturer, WholesalePrice, Price, RetailPrice, Keywords, " +
        "    Product.UrlName, " +
        "    IsTodaySpecial, IsDownloadable, DownloadPath, Product.IsEnabled, RelatedProducts, " +
        "    MerchantRating, Product.DiscountGroupID as ProductDiscountGroupID, " +
        "    WholesalePrice2, WholesalePrice3," +
        "    IsGiftCertificate, IsFixedPrice, ManufacturerPartNumber, Upc, TaxClassID, IsParentVisible, " +
        "    Product.Other1, Product.Other2, Product.Other3, Product.Other4, Product.Other5, " +
        "    UseInventory, ProductDetailsLayoutPath, " +
            // For Recurring
        "    IsRecurring, RecurringInterval, RecurringIntervalUnit, RecurringNumberOfCycles," +
        "    RecurringNumberOfTrialCycles, RecurringTrialAmount, " +
        "    MetaKeyword, MetaDescription " +
        "FROM " +
        "    ((Product INNER JOIN " +
        "        (SELECT ProductID, " +
        "            SUM( Stock ) AS SumStock FROM ProductStock GROUP By ProductID) AS S " +
        "        ON Product.ProductID = S.ProductID) " +
        "    LEFT JOIN (SELECT * FROM ProductLocale WHERE CultureID = @CultureID) AS L " +
        "        ON Product.ProductID = L.ProductID) " +
            // for Recurring
        "    LEFT JOIN ProductRecurring E ON Product.ProductID = E.ProductID ";


        private DataTable GetAllConfigurations( string cultureID )
        {
            DataTable table = DataAccess.ExecuteSelect(
                "SELECT C.ConfigID, Name, ItemValue, V.CultureID " +
                "FROM ((Configuration AS C " +
                "    LEFT JOIN (SELECT * FROM ConfigurationValue " +
                "        WHERE CultureID = @ValueCultureID OR CultureID = 0) AS V " +
                "    ON C.ConfigID = V.ConfigID)); ",
                DataAccess.CreateParameterString( cultureID ) );

            table.Columns.Add( "IsMultiValue", Type.GetType( "System.Boolean" ) );
            foreach (DataRow row in table.Rows)
            {
                if (row["CultureID"].ToString() == "0")
                    row["IsMultiValue"] = false;
                else
                    row["IsMultiValue"] = true;
            }

            return table;
        }

        private string GetConfigValue( string name )
        {
            foreach (DataRow row in _configTable.Rows)
            {
                if (String.Compare( row["Name"].ToString(), name, true ) == 0)
                {
                    return row["ItemValue"].ToString();
                }
            }

            return string.Empty;
        }

        private void GetTitleTextValue( DataTable table )
        {
            foreach (DataRow row in table.Rows)
            {
                _titleLanguageList.Add( row["TextData"] );
                _cultureList.Add( row["CultureID"] );
            }
        }

        private string GetCultureID( Culture culture )
        {
            return culture.CultureID;
        }

        private IList<Product> ConvertProductTableToList( Culture culture, DataTable table, string storeID )
        {
            IList<Product> list = new List<Product>();

            foreach (DataRow row in table.Rows)
            {
                list.Add( CreateOneProductFromRow( culture, row, storeID ) );
            }

            return list;
        }

        private Product CreateOneProductFromRow( Culture culture, DataRow row, string storeID )
        {
            ProductFactory factory = new ProductFactory();
            factory.DefaultCulture = culture;
            factory.BaseData = DataAccessConverter.ConvertDataRowToHashtable( row );
            //Comment - disable Categories from here.
            //Comment - disable GiftCertificate too.
            return factory.CreateProduct( storeID );
        }

        private void DropDatabaseTable( string tableName )
        {
            DataAccess.ExecuteNonQuery( "DROP TABLE " + tableName + " ; " );
        }

        private void DropTableColumn( string tableName, string fieldname )
        {
            DataAccess.ExecuteNonQuery( "ALTER TABLE " + tableName + " DROP COLUMN " + fieldname + ";" );
        }

        private void AddTableColumn( string tableName, string fieldname, string attribute, bool allowNull )
        {
            string nullText = " NULL ";

            if (!allowNull)
            {
                nullText = " NOT NULL ";
            }

            DataAccess.ExecuteNonQuery( "ALTER TABLE " + tableName + " ADD " + fieldname + " " + attribute + " " + nullText + ";" );
        }

        private void SaveSecondaryImage( string largeFilePath, Product product, out string secondaryImagePath )
        {
            using (System.Drawing.Image myImage =
                System.Drawing.Image.FromFile( WebUtilities.GetLocalPath( largeFilePath ) ))
            {
                using (ProductImageFile imageFile = new ProductImageFile(
                    new FileManager(), Path.GetFileName( largeFilePath ), myImage ))
                {
                    imageFile.SaveSecondary();
                    product.ImageSecondary = imageFile.SecondaryFilePath;
                    secondaryImagePath = product.ImageSecondary;
                }
            }
        }

        private void SaveRegularAndThumbnail( string largeFilePath, ProductImage productImage )
        {
            using (System.Drawing.Image myImage =
                               System.Drawing.Image.FromFile(
                               WebUtilities.GetLocalPath( largeFilePath )
                               ))
            {
                using (ProductImageFile myImageFile =
                    new ProductImageFile( new FileManager(), Path.GetFileName( largeFilePath ), myImage ))
                {

                    myImageFile.SaveRegular();
                    myImageFile.SaveThumbnail();

                    productImage.ThumbnailImage = myImageFile.ThumbnailFilePath;
                    productImage.RegularImage = myImageFile.RegularFilePath;
                    productImage.LargeImage = largeFilePath;

                    //ProductImageAccess.Update(
                    //    productImageID,
                    //    myImageFile.ThumbnailFilePath,
                    //    myImageFile.RegularFilePath,
                    //    largeFilePath,
                    //    false,
                    //    false
                    //    );
                }
            }
        }

        private IList<Product> GetAll( Culture culture, string storeID )
        {
            DataTable table = DataAccess.ExecuteSelect(
                SQLSelectProductString,
                DataAccess.CreateParameterString( GetCultureID( culture ) ) );

            return ConvertProductTableToList( culture, table, storeID );
        }

        private void CopyImage( string storeID )
        {
            //DataTable productImageTable;
            //productImageTable = ProductImageAccess.GetAllImages();
            IList<Product> products = GetAll( StoreContext.Culture, storeID );
            //string previousProductID = "";

            foreach (Product product in products)
            {
                string imageSecondary = String.Empty;
                Product productTmp = product;
                foreach (ProductImage productImage in productTmp.ProductImages)
                {
                    if (!String.IsNullOrEmpty( productImage.RegularImage ))
                    {
                        string imagePath = productImage.RegularImage;
                        string largeFilePath = ProductImageFile.CopyToLargeImageFolder( new FileManager(), imagePath );
                        SaveRegularAndThumbnail( largeFilePath, productImage );

                        if (String.IsNullOrEmpty( imageSecondary ))
                        {
                            SaveSecondaryImage( largeFilePath, productTmp, out imageSecondary );
                        }
                    }
                }
                DataAccessContext.ProductRepository.Save( productTmp );
            }

            //if (productImageTable.Rows.Count != 0)
            //{
            //    for (int i = 0; i <= productImageTable.Rows.Count - 1; i++)
            //    {
            //        string currentProductID = productImageTable.Rows[i]["ProductID"].ToString();
            //        string productImageID = productImageTable.Rows[i]["ProductImageID"].ToString();
            //        if (!string.IsNullOrEmpty( productImageTable.Rows[i]["RegularImage"].ToString() ))
            //        {
            //            string imagePath = productImageTable.Rows[i]["RegularImage"].ToString();

            //            string largeFilePath = ProductImageFile.CopyToLargeImageFolder( imagePath );

            //            SaveRegularAndThumbnail( largeFilePath, productImageID );

            //            if (currentProductID != previousProductID)
            //            {
            //                SaveSecondaryImage( largeFilePath, currentProductID );
            //            }
            //            previousProductID = currentProductID;
            //        }
            //    }
            //}
        }

        private void CreateAffiliateRoles()
        {
            Roles.CreateRole( "Affiliates" );
        }

        private void eWayConfigSetup()
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "eWay" );

            paymentOption.Cvv2Required = _eWayUseCVN;

            DataAccessContext.PaymentOptionRepository.Update( paymentOption );
        }

        private DataTable GetAllWishLists()
        {
            return DataAccess.ExecuteSelect( "SELECT * FROM WishList; " );
        }

        private DataTable GetWishListItems( string existingWishListID )
        {
            return DataAccess.ExecuteSelect(
                "SELECT * FROM WishListItem WHERE WishListID = @WishListID; ",
                DataAccess.CreateParameterString( existingWishListID ) );
        }

        private Cart CreateWishListCart( WishList wishList )
        {
            Cart cart = new Cart();

            DataTable wishListItemTable = GetWishListItems( wishList.WishListID );
            foreach (DataRow row in wishListItemTable.Rows)
            {
                Product product =
                    DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, row["ProductID"].ToString(), new StoreRetriever().GetCurrentStoreID() );

                if (!product.IsNull)
                {
                    cart.AddItem(
                        product,
                        ConvertUtilities.ToInt32( row["Quantity"] ),
                        new OptionItemValueCollection(
                            StoreContext.Culture, row["OptionItemIDs"].ToString(), product.ProductID ) );
                }
            }

            return cart;
        }

        private void ConvertWishList( string storeID )
        {
            DataTable wishListTable = GetAllWishLists();

            foreach (DataRow row in wishListTable.Rows)
            {
                WishList wishList = DataAccessContext.WishListRepository.GetOne( row["WishListID"].ToString() );

                Cart cart = CreateWishListCart( wishList );
                DataAccessContext.CartRepository.CreateWhole( cart );

                // Update CartID of WishList database table
                wishList.CartID = cart.CartID;
                DataAccessContext.WishListRepository.Save( wishList );
            }

            DropDatabaseTable( "WishListItem" );
        }

        private DataTable GetAllArticles()
        {
            return DataAccess.ExecuteSelect( "SELECT * FROM Article ORDER BY ArticleOrder " );
        }

        private DataTable GetAllArticleLocales()
        {
            return DataAccess.ExecuteSelect( "SELECT * FROM ArticleLocale; " );
        }

        private void SetContentLocale( Content content, DataRow[] localeList )
        {
            for (int i = 0; i < localeList.Length; i++)
            {
                ContentLocale contentLocale = new ContentLocale();
                contentLocale.CultureID = localeList[i]["CultureID"].ToString();
                contentLocale.Title = localeList[i]["Title"].ToString();
                contentLocale.Body = localeList[i]["Body"].ToString();
                content.Locales.Add( contentLocale );
            }
        }

        private void SetMenuLocale( ContentMenuItem menuItem, DataRow[] localeList )
        {
            for (int i = 0; i < localeList.Length; i++)
            {
                ContentMenuItemLocale menuLocale = new ContentMenuItemLocale();
                menuLocale.CultureID = localeList[i]["CultureID"].ToString();
                menuLocale.Name = localeList[i]["Title"].ToString();
                menuLocale.Description = localeList[i]["Description"].ToString();
                menuItem.Locales.Add( menuLocale );
            }
        }

        private Content CreateNewContent( Culture culture, DataRow articleRow, DataRow[] localeList )
        {
            Content content = new Content( culture );
            content.ContentName = "Article" + articleRow["ArticleID"].ToString();
            content.IsEnabled = ConvertUtilities.ToBoolean( articleRow["Enabled"] );
            content.UrlName = localeList[0]["Title"].ToString();
            SetContentLocale( content, localeList );
            return DataAccessContext.ContentRepository.Save( content );
        }

        private ContentMenuItem CreateNewContentMenu( Content content, DataRow[] localeList )
        {
            ContentMenuItem menu = new ContentMenuItem( content.DefaultCulture );
            menu.ContentMenuID = DataAccessContext.Configurations.GetValue( "LeftContentMenu" );
            menu.ReferringMenuID = "0";
            menu.MenuPosition = ContentMenuItem.MenuPositionType.Left;
            menu.IsEnabled = content.IsEnabled;
            SetMenuLocale( menu, localeList );
            menu.ContentID = content.ContentID;
            return DataAccessContext.ContentMenuItemRepository.Save( menu );
        }

        private void ConvertArticleToContent()
        {
            DataTable articleTable = GetAllArticles();
            DataTable articleLocalesTable = GetAllArticleLocales();

            foreach (DataRow row in articleTable.Rows)
            {
                DataRow[] localeList = articleLocalesTable.Select(
                    string.Format( "ArticleID = '{0}'", row["ArticleID"].ToString() ) );
                Culture culture =
                    DataAccessContext.CultureRepository.GetOne( (localeList[0]["CultureID"]).ToString() );

                Content content = CreateNewContent( culture, row, localeList );
                CreateNewContentMenu( content, localeList );
            }

            DropDatabaseTable( "Article" );
            DropDatabaseTable( "ArticleLocale" );

            ContentMenu contentMenu = DataAccessContext.ContentMenuRepository.GetOne(
                DataAccessContext.Configurations.GetValue( "LeftContentMenu" ) );

            contentMenu.IsEnabled = DataAccessContext.Configurations.GetBoolValue( "InformationModuleDisplay" );
            DataAccessContext.ContentMenuRepository.Save( contentMenu );

        }

        private void ConvertProductDisplayStyle()
        {
            int productDisplayType = DataAccessContext.Configurations.GetIntValue( "ProductDisplayType" );
            if (productDisplayType > 0)
            {
                string productListLayout;
                switch (productDisplayType)
                {
                    case 2:
                        productListLayout = "ProductListColumnStyle1.ascx";
                        break;
                    case 3:
                        productListLayout = "ProductListColumnStyle2.ascx";
                        break;
                    default:
                        productListLayout = "ProductListRowStyle.ascx";
                        break;
                }

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["DefaultProductListLayout"],
                     productListLayout );
            }
        }

        private void UpdateAffiliateIsEnabled()
        {
            IList<Affiliate> affiliateList = DataAccessContextDeluxe.AffiliateRepository.GetAll();
            for (int i = 0; i < affiliateList.Count; i++)
            {
                MembershipUser user = Membership.GetUser( affiliateList[i].UserName );
                if (user.IsApproved && affiliateList[i].IsEnabled)
                    affiliateList[i].IsEnabled = true;
                else
                    affiliateList[i].IsEnabled = false;

                DataAccessContextDeluxe.AffiliateRepository.Save( affiliateList[i] );
            }
        }

        private bool IsUpgradingTo300()
        {
            return _flagBeforeGenerate && (!_flagAfterGenerate);
        }

        private bool IsUpgradingTo303()
        {
            return (!_flagBeforeGenerateUpgrade303) && _flagAfterGenerateUpgrade303;
        }

        private bool IsUpgradingTo400()
        {
            return _flagBeforeGenerateUpgrade400 && (!_flagAfterGenerateUpgrade400);
        }

        private bool IsUpgradingTo410()
        {
            return (!_flagBeforeGenerateUpgrade410) && _flagAfterGenerateUpgrade410;
        }

        private bool IsUpgradingTo420()
        {
            return (!_flagBeforeGenerateUpgrade420) && _flagAfterGenerateUpgrade420;
        }

        private bool IsUpgradingTo421()
        {
            return (!_flagBeforeGenerateUpgrade421) && _flagAfterGenerateUpgrade421;
        }

        private bool IsUpgradingTo430()
        {
            return (!_flagBeforeGenerateUpgrade430) && _flagAfterGenerateUpgrade430;
        }

        private bool IsUpgradingTo440()
        {
            return (!_flagBeforeGenerateUpgrade440) && _flagAfterGenerateUpgrade440;
        }

        private bool IsUpgradingTo500()
        {
            return (!_flagBeforeGenerateUpgrade500) && _flagAfterGenerateUpgrade500;
        }

        private bool IsUpgradingTo520()
        {
            return (!_flagBeforeGenerateUpgrade520) && _flagAfterGenerateUpgrade520;
        }

        private bool IsUpgradingTo530()
        {
            return (!_flagBeforeGenerateUpgrade530) && _flagAfterGenerateUpgrade530;
        }

        private bool IsUpgradeingTo540()
        {
            return (!_flagBeforeGenerateUpgrade540) && _flagAfterGenerateUpgrade540;
        }

        private bool IsUpgradeingTo600()
        {
            return (!_flagBeforeGenerateUpgrade600) && _flagAfterGenerateUpgrade600;
        }

        private bool IsUpgradeingTo601()
        {
            return (!_flagBeforeGenerateUpgrade601) && _flagAfterGenerateUpgrade601;
        }

        private bool IsUpgradingTo610()
        {
            return (!_flagBeforeGenerateUpgrade610) && _flagAfterGenerateUpgrade610;
        }

        private bool IsProductColumnExisted( string columnName )
        {
            DataTable table = DataAccess.ExecuteSelect(
                "SELECT * FROM Product WHERE 1 = 0;" );

            return table.Columns.Contains( columnName );
        }

        private void SetupTaxRule()
        {
            string taxRule = DataAccessContext.Configurations.GetValue( "TaxRule" );
            string taxLocation = DataAccessContext.Configurations.GetValue( "TaxLocation" );
            decimal taxRate = DataAccessContext.Configurations.GetDecimalValue( "TaxPercentage" );

            switch (taxRule)
            {
                case "1":
                    {
                        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( "1" );
                        TaxClassRule taxClassRule = new TaxClassRule();
                        taxClassRule.TaxRate = taxRate;
                        taxClass.TaxClassRule.Add( taxClassRule );
                        DataAccessContext.TaxClassRepository.Save( taxClass );
                        break;
                    }
                case "2":
                    {
                        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( "1" );
                        TaxClassRule taxClassRule = new TaxClassRule();
                        taxClassRule.TaxRate = taxRate;
                        taxClassRule.CountryCode = taxLocation;
                        taxClassRule.IsDefaultCountry = false;
                        taxClass.TaxClassRule.Add( taxClassRule );
                        DataAccessContext.TaxClassRepository.Save( taxClass );
                        break;
                    }
                case "3":
                    {
                        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( "1" );
                        TaxClassRule taxClassRule1 = new TaxClassRule();
                        taxClassRule1.CountryCode = "US";
                        taxClassRule1.TaxRate = 0;
                        taxClassRule1.IsDefaultCountry = false;
                        taxClass.TaxClassRule.Add( taxClassRule1 );

                        TaxClassRule taxClassRule2 = new TaxClassRule();
                        taxClassRule2.CountryCode = "US";
                        taxClassRule2.TaxRate = taxRate;
                        taxClassRule2.StateCode = taxLocation;
                        taxClassRule2.IsDefaultCountry = false;
                        taxClassRule2.IsDefaultState = false;
                        taxClass.TaxClassRule.Add( taxClassRule2 );

                        DataAccessContext.TaxClassRepository.Save( taxClass );

                        break;
                    }
            }
        }

        private void UpdateConfigurations()
        {
            DataAccessContext.EnableConfigurations( true );
            WidgetDirector widgetDirector = new WidgetDirector();
            SystemConfig.UpdateNewConfigValue( widgetDirector.WidgetConfigurationCollection );
            DataAccessContext.ClearConfigurationCache();
            ConfigurationHelper.ApplyConfigurations();
        }

        private void SetUpWebsiteTitleConfig()
        {
            DataTable titleLanguageTable = LanguageTextAccess.GetTextByKeyname( "1", "Title" );
            GetTitleTextValue( titleLanguageTable );
            for (int count = 0; count < _titleLanguageList.Count; count++)
            {
                Culture culture = DataAccessContext.CultureRepository.GetOne( _cultureList[count].ToString() );
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    culture,
                    DataAccessContext.Configurations["Title"],
                    _titleLanguageList[count].ToString(),
                    Store.Null );
                LanguageTextAccess.Delete( "1", culture.CultureID, "Title" );
            }
        }

        private void CreateDefaultStore()
        {
            Store store = new Store();
            store.StoreName = DataAccessContext.Configurations.GetValue(
                CultureUtilities.DefaultCultureID, "CompanyName", Store.Null );
            store.UrlName = UrlPath.GetStoreFrontUrlWithoutWWWandHTTP();
            DataAccessContext.StoreRepository.Save( store );
            store.CreateStoreConfigCollection( store.StoreID );

            OrderCreateExtraFilter orderExtraFilter = new OrderCreateExtraFilter();
            SearchFilter searchFilter = SearchFilter.GetFactory().Create();
            IList<Order> list = DataAccessContext.OrderRepository.ExportOrder(
                "OrderID", searchFilter, orderExtraFilter, String.Empty );

            foreach (Order order in list)
            {
                order.StoreID = store.StoreID;
                DataAccessContext.OrderRepository.Save( order );
            }
        }

        private void CreateRootCategory()
        {
            IList<Category> categoryList = DataAccessContext.CategoryRepository.GetAllWithoutRootCategory( StoreContext.Culture, "CategoryID" );
            if (categoryList == null || categoryList.Count == 0)
            {
                Category rootCategory = new Category( StoreContext.Culture );
                rootCategory.Name = "RootCategory";
                rootCategory = DataAccessContext.CategoryRepository.Save( rootCategory );

                DataAccess.ExecuteNonQuery(
                    "Update Category SET RootID = @RootID WHERE CategoryID != @CategoryID;",
                    DataAccess.CreateParameterString( rootCategory.CategoryID ),
                    DataAccess.CreateParameterString( rootCategory.CategoryID ) );

                DataAccess.ExecuteNonQuery(
                    "UPDATE Category SET ParentCategoryID = @ParentCategoryID " +
                    "WHERE CategoryID != @CategoryID AND ParentCategoryID = 0; ",
                    DataAccess.CreateParameterString( rootCategory.CategoryID ),
                    DataAccess.CreateParameterString( rootCategory.CategoryID ) );

                IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

                foreach (Store store in storeList)
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["RootCategory"],
                        rootCategory.CategoryID,
                        store );
                }
            }
        }

        private void CreateProductPrice()
        {
            DataTable table = DataAccess.ExecuteSelect( "SELECT * FROM Product" );
            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            foreach (DataRow row in table.Rows)
            {
                string productID = ConvertUtilities.ToString( row["ProductID"] );

                Product product =
                        DataAccessContext.ProductRepository.GetOne(
                        StoreContext.Culture, productID, StoreContext.CurrentStore.StoreID );

                SaveProductPrice( product, row, "0" );

                foreach (Store store in storeList)
                {
                    SaveProductPrice( product, row, store.StoreID );
                }

            }

        }

        private void SaveProductPrice(
            Product product, DataRow productRow, string storeID )
        {
            decimal price = ConvertUtilities.ToDecimal( productRow["Price"] );
            decimal retailPrice = ConvertUtilities.ToDecimal( productRow["RetailPrice"] );
            decimal wholesalePrice = ConvertUtilities.ToDecimal( productRow["WholesalePrice"] );
            decimal wholesalePrice2 = ConvertUtilities.ToDecimal( productRow["WholesalePrice2"] );
            decimal wholesalePrice3 = ConvertUtilities.ToDecimal( productRow["WholesalePrice3"] );

            product.SetPrice( storeID, price, retailPrice, wholesalePrice, wholesalePrice2, wholesalePrice3 );
            DataAccessContext.ProductRepository.Save( product );
        }

        private void DropProductPriceColumn()
        {
            DropTableColumn( "Product", "Price" );
            DropTableColumn( "Product", "RetailPrice" );
            DropTableColumn( "Product", "WholesalePrice" );
            DropTableColumn( "Product", "WholesalePrice2" );
            DropTableColumn( "Product", "WholesalePrice3" );
        }

        private void CovertShippingDestionation()
        {
            string merchantCountry = DataAccessContext.Configurations.GetValue( "ShippingMerchantCountry" );
            ShippingZoneGroup merchantCountryZone = new ShippingZoneGroup();
            merchantCountryZone.ZoneName = "Merchant Country Zone";

            ShippingZoneItem zoneItem = new ShippingZoneItem();
            zoneItem.CountryCode = merchantCountry;
            zoneItem.StateCode = String.Empty;
            zoneItem.ZipCode = String.Empty;

            merchantCountryZone.ZoneItem.Add( zoneItem );
            merchantCountryZone = DataAccessContext.ShippingZoneGroupRepository.Save( merchantCountryZone );

            DataTable table = DataAccess.ExecuteSelect( "SELECT * FROM Shipping; " );

            foreach (DataRow row in table.Rows)
            {
                ShippingOption shipping = DataAccessContext.ShippingOptionRepository.GetOne(
                    StoreContext.Culture, row["ShippingID"].ToString() );
                string destination = row["Destination"].ToString();

                if (!String.IsNullOrEmpty( destination ))
                {
                    switch (destination)
                    {
                        case "Domestic":
                            {
                                shipping.AllowedType = ShippingOption.ShippingZoneAllowedType.Allow;
                                shipping.ShippingZone.Add( new ShippingZone( merchantCountryZone.ZoneGroupID ) );
                                break;
                            }
                        case "International":
                            {
                                shipping.AllowedType = ShippingOption.ShippingZoneAllowedType.Deny;
                                shipping.ShippingZone.Add( new ShippingZone( merchantCountryZone.ZoneGroupID ) );
                                break;
                            }
                        default:
                            {
                                shipping.AllowedType = ShippingOption.ShippingZoneAllowedType.Worldwide;
                                break;
                            }
                    }

                    DataAccessContext.ShippingOptionRepository.Save( shipping );
                }
            }

            DropTableColumn( "Shipping", "Destination" );
        }

        private void UpdateCacheDependency()
        {
            if (!KeyUtilities.IsMultistoreLicense())
                return;

            string providerName
                = WebConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ProviderName;

            if (providerName != "System.Data.OleDb")
            {
                CacheDependencyUtility cache = new CacheDependencyUtility();
                string connectionString = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
                cache.CreateSqlCacheTableForNotifications( connectionString );
            }

        }

        private void InsertMetaInformation( DataRow row, string storeID )
        {
            DataAccess.ExecuteNonQuery( "INSERT INTO ProductMetaInformation (ProductID, CultureID,StoreID " +
                        "    ,MetaKeyword, MetaDescription , UseDefaultMetaKeyword, UseDefaultMetaDescription " +
                        "     ) " +
                        "VALUES (@ProductID, @CultureID, @StoreID, " +
                        "    @MetaKeyword, @MetaDescription ,@UseDefaultMetaKeyword, @UseDefaultMetaDescription" +
                        "     ); ",
                        DataAccess.CreateParameterString( row["ProductID"].ToString() ),
                        DataAccess.CreateParameterString( row["CultureID"].ToString() ),
                        DataAccess.CreateParameterString( storeID ),
                        DataAccess.CreateParameterString( row["MetaKeyword"].ToString() ),
                        DataAccess.CreateParameterString( row["MetaDescription"].ToString() ),
                        DataAccess.CreateParameterBool( true ),
                        DataAccess.CreateParameterBool( true )
                    );
        }

        private void updateMetaInformation( DataRow row, string storeID )
        {
            DataAccess.ExecuteNonQuery( "UPDATE ProductMetaInformation SET MetaKeyword = @MetaKeyword, MetaDescription = @MetaDescription " +
            "    WHERE ProductID = @ProductID AND CultureID  = @CultureID AND StoreID = @StoreID ; ",
            DataAccess.CreateParameterString( row["MetaKeyword"].ToString() ),
            DataAccess.CreateParameterString( row["MetaDescription"].ToString() ),
            DataAccess.CreateParameterString( row["ProductID"].ToString() ),
            DataAccess.CreateParameterString( row["CultureID"].ToString() ),
            DataAccess.CreateParameterString( storeID ) );
        }

        private void MoveMetaInformation()
        {
            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            foreach (DataRow row in _metaInformationTable.Rows)
            {
                try
                {
                    InsertMetaInformation( row, "0" );
                }
                catch
                {
                    updateMetaInformation( row, "0" );
                }

                foreach (Store store in storeList)
                {
                    try
                    {
                        InsertMetaInformation( row, store.StoreID );
                    }
                    catch
                    {
                        updateMetaInformation( row, store.StoreID );
                    }
                }
            }

            DropTableColumn( "ProductLocale", "MetaKeyword" );
            DropTableColumn( "ProductLocale", "MetaDescription" );
        }

        private void UpdateEmailTemplateList()
        {
            int emailTemplateCount = 3;
            EmailTemplateDetail emailTemplate;
            EmailTemplateDetailLocale emailTemplateLocale;
            IList<Culture> cultures = DataAccessContext.CultureRepository.GetAll();
            IList<Store> storeLists = new List<Store>();
            if (KeyUtilities.IsMultistoreLicense())
            {
                storeLists = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            }
            else
            {
                storeLists.Add( DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            List<string> name = new List<string>();
            name.Add( "Subscribe Newsletter Confirmation" );
            name.Add( "Customer Approval" );
            name.Add( "Customer Approval Confirmation" );

            List<string> description = new List<string>();
            description.Add( "Sent to customer after subscribe newsletter" );
            description.Add( "Send to web master for approval to their application when the customer registration." );
            description.Add( "Click 'Send Approve Mail' button inside Admin's Customer Edit menu to send this email." );

            List<string> subject = new List<string>();
            subject.Add( "[SiteName] - Subscribe Newsletter Confirmation" );
            subject.Add( "[SiteName] - New customer application for approval." );
            subject.Add( "[SiteName] - Approve Account" );

            List<string> body = new List<string>();
            body.Add( "There is a request to subscribe newsletter from [SiteName]. Please click the link follow to confirm. <BR /> " +
                            "<BR />You may ignore this message if you do not want to subscribe <BR />  " +
                            "<BR /> Email : [Email] <BR /> <BR /> [ConfirmationLink] <BR /> <BR />" );
            body.Add( "Dear Webmaster,<BR /><BR />This is an automatic email. There is a new customer application.<BR /><BR />" +
                            "You will need to approve this application manually.<BR /><BR />" +
                            "You can review the application with the link below.<BR /><BR />" +
                            "[CustomerViewUrl]<BR /><BR />======================<BR />Customer information:<BR /><BR />" +
                            "User Name: [UserName]<BR />Email: [Email]<BR />======================<BR /><BR />" +
                            "Regards,<BR />Support Team" );
            body.Add( "Dear Customer,<BR /><BR />Your register application has been approved.<BR /><BR />" +
                            "You are now able to login to our shop at<BR /><BR />[LoginLink]<BR /><BR /><BR />" +
                            "======================<BR />Your information:<BR /><BR />User Name: [UserName]<BR />Email: [Email]" +
                            "<BR />======================<BR /><BR />Regards,<BR />Support Team" );

            for (int i = 0; i < emailTemplateCount; i++)
            {
                foreach (Store store in storeLists)
                {
                    emailTemplate = new EmailTemplateDetail( StoreContext.Culture );
                    emailTemplate.EmailTemplateDetailName = name[i];
                    emailTemplate.Description = description[i];
                    emailTemplate.StoreID = store.StoreID;

                    foreach (Culture culture in cultures)
                    {
                        emailTemplateLocale = new EmailTemplateDetailLocale();

                        emailTemplateLocale.CultureID = culture.CultureID;
                        emailTemplateLocale.Body = body[i];
                        emailTemplateLocale.Subject = subject[i];

                        emailTemplate.Locales.Add( emailTemplateLocale );
                    }

                    DataAccessContext.EmailTemplateDetailRepository.Save( emailTemplate );
                }
            }
        }

        private void UpdateNewsList()
        {
            UrlNameAccessHelper _urlNameHelper = new UrlNameAccessHelper( "News", "NewsID" );
            IList<Culture> cultures = DataAccessContext.CultureRepository.GetAll();
            IList<Store> stores = new List<Store>();
            if (KeyUtilities.IsMultistoreLicense())
            {
                stores = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            }
            else
            {
                stores.Add( DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            foreach (Store store in stores)
            {
                foreach (Culture culture in cultures)
                {
                    IList<News> newsList = DataAccessContext.NewsRepository.GetAll( culture, "NewsID", store.StoreID );

                    if (newsList.Count != 0)
                    {
                        foreach (News news in newsList)
                        {
                            news.URLName = _urlNameHelper.CreateUrlName( culture.CultureID, news.NewsID.ToString(), news.Topic );
                            news.IsEnabled = true;
                            news.MetaDescription = news.Topic;
                            news.MetaKeyword = news.Topic;
                            news.MetaTitle = news.Topic;

                            DataAccessContext.NewsRepository.Save( news );
                        }
                    }
                }
            }
        }

        private void TransferContentTemplatesToDatabase()
        {
            DataTable list = EmailTemplates.GetTemplateList();
            string subject = String.Empty;
            string body = String.Empty;
            bool exist = false;

            foreach (DataRow row in list.Rows)
            {
                subject = String.Empty;
                body = String.Empty;

                string[] fileName = row["FileName"].ToString().Split( '|' );

                if (!fileName[0].Contains( "Invoice_Html" ))
                {

                    if (fileName.Length == 1)
                    {
                        exist = EmailTemplates.ReadTemplate( fileName[0], out body );
                    }
                    else
                    {
                        exist = EmailTemplates.ReadTemplate( fileName[0], fileName[1], out subject, out body );
                    }


                    EmailTemplateDetail emailTemplate = new EmailTemplateDetail( StoreContext.Culture );
                    EmailTemplateDetailLocale emailTemplateLocale = new EmailTemplateDetailLocale();
                    IList<Store> storeLists = new List<Store>();
                    if (KeyUtilities.IsMultistoreLicense())
                    {
                        storeLists = DataAccessContext.StoreRepository.GetAll( "StoreID" );
                    }
                    else
                    {
                        storeLists.Add( DataAccessContext.StoreRepository.GetOne( "1" ) );
                    }
                    IList<Culture> cultures = DataAccessContext.CultureRepository.GetAll();

                    foreach (Store store in storeLists)
                    {
                        emailTemplate = new EmailTemplateDetail( StoreContext.Culture );
                        emailTemplate.EmailTemplateDetailName = row["FileTitle"].ToString();
                        emailTemplate.Description = row["Description"].ToString();
                        if (KeyUtilities.IsMultistoreLicense())
                        {
                            emailTemplate.StoreID = store.StoreID;
                        }
                        else
                        {
                            emailTemplate.StoreID = "1";
                        }

                        if (exist)
                        {
                            foreach (Culture culture in cultures)
                            {
                                emailTemplateLocale = new EmailTemplateDetailLocale();

                                emailTemplateLocale.CultureID = culture.CultureID;
                                emailTemplateLocale.Body = body;
                                emailTemplateLocale.Subject = subject;

                                emailTemplate.Locales.Add( emailTemplateLocale );
                            }
                        }

                        DataAccessContext.EmailTemplateDetailRepository.Save( emailTemplate );
                    }
                }
            }
        }

        private void MoveShippingAddress()
        {
            DataTable customerTable = DataAccess.ExecuteSelect( "SELECT * FROM Customer ORDER BY CustomerID " );
            foreach (DataRow row in customerTable.Rows)
            {
                ShippingAddress shippingAddress = new ShippingAddress( new Address(
                row["ShippingFirstName"].ToString(), row["ShippingLastName"].ToString(),
                row["ShippingCompany"].ToString(), row["ShippingAddress1"].ToString(),
                row["ShippingAddress2"].ToString(), row["ShippingCity"].ToString(),
                row["ShippingState"].ToString(), row["ShippingZip"].ToString(),
                row["ShippingCountry"].ToString(), row["ShippingPhone"].ToString(),
                row["ShippingFax"].ToString() ),
                ConvertUtilities.ToBoolean( row["ShippingResidential"] ) );

                shippingAddress.CustomerID = row["CustomerID"].ToString();
                shippingAddress.AliasName = row["ShippingState"].ToString() + " , " + row["ShippingCountry"].ToString();

                if (ConvertUtilities.ToBoolean( row["ShippingResidential"] ))
                    shippingAddress.IsSameAsBillingAddress = true;
                else
                    shippingAddress.IsSameAsBillingAddress = false;

                // DataAccessContext.ShippingAddressRepository.Save( shippingAddress );
                DataAccess.ExecuteScalar( "INSERT INTO ShippingAddress ( CustomerID, AliasName, ShippingFirstName, " +
                "ShippingLastName, ShippingCompany, ShippingAddress1, ShippingAddress2, ShippingCity, " +
                "ShippingState, ShippingZip, ShippingCountry, ShippingPhone, ShippingFax, ShippingResidential, IsSameAsBillingAddress ) " +
                "VALUES ( @CustomerID, @AliasName, @ShippingFirstName, " +
                "@ShippingLastName , @ShippingCompany, @ShippingAddress1, @ShippingAddress2, @ShippingCity, " +
                "@ShippingState, @ShippingZip, @ShippingCountry, @ShippingPhone, @ShippingFax, @ShippingResidential, @IsSameAsBillingAddress ); " +
                "SELECT @@Identity; ",
                DataAccess.CreateParameterString( shippingAddress.CustomerID ),
                DataAccess.CreateParameterString( shippingAddress.AliasName ),
                DataAccess.CreateParameterString( shippingAddress.FirstName ),
                DataAccess.CreateParameterString( shippingAddress.LastName ),
                DataAccess.CreateParameterString( shippingAddress.Company ),
                DataAccess.CreateParameterString( shippingAddress.Address1 ),
                DataAccess.CreateParameterString( shippingAddress.Address2 ),
                DataAccess.CreateParameterString( shippingAddress.City ),
                DataAccess.CreateParameterString( shippingAddress.State ),
                DataAccess.CreateParameterString( shippingAddress.Zip ),
                DataAccess.CreateParameterString( shippingAddress.Country ),
                DataAccess.CreateParameterString( shippingAddress.Phone ),
                DataAccess.CreateParameterString( shippingAddress.Fax ),
                DataAccess.CreateParameterBool( shippingAddress.Residential ),
                DataAccess.CreateParameterBool( shippingAddress.IsSameAsBillingAddress )
                );
            }

            DropTableColumn( "Customer", "ShippingFirstName" );
            DropTableColumn( "Customer", "ShippingLastName" );
            DropTableColumn( "Customer", "ShippingCompany" );
            DropTableColumn( "Customer", "ShippingAddress1" );
            DropTableColumn( "Customer", "ShippingAddress2" );
            DropTableColumn( "Customer", "ShippingCity" );
            DropTableColumn( "Customer", "ShippingState" );
            DropTableColumn( "Customer", "ShippingZip" );
            DropTableColumn( "Customer", "ShippingCountry" );
            DropTableColumn( "Customer", "ShippingPhone" );
            DropTableColumn( "Customer", "ShippingFax" );

            DataAccess.ExecuteNonQuery( "ALTER TABLE Customer DROP CONSTRAINT DF__Customer__Shippi__2BC97F7C ; " );
            DropTableColumn( "Customer", "ShippingResidential" );

        }

        private void AddEmailTemplate( string name, string description, string subject, string body )
        {
            EmailTemplateDetail emailTemplate;
            EmailTemplateDetailLocale emailTemplateLocale;
            IList<Culture> cultures = DataAccessContext.CultureRepository.GetAll();
            IList<Store> storeLists = new List<Store>();
            if (KeyUtilities.IsMultistoreLicense())
            {
                storeLists = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            }
            else
            {
                storeLists.Add( DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            foreach (Store store in storeLists)
            {
                emailTemplate = new EmailTemplateDetail( StoreContext.Culture );
                emailTemplate.EmailTemplateDetailName = name;
                emailTemplate.Description = description;
                emailTemplate.StoreID = store.StoreID;
                emailTemplate.EmailTemplateDetailID = "0";

                foreach (Culture culture in cultures)
                {
                    emailTemplateLocale = new EmailTemplateDetailLocale();

                    emailTemplateLocale.CultureID = culture.CultureID;
                    emailTemplateLocale.Body = body;
                    emailTemplateLocale.Subject = subject;

                    emailTemplate.Locales.Add( emailTemplateLocale );
                }

                DataAccessContext.EmailTemplateDetailRepository.Save( emailTemplate );
            }
        }

        private void AddPromotionTellAFriendEmailTemplate()
        {
            string name = "Promotion Tell A Friend";
            string description = "A default email that appears when a customer click \"Tell A Friend\" in Promotion Page.";
            string subject = "Interesting Promotion. Check This Out!";
            string body = "Hello, <br /> <br /> This following promotion has been recommended by your friend.<br /> <br />" +
                    "Name: <span style=\"color: blue;\">[Name]</span> <br /> Description: [ShortDescription] <br /> <br />" +
                    "[ImageSecondary] <br /> Our Price: <span style=\"text-decoration: none; color: #ff0000;\">[Price]</span>  <br /> <br /> <br /> <br />" +
                    "[TellFriendTextLink Text=\"Please visit this website for more details.\"]";

            AddEmailTemplate( name, description, subject, body );
        }

        private void AddRmaRequisitionEmailTemplate()
        {
            string name = "RMA Requisition";
            string description = "Sent to merchant when a customer need to return product";
            string subject = "[SiteName] - RMA Requisition";
            string body = "Dear Webmaster,<BR /><BR />This is an automatic email. " +
                "There is a customer RMA application.<BR /><BR />You will need to approve this application manually.<BR /><BR />" +
                "You can review the application with the link below.<BR /><BR />[RmaViewUrl]<BR /><BR />======================<BR />" +
                "Customer information:<BR /><BR />User Name: [UserName]<BR />Email: [Email]<BR />======================<BR /><BR />" +
                "Regards,<BR />Support Team";

            AddEmailTemplate( name, description, subject, body );
        }

        private void AddRmaApprovalEmailTemplate()
        {
            string name = "RMA Approval";
            string description = "Sent to customer to inform the RMA of the customer has been approved by the merchant.";
            string subject = "[SiteName] - RMA Approval";
            string body = "Dear Customer,<BR /><BR />Your RMA requisition application has been approved.<BR /> <BR />" +
                "If you have any questions, please feel free to contact us by replying to this email.<BR /><BR /><BR />" +
                "======================<BR />Your information:<BR /><BR />[RmaDetails]<BR />======================<BR /><BR />" +
                "Regards,<BR />Support Team";

            AddEmailTemplate( name, description, subject, body );
        }

        private void AddRmaRejectEmailTemplate()
        {
            string name = "RMA Rejected";
            string description = "Sent to customer to inform the RMA of the customer has been rejected by the merchant.";
            string subject = "[SiteName] - RMA Rejected";
            string body = "Dear Customer,<BR /><BR />Your RMA requisition application has been rejected.<BR /> <BR />" +
                "If you have any questions, please feel free to contact us by replying to this email.<BR /><BR /><BR />" +
                "======================<BR />Your information:<BR /><BR />[RmaDetails]<BR />======================<BR /><BR />" +
                "Regards,<BR />Support Team";

            AddEmailTemplate( name, description, subject, body );
        }

        private void AddSubscriptionContentLinkEmailTemplate()
        {
            string name = "Subscription Content Link";
            string description = "Sent to customers when subscription product are purchased to provide subscription content links.";
            string subject = "Subscription Content Link(s) for Your Order";
            string body = "You are granted to content subscription level [SubscriptionLevel] by purchasing order number: [OrderNo]. <br /> <br />" +
                " Level [SubscriptionLevel] content(s) of [SiteName] ([SiteUrl]) allow you to access now.<br />  <br />  <br />";

            AddEmailTemplate( name, description, subject, body );
        }

        private void MoveUrlName()
        {
            Culture englishCulture = DataAccessContext.CultureRepository.GetOne( "1" );

            IList<Store> storeLists = new List<Store>();
            if (KeyUtilities.IsMultistoreLicense())
            {
                storeLists = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            }
            else
            {
                storeLists.Add( DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            //Category Table
            IList<Category> categoryList = DataAccessContext.CategoryRepository.GetAll( englishCulture, "CategoryID" );
            foreach (Category category in categoryList)
            {
                DataAccessContext.CategoryRepository.Save( category );
            }

            //Content Table
            IList<Content> contentList = DataAccessContext.ContentRepository.GetAll( englishCulture, BoolFilter.ShowAll, "ContentID" );
            foreach (Content content in contentList)
            {
                DataAccessContext.ContentRepository.Save( content );
            }

            //ContentMenuItem Table
            IList<ContentMenuItem> contentMenuItemList = DataAccessContext.ContentMenuItemRepository.GetAll( englishCulture, BoolFilter.ShowAll );
            foreach (ContentMenuItem contentMenuItem in contentMenuItemList)
            {
                DataAccessContext.ContentMenuItemRepository.Save( contentMenuItem );
            }

            //Department Table
            IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetAll( englishCulture, "DepartmentID" );
            foreach (Department department in departmentList)
            {
                DataAccessContext.DepartmentRepository.Save( department );
            }

            //News Table
            foreach (Store store in storeLists)
            {
                IList<News> newsList = DataAccessContext.NewsRepository.GetAll( englishCulture, "NewsID", store.StoreID );
                foreach (News news in newsList)
                {
                    DataAccessContext.NewsRepository.Save( news );
                }
            }

            //Product Table
            int productCount = DataAccessContext.ProductRepository.GetAllProductCount();
            int howManyItem = 0;
            SearchFilter filter = new SearchFilter();

            foreach (Store store in storeLists)
            {
                IList<Product> productList = DataAccessContext.ProductRepository.SearchProduct( englishCulture, "", "ProductID", filter, 0, productCount, out howManyItem, store.StoreID, "" );
                foreach (Product product in productList)
                {
                    DataAccessContext.ProductRepository.Save( product );
                }
            }

            //PromotionGroup Table
            foreach (Store store in storeLists)
            {
                IList<PromotionGroup> promotionGroupList = DataAccessContextDeluxe.PromotionGroupRepository.GetAll( englishCulture, store.StoreID, "PromotionGroupID", BoolFilter.ShowAll );
                foreach (PromotionGroup promotionGroup in promotionGroupList)
                {
                    DataAccessContextDeluxe.PromotionGroupRepository.Save( promotionGroup );
                }
            }
        }

        private void GetBannerList()
        {
            IList<Store> storeLists = new List<Store>();
            if (KeyUtilities.IsMultistoreLicense())
            {
                storeLists = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            }
            else
            {
                storeLists.Add( DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            foreach (Store store in storeLists)
            {
                string bannerUrl = DataAccessContext.Configurations.GetValue( "StoreBannerImage", store );

                _bannerDic.Add( store.StoreID, bannerUrl );
            }
        }

        private void MoveStoreBanner()
        {
            Banner banner;
            BannerLocale locale;
            if (_bannerDic.Count > 0)
            {
                foreach (string storeID in _bannerDic.Keys)
                {
                    banner = new Banner( StoreContext.Culture );
                    banner.Name = "Default Banner";
                    banner.IsEnabled = true;
                    banner.CreateDate = DateTime.Now;
                    banner.EndDate = DateTime.Now.AddYears( 5 );
                    banner.StoreID = storeID;

                    IList<Culture> cultureList = DataAccessContext.CultureRepository.GetAll();

                    foreach (Culture culture in cultureList)
                    {
                        locale = new BannerLocale();
                        locale.CultureID = culture.CultureID;
                        locale.ImageURL = _bannerDic[storeID].ToString();

                        banner.Locales.Add( locale );
                    }
                    DataAccessContext.BannerRepository.Save( banner );
                }
            }
        }

        private void MoveNewsImageToBlog( string imagePath )
        {
            if (!String.IsNullOrEmpty( imagePath ))
            {
                string sourcePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + imagePath;
                string destPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + imagePath.Replace( "Images/News/", "Images/Blog/" );

                if (!File.Exists( sourcePath ) || File.Exists( destPath ))
                    return;

                File.Move( sourcePath, destPath );
            }
        }

        private void MoveNewsToBlog()
        {
            IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );

            foreach (Store store in list)
            {
                Culture englishCulture = DataAccessContext.CultureRepository.GetOne( "1" );
                IList<News> newsList = DataAccessContext.NewsRepository.GetAll( englishCulture, "NewsID", store.StoreID );

                foreach (News news in newsList)
                {
                    Blog blog = new Blog();
                    blog.BlogCategoryIDs = new List<string>();
                    blog.BlogCategoryIDs.Add( "1" );
                    blog.BlogContent = news.Description;
                    blog.BlogTitle = news.Topic;
                    blog.CreateDate = news.NewsDate;
                    blog.ImageFile = news.ImageFile.Replace( "Images/News/", "Images/Blog/" );
                    blog.IsEnabled = news.IsEnabled;
                    blog.MetaDescription = news.MetaDescription;
                    blog.MetaKeyword = news.MetaKeyword;
                    blog.MetaTitle = news.MetaTitle;
                    blog.ShortContent = news.Name;
                    blog.StoreIDs = new List<string>();
                    blog.StoreIDs.Add( news.StoreID );

                    DataAccessContext.BlogRepository.Save( blog );
                    MoveNewsImageToBlog( news.ImageFile );
                    DataAccessContext.NewsRepository.Delete( news.NewsID );
                }
            }
        }

        private void UpdateLayout()
        {
            IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );

            IList<string> oldProductListLayouts = new List<string>();
            oldProductListLayouts.Add( "ProductListColumnStyle1.ascx" );
            oldProductListLayouts.Add( "ProductListColumnStyle2.ascx" );
            oldProductListLayouts.Add( "ProductListColumnStyle3.ascx" );
            oldProductListLayouts.Add( "ProductListRowandGridStyle.ascx" );
            oldProductListLayouts.Add( "ProductListRowStyle.ascx" );
            oldProductListLayouts.Add( "ProductListTabelStyle.ascx" );

            IList<string> oldProductDetailLayouts = new List<string>();
            oldProductDetailLayouts.Add( "ProductDetailsDefault.ascx" );
            oldProductDetailLayouts.Add( "ProductDetailsDefault2.ascx" );
            oldProductDetailLayouts.Add( "ProductDetailsDefault3.ascx" );
            oldProductDetailLayouts.Add( "ProductDetailsDefault4.ascx" );

            IList<string> oldThemes = new List<string>();
            oldThemes.Add( "Default" );
            oldThemes.Add( "Default2" );

            IList<string> oldBlogThemes = new List<string>();
            oldBlogThemes.Add( "BlogTheme" );
            oldBlogThemes.Add( "Default" );

            foreach (Store store in list)
            {
                string productListLayout = DataAccessContext.Configurations.GetValue( "DefaultProductListLayout", store );
                if (oldProductListLayouts.Contains( productListLayout ))
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DefaultProductListLayout"],
                        "ProductListRowandGridStyle2.ascx",
                        store );
                }

                string productDetailsLayout = DataAccessContext.Configurations.GetValue( "DefaultProductDetailsLayout", store );
                if (oldProductDetailLayouts.Contains( productDetailsLayout ))
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["DefaultProductDetailsLayout"],
                        "ProductDetailsDefaultBlue.ascx",
                        store );
                }

                string storeTheme = DataAccessContext.Configurations.GetValue( "StoreTheme", store );
                if (oldThemes.Contains( storeTheme ))
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["StoreTheme"],
                        "DefaultBlue",
                        store );
                }

                string blogTheme = DataAccessContext.Configurations.GetValue( "BlogTheme", store );
                if (oldBlogThemes.Contains( blogTheme ))
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations["BlogTheme"],
                        "DefaultBlue",
                        store );
                }
            }
        }

        private void EnableCustomerPerStore()
        {
            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            IList<Customer> customerList = DataAccessContext.CustomerRepository.GetAll( "CustomerID" );

            foreach (Customer customer in customerList)
            {
                if (customer.StoreIDs.Count <= 0)
                {
                    customer.StoreIDs = new List<string>();
                    foreach (Store store in storeList)
                    {
                        customer.StoreIDs.Add( store.StoreID );
                    }
                    DataAccessContext.CustomerRepository.Save( customer );
                }
            }
        }

        private void EnablePaymentPerStore()
        {
            IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            Culture englishCulture = DataAccessContext.CultureRepository.GetOne( "1" );
            IList<PaymentOption> paymentList = DataAccessContext.PaymentOptionRepository.GetAll( englishCulture, BoolFilter.ShowAll );

            foreach (PaymentOption option in paymentList)
            {
                if (DataAccessContext.PaymentOptionRepository.GetAllPaymentOptionStoreByName( option.Name ) == null)
                {
                    foreach (Store store in storeList)
                    {
                        PaymentOptionStore poStore = new PaymentOptionStore();
                        poStore.Name = option.Name;
                        poStore.StoreID = store.StoreID;
                        poStore.IsEnabled = true;
                        DataAccessContext.PaymentOptionRepository.CreatePaymentOptionStore( poStore );
                    }
                }
            }
        }

        #endregion


        #region Constructors

        public DatabaseConverter()
        {
            _configTable = GetAllConfigurations( Culture.Null.CultureID );
        }

        #endregion


        #region Public Methods

        public void OnScriptExecuting()
        {
            _flagBeforeGenerate = IsProductColumnExisted( "ImagePrimary" );
            _flagBeforeGenerateUpgrade303 = DataAccess.IsColumnExisted( "Customer", "AffiliateCode" );
            _flagBeforeGenerateUpgrade400 = DataAccess.IsColumnExisted( "Orders", "SecondaryCurrency" );
            _flagBeforeGenerateUpgrade410 = DataAccess.IsColumnExisted( "Customer", "IsTaxExempt" );
            _flagBeforeGenerateUpgrade420 = DataAccess.IsColumnExisted( "Orders", "StoreID" );
            _flagBeforeGenerateUpgrade421 = DataAccess.IsColumnExisted( "Newsletter", "StoreID" );
            _flagBeforeGenerateUpgrade430 = DataAccess.IsColumnExisted( "Shipping", "AllowedType" );
            _flagBeforeGenerateUpgrade440 = DataAccess.IsTableExisted( "EmailTemplateDetail" );
            _flagBeforeGenerateUpgrade520 = DataAccess.IsColumnExisted( "News", "URLName" );

            string confirmationMailID = DataAccessContext.EmailTemplateDetailRepository.GetIDByNameAndStoreID(
                StoreContext.Culture, "Subscribe Newsletter Confirmation", Store.RegularStoreID );

            if (String.IsNullOrEmpty( confirmationMailID ) || confirmationMailID == "0")
            {
                _flagBeforeGenerateUpgrade500 = false;
            }
            else
            {
                _flagBeforeGenerateUpgrade500 = true;
            }

            if (!String.IsNullOrEmpty( GetConfigValue( "eWayUseCVN" ) ))
                _eWayUseCVN = ConvertUtilities.ToBoolean( GetConfigValue( "eWayUseCVN" ) );

            if (DataAccess.IsColumnExisted( "ProductLocale", "MetaKeyword" ))
            {
                _metaInformationTable = DataAccess.ExecuteSelect( "SELECT ProductID, CultureID, MetaKeyword, MetaDescription FROM ProductLocale; " );
            }

            string templateCount = DataAccess.ExecuteScalar( "SELECT COUNT(*) AS Row FROM EmailTemplateDetail e LEFT JOIN EmailTemplateDetailLocale el " +
                "ON e.EmailTemplateDetailID = el.EmailTemplateDetailID WHERE e.EmailTemplateDetailName = 'Promotion Tell A Friend'" );

            if (ConvertUtilities.ToInt32( templateCount ) == 0)
            {
                _flagBeforeGenerateUpgrade530 = false;
            }
            else
            {
                _flagBeforeGenerateUpgrade530 = true;
            }

            if (DataAccess.IsColumnExisted( "CategoryLocale", "ImageFile" ) && DataAccess.IsColumnExisted( "DepartmentLocale", "ImageFile" ))
            {
                _flagBeforeGenerateUpgrade540 = true;
            }
            else
            {
                _flagBeforeGenerateUpgrade540 = false;
            }

            if (DataAccess.IsTableExisted( "Banner" ))
            {
                _flagBeforeGenerateUpgrade600 = true;
            }
            else
            {
                GetBannerList();
                _flagBeforeGenerateUpgrade600 = false;
            }
        }

        public void OnScriptExecuted()
        {
            _flagAfterGenerate = IsProductColumnExisted( "ImagePrimary" );
            _flagAfterGenerateUpgrade303 = DataAccess.IsColumnExisted( "Customer", "AffiliateCode" );
            _flagAfterGenerateUpgrade400 = DataAccess.IsColumnExisted( "Orders", "SecondaryCurrency" );
            _flagAfterGenerateUpgrade410 = DataAccess.IsColumnExisted( "Customer", "IsTaxExempt" );
            _flagAfterGenerateUpgrade420 = DataAccess.IsColumnExisted( "Orders", "StoreID" );
            _flagAfterGenerateUpgrade421 = DataAccess.IsColumnExisted( "Newsletter", "StoreID" );
            _flagAfterGenerateUpgrade430 = DataAccess.IsColumnExisted( "Shipping", "AllowedType" );
            _flagAfterGenerateUpgrade440 = DataAccess.IsTableExisted( "EmailTemplateDetail" );
            _flagAfterGenerateUpgrade520 = DataAccess.IsColumnExisted( "News", "URLName" );

            string confirmationMailID = DataAccessContext.EmailTemplateDetailRepository.GetIDByNameAndStoreID(
                StoreContext.Culture, "Subscribe Newsletter Confirmation", Store.RegularStoreID );

            _flagAfterGenerateUpgrade500 = !String.IsNullOrEmpty( confirmationMailID );

            string templateCount = DataAccess.ExecuteScalar( "SELECT COUNT(*) AS Row FROM EmailTemplateDetail e LEFT JOIN EmailTemplateDetailLocale el " +
                "ON e.EmailTemplateDetailID = el.EmailTemplateDetailID WHERE e.EmailTemplateDetailName = 'Promotion Tell A Friend'" );

            if (ConvertUtilities.ToInt32( templateCount ) == 0)
            {
                _flagAfterGenerateUpgrade530 = true;
            }
            else
            {
                _flagAfterGenerateUpgrade530 = false;
            }

            if (DataAccess.IsColumnExisted( "CategoryLocale", "ImageFile" ) && DataAccess.IsColumnExisted( "DepartmentLocale", "ImageFile" ))
            {
                _flagAfterGenerateUpgrade540 = true;
            }
            else
            {
                _flagAfterGenerateUpgrade540 = false;
            }

            _flagAfterGenerateUpgrade600 = DataAccess.IsTableExisted( "Banner" );

            if (DataAccess.IsColumnExisted( "PaymentOption", "IntegratedProviderClassName" ))
            {
                _flagAfterGenerateUpgrade601 = true;
            }
            else
            {
                _flagAfterGenerateUpgrade601 = false;
            }

            if (DataAccess.IsTableExisted( "BlogCategory" ) &&
                DataAccess.IsTableExisted( "CustomerStore" ) &&
                DataAccess.IsTableExisted( "PaymentOptionStore" ))
            {
                _flagAfterGenerateUpgrade610 = true;
            }
            else
            {
                _flagAfterGenerateUpgrade610 = false;
            }
        }

        public void Convert()
        {
            if (IsUpgradingTo300())
            {
                System.Threading.Thread.Sleep( SystemConst.WaitDatabaseConfigUpgrade );
                CopyImage( new StoreRetriever().GetCurrentStoreID() );
                CopyDiscount();
            }

            if (IsUpgradingTo303())
            {
                System.Threading.Thread.Sleep( SystemConst.WaitDatabaseConfigUpgrade );
                CreateAffiliateRoles();
            }

            if (IsUpgradingTo400())
            {
                // Only update configuration in version 4.0
                UpdateConfigurations();

                System.Threading.Thread.Sleep( SystemConst.WaitDatabaseConfigUpgrade );
                //CreateAffiliateRoles();
                //if (!KeyUtilities.IsDeluxeLicense())
                //{
                //    SetupTaxRule();
                //}
                eWayConfigSetup();
                ConvertWishList( new StoreRetriever().GetCurrentStoreID() );
                ConvertArticleToContent();
                ConvertProductDisplayStyle();

                // Set up PaymentModule
                PaymentModuleSetup paymentModule = new PaymentModuleSetup();
                paymentModule.ProcessDatabaseConnected();
            }

            if (IsUpgradingTo410())
            {
                UpdateConfigurations();
            }

            if (IsUpgradingTo420())
            {
                UpdateCacheDependency();
                UpdateConfigurations();
                SetUpWebsiteTitleConfig();
                CreateDefaultStore();
            }

            if (IsUpgradingTo421())
            {
                UpdateStoreConfigurations();
                string baseCurrency = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["DefaultDisplayCurrencyCode"],
                    baseCurrency,
                    DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            }

            if (IsUpgradingTo430())
            {
                UpdateCacheDependency();
                UpdateStoreConfigurations();
                CovertShippingDestionation();
                CreateRootCategory();
                CreateProductPrice();
                DropProductPriceColumn();
                MoveMetaInformation();
            }

            if (IsUpgradingTo440())
            {
                TransferContentTemplatesToDatabase();
            }

            if (IsUpgradingTo500())
            {
                UpdateEmailTemplateList();
            }

            if (IsUpgradingTo520())
            {
                UpdateNewsList();
                MoveShippingAddress();
            }

            if (IsUpgradingTo530())
            {
                AddPromotionTellAFriendEmailTemplate();
                AddRmaRequisitionEmailTemplate();
                AddRmaApprovalEmailTemplate();
                AddRmaRejectEmailTemplate();
                AddSubscriptionContentLinkEmailTemplate();
            }

            if (IsUpgradeingTo540())
            {
                MoveUrlName();
            }

            if (IsUpgradeingTo600())
            {
                MoveStoreBanner();
            }

            UpdateCacheDependency();
            UpdateStoreConfigurations();

            if (IsUpgradeingTo601())
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["VevoPayPADSSMode"], "True" );
            }

            if (IsUpgradingTo610())
            {
                MoveNewsToBlog();
                UpdateLayout();
                EnableCustomerPerStore();
                EnablePaymentPerStore();
            }
        }

        public void UpdateStoreConfigurations()
        {
            DataAccessContext.EnableConfigurations( true );
            WidgetDirector widgetDirector = new WidgetDirector();
            SystemConfig.UpdateNewConfigValue( widgetDirector.WidgetConfigurationCollection );
            DataAccessContext.ClearConfigurationCache();
            ConfigurationHelper.ApplyConfigurations();

            IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );
            foreach (Store store in list)
            {
                store.CreateStoreConfigCollection( store.StoreID );

                foreach (string widgetConfigName in widgetDirector.WidgetConfigurationNameAll)
                {
                    DataAccessContext.ConfigurationRepository.UpdateValue(
                        DataAccessContext.Configurations[widgetConfigName],
                    DataAccessContext.Configurations.GetValue( widgetConfigName, Store.Null ),
                        store );
                }
            }
        }

        #endregion
    }

}
