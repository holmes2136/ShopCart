<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsPerPageDrop.ascx.cs"
    Inherits="Components_ItemsPerPageDrop" %>
<asp:Label ID="uxTitleLabel" runat="server" CssClass="OptionControlTitle" Text="[$Show]" />
<asp:DropDownList ID="uxDrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
    CssClass="ItemsPerPageDrop">
</asp:DropDownList>
<asp:Label ID="uxTitleLabel1" runat="server" CssClass="OptionControlTitle" Text="[$PerPages]" />
<asp:HiddenField ID="uxValueHidden" runat="server" />
