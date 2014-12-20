<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="LayoutControls_Header" %>
<%@ Register Src="~/Components/HeaderLogo.ascx" TagName="HeaderImageLogo" TagPrefix="uc1" %>
<%@ Register Src="~/Components/HeaderLogin.ascx" TagName="HeaderLogin" TagPrefix="uc3" %>
<%@ Register Src="~/Components/SwitchLanguage.ascx" TagName="SwitchLanguage" TagPrefix="uc4" %>
<%@ Register Src="~/Components/CurrencyControl.ascx" TagName="CurrencyControl" TagPrefix="uc5" %>
<%@ Register Src="~/Components/HeaderMenu.ascx" TagName="HeaderMenuNormalStyle" TagPrefix="ucMenu" %>
<%@ Register Src="~/Components/CategoryNavTabMenuList.ascx" TagName="HeaderMenuCategoryTabStyle"
    TagPrefix="ucMenu" %>
<%@ Register Src="~/Components/HeaderMenu3.ascx" TagName="HeaderMenuCategoryRootStyle"
    TagPrefix="ucMenu" %>
<div class="header-container1">
    <div class="header-body">
        <uc3:HeaderLogin ID="HeaderLogin" runat="server" />        
        <uc4:SwitchLanguage ID="SwitchLanguage" runat="server" />
        <uc5:CurrencyControl ID="CurrencyControl1" runat="server" />
    </div>
</div>
<div class="header-container2">
    <div class="header-body">
        <uc1:HeaderImageLogo ID="HeaderImageLogo" runat="server" />
    </div>
</div>
<div class="header-container3">
    <div class="header-body">
        <ucMenu:HeaderMenuNormalStyle ID="HeaderMenu" runat="server" />
        <%--<ucMenu:HeaderMenuCategoryRootStyle ID="HeaderMenu" runat="server" />--%>
        <%--<ucMenu:HeaderMenuCategoryTabStyle ID="HeaderMenu" runat="server" />--%>
    </div>
</div>
<div class="header-container4">
    <div class="header-body">
        <asp:Label ID="uxWebsiteTitleLabel" runat="server" CssClass="WebsiteTitle" />
    </div>
</div>
