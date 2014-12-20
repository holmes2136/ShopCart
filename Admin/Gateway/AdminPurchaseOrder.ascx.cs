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
using Vevo.DataAccessLib;
using Vevo.WebAppLib;
using Vevo.Domain.Payments;
using Vevo.Domain.Stores;
using Vevo.Domain;
using System.Collections.Generic;

public partial class Admin_Gateway_AdminPurchaseOrder : Vevo.AdminAdvancedBaseGatewayUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            Refresh();
        }
    }

    public override void Refresh()
    {

    }

    public override void Save()
    {
        uxStoreSelect.UpdateStoreList();
    }
}
