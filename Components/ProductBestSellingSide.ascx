<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Components/ProductBestSellingSide.ascx.cs"
    Inherits="Components_ProductBestSellingSide" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="ProductBestSelling" id="uxProductBestSellingDiv" runat="server">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/ProductBestSellingTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxProductBestSellingTitle" runat="server" Text="[$ProductBestSelling]"
            CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/ProductBestSellingTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxBestSellingList" runat="server" RepeatColumns="1" RepeatDirection="Vertical"
                CellSpacing="0" CellPadding="0" CssClass="ProductBestSellingDatalist" GridLines="None">
                <ItemTemplate>
                    <div class="ProductBestSellingItem">
                        <div class="ProductBestSellingDetails">
                            <div class="ProductBestSellingName">
                                <asp:HyperLink ID="uxNameLink" runat="server" CssClass="ProductBestSellingNameLink"
                                    NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            <%# Eval("Name") %> 
                                </asp:HyperLink>
                            </div>
                            <div class="ProductBestSellingImage">
                                <div class="ProductBestSellingImageDiv">
                                    <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                        <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval("ImageSecondary").ToString() %>' />
                                    </asp:HyperLink></div>
                            </div>
                            <div class="ProductBestSellingPriceDetails">
                                <asp:Panel ID="uxRetailPriceTR" runat="server" Visible='<%# IsAuthorizedToViewPrice() && IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" )  ) %>'
                                    CssClass="ProductBestSellingRetailPrice">
                                    <div class="ProductBestSellingRetailPriceLabel">
                                        [$Retail Price]:</div>
                                    <div class="ProductBestSellingRetailPriceValue">
                                        <%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) )%>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="uxPriceTR" runat="server" Visible='<%# IsAuthorizedToViewPrice() && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  ) %>'
                                    CssClass="ProductBestSellingOurPrice">
                                    <div class="ProductBestSellingOurPriceLabel">
                                        [$Our Price]:</div>
                                    <div class="ProductBestSellingOurPriceValue">
                                        <%# GetFormattedPriceFromContainer( Container ) %>
                                    </div>
                                </asp:Panel>
                                <div class="ProductBestSellingButton">
                                    <ucLinkButton:LinkButton ID="uxAddToCartImageButton" runat="server" OnCommandBubbleEvent="uxAddToCartImageButton_Command"
                                        CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                                        Visible='<%# !CatalogUtilities.IsCatalogMode() && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
                                        ThemeImageUrl="[$BuyButton]" CssClassImage="NoBorder" />
                                </div>
                                <div class="ProductBestSellingStock">
                                    <asp:Label ID="uxStockLabel" runat="server" ForeColor="Red" Text='<%# Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) ? "This product is out of stock " : "" %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="BestsellingQuantityDiscount" runat="server" CssClass="ProductBestSellingQuantityDiscount"
                        Visible='<%# CatalogUtilities.GetQuantityDiscountByProductID( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>'>
                        <asp:HyperLink ID="uxQuantityDiscountLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'><img src="[$DiscountIcon]" class="ProductBestSellingQuantityDiscountImage" /></asp:HyperLink>
                    </asp:Panel>
                    <asp:Panel ID="ProductBestSellingRecurringPanel" runat="server" CssClass="ProductBestSellingRecurringProduct"
                        Visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'>
                        <asp:HyperLink ID="uxRecurringLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'><img src="[$RecurringIcon]" class="ProductBestSellingRecurringProductImage" /></asp:HyperLink>
                    </asp:Panel>
                </ItemTemplate>
                <ItemStyle CssClass="ProductBestSellingDatalistItemStyle" />
            </asp:DataList>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/ProductBestSellingBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/ProductBestSellingBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
