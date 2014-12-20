<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionAds.ascx.cs"
    Inherits="Components_PromotionAds" %>
<div class="PromotionAds">
    <div class="SideBannerTop">
        <asp:Label ID="uxPromotionAdsTitle" runat="server" Text="[$PromotionAds]" CssClass="SideBannerTopTitle"></asp:Label>
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <asp:HyperLink ID="uxPromotionAdsLink" runat="server">
                <vevo:Image ID="uxImage" runat="server" CssClass="NoBorder" />
            </asp:HyperLink>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/PromotionAdsBottomLeft.gif"
            runat="server" CssClass="SideBannerBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/PromotionAdsBottomRight.gif"
            runat="server" CssClass="SideBannerBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
