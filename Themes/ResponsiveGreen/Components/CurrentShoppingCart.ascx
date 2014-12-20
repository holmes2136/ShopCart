<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrentShoppingCart.ascx.cs"
    Inherits="Themes_ResponsiveGreen_Components_CurrentShoppingCart" %>
<div class="HeaderShoppingCart" id="uxCurrentShoppingCartDiv" runat="server">
    <div class="HeaderShoppingCartDiv">
        <div class="HeaderShoppingCartBraclet">(</div>
        <div class="QuantityDiv">
            <asp:Label ID="uxQuantityLabel" runat="server" CssClass="QuantityValue" />[$Item]
            /
        </div>
        <div id="uxDiscountTR" runat="server" class="DiscountDiv">
            <asp:Label ID="uxDiscountLabel" runat="server" CssClass="DiscountValue" />[$Discount]
            /
        </div>
        <div class="AmountDiv">
            <asp:Label ID="uxAmountLabel" runat="server" CssClass="AmountValue" />
        </div>
        <div class="HeaderShoppingCartBraclet">)</div>
         <asp:LinkButton ID="uxViewCart" runat="server" CssClass="HeaderShoppingCartViewCart"
        Text="[$View Cart]" PostBackUrl="~/ShoppingCart.aspx" />
    </div>
    <asp:LinkButton ID="uxCheckOutButton" runat="server" CssClass="HeaderShoppingCartCheckout"
        Text="[$Check Out]" OnClick="uxCheckOutButton_Click" />
</div>
