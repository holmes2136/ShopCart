<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerAdd.ascx.cs" Inherits="AdminAdvanced_MainControls_CustomerAdd" %>
<%@ Register Src="../Components/CustomerDetails.ascx" TagName="CustomerDetails" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc1:CustomerDetails ID="uxDetails" runat="server"></uc1:CustomerDetails>
    </PlainContentTemplate>
</uc1:AdminContent>
