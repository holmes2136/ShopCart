using System;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_ImageLogo : BaseLanguageUserControl
{
    private void SetUpImage()
    {
        try
        {
            if (ImageUrl != null)
            {
                uxImage.ImageUrl = Page.ResolveUrl( ImageUrl );
                if (uxImage.ImageUrl.Contains( "/Blog/" ))
                {
                    uxImage.ImageUrl = uxImage.ImageUrl.Replace( "/Blog/", "/" );
                }

                if (MaximumWidth != null)
                {
                    Unit max = new Unit( MaximumWidth );

                    if (max.Type == UnitType.Pixel &&
                        File.Exists( Server.MapPath( ImageUrl ) ))
                    {
                        Bitmap mypic = new Bitmap( Server.MapPath( ImageUrl ) );

                        if (mypic.Width > max.Value)
                            uxImage.Width = max;

                        mypic.Dispose();
                    }
                }

                if (MaximumHight != null)
                {
                    Unit max = new Unit( MaximumHight );

                    if (max.Type == UnitType.Pixel &&
                        File.Exists( Server.MapPath( ImageUrl ) ))
                    {
                        Bitmap mypic = new Bitmap( Server.MapPath( ImageUrl ) );

                        if (mypic.Height > max.Value)
                            uxImage.Height = max;

                        mypic.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SaveLogFile.SaveLog( ex );
        }
    }


    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( ImageUrl ) &&
            ImageUrl != "~/")
        {
            uxImage.Visible = true;
            SetUpImage();
        }
        else
        {
            uxImage.ImageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
            uxImage.Visible = false;
        }
    }


    public string ImageUrl
    {
        get
        {
            string imagePath = DataAccessContext.Configurations.GetValue( "LogoImage", StoreContext.CurrentStore );
            if (imagePath != string.Empty)
                return imagePath;
            else
                return string.Empty;
        }
    }

    public string MaximumWidth
    {
        get
        {
            return (string) ViewState["MaximumWidth"];
        }
        set
        {
            ViewState["MaximumWidth"] = value;
        }
    }
    public string MaximumHight
    {
        get
        {
            return (string) ViewState["MaximumHight"];
        }
        set
        {
            ViewState["MaximumHight"] = value;
        }
    }
}
