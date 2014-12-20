<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSearchConfiguration.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ProductSearchConfiguration" %>
<%@ Register Src="../Components/ProductDetailsConfig.ascx" TagName="ProductDetailsConfig"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc1:ProductDetailsConfig ID="uxProductConfig" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>
