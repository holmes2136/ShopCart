<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentFooter.ascx.cs"
    Inherits="Themes_ResponsiveGreen_LayoutControls_ContentFooter" %>
<%@ Register Src="~/Components/PaymentLogo.ascx" TagName="PaymentLogo" TagPrefix="uc2" %>
<%@ Register Src="~/Components/Newsletter.ascx" TagName="Newsletter" TagPrefix="uc4" %>
<div class="LayoutFooter">
    <div class="row">
        <div class="footer-left-col columns">
            <div class="row">
                <div class="four MenuFooter" id="uxFirstMenuDiv" runat="server">
                    <div class="MenuFooterTitleShow">
                        <div class="MenuItemTitleOuter">
                            <div class="MenuItemTitleInner">
                                Information</div>
                        </div>
                        <ul class="MenuItem">
                            <li id="uxAboutUsMenu" runat="server">
                                <asp:HyperLink ID="uxAboutUsLink" runat="server" NavigateUrl="~/aboutus-content.aspx"
                                    CssClass="HyperLink">[$AboutUs]</asp:HyperLink></li>
                            <li id="uxNewsMenu" runat="server">
                                <asp:HyperLink ID="uxNewsLink" runat="server" CssClass="HyperLink">[$News]</asp:HyperLink></li>
                            <li id="uxBlogMenu" runat="server">
                                <asp:HyperLink ID="uxBlogLink" runat="server" NavigateUrl="~/Blog/Default.aspx" CssClass="HyperLink">[$Blog]</asp:HyperLink></li>
                            <li id="uxAffiliateMenu" runat="server">
                                <asp:HyperLink ID="uxAffiliateLink" runat="server" NavigateUrl="~/affiliatedashboard.aspx"
                                    CssClass="HyperLink">[$Affiliate]</asp:HyperLink></li>
                            <li id="uxFaqsMenu" runat="server">
                                <asp:HyperLink ID="uxFAQsLink" runat="server" NavigateUrl="~/FAQs-content.aspx" CssClass="HyperLink">[$Faqs]</asp:HyperLink></li>
                            <li id="uxPolicyMenu" runat="server">
                                <asp:HyperLink ID="uxPolicyLink" runat="server" NavigateUrl="~/policy-content.aspx"
                                    CssClass="HyperLink">[$Policies]</asp:HyperLink></li>
                            <li id="uxSitemapMenu" runat="server">
                                <asp:HyperLink ID="uxSitemapLink" runat="server" NavigateUrl="~/StoreSitemap.aspx"
                                    CssClass="HyperLink">[$Site Map]</asp:HyperLink></li>
                        </ul>
                    </div>
                </div>
                <div class="four MenuFooter" id="uxSecondMenuDiv" runat="server">
                    <div class="MenuFooterTitleShow">
                        <div class="MenuItemTitleOuter">
                            <div class="MenuItemTitleInner">
                                My Account</div>
                        </div>
                        <ul class="MenuItem">
                            <li>
                                <asp:HyperLink ID="uxOrderHistoryLink" runat="server" NavigateUrl="~/orderhistory.aspx"
                                    CssClass="HyperLink">[$Order]</asp:HyperLink></li>
                            <li id="uxWishListMenu" runat="server">
                                <asp:HyperLink ID="uxWishListLink" runat="server" NavigateUrl="~/wishlist.aspx" CssClass="HyperLink">[$WishList]</asp:HyperLink></li>
                            <li id="uxGiftCertificateMenu" runat="server">
                                <asp:HyperLink ID="uxGiftCertificateLink" runat="server" NavigateUrl="~/giftcertificate.aspx"
                                    CssClass="HyperLink">[$GiftCertificate]</asp:HyperLink></li>
                            <li id="uxRewardPointMenu" runat="server">
                                <asp:HyperLink ID="uxRewardPointLink" runat="server" NavigateUrl="~/rewardPoints.aspx"
                                    CssClass="HyperLink">[$RewardPoint]</asp:HyperLink></li>
                            <li id="uxComparisonMenu" runat="server">
                                <asp:HyperLink ID="uxComparisionList" runat="server" NavigateUrl="~/comparisonlist.aspx"
                                    CssClass="HyperLink">[$ComparisionList]</asp:HyperLink></li>
                            <li id="uxSubscription" runat="server">
                                <asp:HyperLink ID="uxSubscriptionLink" runat="server" NavigateUrl="~/contentsubscription.aspx"
                                    CssClass="HyperLink">[$ContentSubscriptions]</asp:HyperLink></li>
                        </ul>
                    </div>
                </div>
                <div class="four MenuFooter CustomerMenu" id="uxThirdMenuDiv" runat="server">
                    <div class="MenuFooterTitleShow">
                        <div class="MenuItemTitleOuter">
                            <div class="MenuItemTitleInner">
                                Customer Service</div>
                        </div>
                        <ul class="MenuItem">
                            <li>
                                <asp:HyperLink ID="uxContactLink" runat="server" NavigateUrl="~/ContactUs.aspx" CssClass="HyperLink">[$ContactUs]</asp:HyperLink></li>
                            <li id="uxRmaMenu" runat="server">
                                <asp:HyperLink ID="uxRmaHistoryLink" runat="server" NavigateUrl="~/rmahistory.aspx"
                                    CssClass="HyperLink">[$Rma]</asp:HyperLink></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer-right-col column paddingright">
            <div class="row">
                <div class="twelve MenuFooter paddingleft">
                    <div class="MenuItemTitle">
                        [$ShareAndConnect]</div>
                    <div class="SocialLink">
                        <asp:HyperLink ID="uxFacebookLink" runat="server" CssClass="FaceBookIcon" Visible="false"
                            ToolTip="[$Facebook]">[$Facebook]</asp:HyperLink>
                        <asp:HyperLink ID="uxGoogleLink" runat="server" CssClass="GoogleIcon"
                            ToolTip="[$Google]">[$Google]</asp:HyperLink>
                        <asp:HyperLink ID="uxEmailLink" runat="server" NavigateUrl="~/ContactUs.aspx" CssClass="EmailIcon"
                            ToolTip="[$Email]">[$Email]</asp:HyperLink>
                        <asp:HyperLink ID="uxBlogShareLink" runat="server" NavigateUrl="~/Blog/Default.aspx" CssClass="BlogIcon"
                            ToolTip="[$BlogIcon]">[$BlogIcon]</asp:HyperLink>
                    </div>
                </div>
                <div class="twelve MenuFooter paddingright">
                    <uc4:Newsletter ID="uxNewsletter" runat="server" />
                    <uc2:PaymentLogo ID="uxPaymentLogo" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>
