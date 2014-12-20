<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeaturedMerchants.ascx.cs"
    Inherits="Components_FeaturedMerchants" %>
<div class="FeaturedMerchants">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/FeaturedMerchantsTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxFeaturedMerchantsTitle" runat="server" Text="[$Featured]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/FeaturedMerchantsTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="FeaturedMerchantsAd">
                <asp:HyperLink ID="uxHyperLink1" runat="server" Target="_blank">
                    <vevo:Image ID="uxImage1" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="FeaturedMerchantsAd">
                <asp:HyperLink ID="uxHyperLink2" runat="server" Target="_blank">
                    <vevo:Image ID="uxImage2" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="FeaturedMerchantsAd">
                <asp:HyperLink ID="uxHyperLink3" runat="server" Target="_blank">
                    <vevo:Image ID="uxImage3" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/FeaturedMerchantsBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/FeaturedMerchantsBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
