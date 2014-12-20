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
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_SiteConfig_GoogleAnalytics : AdminAdvancedBaseUserControl
{
    public void PopulateControls(Store store)
    {
        string googleEnable = DataAccessContext.Configurations.GetValue( "GoogleAnalyticsEnabled", store );
        if (string.IsNullOrEmpty( googleEnable )) googleEnable = "False";
        uxGoogleAnalyticsEnabledDrop.SelectedValue = googleEnable;
        uxGoogleAnalyticsAccountText.Text = DataAccessContext.Configurations.GetValue( "GoogleAnalyticsAccount", store ).ToString();
        string customCodeEnable = DataAccessContext.Configurations.GetValue( "GoogleAnalyticsCustomCodeEnabled", store );
        if (string.IsNullOrEmpty( customCodeEnable )) customCodeEnable = "False";
        uxGoogleAnalyticsCustomCodeEnabledDrop.SelectedValue = customCodeEnable;
        uxGoogleAnalyticsCustomCodeText.Text = DataAccessContext.Configurations.GetValue( "GoogleAnalyticsCustomCode", store ).ToString();

        UpdateCustomCodePanel();
        UpdateGoogleAnalyticPanel();
    }

    public void Update( Store store )
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GoogleAnalyticsEnabled"],
            uxGoogleAnalyticsEnabledDrop.SelectedValue, store );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GoogleAnalyticsAccount"],
            uxGoogleAnalyticsAccountText.Text, store );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GoogleAnalyticsCustomCodeEnabled"],
            uxGoogleAnalyticsCustomCodeEnabledDrop.SelectedValue, store );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GoogleAnalyticsCustomCode"],
            uxGoogleAnalyticsCustomCodeText.Text, store );
    }

    private void UpdateCustomCodePanel()
    {
        if (uxGoogleAnalyticsCustomCodeEnabledDrop.SelectedValue == "True")
        {
            uxGoogleAnalyticsAccount.Visible = false;
            uxGoogleAnalyticsCustomCode.Visible = true;
        }
        else
        {
            uxGoogleAnalyticsAccount.Visible = true;
            uxGoogleAnalyticsCustomCode.Visible = false;
        }
    }

    private void UpdateGoogleAnalyticPanel()
    {
        if (uxGoogleAnalyticsEnabledDrop.SelectedValue == "True")
        {
            uxGoogleAnalyticsAccountTR.Visible = true;
        }
        else
        {
            uxGoogleAnalyticsAccountTR.Visible = false;
            uxGoogleAnalyticsAccount.Visible = false;
            uxGoogleAnalyticsCustomCode.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxGoogleAnalyticsEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateCustomCodePanel();
        UpdateGoogleAnalyticPanel();
    }

    protected void uxGoogleAnalyticsCustomCodeEnabledDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        UpdateCustomCodePanel();
    }
}
