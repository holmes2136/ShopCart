<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Components/RandomProduct.ascx.cs"
    Inherits="Components_RandomProduct" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uxAddToCartNotification" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="RandomProduct" id="uxRandomProductDiv" runat="server">
    <div class="CenterBlockTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/RandomProductTopLeft.gif"
            runat="server" CssClass="CenterBlockTopImgLeft" />
        <asp:Label ID="uxRandomProductTitle" runat="server" Text="[$RandomShow]" CssClass="CenterBlockTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/RandomProductTopRight.gif"
            runat="server" CssClass="CenterBlockTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <asp:UpdatePanel ID="uxRandomListUpdate" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:DataList ID="uxRandomList" runat="server" CssClass="RandomProductDataList" CellPadding="0"
                        CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <table class="RandomProductDetailsTable" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="RandomProductImage">
                                        <asp:Panel ID="uxImagePanel" runat="server" CssClass="RandomProductImagePanel">
                                            <table class="RandomProductImage" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td valign="middle">
                                                        <asp:HyperLink ID="uxProductLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                                            CssClass="ProductLink">
                                                            <uc1:CatalogImage ID="uxImage" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                                                        </asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                            <uc6:ProductQuickView ID="ProductQuickView1" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                                        </asp:Panel>
                                    </td>
                                    <td class="RandomProductDescription">
                                        <div class="RandomProductName">
                                            <asp:HyperLink ID="uxNameLink" runat="server" CssClass="RandomProductNameLink" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                                                <%# Eval("Name") %> 
                                            </asp:HyperLink>
                                        </div>
                                        <div class="RandomProductRating">
                                            <uc1:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
                                        </div>
                                        <div class="RandomProductStock">
                                            <asp:Label ID="uxStockLabel" runat="server" ForeColor="Red" Text='<%# (Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory")))) ? "This product is out of stock " :"" %>'></asp:Label>
                                        </div>
                                        <div class="RandomProductPriceDetails">
                                            <asp:Panel ID="uxRetailPriceTR" runat="server" Visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" )  ) %>'
                                                CssClass="RandomProductRetailPricePanel">
                                                <div class="RandomProductRetailPriceLabel">
                                                    <%# GetLanguageText("Retail Price") %>:</div>
                                                <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="RandomProductPriceDiscount"
                                                    Visible='<%# CheckEqualPrice (Eval( "ProductID" )) %>'>
                                                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="RandomProductRetailPriceValue"
                                                        Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( GetRetailPrice( Eval( "ProductID" ) ) ) )%>' />
                                                    <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                                                        <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                                                            <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
                                                </asp:Panel>
                                                <div class="Clear">
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="uxCallForPriceTR" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                                                CssClass="RandomProductOurPricePanel">
                                                <div class="RandomProductOurPriceLabel">
                                                    <%# GetLanguageText( "Our Price" )%>:</div>
                                                <div class="RandomProductOurPriceValue">
                                                    <%# GetLanguageText( "CallForPrice" )%>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="uxPriceTR" runat="server" CssClass="RandomProductOurPricePanel" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  ) %>'>
                                                <div class="RandomProductOurPriceLabel">
                                                    <%# GetLanguageText( "Our Price" )%>:</div>
                                                <div class="RandomProductOurPriceValue">
                                                    <%# GetFormattedPriceFromContainer( Container ) %>
                                                </div>
                                                <div class="Clear">
                                                </div>
                                            </asp:Panel>
                                            <div class="Clear">
                                            </div>
                                        </div>
                                        <div class="RandomProductButton">
                                            <asp:LinkButton ID="uxAddToCartImageButton" runat="server" Text='<%# GetLanguageText( "BtnAddtoCart" )%>'
                                                CssClass="BtnStyle1" OnCommand="uxAddToCartImageButton_Command" CommandName='<%# Eval( "UrlName" ) %>'
                                                CommandArgument='<%# Eval( "ProductID" ) %>' Visible='<%# !CatalogUtilities.IsCatalogMode() && IsAuthorizedToViewPrice( Eval( "IsCallForPrice" ) )%>' />
                                        </div>
                                        <asp:Panel ID="ProductBestSellingRecurringPanel" runat="server" CssClass="RandomProductRecurringPanel"
                                            Visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'>
                                            <asp:HyperLink ID="uxRecurringLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                                <img src='<%# GetLanguageText( "RecurringIcon" )%>' class="RandomProductRecurringImage" /></asp:HyperLink>
                                        </asp:Panel>
                                        <asp:Panel ID="uxQuantityDiscountPanel" runat="server" Visible='<%# QuantityDiscountVisible( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>'
                                            CssClass="RandomProductQuantityDiscountPanel">
                                            <asp:Label ID="uxQuantityDiscountLabel" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]" />
                                        </asp:Panel>
                                        <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                                            class="RandomProductFreeShippingPanel">
                                            <asp:Label ID="uxFreeShippingLabel" runat="server" CssClass="FreeShippingLabel" Text="[$FreeShippingIcon]" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle CssClass="RandomProductItemStyle" />
                    </asp:DataList>
                    <div class="Clear">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/RandomProductBottomLeft.gif"
            runat="server" CssClass="CenterBlockBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/RandomProductBottomRight.gif"
            runat="server" CssClass="CenterBlockBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
<uxAddToCartNotification:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
