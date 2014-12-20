<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateAdd.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliateAdd" %>
<%@ Register Src="../Components/AffiliateDetails.ascx" TagName="AffiliateDetails"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <PlainContentTemplate>
        <uc2:AffiliateDetails ID="uxDetails" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>
