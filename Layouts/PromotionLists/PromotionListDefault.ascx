<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionListDefault.ascx.cs"
    Inherits="Layouts_PromotionLists_PromotionListDefault" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="~/Layouts/PromotionLists/Controls/PromotionListItemDefault.ascx"
    TagName="PromotionGroupItem" TagPrefix="uc3" %>
<div class="PromotionListDefault">
    <asp:Panel ID="uxPageControlTR" runat="server" CssClass="ProductListDefaultPageControlPanel">
        <div class="ProductListDefaultSortUpDown">
            <asp:Label ID="uxTitleLabel" runat="server" CssClass="OptionControlTitle" Text="[$SortBy]" />
            <div class="PromotionSortString">
                <asp:DropDownList ID="uxSortField" runat="server" OnSelectedIndexChanged="uxFieldSortDrop_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="Name" Value="Name" />
                    <asp:ListItem Text="Price" Value="Price" />
                </asp:DropDownList>
            </div>
            <div class="ProductItemIconSort">
                <asp:LinkButton ID="uxSortUpLink" runat="server" OnClick="uxSortUpLink_Click">
                    <asp:Image ID="uxSortUpImage" runat="server" ImageUrl="~/Images/Design/Icon/SortUp.gif" /></asp:LinkButton>
                <asp:LinkButton ID="uxSortDownLink" runat="server" OnClick="uxSortDownLink_Click">
                    <asp:Image ID="uxSortDownImage" runat="server" ImageUrl="~/Images/Design/Icon/SortDown.gif" /></asp:LinkButton></div>
        </div>
        <div class="ProductListDefaultItemPerPage">
            <uc2:ItemsPerPageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="ProductItemsPerPage" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:DataList ID="uxList" CssClass="ProductListDefaultDataList" runat="server" ShowFooter="false"
        HorizontalAlign="center">
        <HeaderStyle CssClass="ProductListDefaultDataListHeader" />
        <ItemStyle CssClass="ProductListDefaultDataListItemStyle" />
        <ItemTemplate>
            <uc3:PromotionGroupItem ID="uxItem" runat="server" />
        </ItemTemplate>
    </asp:DataList>
    <div id="uxMessageDiv" runat="server" class="CommonGridViewEmptyRowStyle" visible="false">
        <asp:Label ID="uxMessageLabel" runat="server" Font-Bold="true" />
    </div>
    <div class="ProductListDefaultPagingControl">
        <div class="ProductLinkToTopDiv">
            <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$Gototop]" />
        </div>
        <div class="ProductItemPaging">
            <uc1:PagingControl ID="uxPagingControl" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="uxSortValueHidden" runat="server" />
</div>
