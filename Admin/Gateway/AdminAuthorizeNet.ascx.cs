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


public partial class AdminAdvanced_Gateway_AdminAuthorizeNet : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxLoginIDText.Text = DataAccessContext.Configurations.GetValue( "AuthorizeLoginID" );
        uxTransactionKeyText.Text = DataAccessContext.Configurations.GetValue( "AuthorizeTransectionKey" );
        uxTestModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "AuthorizeTestMode" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["AuthorizeLoginID"],
            uxLoginIDText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["AuthorizeTransectionKey"],
            uxTransactionKeyText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["AuthorizeTestMode"],
            uxTestModeDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
