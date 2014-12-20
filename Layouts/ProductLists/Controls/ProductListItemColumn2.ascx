<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListItemColumn2.ascx.cs"
    Inherits="Components_ProductListItemColumn2" %>
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
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="CommonProductItemStyle ProductListItemColumn2">
    <asp:Panel ID="uxProductListItemColumn2Panel" runat="server" CssClass="DummyCommonProductItemStyle">
        <uc6:ProductQuickView ID="uxProductQuickView" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
        <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="PriceDiscountLabel" Visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
            <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
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
            </asp:Panel>
        </div>
        <div class="ProductListItemColumn2DetailsColumn">
            <div class="CommonProductName">
                <asp:HyperLink ID="uxNameLink" CssClass="CommonProductNameLink" NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    runat="server"> 
                    <%# LimitDisplayCharactor(Eval("Name"),26) %> 
                </asp:HyperLink>
            </div>
            <div class="CommonProductRating">
                <uc1:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <div id="uxRecurringDiv" runat="server" visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'
                class="ProductListItemColumn2RecurringDiv">
                <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
                    CssClass="ProductListItemColumn2RecurringCycles">
                    <%# "[$RecurringAmountEvery] " + ShowRecurringCycles() %>
                </asp:Panel>
                <uc5:RecurringSpecial ID="uxRecurringSpecial" runat="server" Visible='<%# IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>' />
            </div>
            <div class="CommonProductPriceDetails">
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
            <asp:Panel ID="uxStockMessagePanel" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'
                CssClass="ProductListItemColumn2OutOfStockPanel">
                <asp:Label ID="uxStockLabel" runat="server" Text='<%# CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean( Eval("UseInventory") ) ) ? "[$Out]" : "" %>'
                    ForeColor="Red"></asp:Label>
            </asp:Panel>
            <div class="CommonProductSpecialLabel">
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
            <div class="ProductListItemColumn2Message">
                <uc3:Message ID="uxMessage" runat="server" />
            </div>
            <asp:Panel ID="uxOptionGroupPanel" runat="server" CssClass="OptionGroupPanel" Visible='<%# OptionVisibleInProductList(Eval( "ProductID" ).ToString()) %>'>
                <uc2:OptionGroupDetails ID="uxOptionGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                    ValidGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>' />
            </asp:Panel>
        </div>
        <asp:Panel ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'
            CssClass="CommonProductButton">
            <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Command"
                CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                Visible='<%# !(CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
                ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                Text="[$BtnAddToCart]" CssClass="BtnStyle1" />
        </asp:Panel>
        <div class="ProductListItemColumn2TellFriendPanel">
            <asp:LinkButton ID="uxTellFriendLinkButton" runat="server" CssClass="BtnStyle5 TellFriendLinkButton"
                OnCommand="uxTellFriendButton_Command" CommandArgument='<%#Vevo.UrlManager.GetTellFriendUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                ToolTip="[$BtnTellFriend]" Text="[$BtnTellFriend]" />
        </div>
        <asp:Panel ID="uxAddtoWishListDiv" runat="server" CssClass="ProductListItemColumn2AddtoWishListPanel"
            Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>'>
            <uc4:AddtoWishListButton ID="uxAddtoWishListButton" runat="server" Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) &&
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                ProductID='<%# Eval( "ProductID" ) %>' UrlName='<%# Eval( "UrlName" ) %>' ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                Text="[$BtnAddtoWishlist]" />
        </asp:Panel>
        <asp:Panel ID="uxAddtoCompareListPanel" runat="server" CssClass="ProductListItemColumn2AddtoCompareListPanel"
            Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'>
            <uc7:AddtoCompareListButton ID="uxAddtoCompareListButton" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                UrlName='<%# Eval( "UrlName" ) %>' Text="[$BtnCompareProduct]" />
        </asp:Panel>
        <div class="AddThisWidget">
            <ucAddThis:AddThis ID="AddThis1" LinkURL='<%# UrlManager.GetProductFullUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                Title='<%# Eval("Name") %>' runat="server" />
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
