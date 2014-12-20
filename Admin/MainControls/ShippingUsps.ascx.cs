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
using Vevo.Domain.Shipping.Usps;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_ShippingUsps : AdminAdvancedBaseUserControl
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

    private void PopulateServiceList()
    {
        uxServiceFreeShippingCheckList.DataSource = UspsShippingGateway.ReadServiceFile();
        uxServiceFreeShippingCheckList.DataValueField = "Code";
        uxServiceFreeShippingCheckList.DataTextField = "Name";
        uxServiceFreeShippingCheckList.DataBind();

        foreach (ListItem list in uxServiceFreeShippingCheckList.Items)
        {
            list.Text = Server.HtmlDecode( list.Text );
        }

        SelectCheckboxList(
            uxServiceFreeShippingCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUspsServiceFreeShipping" ) );

        uxServiceEnabledCheckList.DataSource = UspsShippingGateway.ReadServiceFile();
        uxServiceEnabledCheckList.DataValueField = "Code";
        uxServiceEnabledCheckList.DataTextField = "Name";
        uxServiceEnabledCheckList.DataBind();

        foreach (ListItem list in uxServiceEnabledCheckList.Items)
        {
            list.Text = Server.HtmlDecode( list.Text );
        }

        SelectCheckboxList(
            uxServiceEnabledCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUspsServiceEnabledList" ) );

        uxUspsAllowedSetFreeCheckList.DataSource = UspsShippingGateway.ReadServiceFile();
        uxUspsAllowedSetFreeCheckList.DataValueField = "Code";
        uxUspsAllowedSetFreeCheckList.DataTextField = "Name";
        uxUspsAllowedSetFreeCheckList.DataBind();

        foreach (ListItem list in uxUspsAllowedSetFreeCheckList.Items)
        {
            list.Text = Server.HtmlDecode( list.Text );
        }

        SelectCheckboxList(
            uxUspsAllowedSetFreeCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUspsServiceAllowedSetFree" ) );

        uxUspsAllowedUseFreeCouponCheckList.DataSource = UspsShippingGateway.ReadServiceFile();
        uxUspsAllowedUseFreeCouponCheckList.DataValueField = "Code";
        uxUspsAllowedUseFreeCouponCheckList.DataTextField = "Name";
        uxUspsAllowedUseFreeCouponCheckList.DataBind();

        foreach (ListItem list in uxUspsAllowedUseFreeCouponCheckList.Items)
        {
            list.Text = Server.HtmlDecode( list.Text );
        }

        SelectCheckboxList(
            uxUspsAllowedUseFreeCouponCheckList,
            DataAccessContext.Configurations.GetValueList( "RTShippingUspsServiceAllowedUseCouponFree" ) );

    }

    private void PopulateControl()
    {
        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( AdminUtilities.CurrentCulture, ShippingID );
        uxIsEnabledDrop.SelectedValue = shippingMethod.IsEnabled.ToString();
        uxUserIDText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUspsUserID" );
        uxUspsMerchantZipText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUspsMerchantZip" );
        //uxUrlDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUspsUrl" );
        uxMailTypeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUspsMailType" );
        uxIsFreeShippingCostDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUspsIsFreeShipping" );
        uxFreeShippingCostText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUspsFreeShippingCost" );
        uxIsMinWeightDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "RTShippingUspsIsMinWeight" );
        uxMinWeightOrderText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUspsMinWeightOrder" );
        uxMarkUpPriceText.Text = DataAccessContext.Configurations.GetValue( "RTShippingUspsMarkUpPrice" );
        uxHandlingFeeText.Text = string.Format( "{0:f2}", shippingMethod.HandlingFee );
        PopulateServiceList();
        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
    }

    private void UpdateShippingMethod()
    {
        ShippingOption shippingMethod = DataAccessContext.ShippingOptionRepository.GetOne( StoreContext.Culture, ShippingID );
        shippingMethod.IsEnabled = ConvertUtilities.ToBoolean( uxIsEnabledDrop.SelectedValue );
        shippingMethod.HandlingFee = ConvertUtilities.ToDecimal( uxHandlingFeeText.Text );
        DataAccessContext.ShippingOptionRepository.Save( shippingMethod );
    }

    private void UpdateValidationControl()
    {
        if (uxIsEnabledDrop.SelectedValue.Equals( "False" ))
        {
            uxUserIDRequired.Enabled = false;
            uxMerchantZipRequired.Enabled = false;
            uxValidationSummary.Enabled = false;
        }
        else
        {
            uxUserIDRequired.Enabled = true;
            uxMerchantZipRequired.Enabled = true;
            uxValidationSummary.Enabled = true;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControl();
        if (!IsAdminModifiable())
        {
            uxSubmitButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        UpdateValidationControl();
    }

    protected void uxSubmitButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                UpdateShippingMethod();

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsUserID"],
                    uxUserIDText.Text );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsMerchantZip"],
                    uxUspsMerchantZipText.Text );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsUrl"],
                     uxUrlDrop.SelectedValue );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsMailType"],
                    uxMailTypeDrop.SelectedValue );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsIsFreeShipping"],
                    uxIsFreeShippingCostDrop.SelectedValue );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsFreeShippingCost"],
                    uxFreeShippingCostText.Text );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsIsMinWeight"],
                    uxIsMinWeightDrop.SelectedValue );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsMinWeightOrder"],
                    uxMinWeightOrderText.Text );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsMarkUpPrice"],
                    uxMarkUpPriceText.Text );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsServiceEnabledList"],
                    GetServiceSelected( uxServiceEnabledCheckList ) );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                     DataAccessContext.Configurations["RTShippingUspsServiceFreeShipping"],
                    GetServiceSelected( uxServiceFreeShippingCheckList ) );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["RTShippingUspsServiceAllowedSetFree"],
                   GetServiceSelected( uxUspsAllowedSetFreeCheckList ) );

                DataAccessContext.ConfigurationRepository.UpdateValue(
                  DataAccessContext.Configurations["RTShippingUspsServiceAllowedUseCouponFree"],
                 GetServiceSelected( uxUspsAllowedUseFreeCouponCheckList ) );

                AdminUtilities.LoadSystemConfig();
                uxMessage.DisplayMessage( Resources.ShippingMessages.UpdateSuccess );
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
