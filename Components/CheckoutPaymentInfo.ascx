<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutPaymentInfo.ascx.cs"
    Inherits="Components_CheckoutPaymentInfo" %>
<div class="PaymentDiv">
    <div class="SidebarTop">
        <asp:Label ID="uxHeaderCheckoutLabel" runat="server" Text="[$Title]" CssClass="CheckoutAddressTitle"></asp:Label>
    </div>
    <iframe id="uxPaymentFrame" runat="server" frameborder="0" class="PaymentFrame">
    </iframe>
</div>
