<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListDefault.ascx.cs"
    Inherits="Layouts_CategoryLists_CategoryListDefault" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsperpageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="~/Layouts/CategoryLists/Controls/CategoryListItemDefault.ascx"
    TagName="CategoryListItem" TagPrefix="uc3" %>
<asp:UpdatePanel ID="uxCategoryListUpdate" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <div class="CategoryListDefault">
            <div class="CategoryListDefaultPageItemControlDiv" id="uxCategoryPageControlDiv"
                runat="server">
                <div class="CategoryListDefaultItemPerPageDiv">
                    <uc2:ItemsperpageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="CategoryItemsPerPage" />
                </div>
                <div class="ProductItemPaging">
                    <uc3:PagingControl ID="uxPagingControl" runat="server" />
                </div>
            </div>
            <asp:DataList ID="uxList" CssClass="CategoryListDefaultDataList" runat="server" ShowFooter="False">
                <HeaderStyle CssClass="CategoryListDefaultDataListHeader" />
                <ItemStyle CssClass="CategoryListDefaultDataListItem" />
                <ItemTemplate>
                    <uc3:CategoryListItem ID="uxCategoryList" runat="server" />
                </ItemTemplate>
            </asp:DataList>
            <div class="ProductListDefaultPagingControl" id="uxCategoryLinkToTopDiv" runat="server">
                <div class="ProductLinkToTopDiv">
                    <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$Gototop]" />
                </div>
            </div>
            <asp:Panel ID="uxCatalogControlPanel" runat="server" CssClass="CatalogControlPanel">
            </asp:Panel>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxItemsPerPageControl" />
        <asp:AsyncPostBackTrigger ControlID="uxPagingControl" />
    </Triggers>
</asp:UpdatePanel>
