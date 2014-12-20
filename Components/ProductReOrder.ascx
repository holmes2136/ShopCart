<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReOrder.ascx.cs"
    Inherits="Components_ProductReOrder" %>
<asp:Panel ID="uxPanelReorder" runat="server">
    <asp:Label ID="uxErrorMessage" runat="server"></asp:Label>
    <div class="ShoppingCartBackHomeLinkDiv">
        <asp:HyperLink ID="uxBackLink" runat="server" NavigateUrl="~/OrderHistory.aspx">[$Go back to order history page]</asp:HyperLink>
    </div>
</asp:Panel>
