using System;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Shipping;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;

public partial class Mobile_Components_ShippingAddressDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string CustomerID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        }
    }

    private bool IsEditMode
    {
        get
        {
            if (Request.QueryString["EditMode"] == null)
                return false;
            else
                return true;
        }
    }

    private string ShippingAddressID
    {
        get
        {
            if (Request.QueryString["ShippingAddressID"] == null)
                return String.Empty;
            else
                return Request.QueryString["ShippingAddressID"];
        }
    }


    private void PopulateShipping()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
        ShippingAddress address = customer.GetShippingAddress( ShippingAddressID );
        uxShippingFirstName.Text = address.FirstName;
        uxShippingLastName.Text = address.LastName;
        uxShippingCompany.Text = address.Company;
        uxShippingAddress1.Text = address.Address1;
        uxShippingAddress2.Text = address.Address2;
        uxShippingCity.Text = address.City;
        uxShippingCountryState.CurrentState = address.State;
        uxShippingZip.Text = address.Zip;
        uxShippingCountryState.CurrentCountry = address.Country;
        uxShippingPhone.Text = address.Phone;
        uxShippingFax.Text = address.Fax;
        uxShippingAliasName.Text = address.AliasName;

        uxUseBillingAsShipping.Checked = address.IsSameAsBillingAddress;

        uxShippingResidentialDrop.SelectedValue = address.Residential.ToString();

        if (address.IsSameAsBillingAddress)
        {
            uxShippingDetailsPanel.Enabled = false;
        }
    }

    private void UpdateShippingAddress()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

        for (int i = 0; i < customer.ShippingAddresses.Count; i++)
        {
            if (customer.ShippingAddresses[i].ShippingAddressID == ShippingAddressID)
            {
                customer.ShippingAddresses[i] = new ShippingAddress( new Address(
                     uxShippingFirstName.Text,
                     uxShippingLastName.Text,
                     uxShippingCompany.Text,
                     uxShippingAddress1.Text,
                     uxShippingAddress2.Text,
                     uxShippingCity.Text,
                     uxShippingCountryState.CurrentState,
                     uxShippingZip.Text,
                     uxShippingCountryState.CurrentCountry,
                     uxShippingPhone.Text,
                     uxShippingFax.Text ),
                     ConvertUtilities.ToBoolean( uxShippingResidentialDrop.SelectedValue ) );

                customer.ShippingAddresses[i].AliasName = uxShippingAliasName.Text;
                customer.ShippingAddresses[i].ShippingAddressID = ShippingAddressID;

                if (uxUseBillingAsShipping.Checked)
                {
                    customer.ShippingAddresses[i].IsSameAsBillingAddress = true;
                }
                else
                {
                    customer.ShippingAddresses[i].IsSameAsBillingAddress = false;
                }
            }
        }

        customer = DataAccessContext.CustomerRepository.Save( customer );
    }

    private void AddShippingAddress()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

        ShippingAddress shippingAddress = new ShippingAddress( new Address(
            uxShippingFirstName.Text,
            uxShippingLastName.Text,
            uxShippingCompany.Text,
            uxShippingAddress1.Text,
            uxShippingAddress2.Text,
            uxShippingCity.Text,
            uxShippingCountryState.CurrentState,
            uxShippingZip.Text,
            uxShippingCountryState.CurrentCountry,
            uxShippingPhone.Text,
            uxShippingFax.Text ),
             ConvertUtilities.ToBoolean( uxShippingResidentialDrop.SelectedValue ) );

        shippingAddress.AliasName = uxShippingAliasName.Text;

        if (uxUseBillingAsShipping.Checked)
            shippingAddress.IsSameAsBillingAddress = true;
        else
            shippingAddress.IsSameAsBillingAddress = false;

        customer.ShippingAddresses.Add( shippingAddress );

        DataAccessContext.CustomerRepository.Save( customer );
    }

    private string ValidateCountryAndState( bool validateCountry, bool validateState )
    {
        if (!validateCountry)
        {
            return "Required Country.";
        }
        else if (!validateState)
        {
            return "Required State.";
        }
        else
        {
            return "";
        }
    }

    private bool VerifyCountryAndState()
    {
        bool result = true;
        bool validateCountry, validateState;
        bool validateBilling = uxShippingCountryState.Validate( out validateCountry, out validateState );

        if (!validateBilling)
        {
            uxShippingCountryStateDiv.Visible = true;
            uxShippingCountryStateMessage.Text = ValidateCountryAndState( validateCountry, validateState );
            result = false;
        }

        return result;
    }

    private void DeleteShippingAddress()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

        ShippingAddress shippingAddress = new ShippingAddress();
        int deleteIndex = -1;

        for (int i = 0; i < customer.ShippingAddresses.Count; i++)
        {
            if (customer.ShippingAddresses[i].ShippingAddressID == ShippingAddressID)
            {
                deleteIndex = i;
            }
        }

        customer.ShippingAddresses.RemoveAt( deleteIndex );
        customer = DataAccessContext.CustomerRepository.Save( customer );
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
            uxShippingResidentialPanel.Visible = true;
        else
            uxShippingResidentialPanel.Visible = false;
    }


    protected void uxUseBillingAsShipping_CheckedChanged( object sender, EventArgs e )
    {
        if (uxUseBillingAsShipping.Checked)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
            uxShippingFirstName.Text = customer.BillingAddress.FirstName;
            uxShippingLastName.Text = customer.BillingAddress.LastName;
            uxShippingCountryState.CurrentCountry = customer.BillingAddress.Country;
            uxShippingCountryState.CurrentState = customer.BillingAddress.State;
            uxShippingCompany.Text = customer.BillingAddress.Company;
            uxShippingAddress1.Text = customer.BillingAddress.Address1;
            uxShippingAddress2.Text = customer.BillingAddress.Address2;
            uxShippingCity.Text = customer.BillingAddress.City;
            uxShippingZip.Text = customer.BillingAddress.Zip;
            uxShippingPhone.Text = customer.BillingAddress.Phone;
            uxShippingFax.Text = customer.BillingAddress.Fax;

            uxShippingDetailsPanel.Enabled = false;

        }
        else
        {
            uxShippingDetailsPanel.Enabled = true;
        }

    }
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( ShippingAddressID ))
        {
            if (IsEditMode)
            {
                if (!IsPostBack)
                    PopulateShipping();

                uxAddButton.Visible = false;
                uxUpdateButton.Visible = true;
            }
            else
            {
                try
                {
                    DeleteShippingAddress();
                    Response.Redirect( "AccountDetails.aspx" );

                }
                catch (DataAccessException ex)
                {
                    uxMessage.DisplayException( ex );
                }
            }
        }
        else
        {
            uxAddButton.Visible = true;
            uxUpdateButton.Visible = false;
        }

        ShowHideShippingResidential();

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid && VerifyCountryAndState())
            {
                AddShippingAddress();

                Response.Redirect( "AccountDetails.aspx" );
            }
        }
        catch (DataAccessException ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (!String.IsNullOrEmpty( ShippingAddressID ))
        {
            try
            {
                if (Page.IsValid && VerifyCountryAndState())
                {
                    UpdateShippingAddress();

                    Response.Redirect( "AccountDetails.aspx" );
                }
            }
            catch (DataAccessException ex)
            {
                uxMessage.DisplayException( ex );
            }
        }
        else
        {
            Response.Redirect( "AccountDetails.aspx" );
        }
    }
}
