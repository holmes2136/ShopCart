<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryNavNormalList.ascx.cs"
    Inherits="Components_CategoryNavNormalList" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="CategoryNavNormalList">
    <HeaderStyle />
    <ItemTemplate>
        <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# Vevo.UrlManager.GetCategoryUrl( Eval( "CategoryID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
            Text='<%# GetNavName( Container.DataItem ) %>' ToolTip='<%# Eval("Description") %>'>
        </asp:HyperLink>
    </ItemTemplate>
</asp:DataList>
