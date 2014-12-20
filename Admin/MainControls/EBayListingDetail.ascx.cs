using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain.EBay;
using Vevo.Domain;
using Vevo.WebUI.ServerControls;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_MainControls_EBayListingDetail : AdminAdvancedBaseUserControl
{
    #region Protected

    private void Page_Init( object sender, System.EventArgs e )
    {
        PopulateControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );
    }

    protected void RedirectToOrderDetail( string orderID )
    {
        MainContext.RedirectMainControl( "OrdersEdit.ascx", "OrderID=" + orderID );
    }

    protected void uxLinkButton_Click( object sender, EventArgs e )
    {
        RedirectToOrderDetail( ((LinkButton) sender).CommandArgument.ToString() );
    }

    #endregion

    #region Public
    public string CurrentID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["ListID"] ))
                return "0";
            else
                return MainContext.QueryString["ListID"];
        }
    }


    public void PopulateControls()
    {
        EBayList list = DataAccessContextDeluxe.EBayListRepository.GetOne( CurrentID );
        Currency currency = DataAccessContext.CurrencyRepository.GetOne( list.Currency );
        lcListIDData.Text = CurrentID;
        lcBidAmountData.Text = list.BidAmount.ToString();
        lcBidPriceData.Text = currency.CurrencySymbol + String.Format( "{0:f2}", list.BidPrice );
        lcBuyItNowPriceData.Text = currency.CurrencySymbol + String.Format( "{0:f2}", list.BuyItNowPrice );
        lcItemNameData.Text = list.ItemName;
        lcItemNumberData.Text = list.ItemNumber;
        lcLastStatusData.Text = list.LastStatus.ToString();
        lcLastUpdateData.Text = list.LastUpdate.ToShortDateString() + " " + list.LastUpdate.ToShortTimeString();
        lcListTypeData.Text = list.ListType.ToString();
        lcQtyLeftData.Text = list.QtyLeft.ToString();

        foreach (EBayListOrder orderList in list.EBayListOrders)
        {
            LinkButton orderLink = new LinkButton();
            orderLink.ID = "uxOrderLink" + orderList.OrderID;
            orderLink.CommandArgument = orderList.OrderID;
            orderLink.Click += new System.EventHandler( uxLinkButton_Click );
            orderLink.Text = orderList.OrderID;
            orderLink.CssClass = "mgr5";
            lcOrderLinkHolder.Controls.Add( orderLink );
        }
    }

    #endregion
}
