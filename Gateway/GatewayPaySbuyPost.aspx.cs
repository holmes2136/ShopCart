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
using Vevo.Domain;
using Vevo.Domain.Orders;

public partial class GatewayPaySbuyPost : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        string result = Request.Form["result"];
        string orderID;
        orderID = result.Substring( 2, result.Length - 2 ); //invoice ID
        result = result.Substring( 0, 2 );
        string amount = Request.Form["amt"];
        string apCode = Request.Form["apCode"]; //Transaction ID
        if (result == "00") //success
        {
            //do delivery proecss
            //etc
            //etc
            Response.Write( result + "<br>" + orderID );
        }
        else
        {
            Order order = DataAccessContext.OrderRepository.GetOne( orderID );
            order.GatewayOrderID = apCode;
            order.GatewayPaymentStatus = "testja";
            DataAccessContext.OrderRepository.Save( order );
            //01 not enoegh fund
            //99 Unknow Error
            //Show error
        }
    }
}
