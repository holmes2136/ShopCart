<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListViewType.ascx.cs"
    Inherits="Components_ProductListViewType" %>
<asp:Label ID="uxViewLabel" runat="server" CssClass="OptionControlTitle">[$View As]</asp:Label>
<asp:LinkButton ID="uxGridViewLinkButton" runat="server" Text="[$Grid]" OnClick="uxGridViewLinkButton_Click" ToolTip="Grid View Style" />
<asp:LinkButton ID="uxListViewLinkButton" runat="server" Text="[$List]" OnClick="uxListViewLinkButton_Click" ToolTip="List View Style"  />
<asp:LinkButton ID="uxTableViewLinkButton" runat="server" Text="[$Table]" OnClick="uxTableViewLinkButton_Click" ToolTip="Table View Style" />