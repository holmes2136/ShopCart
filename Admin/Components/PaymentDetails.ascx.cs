using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_Components_PaymentDetails : AdminAdvancedBaseUserControl
{
    private const string GatewayUserControlID = "uxGatewayUserControl";

    private string PaymentName
    {
        get
        {
            if (String.IsNullOrEmpty( MainContext.QueryString["Name"] ))
                return String.Empty;
            else
                return MainContext.QueryString["Name"];
        }
    }

    private void AddWarningDialog()
    {
        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
        {
            WebUtilities.ButtonAddConfirmation(
                uxUpdateButton,
                Resources.PaymentMessages.UpdateCurrencyConfirmation );
        }
    }

    private void RefreshConfirmationDialogHandler( object sender, EventArgs e )
    {
        AddWarningDialog();
    }

    private void HideCommonData( PaymentOption paymentOption )
    {
        if (paymentOption.ShowButton)
        {
            uxLanguageDependentPlaceHolder.Visible = false;
            uxImageTR.Visible = false;
        }
    }

    private void PopulateCommonData()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            uxLanguageControl.CurrentCulture, PaymentName );

        uxPaymentNameLabel.Text = paymentOption.Name;
        uxDisplayNameText.Text = paymentOption.DisplayName;
        uxDescriptionText.Text = paymentOption.Description;
        uxPaymentImageText.Text = paymentOption.PaymentImage;
        uxIsEnabledDrop.SelectedValue = paymentOption.IsEnabled.ToString();

        HideCommonData( paymentOption );

        if (!IsAdminModifiable())
        {
            uxPaymentImageLinkButton.Visible = false;
            uxUpdateButton.Visible = false;
        }
    }

    private string GetPaymentPath( string path )
    {
        Regex regex = new Regex( @"\[Admin\]", RegexOptions.IgnoreCase );
        UrlPath urlPath = new UrlPath( Request.Url.AbsoluteUri );
        return regex.Replace( path, "~/" + urlPath.ExtractFirstApplicationSubfolder() );
    }

    private void PopulatePermissionWarning()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            uxLanguageControl.CurrentCulture, PaymentName );

        uxPermissionWarningPanel.Visible = false;

        if (paymentOption.IsEnabled
            && paymentOption.CreditCardRequired)
        {
            PaymentModuleSetup paymentModuleSetup = new PaymentModuleSetup();
            if (paymentModuleSetup.IsPaymentModuleIntegrated
                && !paymentModuleSetup.EncryptionKeyExists())
                uxPermissionWarningPanel.Visible = true;
        }
    }

    private void PopulatePaymentSpecificControls()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            uxLanguageControl.CurrentCulture, PaymentName );

        if (!String.IsNullOrEmpty( paymentOption.AdminUserControl ))
        {
            Control paymentControl = LoadControl( GetPaymentPath( paymentOption.AdminUserControl ) );
            paymentControl.ID = GatewayUserControlID;

            uxPaymentPanel.Controls.Add( paymentControl );
        }
    }

    private AdminAdvancedBaseGatewayUserControl GetGatewayUserControl()
    {
        return (AdminAdvancedBaseGatewayUserControl) uxPaymentPanel.FindControl( GatewayUserControlID );
    }

    private PaymentOption SetUpPaymentOption( PaymentOption paymentOption )
    {
        paymentOption.DisplayName = uxDisplayNameText.Text;
        paymentOption.Description = uxDescriptionText.Text;
        paymentOption.PaymentImage = uxPaymentImageText.Text;
        paymentOption.IsEnabled = ConvertUtilities.ToBoolean( uxIsEnabledDrop.SelectedValue );

        return paymentOption;
    }

    protected void uxLanguageControl_BubbleEvent( object sender, EventArgs e )
    {
        PopulateCommonData();
        GetGatewayUserControl().CultureID = uxLanguageControl.CurrentCultureID;
        GetGatewayUserControl().Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxCurrencySelector.BubbleEvent += new EventHandler( RefreshConfirmationDialogHandler );

        PopulatePaymentSpecificControls();

        uxPaymentImageUpload.ReturnTextControlClientID = uxPaymentImageText.ClientID;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateCommonData();
            PopulatePermissionWarning();
        }

        uxCurrencySelector.CurrentPaymentName = PaymentName;
        uxCurrencySelector.IsPaymentEnabled = Convert.ToBoolean( uxIsEnabledDrop.SelectedValue );
        uxCurrencySelector.RefreshDropDownList();
        if (!MainContext.IsPostBack)
            uxCurrencySelector.SelectedValue = DataAccessContext.Configurations.GetValue( "PaymentCurrency" );
    }

    protected void uxPaymentImageLinkButton_Click( object sender, EventArgs e )
    {
        uxPaymentImageUpload.ShowControl = true;
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                    uxLanguageControl.CurrentCulture, PaymentName );
                paymentOption = SetUpPaymentOption( paymentOption );
                paymentOption = DataAccessContext.PaymentOptionRepository.Update( paymentOption );

                AdminAdvancedBaseGatewayUserControl gatewayUserControl = GetGatewayUserControl();
                if (gatewayUserControl != null)
                    gatewayUserControl.Save();

                DataAccessContext.ConfigurationRepository.UpdateValue( DataAccessContext.Configurations["PaymentCurrency"], uxCurrencySelector.SelectedValue );

                uxMessage.DisplayMessage( Resources.PaymentMessages.UpdateSuccess );
                PopulatePermissionWarning();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

}
