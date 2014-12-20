<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SwitchLanguageMenuDrop.ascx.cs"
    Inherits="Components_SwitchLanguageMenuDrop" %>
<asp:Panel ID="uxSwitchLanguageDropPanel" runat="server" CssClass="SwitchLanguageDropPanel">
    <div class="LanguageSwitch">
        <asp:Menu ID="uxLanguageMenuDropDataList" runat="server" CssClass="CommonTopDynamicDropdownList"
            OnMenuItemClick="uxLanguageMenuDrop_OnMenuItemClick" Orientation="horizontal"
            StaticEnableDefaultPopOutImage="false" DynamicEnableDefaultPopOutImage="false"
            StaticHoverStyle-CssClass="CommonTopDynamicDropdownListStaticHover" StaticMenuItemStyle-CssClass="CommonTopDynamicDropdownListStaticMenuItem"
            StaticSelectedStyle-CssClass="CommonTopDynamicDropdownListStaticSelectItem" StaticMenuStyle-CssClass="CommonTopDynamicDropdownListStaticMenuStyle"
            DynamicHoverStyle-CssClass="CommonTopDynamicDropdownListDynamicHover" DynamicMenuItemStyle-CssClass="CommonTopDynamicDropdownListDynamicMenuItem"
            DynamicSelectedStyle-CssClass="CommonTopDynamicDropdownListDynamicSelectItem" DynamicMenuStyle-CssClass="CommonTopDynamicDropdownListDynamicMenuStyle">
        </asp:Menu>
    </div>
</asp:Panel>
<asp:HiddenField ID="uxCultureIDHidden" runat="server" />
