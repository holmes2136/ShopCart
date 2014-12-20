<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Mobile/Mobile.master"
    CodeFile="ShippingAddress.aspx.cs" Inherits="Mobile_ShippingAddress" %>

<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryAndState"
    TagPrefix="uc1" %>
<%@ Register Src="Components/ShippingAddressDetails.ascx" TagName="ShippingAddressDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:ShippingAddressDetails ID="uxShippingAddressDetails" runat="server" />
</asp:Content>
