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
public partial class AdminAdvanced_Components_Common_HeaderTitle : System.Web.UI.UserControl
{
    public string Text
    {
        set { uxTitleLabel.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
