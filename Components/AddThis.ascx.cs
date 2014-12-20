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

public partial class Components_AddThis : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        
    }

    public string LinkURL
    {
        get { return AddThisWidget.LinkURL; }
        set { AddThisWidget.LinkURL = value; }
    }

    public string Title
    {
        get { return AddThisWidget.Title; }
        set { AddThisWidget.Title = value; }
    }
}
