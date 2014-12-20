using System;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Shared.DataAccess;

public partial class Components_MyAccountMenuNavList : BaseLanguageUserControl
{
    private bool IsGiftVisibility()
    {
        HtmlTableRow giftCertificateDiv = (HtmlTableRow) uxLoginView.FindControl( "uxGiftCertificateDiv" );
        HtmlTableRow giftRegistryDiv = (HtmlTableRow) uxLoginView.FindControl( "uxGiftRegistryDiv" );
        return giftCertificateDiv.Visible || giftRegistryDiv.Visible;
    }

    private void PopulateControls()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            HtmlTableRow rmaDiv = (HtmlTableRow) uxLoginView.FindControl( "uxRmaDiv" );
            HtmlTableRow rewardPointDiv = (HtmlTableRow) uxLoginView.FindControl( "uxRewardPointDiv" );
            HtmlTableRow comparisionListDiv = (HtmlTableRow) uxLoginView.FindControl( "uxComparisionListDiv" );
            HtmlTableRow giftCertificateDiv = (HtmlTableRow) uxLoginView.FindControl( "uxGiftCertificateDiv" );
            HtmlTableRow giftRegistryDiv = (HtmlTableRow) uxLoginView.FindControl( "uxGiftRegistryDiv" );
            HtmlTableRow wishlistDiv = (HtmlTableRow) uxLoginView.FindControl( "uxWishlistDiv" );
            HtmlTableRow subscriptionDiv = (HtmlTableRow) uxLoginView.FindControl( "uxSubscriptionDiv" );

            rmaDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "EnableRMA" );
            rewardPointDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
            comparisionListDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "CompareListEnabled" );

            giftRegistryDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftRegistryModuleDisplay" ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
            giftCertificateDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );
            wishlistDiv.Visible = true;

            subscriptionDiv.Visible = KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxLoginStatus_LoggedOut( object sender, EventArgs e )
    {
    }
}