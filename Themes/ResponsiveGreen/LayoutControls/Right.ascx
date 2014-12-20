<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Right.ascx.cs" Inherits="Themes_ResponsiveGreen_LayoutControls_Right" %>
<%@ Register Src="~/Components/LikeBox.ascx" TagName="LikeBox" TagPrefix="uc" %>
<%@ Register Src="~/Components/ShoppingCartDetails.ascx" TagName="ShoppingCart" TagPrefix="uc" %>
<%@ Register Src="../Components/RecentlyViewProduct.ascx" TagName="RecentlyViewed"
    TagPrefix="uc" %>

<asp:UpdatePanel ID="uxMiniCartUpdatePanel" runat="server">
    <ContentTemplate>
        <uc:ShoppingCart ID="uxShoppingCart" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="uxRecentlyViewedUpdatePanel" runat="server">
    <ContentTemplate>
        <uc:RecentlyViewed ID="RecentlyViewed" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<uc:LikeBox ID="uxLikeBox" runat="server" />
