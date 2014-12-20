using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.Custom;
using Vevo.Domain.Users;
using Vevo.DataAccessLib.Cart;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.Deluxe.Domain.Orders;

namespace Vevo
{
    /// <summary>
    /// Summary description for ExportUtility
    /// </summary>
    public class ExportUtility
    {
        public bool IncludeOutOfStock( bool includeOutOfStock, object stock, object useInventory )
        {
            if (includeOutOfStock || (!CatalogUtilities.IsOutOfStock( stock, useInventory )))
                return true;
            else
                return false;
        }

        public string ReplaceString( string str )
        {
            return "\"" + str.Replace( "\"", "\"\"" ) + "\"";
        }

        public string FormatNumber( object obj )
        {
            decimal number = ConvertUtilities.ToDecimal( obj );
            return String.Format( "{0:n2}", number );
        }

        public string GetCategoryName( string categoryID )
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( AdminConfig.CurrentCulture, categoryID );
            return category.CreateFullCategoryPath();
        }

        public string GetProductUrl( string productID, string url, string storefrontUrl )
        {
            if (storefrontUrl[storefrontUrl.Length - 1] != '/')
                storefrontUrl += "/";

            string productUrl = EncodeUrl( storefrontUrl + UrlManager.GetProductUrl( productID, url ).Replace( "~/", "" ) );

            return productUrl.Replace( "https://", "http://" );     // Do not return https:// for storefront prouduct & image links
        }

        public string GetImageUrl( Product product, string storefrontUrl )
        {
            if (storefrontUrl[storefrontUrl.Length - 1] != '/')
                storefrontUrl += "/";

            string imageFile = product.GetPrimaryProductImage().RegularImage;
            string imageUrl = "";
            if (!string.IsNullOrEmpty( imageFile ))
                imageUrl = storefrontUrl + imageFile;

            // Do not return https:// for storefront prouduct & image links
            return EncodeUrl( imageUrl ).Replace( "https://", "http://" );
        }

        public string GetPrice( object price )
        {
            return FormatNumber( price );
        }

        public string GetShippingCost( bool useProductShippingCost, string productID, string shippingID, object price )
        {
            decimal shippingCost = 0;
            decimal handlingFee = 0;
            Product product = DataAccessContext.ProductRepository.GetOne(
                AdminConfig.CurrentCulture, productID, new StoreRetriever().GetCurrentStoreID() );

            CartItem cartItem = new CartItem( Cart.Null.CartID, product, 1 );
            CartItemGroup cartItemGroup = new CartItemGroup( cartItem );

            ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
                AdminConfig.CurrentCulture, shippingID );

            if (!shippingOption.IsNull)
            {
                ShippingMethod shippingMethod = shippingOption.CreateNonRealTimeShippingMethod();

                shippingCost = shippingMethod.GetShippingCost( cartItemGroup, WholesaleStatus.Null, 0 )
                    + CartItemPromotion.GetShippingCostFromPromotion( shippingMethod,
                        cartItemGroup,
                        WholesaleStatus.Null,
                        0 );
                handlingFee = shippingMethod.GetHandlingFee( cartItemGroup, WholesaleStatus.Null );
            }
            return FormatNumber( shippingCost + handlingFee );
        }

        public string ReplaceNonAlphaAndDigit( string name )
        {
            string newName = string.Empty;
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsLetterOrDigit( name[i] ) || char.IsWhiteSpace( name[i] ))
                    newName += name[i];
                else
                    newName += "";
            }
            return newName;
        }

        public string EncodeUrl( string url )
        {
            string fileName = Path.GetFileName( url );
            string urlPath = url.Substring( 0, url.Length - fileName.Length );
            return urlPath + Uri.EscapeDataString( fileName );
        }

        public bool GetStockAvailable( object stock, object productID, string storeID )
        {
            Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, Convert.ToString( productID ), storeID );
            if (CatalogUtilities.IsOutOfStock( stock, product.UseInventory ))
                return true;
            else
                return false;
        }
    }
}
