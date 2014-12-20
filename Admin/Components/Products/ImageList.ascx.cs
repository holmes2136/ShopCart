using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Products.BackOffice;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_Products_ImageList : AdminAdvancedBaseUserControl
{
    #region Private

    private const int ImageIDIndex = 0;
    private const int AltTagIndex = 1;
    private const int TitleTagIndex = 2;
    private const int ZoomCheckBoxIndex = 5;
    private const int EnlargeCheckBoxIndex = 6;

    private DataTable _imageSource = new DataTable( "ProductImage" );
    private DataTable _tempImageSource = new DataTable( "TempProductImage" );

    private DataTable CreateTable( string name )
    {
        DataTable table = new DataTable( name );
        table.Columns.Add( "ProductImageID", typeof( string ) );
        table.Columns.Add( "ProductID", typeof( Int32 ) );
        table.Columns.Add( "RegularImage", typeof( string ) );
        table.Columns.Add( "LargeImage", typeof( string ) );
        table.Columns.Add( "ThumbnailImage", typeof( string ) );
        table.Columns.Add( "Imagesize", typeof( Int32 ) );
        table.Columns.Add( "SortOrder", typeof( Int32 ) );
        table.Columns.Add( "IsZoom", typeof( Boolean ) );
        table.Columns.Add( "IsEnlarge", typeof( Boolean ) );
        table.Columns.Add( "ImageWidth", typeof( Int32 ) );
        table.Columns.Add( "ImageHeight", typeof( Int32 ) );
        table.Columns.Add( "AltTag", typeof( string ) );
        table.Columns.Add( "TitleTag", typeof( string ) );
        return table;
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGridProductImage, "SortOrder" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void RefreshGrid()
    {
        CrateDataSource();
        SortDataBySortOrder();
        PopulateThumbnail();
        uxGridProductImage.DataSource = _imageSource;
        uxGridProductImage.DataBind();
    }

    private void CrateDataSource()
    {
        _imageSource = CreateTable( "ProductImage" );
        _tempImageSource = CreateTable( "TempProductImage" );
        if (ProductID == "0")
        {
            uxSecondaryImageHidden.Value = ProductImageData.SecondaryImagePath;
            foreach (ImageItem item in ProductImageData.GetAllItems())
            {
                CreateRow(
                    _tempImageSource, item.ProductImageID, 0,
                    item.RegularImage, item.LargeImage, item.ThumbnailImage,
                    item.ImageSize, item.SortOrder, item.IsZoom, item.IsEnlarge,
                    item.ImageWidth, item.ImageHeight, item.Locales[CurrentCulture].AltTag, item.Locales[CurrentCulture].TitleTag );
            }
        }
        else
        {
            Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, ProductID, new StoreRetriever().GetCurrentStoreID() );
            uxSecondaryImageHidden.Value = product.ImageSecondary;
            foreach (ProductImage productImage in product.ProductImages)
            {
                ProductImageLocale locale = productImage.Locales[CurrentCulture];
                using (ProductImageFile imageFile =
                    ProductImageFile.Load( new FileManager(), Path.GetFileName( productImage.LargeImage ) ))
                {
                    CreateRow( _tempImageSource, productImage.ProductImageID,
                       ConvertUtilities.ToInt32( product.ProductID ),
                        productImage.RegularImage,
                        productImage.LargeImage,
                        productImage.ThumbnailImage,
                        ProductImageFile.GetImageSize( new FileManager(), productImage.LargeImage ),
                        productImage.SortOrder,
                        productImage.IsZoom,
                        productImage.IsEnlarge,
                        imageFile.LargeImageWidth,
                        imageFile.LargeImageHeight,
                        locale.AltTag,
                        locale.TitleTag );
                }
            }
        }
    }

    private void CreateRow(
        DataTable table, string imageID, int productID, string regularImage, string largeImage,
        string thumbnailImage, int imageSize, int SortOrder, bool isZoom, bool isEnlarge,
        int imageWidth, int imageHeight, string altTag, string titleTag )
    {
        DataRow row;
        row = table.NewRow();
        row["ProductImageID"] = imageID;
        row["ProductID"] = productID;
        row["RegularImage"] = regularImage;
        row["LargeImage"] = largeImage;
        row["ThumbnailImage"] = thumbnailImage;
        row["imageSize"] = imageSize;
        row["SortOrder"] = SortOrder;
        row["IsZoom"] = isZoom;
        row["IsEnlarge"] = isEnlarge;
        row["ImageWidth"] = imageWidth;
        row["ImageHeight"] = imageHeight;
        row["AltTag"] = altTag;
        row["TitleTag"] = titleTag;
        table.Rows.Add( row );
    }

    private void SortDataBySortOrder()
    {
        DataRow[] dataRows = _tempImageSource.Select( "", "SortOrder" );
        for (int i = 0; i < dataRows.Length; i++)
        {
            CreateRow(
                _imageSource,
                dataRows[i]["ProductImageID"].ToString(), ConvertUtilities.ToInt32( dataRows[i]["ProductID"] ),
                dataRows[i]["RegularImage"].ToString(), dataRows[i]["LargeImage"].ToString(),
                dataRows[i]["ThumbnailImage"].ToString(), ConvertUtilities.ToInt32( dataRows[i]["ImageSize"] ),
                ConvertUtilities.ToInt32( dataRows[i]["SortOrder"] ),
                ConvertUtilities.ToBoolean( dataRows[i]["IsZoom"] ),
                ConvertUtilities.ToBoolean( dataRows[i]["IsEnlarge"] ),
                ConvertUtilities.ToInt32( dataRows[i]["ImageWidth"] ),
                ConvertUtilities.ToInt32( dataRows[i]["ImageHeight"] ),
                dataRows[i]["AltTag"].ToString(),
                dataRows[i]["TitleTag"].ToString()
                );
        }
    }

    private void SaveSecondaryNoProduct( ProductImageFile imageFile )
    {
        imageFile.SaveSecondary();

        if (!String.IsNullOrEmpty( ProductImageData.SecondaryImagePath )
            && String.Compare( imageFile.SecondaryFilePath, ProductImageData.SecondaryImagePath, true ) != 0)
        {
            DeleteFile( ProductImageData.SecondaryImagePath );
        }

        ProductImageData.SecondaryImagePath = imageFile.SecondaryFilePath;
        uxSecondaryImageHidden.Value = ProductImageData.SecondaryImagePath;
    }

    private void SaveSecondary( Product product, ProductImageFile imageFile )
    {
        imageFile.SaveSecondary();

        string secondaryImage = product.ImageSecondary;

        if (!String.IsNullOrEmpty( secondaryImage )
            && String.Compare( imageFile.SecondaryFilePath, secondaryImage, true ) != 0)
        {
            DeleteFile( secondaryImage );
        }
        product.ImageSecondary = imageFile.SecondaryFilePath;
        uxSecondaryImageHidden.Value = product.ImageSecondary;
    }

    private bool IsZoomable( ProductImageFile imageFile )
    {
        return IsZoomableSize( imageFile.LargeImageWidth, imageFile.LargeImageHeight );
    }

    private void DisplayUploadMessage( ProductImageFile imageFile )
    {
        if (IsZoomable( imageFile ))
            uxMessage.DisplayMessage( Resources.ProductImageMessages.UploadSuccess );
        else
            uxMessage.DisplayMessage( Resources.ProductImageMessages.UploadSuccessNoZoom );
    }

    private int TotalImageSize()
    {
        int total = 0;
        for (int i = 0; i < _imageSource.Rows.Count; i++)
        {
            total += ConvertUtilities.ToInt32( _imageSource.Rows[i]["ImageSize"] );
        }
        return total;
    }

    private bool VerifyInput( out string message )
    {
        int zeroCount = 0;
        foreach (DataListItem item in uxThumbnailDataList.Items)
        {
            int sortOrder = ConvertUtilities.ToInt32( ((TextBox) item.FindControl( "uxSortOrderText" )).Text );
            if (sortOrder == 0)
                zeroCount++;

            if (sortOrder < 0 ||
                zeroCount > 1)
            {
                message = Resources.ProductImageMessages.UpdateErrorSortOrder;
                return false;
            }
        }

        message = String.Empty;
        return true;
    }

    private bool GetZoomValue( GridViewRow row )
    {
        CheckBox checkBox = (CheckBox) row.Cells[ZoomCheckBoxIndex].FindControl( "uxZoomCheck" );
        if (checkBox.Enabled)
            return checkBox.Checked;
        else
            return false;
    }

    private void DeleteFile( string filePath )
    {
        string localPath = Server.MapPath( "~/" + filePath );
        System.IO.FileInfo deleteFile = new System.IO.FileInfo( localPath );
        if (deleteFile.Exists)
            deleteFile.Delete();
    }

    private void DeleteImage( string regularImage, string largeImage, string thumbnailImage )
    {
        DeleteFile( regularImage );
        DeleteFile( largeImage );
        DeleteFile( thumbnailImage );
    }

    private void SetNewPrimaryImageAfterDelete( string imageID )
    {
        ProductImageData.SetPrimary( imageID );
        ImageItem item1 = ProductImageData.FindItem( imageID );
        string originalFilePath = item1.LargeImage;
        using (ProductImageFile imageFile = ProductImageFile.Load( new FileManager(), Path.GetFileName( originalFilePath ) ))
        {
            SaveSecondaryNoProduct( imageFile );
        }
    }

    private void RemoveNewProductImage( string imageID )
    {
        ImageItem item = ProductImageData.FindItem( imageID );

        if (item.SortOrder == 0)
        {
            DeleteFile( ProductImageData.SecondaryImagePath );
            ProductImageData.SecondaryImagePath = "";

            if (uxGridProductImage.Rows.Count > 1)
            {
                string[] gridImageIDArray = new string[uxGridProductImage.Rows.Count - 1];

                for (int i = 1; i < uxGridProductImage.Rows.Count; i++)
                {
                    gridImageIDArray[i - 1] = ((HiddenField) uxGridProductImage.Rows[i].FindControl( "uxImageIDHidden" )).Value;
                }
                Array.Sort( gridImageIDArray );
                SetNewPrimaryImageAfterDelete( gridImageIDArray[0] );
            }
        }

        DeleteImage( item.RegularImage, item.LargeImage, item.ThumbnailImage );
        ProductImageData.RemoveItem( imageID );
    }

    private void RemoveExistingProductImage( Product product, string imageID )
    {
        bool deletePrimary = false;
        ProductImage productImageToDelete = new ProductImage();
        foreach (ProductImage productimage in product.ProductImages)
        {
            if (productimage.ProductImageID == imageID)
            {
                DeleteImage( productimage.RegularImage, productimage.LargeImage, productimage.ThumbnailImage );
                if (productimage.SortOrder == 0)
                {
                    DeleteFile( product.ImageSecondary );
                    deletePrimary = true;
                }
                productImageToDelete = productimage;
            }
        }

        product.ProductImages.Remove( productImageToDelete );

        if (deletePrimary)
        {
            foreach (ProductImage productimage in product.ProductImages)
            {
                productimage.SortOrder = productimage.SortOrder - 1;
                if (productimage.SortOrder == 0)
                {
                    using (ProductImageFile imageFile = ProductImageFile.Load( new FileManager(), Path.GetFileName( productimage.LargeImage ) ))
                    {
                        SaveSecondary( product, imageFile );
                    }
                }
            }
        }
    }

    private void SetProductImageOrder( Product product, string productImageID, int sortOrder )
    {
        foreach (ProductImage productImage in product.ProductImages)
        {
            if (productImage.ProductImageID == productImageID)
                productImage.SortOrder = sortOrder;
        }
    }

    private void UpdateProductImageFromGrid( Product product, string productImageID, bool isZoom, bool isEnlarge, Culture culture, string altTag, string titleTag )
    {
        foreach (ProductImage productImage in product.ProductImages)
        {
            if (productImage.ProductImageID == productImageID)
            {
                productImage.IsZoom = isZoom;
                productImage.IsEnlarge = isEnlarge;
                productImage.Locales[culture].AltTag = altTag;
                productImage.Locales[culture].TitleTag = titleTag;
            }
        }
    }


    private void UpdateImage()
    {
        string message;
        if (!VerifyInput( out message ))
        {
            uxMessage.DisplayError( message );
            return;
        }

        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        foreach (GridViewRow row in uxGridProductImage.Rows)
        {
            ProductImage productImage = new ProductImage();
            string id = ((HiddenField) row.Cells[ImageIDIndex].FindControl( "uxImageIDHidden" )).Value.Trim();
            bool isZoom = GetZoomValue( row );
            bool isEnlarge = ((CheckBox) row.Cells[EnlargeCheckBoxIndex].FindControl( "uxEnlargeCheck" )).Checked;
            string altTag = ((TextBox) row.Cells[AltTagIndex].FindControl( "uxAltTag" )).Text;
            string titleTag = ((TextBox) row.Cells[TitleTagIndex].FindControl( "uxTitleTag" )).Text;
            if (ProductID == "0")
            {
                ProductImageData.UpdateItem( id, isZoom, isEnlarge, CurrentCulture, altTag, titleTag );
            }
            else
            {
                UpdateProductImageFromGrid( product, id, isZoom, isEnlarge, CurrentCulture, altTag, titleTag );
            }
        }

        foreach (DataListItem item in uxThumbnailDataList.Items)
        {
            string id = ((HiddenField) item.FindControl( "uxImageIDHidden" )).Value.Trim();
            int SortOrder = ConvertUtilities.ToInt32( ((TextBox) item.FindControl( "uxSortOrderText" )).Text );
            if (ProductID == "0")
                ProductImageData.UpdateSortOrder( id, SortOrder );
            else
                SetProductImageOrder( product, id, SortOrder );
        }

    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxUpload.ProductID = ProductID;
        uxUpload.GridViewControlID = uxGridProductImage.ClientID;
        uxUpload.MessageControlID = uxMessage.MessageControlID;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( uxUpload.ErrorFileList ))
            uxMessage.DisplayError( uxUpload.ErrorFileList );

        uxUpload.Visible = true;
        PopulateControls();
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
    }

    protected string GetImageName( string largeImage )
    {
        return largeImage.Substring( largeImage.LastIndexOf( "/" ) + 1 );
    }

    protected bool IsPrimaryImage( object SortOrder )
    {
        if (ConvertUtilities.ToInt32( SortOrder ) == 0)
            return true;
        else
            return false;
    }

    protected bool IsSorting()
    {
        if (ProductID == "0")
            return false;
        else
            return true;
    }

    protected bool IsZoomableSize( object largeImageWidth, object largeImageHeight )
    {
        return (ConvertUtilities.ToInt32( largeImageWidth ) > SystemConst.ProductMagnifierSize) &&
            (ConvertUtilities.ToInt32( largeImageHeight ) > SystemConst.ProductMagnifierSize);
    }

    protected string ImageUrl( string imageName )
    {
        imageName = "~/" + imageName;
        return imageName;
    }

    protected void PopulateThumbnail()
    {
        uxThumbnailDataList.DataSource = _imageSource;
        uxThumbnailDataList.DataBind();
    }

    protected void uxRemoveImageButton_PreRender( object sender, EventArgs e )
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            LinkButton button = (LinkButton) sender;
            button.Attributes.Add(
                "onclick",
                "if (confirm('" + Resources.ProductImageMessages.DeleteConfirmation + "')) {} else {return false}" );
        }
    }

    protected void uxRemoveImageButton_Click( object sender, EventArgs e )
    {
        LinkButton myButton = (LinkButton) sender;
        string imageID = myButton.CommandArgument;

        if (ProductID == "0")
        {
            RemoveNewProductImage( imageID );
        }
        else
        {
            string storeID = new StoreRetriever().GetCurrentStoreID();
            Product product = DataAccessContext.ProductRepository.GetOne( Culture.Null, ProductID, storeID );
            RemoveExistingProductImage( product, imageID );
            DataAccessContext.ProductRepository.Save( product );
        }
    }

    protected void uxSetAsPrimayImageButton_Click( object sender, EventArgs e )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        LinkButton myButton = (LinkButton) sender;
        string imageID = myButton.CommandArgument;
        Product product = DataAccessContext.ProductRepository.GetOne( Culture.Null, ProductID, storeID );

        string originalFilePath;
        if (ProductID == "0")
        {
            ProductImageData.SetPrimary( imageID );
            ImageItem item = ProductImageData.FindItem( imageID );
            originalFilePath = item.LargeImage;
        }
        else
        {
            ProductImage oldPrimaryImage = new ProductImage();
            ProductImage newPrimaryImage = new ProductImage();
            foreach (ProductImage productImage in product.ProductImages)
            {
                if (productImage.ProductImageID == imageID)
                    newPrimaryImage = productImage;
                if (productImage.SortOrder == 0)
                    oldPrimaryImage = productImage;
            }

            SetProductImageOrder( product, oldPrimaryImage.ProductImageID, newPrimaryImage.SortOrder );
            SetProductImageOrder( product, newPrimaryImage.ProductImageID, 0 );

            originalFilePath = newPrimaryImage.LargeImage;
        }

        using (ProductImageFile imageFile = ProductImageFile.Load( new FileManager(), Path.GetFileName( originalFilePath ) ))
        {
            if (ProductID == "0")
                SaveSecondaryNoProduct( imageFile );
            else
                SaveSecondary( product, imageFile );
        }

        if (!product.IsNull)
            DataAccessContext.ProductRepository.Save( product );

        uxMessage.DisplayMessage( Resources.ProductImageMessages.SetPrimarySuccess );

        uxStatusHidden.Value = "SetPrimary";
    }

    #endregion

    #region Public Methods

    public void PopulateControls()
    {
        RefreshGrid();

        if (uxGridProductImage.Rows.Count > 0)
        {
            uxPrimarImage.Visible = true;
            lcPrimaryImageMessageLabel.Visible = true;
        }
        else
        {
            uxPrimarImage.Visible = false;
            lcPrimaryImageMessageLabel.Visible = false;
        }
    }

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["Culture"] == null)
                return null;
            else
                return (Culture) ViewState["Culture"];
        }
        set
        {
            ViewState["Culture"] = value;
        }
    }

    public void SetFooter( Object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells[2].Text = "Total";
            cells[2].HorizontalAlign = HorizontalAlign.Center;
            cells[2].Font.Bold = true;
            cells[3].Text = String.Format( "{0:0,0}", (TotalImageSize()) ).ToString() + " KB.";
            cells[3].HorizontalAlign = HorizontalAlign.Right;
            cells[3].Font.Bold = true;
        }
    }

    public string ProductID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ProductID"] ))
                return "0";
            else
                return MainContext.QueryString["ProductID"];
        }
    }

    public void Update()
    {
        UpdateImage();
    }

    public string SecondaryImage()
    {
        return uxSecondaryImageHidden.Value;
    }

    public void HideUploadImageButton()
    {
        uxProductImageUploadPanel.Style["display"] = "none";
    }

    public Product Update( Product product )
    {
        string message;
        if (!VerifyInput( out message ))
        {
            uxMessage.DisplayError( message );
            return null;
        }

        foreach (GridViewRow row in uxGridProductImage.Rows)
        {
            ProductImage productImage = new ProductImage();
            string id = ((HiddenField) row.Cells[ImageIDIndex].FindControl( "uxImageIDHidden" )).Value.Trim();
            bool isZoom = GetZoomValue( row );
            bool isEnlarge = ((CheckBox) row.Cells[EnlargeCheckBoxIndex].FindControl( "uxEnlargeCheck" )).Checked;
            string altTag = ((TextBox) row.Cells[AltTagIndex].FindControl( "uxAltTag" )).Text;
            string titleTag = ((TextBox) row.Cells[TitleTagIndex].FindControl( "uxTitleTag" )).Text;
            if (ProductID == "0")
            {
                ProductImageData.UpdateItem( id, isZoom, isEnlarge, CurrentCulture, altTag, titleTag );
            }
            else
            {
                UpdateProductImageFromGrid( product, id, isZoom, isEnlarge, CurrentCulture, altTag, titleTag );
            }
        }

        foreach (DataListItem item in uxThumbnailDataList.Items)
        {
            string id = ((HiddenField) item.FindControl( "uxImageIDHidden" )).Value.Trim();
            int SortOrder = ConvertUtilities.ToInt32( ((TextBox) item.FindControl( "uxSortOrderText" )).Text );
            if (ProductID == "0")
                ProductImageData.UpdateSortOrder( id, SortOrder );
            else
                SetProductImageOrder( product, id, SortOrder );
        }
        return product;
    }

    #endregion
}
