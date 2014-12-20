using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI.International;
using Vevo.WebUI;
using Vevo.Shared.DataAccess;

public partial class MyAccount : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsGiftVisibility()
    {
        return uxGiftCertificateDiv.Visible || uxGiftRegistryDiv.Visible;
    }

    private void PopulateControls()
    {
        uxGiftRegistryDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftRegistryModuleDisplay" ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
        uxGiftCertificateDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );
        uxRmaDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "EnableRMA" );
        uxRewardPointLink.Visible = DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );

        uxGiftPanel.Visible = IsGiftVisibility(); ;
        uxWishlistPanel.Visible = true;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxLoginStatus_LoggedOut( object sender, EventArgs e )
    {
    }
}
