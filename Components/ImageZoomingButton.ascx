<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageZoomingButton.ascx.cs"
    Inherits="Components_ImageZoomingButton" %>
<div class="ImageZoomingButton">
    <div class="ImageZoomingButtonPopup">
        <asp:PlaceHolder ID="uxPopupPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div class="ImageZoomingButtonZoom">
        <asp:HyperLink ID="uxZoomLink" runat="server" CssClass="ImageZoomingButtonZoomLink">
            <asp:Image ID="uxZoomImage" runat="server" ImageUrl="~/Images/Design/Icon/Zoom.gif"
                CssClass="ImageZoomingButtonZoomImage" />
            <div id="uxZoomLabel" runat="server" class="ImageZoomingButtonZoomMessage">
                [$Zoom]
            </div>
        </asp:HyperLink>
    </div>
    <div class="Clear">
    </div>
</div>
