<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentListDefault.ascx.cs"
    Inherits="Layouts_DepartmentLists_DepartmentListDefault" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsperpageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="~/Layouts/DepartmentLists/Controls/DepartmentListItemDefault.ascx"
    TagName="DepartmentListItem" TagPrefix="uc3" %>
<asp:UpdatePanel ID="uxDepartmentListUpdate" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <div class="DepartmentListDefault">
            <div class="DepartmentListDefaultPageItemControlDiv" id="uxDepartmentPageControlDiv"
                runat="server">
                <div class="DepartmentListDefaultItemPerPageDiv">
                    <uc2:ItemsperpageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="DepartmentItemsPerPage" />
                </div>
                <div class="ProductItemPaging">
                    <uc3:PagingControl ID="uxPagingControl" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <asp:DataList ID="uxList" CssClass="DepartmentListDefaultDataList" runat="server"
                ShowFooter="False">
                <HeaderStyle CssClass="DepartmentListDefaultDataListHeader" />
                <ItemStyle CssClass="DepartmentListDefaultDataListItem" />
                <ItemTemplate>
                    <uc3:DepartmentListItem ID="uxDepartmentList" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                </HeaderTemplate>
            </asp:DataList>
            <div class="ProductListDefaultPagingControl">
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
