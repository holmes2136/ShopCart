<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreFilterDrop.ascx.cs"
    Inherits="Admin_Components_StoreFilterDrop" %>
<asp:Panel ID="uxStorePanel" runat="server" CssClass="CategoryFilter">
    <asp:Label ID="lcStoreFilter" runat="server" meta:resourcekey="lcStoreFilter" CssClass="Label" />
    <asp:DropDownList ID="uxStoreDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxStoreDrop_SelectedIndexChanged"
        CssClass="DropDown">
    </asp:DropDownList>
</asp:Panel>
