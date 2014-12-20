<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuNavCascadeList.ascx.cs"
    Inherits="Components_ContentMenuNavCascadeList" %>
<asp:Menu ID="uxContentMenuNavList" runat="server" CssClass="ContentMenuNavMenuList"
    StaticHoverStyle-CssClass="ContentMenuNavMenuListStaticHover" StaticMenuItemStyle-CssClass="ContentMenuNavMenuListStaticMenuItem"
    StaticSelectedStyle-CssClass="ContentMenuNavMenuListStaticSelectItem" StaticMenuStyle-CssClass="ContentMenuNavMenuListStaticMenuStyle"
    DynamicHoverStyle-CssClass="ContentMenuNavMenuListDynamicHover" DynamicMenuItemStyle-CssClass="ContentMenuNavMenuListDynamicMenuItem"
    DynamicSelectedStyle-CssClass="ContentMenuNavMenuListDynamicSelectItem" DynamicMenuStyle-CssClass="ContentMenuNavMenuListDynamicMenuStyle"
    StaticEnableDefaultPopOutImage='false' DynamicEnableDefaultPopOutImage="false">
</asp:Menu>
<asp:Menu ID="uxContentMenuNavListTop" runat="server" CssClass="ContentMenuNavMenuList"
    StaticHoverStyle-CssClass="ContentMenuNavMenuListStaticHover" StaticMenuItemStyle-CssClass="ContentMenuNavListStaticMenuItem"
    StaticSelectedStyle-CssClass="ContentMenuNavMenuListStaticSelectItem" StaticMenuStyle-CssClass="ContentMenuNavMenuListStaticMenuStyle"
    DynamicHoverStyle-CssClass="ContentMenuNavMenuListDynamicHover" DynamicMenuItemStyle-CssClass="ContentMenuNavMenuListDynamicMenuItem"
    DynamicSelectedStyle-CssClass="ContentMenuNavMenuListDynamicSelectItem" DynamicMenuStyle-CssClass="ContentMenuNavMenuListDynamicMenuStyle"
    StaticEnableDefaultPopOutImage='false' DynamicEnableDefaultPopOutImage="false">
    <LevelSubMenuStyles>
        <asp:SubMenuStyle />
        <asp:SubMenuStyle CssClass="DynamicMenuFirstLevel" />
    </LevelSubMenuStyles>
</asp:Menu>
