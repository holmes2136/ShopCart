<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchBooleanPanel.ascx.cs"
    Inherits="AdminAdvanced_Components_SearchFilter_SearchBooleanPanel" %>
<asp:Label ID="lcYesNoValue" runat="server" meta:resourcekey="lcYesNoValue" CssClass="Label" />
<asp:DropDownList ID="uxBooleanDrop" runat="server" CssClass="DropDown">
    <asp:ListItem Value="True">Yes</asp:ListItem>
    <asp:ListItem Value="False">No</asp:ListItem>
</asp:DropDownList>