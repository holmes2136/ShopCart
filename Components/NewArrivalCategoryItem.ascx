<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewArrivalCategoryItem.ascx.cs"
    Inherits="Components_NewArrivalCategoryItem" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc7" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="CommonProductItemStyle NewArrivalCategoryItem" style="position: relative">
    <asp:Panel ID="uxNewArrivalItemPanel" runat="server">
        <uc:ProductQuickView ID="uxProductQuickView" runat="server" ProductID='<%# Eval( "ProductID" ).ToString() %>' />
        <div class="NewArrivalItem">
            <asp:Label ID="uxNewArrivalLabel" runat="server" CssClass="NewArrivalLabel" Text="NEW" />
            <div class="CommonProductImage NewArrivalItemImage">
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
                            <%# LimitDisplayCharactor(Eval("Name") , 26) %> 
                </asp:HyperLink>
            </div>
            <div class="CommonProductRating">
                <uc:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <div class="CommonProductPriceDetails">
                <asp:Panel ID="uxRetailPrice" runat="server" CssClass="RetailPricePanel" Visible='<%# (IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) ) && !IsDynamicProductKitPrice(BindingContainer)) || CheckEqualPrice (Eval( "ProductID" ))%>'>
                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="NewArrivalItemColumnRetailPriceValue"
                        Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( GetRetailPrice( Eval( "ProductID" ) ) ) )%>' />
                </asp:Panel>
                <asp:Panel ID="uxPricePanel" runat="server" CssClass="OurPricePanel" Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  ) %>'>
                    <div class="OurPriceValue">
                        <%# GetFormattedPrice(Eval("ProductID"))%>
                    </div>
                </asp:Panel>
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
                <uc4:addtowishlistbutton id="uxAddtoWishListButton" runat="server" visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) &&
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                    productid='<%# Eval( "ProductID" ) %>' urlname='<%# Eval( "UrlName" ) %>' validationgroup='<%# String.Format( "ProductValid{0}", Eval( "ProductID" ).ToString() ) %>'
                    text="[$BtnAddtoWishlist]" />
            </asp:Panel>
            <asp:Panel ID="uxAddtoCompareListPanel" runat="server" CssClass="ProductListItemColumn2AddtoCompareListPanel"
                Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'>
                <uc7:addtocomparelistbutton id="uxAddtoCompareListButton" runat="server" productid='<%# Eval( "ProductID" ) %>'
                    urlname='<%# Eval( "UrlName" ) %>' text="[$BtnCompareProduct]" />
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="uxNotificationUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uxAddToCartImageButton" />
        </Triggers>
    </asp:UpdatePanel>
</div>
