<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialOffer.ascx.cs"
    Inherits="Components_SpecialOffer" %>
<%@ Register Src="../Components/DotLine.ascx" TagName="DotLine" TagPrefix="uc5" %>
<div class="SpecialOfferAd">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/SpecialOfferTopLeft.gif"
            runat="server" CssClass="SideBannerTopImgLeft" />
        <asp:Label ID="uxSpecialOfferTitle" runat="server" Text="[$SpecialOffer]" CssClass="SideBannerTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/SpecialOfferTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <asp:HyperLink ID="uxSpecialOfferLink" runat="server" NavigateUrl="~/SpecialOffer.aspx">
                <vevo:Image ID="uxImage" runat="server" CssClass="NoBorder" />
            </asp:HyperLink>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/SpecialOfferBottomLeft.gif"
            runat="server" CssClass="SideBannerBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/SpecialOfferBottomRight.gif"
            runat="server" CssClass="SideBannerBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
