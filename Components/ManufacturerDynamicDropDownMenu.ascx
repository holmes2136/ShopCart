<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerDynamicDropDownMenu.ascx.cs" Inherits="Components_ManufacturerDynamicDropDownMenu" %>
<div class="ContentMenuNavList">
    <asp:Menu ID="uxManufacturerDropDownMenu" runat="server" CssClass="ContentMenuNavMenuList"
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