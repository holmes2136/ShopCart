<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentSearchConfiguration.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ContentSearchConfiguration" %>
<%@ Register Src="../Components/ContentDetailsConfig.ascx" TagName="ContentDetailsConfig"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc1:ContentDetailsConfig ID="uxContentConfig" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>
