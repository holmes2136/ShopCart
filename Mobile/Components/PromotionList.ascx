<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionList.ascx.cs"
    Inherits="Mobile_Components_PromotionList" %>
<%@ Register Src="MobilePagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:QuickSearch ID="uxQuickSearch" runat="server" />
<div class="MobilePromotionSorting">
    <asp:Label ID="uxSortingText" runat="server" Text="Sort By " />
    <asp:DropDownList ID="uxSortField" runat="server" OnSelectedIndexChanged="uxFieldSortDrop_SelectedIndexChanged"
        AutoPostBack="true">
        <asp:ListItem Text="Name" Value="Name" />
        <asp:ListItem Text="Price" Value="Price" />
    </asp:DropDownList>
    <asp:LinkButton ID="uxSortUpLink" runat="server" OnClick="uxSortUpLink_Click" CssClass="MobileSortUpLinkImage"></asp:LinkButton>
    <asp:LinkButton ID="uxSortDownLink" runat="server" OnClick="uxSortDownLink_Click"
        CssClass="MobileSortDownLinkImage">
    </asp:LinkButton>
</div>
<div class="MobilePromotionListDiv">
    <asp:DataList ID="uxMobileList" runat="server" ShowFooter="false" CssClass="MobilePromotionDataList"
        OnItemDataBound="uxMobileList_OnItemDataBound" HorizontalAlign="Center">
        <ItemTemplate>
            <table cellpadding="5" cellspacing="0" class="MobilePromotionList">
                <tr>
                    <td class="MobilePromotionListImage">
                        <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetMobilePromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                            <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("PromotionGroupID"))%>'
                                Width="85" />
                        </asp:HyperLink>
                    </td>
                    <td class="MobilePromotionListName">
                        <asp:HyperLink ID="uxProductLink1" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetMobilePromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                            <%# Eval("Name") %>
                            <asp:Label ID="uxPromotionPrice" runat="server" CssClass="MobilePromotionListPrice"
                                Visible='<%# IsAuthorizedToViewPrice() %>'><%# StoreContext.Currency.FormatPrice( Eval( "Price" ) )%></asp:Label>
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle CssClass="MobilePromotionListItem" />
        <AlternatingItemStyle CssClass="MobilePromotionListAlternatingItem" />
    </asp:DataList>
</div>
<div class="MobilePagingControlMainDiv">
    <table cellpadding="0" cellspacing="0" class="MobilePagingControl">
        <tr>
            <uc3:PagingControl ID="uxMobilePagingControl" runat="server" />
        </tr>
    </table>
</div>
<asp:HiddenField ID="uxSortValueHidden" runat="server" />
