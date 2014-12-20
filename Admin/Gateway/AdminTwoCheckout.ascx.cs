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


public partial class AdminAdvanced_Gateway_AdminTwoCheckout : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxMerchantAccountText.Text = DataAccessContext.Configurations.GetValue( "2COMerchantAccount" );
        uxTestModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "2COTestMode" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["2COMerchantAccount"],
            uxMerchantAccountText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["2COTestMode"],
            uxTestModeDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }

}
