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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Orders;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using System.Collections.Generic;

public partial class Mobile_ProductReOrder : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["OrderID"] ))
                return Request.QueryString["OrderID"];
            else
                return "0";
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (CurrentOrderID.Equals( "0" ))
            return;
    }
}
