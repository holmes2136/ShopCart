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


public partial class AdminAdvanced_MainControls_AdminMessage : AdminAdvancedBaseUserControl
{
    private void PopulateControls()
    {
        if (String.IsNullOrEmpty( AdminMessageMainControl.Header ) ||
            String.IsNullOrEmpty( AdminMessageMainControl.Message ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );

        uxContentTemplate.HeaderText = AdminMessageMainControl.Header;
        uxMessageLiteral.Text = AdminMessageMainControl.Message;

        if (!String.IsNullOrEmpty( AdminMessageMainControl.ReturnLink ))
        {
            uxReturnLink.Visible = true;
            uxReturnLink.PageName = AdminMessageMainControl.ReturnLink;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }
}
