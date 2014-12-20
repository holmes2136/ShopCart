<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryNavTreeList.ascx.cs"
    Inherits="Components_CategoryNavTreeList" %>
<asp:TreeView ID="uxCategoryNavListTreeView" runat="server" 
    CssClass="CategoryNavTreeList"
    SelectedNodeStyle-CssClass="CategoryNavTreeListSelectedNode" 
    NodeStyle-CssClass="CategoryNavTreeListNode"
    LeafNodeStyle-CssClass="CategoryNavTreeListLeafNode"  
    OnDataBound="uxCategoryNavListTreeView_DataBound" 
    EnableClientScript="True"
    PopulateNodesFromClient="True" />
