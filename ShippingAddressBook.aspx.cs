using System;
using Vevo.Domain.Users;
using Vevo.Domain;

public partial class ShippingAddressBook : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            RefreshGrid();
        }
    }

    protected void uxAddNewShippingAddress_Click( object sender, EventArgs e )
    {
        Response.Redirect( "ShippingAddress.aspx" );
    }

    public void RefreshGrid()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        uxList.DataSource = customer.ShippingAddresses;
        uxList.DataBind();

    }
}