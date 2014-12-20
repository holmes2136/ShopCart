<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentTemplateEdit.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ContentTemplateEdit" %>
<%@ Register Src="../Components/ContentDetails.ascx" TagName="ContentDetails" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="Edit Content" BackToTopLink="false">
    <ContentTemplate>
        <uc1:ContentDetails ID="uxDetails" runat="server"></uc1:ContentDetails>
    </ContentTemplate>
</uc1:AdminContent>
