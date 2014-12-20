using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using System.IO;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Mobile_Components_MobileImageSwipe : Vevo.WebUI.International.BaseLanguageUserControl
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

    private void RegisterImageSwipeScript()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine();
        sb.Append( "(function(window, PhotoSwipe){" );
        sb.Append( "	document.addEventListener('DOMContentLoaded', function(){" );
        sb.Append( "		var" );
        sb.Append( "			options = {}," );
        sb.Append( "			instance = PhotoSwipe.attach( window.document.querySelectorAll('#Gallery a'), options );" );
        sb.Append( "		}, false);" );
        sb.Append( "	}(window, window.Code.PhotoSwipe));" );
        sb.AppendLine();

        ScriptManager.RegisterStartupScript( this, this.GetType(), "ImageSwipeScript3", sb.ToString(), true );
    }
    private void SetUpImageSwipe()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        SetUpJavaScript( "ImageSwipeScript", "~/Mobile/ClientScripts/ImageSwipe/klass.min.js" );
        SetUpJavaScript( "ImageSwipeScript2", "~/Mobile/ClientScripts/ImageSwipe/code.photoswipe-3.0.5.min.js" );

        AddCssFile( "~/Mobile/ClientScripts/ImageSwipe/photoswipe.css" );

        RegisterImageSwipeScript();

        IList<ProductImage> imageList = product.ProductImages;
        bool checkFirst = true;

        HtmlGenericControl ul = new HtmlGenericControl( "ul" );
        ul.Attributes.Add( "id", "Gallery" );
        ul.Attributes.Add( "class", "gallery" );
        if (ImageUrl == null)
        {
            foreach (ProductImage image in imageList)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink HyLink = new HyperLink();
                HyLink.NavigateUrl = "~/" + image.LargeImage;

                if (checkFirst)
                {
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    img.ImageUrl = "~/" + image.RegularImage;

                    if (MaximumWidth != null)
                    {
                        Unit max = new Unit(MaximumWidth);

                        if (max.Type == UnitType.Pixel &&
                            File.Exists(Server.MapPath(img.ImageUrl)))
                        {
                            using (Bitmap mypic = new Bitmap(Server.MapPath(img.ImageUrl)))
                            {
                                if (mypic.Width > max.Value)
                                    img.Width = max;
                            }
                        }
                    }

                    HyLink.Controls.Add(img);
                    li.Controls.Add(HyLink);

                    checkFirst = false;
                }
                else
                {
                    li.Style.Add("visibility", "hidden");
                    li.Controls.Add(HyLink);
                }


                ul.Controls.Add(li);
            }
        }
        else
        {
            //for (int i = 0; i <= 1;i++ ){
                foreach (ProductImage image in imageList)
                {
               // ProductImage image = imageList[i];
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HyperLink HyLink = new HyperLink();
                    HyLink.NavigateUrl = "~/" + image.LargeImage;


                    if (checkFirst)
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ImageUrl = ImageUrl;

                        if (MaximumWidth != null)
                        {
                            Unit max = new Unit(MaximumWidth);

                            if (max.Type == UnitType.Pixel &&
                                File.Exists(Server.MapPath(img.ImageUrl)))
                            {
                                using (Bitmap mypic = new Bitmap(Server.MapPath(img.ImageUrl)))
                                {
                                    if (mypic.Width > max.Value)
                                        img.Width = max;
                                }
                            }
                        }

                        HyLink.Controls.Add(img);
                        li.Controls.Add(HyLink);

                        checkFirst = false;
                    }
                    else
                    {
                        li.Style.Add("visibility", "hidden");
                        li.Controls.Add(HyLink);
                    }
                    ul.Controls.Add(li);
                }
        }

        uxImagePlaceHolder.Controls.Add( ul );
    }
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpImageSwipe();
    }

    public string ImageUrl
    {
        get
        {
            return (string)ViewState["ImageUrl"];
        }
        set
        {
            ViewState["ImageUrl"] = value;
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

    public string MaximumWidth
    {
        get
        {
            if (ViewState["MaximumWidth"] == null)
                ViewState["MaximumWidth"] = "100px";
            return (string) ViewState["MaximumWidth"];
        }
        set
        {
            ViewState["MaximumWidth"] = value;
        }
    }
}