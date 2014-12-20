<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MobileHeaderLogo.ascx.cs"
    Inherits="Mobile_Components_MobileHeaderLogo" %>
<div class="MobileHeaderLogo">
    <asp:HyperLink ID="uxMobileLogoImageLink" runat="server" NavigateUrl="../Default.aspx">
        <asp:Image ID="uxMobileLogoImage" runat="server" CssClass="MobileHeaderLogo" />
    </asp:HyperLink>
</div>
