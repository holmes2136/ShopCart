using System;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Shipping;
using Vevo.Domain.Users;
using Vevo.Base.Domain;

public partial class Components_ShippingAddressItemDetails : Vevo.WebUI.Products.BaseProductListItemUserControl
{
    private string CurrentID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        }
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
            uxShippingResidentialPanel.Visible = true;
        else
            uxShippingResidentialPanel.Visible = false;
    }

    private void PopulateControls()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        ShippingAddress shippingAddress = customer.GetShippingAddress( ShippingAddressID );
        if (shippingAddress.Residential)
            uxShippingResidentialDrop.SelectedValue = "True";
        else
            uxShippingResidentialDrop.SelectedValue = "False";
    }

    protected string GetFullCountryName(string country)
    {
        return DataAccessContext.CountryRepository.GetOne(country).CommonName;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
            ShowHideShippingResidential();
        }
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( String.Format( "~/ShippingAddress.aspx?EditMode=true&ShippingAddressID={0}", ShippingAddressID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( String.Format( "~/ShippingAddress.aspx?ShippingAddressID={0}", ShippingAddressID ) );
    }

    public string ShippingAddressID
    {
        get
        {
            if (ViewState["ShippingAddressID"] == null)
                return "0";
            else
                return (string) ViewState["ShippingAddressID"];
        }
        set
        {
            ViewState["ShippingAddressID"] = value;
        }
    }


}
