<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyEdit.ascx.cs"
    Inherits="AdminAdvanced_MainControls_CurrencyEdit" %>
<%@ Register Src="../Components/CurrencyDetails.ascx" TagName="CurrencyDetails"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
   <PlainContentTemplate>
        <uc2:CurrencyDetails ID="uxDetails" runat="server" />
   </PlainContentTemplate>
</uc1:AdminContent>
