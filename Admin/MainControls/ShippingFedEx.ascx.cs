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
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.FedEx;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ShippingFedEx : AdminAdvancedBaseUserControl
{
    private string ShippingID
    {
        get
        {
            if (String.IsNullOrEmpty( MainContext.QueryString["ShippingID"] ))
                return "0";
            else
                return MainContext.QueryString["ShippingID"];
        }
    }

    private void SelectCheckboxList( CheckBoxList checkList, string[] serviceList )
    {
        foreach (string service in serviceList)
        {
            foreach (ListItem item in checkList.Items)
            {
                if (item.Value == service)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
    }

    private void PopulateServiceList()
    {
        uxFedExServiceCheckList.DataSource = FedExShippingGateway.ReadServiceFile();
        uxFedExServiceCheckList.DataValueField = "Code";
        uxFedExServiceCheckList.DataTextField = "Name";
        uxFedExServiceCheckList.DataBind();

        SelectCheckboxList(
            uxFedExServiceCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingFedExServiceEnabled" ) );


        uxFreeShippingServiceCheckList.DataSource = FedExShippingGateway.ReadServiceFile();
        uxFreeShippingServiceCheckList.DataValueField = "Code";
        uxFreeShippingServiceCheckList.DataTextField = "Name";
        uxFreeShippingServiceCheckList.DataBind();

        SelectCheckboxList(
            uxFreeShippingServiceCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingFedExFreeShippingCostService" ) );

        uxFedExAllowedSetFreeCheckList.DataSource = FedExShippingGateway.ReadServiceFile();
        uxFedExAllowedSetFreeCheckList.DataValueField = "Code";
        uxFedExAllowedSetFreeCheckList.DataTextField = "Name";
        uxFedExAllowedSetFreeCheckList.DataBind();

        SelectCheckboxList(
            uxFedExAllowedSetFreeCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingFedExServiceAllowedSetFree" ) );

        uxFedExAllowedUseFreeCouponCheckList.DataSource = FedExShippingGateway.ReadServiceFile();
        uxFedExAllowedUseFreeCouponCheckList.DataValueField = "Code";
        uxFedExAllowedUseFreeCouponCheckList.DataTextField = "Name";
        uxFedExAllowedUseFreeCouponCheckList.DataBind();

        SelectCheckboxList(
            uxFedExAllowedUseFreeCouponCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingFedExServiceAllowedUseCouponFree" ) );
    }

    private void PopulateControls()
    {
        PopulateServiceList();

        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( AdminUtilities.CurrentCulture, ShippingID );
        uxIsEnabledDrop.SelectedValue = shippingMethod.IsEnabled.ToString();
        uxMerchantZipText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExMerchantZip" );
        uxCountryList.CurrentSelected = DataAccessContext.Configurations.GetValue( "RTShippingFedExMerchantCountry" );
        uxStateList.CountryCode = uxCountryList.CurrentSelected;
        uxStateList.Refresh();
        uxStateList.CurrentSelected = DataAccessContext.Configurations.GetValue( "RTShippingFedExMerchantState" );
        uxKeyText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExKey" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExPassword" );
        uxAccountNumberText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExAccountNumber" );
        uxMeterNumberText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExMeterNumber" );
        uxFedExTestModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExUrl" );
        uxRateRequestDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExRateRequest" );
        uxInsuranceValueDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExIncludeInsurance" );
        uxCustomsDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExCustoms" );
        uxFreeShippingDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExFreeShipping" );
        uxFreeShippingCostText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExFreeShippingCost" );
        uxDropoffTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExDropoffType" );
        uxPickupTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExPackingType" );
        uxMinimumWeightDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingFedExUseMinOrderWeight" );
        uxMinimumWeightText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExMinOrderWeight" );
        uxMarkupText.Text = DataAccessContext.Configurations.GetValue( "RTShippingFedExMarkup" );
        uxHandlingFeeText.Text = string.Format( "{0:f2}", shippingMethod.HandlingFee );
        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
    }

    private void UpdateValidationControl()
    {
        if (uxIsEnabledDrop.SelectedValue.Equals( "False" ))
        {
            uxMerchantZipRequiredValidator.Enabled = false;
            uxKeyRequiredValidator.Enabled = false;
            uxPasswordRequiredValidator.Enabled = false;
            uxAccountNumberRequiredValidator.Enabled = false;
            uxMeterNumberRequiredValidator.Enabled = false;
            uxValidationSummary.Enabled = false;
        }
        else
        {
            uxMerchantZipRequiredValidator.Enabled = true;
            uxKeyRequiredValidator.Enabled = true;
            uxPasswordRequiredValidator.Enabled = true;
            uxAccountNumberRequiredValidator.Enabled = true;
            uxMeterNumberRequiredValidator.Enabled = true;
            uxValidationSummary.Enabled = true;
        }
    }

    private string GetServiceSelected( CheckBoxList checkList )
    {
        ArrayList arService = new ArrayList();
        foreach (ListItem item in checkList.Items)
        {
            if (item.Selected)
                arService.Add( item.Value );
        }
        string[] result = new string[arService.Count];
        arService.CopyTo( result );
        return String.Join( ",", result );
    }

    private bool VerifyStateCountry( out string message )
    {
        message = String.Empty;

        if (uxIsEnabledDrop.SelectedValue.Equals( "False" ))
            return true;

        if (String.IsNullOrEmpty( uxStateList.CurrentSelected ))
            message = Resources.ShippingMessages.UpdateErrorNoState;
        else if (String.IsNullOrEmpty( uxCountryList.CurrentSelected ))
            message = Resources.ShippingMessages.UpdateErrorNoCountry;

        if (String.IsNullOrEmpty( message ))
            return true;
        else
            return false;
    }

    private void uxState_RefreshHandler( object sender, EventArgs e )
    {
        uxStateList.CountryCode = uxCountryList.CurrentSelected;
        uxStateList.Refresh();
    }

    private void UpdateShippingMethod()
    {
        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( StoreContext.Culture, ShippingID );
        shippingMethod.IsEnabled = ConvertUtilities.ToBoolean( uxIsEnabledDrop.SelectedValue );
        shippingMethod.HandlingFee = ConvertUtilities.ToDecimal( uxHandlingFeeText.Text );
        DataAccessContext.ShippingOptionRepository.Save( shippingMethod );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCountryList.BubbleEvent += new EventHandler( uxState_RefreshHandler );

        if (!IsAdminModifiable())
        {
            uxUpdateConfigButton.Visible = false;
        }
        if (!MainContext.IsPostBack)
            PopulateControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        UpdateValidationControl();
    }

    protected void uxUpdateConfigButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                string message;
                if (VerifyStateCountry( out message ))
                {
                    UpdateShippingMethod();

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMerchantZip"],
                        uxMerchantZipText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMerchantState"],
                        uxStateList.CurrentSelected );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMerchantCountry"],
                        uxCountryList.CurrentSelected );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExKey"],
                         uxKeyText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExPassword"],
                        uxPasswordText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExAccountNumber"],
                        uxAccountNumberText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMeterNumber"],
                        uxMeterNumberText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExUrl"],
                        uxFedExTestModeDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExRateRequest"],
                        uxRateRequestDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExIncludeInsurance"],
                        uxInsuranceValueDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExCustoms"],
                        uxCustomsDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExServiceEnabled"],
                        GetServiceSelected( uxFedExServiceCheckList ) );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExFreeShippingCostService"],
                        GetServiceSelected( uxFreeShippingServiceCheckList ) );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExFreeShipping"],
                        uxFreeShippingDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExFreeShippingCost"],
                        Currency.ConvertPriceToUSFormat( uxFreeShippingCostText.Text ) );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExDropoffType"],
                        uxDropoffTypeDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExPackingType"],
                        uxPickupTypeDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExUseMinOrderWeight"],
                        uxMinimumWeightDrop.SelectedValue );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMinOrderWeight"],
                        uxMinimumWeightText.Text );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                         DataAccessContext.Configurations["RTShippingFedExMarkup"],
                        Currency.ConvertPriceToUSFormat( uxMarkupText.Text ) );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                       DataAccessContext.Configurations["RTShippingFedExServiceAllowedSetFree"],
                       GetServiceSelected( uxFedExAllowedSetFreeCheckList ) );

                    DataAccessContext.ConfigurationRepository.UpdateValue(
                       DataAccessContext.Configurations["RTShippingFedExServiceAllowedUseCouponFree"],
                       GetServiceSelected( uxFedExAllowedUseFreeCouponCheckList ) );

                    AdminUtilities.LoadSystemConfig();
                    uxMessage.DisplayMessage( Resources.ShippingMessages.UpdateSuccess );

                    PopulateControls();
                }
                else
                {
                    uxMessage.DisplayError( message );
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxIsEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateValidationControl();
    }
}
