<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentDynamicDropDownMenu.ascx.cs"
    Inherits="Components_DepartmentDynamicDropDownMenu" %>
<div class="ContentMenuNavList">
    <asp:Menu ID="uxDepartmentDropDownMenu" runat="server" CssClass="ContentMenuNavMenuList"
        StaticHoverStyle-CssClass="ContentMenuNavMenuListStaticHover" StaticMenuItemStyle-CssClass="ContentMenuNavListStaticMenuItem"
        StaticSelectedStyle-CssClass="ContentMenuNavMenuListStaticSelectItem" StaticMenuStyle-CssClass="ContentMenuNavMenuListStaticMenuStyle"
        DynamicHoverStyle-CssClass="ContentMenuNavMenuListDynamicHover" DynamicMenuItemStyle-CssClass="ContentMenuNavMenuListDynamicMenuItem"
        DynamicSelectedStyle-CssClass="ContentMenuNavMenuListDynamicSelectItem" DynamicMenuStyle-CssClass="ContentMenuNavMenuListDynamicMenuStyle"
        StaticEnableDefaultPopOutImage='false' 
        Orientation="Horizontal" >
        <LevelSubMenuStyles>
            <asp:SubMenuStyle/>
            <asp:SubMenuStyle CssClass="DynamicMenuFirstLevel" />
        </LevelSubMenuStyles>
    </asp:Menu>
</div>
