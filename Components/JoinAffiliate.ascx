<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JoinAffiliate.ascx.cs"
    Inherits="Components_JoinAffiliate" %>
<div class="JoinAffiliate">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/JoinAffiliateTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxAffiliateTitle" runat="server" Text="[$Affiliate]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/JoinAffiliateTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <vevo:ImageButton ID="uxJoinAffiliateImageButton" runat="server" ThemeImageUrl="Images/Configuration/AffiliateBanner.png"
                OnClick="uxJoinAffiliateImageButton_Click" CssClass="NoBorder" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/JoinAffiliateBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/JoinAffiliateBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
