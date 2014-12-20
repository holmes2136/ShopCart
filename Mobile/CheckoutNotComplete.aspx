<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="CheckoutNotComplete.aspx.cs" Inherits="Mobile_CheckoutNotComplete" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Title]
    </div>
    <div class="MobileCommonBox">
        <div class="MobileCheckoutNotComplete">
            <asp:Literal ID="uxHeadLiteral" runat="server"></asp:Literal>
        </div>
        <div class="MobileCheckoutNotComplete">
            <asp:Literal ID="uxDescriptionLiteral" runat="server"></asp:Literal></div>
    </div>
    <div class="MobileShoppingCartBackHomeLinkDiv">
        <asp:HyperLink ID="uxGotoPageLink" runat="server" CssClass="MobileCommonHyperLink">[uxGotoPageLink]</asp:HyperLink></div>
</asp:Content>
