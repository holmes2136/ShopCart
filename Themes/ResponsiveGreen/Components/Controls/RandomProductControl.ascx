<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RandomProductControl.ascx.cs" Inherits="Themes_ResponsiveGreen_Components_Controls_RandomProductControl" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uxAddToCartNotification" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc1" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc7" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="CommonProductItemStyle">
    <asp:Panel ID="uxRandomProductItemPanel" runat="server" CssClass="DummyCommonProductItemStyle">
        <uc6:ProductQuickView ID="uxProductQuickView" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
        <div class="RandomProductItem">
            <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="PriceDiscountLabel" Visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                <span class="DiscountPercent" id="Span1" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                    <span class="PercentValue">
                        <%# GetDiscountPercent( Eval( "ProductID" ))%></span> <span class="PercentLabel">[$Off]</span>
                </span>
            </asp:Panel>
            <div class="CommonProductImage RandomProductImage">
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
            <div class="CommonProductName">
                <asp:HyperLink ID="uxNameLink" runat="server" CssClass="CommonProductNameLink" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                                                <%# LimitDisplayCharactor(Eval("Name"),26) %> 
                </asp:HyperLink>
            </div>
            <div class="CommonProductRating">
                <uc1:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <div class="CommonProductPriceDetails">
                <asp:Panel ID="uxRetailPricePanel" runat="server" CssClass="RetailPricePanel" Visible='<%# CheckEqualPrice (Eval( "ProductID" )) %>'>
                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="RetailPriceValue" Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( GetRetailPrice( Eval( "ProductID" ) ) ) )%>' />
                </asp:Panel>
                <asp:Panel ID="uxCallForPriceTR" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                    CssClass="OurPricePanel">
                    <div class="OurPriceValue">
                        <%# GetLanguageText( "CallForPrice" )%>
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxPriceTR" runat="server" CssClass="OurPricePanel" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  ) %>'>
                    <div class="OurPriceValue">
                        <%# GetFormattedPriceFromContainer( Container ) %>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxRamdomProductRecurringPanel" runat="server" CssClass="CommonProductRecurringPanel"
                Visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'>
                <asp:HyperLink ID="uxRecurringLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                                <img src='<%# GetLanguageText( "RecurringIcon" )%>' class="RecurringImage" /></asp:HyperLink>
            </asp:Panel>
            <asp:Panel ID="uxQuantityDiscountPanel" runat="server" Visible='<%# QuantityDiscountVisible( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>'
                CssClass="CommonProductQuantityDiscountPanel">
                <asp:Label ID="uxQuantityDiscountLabel" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]" />
            </asp:Panel>
            <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                class="CommonProductFreeShippingPanel">
                <asp:Label ID="uxFreeShippingLabel" runat="server" CssClass="FreeShippingLabel" Text="[$FreeShippingIcon]" />
            </div>
        </div>
        <asp:Panel ID="uxAddtoCartPlaceHolder" runat="server" Visible='<%# !CatalogUtilities.IsCatalogMode() %>'>
            <div class="CommonProductButton">
                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Command"
                    CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                    Visible='<%# !(CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
                    ValidationGroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                    Text="[$BtnAddtoCart]" CssClass="BtnStyle1" />
            </div>
        </asp:Panel>
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
    </asp:Panel>

    <asp:UpdatePanel ID="uxNotificationUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uxAddToCartNotification:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxAddToCartImageButton" />
        </Triggers>
    </asp:UpdatePanel>
</div>

