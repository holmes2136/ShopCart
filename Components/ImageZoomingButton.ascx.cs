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


public partial class Components_ImageZoomingButton : Vevo.WebUI.International.BaseLanguageUserControl
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

    private void SetUpThickBox( Product product )
    {
        SetUpJavaScript( "ThickBoxScript", "~/ClientScripts/ThickBox/thickbox.js" );

        AddCssFile( "~/ClientScripts/ThickBox/ThickBox.css" );

        // Fix for Ajax asynchronous postback
        if (IsPostBack)
        {
            ScriptManager.RegisterStartupScript(
                this, this.GetType(), "ThickboxInit",
                "tb_init('a.thickbox, area.thickbox, input.thickbox');",
                true );
        }

        IList<ProductImage> imageList = product.ProductImages;
        IList<String> imageUrl = new List<String>();
        foreach (ProductImage image in imageList)
        {
            HyperLink DynLink = new HyperLink();
            DynLink.NavigateUrl = "~/" + image.LargeImage;
            DynLink.ToolTip = product.Name;
            DynLink.CssClass = "thickbox";
            DynLink.Attributes.Add( "rel", "gallery" + product.ProductID );
            if (!image.LargeImage.Equals( ImageDetails.LargeImage ))
                DynLink.Style.Add( "visibility", "hidden" );
            else
            {
                DynLink.Visible = true;

                Image HtmlImage = new Image();
                HtmlImage.ImageUrl = "~/Images/Design/Icon/Popup.gif";
                HtmlImage.CssClass = "ImageZoomingButtonPopupImage";
                DynLink.Controls.Add( HtmlImage );

                Label Htmllabel = new Label();
                Htmllabel.CssClass = "ImageZoomingButtonPopupMessage";
                Htmllabel.Text = "[$Popup]";
                DynLink.Controls.Add( Htmllabel );
            }

            uxPopupPlaceHolder.Controls.Add( DynLink );
        }
    }

    private void SetUpZoomCss()
    {
        AddCssFile( "~/ClientScripts/ImageZoom/zoom2.css" );
        if (Request.Browser.Browser == "IE")
        {
            AddCssFile( "~/ClientScripts/ImageZoom/zoom2IE.css" );
        }
    }

    private void RegisterZoomScript()
    {
        StringBuilder sb = new StringBuilder();

        if (!uxZoomLink.Visible)
        {
            sb.AppendLine();
            sb.Append( "function pageLoad(sender, args) { } " );
        }
        else
        {
            sb.AppendLine();
            sb.Append( "function pageLoad(sender, args) { " );
            sb.Append( "HotSpotController.init(\"" + TargetImageClientID + "\", " );
            sb.Append( SystemConst.ProductMagnifierSize + ", " );
            sb.Append( "\"" + ImageDetails.LargeImage + "\", '" + uxZoomLink.ClientID + "'); " );
            sb.Append( "}" );
            sb.AppendLine();
        }

        sb.Append( "function addEvent(obj, evType, fn) {" );
        sb.Append( "if (obj.addEventListener) {" );
        sb.Append( "    obj.addEventListener(evType, fn, false);" );
        sb.Append( "    return true;" );
        sb.Append( "    } else if (obj.attachEvent) {" );
        sb.Append( "        var r = obj.attachEvent(\"on\" + evType, fn);" );
        sb.Append( "        return r;" );
        sb.Append( "    } else {" );
        sb.Append( "            return false;" );
        sb.Append( "    }" );
        sb.Append( "}" );
        sb.AppendLine();

        ScriptManager.RegisterStartupScript( this, this.GetType(), "ZoomScript", sb.ToString(), true );
    }

    private void SetUpZoomButton()
    {
        SetUpJavaScript( "HotSpotkScript", "~/ClientScripts/ImageZoom/HotSpot2.js" );
        SetUpJavaScript( "DragScript", "~/ClientScripts/ImageZoom/dom-drag.js" );

        SetUpZoomCss();

        uxZoomLink.NavigateUrl = "javascript: void(0);";

        RegisterZoomScript();
    }

    private void SetUpButtonVisibility()
    {
        if ( !IsEnabledZoomImage )
        {
            uxZoomLink.Visible = false;
            uxZoomLabel.Visible = false;
        }
        else
        {
            uxZoomLink.Visible = ImageDetails.IsZoom;
            uxZoomLabel.Visible = ImageDetails.IsZoom;
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );
        ImageDetails = product.GetProductImage( ProductImageID );

        SetUpButtonVisibility();
        SetUpThickBox( product );
        SetUpZoomButton();
    }

    public bool IsEnabledZoomImage
    {
        get
        {
            if (ViewState["IsEnabledZoomImage"] == null)
                ViewState["IsEnabledZoomImage"] = true;
            return (bool) ViewState["IsEnabledZoomImage"];
        }
        set
        {
            ViewState["IsEnabledZoomImage"] = value;
        }
    }

    public ProductImage ImageDetails
    {
        get
        {
            if (ViewState["ImageDetails"] == null)
                ViewState["ImageDetails"] = new ProductImage();
            return (ProductImage) ViewState["ImageDetails"];
        }
        set
        {
            ViewState["ImageDetails"] = value;
        }
    }

    public string ProductID
    {
        get
        {
            if (ViewState["ProductID"] == null)
                ViewState["ProductID"] = "0";

            return ViewState["ProductID"].ToString();
        }
        set
        {
            ViewState["ProductID"] = value;
        }
    }

    public string ProductImageID
    {
        get
        {
            if (ViewState["ProductImageID"] == null)
                ViewState["ProductImageID"] = "0";
            return ViewState["ProductImageID"].ToString();
        }
        set
        {
            ViewState["ProductImageID"] = value;
        }
    }

    public string TargetImageClientID
    {
        get
        {
            if (ViewState["TargetImageClientID"] == null)
                ViewState["TargetImageClientID"] = String.Empty;
            return ViewState["TargetImageClientID"].ToString();
        }
        set
        {
            ViewState["TargetImageClientID"] = value;
        }
    }

}
