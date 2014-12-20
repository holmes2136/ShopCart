<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.ascx.cs"
    Inherits="Mobile_Components_ProductDetails" %>
<%@ Register Src="~/Mobile/Components/MobileImageSwipe.ascx" TagName="MobileImage"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/OptionGroupDetails.ascx" TagName="OptionGroupDetails"
    TagPrefix="uc2" %>
<%@ Register Src="~/Components/QuantityDiscount.ascx" TagName="QuantityDiscount"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/GiftCertificateDetails.ascx" TagName="GiftCertificateDetails"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/ProductKitGroupDetails.ascx" TagName="ProductKitGroupDetails"
    TagPrefix="uc6" %>
<%@ Register Src="~/Mobile/Components/RelatedProducts.ascx" TagName="RelatedProducts"
    TagPrefix="uc7" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="MobileTitle">
    <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
</div>
<table cellpadding="0" cellspacing="0" border="0" class="MobileProductDetails">
    <tr>
        <td class="MobileProductDetailsImage">
            <uc1:MobileImage ID="uxImageSwipe" runat="server" MaximumWidth="200px" ProductID='<%# Eval( "ProductID" ).ToString() %>'>
            </uc1:MobileImage>
        </td>
    </tr>
</table>
<div class="MobileProductDetailsShortDescription">
    <asp:Label ID="uxMobileShortDescriptionLabel" runat="server" Text='<%# Eval("ShortDescription") %>'
        Visible='<%# !String.IsNullOrEmpty(String.Format("{0}",Eval("ShortDescription"))) %>' />
</div>
<div class="MobileProductDetailsPrice">
    <uc5:GiftCertificateDetails ID="uxGiftCertificateDetails" runat="server" IsGiftCertificate='<%# Eval( "IsGiftCertificate" ) %>'
        ProductID='<%# Eval( "ProductID" ).ToString() %>' />
    <uc4:QuantityDiscount ID="uxQuantityDiscount" runat="server" DiscountGroupID="<%# DiscountGroupID %>" />
    <div class="MobileProductDetailsOption" runat="server" visible='<%# IsHaveOption()%>'>
        <uc2:OptionGroupDetails ID="uxOptionGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'>
        </uc2:OptionGroupDetails>
    </div>
    <asp:Panel ID="uxProductKitPanel" runat="server" Visible='<%# Eval( "IsProductKit" ) %>'>
        <div class="MobileProductKitDetails">
            <asp:Label ID="uxProductKitTitle" runat="server" Text="[$ProductKitTitle]" CssClass="MobileProductKitTitle"></asp:Label>
            <uc6:ProductKitGroupDetails ID="uxProductKitGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'>
            </uc6:ProductKitGroupDetails>
        </div>
    </asp:Panel>
    <div class="Column1">
        <asp:Panel ID="uxCallForPriceTR" runat="server" Visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
            CssClass="Price">
            <div>
                [$CallForPrice]
            </div>
        </asp:Panel>
        <div id="uxRetailPriceDiv" runat="server" class="RetailPrice" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) ) %>'>
            [$Retail Price] :
            <asp:Label ID="uxRetailLabel" runat="server" Font-Strikeout="true" Text='<%# StoreContext.Currency.FormatPrice(GetRetailPrice(Eval("ProductID"))) %>'
                Font-Bold="false" />
            <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                    <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
        </div>
        <div id="uxPriceDiv" runat="server" class="Price" visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) ) %>'>
            [$Our Price] :
            <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPriceFromContainer(BindingContainer) %>'
                Visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) ) %>'></asp:Label>
            <asp:Label ID="uxTaxIncludeLabel" runat="server" Text="[$TaxIncluded]" CssClass="CommonValidateText"
                Visible='<%# IsTaxInclude() %>' />
        </div>
    </div>
    <div class="Column2">
        <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
            class="FreeShippingLabel">
            <asp:Label ID="uxFreeShippingText" runat="server" Text="[$FreeShippingIcon]" />
        </div>
        <asp:Panel ID="uxRmaMessagePanel" runat="server" Visible="<%# IsRmaEmable() %>">
            <asp:Label ID="uxRmaMessageLabel" runat="server" Text='<%# RmaMessage( Eval( "ReturnTime" ) ) %>' />
        </asp:Panel>
    </div>
</div>
<div class="MobileProductDetailsQty">
    <div id="uxCustomPriceDiv" runat="server" class="CustomPriceDiv" visible='<%# IsShowCustomPrice(Eval ( "IsCustomPrice" ),Eval( "IsCallForPrice" ))%>'>
        <asp:Label ID="uxCustomPriceLabel" runat="server" CssClass="CustomPriceLabel">[$CustomPrice]</asp:Label>
        <asp:TextBox ID="uxEnterAmountText" runat="server" CssClass="CustomPriceTextbox"
            MaxLength="10" Text='<%# Convert.ToDouble( Eval( "ProductCustomPrice.DefaultPrice" ) )%>' />
        <div class="CustomPriceRequiredNote">
            <asp:RequiredFieldValidator ID="uxEnterAmountTextRequired" runat="server" Display="Dynamic"
                ControlToValidate="uxEnterAmountText" ValidationGroup="ValidOption" CssClass="MobileCommonValidatorText MobileProductDetailsCustomPriceValidatorText">
                <div class="MobileCommonValidateDiv MobileProductCustomPriceValidateDiv"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Enter amount is required.
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxEnterAmountTextCompare" runat="server" Display="Dynamic"
                ValueToCompare='<%#Convert.ToDouble( Eval( "ProductCustomPrice.MinimumPrice" ) )%>'
                Type="Currency" Operator="GreaterThanEqual" ControlToValidate="uxEnterAmountText"
                ValidationGroup="ValidOption" CssClass="MobileCommonValidatorText MobileProductDetailsCustomPriceValidatorText">
                <div class="MobileCommonValidateDiv MobileProductCustomPriceValidateDiv"></div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Enter amount must be equal or greater than minimum price.
            </asp:CompareValidator>
        </div>
        <div id="uxCustomPriceNote" runat="server" class="CustomPriceNote" visible='<%# Convert.ToInt32(Eval( "ProductCustomPrice.MinimumPrice" ))>0?true:false%>'>
            <asp:Label ID="uxMinPriceLabel" runat="server" Text="[$MinimumPrice]" /><%# StoreContext.Currency.FormatPrice( Eval( "ProductCustomPrice.MinimumPrice" ) )%></div>
    </div>
    <div id="uxProductDetailsDefaultQuantity" runat="server" class="QuantityDiv" visible='<%#VisibleQuantity( Convert.ToInt32( Eval( "SumStock" ) ), Convert.ToBoolean( Eval ( "UseInventory" ) ) ) %>'>
        <span id="uxQuantitySpan" runat="server" class="ProductDetailsDefaultQuantitySpan">[$Quantity]</span>
        <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Eval( "MinQuantity" )  %>'
            Width="40" CssClass="MobileQuantityText" />
    </div>
    <asp:RequiredFieldValidator ID="uxQuantityRequiredValidator" runat="server" ControlToValidate="uxQuantityText"
        ValidationGroup="ValidOption" Display="Dynamic" CssClass="MobileCommonValidatorText MobileProductDetailsQtyValidatorText">
        <div class="MobileCommonValidateDiv MobileProductValidateDiv"></div>
        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is required.
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
        Operator="GreaterThan" ValueToCompare="0" Type="Integer" ValidationGroup="ValidOption"
        Display="Dynamic" CssClass="MobileCommonValidatorText MobileProductDetailsQtyValidatorText">
        <div class="MobileCommonValidateDiv MobileProductValidateDiv"></div>
        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is invalid.
    </asp:CompareValidator>
    <div id="uxMessageDiv" runat="server" class="MobileCommonValidatorText MobileProductDetailsQtyValidatorText">
        <div class="MobileCommonValidateDiv MobileProductValidateDiv">
        </div>
        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
        <asp:Label ID="uxMessage" runat="server"></asp:Label>
    </div>
    <div class="OutOfRangeQuantityDiv">
        <div id="uxMessageMinQuantityDiv" runat="server" class="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv MobileProductValidateDiv">
            </div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
            <asp:Label ID="uxMinQuantityLabel" runat="server"></asp:Label>
        </div>
        <div id="uxMessageMaxQuantityDiv" runat="server" class="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv MobileProductValidateDiv">
            </div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
            <asp:Label ID="uxMaxQuantityLabel" runat="server"></asp:Label>
        </div>
    </div>
    <div class="OutOfStockDiv">
        <asp:Label ID="uxStockLabel" runat="server" Text="[$This product is out of stock]"
            Visible='<%# CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>' />
    </div>
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="uxTellFriendLinkButton" runat="server" Text="Tell a Friend" OnClick="uxTellFriendLinkButton_Click"
                        Visible='<%# Vevo.Domain.DataAccessContext.Configurations.GetBoolValue( "TellAFriendEnabled" ) %>'
                        CssClass="MobileButton TellAFriendBottonDiv" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="uxAddToWishList" runat="server" Text="Add to Wish List" OnClick="uxAddToWishList_Click"
                        Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && DataAccessContext.Configurations.GetBoolValue( "WishListEnabled" ) &&
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) && 
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                        ValidationGroup="ValidOption" CssClass="MobileButton WishListBottonDiv" />
                </td>
            </tr>
        </table>
    </div>
    <div class="MobileUserLoginControlPanel">
        <asp:LinkButton ID="uxAddToCartButton" runat="server" Text="Add to Cart" OnClick="uxAddToCartButton_Click"
            Visible='<%# IsExpressCheckoutEnabled() && !CatalogUtilities.IsCatalogMode() && !Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean( Eval("UseInventory") ) ) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'
            ValidationGroup="ValidOption" CssClass="MobileButton" />
    </div>
</div>
<div class="MobileProductDetailsLongDescription">
    <asp:Label ID="uxMobileProductDetailsTitle" runat="server" CssClass="MobileProductDetailsTitle">Product Details</asp:Label>
    <asp:Label ID="uxMobileLongDescriptionLabel" runat="server" CssClass="MobileProductDetailsText"
        Text='<%# Eval("LongDescription") %>' Visible='<%# !String.IsNullOrEmpty(String.Format("{0}",Eval("LongDescription"))) %>' />
</div>
<div class="MobileProductDetailsLongDescription">
    <uc7:RelatedProducts ID="uxRelatedProducts" runat="server"></uc7:RelatedProducts>
</div>
