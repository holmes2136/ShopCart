using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Data;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_MainControls_Invoice : AdminAdvancedBaseUserControl
{
    #region Private

    private bool IsInvoice()
    {
        if (MainContext.QueryString["Action"] == "invoice")
            return true;
        else
            return false;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {

            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
            uxToText.Text = order.Email;

            if (IsInvoice())
            {
                uxTitleLabel.Text = GetLocalResourceObject( "Invoice" ).ToString();
                uxPrintButton.Attributes.Add( "onclick",
                    "getPrintInvoice( 'uxBody' );" );
                uxPackingSlipForm.Visible = false;
            }
            else
            {
                uxTitleLabel.Text = GetLocalResourceObject( "Packing" ).ToString();
                uxEmailButton.Visible = false;
                uxEmailTABLE.Visible = false;
                uxPrintButton.Attributes.Add( "onclick",
                    "getPrintInvoice( 'uxPacking' );" );
                uxInvoiceForm.Visible = false;
            }
        }

        if (!IsAdminModifiable())
        {
            uxEmailButton.Visible = false;
        }
        uxOrderItemSource.SelectParameters.Add( new Parameter( "OrderID", TypeCode.String, CurrentOrderID ) );
        uxInvoiceSource.SelectParameters.Add( new Parameter( "OrderID", TypeCode.String, CurrentOrderID ) );

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxEmailButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (IsInvoice())
            {
                Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
                Store store = DataAccessContext.StoreRepository.GetOne( order.StoreID );

                string companyEmail = DataAccessContext.Configurations.GetValue( "CompanyEmail", store );
                WebUtilities.SendHtmlMail(
                    new MailAddress( companyEmail, uxSenderNameText.Text ),
                    uxToText.Text,
                    uxSubjectText.Text,
                    EmailTemplates.ReadInvoiceTemplate(
                        CurrentOrderID, StoreContext.Culture, StoreContext.Currency ), store );
                uxMessage.DisplayMessage( "Send email is completed.", String.Empty );
            }
        }
        catch
        {
            uxMessage.DisplayError( "can not send email", String.Empty );
        }
    }

    protected string GetConfigValue( string configName )
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        return DataAccessContext.Configurations.GetValue(
            StoreContext.Culture.CultureID,
            configName,
            DataAccessContext.StoreRepository.GetOne( order.StoreID ) );
    }

    protected string GetConfigValueNoCulture( string configName )
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        return DataAccessContext.Configurations.GetValue(
            configName, DataAccessContext.StoreRepository.GetOne( order.StoreID ) );
    }

    protected decimal DisplayDiscount( object discount )
    {
        decimal newDiscount = ConvertUtilities.ToDecimal( discount );
        if (newDiscount > 0)
        {
            newDiscount = newDiscount * -1;
        }
        return newDiscount;
    }

    protected bool DisplayPointDiscount()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ))
        {
            return true;
        }

        if (order.RedeemPoint > 0 || order.RedeemPrice > 0)
            return true;

        return false;
    }

    #endregion


    #region Public Properties

    public string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["OrderID"] ))
                return MainContext.QueryString["OrderID"];
            else
                return "0";
        }
    }

    #endregion

}
