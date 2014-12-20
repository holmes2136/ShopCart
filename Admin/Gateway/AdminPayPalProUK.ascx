<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminPayPalProUK.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminPayPalProUK" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxTestAccountLabel" runat="server" meta:resourcekey="lcTestAccount"></asp:Label></div>
    <asp:DropDownList ID="uxTestAccountDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Test</asp:ListItem>
        <asp:ListItem Value="False">Live</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxUserAccountLabel" runat="server" meta:resourcekey="lcUserAccount"></asp:Label></div>
    <asp:TextBox ID="uxUserAccountText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxVendorAccountLabel" runat="server" meta:resourcekey="lcVendorAccount"></asp:Label></div>
    <asp:TextBox ID="uxVendorAccountText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxPartnerLabel" runat="server" meta:resourcekey="lcPartner">
        </asp:Label></div>
    <asp:TextBox ID="uxPartnerText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="Clear">
    </div>
</div>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="uxPasswordLabel" runat="server" meta:resourcekey="lcPassword">
        </asp:Label></div>
    <asp:TextBox ID="uxPasswordText" runat="server" CssClass="fl TextBox">
    </asp:TextBox>
    <div class="Clear">
    </div>
</div>
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="PayPal Pro UK" />