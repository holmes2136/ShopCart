using System;
using Vevo.WebUI.BaseControls;
using Vevo.Domain;
using Vevo.Domain.Contents;
using Vevo.WebUI;
using Vevo;
using Vevo.Shared.DataAccess;

public partial class Themes_Default_LayoutControls_ContentFooter : BaseLayoutControl
{
    private bool IsContentMenuEnable(string name)
    {
        bool isEnable = false;
        string contentID = DataAccessContext.ContentRepository.GetContentIDFromUrlName(name);
        if (!contentID.Equals("0") && (contentID != "") )
            isEnable = DataAccessContext.ContentRepository.GetOne(StoreContext.Culture, contentID).IsEnabled;

        return isEnable;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        uxNewsMenu.Visible = DataAccessContext.Configurations.GetBoolValue("NewsModuleDisplay");
        uxBlogMenu.Visible = DataAccessContext.Configurations.GetBoolValue("BlogEnabled");
        uxAffiliateMenu.Visible = DataAccessContext.Configurations.GetBoolValue( "AffiliateEnabled" ) && (KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ));
        uxSitemapMenu.Visible = DataAccessContext.Configurations.GetBoolValue("SiteMapEnabled");
        uxWishListMenu.Visible = DataAccessContext.Configurations.GetBoolValue("WishListEnabled");
        uxGiftCertificateMenu.Visible = DataAccessContext.Configurations.GetBoolValue("GiftCertificateEnabled");
        uxRewardPointMenu.Visible = DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled" ) && (KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ));
        uxComparisonMenu.Visible = DataAccessContext.Configurations.GetBoolValue("CompareListEnabled");
        uxRmaMenu.Visible = DataAccessContext.Configurations.GetBoolValue("EnableRMA");

        uxAboutUsMenu.Visible = IsContentMenuEnable(GetLanguageText("AboutUs").Replace(" ", ""));
        uxFaqsMenu.Visible = IsContentMenuEnable(GetLanguageText("Faqs").Replace(" ", ""));
        uxPolicyMenu.Visible = IsContentMenuEnable(GetLanguageText("Policies").Replace(" ", ""));
    }

}
