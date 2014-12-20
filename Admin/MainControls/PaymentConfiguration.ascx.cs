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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using System.Drawing;
using System.Collections.Generic;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_MainControls_PaymentConfiguration : AdminAdvancedBaseUserControl
{
    private void AddWarningDialog()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            uxUpdatemButtonConfirm.TargetControlID = "uxUpdateButton";
            uxConfirmModalPopup.TargetControlID = "uxUpdateButton";
        }
        else
        {
            uxUpdatemButtonConfirm.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private void RefreshConfirmationDialogHandler( object sender, EventArgs e )
    {
        AddWarningDialog();
    }

    private void PopulateControls()
    {
        uxCurrencySelector.RefreshDropDownList();

        IList<PaymentOption> sourcePayments = DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            AdminUtilities.CurrentCulture, BoolFilter.ShowAll );
        if (DataAccessContext.Configurations.GetBoolValue( "VevoPayPADSSMode" ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxPaymentListGrid.DataSource = sourcePayments;
        }
        else
        {
            IList<PaymentOption> integratedPayments = new List<PaymentOption>();
            foreach (PaymentOption payment in sourcePayments)
            {
                if (!string.IsNullOrEmpty( payment.IntegratedProviderClassName ) ||
                    (payment.Name == "Custom") ||
                     (payment.Name == "Bank Transfer") || 
                     (payment.Name == "No Payment") ||
                     (payment.Name == "Purchase Order"))
                    integratedPayments.Add( payment );
            }
            uxPaymentListGrid.DataSource = integratedPayments;
        }

        uxPaymentListGrid.DataBind();

        uxButtonListGrid.DataSource = DataAccessContext.PaymentOptionRepository.GetButtonList(
            AdminUtilities.CurrentCulture, BoolFilter.ShowAll );
        uxButtonListGrid.DataBind();
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        uxCurrencySelector.BubbleEvent += new EventHandler( RefreshConfirmationDialogHandler );

        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    private void UpdateGatewayData( GridView grid )
    {
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            if (grid.Rows[i].RowType == DataControlRowType.DataRow)
            {
                string sortOrder = ((TextBox) grid.Rows[i].FindControl( "uxSortOrderText" )).Text;

                PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                    AdminUtilities.CurrentCulture, grid.DataKeys[i].Value.ToString() );
                paymentOption.SortOrder = ConvertUtilities.ToInt32( sortOrder );
                DataAccessContext.PaymentOptionRepository.Update( paymentOption );
            }
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            UpdateVisible( false );
        }
    }

    private void UpdateVisible( bool value )
    {
        uxUpdateButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxUpdatemButtonConfirm.TargetControlID = "uxUpdateButton";
                uxConfirmModalPopup.TargetControlID = "uxUpdateButton";
            }
            else
            {
                uxUpdatemButtonConfirm.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxUpdatemButtonConfirm.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateGatewayData( uxPaymentListGrid );
                UpdateGatewayData( uxButtonListGrid );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["PaymentCurrency"],
                    uxCurrencySelector.SelectedValue );

                uxMessage.DisplayMessage( Resources.PaymentMessages.UpdateSuccess );
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected Color GetEnabledColor( object isEnabled )
    {
        if (ConvertUtilities.ToBoolean( isEnabled ))
            return Color.DarkGreen;
        else
            return Color.Chocolate;
    }
}
