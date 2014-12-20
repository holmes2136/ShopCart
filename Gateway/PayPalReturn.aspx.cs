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

public partial class Gateway_PayPalReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string orderID = Request.QueryString["OrderID"];
        if (orderID == null)
            orderID = Request.QueryString["invoice"];

        Response.Redirect( "~/CheckoutComplete.aspx?OrderID=" + orderID + "&IsTransaction=True" );
    }

}
