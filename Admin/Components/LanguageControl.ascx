<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguageControl.ascx.cs"
    Inherits="AdminAdvanced_Components_LanguageControl" %>
<div class="LanguageControl">
    <asp:DropDownList ID="uxDrop" runat="server" AutoPostBack="true" DataTextField="DisplayName"
        DataValueField="CultureID" Width="100px" CssClass="DropDown" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="uxTitleLabel" runat="server" meta:resourcekey="uxTitleLabel" CssClass="Label" />
    <asp:Label ID="uxLanguageHeaderLabel" runat="server" meta:resourcekey="uxLanguageHeaderLabel"
        Visible="false" CssClass="Label" />
    <div class="Clear">
    </div>
</div>
<div class="LanguageControl">
    <asp:Label ID="uxLanguageDescriptionLabel" runat="server" meta:resourcekey="uxLanguageDescriptionLabel"
        Visible="false" CssClass="dn" /></div>
<asp:HiddenField ID="uxLanguageHidden" runat="server" />
