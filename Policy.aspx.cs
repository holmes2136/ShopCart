using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.WebAppLib;

public partial class Policy : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxContentLayout.ContentName = "Policy";
    }
}
