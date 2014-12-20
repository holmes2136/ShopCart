<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReOrder.ascx.cs"
    Inherits="Mobile_Components_ProductReOrder" %>
<asp:Panel ID="uxPanelReorder" runat="server">
    <div class="MobileShoppingCartMessage MobileCommonBox">
        <asp:Label ID="uxErrorMessage" runat="server"></asp:Label></div>
    <div class="MobileShoppingCartBackHomeLinkDiv">
        <asp:HyperLink ID="uxBackLink" runat="server" NavigateUrl="../OrderHistory.aspx">[$Go back to order history page]</asp:HyperLink>
    </div>
</asp:Panel>
