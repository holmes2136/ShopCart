<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Mobile_Register" %>

<%@ Register Src="Components/CustomerRegister.ascx" TagName="CustomerRegister" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:CustomerRegister ID="uxCustomerRegister" runat="server" />
</asp:Content>
