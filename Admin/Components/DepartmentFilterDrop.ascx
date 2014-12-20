<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentFilterDrop.ascx.cs" 
Inherits="AdminAdvanced_Components_DepartmentFilterDrop" %>
<div class="DepartmentFilter">
    <asp:Panel ID="uxRootDepartmentFilterPanel" runat="server">
        <asp:Label ID="uxRootDepartmentFilterLabel" runat="server" meta:resourcekey="uxRootDepartmentFilterLabel"
            CssClass="Label" />
        <asp:DropDownList ID="uxRootDepartmentDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxRootDepartmentDrop_SelectedIndexChanged"
            CssClass="DropDown">
        </asp:DropDownList>
    </asp:Panel>
     <div class="Clear">
        </div>
    <div>
    <asp:Label ID="lcDepartmentFilter" runat="server" meta:resourcekey="lcDepartmentFilter"
        CssClass="Label" />
    <asp:DropDownList ID="uxDepartmentDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxDepartmentDrop_SelectedIndexChanged"
        CssClass="DropDown">
    </asp:DropDownList>
    </div>
</div>
