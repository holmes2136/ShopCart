using System;
using System.Collections.Generic;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Base.Domain;

public partial class Mobile_AccountDetails : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private

    private string CurrentID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        }
    }

    private bool IsWholesale
    {
        get
        {
            return (bool) ViewState["IsWholesale"];
        }
        set
        {
            ViewState["IsWholesale"] = value;
        }
    }

    private void PopulateControls()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        uxUserName.Text = customer.UserName;
        uxFirstName.Text = customer.BillingAddress.FirstName;
        uxLastName.Text = customer.BillingAddress.LastName;
        uxCompany.Text = customer.BillingAddress.Company;
        uxAddress1.Text = customer.BillingAddress.Address1;
        uxAddress2.Text = customer.BillingAddress.Address2;
        uxCity.Text = customer.BillingAddress.City;
        uxZip.Text = customer.BillingAddress.Zip;

        uxCountryAndState.CurrentCountry = customer.BillingAddress.Country;
        uxCountryAndState.CurrentState = customer.BillingAddress.State;

        uxPhone.Text = customer.BillingAddress.Phone;
        uxFax.Text = customer.BillingAddress.Fax;
        uxEmail.Text = customer.Email;
        IsWholesale = customer.IsWholesale;

        StoreRetriever storeRetriever = new StoreRetriever();
        NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( customer.Email, storeRetriever.GetStore() );
        if (!newsLetter.IsNull)
            uxSubscribeCheckBox.Checked = true;
        else
            uxSubscribeCheckBox.Checked = false;
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
        bool validateBilling = uxCountryAndState.Validate( out validateCountry, out validateState );

        if (!validateBilling)
        {
            uxBillingCountryStateDiv.Visible = true;
            uxBillingCountryStateMessage.Text = ValidateCountryAndState( validateCountry, validateState );
            result = false;
        }

        return result;
    }

    private bool MoreThanOneUserSubscribeWithEmail( string email )
    {
        IList<Customer> customerList = DataAccessContext.CustomerRepository.GetCustomerByEmail( email );
        return customerList.Count > 1;
    }

    private void UpdateEmailSubscriber( string email, string emailOld )
    {
        StoreRetriever storeRetriever = new StoreRetriever();
        Store store = storeRetriever.GetStore();
        string emailHash = SecurityUtilities.HashMD5( email + WebConfiguration.SecretKey );
        if (uxSubscribeCheckBox.Checked)
        {
            NewsLetter newsLetterOld = DataAccessContext.NewsLetterRepository.GetOne( emailOld, store );
            NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( email, store );

            if (newsLetterOld.IsNull)
            {
                if (newsLetter.IsNull)
                {
                    newsLetter.Email = email;
                    newsLetter.EmailHash = emailHash;
                    newsLetter.JoinDate = DateTime.Now;
                    newsLetter.StoreID = store.StoreID;
                    DataAccessContext.NewsLetterRepository.Create( newsLetter );
                }
            }
            else
            {
                if (MoreThanOneUserSubscribeWithEmail( emailOld ))
                {
                    // No need to delete old email
                    if (newsLetter.IsNull)
                    {
                        newsLetter.Email = email;
                        newsLetter.EmailHash = emailHash;
                        newsLetter.JoinDate = DateTime.Now;
                        newsLetter.StoreID = store.StoreID;
                        DataAccessContext.NewsLetterRepository.Create( newsLetter );
                    }
                }
                else
                {
                    if (String.Compare( email, emailOld ) != 0)
                    {
                        // No need to keep old email
                        if (newsLetter.IsNull)
                        {
                            newsLetterOld.EmailHash = emailHash;
                            DataAccessContext.NewsLetterRepository.Update( newsLetterOld, email, store.StoreID );
                        }
                        else
                        {
                            DataAccessContext.NewsLetterRepository.DeleteEmailNoHash( emailOld, store );
                        }
                    }
                }
            }
        }
        else
        {
            if (!MoreThanOneUserSubscribeWithEmail( email ))
                DataAccessContext.NewsLetterRepository.DeleteEmail( emailHash, store );
        }
    }

    private void UpdateCustomer()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        string emailOld = customer.Email;
        UpdateEmailSubscriber( uxEmail.Text.ToString().Trim(), emailOld );

        Address billingAddress = new Address( uxFirstName.Text, uxLastName.Text, uxCompany.Text,
            uxAddress1.Text, uxAddress2.Text, uxCity.Text, uxCountryAndState.CurrentState,
            uxZip.Text, uxCountryAndState.CurrentCountry, uxPhone.Text, uxFax.Text );

        customer.UserName = uxUserName.Text;
        customer.BillingAddress = billingAddress;

        customer.Email = uxEmail.Text;
        DataAccessContext.CustomerRepository.Save( customer );
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
        Refresh();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid && VerifyCountryAndState())
            {
                UpdateCustomer();
                AffiliateHelper.UpdateAffiliateReference( uxUserName.Text );
                uxMessage.DisplayMessage( "[$UpdateComplete]" );
            }
        }
        catch (DataAccessException ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddNewShippingAddress_Click( object sender, EventArgs e )
    {
        Response.Redirect( "ShippingAddress.aspx" );
    }

    public void Refresh()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
        uxList.DataSource = customer.ShippingAddresses;
        uxList.DataBind();
    }

    #endregion

}
