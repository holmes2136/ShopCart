<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListRowandGridStyle2.ascx.cs"
    Inherits="Layouts_ProductLists_ProductListRowandGridStyle2" %>
<%@ Register Src="~/Components/ProductListViewType.ascx" TagName="ProductListViewType"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/ProductListItem.ascx" TagName="ProductListItem" TagPrefix="uc1" %>
<%@ Register Src="Controls/ProductListItemTable.ascx" TagName="ProductListTable"
    TagPrefix="uc6" %>
<%@ Register Src="Controls/ProductListItemColumn2.ascx" TagName="ProductListItemColumn2"
    TagPrefix="uc4" %>
<div class="ProductListDefault">
    <asp:UpdatePanel ID="uxProductListUpdate" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row">
                <div class="CategoryDescriptionDiv">
                    <asp:Label ID="uxCategoryDescriptionText" runat="server" CssClass="CategoryDescriptionLeaf"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="uxPageControlTR" runat="server" CssClass="ProductListDefaultPageControlPanel">
                <div class="row">
                    <div class="twelve columns">
                        <div class="ProductListDefaultSortString">
                            <uc5:ProductListViewType ID="uxProductListViewType" runat="server" ProductColumnConfig="NumberOfCategoryColumn" />
                        </div>
                        <div class="ProductListGridListSortUpDown">
                            <asp:Label ID="uxTitleLabel" runat="server" CssClass="OptionControlTitle" Text="[$SortBy]"></asp:Label>
                            <asp:DropDownList ID="uxSortField" runat="server" CssClass="ProductItemDropSort"
                                OnSelectedIndexChanged="uxFieldSortDrop_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="" Value="SortOrder" />
                                <asp:ListItem Text="Name" Value="Name" />
                                <asp:ListItem Text="Price" Value="Price" />
                            </asp:DropDownList>
                            <div class="ProductItemIconSort">
                                <asp:LinkButton ID="uxSortUpLink" runat="server" OnClick="uxSortUpLink_Click">
                                    <asp:Image ID="uxSortUpImage" runat="server" ImageUrl="~/Images/Design/Icon/SortUp.gif" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="uxSortDownLink" runat="server" OnClick="uxSortDownLink_Click">
                                    <asp:Image ID="uxSortDownImage" runat="server" ImageUrl="~/Images/Design/Icon/SortDown.gif" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="ProductListGridListItemPerPage">
                            <uc2:ItemsPerPageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="ProductItemsPerPage" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxDataListPanel" runat="server">
                <div class="row">
                    <div class="product-productlist-row columns">
                        <div class="ProductRowDataList">
                            <asp:Repeater ID="uxList" runat="server">
                                <ItemTemplate>
                                    <uc1:ProductListItem ID="uxItem" runat="server" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxGridViewPanel" runat="server">
                <div class="row">
                    <div class="product-productlist-col columns">
                        <div class="ProductColumn2DataList">
                            <asp:Repeater ID="uxList2" runat="server">
                                <ItemTemplate>
                                    <uc4:ProductListItemColumn2 ID="uxItemGrid" runat="server" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="uxTableViewPanel" runat="server">
                <div class="row">
                    <div class="product-productlist-row columns">
                        <asp:DataList ID="uxTableList" CssClass="ProductTableDataList" runat="server" RepeatColumns="1"
                            CellPadding="0" CellSpacing="0" ShowFooter="False" RepeatDirection="Horizontal">
                            <HeaderStyle CssClass="ProductListTableViewHeader" />
                            <ItemStyle CssClass="ProductListTableViewItem" />
                            <ItemTemplate>
                                <uc6:ProductListTable ID="uxItemTable" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0" class="ProductListTableViewTopItem">
                                    <tr>
                                        <td class="Image">&nbsp;
                                        </td>
                                        <td class="Name">
                                            <%# GetLanguageText( "TableViewName" )%>
                                        </td>
                                        <td class="Sku">
                                            <%# GetLanguageText( "TableViewStock" )%>
                                        </td>
                                        <td class="Price">
                                            <%# GetLanguageText( "TableViewPrice" )%>
                                        </td>
                                        <td class="Button">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </asp:Panel>
            <div id="uxMessageDiv" runat="server" class="CommonGridViewEmptyRowStyle" visible="false">
                <asp:Label ID="uxMessageLabel" runat="server" />
            </div>
            <div class="row">
                <div class="twelve columns">
                    <div class="ProductListDefaultPagingControl">
                        <asp:Label ID="uxItemCounLabel" runat="server" CssClass="ProductItemCountItemCount" />
                        <div class="ProductLinkToTopDiv">
                            <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$GoToTop]" />
                        </div>
                        <div class="ProductItemPaging">
                            <uc3:PagingControl ID="uxPagingControl" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxSortField" />
            <asp:AsyncPostBackTrigger ControlID="uxSortUpLink" />
            <asp:AsyncPostBackTrigger ControlID="uxSortDownLink" />
            <asp:AsyncPostBackTrigger ControlID="uxItemsPerPageControl" />
            <asp:AsyncPostBackTrigger ControlID="uxPagingControl" />
            <asp:AsyncPostBackTrigger ControlID="uxProductListViewType" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="uxSortValueHidden" runat="server" />
</div>
