<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderItemsAdd.ascx.cs" Inherits="AdminAdvanced_MainControls_OrderItemsAdd" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/OrderItemDetails.ascx" TagName="OrderItemDetails"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc1:OrderItemDetails ID="uxDetails" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>