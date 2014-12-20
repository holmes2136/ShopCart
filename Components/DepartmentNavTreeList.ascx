<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentNavTreeList.ascx.cs"
    Inherits="Components_DepartmentNavTreeList" %>
<asp:TreeView ID="uxDepartmentNavListTreeView" runat="server" 
    CssClass="DepartmentNavTreeList"
    SelectedNodeStyle-CssClass="DepartmentNavTreeListSelectedNode" 
    NodeStyle-CssClass="DepartmentNavTreeListNode"
    LeafNodeStyle-CssClass="DepartmentNavTreeListLeafNode" />
