using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;


public partial class AdminAdvanced_Components_CustomerTracking : AdminAdvancedBaseUserControl
{
    private string _customerEmail;


    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["OrderID"] ))
                return MainContext.QueryString["OrderID"];
            else
                return "0";
        }
    }


    private void ClearInput()
    {
        uxSenderNameText.Text = String.Empty;
        uxSenderEmailText.Text = String.Empty;
        uxMessageText.Text = String.Empty;
    }

    private void PopulateControls()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        _customerEmail = order.Email;

        uxOrderIDLabel.Text = CurrentOrderID;
        uxCustomerEmailLabel.Text = _customerEmail;
        uxSenderNameText.Text = "";
        uxSenderEmailText.Text = DataAccessContext.Configurations.GetValue( "CompanyEmail" );
        uxMessageText.Text = "";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
            uxSendButton.Visible = false;
    }

    protected void uxSendButton_Click( object sender, EventArgs e )
    {
        try
        {
            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
            Store store = DataAccessContext.StoreRepository.GetOne( order.StoreID );

            WebUtilities.SendMail(
                uxSenderEmailText.Text,
                uxCustomerEmailLabel.Text,
                "Message regarding your order ID: " + CurrentOrderID,
                "This is a message from your merchant." +
                "From: " + uxSenderNameText.Text + "\n\n" +
                "Message:\n" + uxMessageText.Text,
                store );

            OrderTrackingAccess.Create( CurrentOrderID, uxSenderNameText.Text,
                uxSenderEmailText.Text, uxMessageText.Text );

            MessageControl.DisplayMessage( Resources.OrdersMessages.TrackingSendSuccess );
        }
        catch (Exception ex)
        {
            MessageControl.DisplayException( ex );
        }

        ClearInput();
    }


}
