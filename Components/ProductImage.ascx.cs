using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_ProductImage : BaseLanguageUserControl
{
    private void SetUpJavaScript( string scriptName, string scriptUrl )
    {
        ScriptManager.RegisterClientScriptInclude(
            this, this.GetType(), scriptName, ResolveClientUrl( scriptUrl ) );
    }

    private void AddCssFile( string cssFilePath )
    {
        HtmlLink cssLink = new HtmlLink();
        cssLink.Href = cssFilePath;
        cssLink.Attributes.Add( "rel", "stylesheet" );
        cssLink.Attributes.Add( "type", "text/css" );
        Page.Header.Controls.Add( cssLink );
    }

    private void RegisterZoomScript( HyperLink hyLink )
    {
        StringBuilder sb = new StringBuilder();
        string zoomID = "easy_zoom";
        if ( this.ClientID.ToLower().Contains( "quickview" ) )
        {
            zoomID = "easy_zoom1";
        }

        sb.Append( "jQuery(function($){" );
        sb.Append( "	$('#" + hyLink.ClientID + ".zoom').easyZoom('','" + zoomID + "','" + this.ClientID + "');" );
        sb.Append( "});" );

        sb.AppendLine();

        ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ZoomScript2", sb.ToString(), true );
    }

    private void SetUpZoomImage()
    {
        SetUpJavaScript( "ZoomScript", "~/ClientScripts/easyzoom/easyzoom.js" );

        if (!UrlManager.IsMobileDevice( Request ))
        {
            SetUpJavaScript( "ThickBoxScript", "~/ClientScripts/ThickBox/thickbox.js" );
        }

        ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ThickboxInit", "tb_init('a.zoom, area.zoom, input.zoom');", true );

        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        IList<ProductImage> imageList = product.ProductImages;

        if ( product.ProductImages.Count != 0 )
        {
            if ( ImageUrl == null )
            {
                bool checkFirst = true;
                foreach ( ProductImage image in imageList )
                {
                    HyperLink hyLink = new HyperLink();
                    if ( checkFirst )
                    {
                        hyLink.Attributes.Add( "class", "zoom" );

                        HtmlGenericControl div = new HtmlGenericControl( "div" );
                        div.ID = "len";
                        div.Attributes.Add( "class", "zoom_len" );
                        hyLink.Controls.Add( div );
                    }
                    else
                    {
                        hyLink.Style.Add( "display", "none" );
                    }
                    hyLink.Attributes.Add( "rel", "gallery" + product.ProductID );
                    if (!UrlManager.IsMobileDevice( Request ))
                    {
                        hyLink.NavigateUrl = "~/" + image.LargeImage;
                    }
                    if(image.Locales[StoreContext.Culture].TitleTag != null)
                        hyLink.ToolTip = image.Locales[StoreContext.Culture].TitleTag;
                    else
                        hyLink.ToolTip = product.Name;

                    if(image.Locales[StoreContext.Culture].AltTag != null)
                        hyLink.Attributes.Add( "alt", image.Locales[StoreContext.Culture].AltTag);

                    Image HtmlImage = new Image();

                    HtmlImage = SetUpImage( HtmlImage, image.RegularImage );
                    HtmlImage.ImageUrl = "~/" + image.RegularImage;
                    hyLink.Controls.Add( HtmlImage );

                    PlaceHolder1.Controls.Add( hyLink );

                    if (checkFirst)
                    {
                        RegisterZoomScript( hyLink );
                        checkFirst = false;
                    }
                }
            }
            else
            {
                foreach ( ProductImage image in imageList )
                {
                    HyperLink hyLink = new HyperLink();

                    if (image.Locales[StoreContext.Culture].TitleTag != null)
                        hyLink.ToolTip = image.Locales[StoreContext.Culture].TitleTag;
                    else
                        hyLink.ToolTip = product.Name;

                    if (image.Locales[StoreContext.Culture].AltTag != null)
                        hyLink.Attributes.Add( "alt", image.Locales[StoreContext.Culture].AltTag );
                    if (!UrlManager.IsMobileDevice( Request ))
                    {
                        hyLink.NavigateUrl = "~/" + image.LargeImage;
                    }
                    hyLink.Attributes.Add( "rel", "gallery" + product.ProductID );

                    if ( !image.RegularImage.Equals( ImageUrl.Replace( "~/", "" ) ) )
                        hyLink.Style.Add( "display", "none" );
                    else
                    {
                        hyLink.Attributes.Add( "class", "zoom" );
                        HtmlGenericControl div = new HtmlGenericControl( "div" );
                        div.ID = "len";
                        div.Attributes.Add( "class", "zoom_len" );
                        hyLink.Controls.Add( div );
                    }

                    Image HtmlImage = new Image();
                    HtmlImage = SetUpImage( HtmlImage, image.RegularImage );
                    HtmlImage.ImageUrl = "~/" + image.RegularImage;
                    hyLink.Controls.Add( HtmlImage );

                    PlaceHolder1.Controls.Add( hyLink );

                    if (image.RegularImage.Equals( ImageUrl.Replace( "~/", "" ) ))
                        RegisterZoomScript( hyLink );
                }
            }
        }
        else
        {
            ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
            Image HtmlImage = new Image();
            HtmlImage.ImageUrl = ImageUrl;
            PlaceHolder1.Controls.Add( HtmlImage );
        }
    }

    private Image SetUpImage( Image HtmlImage, string img )
    {
        if ( MaximumWidth != null )
        {
            Unit max = new Unit( MaximumWidth );

            if ( max.Type == UnitType.Pixel &&
                System.IO.File.Exists( Server.MapPath( img ) ) )
            {
                using ( System.Drawing.Bitmap mypic = new System.Drawing.Bitmap( Server.MapPath( img ) ) )
                {
                    if (mypic.Width > max.Value)
                        HtmlImage.Width = max;
                }
            }
        }

        return HtmlImage;
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        AddCssFile( "~/ClientScripts/easyzoom/default.css" );
        AddCssFile( "~/ClientScripts/ThickBox/ThickBox.css" );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpZoomImage();
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

    public string ProductID
    {
        get
        {
            if ( ViewState[ "ProductID" ] == null )
                ViewState[ "ProductID" ] = "0";

            return ViewState[ "ProductID" ].ToString();
        }
        set
        {
            ViewState[ "ProductID" ] = value;
        }
    }
}
