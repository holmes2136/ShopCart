<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryFilterDrop.ascx.cs"
    Inherits="AdminAdvanced_Components_CategoryFilterDrop" %>
<div class="CategoryFilter">
    <asp:Panel ID="uxRootCategoryFilterPanel" runat="server">
        <asp:Label ID="uxRootCategoryFilterLabel" runat="server" meta:resourcekey="uxRootCategoryFilterLabel"
            CssClass="Label" />
        <asp:DropDownList ID="uxRootCategoryDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxRootCategoryDrop_SelectedIndexChanged"
            CssClass="DropDown">
        </asp:DropDownList>
    </asp:Panel>
     <div class="Clear">
        </div>
    <div>
    <asp:Label ID="lcCategoryFilter" runat="server" meta:resourcekey="lcCategoryFilter"
        CssClass="Label" />
    <asp:DropDownList ID="uxCategoryDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxCategoryDrop_SelectedIndexChanged"
        CssClass="DropDown">
    </asp:DropDownList>
    </div>
</div>
