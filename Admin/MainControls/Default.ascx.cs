using System;
using System.Collections.Generic;
using System.Data;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Reports;
using Vevo.WebUI;
using Vevo.WebUI.Users;

public partial class AdminAdvanced_MainControls_Default : AdminAdvancedBaseUserControl
{
    private const string _orderPage = "OrdersList.ascx";
    private const string _customerPage = "CustomerList.ascx";
    private string message = "";

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            SetupDisplayBox();
        }

        if (KeyUtilities.IsMultistoreLicense())
        {
            uxStoreViewLabel.Visible = true;
            uxStoreList.Visible = true;
        }

        uxWarningPanel.Style["display"] = "none";
        VulnerableFiles vulnerable = new VulnerableFiles();
        if (vulnerable.Existed)
        {
            uxSecurityWarningLink.Visible = true;
            uxSecurityWarningLink.Attributes.Add( "onclick", String.Format( "$('#{0}').toggle('slow'); return false;", uxWarningPanel.ClientID ) );

            RecoveryPageValid();
            uxMessageLabel.Text = message;//document.getElementById('{0}').className='mgl30p';
        }

        uxTabContainerReport.ActiveTabIndex = 0;
        uxTabularView.StoreID = StoreID;
        uxGraphicView.StoreID = StoreID;
        RefreshGrid();
    }

    protected void RecoveryPageValid()
    {
        VulnerableFiles vulnerable = new VulnerableFiles();
        foreach (string path in vulnerable.ExistedPaths)
        {
            message += path + "<br/>";
        }
    }

    protected void uxRefreshButton_Click( object sender, EventArgs e )
    {
    }

    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        uxTabularView.StoreID = StoreID;
        uxGraphicView.StoreID = StoreID;
    }

    protected void uxViewAllOrder_Click( object sender, EventArgs e )
    {
        UrlQuery urlQuery = new UrlQuery();

        urlQuery.AddQuery( "Processed", "ShowAll" );
        urlQuery.AddQuery( "Payment", "ShowAll" );
        urlQuery.AddQuery( "Sort", "OrderDate DESC" );

        MainContext.RedirectMainControl( "OrdersList.ascx", urlQuery.RawQueryString );
    }

    protected void uxViewAllCustomer_Click( object sender, EventArgs e )
    {
        UrlQuery urlQuery = new UrlQuery();

        urlQuery.AddQuery( "Sort", "CustomerID DESC" );

        MainContext.RedirectMainControl( "CustomerList.ascx", urlQuery.RawQueryString );
    }

    protected void uxViewAllProduct_Click( object sender, EventArgs e )
    {
        UrlQuery urlQuery = new UrlQuery();

        MainContext.RedirectMainControl( "ProductList.ascx", urlQuery.RawQueryString );
    }

    private void SetupDisplayBox()
    {
        AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
        if (!admin.CanViewPage( _orderPage ))
        {
            uxTabContainerReport.Visible = false;
            uxTabLastestOrder.Visible = false;
            uxTabBestSeller.Visible = false;
        }

        if (!admin.CanViewPage( _customerPage ))
        {
            uxTabNewCustomers.Visible = false;
        }
    }

    public string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return uxStoreList.CurrentSelected;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    protected void RefreshGrid()
    {
        IList<Order> orderList = DataAccessContext.OrderRepository.GetLastestOrder( 5 );

        uxLastestOrderGrid.DataSource = orderList;
        uxLastestOrderGrid.DataBind();

        BestSellingReportBuilder bestSellingReportBuilder = new BestSellingReportBuilder();
        DataTable tablebestSelling = bestSellingReportBuilder.GetBestSellingReportData(
            "ProductPrice DESC",
            StoreContext.Culture.CultureID,
            PeriodType.Custom,
            BestSellReportType.NumberOfProduct,
            "5",
            "0",
            DateTime.Now.AddYears( -100 ),
            DateTime.Now );

        uxBestSellerGrid.DataSource = tablebestSelling;
        uxBestSellerGrid.DataBind();

        uxGridCustomer.DataSource = DataAccessContext.CustomerRepository.GetLastestCustomers( 5 );
        uxGridCustomer.DataBind();
    }
}
