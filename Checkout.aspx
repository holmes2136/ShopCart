<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs"
    Inherits="Checkout" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutShippingAddress.ascx" TagName="CheckoutShippingAddress"
    TagPrefix="uc4" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" StepID="2" Title="[$Shipping Details]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="Checkout">
        <uc4:CheckoutShippingAddress ID="uxCheckoutDetails" runat="server" />
        <div id="uxButtonDiv" runat="server" class="CheckoutButtonDiv">
            <asp:LinkButton ID="uxPlaceOrderImageButton" runat="server" OnClick="uxPlaceOrderImageButton_Click"
                Text="[$BtnNext]" ValidationGroup="shippingAddress" CssClass="BtnStyle1" />
        </div>
    </div>
</asp:Content>
