<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacetSearch.ascx.cs" Inherits="Components_FacetSearch" %>
<%@ Register Src="~/Components/FacetSearchCategoryItem.ascx" TagName="FacetSearchCategoryItem"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/FacetSearchDepartmentItem.ascx" TagName="FacetSearchDepartmentItem"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/FacetSearchManufacturerItem.ascx" TagName="FacetSearchManufacturer"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/FacetSearchSpecification.ascx" TagName="FacetSearchSpecification"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/FacetSearchSelected.ascx" TagName="FacetSearchSelected"
    TagPrefix="uc2" %>
<div class="FacetedNavList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxFacetedSearchTitle" runat="server" Text="[$ShopBy]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <uc2:FacetSearchSelected ID="uxFacetSearchSelected" runat="server" />
            <uc1:FacetSearchCategoryItem ID="uxFacetSearchCategoryItem" runat="server" />
            <uc1:FacetSearchDepartmentItem ID="uxFacetSearchDepartmentItem" runat="server" />
            <uc1:FacetSearchManufacturer ID="FacetSearchManufacturerItem" runat="server" />
            <asp:Panel ID="uxPricePanel" runat="server">
                <div class="PriceTitle">
                    <asp:Label ID="uxPriceTitle" runat="server" Text="[$Price]"></asp:Label>
                </div>
                <asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="FacetedSearchNavList">
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# GetNavUrl(Container.DataItem) %>'
                            Text='<%# GetNavName( Container.DataItem ) %>'>
                        </asp:HyperLink>
                        <asp:Label ID="uxCountItem" runat="server" Text='<%# CountItem(Container.DataItem) %>'></asp:Label>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
            <uc1:FacetSearchSpecification ID="uxFacetSearchSpecification" runat="server" />
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CategoryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CategoryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
