using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Base.Domain;

public partial class Admin_Components_ShippingAddressDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["ShippingAddressID"];
        }
    }

    private string CustomerID
    {
        get
        {
            return MainContext.QueryString["CustomerID"];
        }
    }

    private void uxShippingState_RefreshHandler( object sender, EventArgs e )
    {
        uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
        uxShippingStateList.Refresh();
    }

    private void PopulateControls()
    {
        if (CustomerID != null &&
             int.Parse( CustomerID ) >= 0)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

            ShippingAddress shippingAddress = customer.GetShippingAddress( CurrentID );

            uxShippingAliasName.Text = shippingAddress.AliasName;
            uxShippingFirstName.Text = shippingAddress.FirstName;
            uxShippingLastName.Text = shippingAddress.LastName;
            uxShippingCompany.Text = shippingAddress.Company;
            uxShippingAddress1.Text = shippingAddress.Address1;
            uxShippingAddress2.Text = shippingAddress.Address2;
            uxShippingCity.Text = shippingAddress.City;
            uxShippingZip.Text = shippingAddress.Zip;
            uxShippingCountryList.CurrentSelected = shippingAddress.Country;
            uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
            uxShippingStateList.Refresh();
            uxShippingStateList.CurrentSelected = shippingAddress.State;
            uxShippingPhone.Text = shippingAddress.Phone;
            uxShippingFax.Text = shippingAddress.Fax;

            if (shippingAddress.IsSameAsBillingAddress)
            {
                uxUseBillingAsShipping.Checked = true;
                SetEnabledControl( false );
            }

        }
    }

    private void SetEnabledControl( bool isEnable )
    {
        uxShippingFirstName.Enabled = isEnable;
        uxShippingLastName.Enabled = isEnable;
        uxShippingCountryList.SetEnable( isEnable );
        uxShippingCompany.Enabled = isEnable;
        uxShippingAddress1.Enabled = isEnable;
        uxShippingAddress2.Enabled = isEnable;
        uxShippingCity.Enabled = isEnable;
        uxShippingStateList.SetEnable( isEnable );
        uxShippingZip.Enabled = isEnable;
        uxShippingPhone.Enabled = isEnable;
        uxShippingFax.Enabled = isEnable;

    }

    private void ClearInputFields()
    {
        uxShippingAliasName.Text = "";
        uxShippingFirstName.Text = "";
        uxShippingLastName.Text = "";
        uxShippingCompany.Text = "";
        uxShippingAddress1.Text = "";
        uxShippingAddress2.Text = "";
        uxShippingCity.Text = "";
        uxShippingStateList.CurrentSelected = "";
        uxShippingZip.Text = "";
        uxShippingCountryList.CurrentSelected = "";
        uxShippingPhone.Text = "";
        uxShippingFax.Text = "";

        uxUseBillingAsShipping.Checked = false;
        SetEnabledControl( true );
    }

    private void Update()
    {
        try
        {

            if (Page.IsValid)
            {
                Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

                for (int i = 0; i < customer.ShippingAddresses.Count; i++)
                {
                    if (customer.ShippingAddresses[i].ShippingAddressID == CurrentID)
                    {
                        customer.ShippingAddresses[i] = new ShippingAddress( new Address(
                             uxShippingFirstName.Text,
                             uxShippingLastName.Text,
                             uxShippingCompany.Text,
                             uxShippingAddress1.Text,
                             uxShippingAddress2.Text,
                             uxShippingCity.Text,
                             uxShippingStateList.CurrentSelected,
                             uxShippingZip.Text,
                             uxShippingCountryList.CurrentSelected,
                             uxShippingPhone.Text,
                             uxShippingFax.Text ),
                             false );

                        if (uxShippingAliasName.Text.Length > 50)
                            customer.ShippingAddresses[i].AliasName = uxShippingAliasName.Text.Substring( 0, 50 );
                        else
                            customer.ShippingAddresses[i].AliasName = uxShippingAliasName.Text;

                        customer.ShippingAddresses[i].ShippingAddressID = CurrentID;

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

                PopulateControls();
                uxMessage.DisplayMessage( Resources.CustomerMessages.UpdateShippingAddressSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxShippingCountryList.BubbleEvent += new EventHandler( uxShippingState_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
                uxAddButton.Visible = false;

                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }
            }
            else
            {

                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxUpdateButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "ShippingAddressList.ascx" );
                }
            }

        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {

        try
        {
            if (Page.IsValid)
            {
                Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

                ShippingAddress shippingAddress = new ShippingAddress( new Address(
                    uxShippingFirstName.Text,
                    uxShippingLastName.Text,
                    uxShippingCompany.Text,
                    uxShippingAddress1.Text,
                    uxShippingAddress2.Text,
                    uxShippingCity.Text,
                    uxShippingStateList.CurrentSelected,
                    uxShippingZip.Text,
                    uxShippingCountryList.CurrentSelected,
                    uxShippingPhone.Text,
                    uxShippingFax.Text ),
                    false );

                if (uxShippingAliasName.Text.Length > 50)
                    shippingAddress.AliasName = uxShippingAliasName.Text.Substring( 0, 50 );
                else
                    shippingAddress.AliasName = uxShippingAliasName.Text;

                if (uxUseBillingAsShipping.Checked)
                    shippingAddress.IsSameAsBillingAddress = true;

                customer.ShippingAddresses.Add( shippingAddress );

                DataAccessContext.CustomerRepository.Save( customer );
                uxMessage.DisplayMessage( Resources.CustomerMessages.AddShippingAddressSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
    }


    protected void uxUseBillingAsShipping_CheckedChanged( object sender, EventArgs e )
    {
        if (uxUseBillingAsShipping.Checked == true)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
            uxShippingFirstName.Text = customer.BillingAddress.FirstName;
            uxShippingLastName.Text = customer.BillingAddress.LastName;
            uxShippingCountryList.CurrentSelected = customer.BillingAddress.Country;
            uxShippingCompany.Text = customer.BillingAddress.Company;
            uxShippingAddress1.Text = customer.BillingAddress.Address1;
            uxShippingAddress2.Text = customer.BillingAddress.Address2;
            uxShippingCity.Text = customer.BillingAddress.City;
            uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
            uxShippingStateList.Refresh();
            uxShippingStateList.CurrentSelected = customer.BillingAddress.State;
            uxShippingZip.Text = customer.BillingAddress.Zip;
            uxShippingPhone.Text = customer.BillingAddress.Phone;
            uxShippingFax.Text = customer.BillingAddress.Fax;

            SetEnabledControl( false );
        }
        else
        {
            SetEnabledControl( true );
        }
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }
}
