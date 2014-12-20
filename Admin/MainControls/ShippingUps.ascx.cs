using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.Ups;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ShippingUps : AdminAdvancedBaseUserControl
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

    private void PopulateContorls()
    {
        // Ups Configuration
        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( AdminUtilities.CurrentCulture, ShippingID );
        uxIsEnabledDrop.SelectedValue = shippingMethod.IsEnabled.ToString();
        uxMerchantZipText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsMerchantZip" );
        uxCountryList.CurrentSelected = DataAccessContext.Configurations.GetValue( "RTShippingUpsMerchantCountry" );
        uxUserIDText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsUserID" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsPassword" );
        uxAccessLicenseNumberText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsAccessLicenseNumber" );
        uxUpsTestModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUpsTestMode" );
        PopulateServiceUps();

        uxUpsFreeShippingDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUpsFreeShippingEnabled" );
        uxUpsFreeShippingCostText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsFreeShippingCost" );

        uxPickupTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUpsPickupType" );
        uxMinPackageDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUpsUseMinOrderWeight" );
        uxMinOrderWieghtText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsMinOrderWeight" );
        uxMarkupText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsMarkup" );
        uxHandlingFeeText.Text = string.Format( "{0:f2}", shippingMethod.HandlingFee );
        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );

        /*FOR NEGOTIATED RATES*/
        uxNegotiatedRatesIndicatorDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUpsServiceNegotiatedRatesIndicator" );
        uxShipperNumberText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUpsServiceShipperNumber" );
        uxShipperNumberDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "RTShippingUpsServiceNegotiatedRatesIndicator" );
    }

    private void SelectCheckboxList( CheckBoxList checkList, string[] serviceList )
    {
        foreach (string serviceCode in serviceList)
        {
            foreach (ListItem item in checkList.Items)
            {
                if (item.Text == UpsShippingGateway.GetServiceByServiceCode( serviceCode ))
                {
                    item.Selected = true;
                    break;
                }
            }
        }
    }

    private void PopulateServiceUps()
    {
        DataTable serviceList = UpsShippingGateway.ReadServiceFile(
            HttpContext.Current.Server.MapPath( UpsShippingGateway.UpsServiceListFilePath ) );

        uxUPSServiceCheckList.DataSource = serviceList;
        uxUPSServiceCheckList.DataValueField = "Code";
        uxUPSServiceCheckList.DataTextField = "Name";
        uxUPSServiceCheckList.DataBind();

        SelectCheckboxList(
            uxUPSServiceCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUpsService" ) );

        uxUpsFreeShippingServiceCheckList.DataSource = serviceList;
        uxUpsFreeShippingServiceCheckList.DataValueField = "Code";
        uxUpsFreeShippingServiceCheckList.DataTextField = "Name";
        uxUpsFreeShippingServiceCheckList.DataBind();

        SelectCheckboxList(
            uxUpsFreeShippingServiceCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUpsFreeShippingService" ) );


        uxUpsAllowedSetFreeCheckList.DataSource = serviceList;
        uxUpsAllowedSetFreeCheckList.DataValueField = "Code";
        uxUpsAllowedSetFreeCheckList.DataTextField = "Name";
        uxUpsAllowedSetFreeCheckList.DataBind();

        SelectCheckboxList(
           uxUpsAllowedSetFreeCheckList,
           DataAccessContext.Configurations.GetValueList( "RTShippingUpsServiceAllowedSetFree" ) );

        uxUpsAllowedUseFreeCouponCheckList.DataSource = serviceList;
        uxUpsAllowedUseFreeCouponCheckList.DataValueField = "Code";
        uxUpsAllowedUseFreeCouponCheckList.DataTextField = "Name";
        uxUpsAllowedUseFreeCouponCheckList.DataBind();

        SelectCheckboxList(
           uxUpsAllowedUseFreeCouponCheckList,
           DataAccessContext.Configurations.GetValueList( "RTShippingUpsServiceAllowedUseCouponFree" ) );
    }

    private string GetServiceUpsSelected( CheckBoxList checkList )
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

    private void UpdateShippingMethod()
    {
        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( StoreContext.Culture, ShippingID );
        shippingMethod.IsEnabled = ConvertUtilities.ToBoolean( uxIsEnabledDrop.SelectedValue );
        shippingMethod.HandlingFee = ConvertUtilities.ToDecimal( uxHandlingFeeText.Text );
        DataAccessContext.ShippingOptionRepository.Save( shippingMethod );
    }

    private void UpdateUpsShipping()
    {
        UpdateShippingMethod();

        // Need to keep configurations temporary. Otherwise, the configurations in 
        // DataAccessContext.Configurations
        // will be reloaded from database everytime UpdateValue is called.
        ConfigurationCollection configurations = DataAccessContext.Configurations;

        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsMerchantZip"], uxMerchantZipText.Text );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsMerchantCountry"], uxCountryList.CurrentSelected );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsUserID"], uxUserIDText.Text );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsPassword"], uxPasswordText.Text );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsAccessLicenseNumber"], uxAccessLicenseNumberText.Text );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsTestMode"], uxUpsTestModeDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsService"], GetServiceUpsSelected( uxUPSServiceCheckList ) );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsFreeShippingEnabled"],
            uxUpsFreeShippingDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsFreeShippingCost"],
            Currency.ConvertPriceToUSFormat( uxUpsFreeShippingCostText.Text ) );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsFreeShippingService"],
            GetServiceUpsSelected( uxUpsFreeShippingServiceCheckList ) );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsPickupType"], uxPickupTypeDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsUseMinOrderWeight"], uxMinPackageDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsMinOrderWeight"], uxMinOrderWieghtText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsMarkup"],
            Currency.ConvertPriceToUSFormat( uxMarkupText.Text ) );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            configurations["RTShippingUpsServiceAllowedSetFree"],
            GetServiceUpsSelected( uxUpsAllowedSetFreeCheckList ) );

        DataAccessContext.ConfigurationRepository.UpdateValue(
           configurations["RTShippingUpsServiceAllowedUseCouponFree"],
           GetServiceUpsSelected( uxUpsAllowedUseFreeCouponCheckList ) );

        /*FOR NEGOTIATED RATES*/
        DataAccessContext.ConfigurationRepository.UpdateValue(
           configurations["RTShippingUpsServiceNegotiatedRatesIndicator"],
           uxNegotiatedRatesIndicatorDrop.SelectedValue );
        DataAccessContext.ConfigurationRepository.UpdateValue(
           configurations["RTShippingUpsServiceShipperNumber"],
           uxShipperNumberText.Text );
    }

    private void UpdateValidationControl()
    {
        if (uxIsEnabledDrop.SelectedValue.Equals( "False" ))
        {
            uxMerchantZipRequiredValidator.Enabled = false;
            uxUserIDRequiredValidator.Enabled = false;
            uxPasswordRequiredValidator.Enabled = false;
            uxAccessLicenseRequiredValidator.Enabled = false;
            uxValidationSummary.Enabled = false;
            uxShipperNumberRequiredValidator.Enabled = false;
        }
        else
        {
            uxMerchantZipRequiredValidator.Enabled = true;
            uxUserIDRequiredValidator.Enabled = true;
            uxPasswordRequiredValidator.Enabled = true;
            uxAccessLicenseRequiredValidator.Enabled = true;
            uxValidationSummary.Enabled = true;
            uxShipperNumberRequiredValidator.Enabled = true;
        }
    }

    private bool VerifyCountry( out string message )
    {
        if (uxIsEnabledDrop.SelectedValue.Equals( "True" ) && String.IsNullOrEmpty( uxCountryList.CurrentSelected ))
        {
            message = Resources.ShippingMessages.UpdateErrorNoCountry;
            return false;
        }
        else
        {
            message = String.Empty;
            return true;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateContorls();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            uxUpdateConfigButton.Visible = false;
        }

        UpdateValidationControl();
    }

    protected void uxUpdateConfigButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                string message;
                if (VerifyCountry( out message ))
                {
                    UpdateUpsShipping();

                    AdminUtilities.LoadSystemConfig();

                    uxMessage.DisplayMessage( Resources.ShippingMessages.UpdateSuccess );
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

    protected void uxNegotiatedRatesIndicatorDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxShipperNumberDiv.Visible = ConvertUtilities.ToBoolean( uxNegotiatedRatesIndicatorDrop.SelectedValue );
    }
}
