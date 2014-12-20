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
using Vevo;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;

public partial class AdminAdvance_Login : AdminAdvancedBasePage
{
    private void SetLoginReturnUrl()
    {
        if (string.IsNullOrEmpty( Request.QueryString["ReturnUrl"] ))
        {
            UrlPath urlPath = new UrlPath( Request.Url.AbsoluteUri );
            uxLogin.DestinationPageUrl = "~/" + urlPath.ExtractFirstApplicationSubfolder();
        }
        else
        {
            uxLogin.DestinationPageUrl = Request.QueryString["ReturnUrl"];
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SetLoginReturnUrl();

        WebUtilities.TieLoginControl( this.Page, uxLogin );
        if (!IsPostBack)
        {
            uxPageThemeDrop.SelectedValue = AdminTheme;
        }

        uxPageThemeDrop.Visible = false;
        uxPageThemeLabel.Visible = false;
    }

    protected void uxLogin_Error( object sender, EventArgs e )
    {
        if (Membership.GetUser( uxLogin.UserName ) != null)
        {
            if (Membership.GetUser( uxLogin.UserName ).IsLockedOut == true)
                Membership.GetUser( uxLogin.UserName ).UnlockUser();
        }
    }

    protected void uxPageThemeDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        AdminTheme = uxPageThemeDrop.SelectedValue;
        Response.Redirect( Request.Url.AbsolutePath );
    }

    protected void uxLogin_LoggingIn( object sender, LoginCancelEventArgs e )
    {
    }
}
