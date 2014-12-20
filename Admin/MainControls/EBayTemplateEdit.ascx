<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayTemplateEdit.ascx.cs"
    Inherits="Admin_MainControls_EBayTemplateEdit" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/EBay/EBayTemplateDetails.ascx" TagName="EBayTemplateDetails"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <PlainContentTemplate>
        <uc1:EBayTemplateDetails ID="uxDetails" runat="server"></uc1:EBayTemplateDetails>
    </PlainContentTemplate>
</uc1:AdminContent>
