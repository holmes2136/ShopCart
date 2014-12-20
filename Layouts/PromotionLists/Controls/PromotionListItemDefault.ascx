<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionListItemDefault.ascx.cs"
    Inherits="Layouts_PromotionLists_Controls_PromotionListItemDefault" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="PromotionGroupListItem">
    <table class="PromotionGroupListItemTable">
        <tr>
            <td class="PromotionGroupListItemImageColumn">
                <div class="PromotionGroupListItemImageDiv">
                    <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                        <asp:Image ID="uxImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("PromotionGroupID"))%>'
                            CssClass="NoBorder" />
                    </asp:HyperLink>
                </div>
            </td>
            <td class="PromotionGroupListItemDetailsColumn">
                <div class="PromotionGroupListItemDetailsDiv">
                    <div class="PromotionGroupListItemNameDiv">
                        <asp:HyperLink ID="uxNameLink" runat="server" CssClass="PromotionGroupListItemNameLink"
                            NavigateUrl='<%# Vevo.UrlManager.GetPromotionUrl( Eval( "PromotionGroupID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            <%# Eval("Name") %> 
                        </asp:HyperLink>
                    </div>
                    <div class="PromotionGroupListItemDescription">
                        <%# Eval("Description") %>
                    </div>
                    <div class="PromotionGroupListItemDetailsPriceDiv">
                        <div class="PromotionGroupListItemPriceDiv">
                            <asp:Panel ID="uxRetailPriceTR" runat="server" CssClass="PromotionGroupListItemPricePanel"
                                Visible='<%# IsAuthorizedToViewPrice() %>'>
                                <div class="PromotionGroupListItemPriceLabel">
                                    [$Promotion Price]
                                </div>
                                <div class="PromotionGroupListItemPriceValue">
                                    <%# StoreContext.Currency.FormatPrice( Eval( "Price" ) )%>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="PromotionGroupListItemButtonDiv">
                            <asp:PlaceHolder ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'>
                                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnCommand="uxAddToCartImageButton_Command"
                                    CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "PromotionGroupID" ) %>'
                                    Visible='<%# !IsCatalogMode() && IsAuthorizedToViewPrice() %>' CssClass="PromotionGroupListItemAddToCartImage BtnStyle1"
                                    Text="[$BtnAddtoCart]" />
                            </asp:PlaceHolder>
                            <asp:LinkButton ID="uxTellFriendLinkButton" runat="server" CssClass="BtnStyle5 TellFriendLinkButton"
                                Text="[$BtnTellFriend]" OnCommand="uxTellFriendLinkButton_Command" CommandArgument='<%# Eval( "PromotionGroupID" ) %>' />
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
