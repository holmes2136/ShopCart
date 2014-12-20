<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentLogo.ascx.cs" Inherits="Components_PaymentLogo" %>
<div class="PaymentLogo">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/PaymentLogoTopLeft.gif" runat="server"
            CssClass="SideBannerTopImgLeft" />
        <asp:Label ID="uxPaymentLogoTitle" runat="server" Text="Special Offer" CssClass="SideBannerTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/PaymentLogoTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <vevo:Image ID="uxImage" runat="server" CssClass="NoBorder" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/PaymentLogoBottomLeft.gif"
            runat="server" CssClass="fSideBannerBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/PaymentLogoBottomRight.gif"
            runat="server" CssClass="SideBannerBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
