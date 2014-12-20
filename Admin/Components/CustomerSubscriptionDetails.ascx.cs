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

using Vevo;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Domain.Contents;
using Vevo.Deluxe.Domain.Contents;
using Vevo.Deluxe.Domain.Users;
using Vevo.Deluxe.Domain;
using System.Collections.Generic;

public partial class Admin_Components_CustomerSubscriptionDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string _subscriptionLevelID = String.Empty;
    private string CustomerID
    {
        get
        {
            return MainContext.QueryString["CustomerID"];
        }
    }

    private string SubscriptionLevelID
    {
        get
        {
            if (String.IsNullOrEmpty( _subscriptionLevelID ))
                _subscriptionLevelID = MainContext.QueryString["SubscriptionLevelID"];

            return _subscriptionLevelID;
        }
        set
        {
            _subscriptionLevelID = value;
        }
    }

    private void PopulateControls()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
        uxUserNameLabel.Text = customer.UserName;
        CustomerSubscription subscription = new CustomerSubscription();
        uxSubscriptionLevel.SelectedValue = SubscriptionLevelID;
        uxSubscriptionLevelHidden.Value = SubscriptionLevelID;

        uxStartDateCalendarPopup.SelectedDate = subscription.GetCustomerSubscriptionByLevelID( SubscriptionLevelID, CustomerID ).StartDate;
        uxExpireDateCalendarPopup.SelectedDate = subscription.GetCustomerSubscriptionByLevelID( SubscriptionLevelID, CustomerID ).EndDate;
        uxIsActiveCheck.Checked = subscription.GetCustomerSubscriptionByLevelID( SubscriptionLevelID, CustomerID ).IsActive;
    }

    private void PopulateSubscriptionLevelControl()
    {
        uxSubscriptionLevel.Items.Clear();
        uxSubscriptionLevel.DataSource = DataAccessContextDeluxe.SubscriptionLevelRepository.GetAll( "SubscriptionLevelID" ); ;
        uxSubscriptionLevel.DataBind();
    }

    private void ClearInputFields()
    {
        uxSubscriptionLevel.SelectedIndex = 0;
        uxStartDateCalendarPopup.SelectedDate = DateTime.Now;
        uxExpireDateCalendarPopup.SelectedDate = DateTime.Now;
        uxIsActiveCheck.Checked = true;
    }

    private void AddNew()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );

        IList<CustomerSubscription> subscriptionList = DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( CustomerID );

        CustomerSubscription subscription = new CustomerSubscription();
        subscription.StartDate = uxStartDateCalendarPopup.SelectedDate;
        subscription.EndDate = uxExpireDateCalendarPopup.SelectedDate;
        subscription.SubscriptionLevelID = uxSubscriptionLevel.SelectedValue;
        subscription.IsActive = uxIsActiveCheck.Checked;
        subscriptionList.Add( subscription );
        DataAccessContext.CustomerRepository.Save( customer );
        DataAccessContextDeluxe.CustomerSubscriptionRepository.Save( customer.CustomerID, subscriptionList );
    }

    private void Update()
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
        CustomerSubscription subscription = new CustomerSubscription();
        subscription = subscription.GetCustomerSubscriptionByLevelID( uxSubscriptionLevel.SelectedValue, CustomerID );

        IList<CustomerSubscription> customerSubscriptionList = DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( CustomerID );

        if (subscription != null)
        {
            subscription.StartDate = uxStartDateCalendarPopup.SelectedDate;
            subscription.EndDate = uxExpireDateCalendarPopup.SelectedDate;
            subscription.IsActive = uxIsActiveCheck.Checked;

            int index = -1;
            for (int i = 0; i < customerSubscriptionList.Count; i++)
            {
                if (customerSubscriptionList[i].SubscriptionLevelID == uxSubscriptionLevelHidden.Value)
                {
                    index = i;
                }
            }

            customerSubscriptionList.RemoveAt( index );
            customerSubscriptionList.Add( subscription );
        }
        else
        {
            subscription = new CustomerSubscription();
            subscription.StartDate = uxStartDateCalendarPopup.SelectedDate;
            subscription.EndDate = uxExpireDateCalendarPopup.SelectedDate;
            subscription.SubscriptionLevelID = uxSubscriptionLevel.SelectedValue;
            subscription.IsActive = uxIsActiveCheck.Checked;

            int index = -1;
            for (int i = 0; i < customerSubscriptionList.Count; i++)
            {
                if (customerSubscriptionList[i].SubscriptionLevelID == uxSubscriptionLevelHidden.Value)
                {
                    index = i;
                }
            }

            customerSubscriptionList.RemoveAt( index );
            customerSubscriptionList.Add( subscription );
        }
        DataAccessContextDeluxe.CustomerSubscriptionRepository.Save( customer.CustomerID, customerSubscriptionList );
        SubscriptionLevelID = uxSubscriptionLevel.SelectedValue;

    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateSubscriptionLevelControl();
        }

        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

            if (IsAdminModifiable())
            {
                uxUpdateButton.Visible = true;
            }
            else
            {
                uxUpdateButton.Visible = false;
            }

            uxAddButton.Visible = false;

        }
        else
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
            uxUserNameLabel.Text = customer.UserName;

            uxStartDateCalendarPopup.SelectedDate = DateTime.Now;
            uxExpireDateCalendarPopup.SelectedDate = DateTime.Now;

            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "CustomerList.ascx", "" );
            }
        }

    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (Page.IsValid)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
            CustomerSubscription subscription = new CustomerSubscription();
            subscription = subscription.GetCustomerSubscriptionByLevelID( uxSubscriptionLevel.SelectedValue, CustomerID );
            if (subscription != null)
            {
                uxMessage.DisplayError( Resources.CustomerMessages.DuplicateCustomerSubscriptionError );
                return;
            }

            try
            {
                AddNew();
                ClearInputFields();
                uxMessage.DisplayMessage( Resources.CustomerMessages.AddCustomerSubscriptionSuccess );
            }
            catch (Exception ex)
            {
                uxMessage.DisplayException( ex );
            }
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        if (Page.IsValid)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
            CustomerSubscription subscription = new CustomerSubscription();
            if (SubscriptionLevelID != uxSubscriptionLevel.SelectedValue)
            {
                subscription = subscription.GetCustomerSubscriptionByLevelID( uxSubscriptionLevel.SelectedValue, CustomerID );
                if (subscription != null)
                {
                    uxMessage.DisplayError( Resources.CustomerMessages.DuplicateCustomerSubscriptionError );
                    return;
                }
            }

            try
            {
                Update();
                PopulateControls();
                uxMessage.DisplayMessage( Resources.CustomerMessages.UpdateCustomerSubscriptionSuccess );
            }
            catch (Exception ex)
            {
                uxMessage.DisplayException( ex );
            }
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
