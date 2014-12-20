<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleCheckoutButton.ascx.cs"
    Inherits="Gateway_Buttons_GoogleCheckoutButton" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>
<cc1:GCheckoutButton ID="uxGCheckoutButton" runat="server" ImageUrl="~/Images/Design/Button/GooGleBuy.gif"
    OnClick="PostCartToGoogle" UseHttps="True" />
    