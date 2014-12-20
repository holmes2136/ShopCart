<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearchSpecificationValue.ascx.cs"
    Inherits="Components_FacetSearchSpecificationValue" %>
<div id="uxSpecValueDiv" runat="server">
    <div class="PriceTitle">
        <asp:Label ID="uxPriceTitle" runat="server" Text='<%# GetGroupText( Eval("DisplayName") ) %>'>
            ></asp:Label>
    </div>
    <asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="FacetedSearchNavList">
        <HeaderStyle />
        <ItemTemplate>
            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# GetNavUrl(DataBinder.Eval(Container.DataItem, "Value")) %>'
                Text='<%# GetNavName( DataBinder.Eval(Container.DataItem, "DisplayValue") ) %>'>
            </asp:HyperLink>
            <asp:Label ID="uxCountItem" runat="server" Text='<%# GetCountSpecItem(DataBinder.Eval(Container.DataItem, "SpecificationItemValueID")) %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
</div>
<asp:HiddenField ID="uxHiddenName" Value='<%# Eval("Name") %>' runat="server" />
<asp:HiddenField ID="uxHiddenID" Value='<%# Eval("SpecificationItemID") %>' runat="server" />
