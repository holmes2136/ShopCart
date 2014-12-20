<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencySelector.ascx.cs"
    Inherits="AdminAdvanced_Components_CurrencySelector" %>
<asp:DropDownList ID="uxCurrencyDrop" runat="server" DataTextField="DisplayName"
    DataValueField="CurrencyCode" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="uxCurrencyDrop_SelectedIndexChanged"
    CssClass="DropDown">
</asp:DropDownList>
<p class="Warning">
    <asp:Label ID="uxWarningLabel" meta:resourcekey="uxWarningLabel" runat="server" />
</p>
