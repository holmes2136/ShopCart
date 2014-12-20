<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SecureShoppingAd.ascx.cs"
    Inherits="Components_SecureShoppingAd" %>
<div class="SecureShoppingAd">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/SecureShoppingAdTopLeft.gif"
            runat="server" CssClass="SideBannerTopImgLeft" />
        <asp:Label ID="uxSecureShoppingAdTitle" runat="server" Text="[$SecureShoppingAd]"
            CssClass="SideBannerTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/SecureShoppingAdTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Secure.aspx">
                <vevo:Image ID="uxImage" runat="server" CssClass="NoBorder" />
            </asp:HyperLink>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/SecureShoppingAdBottomLeft.gif"
            runat="server" CssClass="SideBannerImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/SecureShoppingAdBottomRight.gif"
            runat="server" CssClass="SideBannerImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
