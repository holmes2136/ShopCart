<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="DirectPaymentSale.aspx.cs"
    Inherits="DirectPaymentSale" Title="[$Title]" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<%@ Register Src="Components/CheckoutPaymentInfo.ascx" TagName="PaymentInfo" TagPrefix="uc4" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator" TagPrefix="uc1" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" StepID="4" Title="[$Head]" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="DirectPaymentSale">
        <div class="CommonPage">
            <div class="CommonPageLeft">
            <uc4:PaymentInfo id="uxPaymentInfo" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
