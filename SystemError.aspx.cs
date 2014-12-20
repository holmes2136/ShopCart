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
using Vevo.WebAppLib;

public partial class SystemError : System.Web.UI.Page
{
    private void PopulateControls()
    {
        if (!IsPostBack)
        {
            if (SystemErrorPage.Header == null ||
                SystemErrorPage.Message == null)
                Response.Redirect( "Default.aspx" );

            uxHeaderLiteral.Text = SystemErrorPage.Header;
            uxMessageLiteral.Text = WebUtilities.ReplaceNewLine( SystemErrorPage.Message );
        }
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }
}
