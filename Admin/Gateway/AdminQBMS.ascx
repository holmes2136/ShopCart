<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminQBMS.ascx.cs" Inherits="AdminAdvanced_Gateway_AdminQBMS" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxAppIDLabel" runat="server" meta:resourcekey="lcAppID"></asp:Label></div>
    <asp:TextBox ID="uxAppIDText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxAppLoginLabel" runat="server" meta:resourcekey="lcAppLogin"></asp:Label></div>
    <asp:TextBox ID="uxAppLoginText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxConnectionTicketLabel" runat="server" meta:resourcekey="lcConnectionTicket"></asp:Label></div>
    <asp:TextBox ID="uxConnectionTicketText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxQbmsTestLabel" runat="server" meta:resourcekey="lcQbmsTest"></asp:Label></div>
    <asp:DropDownList ID="uxQbmsTestDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Selected="True" Value="True">Test</asp:ListItem>
        <asp:ListItem Value="False">Production</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="QuickBooks QBMS" />