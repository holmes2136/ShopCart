<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchManufacturerItem.ascx.cs"
    Inherits="Components_FacetSearchManufacturerItem" %>
<div id="uxManufacturerItemDiv" runat="server">
    <div class="PriceTitle">
        <asp:Label ID="uxManufacturerTitle" runat="server" Text="[$Manufacturer]">
        </asp:Label>
    </div>
    <asp:DataList ID="uxList" runat="server" ShowFooter="false" CssClass="FacetedSearchNavList">
        <HeaderStyle />
        <ItemTemplate>
            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# GetNavUrl(DataBinder.Eval(Container.DataItem, "ManufacturerID")) %>'
                Text='<%# GetNavName( DataBinder.Eval(Container.DataItem, "Name") ) %>'>
            </asp:HyperLink>
            <asp:Label ID="uxCountItem" runat="server" Text='<%# GetCountManufacturerItem(DataBinder.Eval(Container.DataItem, "ManufacturerID")) %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
</div>
