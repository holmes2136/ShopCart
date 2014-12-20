<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SwitchLanguage.ascx.cs"
    Inherits="Components_SwitchLanguage" %>
<%@ Register Src="SwitchLanguageMenu.ascx" TagName="SwitchLanguageMenu" TagPrefix="uc1" %>
<%@ Register Src="SwitchLanguageMenuDrop.ascx" TagName="SwitchLanguageMenuDrop" TagPrefix="uc2" %>
<div class="SwitchLanguage">
    <div class="SwitchLanguageTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/SwitchLanguageTopLeft.gif"
            runat="server" CssClass="SwitchLanguageTopImgLeft" />
        <asp:Label ID="uxCategoryNavTitle" runat="server" Text="[$SwitchLanguageTitle]" CssClass="SwitchLanguageTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/SwitchLanguageTopRight.gif"
            runat="server" CssClass="SwitchLanguageTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SwitchLanguageLeft">
        <div class="SwitchLanguageRight">
            <uc1:SwitchLanguageMenu ID="uxSwitchLanguageMenu" runat="server" />
            <uc2:SwitchLanguageMenuDrop ID="uxMenuDrop" runat="server" />
        </div>
    </div>
    <div class="SwitchLanguageBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/SwitchLanguageBottomLeft.gif"
            runat="server" CssClass="SwitchLanguageBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/SwitchLanguageBottomRight.gif"
            runat="server" CssClass="SwitchLanguageBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
