<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagingControl.ascx.cs"
    Inherits="Advanced_Components_PagingControl" %>
<div class="PagingControl">
    <asp:Label ID="lcPage" runat="server" meta:resourcekey="lcPage" CssClass="Label"></asp:Label>
    <asp:Panel ID="uxPanel" runat="server" DefaultButton="uxSelectPageButton" CssClass="Panel">
        <asp:PlaceHolder ID="uxPlaceHolder" runat="server"></asp:PlaceHolder>
        <div style="display: none">
            <asp:Button ID="uxSelectPageButton" runat="server" Text="SelectPage" OnClick="uxSelectPageButton_Click">
            </asp:Button>
        </div>
    </asp:Panel>
</div>
<div class="ItemsPerPage">
    <asp:DropDownList ID="uxItemsPerPagesDrop" runat="server" OnSelectedIndexChanged="uxItemsPerPagesDrop_SelectedIndexChanged"
        AutoPostBack="true" CssClass="DropDownList">
        <asp:ListItem Text="20" Value="20"></asp:ListItem>
        <asp:ListItem Text="40" Value="40"></asp:ListItem>
        <asp:ListItem Text="100" Value="100"></asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="uxPerPageLabel" runat="server" Text="per page" CssClass="Label"></asp:Label>
</div>
<div class="Clear">
</div>
