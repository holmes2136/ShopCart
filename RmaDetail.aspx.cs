using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo.WebUI.International;
using Vevo.Domain;
using Vevo.Domain.Returns;
using Vevo.WebUI;

public partial class RmaDetail : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string RmaID
    {
        get
        {
            if ( Request.QueryString[ "RmaID" ] == null )
                return "0";
            else
                return Request.QueryString[ "RmaID" ];
        }
    }

    private void PopulateControl()
    {
        Rma rma = DataAccessContext.RmaRepository.GetOne( RmaID );

        if ( rma.IsNull || rma.CustomerID != StoreContext.Customer.CustomerID )
        {
            Response.Redirect( "RmaHistory.aspx" );
        }
        else
        {
            uxRmaIDLabel.Text = rma.RmaID;
            uxOrderIDLink.PostBackUrl = "~/CheckoutComplete.aspx?showorder=true&OrderID=" + rma.OrderID;
            uxOrderIDLink.Text = rma.OrderID;
            uxProductNameLabel.Text = rma.ProductName;
            uxQuantityLabel.Text = rma.Quantity.ToString();
            uxRequestDateLabel.Text = rma.RequestDate.ToLongDateString();
            uxReturnReasonLabel.Text = rma.ReturnReason;
            uxRmaActionLabel.Text = rma.GetRmaAction.Name;
            uxRequestStatusLabel.Text = rma.RequestStatus;
            uxRmaNoteLabel.Text = rma.RmaNote;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControl();
    }
}
