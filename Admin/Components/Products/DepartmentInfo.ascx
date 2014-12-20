<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentInfo.ascx.cs" Inherits="AdminAdvanced_Components_Products_DepartmentInfo" %>
<%@ Register Src="../MultiDepartment.ascx" TagName="MultiDepartment" TagPrefix="uc1" %>

<div class="ProductDetailsRow">
    <asp:Label ID="lcDepartment" runat="server" meta:resourcekey="lcDepartment" CssClass="Label" />
    <asp:DropDownList ID="uxDepartmentDrop" runat="server" Width="255px" Visible="false"
        CssClass="fl DropDown">
    </asp:DropDownList>
    <uc1:MultiDepartment ID="uxMultiDepartment" runat="server" />
</div>
<div class="Clear">
</div>