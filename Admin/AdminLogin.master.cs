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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using System.Text;

public partial class AdminAdvanced_AdminLogin : Vevo.WebUI.International.BaseLanguageMasterPage
{
    private string CurrentUrl
    {
        get
        {
            return Request.Url.AbsoluteUri;
        }
    }

    private void SSLVerifyAndRediect()
    {
        UrlPath urlPath = new UrlPath( CurrentUrl );
        if (!urlPath.IsSslEnabled())
        {
            Response.Redirect( urlPath.CreateSslUrl() );
        }
    }

    private void AddIconLink()
    {
        HtmlLink link = new HtmlLink();
        link.Attributes.Add( "rel", "shortcut icon" );
        link.Attributes.Add( "type", "image/vnd.microsoft.icon" );
        link.Href = "~/favicon.ico";
        Page.Header.Controls.Add( link );
    }

    private void RegisterJavaScript()
    {
        AdminLoginBody.Attributes.Add("Onload", "Initialize()");
        AdminLoginBody.Attributes.Add("OnResize", "Initialize()");
        String csname = "SetupLoginBoxPosition";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("function Initialize()");
        sb.AppendLine("{");
        sb.AppendLine("     var myHeight = window.innerHeight;");
        sb.AppendLine("     var footerHeight = document.getElementById('" + AdminFooter.ClientID + "').clientHeight;");
        sb.AppendLine("     var offsetShadowHeight = 20;");
        sb.AppendLine("     var loginBoxHeight = document.getElementById('" + AdminLoginBox.ClientID + "').clientHeight;");
        sb.AppendLine("     var loginMarginTop = myHeight - footerHeight - loginBoxHeight + offsetShadowHeight;");
        sb.AppendLine("     document.getElementById('" + AdminLoginBox.ClientID + "').style.marginTop= (loginMarginTop/2) + 'px';");
        sb.AppendLine("}");

        if (!cs.IsClientScriptBlockRegistered(this.GetType(), csname))
        {
            cs.RegisterClientScriptBlock(this.GetType(), csname, sb.ToString(), true);
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterJavaScript();

        if (!WebConfiguration.AdminSSLDisabled &&
            DataAccessContext.Configurations.GetBoolValueNoThrow( "EnableAdminSSL" ) == true)
        {
            SSLVerifyAndRediect();
        }
        AddIconLink();

        if (KeyUtilities.IsTrialLicense())
            uxTrialWarningPlaceHolder.Visible = true;
        else
            uxTrialWarningPlaceHolder.Visible = false;

    }
}
