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
using Vevo.DataAccessLib.Cart;

public partial class AdminAdvanced_Components_SiteConfig_ShippingTracking : AdminAdvancedBaseUserControl
{
    public void PopulateControls()
    {
        uxUpsTrackUrlText.Text = DataAccessContext.Configurations.GetValue( "UpsTrackingUrl" ).ToString();
        uxFedExTrackUrlText.Text = DataAccessContext.Configurations.GetValue( "FedExTrackingUrl" ).ToString();
        uxUspsTrackUrlText.Text = DataAccessContext.Configurations.GetValue( "UspsTrackingUrl" ).ToString();
        uxOtherTrackUrlText.Text = DataAccessContext.Configurations.GetValue( "OtherTrackingUrl" ).ToString();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UpsTrackingUrl"],
            uxUpsTrackUrlText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["FedExTrackingUrl"],
            uxFedExTrackUrlText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["UspsTrackingUrl"],
            uxUspsTrackUrlText.Text );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["OtherTrackingUrl"],
            uxOtherTrackUrlText.Text );
    }
}
