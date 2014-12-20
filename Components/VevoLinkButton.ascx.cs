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


public partial class Components_VevoLinkButton : Vevo.WebUI.International.BaseLanguageUserControl
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
        if (UsesFullPostback)
            ScriptManager.GetCurrent( Page ).RegisterPostBackControl( uxLinkButton );

        uxImage.ImageUrl = GetThemeImageUrl( ThemeImageUrl );

        uxLinkButton.Command += new CommandEventHandler( uxLinkButton_Command );
        uxLinkButton.Click += new EventHandler( uxLinkButton_Click );
    }

    protected void uxLinkButton_Click( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    protected void uxLinkButton_Command( object sender, CommandEventArgs e )
    {
        OnCommandBubbleEvent( e );
    }

    public string CommandArgument
    {
        get { return uxLinkButton.CommandArgument; }
        set { uxLinkButton.CommandArgument = value; }
    }

    public string CommandName
    {
        get { return uxLinkButton.CommandName; }
        set { uxLinkButton.CommandName = value; }
    }


    public string CssClassImage
    {
        get { return uxImage.CssClass; }
        set { uxImage.CssClass = value; }
    }

    public string CssClass
    {
        get { return uxLinkButton.CssClass; }
        set { uxLinkButton.CssClass = value; }
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

    public string ValidationGroup
    {
        get { return uxLinkButton.ValidationGroup; }
        set { uxLinkButton.ValidationGroup = value; }
    }

    public bool UsesFullPostback
    {
        get
        {
            if (ViewState["UsesFullPostback"] != null)
                return (bool) ViewState["UsesFullPostback"];
            else
                return false;
        }
        set
        {
            ViewState["UsesFullPostback"] = value;
        }
    }
}
