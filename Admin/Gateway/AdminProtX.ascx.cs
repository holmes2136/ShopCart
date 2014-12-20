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

public partial class AdminAdvanced_Gateway_AdminProtX : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "ProtXMode" );
        uxVendorNameText.Text = DataAccessContext.Configurations.GetValue( "ProtXVendorName" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProtXMode"],
           uxModeDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["ProtXVendorName"],
           uxVendorNameText.Text );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
