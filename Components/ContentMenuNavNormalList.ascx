<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuNavNormalList.ascx.cs"
    Inherits="Components_ContentMenuNavNormalList" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="CategoryNavNormalList">
    <HeaderStyle />
    <ItemTemplate>
        <asp:HyperLink ID="hyperLink" runat="server"  NavigateUrl='<%# GetNavUrl( Container.DataItem ) %>'
            Text='<%# GetNavName( Container.DataItem ) %>' ToolTip='<%# Eval("Description") %>'>
        </asp:HyperLink>
    </ItemTemplate>
</asp:DataList>

<asp:Menu ID="uxContentMenuListTop" runat="server" CssClass="ContentMenuNavNormalTopList"
    StaticHoverStyle-CssClass="ContentMenuNavMenuListStaticHover" StaticMenuItemStyle-CssClass="ContentMenuNavListStaticMenuItem"
    StaticSelectedStyle-CssClass="ContentMenuNavMenuListStaticSelectItem" StaticMenuStyle-CssClass="ContentMenuNavMenuListStaticMenuStyle"
    DynamicHoverStyle-CssClass="ContentMenuNavMenuListDynamicHover" DynamicMenuItemStyle-CssClass="ContentMenuNavMenuListDynamicMenuItem"
    DynamicSelectedStyle-CssClass="ContentMenuNavMenuListDynamicSelectItem" DynamicMenuStyle-CssClass="ContentMenuNavMenuListDynamicMenuStyle"
    StaticEnableDefaultPopOutImage ='false' DynamicEnableDefaultPopOutImage="false" >
</asp:Menu>