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
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;


public partial class AdminAdvanced_Gateway_AdmineWay : Vevo.AdminAdvancedBaseGatewayUserControl
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
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "eWay" );
        uxCustomerIDText.Text = DataAccessContext.Configurations.GetValue( "eWayCustomerID" );
        uxUseCvnDrop.SelectedValue = paymentOption.Cvv2Required.ToString();
        uxTestModeDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "eWayTest" );
        uxTestErrorCodeText.Text = DataAccessContext.Configurations.GetValue( "eWayErrorCode" );
    }

    public override void Save()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, "eWay" );
        paymentOption.Cvv2Required = ConvertUtilities.ToBoolean( uxUseCvnDrop.SelectedValue );
        DataAccessContext.PaymentOptionRepository.Update( paymentOption );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eWayCustomerID"],
            uxCustomerIDText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eWayTest"],
            uxTestModeDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["eWayErrorCode"],
            uxTestErrorCodeText.Text );

        SystemConfig.Load();

        uxStoreSelect.UpdateStoreList();
    }

}
