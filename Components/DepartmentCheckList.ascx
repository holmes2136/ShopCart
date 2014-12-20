<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentCheckList.ascx.cs" Inherits="Components_DepartmentCheckList" %>

<asp:Panel id="DepartmentCheckListPanel" runat="server" CssClass="DepartmentCheckList">
    <asp:CheckBoxList ID="uxDepartmentCheckList" runat="server" RepeatColumns="4" CssClass="DepartmentCheckListCheckBoxList"
        RepeatDirection="Horizontal" Width="100%">
    </asp:CheckBoxList>
</asp:Panel>