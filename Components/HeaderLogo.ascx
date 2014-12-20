<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderLogo.ascx.cs" Inherits="Components_HeaderLogo" %>
<%@ Register Src="../Components/ImageLogo.ascx" TagName="ImageLogo" TagPrefix="uc3" %>
<div class="HeaderLogo">
    <asp:HyperLink ID="uxImageLogoLink" runat="server" NavigateUrl="~/Default.aspx">
        <uc3:ImageLogo ID="ImageLogo" runat="server" MaximumWidth="354px" MaximumHight="117px" />
    </asp:HyperLink>
</div>
