using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;

public partial class AdminAdvanced_Components_OrdersInformation : AdminAdvancedBaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<Order> list = DataAccessContext.OrderRepository.GetOrdersByProcessedAndStatus( false, "New" );
        if (list.Count > 0)
        {
            uxNewOrderLabel.Visible = false;
            
            uxNewOrderLink.Visible = true;
            uxNewOrderLink.Text = String.Format( "You have {0} new orders", list.Count );
        }
        else
        {
            uxNewOrderLabel.Visible = true;
            uxNewOrderLabel.Text = String.Format( "There is no new order to process" );

            uxNewOrderLink.Visible = false;
        }
               
        decimal subtotal = 0;
        for (int i = 0; i < list.Count;i++ )
        {
            subtotal += list[i].Total;
        }
        uxNewOrdersAmout.Text = String.Format( 
            "New Order Amount: <strong>{0}</strong>", 
            AdminUtilities.FormatPrice( subtotal ) );
    }
}
