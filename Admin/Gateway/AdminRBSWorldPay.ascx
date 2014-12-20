<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminRBSWorldPay.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminRBSWorldPay" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxRBSWorldPayinstIdLabel" runat="server" meta:resourcekey="uxRBSWorldPayinstId" /></div>
    <asp:TextBox ID="uxRBSWorldPayinstIdText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxRBSWorldPayTestLabel" runat="server" meta:resourcekey="uxRBSWorldPayTest" /></div>
    <asp:DropDownList ID="uxRBSWorldPayModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Selected="True" Value="CAPTURED">Test with result value : CAPTURED</asp:ListItem>
        <asp:ListItem Value="REFUSED">Test with result value : REFUSED</asp:ListItem>
        <asp:ListItem Value="AUTHORISED">Test with result value : AUTHORISED</asp:ListItem>
        <asp:ListItem Value="ERROR">Test with result value : ERROR</asp:ListItem>
        <asp:ListItem Value="LIVE">Live</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="RBSWorldPay" />