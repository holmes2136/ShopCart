<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CultureEdit.ascx.cs" Inherits="AdminAdvanced_MainControls_CultureEdit" %>
<%@ Register Src="../Components/CultureDetails.ascx" TagName="CultureDetails" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc2:CultureDetails ID="uxDetails" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>
