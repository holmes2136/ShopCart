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

public partial class AdminAdvanced_Gateway_AdminPayPalProExpress : Vevo.AdminAdvancedBaseGatewayUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            Refresh();
        }
    }


    public override void Refresh()
    {
        uxUserNameText.Text = DataAccessContext.Configurations.GetValue( "PayPalProExpressUser" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "PayPalProExpressPassword" );
        uxSignatureText.Text = DataAccessContext.Configurations.GetValue( "PayPalProExpressSignature" );
        uxEnvironmentDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "PayPalProExpressEnvironment" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProExpressUser"],
            uxUserNameText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProExpressPassword"],
            uxPasswordText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProExpressSignature"],
            uxSignatureText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PayPalProExpressEnvironment"],
            uxEnvironmentDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
