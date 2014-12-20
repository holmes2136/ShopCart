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
using Vevo.Domain.Payments;
using Vevo.Domain.Configurations;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_Gateway_AdminLinkPoint : Vevo.AdminAdvancedBaseGatewayUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            Refresh();
    }

    protected void uxKeyFileLink_Load( Object sender, EventArgs e )
    {
        LinkButton uxKeyFileLink = (LinkButton) sender;

        string redirectUrl = PaymentAppGateway.GetPaymentAppUrl( "/LinkPoint.aspx", UrlPath.StorefrontUrl );

        string script = String.Format( "window.open( '{0}', 'mywin' ); return false;", redirectUrl );

        uxKeyFileLink.Attributes.Add( "onclick", script );
    }

    public override void Refresh()
    {
        uxStoreNumberText.Text = DataAccessContext.Configurations.GetValue( "LinkPointStoreNumber" );
        uxHostText.Text = DataAccessContext.Configurations.GetValue( "LinkPointHost" );
        uxPortText.Text = DataAccessContext.Configurations.GetValue( "LinkPointPort" );
        uxModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "LinkPointMode" );
    }

    public override void Save()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LinkPointStoreNumber"],
            uxStoreNumberText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LinkPointHost"],
            uxHostText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LinkPointPort"],
            uxPortText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LinkPointMode"],
            uxModeDrop.SelectedValue );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }
}
