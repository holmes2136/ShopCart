<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductBestSelling.ascx.cs"
    Inherits="Components_ProductBestSelling" %>
<%@ Register Src="DotLine.ascx" TagName="DotLine" TagPrefix="uc2" %>
<%@ Register Src="CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="ProductBestSelling" id="uxProductBestSellingDiv" runat="server">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxTitle" runat="server" Text="[$ProductBestSelling]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxBestSellingList" runat="server" RepeatDirection="Horizontal"
                RepeatColumns="1" CellSpacing="5" CellPadding="5" CssClass="ProductBestSellingDataList slides_container"
                GridLines="None">
                <ItemTemplate>
                    <div class="ProductBestSellingImage">
                        <div class="ProductBestSellingImageDiv">
                            <asp:Panel ID="uxImagePanel" runat="server">
                                <asp:HyperLink ID="uxImageLink" runat="server" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                    <uc1:CatalogImage ID="CatalogImage1" runat="server" ImageUrl='<%# Eval("ImageSecondary").ToString() %>' />
                                </asp:HyperLink>
                            </asp:Panel>
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
                    <div class="ProductBestSellingItem">
                        <div class="ProductBestSellingName">
                            <asp:HyperLink ID="uxNameLink" runat="server" CssClass="ProductBestSellingNameLink"
                                NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            <%# Eval("Name") %> 
                            </asp:HyperLink>
                        </div>
                        <div class="ProductBestSellingPriceDetails">
                            <asp:Panel ID="uxRetailPriceTR" runat="server" Visible='<%# IsAuthorizedToViewPrice() && CheckEqualPrice (Eval( "ProductID" )) || IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ) ) %>'
                                CssClass="ProductBestSellingRetailPrice">
                                <div class="ProductBestSellingRetailPriceLabel">
                                    [$Retail Price]:</div>
                                <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="ProductBestSellingPriceDiscount"
                                    Visible='<%# CheckEqualPrice (Eval( "ProductID" )) %>'>
                                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="ProductBestSellingRetailPriceValue"
                                        Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( GetRetailPrice( Eval( "ProductID" ) ) ) )%>' />
                                    <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                                        <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                                            <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
                                </asp:Panel>
                                <div class="Clear">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="uxCallForPriceTR" runat="server">
                                <div class="ProductBestSellingOurPriceLabel">
                                    [$CallForPrice]:</div>
                                <div class="Clear">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="uxPriceTR" runat="server" Visible='<%# IsAuthorizedToViewPrice() && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ) ) %>'
                                CssClass="ProductBestSellingOurPrice">
                                <div class="ProductBestSellingOurPriceLabel">
                                    [$Our Price]:</div>
                                <div class="ProductBestSellingOurPriceValue">
                                    <%# GetFormattedPriceFromContainer( Container ) %>
                                </div>
                                <div class="Clear">
                                </div>
                            </asp:Panel>
                            <div class="Clear">
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <ItemStyle CssClass="ProductBestSellingDatalistItemStyle" />
            </asp:DataList>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CategoryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CategoryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
