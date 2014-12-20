using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Components_CatalogImage : Vevo.WebUI.International.BaseLanguageUserControl
{
    private bool isLargeImage( string imageURL, out Unit max )
    {
        if ( MaximumWidth != null )
        {
            max = new Unit( MaximumWidth );

            if ( max.Type == UnitType.Pixel &&
                File.Exists( Server.MapPath( ImageUrl ) ) )
            {
                using ( Bitmap mypic = new Bitmap( Server.MapPath( ImageUrl ) ) )
                {
                    if ( mypic.Width > max.Value )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            max = 0;
            return false;
        }
    }

    private void SetUpImage()
    {
        if ( ImageUrl != null )
        {
            uxImage.ImageUrl = Page.ResolveUrl( ImageUrl );
            Unit width;

            if ( isLargeImage( Page.ResolveUrl( ImageUrl ), out width ) )
                uxImage.Width = width;

            if ( ImageUrl2 != null )
            {
                Unit width2;

                if ( isLargeImage( Page.ResolveUrl( ImageUrl2 ), out width2 ) )
                    uxImage.Width = width2;

                uxImage.Attributes.Add( "onmouseover", "this.src='" + Page.ResolveUrl( ImageUrl2 ) + "'" );
                uxImage.Attributes.Add( "onmouseout", "this.src='" + Page.ResolveUrl( ImageUrl ) + "'" );
            }
        }
    }

    private void PopulateProductImage()
    {
        IList<ProductImage> productImages = DataAccessContext.ProductRepository.GetProductImages( ProductID );

        switch ( productImages.Count )
        {
            case 0:
                {
                    ImageUrl = Page.ResolveUrl( "Images/Products/DefaultNoImage.gif" );
                }
                break;
            case 1:
                {
                    ImageUrl = Page.ResolveUrl( productImages[ 0 ].RegularImage );
                }
                break;
            default:
                {
                    ImageUrl = Page.ResolveUrl( productImages[ 0 ].RegularImage );
                    ImageUrl2 = Page.ResolveUrl( productImages[ 1 ].RegularImage );
                }
                break;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( String.IsNullOrEmpty( ImageUrl ) ||
            ImageUrl == "~/" )
        {
            ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
            uxImage.ImageUrl = ImageUrl;
        }

        if ( ProductID != null )
        {
            PopulateProductImage();
        }

        SetUpImage();
    }

    public string ProductID
    {
        get
        {
            return ( string ) ViewState[ "ProductID" ];
        }
        set
        {
            ViewState[ "ProductID" ] = value;
        }
    }


    public string ImageUrl
    {
        get
        {
            return ( string ) ViewState[ "ImageUrl" ];
        }
        set
        {
            ViewState[ "ImageUrl" ] = value;
        }
    }

    public string ImageUrl2
    {
        get
        {
            return ( string ) ViewState[ "ImageUrl2" ];
        }
        set
        {
            ViewState[ "ImageUrl2" ] = value;
        }
    }

    public string MaximumWidth
    {
        get
        {
            return ( string ) ViewState[ "MaximumWidth" ];
        }
        set
        {
            ViewState[ "MaximumWidth" ] = value;
        }
    }

    public string AlternateText
    {
        get
        {
            return uxImage.AlternateText;
        }
        set
        {
            uxImage.AlternateText = value;
        }
    }

    public string Title
    {
        get
        {
            return uxImage.ToolTip;
        }
        set
        {
            uxImage.ToolTip = value;
        }
    }
}
