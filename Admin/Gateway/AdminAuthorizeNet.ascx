<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminAuthorizeNet.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminAuthorizeNet" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcLoginID" runat="server" meta:resourcekey="lcLoginID" /></div>
    <asp:TextBox ID="uxLoginIDText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcTransactionKey" runat="server" meta:resourcekey="lcTransactionKey" /></div>
    <asp:TextBox ID="uxTransactionKeyText" runat="server" Width="250px" CssClass="fl TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcTestMode" runat="server" meta:resourcekey="lcTestMode" /></div>
    <asp:DropDownList ID="uxTestModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="Authorize.Net" />
