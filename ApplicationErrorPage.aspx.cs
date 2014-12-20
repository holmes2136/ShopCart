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

public partial class ApplicationErrorPage : System.Web.UI.Page
{
    private void PopulateControls()
    {
        if (!IsPostBack)
        {
            if (ApplicationError.Header == null ||
                ApplicationError.Message == null)
                Response.Redirect( "Default.aspx" );

            uxHeaderLiteral.Text = ApplicationError.Header;
            uxMessageLiteral.Text = WebUtilities.ReplaceNewLine( ApplicationError.Message );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }
}
