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
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

public partial class AdminAdvanced_Components_SiteConfig_GiftCertificate : System.Web.UI.UserControl
{
    private void GiftCertificateEnabled()
    {
        uxOnlyProductDiv.Visible = ConvertUtilities.ToBoolean( uxGiftCertificateEnabledDrop.SelectedValue );
    }

    public void PopulateControls()
    {
        uxGiftCertificateEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "GiftCertificateEnabled" );
        uxGiftCertificateEnabledHelp.ConfigName = "GiftCertificateEnabled";
        uxOnlyProductDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "GiftRedeemProductOnly" );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GiftCertificateEnabled"],
            uxGiftCertificateEnabledDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["GiftRedeemProductOnly"],
            uxOnlyProductDrop.SelectedValue );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxGiftCertificateEnabledHelp.ConfigName = "GiftCertificateEnabled";
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        GiftCertificateEnabled();
    }
}
