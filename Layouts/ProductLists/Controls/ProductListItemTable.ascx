<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListItemTable.ascx.cs"
    Inherits="Components_ProductListItemTable" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc7" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uc8" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<table cellpadding="5" cellspacing="0" class="ProductListTableViewItem">
    <tr>
        <td class="Image">
            <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="PriceDiscountLabel" Visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                <span class="DiscountPercent" id="Span1" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                    <span class="PercentValue">
                        <%# GetDiscountPercent( Eval( "ProductID" ))%></span> <span class="PercentLabel">[$Off]</span>
                </span>
            </asp:Panel>
            <asp:Panel ID="uxImagePanel" runat="server" CssClass="ImagePanel">
                 <uc6:ProductQuickView ID="ProductQuickView1" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                <table class="Image1">
                    <tr>
                        <td valign="middle">
                            <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                CssClass="ProductLink">
                                <uc1:CatalogImage ID="uxCatalogImage" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
               
            </asp:Panel>
        </td>
        <td class="Name">
            <div class="CommonProductName">
                <asp:HyperLink ID="uxNameLink" CssClass="CommonProductNameLink" NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    runat="server"> 
                    <%# Eval("Name") %> 
                </asp:HyperLink>
            </div>
            <div class="CommonProductRating">
                <uc1:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <asp:Panel ID="uxProductListItemQuantityDiscountPanel" runat="server" Visible='<%# QuantityDiscountVisible( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>'
                CssClass="ProductDiscount">
                <asp:Label ID="uxQuantityDiscountPanel" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]" />
            </asp:Panel>
            <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                class="ProductDiscount">
                <asp:Label ID="Label1" runat="server" CssClass="FreeShippingLabel" Text="[$FreeShippingIcon]" />
            </div>
        </td>
        <td class="Sku">
            <asp:Label ID="uxStockLabel" runat="server" Text='<%# CheckValidStock ( Eval("UseInventory"), Eval("SumStock"))%>' />
        </td>
        <td class="Price">
            <asp:Panel ID="uxCallForPriceTR" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                CssClass="OurPricePanel">
                <div class="CallFroPrice">
                    [$CallForPrice]
                </div>
            </asp:Panel>
            <asp:Panel ID="uxRetailPriceTR" runat="server" Visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) ) && !IsDynamicProductKitPrice(BindingContainer)%>'
                CssClass="RetailPricePanel">
                <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="RetailPriceValue">
                        <%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) )%>
                </asp:Label>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxOurPriceDiv" runat="server" CssClass="OurPricePanel" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) )&& !IsDynamicProductKitPrice(BindingContainer) %>'>
                <div class="OurPriceValue">
                    <asp:Label ID="uxPriceLabel" runat="server" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ) ) %>'
                        Text='<%# GetFormattedPriceFromContainer( BindingContainer ) %>' />
                </div>
                <div class="Clear">
                </div>
            </asp:Panel>
        </td>
        <td class="Button">
            <asp:PlaceHolder ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'>
                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Command"
                    CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                    Visible='<%# !(CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
                    ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                    Text="[$BtnAddToCart]" CssClass="BtnStyle1" />
            </asp:PlaceHolder>
            <div class="TableViewTellFriendPanel">
                <asp:LinkButton ID="uxTellFriendLinkButton" runat="server" CssClass="BtnStyle5 TellFriendLinkButton"
                    OnCommand="uxTellFriendButton_Command" CommandArgument='<%#Vevo.UrlManager.GetTellFriendUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    ToolTip="[$BtnTellFriend]" Text="[$BtnTellFriend]" />
            </div>
            <asp:Panel ID="uxAddtoWishListDiv" runat="server" CssClass="TableViewAddtoWishListPanel"
                Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>'>
                <uc4:AddtoWishListButton ID="uxAddtoWishListButton" runat="server" Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) &&
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                    ProductID='<%# Eval( "ProductID" ) %>' UrlName='<%# Eval( "UrlName" ) %>' ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                    Text="[$BtnAddtoWishlist]" />
            </asp:Panel>
            <asp:Panel ID="uxAddtoCompareListPanel" runat="server" CssClass="TableViewAddtoCompareListPanel"
                Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'>
                <uc7:AddtoCompareListButton ID="uxAddtoCompareListButton" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                    UrlName='<%# Eval( "UrlName" ) %>' Text="[$BtnCompareProduct]" />
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="uxNotificationUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <uc8:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxAddToCartImageButton" />
    </Triggers>
</asp:UpdatePanel>
