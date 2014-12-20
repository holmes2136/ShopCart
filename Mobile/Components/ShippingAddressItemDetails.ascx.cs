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
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;

public partial class Mobile_Components_ShippingAddressItemDetails : Vevo.WebUI.Products.BaseProductListItemUserControl
{
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
        ShippingAddress shippingAddress = customer.GetShippingAddress( ShippingAddressID );
        if (shippingAddress.Residential)
            uxShippingResidentialDrop.SelectedValue = "True";
        else
            uxShippingResidentialDrop.SelectedValue = "False";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( String.Format( "ShippingAddress.aspx?EditMode=true&ShippingAddressID={0}", ShippingAddressID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        Response.Redirect( String.Format( "ShippingAddress.aspx?ShippingAddressID={0}", ShippingAddressID ) );
    }

    protected string GetCountry( object country )
    {
        return DataAccessContext.CountryRepository.GetOne( ConvertUtilities.ToString( country ) ).CommonName;
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
