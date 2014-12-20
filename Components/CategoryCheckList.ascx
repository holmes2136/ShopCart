<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryCheckList.ascx.cs"
    Inherits="Components_CategoryCheckList" %>

<asp:Panel id="CategoryCheckListPanel" runat="server" CssClass="CategoryCheckList">
    <asp:CheckBoxList ID="uxCategoryCheckList" runat="server" RepeatColumns="4" CssClass="CategoryCheckListCheckBoxList"
        RepeatDirection="Horizontal" Width="100%">
    </asp:CheckBoxList>
</asp:Panel>