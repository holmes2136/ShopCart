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


public partial class AdminAdvanced_Gateway_AdminPayPalPro : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxUserNameText.Text = DataAccessContext.Configurations.GetValue( "PayPalAPIUserName" );
        uxPasswordText.Text = DataAccessContext.Configurations.GetValue( "PayPalAPIPassword" );
        uxSignatureText.Text = DataAccessContext.Configurations.GetValue( "PayPalAPISignature" );
        uxEnvironmentDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "PayPalEnvironment" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["PayPalAPIUserName"],
            uxUserNameText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["PayPalAPIPassword"],
            uxPasswordText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["PayPalAPISignature"],
            uxSignatureText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["PayPalEnvironment"],
            uxEnvironmentDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }

}
