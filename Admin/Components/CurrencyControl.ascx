<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyControl.ascx.cs"
    Inherits="Admin_Components_CurrencyControl" %>
<div class="CurrencyControl">
    <asp:DropDownList ID="uxDrop" runat="server" AutoPostBack="true" DataTextField="Name"
        DataValueField="CurrencyCode" Width="100px" CssClass="DropDown" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="uxTitleLabel" runat="server" meta:resourcekey="uxTitleLabel" CssClass="Label" />
  <%--  <asp:Label ID="uxLanguageHeaderLabel" runat="server" meta:resourcekey="uxLanguageHeaderLabel"
        Visible="false" CssClass="Label" />--%>
    <div class="Clear">
    </div>
</div>
<%--<div class="CurrencyControl">
    <asp:Label ID="uxLanguageDescriptionLabel" runat="server" meta:resourcekey="uxLanguageDescriptionLabel"
        Visible="false" CssClass="dn" /></div>--%>
<asp:HiddenField ID="uxLanguageHidden" runat="server" />