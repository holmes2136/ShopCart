<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListItem.ascx.cs"
    Inherits="Components_ProductListItem" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc7" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="~/Components/OptionGroupDetails.ascx" TagName="OptionGroupDetails"
    TagPrefix="uc2" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/AddThis.ascx" TagName="AddThis" TagPrefix="ucAddThis" %>
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
<div class="CommonProductItemStyle ProductListItem">
    <asp:Panel ID="uxProductListItemPanel" runat="server" CssClass="DummyCommonProductItemStyle">
        <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="PriceDiscountLabel" Visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
            <span class="DiscountPercent" id="Span1" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                <span class="PercentValue">
                    <%# GetDiscountPercent( Eval( "ProductID" ))%></span> <span class="PercentLabel">[$Off]</span>
            </span>
        </asp:Panel>
        <div class="CommonProductImage">
            <asp:Panel ID="uxImagePanel" runat="server" CssClass="CommonProductImagePanel">
                <table class="CommonProductImage" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td valign="middle">
                            <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                CssClass="ProductLink">
                                <uc1:CatalogImage ID="uxImage" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <uc6:ProductQuickView ID="uxProductQuickView" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
            </asp:Panel>
        </div>
        <div class="ProductListItemDetailsColumn">
            <div class="ProductListItemDetailsDiv">
                <div class="CommonProductName">
                    <asp:HyperLink ID="uxNameLink" CssClass="CommonProductNameLink" NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        runat="server"> 
                    <%# Eval("Name") %> 
                    </asp:HyperLink>
                </div>
                <div class="CommonProductDescription">
                    <%# Eval("ShortDescription") %>
                </div>
                <div class="CommonProductRating">
                    <uc1:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
                </div>
                <div class="ProductListItemDetailsPriceDiv">
                    <div id="uxRecurringDiv" runat="server" visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'
                        class="ProductListItemRecurringDiv">
                        <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
                            CssClass="ProductListItemRecurringCycles">
                            <%# "[$RecurringAmountEvery] " + ShowRecurringCycles() %>
                        </asp:Panel>
                        <uc5:RecurringSpecial ID="uxRecurringSpecail" runat="server" Visible='<%# IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>' />
                    </div>
                    <div class="ProductListItemPriceDiv">
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
                            <div class="OurPriceLabel">
                                <asp:Label ID="uxPriceHeadLabel" runat="server">[$Our Price]</asp:Label>
                            </div>
                            <div class="OurPriceValue">
                                <asp:Label ID="uxPriceLabel" runat="server" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ) ) %>'
                                    Text='<%# GetFormattedPriceFromContainer( BindingContainer ) %>' />
                            </div>
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="ProductListItemSpecialLabel">
                    <div id="uxProductListItemQuantityDiscountPanel" runat="server" visible='<%# QuantityDiscountVisible( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>'
                        class="ProductListItemQuantityDiscountPanel">
                        <asp:Label ID="uxQuantityDiscountPanel" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]" />
                    </div>
                    <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                        class="ProductListItemQuantityDiscountPanel">
                        <asp:Label ID="Label1" runat="server" CssClass="FreeShippingLabel" Text="[$FreeShippingIcon]"
                            Visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>' />
                    </div>
                </div>
                <asp:Panel ID="uxRmaMessagePanel" runat="server" CssClass="ProductListItemRmaPanel"
                    Visible="<%# IsRmaEmable() %>">
                    <asp:Label ID="uxRmaMessageLabel" runat="server" Text='<%# RmaMessage( Eval( "ReturnTime" ) ) %>' />
                </asp:Panel>
                <asp:Panel ID="uxStockPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'
                    CssClass="ProductListItemOutOfStockPanel">
                    <asp:Label ID="uxStockLabel" runat="server" Text='<%# (CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) ? "[$Out]" : "" %>'></asp:Label>
                </asp:Panel>
                <asp:Panel ID="uxOptionGroupPanel" runat="server" CssClass="ProductListItemOptionGroupPanel"
                    Visible='<%# OptionVisibleInProductList(Eval( "ProductID" ).ToString()) %>'>
                    <uc2:OptionGroupDetails ID="uxOptionGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                        ValidGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>' />
                </asp:Panel>
                <div class="ProductListItemMessageDiv">
                    <uc3:Message ID="uxMessage" runat="server" />
                </div>
            </div>
            <div class="ProductListItemButtonDiv">
                <asp:PlaceHolder ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'>
                    <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Command"
                        CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                        Visible='<%# !(CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
                        ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                        Text="[$BtnAddToCart]" CssClass="BtnStyle1" />
                </asp:PlaceHolder>
                <div class="ProductListItemTellFriendPanel">
                    <asp:LinkButton ID="uxTellFriendLinkButton" runat="server" CssClass="BtnStyle5 TellFriendLinkButton"
                        OnCommand="uxTellFriendButton_Command" CommandArgument='<%#Vevo.UrlManager.GetTellFriendUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        ToolTip="[$BtnTellFriend]" Text="[$BtnTellFriend]" />
                </div>
                <asp:Panel ID="uxAddtoWishListDiv" runat="server" CssClass="ProductListItemAddtoWishListPanel"
                    Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>'>
                    <uc4:AddtoWishListButton ID="uxAddtoWishListButton" runat="server" Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) &&
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                        ProductID='<%# Eval( "ProductID" ) %>' UrlName='<%# Eval( "UrlName" ) %>' ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                        Text="[$BtnAddtoWishlist]" />
                </asp:Panel>
                <asp:Panel ID="uxAddtoCompareListPanel" runat="server" CssClass="ProductListItemAddtoCompareListPanel"
                    Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'>
                    <uc7:AddtoCompareListButton ID="uxAddtoCompareListButton" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                        UrlName='<%# Eval( "UrlName" ) %>' Text="[$BtnCompareProduct]" />
                </asp:Panel>
                <div class="AddThisWidget">
                    <ucAddThis:AddThis ID="AddThis2" LinkURL='<%# UrlManager.GetProductFullUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        Title='<%# Eval("Name") %>' runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="uxNotificationUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc8:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxAddToCartImageButton" />
        </Triggers>
    </asp:UpdatePanel>
</div>
