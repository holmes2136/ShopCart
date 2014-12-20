using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Products.BackOffice;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class _Components_Upload_Upload : System.Web.UI.Page
{
    public string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.Form["ProductID"] ))
                return "0";
            else
                return Request.Form["ProductID"];
        }
    }

    public string UploadDirectory
    {
        get
        {
            return Request.Form["UploadDirectory"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        String fileNameInfo = String.Empty;
        MemoryStream ms = null;
        bool errorDuplicate = false;

        try
        {
            // Get the data
            HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];
            fileNameInfo = Path.GetFileName( jpeg_image_upload.FileName );
            using (original_image = System.Drawing.Image.FromStream( jpeg_image_upload.InputStream ))
            {
                using (ProductImageFile imageFile = new ProductImageFile( new FileManager(), fileNameInfo, original_image ))
                {
                    if (IsUsedByAnotherProduct( imageFile.LargeFilePath ))
                    {
                        errorDuplicate = true;
                        Response.End();
                    }

                    imageFile.SaveLargeImage();
                    imageFile.SaveRegular();
                    imageFile.SaveThumbnail();

                    if (ProductID == "0")
                    {
                        int sortOrder = ProductImageData.GetNexOrder();

                        ProductImageData.AddImageItem(
                            imageFile.ThumbnailFilePath,
                            imageFile.RegularFilePath,
                            imageFile.LargeFilePath,
                            ProductImageFile.GetImageSize( new FileManager(), imageFile.LargeFilePath ),
                            sortOrder,
                            IsZoomable( imageFile ),
                            true,
                            imageFile.LargeImageWidth,
                            imageFile.LargeImageHeight, 
							StoreContext.Culture,
							"",
							""
                            );

                        if (sortOrder == 0)
                            SaveSecondaryNoProduct( imageFile );
                    }
                    else
                    {
                        string storeID = new StoreRetriever().GetCurrentStoreID();
                        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, storeID );
                        ProductImage productImage = new ProductImage();

                        productImage.RegularImage = imageFile.RegularFilePath;
                        productImage.LargeImage = imageFile.LargeFilePath;
                        productImage.ThumbnailImage = imageFile.ThumbnailFilePath;
                        productImage.IsZoom = IsZoomable( imageFile );
                        productImage.IsEnlarge = true;

                        if (product.ProductImages.Count == 0)
                            SaveSecondary( product, imageFile );

                        productImage.SortOrder = product.ProductImages.Count;

                        product.ProductImages.Add( productImage );

                        DataAccessContext.ProductRepository.Save( product );
                    }
                }
            }
            Response.StatusCode = 200;
            Response.Write( "Success" );
        }
        catch
        {
            // If any kind of error occurs return a 500 Internal Server error

            if (!errorDuplicate)
            {
                Response.StatusCode = 500;
                Response.Write( "An error occured" );
            }
            else
            {
                Response.StatusCode = 601;
                Response.Write( "Upload Error Duplicated" );
            }
            Response.End();
        }
        finally
        {
            // Clean up
            if (final_image != null) final_image.Dispose();
            if (graphic != null) graphic.Dispose();
            if (original_image != null) original_image.Dispose();
            if (thumbnail_image != null) thumbnail_image.Dispose();
            if (ms != null) ms.Close();
            Response.End();
        }
    }

    private bool IsUsedByAnotherProduct( string largeFilePath )
    {
        return DataAccessContext.ProductRepository.ImageIsUsedByAnotherProduct( largeFilePath );
    }

    private bool IsZoomable( ProductImageFile imageFile )
    {
        return IsZoomableSize( imageFile.LargeImageWidth, imageFile.LargeImageHeight );
    }

    protected bool IsZoomableSize( object largeImageWidth, object largeImageHeight )
    {
        return (ConvertUtilities.ToInt32( largeImageWidth ) > SystemConst.ProductMagnifierSize) &&
            (ConvertUtilities.ToInt32( largeImageHeight ) > SystemConst.ProductMagnifierSize);
    }

    private void SaveSecondaryNoProduct( ProductImageFile imageFile )
    {
        imageFile.SaveSecondary();

        if (!String.IsNullOrEmpty( ProductImageData.SecondaryImagePath ) &&
    String.Compare( imageFile.SecondaryFilePath, ProductImageData.SecondaryImagePath, true ) != 0)
        {
            DeleteFile( ProductImageData.SecondaryImagePath );
        }

        ProductImageData.SecondaryImagePath = imageFile.SecondaryFilePath;
    }

    private void SaveSecondary( Product product, ProductImageFile imageFile )
    {
        imageFile.SaveSecondary();

        if (!product.IsNull)
        {
            string secondaryImage = product.ImageSecondary;

            if (!String.IsNullOrEmpty( secondaryImage ) &&
                String.Compare( imageFile.SecondaryFilePath, secondaryImage, true ) != 0)
            {
                DeleteFile( secondaryImage );
            }
            product.ImageSecondary = imageFile.SecondaryFilePath;
        }
    }

    private void DeleteFile( string filePath )
    {
        string localPath = Server.MapPath( "~/" + filePath );
        System.IO.FileInfo deleteFile = new System.IO.FileInfo( localPath );
        if (deleteFile.Exists)
            deleteFile.Delete();
    }
}
