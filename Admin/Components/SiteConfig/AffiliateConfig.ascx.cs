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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;


public partial class AdminAdvanced_Components_SiteConfig_AffiliateConfig : System.Web.UI.UserControl
{
    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return String.Empty;
            else
                return (String) ViewState["ValidationGroup"];
        }
        set { ViewState["ValidationGroup"] = value; }
    }

    public void PopulateControls()
    {
        uxAffiliateEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "AffiliateEnabled" );
        uxAffiliateAutoApproveDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "AffiliateAutoApprove" );
        uxAffiliateReferenceDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "AffiliateReference" );
        uxAffiliateExpirePeriodText.Text = DataAccessContext.Configurations.GetValue( "AffiliateExpirePeriod" );
        uxAffiliateCommissionRateText.Text = DataAccessContext.Configurations.GetValue( "AffiliateCommissionRate" );
        uxAffiliatePaidBalanceText.Text = DataAccessContext.Configurations.GetValue( "AffiliateDefaultPaidBalance" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateEnabled"],
            uxAffiliateEnabledDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateAutoApprove"],
            uxAffiliateAutoApproveDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateReference"],
            uxAffiliateReferenceDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateExpirePeriod"],
            uxAffiliateExpirePeriodText.Text.Trim() );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateCommissionRate"],
            uxAffiliateCommissionRateText.Text.Trim() );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["AffiliateDefaultPaidBalance"],
            uxAffiliatePaidBalanceText.Text.Trim() );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        //uxAffiliateExpirePeriodCompare.ValidationGroup = ValidationGroup;
        uxAffiliateExpirePeriodCompareWithZero.ValidationGroup = ValidationGroup;
        uxAffiliateCommissionRateCompare.ValidationGroup = ValidationGroup;
        uxAffiliatePaidBalanceText.ValidationGroup = ValidationGroup;
        uxAffiliatePaidBalanceCompareWithZero.ValidationGroup = ValidationGroup;
    }
}
