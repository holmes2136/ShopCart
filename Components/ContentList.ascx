<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentList.ascx.cs" Inherits="Components_ContentList" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc2" %>
<div class="ContentList">
    <asp:Panel ID="uxPageControlTR" runat="server" CssClass="ProductListPageControlPanel">
        <div class="ProductListDefaultItemPerPage">
            <uc2:ItemsPerPageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="ProductItemsPerPage" />
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:GridView ID="uxList" runat="server" AutoGenerateColumns="False" GridLines="None"
        CssClass="CommonGridView">
        <Columns>
            <asp:TemplateField HeaderText="Result List">
                <ItemStyle CssClass="NewsTopicItemColumnTopicStyle" />
                <ItemTemplate>
                    <asp:HyperLink ID="uxTopicLink" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Vevo.UrlManager.GetContentUrl(  Eval("ContentID").ToString(),Eval("UrlName").ToString() )%>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle CssClass="NewsTopicHeaderColumnTopicStyle" />
            </asp:TemplateField>
        </Columns>
        <RowStyle CssClass="CommonGridViewRowStyle" />
        <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
        <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
    </asp:GridView>
    <div id="uxMessageDiv" runat="server" class="CommonGridViewEmptyRowStyle" visible="false">
        <asp:Label ID="uxMessageLabel" runat="server" Font-Bold="true" />
    </div>
    <div class="ProductListDefaultPagingControl">
        <div class="ProductLinkToTopDiv">
            <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$Gototop]" />
        </div>
        <div class="ProductItemPaging">
            <uc3:PagingControl ID="uxPagingControl" runat="server" />
        </div>
    </div>
</div>
