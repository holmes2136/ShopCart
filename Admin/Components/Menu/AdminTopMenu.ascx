<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminTopMenu.ascx.cs"
    Inherits="AdminAdvanced_Components_Menu_AdminTopMenu" %>
<asp:Panel ID="uxMenuPanel" runat="server" CssClass="AdminTopMenu">
    <div class="AdminTopMenuLeft">
        <div class="AdminTopMenuRight">
            <asp:Menu ID="uxContentMenuNavListTop" runat="server" CssClass="ContentMenuNavMenuList" 
                StaticEnableDefaultPopOutImage="false" DynamicEnableDefaultPopOutImage="false" >
                <StaticHoverStyle CssClass="ContentMenuNavMenuListStaticHover" />
                <StaticMenuStyle CssClass="ContentMenuNavMenuListStaticMenuStyle" />
                <StaticMenuItemStyle CssClass="ContentMenuNavListStaticMenuItem" ItemSpacing="0" />
                <StaticSelectedStyle CssClass="ContentMenuNavMenuListStaticSelectItem" />
                <DynamicHoverStyle CssClass="ContentMenuNavMenuListDynamicHover" />
                <DynamicMenuStyle CssClass="ContentMenuNavMenuListDynamicMenuStyle" />
                <DynamicMenuItemStyle CssClass="ContentMenuNavMenuListDynamicMenuItem" />
                <DynamicSelectedStyle CssClass="ContentMenuNavMenuListDynamicSelectItem" />
            </asp:Menu>
            <div class="Clear">
            </div>
        </div>
    </div>
</asp:Panel>
