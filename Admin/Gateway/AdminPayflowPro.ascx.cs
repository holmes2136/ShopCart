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
using Vevo.Domain;
using Vevo.WebAppLib;

public partial class Admin_Gateway_AdminPayflowPro : Vevo.AdminAdvancedBaseGatewayUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            Refresh();
    }

    public override void Refresh()
    {
        uxTestAccountDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "PayflowProTest" );
        uxUserAccountText.Text = DataAccessContext.Configurations.GetValue( "PayflowProUser" );
        uxVendorAccountText.Text = DataAccessContext.Configurations.GetValue( "PayflowProVendor" );
        uxPartnerText.Text = DataAccessContext.Configurations.GetValue( "PayflowProPartner" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "PayflowProPassword" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayflowProTest"],
            uxTestAccountDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayflowProUser"],
            uxUserAccountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayflowProVendor"],
            uxVendorAccountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayflowProPartner"],
            uxPartnerText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayflowProPassword"],
            uxPasswordText.Text );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
