<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminEdit.ascx.cs" Inherits="AdminAdvanced_MainControls_AdminEdit" %>
<%@ Register Src="../Components/AdminDetails.ascx" TagName="AdminDetails" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc1:AdminDetails ID="uxDetails" runat="server"></uc1:AdminDetails>
    </PlainContentTemplate>
</uc1:AdminContent>
