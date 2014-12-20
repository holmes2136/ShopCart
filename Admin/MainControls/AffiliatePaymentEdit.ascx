<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePaymentEdit.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliatePaymentEdit" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/AffiliatePaymentDetails.ascx" TagName="AffiliatePaymentDetails" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
   <PlainContentTemplate>
        <uc2:AffiliatePaymentDetails ID="uxDetails" runat="server" />
    </PlainContentTemplate>
</uc1:AdminContent>