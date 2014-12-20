<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WholesaleAdd.ascx.cs"
    Inherits="AdminAdvanced_MainControls_WholesaleAdd" %>
<%@ Register Src="../Components/CustomerDetails.ascx" TagName="CustomerDetails" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <plaincontenttemplate>
        <uc1:CustomerDetails ID="uxDetails" runat="server"></uc1:CustomerDetails>
    </plaincontenttemplate>
</uc1:AdminContent>
