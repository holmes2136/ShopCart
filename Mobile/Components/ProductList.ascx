<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductList.ascx.cs" Inherits="Mobile_Components_ProductList" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="MobilePagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="MobileProductSorting">
    <asp:Label ID="uxSortingText" runat="server" Text="Sort By " />
    <asp:DropDownList ID="uxSortField" runat="server" OnSelectedIndexChanged="uxFieldSortDrop_SelectedIndexChanged"
        AutoPostBack="true">
        <asp:ListItem Text="" Value="SortOrder" />
        <asp:ListItem Text="Name" Value="Name" />
        <asp:ListItem Text="Price" Value="Price" />
    </asp:DropDownList>
    <asp:LinkButton ID="uxSortUpLink" runat="server" OnClick="uxSortUpLink_Click" CssClass="MobileSortUpLinkImage"></asp:LinkButton>
    <asp:LinkButton ID="uxSortDownLink" runat="server" OnClick="uxSortDownLink_Click"
        CssClass="MobileSortDownLinkImage">
    </asp:LinkButton>
</div>
<div class="MobileProductDataListDiv">
    <asp:DataList ID="uxMobileList" runat="server" ShowFooter="false" CssClass="MobileProductDataList"
        HorizontalAlign="Center" OnItemDataBound="uxMobileList_OnItemDataBound">
        <ItemTemplate>
            <table cellpadding="5" cellspacing="0" class="MobileProductList">
                <tr>
                    <td class="MobileProductListImage">
                        <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# GetProUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                            <uc1:CatalogImage ID="uxCatalogImage" runat="server" MaximumWidth="85" ImageUrl='<%# "~/" + Eval( "ImageSecondary" ).ToString() %>' />
                        </asp:HyperLink>
                    </td>
                    <td class="MobileProductListName">
                        <asp:HyperLink ID="uxProductLink1" runat="server" NavigateUrl='<%# GetProUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                            <%# Eval("Name") %>
                            <asp:Label ID="uxRetailPriceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) )%>'
                                CssClass="MobileProductListRetailPrice" Visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )%>' />
                            <span class="DiscountPercent" id="Span1" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                                <span class="MobileProductListPercentLabel">[$Percent]</span> <span class="MobileProductListPercentValue">
                                    <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
                            <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPriceFromContainer( Eval("ProductID") ) %>'
                                Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ),  Eval( "IsCallForPrice" )  ) %>'
                                CssClass="MobileProductListPrice" />
                            <asp:Label ID="uxCallForPriceLabel" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                                CssClass="MobileProductListPrice">[$CallForPrice]</asp:Label>
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle CssClass="MobileProductListItem" />
        <AlternatingItemStyle CssClass="MobileProductListAlternatingItem" />
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
