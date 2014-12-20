<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePaymentAdd.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliatePaymentAdd" %>
<%@ Register Src="../Components/AffiliatePaymentDetails.ascx" TagName="AffiliatePaymentDetails"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
   <PlainContentTemplate>
        <uc2:AffiliatePaymentDetails ID="uxDetails" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>
