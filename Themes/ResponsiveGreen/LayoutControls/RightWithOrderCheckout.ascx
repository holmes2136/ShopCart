<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightWithOrderCheckout.ascx.cs"
    Inherits="Themes_ResponsiveGreen_LayoutControls_RightWithOrderCheckout" %>
<%@ Register Src="~/Components/OrderSummaryRightMenu.ascx" TagName="OrderSummary"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/GiftCouponDetailRightMenu.ascx" TagName="Giftcoupondetail"
    TagPrefix="uc2" %>
<asp:UpdatePanel ID="uxMiniCartUpdatePanel" runat="server">
    <ContentTemplate>
        <uc1:OrderSummary ID="uxShoppingCart" runat="server" />
        <uc2:Giftcoupondetail ID="uxGiftCouponDetail" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxGiftCouponDetail" />
    </Triggers>
</asp:UpdatePanel>
