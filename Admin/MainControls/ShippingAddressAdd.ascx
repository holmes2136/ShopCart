<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressAdd.ascx.cs" Inherits="Admin_MainControls_ShippingAddressAdd" %>
<%@ Register Src="../Components/ShippingAddressDetails.ascx" TagName="ShippingAddressDetails" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" >
    <PlainContentTemplate>
        <uc1:ShippingAddressDetails ID="uxDetails" runat="server"></uc1:ShippingAddressDetails>
    </PlainContentTemplate>
</uc1:AdminContent>