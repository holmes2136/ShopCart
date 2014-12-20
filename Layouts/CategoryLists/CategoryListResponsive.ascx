<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListResponsive.ascx.cs"
    Inherits="Layouts_CategoryLists_CategoryListResponsive" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsperpageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="~/Layouts/CategoryLists/Controls/CategoryListItemResponsive.ascx"
    TagName="CategoryListItem" TagPrefix="uc3" %>
    <%@ Register Src="~/Components/NewArrivalCategory.ascx" TagName="NewArrivalCategory"
    TagPrefix="uc" %>
<asp:UpdatePanel ID="uxCategoryListUpdate" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <div class="row">
            <div class="twelve columns">
                <div class="row">
                    <div class="twelve columns">
                        <asp:Label ID="uxCategoryDescriptionText" runat="server" CssClass="CategoryDescription"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="twelve columns">
                        <div class="CategoryListDefaultPageItemControlDiv" id="uxCategoryPageControlDiv"
                            runat="server">
                            <div class="CategoryListDefaultItemPerPageDiv">
                                <uc2:ItemsperpageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="CategoryItemsPerPage" />
                            </div>
                            <div class="ProductItemPaging">
                                <uc3:PagingControl ID="uxPagingControl" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="twelve columns CategoryListDefault3">
                        <div class="CommonCategoryDataList">
                            <asp:Repeater ID="uxList" runat="server">
                                <ItemTemplate>
                                    <uc3:CategoryListItem ID="uxCategoryList" runat="server" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="twelve columns">
                        <uc:NewArrivalCategory ID="uxNewArrivalList" runat="server" />
                        <asp:Panel ID="uxCatalogControlPanel" runat="server" CssClass="CatalogControlPanel">
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxItemsPerPageControl" />
        <asp:AsyncPostBackTrigger ControlID="uxPagingControl" />
    </Triggers>
</asp:UpdatePanel>
