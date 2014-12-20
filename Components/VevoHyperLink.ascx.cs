using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Shared.WebUI;
using Vevo;


public partial class Components_VevoHyperLink : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string GetThemeImageUrl( string imageName )
    {
        if (UrlManager.IsMobile() || UrlManager.IsFacebook())
        {
            return Page.ResolveUrl( imageName );
        }
        else
        {
            return Page.ResolveUrl( "Themes/" +
                DataAccessContext.Configurations.GetValueNoThrow( "StoreTheme" ) + "/" + imageName );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxHyperLink.CssClass = CssClass;

        uxImage.CssClass = CssClassImage;
        uxImage.ImageUrl = GetThemeImageUrl( ThemeImageUrl );
    }

    public string CssClassImage
    {
        get
        {
            if (ViewState["CssClassImage"] != null)
                return (string) ViewState["CssClassImage"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["CssClassImage"] = value;
        }
    }

    public string CssClass
    {
        get
        {
            if (ViewState["CssClass"] != null)
                return (string) ViewState["CssClass"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["CssClass"] = value;
        }
    }

    public string NavigateUrl
    {
        get
        {
            if (ViewState["NavigateUrl"] != null)
                return (string) ViewState["NavigateUrl"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["NavigateUrl"] = value;
            uxHyperLink.NavigateUrl = Page.ResolveUrl( (string) ViewState["NavigateUrl"] );
        }
    }

    public string ThemeImageUrl
    {
        get
        {
            if (ViewState["ThemeImageUrl"] != null)
                return (string) ViewState["ThemeImageUrl"];
            else
                return String.Empty;
        }
        set
        {
            ViewState["ThemeImageUrl"] = value;
        }
    }
}
