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
using Vevo.Domain;


public partial class Components_OrderItemDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _orderID;

    public string CurrentOrderID
    {
        get { return _orderID; }
        set { _orderID = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (_orderID == null)
            return;

        SetGrid();
    }

    private void SetGrid()
    {
        uxItemDetailGrid.DataSource = DataAccessContext.OrderItemRepository.GetByOrderID( _orderID );
        uxItemDetailGrid.DataBind();
    }
}
