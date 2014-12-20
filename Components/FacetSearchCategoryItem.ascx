<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchCategoryItem.ascx.cs"
    Inherits="Components_FacetSearchCategoryItem" %>
<div id="uxCategoryItemDiv" runat="server">
    <div class="PriceTitle">
        <asp:Label ID="uxCategoryTitle" runat="server" Text="[$Category]">
        </asp:Label>
    </div>
    <asp:DataList ID="uxList" runat="server" ShowFooter="false" CssClass="FacetedSearchNavList">
        <HeaderStyle />
        <ItemTemplate>
            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# GetNavUrl(DataBinder.Eval(Container.DataItem, "CategoryID")) %>'
                Text='<%# GetNavName( DataBinder.Eval(Container.DataItem, "Name") ) %>'>
            </asp:HyperLink>
            <asp:Label ID="uxCountItem" runat="server" Text='<%# GetCountCategoryItem(DataBinder.Eval(Container.DataItem, "CategoryID")) %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
</div>
