using System;
using System.Text;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Blogs;
using Vevo.Shared.DataAccess;
using Vevo.Shared.WebUI;
using Vevo.WebUI;
using Vevo.WebUI.BaseControls;

public partial class Themes_ResponsiveGreen_LayoutControls_ContentFooter : BaseLayoutControl
{
    private string _blogCategoryNewsID = "1";

    private bool IsContentMenuEnable(string name)
    {
        bool isEnable = false;
        string contentID = DataAccessContext.ContentRepository.GetContentIDFromUrlName(name);
        if (!contentID.Equals("0") && (contentID != ""))
            isEnable = DataAccessContext.ContentRepository.GetOne(StoreContext.Culture, contentID).IsEnabled;

        return isEnable;
    }

    private void RegisterJavaScript()
    {
        String csname = "SetupToggleTitle";
        ClientScriptManager cs = Page.ClientScript;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("$(document).ready(function () {");
        sb.AppendLine(" var myWidth = $(window).width();");
        sb.AppendLine(" $('.MenuItem').show();");
        sb.AppendLine(" if (myWidth < 768) {");
        sb.AppendLine("    $('.MenuItem').show();");
        sb.AppendLine("    $('.MenuItemTitleOuter').click(function () {");
        sb.AppendLine("         $(this).toggleClass('MenuItemTitleOuterHide');");
        sb.AppendLine("         $(this).next('.MenuItem').slideToggle('slow');");
        sb.AppendLine("    });");
        sb.AppendLine(" }");
        sb.AppendLine("});");

        if (!cs.IsClientScriptBlockRegistered(this.GetType(), csname))
        {
            cs.RegisterClientScriptBlock(this.GetType(), csname, sb.ToString(), true);
        }
    }

    private string GetFanpageURL()
    {
        return DataAccessContext.Configurations.GetValue("WidgetLikeBoxParameterValue", StoreContext.CurrentStore);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterJavaScript();

        uxNewsMenu.Visible = DataAccessContext.Configurations.GetBoolValue("NewsModuleDisplay");
        uxBlogMenu.Visible = DataAccessContext.Configurations.GetBoolValue("BlogEnabled");
        uxAffiliateMenu.Visible = DataAccessContext.Configurations.GetBoolValue("AffiliateEnabled") && (KeyUtilities.IsDeluxeLicense(DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName));
        uxSitemapMenu.Visible = DataAccessContext.Configurations.GetBoolValue("SiteMapEnabled");
        uxWishListMenu.Visible = DataAccessContext.Configurations.GetBoolValue("WishListEnabled");
        uxGiftCertificateMenu.Visible = DataAccessContext.Configurations.GetBoolValue("GiftCertificateEnabled");
        uxRewardPointMenu.Visible = DataAccessContext.Configurations.GetBoolValue("PointSystemEnabled") && (KeyUtilities.IsDeluxeLicense(DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName));
        uxComparisonMenu.Visible = DataAccessContext.Configurations.GetBoolValue("CompareListEnabled");
        uxRmaMenu.Visible = DataAccessContext.Configurations.GetBoolValue("EnableRMA");
        uxSubscription.Visible = KeyUtilities.IsDeluxeLicense(DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName);

        uxAboutUsMenu.Visible = IsContentMenuEnable(GetLanguageText("AboutUs").Replace(" ", ""));
        uxFaqsMenu.Visible = IsContentMenuEnable(GetLanguageText("Faqs").Replace(" ", ""));
        uxPolicyMenu.Visible = IsContentMenuEnable(GetLanguageText("Policies").Replace(" ", ""));

        uxBlogShareLink.Visible = DataAccessContext.Configurations.GetBoolValue("BlogEnabled");
        uxGoogleLink.NavigateUrl = "https://plus.google.com/share?url=" + UrlPath.StorefrontUrl;

        if (!String.IsNullOrEmpty(GetFanpageURL()))
        {
            uxFacebookLink.NavigateUrl = GetFanpageURL();
            uxFacebookLink.Visible = true;
        }

        BlogCategory blogCategoryNews = DataAccessContext.BlogCategoryRepository.GetOne(StoreContext.Culture, _blogCategoryNewsID);
        uxNewsLink.NavigateUrl = UrlManager.GetBlogCategoryUrl(blogCategoryNews.UrlName);
    }

}
