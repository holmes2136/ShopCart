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

public partial class AdminAdvanced_Gateway_AdminQBMS : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxAppIDText.Text = DataAccessContext.Configurations.GetValue( "QbmsAppID" );
        uxAppLoginText.Text = DataAccessContext.Configurations.GetValue( "QbmsAppLogin" );
        uxConnectionTicketText.Text = DataAccessContext.Configurations.GetValue( "QbmsConnectionTicket" );
        uxQbmsTestDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "QbmsTest" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["QbmsAppID"],
            uxAppIDText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["QbmsAppLogin"],
            uxAppLoginText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["QbmsConnectionTicket"],
            uxConnectionTicketText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["QbmsTest"],
            uxQbmsTestDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
