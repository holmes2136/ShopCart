<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryDynamicDropDownMenu.ascx.cs"
    Inherits="Components_CategoryDynamicDropDownMenu" %>
<div class="ContentMenuNavList">
    <asp:Menu ID="uxCategoryDropDownMenu" runat="server" CssClass="ContentMenuNavMenuList"
        StaticHoverStyle-CssClass="ContentMenuNavMenuListStaticHover" StaticMenuItemStyle-CssClass="ContentMenuNavListStaticMenuItem"
        StaticSelectedStyle-CssClass="ContentMenuNavMenuListStaticSelectItem" StaticMenuStyle-CssClass="ContentMenuNavMenuListStaticMenuStyle"
        DynamicHoverStyle-CssClass="ContentMenuNavMenuListDynamicHover" DynamicMenuItemStyle-CssClass="ContentMenuNavMenuListDynamicMenuItem"
        DynamicSelectedStyle-CssClass="ContentMenuNavMenuListDynamicSelectItem" DynamicMenuStyle-CssClass="ContentMenuNavMenuListDynamicMenuStyle"
        Orientation="Horizontal" StaticEnableDefaultPopOutImage="false" >
        <LevelSubMenuStyles>
            <asp:SubMenuStyle/>
            <asp:SubMenuStyle CssClass="DynamicMenuFirstLevel" />
        </LevelSubMenuStyles>
    </asp:Menu>
</div>
