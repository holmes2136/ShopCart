<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchDepartmentItem.ascx.cs"
    Inherits="Components_FacetSearchDepartmentItem" %>
<div id="uxDepartmentItemDiv" runat="server">
    <div class="PriceTitle">
        <asp:Label ID="uxDepartmentTitle" runat="server" Text="[$Department]">
        </asp:Label>
    </div>
    <asp:DataList ID="uxList" runat="server" ShowFooter="false" CssClass="FacetedSearchNavList">
        <HeaderStyle />
        <ItemTemplate>
            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# GetNavUrl(DataBinder.Eval(Container.DataItem, "DepartmentID")) %>'
                Text='<%# GetNavName( DataBinder.Eval(Container.DataItem, "Name") ) %>'>
            </asp:HyperLink>
            <asp:Label ID="uxCountItem" runat="server" Text='<%# GetCountDepartmentItem(DataBinder.Eval(Container.DataItem, "DepartmentID")) %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
</div>
