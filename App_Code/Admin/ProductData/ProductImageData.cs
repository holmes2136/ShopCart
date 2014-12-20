using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.DataAccessLib.Cart;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Domain.Products;
using Vevo.Domain;

namespace Vevo
{
    /// <summary>
    /// Summary description for ProductImageData
    /// </summary>
    public static class ProductImageData
    {
        private const string SecondarySessionName = "AdminProductImageSecondary";

        private static ArrayList ImageSession
        {
            get
            {
                return (ArrayList) HttpContext.Current.Session["ProductImage"];
            }
        }

        private static string GetNextImageID()
        {
            if (ImageSession.Count == 0)
                return "1";
            else
                return ConvertUtilities.ToInt32( ImageSession.Count + 1 ).ToString();
        }


        public static string SecondaryImagePath
        {
            get
            {
                if (HttpContext.Current.Session[SecondarySessionName] == null)
                    HttpContext.Current.Session[SecondarySessionName] = String.Empty;
                return HttpContext.Current.Session[SecondarySessionName].ToString();
            }
            set
            {
                HttpContext.Current.Session[SecondarySessionName] = value;
            }
        }


        public static void Clear()
        {
            ImageSession.Clear();
            HttpContext.Current.Session.Remove( SecondarySessionName );
        }

        public static void AddImageItem(
            string thumbnailImage,
            string regularImage,
            string largeImage,
            int imageSize,
            int sortOrder,
            bool isZoom,
            bool isEnlarge,
            int imageWidth,
            int imageHeight,
            Culture culture,
            string altTag,
            string titleTag
            )
        {
            ImageItem item = new ImageItem( GetNextImageID(), thumbnailImage, regularImage,
                largeImage, imageSize, sortOrder, isZoom, isEnlarge, imageWidth, imageHeight,culture, altTag,titleTag );
            ImageSession.Add( item );
        }

        public static int GetNexOrder()
        {
            int nextOrder = 0;
            foreach (ImageItem item in ImageSession)
            {
                if (item.SortOrder > nextOrder)
                    nextOrder = item.SortOrder;
            }
            if (ImageSession.Count > 0)
                nextOrder = nextOrder + 1;
            return nextOrder;
        }

        public static ArrayList GetAllItems()
        {
            return ImageSession;
        }

        public static int GetTotalImageSize()
        {
            int totalImageSize = 0;
            foreach (ImageItem item in ImageSession)
            {
                totalImageSize += item.ImageSize;
            }
            return totalImageSize;
        }

        public static void RemoveItem( string imageID )
        {
            ImageSession.Remove( FindItem( imageID ) );
        }

        public static ImageItem FindItem( string imageID )
        {
            foreach (ImageItem item in ImageSession)
            {
                if (item.ProductImageID == imageID)
                    return item;
            }
            return null;
        }

        public static ImageItem FindPrimaryItem()
        {
            foreach (ImageItem item in ImageSession)
            {
                if (item.SortOrder == 0)
                    return item;
            }
            return null;
        }

        public static void SetPrimary( string imageID )
        {
            ImageItem primaryItem = FindPrimaryItem();
            ImageItem item = FindItem( imageID );

            if (primaryItem == null && item != null)
            {
                item.SortOrder = 0;
            }
            else if (primaryItem != null && item != null)
            {
                primaryItem.SortOrder = item.SortOrder;
                item.SortOrder = 0;
            }
        }

        public static void UpdateItem( string imageID, bool isZoom, bool isEnlarge,Culture culture,string altTag,string titleTag )
        {
            ImageItem item = FindItem( imageID );
            item.IsZoom = isZoom;
            item.IsEnlarge = isEnlarge;
            item.Locales[culture].AltTag = altTag;
            item.Locales[culture].TitleTag = titleTag;
        }

        public static void UpdateSortOrder( string imageID, int sortigOrder )
        {
            ImageItem item = FindItem( imageID );
            item.SortOrder = sortigOrder;
        }

        public static void PopulateProductImages( Product product )
        {
            if (ImageSession.Count > 0)
                product.ProductImages.Clear();

            foreach (ImageItem item in ImageSession)
            {
                ProductImage productImage = new ProductImage();
                productImage.RegularImage = item.RegularImage;
                productImage.LargeImage = item.LargeImage;
                productImage.ThumbnailImage = item.ThumbnailImage;
                productImage.SortOrder = item.SortOrder;
                productImage.IsZoom = item.IsZoom;
                productImage.IsEnlarge = item.IsEnlarge;

                foreach (ProductImageLocale locale in item.Locales)
                {
                    productImage.Locales.Add( locale );
                }

                product.ProductImages.Add( productImage );
            }
        }
    }
}
