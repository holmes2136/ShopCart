<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderCreateCheckOutSummary.ascx.cs"
    Inherits="Admin_MainControls_OrderCreateCheckOutSummary" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Order/CheckOutSummary.ascx" TagName="CheckOutSummary"
    TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
<PlainContentTemplate>
        <uc2:CheckOutSummary ID="uxCheckOutSummary" runat="server"></uc2:CheckOutSummary>
    </PlainContentTemplate>
</uc1:AdminContent>
