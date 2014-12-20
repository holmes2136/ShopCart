using System;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;

public partial class AccountDashboard : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxHistoryGrid, "OrderID", GridViewHelper.Direction.DESC );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string CurrentID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        }
    }

    private void PopulateControls()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        uxEmailLabel.Text = customer.Email;
        uxFirstNameLabel.Text = customer.BillingAddress.FirstName;
        uxLastNameLable.Text = customer.BillingAddress.LastName;
        uxUsernameLabel.Text = customer.UserName;

        uxBillingAddress1.Text = customer.BillingAddress.Address1;
        uxBillingAddress2.Text = customer.BillingAddress.Address2;
        uxBillingCity.Text = customer.BillingAddress.City;
        uxBillingCompany.Text = customer.BillingAddress.Company;
        uxBillingCountry.Text = DataAccessContext.CountryRepository.GetOne( customer.BillingAddress.Country ).CommonName;
        uxBillingFax.Text = customer.BillingAddress.Fax;
        uxBillingFirstName.Text = customer.BillingAddress.FirstName;
        uxBillingLastName.Text = customer.BillingAddress.LastName;
        uxBillingPhone.Text = customer.BillingAddress.Phone;
        uxBillingState.Text = customer.BillingAddress.State;
        uxBillingZip.Text = customer.BillingAddress.Zip;

        if (DataAccessContext.Configurations.GetBoolValue( "EnableAccountGreetingText", StoreContext.CurrentStore ))
        {
            uxMyAccountMessagePanel.Visible = true;

            uxGreetingCustomerName.Text = customer.UserName;
            uxGreetingText.Text =
                DataAccessContext.Configurations.GetValue( StoreContext.Culture.CultureID, "AccountGreetingText", StoreContext.CurrentStore );
        }
    }

    private string CreateExtraField()
    {
        return " PaymentComplete = " + DataAccess.CreateLiteralBool( true ) + " AND Username = @Username ";
    }

    private void RefreshGrid()
    {
        string username = this.User.Identity.Name;
        int totalItems;
        IList<Order> orderList = new List<Order>();

        orderList = DataAccessContext.OrderRepository.SearchOrder(
                  GridHelper.GetFullSortText(),
                  uxSearchFilter.SearchFilterObj,
                  CreateExtraField(),
                  new StoreRetriever().GetCurrentStoreID(), 0, 4,
                  out totalItems, DataAccess.CreateParameterString( username ) );

        if (orderList.Count > 0)
            GridViewHelper.ShowGridAlways( uxHistoryGrid, orderList, String.Empty );
        else
        {
            uxHistoryGrid.DataSource = orderList;
            uxHistoryGrid.DataBind();
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
            RefreshGrid();
        }
    }

    protected void uxEditAccountButton_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect( "~/accountdetails.aspx?editmode=account" );
    }

    protected void uxEditBillingAddressButton_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect( "~/accountdetails.aspx?editmode=address" );
    }

    protected void uxViewOrders_OnClick( object sender, EventArgs e )
    {
        Page.Response.Redirect( "~/orderhistory.aspx" );
    }
}