<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryList.ascx.cs" Inherits="AdminAdvanced_Components_Common_CountryList" %>
<asp:DropDownList ID="uxCountryDrop" runat="server" Width="155px" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
    AutoPostBack="True" CssClass="DropDown">
</asp:DropDownList><span id="uxStar" runat="server" class="ValidateText">*</span>
<asp:TextBox ID="uxCountryText" runat="server" CssClass="TextBox"></asp:TextBox>
<asp:HiddenField ID="uxSelectedCountryHidden" runat="server" />
