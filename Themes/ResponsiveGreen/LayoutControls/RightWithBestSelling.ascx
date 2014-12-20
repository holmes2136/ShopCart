<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightWithBestSelling.ascx.cs"
    Inherits="Themes_ResponsiveGreen_LayoutControls_Right" %>
<%@ Register Src="~/Components/ProductSpecial.ascx" TagName="ProductSpecial" TagPrefix="uc1" %>
<%@ Register Src="~/Components/VerifyCoupon.ascx" TagName="VerifyCoupon" TagPrefix="uc2" %>
<%@ Register Src="~/Components/ContentMenuNavList.ascx" TagName="ContentMenuNavList"
    TagPrefix="uc3" %>
<%@ Register Src="~/Components/FindGiftRegistry.ascx" TagName="FindGiftRegistry"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/FeaturedMerchants.ascx" TagName="FeaturedMerchants"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/LikeBox.ascx" TagName="LikeBox" TagPrefix="uc7" %>
<%@ Register Src="~/Components/ShoppingCartDetails.ascx" TagName="ShoppingCart" TagPrefix="uc8" %>
<%@ Register Src="../Components/RecentlyViewProduct.ascx" TagName="RecentlyViewed"
    TagPrefix="uc3" %>

<%@ Register Src="~/Components/SecureShoppingAd.ascx" TagName="SecureShoppingAd"
    TagPrefix="uc8" %>
<%@ Register Src="~/Components/ComparisonListBox.ascx" TagName="CompararionListBox"
    TagPrefix="uc4" %>
<uc1:ProductSpecial ID="ProductSpecial" runat="server" />
<asp:UpdatePanel ID="uxMiniCartUpdatePanel" runat="server">
    <ContentTemplate>
        <uc8:ShoppingCart ID="uxShoppingCart" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="uxRecentlyViewedUpdatePanel" runat="server">
    <ContentTemplate>
        <uc3:RecentlyViewed ID="RecentlyViewed" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="uxCompararionUpdatePanel" runat="server">
    <ContentTemplate>
        <uc4:CompararionListBox ID="uxCompararionListBox" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<uc2:VerifyCoupon ID="uxVerifyCoupon" runat="server" />
<uc3:ContentMenuNavList ID="uxContentMenuNavList" runat="server" />
<uc8:SecureShoppingAd ID="uxSecureShoppingAd" runat="server" />
<uc5:FindGiftRegistry ID="uxFindGiftRegistry" runat="server" />
<uc6:FeaturedMerchants ID="uxFeaturedMerchants" runat="server" />
<uc7:LikeBox ID="uxLikeBox" runat="server" />
