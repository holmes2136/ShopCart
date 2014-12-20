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

public partial class AdminAdvanced_MainControls_ShippingTracking : AdminAdvancedBaseUserControl
{
    private void PopulateControls()
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
        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
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
            uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
