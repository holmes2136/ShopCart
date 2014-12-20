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

public partial class AdminAdvanced_Gateway_AdminMoneris : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxMonerisModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "MonerisTestMode" );
        uxMonerisStoreIDText.Text = DataAccessContext.Configurations.GetValue( "MonerisStoreID" );
        uxAPITokenText.Text = DataAccessContext.Configurations.GetValue( "MonerisAPI_Token" );
        uxMonerisUseCVDDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "MonerisUseCVD" );
        uxMonerisCryptDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "MonerisCrypt" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MonerisTestMode"],
            uxMonerisModeDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MonerisStoreID"],
            uxMonerisStoreIDText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MonerisAPI_Token"],
            uxAPITokenText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MonerisUseCVD"],
            uxMonerisUseCVDDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["MonerisCrypt"],
            uxMonerisCryptDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
