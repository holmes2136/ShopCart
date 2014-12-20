<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminProtX.ascx.cs" Inherits="AdminAdvanced_Gateway_AdminProtX" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcMode" runat="server" meta:resourcekey="lcMode" /></div>
    <asp:DropDownList ID="uxModeDrop" runat="server" CssClass="fl">
        <asp:ListItem Value="LIVE">Live</asp:ListItem>
        <asp:ListItem Value="TEST">Test</asp:ListItem>
        <asp:ListItem Value="SIMULATOR">Simulator</asp:ListItem>
    </asp:DropDownList>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcVendorName" runat="server" meta:resourcekey="lcVendorName" /></div>
    <asp:TextBox ID="uxVendorNameText" runat="server" Width="250px" CssClass="fl" />
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="ProtX" />

