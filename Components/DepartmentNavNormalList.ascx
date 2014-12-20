<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentNavNormalList.ascx.cs"
    Inherits="Components_DepartmentNavNormalList" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="DepartmentNavNormalList">
    <HeaderStyle />
    <ItemTemplate>
        <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# Vevo.UrlManager.GetDepartmentUrl( Eval( "DepartmentID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
            Text='<%# GetNavName( Container.DataItem ) %>' ToolTip='<%# Eval("Description") %>'>
        </asp:HyperLink>
    </ItemTemplate>
</asp:DataList>
