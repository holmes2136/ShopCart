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
using Vevo.DataAccessLib.Cart;

public partial class AdminAdvanced_Components_SiteConfig_EBayConfig : AdminAdvancedBaseUserControl
{
    private void PopulateControls()
    {   
        uxeBayConfigAppIDText.Text = DataAccessContext.Configurations.GetValue( "eBayConfigAppID" ).ToString();
        uxeBayConfigCertIDText.Text = DataAccessContext.Configurations.GetValue( "eBayConfigCertID" ).ToString();
        uxeBayConfigDevIDText.Text = DataAccessContext.Configurations.GetValue( "eBayConfigDevID" ).ToString();
        uxeBayConfigTokenText.Text = DataAccessContext.Configurations.GetValue( "eBayConfigToken" ).ToString();
        uxeBayConfigListingMode.SelectedValue = DataAccessContext.Configurations.GetValue( "eBayConfigListingMode" ).ToString();
    }

    private void DisableForFreeAndLiteLicense()
    {
        uxeBayConfigAppIDText.Visible = false;
        uxeBayConfigCertIDText.Visible = false;
        uxeBayConfigDevIDText.Visible = false;
        uxeBayConfigTokenText.Visible = false;
        uxeBayConfigListingMode.Visible = false;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eBayConfigAppID"],
            uxeBayConfigAppIDText.Text.Trim() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eBayConfigCertID"],
            uxeBayConfigCertIDText.Text.Trim() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eBayConfigDevID"],
            uxeBayConfigDevIDText.Text.Trim() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eBayConfigToken"],
            uxeBayConfigTokenText.Text.Trim() );
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eBayConfigListingMode"],
            uxeBayConfigListingMode.SelectedValue );
    }
}
