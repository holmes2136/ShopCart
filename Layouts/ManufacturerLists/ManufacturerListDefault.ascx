<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerListDefault.ascx.cs"
    Inherits="Layouts_ManufacturerLists_ManufacturerListDefault" %>
<%@ Register Src="~/Layouts/ManufacturerLists/Controls/ManufacturerListItemDefault.ascx"
    TagName="ManufacturerListItem" TagPrefix="uc3" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsperpageDrop"
    TagPrefix="uc2" %>
<asp:UpdatePanel ID="uxManufacturerListUpdate" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <div class="ManufacturerListItemDefault">
            <div class="ManufacturerListDefaultPageItemControlDiv" id="uxManufacturerPageControlDiv"
                runat="server">
                <div class="ManufacturerListDefaultItemPerPageDiv">
                    <uc2:ItemsperpageDrop ID="uxItemsPerPageControl" runat="server" PageListConfig="ManufacturerItemsPerPage" />
                </div>
                <div class="ProductItemPaging">
                    <uc3:PagingControl ID="uxPagingControl" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <asp:DataList ID="uxList" CssClass="ManufacturerListDefaultDataList" runat="server"
                ShowFooter="False">
                <HeaderStyle CssClass="ManufacturerListDefaultDataListHeader" />
                <ItemStyle CssClass="ManufacturerListDefaultDataListItem" />
                <ItemTemplate>
                    <uc3:ManufacturerListItem ID="uxManufacturerList" runat="server" />
                </ItemTemplate>
            </asp:DataList>
            <div class="ProductListDefaultPagingControl">
                <div class="ProductLinkToTopDiv">
                    <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$Gototop]" />
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxItemsPerPageControl" />
        <asp:AsyncPostBackTrigger ControlID="uxPagingControl" />
    </Triggers>
</asp:UpdatePanel>
