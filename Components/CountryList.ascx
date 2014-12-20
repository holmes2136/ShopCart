<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryList.ascx.cs" Inherits="Components_CountryList" %>
<asp:DropDownList ID="uxCountryDrop" runat="server" Width="155px" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
    AutoPostBack="True" CssClass="CommonDropDownList">
</asp:DropDownList><span id="uxStar" runat="server" class="CommonAsterisk">*</span>
<asp:TextBox ID="uxCountryText" runat="server" CssClass="CommonTextBox"></asp:TextBox>
<asp:HiddenField ID="uxSelectedCountryHidden" runat="server" />
