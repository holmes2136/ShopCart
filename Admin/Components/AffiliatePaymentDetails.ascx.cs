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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Marketing;

public partial class AdminAdvanced_Components_AffiliatePaymentDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string AffiliatePaymentID
    {
        get
        {
            return MainContext.QueryString["AffiliatePaymentID"];
        }
    }

    private string AffiliateOrderID
    {
        get
        {
            return MainContext.QueryString["AffiliateOrderID"];
        }
    }

    private string AffiliateCode
    {
        get
        {
            return MainContext.QueryString["AffiliateCode"];
        }
    }

    private void PopulateControls()
    {
        if (AffiliatePaymentID != null &&
             int.Parse( AffiliatePaymentID ) >= 0)
        {
            AffiliatePayment affiliatePayment = DataAccessContextDeluxe.AffiliatePaymentRepository.GetOne( AffiliatePaymentID );
            uxCalendarPopup.SelectedDate = Convert.ToDateTime( affiliatePayment.PaidDate );
            uxAmountText.Text = affiliatePayment.Amount.ToString();
            uxNoteText.Text = affiliatePayment.PaymentNote;
            PopulateOrderID();
        }
    }

    private void PopulateOrderID()
    {
        string orderID = string.Empty;
        if (IsEditMode())
        {
            orderID = DataAccessContextDeluxe.AffiliateOrderRepository.GetOrderIDByAffiliatePaymentID( AffiliatePaymentID );
        }
        else
        {
            string[] affiliateOrderIDs = AffiliateOrderID.Split( '-' );
            AffiliateOrder affiliateOrder;
            decimal amount = 0;
            for (int i = 0; i < affiliateOrderIDs.Length; i++)
            {
                affiliateOrder = DataAccessContextDeluxe.AffiliateOrderRepository.GetOne( affiliateOrderIDs[i].Trim() );
                amount += affiliateOrder.Commission;
                if (String.IsNullOrEmpty( orderID ))
                    orderID = affiliateOrder.OrderID;
                else
                    orderID += ", " + affiliateOrder.OrderID;
            }
            uxAmountText.Text = Currency.RoundAmount( amount ).ToString();
            uxCalendarPopup.SelectedDate = DateTime.Today;
        }
        uxOrderIDLabel.Text = orderID;

    }

    private void UpdateCommission( string affiliatePaymentID )
    {
        string[] orderIDs = uxOrderIDLabel.Text.Split( ',' );

        for (int i = 0; i < orderIDs.Length; i++)
        {
            string affiliateOrderID = DataAccessContextDeluxe.AffiliateOrderRepository.GetAffiliateOrderIDFromOrderID( orderIDs[i].Trim() );
            AffiliateOrder affiliateOrder = DataAccessContextDeluxe.AffiliateOrderRepository.GetOne( affiliateOrderID );
            affiliateOrder.AffiliatePaymentID = affiliatePaymentID;
            DataAccessContextDeluxe.AffiliateOrderRepository.Save( affiliateOrder );
        }
    }

    private AffiliatePayment SetUpAffiliatePayment( AffiliatePayment affiliatePayment )
    {
        affiliatePayment.AffiliateCode = AffiliateCode;
        affiliatePayment.Amount = ConvertUtilities.ToDecimal( uxAmountText.Text.Trim() );
        affiliatePayment.PaidDate = uxCalendarPopup.SelectedDate;
        affiliatePayment.PaymentNote = uxNoteText.Text;
        return affiliatePayment;
    }

    private void CreateAndRedirect()
    {
        try
        {
            if (Page.IsValid)
            {
                AffiliatePayment affiliatePayment = new AffiliatePayment();
                affiliatePayment = SetUpAffiliatePayment( affiliatePayment );
                affiliatePayment = DataAccessContextDeluxe.AffiliatePaymentRepository.Save( affiliatePayment );

                UpdateCommission( affiliatePayment.AffiliatePaymentID );

                MainContext.RedirectMainControl( "AffiliatePaymentList.ascx", "AffiliateCode=" + AffiliateCode );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid)
            {
                AffiliatePayment affiliatePayment = DataAccessContextDeluxe.AffiliatePaymentRepository.GetOne( AffiliatePaymentID );
                affiliatePayment = SetUpAffiliatePayment( affiliatePayment );
                DataAccessContextDeluxe.AffiliatePaymentRepository.Save( affiliatePayment );

                PopulateControls();
                PopulateOrderID();
                uxMessage.DisplayMessage( Resources.AffiliatePaymentMessages.UpdateSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

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
                    uxSendMailButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                    uxSendMailButton.Visible = false;
                }

            }
            else
            {
                PopulateOrderID();

                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxUpdateButton.Visible = false;
                    uxSendMailButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "AffiliatePaymentList.ascx" );
                }
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        CreateAndRedirect();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
    }

    protected void uxSendMailButton_Click( object sender, EventArgs e )
    {
        Update();
        MainContext.RedirectMainControl( "AffiliatePaymentSendMail.ascx", "AffiliatePaymentID=" +
            AffiliatePaymentID + "&AffiliateCode=" + AffiliateCode );
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
