using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain.Products;
using Vevo.Domain.Base;
using Vevo.Domain;
using System.Collections.Generic;

namespace Vevo
{
    /// <summary>
    /// Summary description for ImageItem
    /// </summary>
    public class ImageItem: ILocaleLoader
    {
        private string _productImageID;
        private string _thumbnailImage;
        private string _regularImage;
        private string _largeImage;
        private int _imagize;
        private int _sortOrder;
        private bool _isZoom;
        private bool _isEnlarge;
        private int _imageWidth;
        private int _imageHeight;

        private LocaleCollection<ProductImageLocale> _locales;

        public ImageItem(
            string productImageID,
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
            _productImageID = productImageID;
            _thumbnailImage = thumbnailImage;
            _regularImage = regularImage;
            _largeImage = largeImage;
            _imagize = imageSize;
            _sortOrder = sortOrder;
            _isZoom = isZoom;
            _isEnlarge = isEnlarge;
            _imageWidth = imageWidth;
            _imageHeight = imageHeight;

            _locales = new LocaleCollection<ProductImageLocale>( this);
            Locales[culture].AltTag = altTag;
            Locales[culture].TitleTag = titleTag;
        }

        public string ProductImageID
        {
            get { return _productImageID; }
            set { _productImageID = value; }
        }

        public string ThumbnailImage
        {
            get { return _thumbnailImage; }
            set { _thumbnailImage = value; }
        }

        public string RegularImage
        {
            get { return _regularImage; }
            set { _regularImage = value; }
        }

        public string LargeImage
        {
            get { return _largeImage; }
            set { _largeImage = value; }
        }

        public int ImageSize
        {
            get { return _imagize; }
            set { _imagize = value; }
        }
        
        public int SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        public bool IsZoom
        {
            get { return _isZoom; }
            set { _isZoom = value; }
        }

        public bool IsEnlarge
        {
            get { return _isEnlarge; }
            set { _isEnlarge = value; }
        }

        public int ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; }
        }

        public int ImageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; }
        }

        public LocaleCollection<ProductImageLocale> Locales { get { return _locales; } }

        #region ILocaleLoader Members
        public IList<ILocale> GetLocales()
        {
            List<ILocale> result = new List<ILocale>();

            foreach (ProductLocale locale in DataAccessContext.ProductRepository.GetImageLocales( ProductImageID ))
                result.Add( locale );

            return result;
        }
        #endregion

    }    
}
