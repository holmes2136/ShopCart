<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderSummaryRightMenu.ascx.cs"
    Inherits="Components_OrderSummaryRightMenu" %>
<div class="OrderSummaryRightMenu">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/BoxTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxShoppingCartTitle" runat="server" Text="[$OrderSummary]" CssClass="SidebarTopTitle" />
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/BoxTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="ShoppingCartDetail">
                <div>
                    <asp:Label ID="uxSubTotalLabel" runat="server" CssClass="Label" Text="[$CartSubtotal]" />
                    <asp:Label ID="uxSubTotalValueLabel" runat="server" CssClass="Value" />
                </div>
                <asp:Panel ID="uxDiscountPanel" runat="server">
                    <asp:Label ID="uxDiscountLabel" runat="server" CssClass="Label" Text="[$Discount]" />
                    <asp:Label ID="uxDiscountValueLabel" runat="server" CssClass="Value" />
                </asp:Panel>
                <asp:Panel ID="uxPointDiscountPanel" runat="server">
                    <asp:Label ID="uxPointDiscountLabel" runat="server" CssClass="Label" Text="[$PointDiscount]" />
                    <asp:Label ID="uxPointDiscountValueLabel" runat="server" CssClass="Value" />
                </asp:Panel>
                <div>
                    <asp:Label ID="uxTaxLabel" runat="server" CssClass="Label" Text="[$Tax]" />
                    <asp:Label ID="uxTaxValueLabel" runat="server" CssClass="Value" />
                </div>
                <div>
                    <asp:Label ID="uxShippingLabel" runat="server" CssClass="Label" Text="[$Shipping]" />
                    <asp:Label ID="uxShippingValueLabel" runat="server" CssClass="Value" />
                </div>
                <asp:Panel ID="uxHandlingFeePanel" runat="server">
                    <asp:Label ID="uxHandlingFeeLabel" runat="server" CssClass="Label" Text="[$HandlingFee]" />
                    <asp:Label ID="uxHandlingFeeValueLabel" runat="server" CssClass="Value" />
                </asp:Panel>
                <asp:Panel ID="uxGiftCertificatePanel" runat="server">
                    <asp:Label ID="uxGiftCertificateLabel" runat="server" CssClass="Label" Text="[$GiftCertificate]" />
                    <asp:Label ID="uxGiftCertificateValueLabel" runat="server" CssClass="Value" />
                </asp:Panel>
            </div>
            <div class="TotalDetail">
                <asp:Label ID="uxTotalLabel" runat="server" CssClass="Label" Text="[$Total]" />
                <asp:Label ID="uxTotalValueLabel" runat="server" CssClass="Value" />
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/BoxBottomLeft.gif" runat="server"
            CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/BoxBottomRight.gif" runat="server"
            CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
