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

public partial class AdminAdvanced_Gateway_AdminPayPalProUK : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxTestAccountDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "PayPalProUKTest" );
        uxUserAccountText.Text = DataAccessContext.Configurations.GetValue( "PayPalProUKUser" );
        uxVendorAccountText.Text = DataAccessContext.Configurations.GetValue( "PayPalProUKVender" );
        uxPartnerText.Text = DataAccessContext.Configurations.GetValue( "PayPalProUKPartner" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "PayPalProUKPassword" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProUKTest"],
            uxTestAccountDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProUKUser"],
            uxUserAccountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProUKVender"],
            uxVendorAccountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProUKPartner"],
            uxPartnerText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProUKPassword"],
            uxPasswordText.Text );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
