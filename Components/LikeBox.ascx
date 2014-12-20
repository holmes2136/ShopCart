<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LikeBox.ascx.cs" Inherits="Components_LikeBox" %>
<%@ Register Src="../Components/Widget.ascx" TagName="Widget" TagPrefix="uc1" %>
<div class="LikeBoxWidget">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/LikeBoxWidgetTopLeft.gif"
            runat="server" CssClass="SideBannerTopImgLeft" />
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/LikeBoxWidgetTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
            <uc1:Widget WidgetStyle="LikeBox" runat="server" ID="LikeBoxWidget" />
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/LikeBoxWidgetBottomLeft.gif"
            runat="server" CssClass="SideBannerImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/LikeBoxWidgetBottomRight.gif"
            runat="server" CssClass="SideBannerImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
