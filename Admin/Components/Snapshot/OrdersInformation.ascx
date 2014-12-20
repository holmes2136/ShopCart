<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrdersInformation.ascx.cs"
    Inherits="AdminAdvanced_Components_OrdersInformation" %>
<div class="fb c11">
    Unprocessed Orders
</div>
<div>
    <asp:Label ID="uxNewOrderLabel" runat="server" Text="" CssClass="fl"></asp:Label>
    <vevo:AdvancedLinkButton ID="uxNewOrderLink" runat="server" PageName="OrdersList.ascx"
        PageQueryString="" OnClick="ChangePage_Click" CssClassBegin="fl" CssClassEnd=""
        CssClass="c11 UnderlineDashed"></vevo:AdvancedLinkButton>
    <div class="Clear">
    </div>
</div>
<br />
<asp:Label ID="uxNewOrdersAmout" runat="server"></asp:Label>