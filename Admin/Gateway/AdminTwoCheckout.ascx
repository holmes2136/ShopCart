<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminTwoCheckout.ascx.cs"
    Inherits="AdminAdvanced_Gateway_AdminTwoCheckout" %>
<%@ Register Src="Components/StoreMultiSelect.ascx" TagName="StoreMultiSelect" TagPrefix="uc1" %>
<div class="CommonRowStyle">
    <div class="Label">
        <asp:Label ID="lcMerchantAccount" runat="server" meta:resourcekey="lcMerchantAccount" /></div>
    <asp:TextBox ID="uxMerchantAccountText" runat="server" Width="250px" CssClass="fl TextBox" />
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
<uc1:StoreMultiSelect ID="uxStoreSelect" runat="server" PaymentName="2Checkout" />
