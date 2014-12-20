<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LivePerson.ascx.cs" Inherits="Components_LivePerson" %>
<%@ Register Src="../Components/Widget.ascx" TagName="Widget" TagPrefix="uc1" %>
<div class="LivePersonWidget">
    <div class="SideBannerTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/LivePersonWidgetTopLeft.gif"
            runat="server" CssClass="SideBannerTopImgLeft" />
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/LivePersonWidgetTopRight.gif"
            runat="server" CssClass="SideBannerTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SideBannerLeft">
        <div class="SideBannerRight">
             <uc1:Widget ID="Widget1" WidgetStyle="LivePerson" runat="server" />
        </div>
    </div>
    <div class="SideBannerBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/LivePersonWidgetBottomLeft.gif"
            runat="server" CssClass="SideBannerImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/LivePersonWidgetBottomRight.gif"
            runat="server" CssClass="SideBannerImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>

