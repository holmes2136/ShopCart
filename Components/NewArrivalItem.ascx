<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewArrivalItem.ascx.cs"
    Inherits="Components_NewArrivalItem" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Label ID="uxNewArrivalLabel" runat="server" CssClass="NewArrivalLabel" Text="[$NewItem]" />
<table class="NewArrivalItem" cellpadding="0" cellspacing="0">
    <tr>
        <td class="NewArrivalItemImage">
            <asp:Panel ID="uxImagePanel" runat="server" CssClass="NewArrivalItemImagePanel">
                <table class="NewArrivalItemImage" cellpadding="0">
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
    </tr>
    <tr>
        <td class="NewArrivalItemColumn">
            <div class="NewArrivalItemSpecialLabel">
                 <asp:Label ID="uxQuantityDiscountLabel" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]"
                Visible='<%# CatalogUtilities.GetQuantityDiscountByProductID( Eval("DiscountGroupID"), Eval("CategoryIDs") ) %>' />
            <asp:Label ID="uxFreeShippingLabel" runat="server" CssClass="FreeShippingLabel" Text="[$FreeShippingIcon]"
                Visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>' />
            </div>
            <div class="NewArrivalNameItemDiv">
                <asp:HyperLink ID="uxNameLink" runat="server" CssClass="NewArrivalNameLink" NavigateUrl='<%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'> 
                            <%# Eval("Name") %> 
                </asp:HyperLink>
            </div>
            <asp:Panel ID="uxRetailPrice" runat="server" Visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) ) && !IsDynamicProductKitPrice(BindingContainer)%>'
                CssClass="NewArrivalItemColumnPricePanel">
                <asp:Panel ID="uxDiscountPercent" runat="server" CssClass="NewArrivalItemPriceDiscount"
                    Visible='<%# CheckEqualPrice (Eval( "ProductID" )) %>'>
                    <asp:Label ID="uxRetailPriceLabel" runat="server" CssClass="NewArrivalItemColumnRetailPriceValue"
                        Text='<%# StoreContext.Currency.FormatPrice( Convert.ToDecimal( GetRetailPrice( Eval( "ProductID" ) ) ) )%>' />
                    <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                    <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                            <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
                </asp:Panel>
            </asp:Panel>
            <div class="NewArrivalItemColumnPriceValue" id="uxNewArrivalItemValue" runat="server">
                <%# GetFormattedPrice(Eval("ProductID"))%>
            </div>
        </td>
    </tr>
</table>
