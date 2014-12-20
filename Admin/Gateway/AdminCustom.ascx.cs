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


public partial class AdminAdvanced_Gateway_AdminCustom : Vevo.AdminAdvancedBaseGatewayUserControl
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
        uxCustomListText.Text = DataAccessContext.Configurations.GetValue( "PaymentByCustomList" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["PaymentByCustomList"],
           uxCustomListText.Text );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }

}
