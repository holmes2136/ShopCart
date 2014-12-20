<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogListRowStyle.ascx.cs"
    Inherits="Layouts_BlogLists_BlogListRowStyle" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/BlogListItem.ascx" TagName="BlogListItem" TagPrefix="uc3" %>
<div class="BlogListDefault">
    <asp:Panel ID="uxPageControlTR" runat="server" CssClass="BlogListDefaultPageControlPanel">
        <div class="BlogListDefaultItemPerPage">
            <uc2:ItemsPerPageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="BlogListItemsPerPage" />
        </div>
        <div class="BlogListDefaultPagingControl">
            <uc1:PagingControl ID="uxPagingControl" runat="server" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:DataList ID="uxList" CssClass="BlogListDefaultDataList" runat="server" ShowFooter="false"
        HorizontalAlign="center">
        <HeaderStyle CssClass="BlogListDefaultDataListHeader" />
        <ItemStyle CssClass="BlogListDefaultDataListItemStyle" />
        <ItemTemplate>
            <uc3:BlogListItem ID="uxItem" runat="server" />
        </ItemTemplate>
    </asp:DataList>
    <div id="uxBlogListNoDataDiv" runat="server" class="BlogNoData" visible="false">
        <asp:Label ID="uxMessageLabel" runat="server" />
    </div>
    <asp:Panel ID="uxPageControlTRBottom" runat="server" CssClass="BlogListDefaultPageControlPanel">
        <div class="BlogListDefaultItemPerPage">
            <uc2:ItemsPerPageDrop ID="uxItemsPerPageControlBottom" runat="server" PageListConfig="BlogListItemsPerPage" />
        </div>
        <div class="BlogListDefaultPagingControl">
            <uc1:PagingControl ID="uxPagingControlBottom" runat="server" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
</div>
