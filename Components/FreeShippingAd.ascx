<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FreeShippingAd.ascx.cs"
    Inherits="Components_FreeShippingAd" %>
<div class="FreeShippingAd">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/FreeShippingAdTopLeft.gif"
            runat="server" CssClass="SideBannerTopImgLeft" />
        <asp:Label ID="uxFreeShippingAdTitle" runat="server" Text="[$FreeShippingAd]" CssClass="SideBannerTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/FreeShippingAdTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <asp:HyperLink ID="uxFreeLink" runat="server" NavigateUrl="~/FreeShipping.aspx">
                <vevo:Image ID="uxImage" runat="server" CssClass="NoBorder" />
            </asp:HyperLink>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/FreeShippingAdBottomLeft.gif"
            runat="server" CssClass="SideBannerBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/FreeShippingAdBottomRight.gif"
            runat="server" CssClass="SideBannerBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
