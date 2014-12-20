<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetailsDefaultBlue.ascx.cs"
    Inherits="Layouts_ProductDetails_ProductDetailsDefaultBlue" %>
<%@ Register Src="~/Components/QuantityDiscount.ascx" TagName="QuantityDiscount"
    TagPrefix="uc12" %>
<%@ Register Src="~/Components/GiftCertificateDetails.ascx" TagName="GiftCertificateDetails"
    TagPrefix="uc11" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc10" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc15" %>
<%@ Register Src="~/Components/StarRatingSummary.ascx" TagName="StarRatingSummary"
    TagPrefix="uc8" %>
<%@ Register Src="~/Components/CustomerReviews.ascx" TagName="CustomerReviews" TagPrefix="uc7" %>
<%@ Register Src="~/Components/RelatedProducts.ascx" TagName="RelatedProducts" TagPrefix="uc6" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc4" %>
<%@ Register Src="~/Components/RatingControl.ascx" TagName="RatingControl" TagPrefix="uc3" %>
<%@ Register Src="~/Components/ProductImage.ascx" TagName="ProductImage" TagPrefix="uc2" %>
<%@ Register Src="~/Components/OptionGroupDetails.ascx" TagName="OptionGroupDetails"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/DotLine.ascx" TagName="DotLine" TagPrefix="uc5" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc14" %>
<%@ Register Src="~/Components/AddThis.ascx" TagName="AddThis" TagPrefix="ucAddThis" %>
<%@ Register Src="~/Components/LikeButton.ascx" TagName="LikeButton" TagPrefix="ucLikeButton" %>
<%@ Register Src="~/Components/ProductSpecificationDetails.ascx" TagName="ProductSpecificationDetails"
    TagPrefix="ucProductSpecificationDetails" %>
<%@ Register Src="~/Components/ProductKitGroupDetails.ascx" TagName="ProductKitGroupDetails"
    TagPrefix="ucProductKitGroupDetails" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uxAddToCartNotification" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="ProductDetailsDefaultBlue">
    <div class="ProductImage">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="ProductDetailsDefaultBlueImage">
                    <tr>
                        <td class="ProductDetailsDefaultBlueImageColumn">
                            <div class="ProductDetailsImageRowOverlayArea">
                                <uc2:ProductImage ID="uxCatalogImage" runat="server" MaximumWidth="300px" ProductID='<%# Eval( "ProductID" ) %>' />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ProductDetailsDefaultBlueImageThumbnail">
                            <asp:DataList ID="uxThumbnailDataList" runat="server" CssClass="ImageThumbnailList"
                                RepeatDirection="Horizontal" RepeatColumns="5" CellSpacing="4" CellPadding="2">
                                <ItemTemplate>
                                    <div class="ImageThumbnailItem">
                                        <div class="ProductDetailsDefault2ImageThumbnailDataListItemDiv">
                                            <asp:LinkButton ID="uxThumbBtn" runat="server" OnClick="uxThumbBtn_Click" CommandArgument='<%# Eval("ProductImageID").ToString() %>'
                                                ToolTip='<%# ImageTitleText(BindingContainer,Eval("ProductImageID").ToString()) %>'>
                                                <asp:Image ID="uxThumbImage" runat="server" ImageUrl='<%# ImageUrl(Eval("ThumbnailImage").ToString()) %>'
                                                    Width="55px" /></asp:LinkButton>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle CssClass="ImageThumbnailItemStyle" />
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="ProductQuickInfo">
        <div class="ProductQuickInfoName">
            <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval( "Name" ) %>' /></div>
        <div id="uxSkuNumberTR" runat="server" visible='<%# DataAccessContext.Configurations.GetBoolValue( "ShowSkuMode" ) %>'
            class="ProductQuickInfoSku">
            [$SkuNumber]:
            <asp:Label ID="uxSkuLabel" runat="server" Text='<%# Eval( "sku" ).ToString() %>' />
        </div>
        <div id="uxDimensionTR" runat="server" class="ProductQuickInfoDimension">
            [$Dimension]:
            <asp:Label ID="uxDimensionLabel" runat="server" Text='<%# GetProductDimension( Eval( "ProductID" )) %>'></asp:Label>
        </div>
        <div class="ProductQuickInfoShortDescription">
            <asp:Label ID="uxShortDescriptionLabel" runat="server" Text='<%# Eval( "ShortDescription" ) %>'
                Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "ShortDescription" ))) %>' />
        </div>
        <div id="uxRatingCustomerDIV" runat="server" class="ProductQuickInfoRating">
            <div class="StarRating">
                <uc4:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <div class="WriteReviewLink">
                <asp:LinkButton ID="uxAddReviewButton" runat="server" Text="[$ReviewLink]" OnCommand="uxAddReviewButton_Command"
                    CommandArgument='<%#Vevo.UrlManager.GetCustomerReviewUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    CssClass="CommonHyperLink" />
            </div>
        </div>
        <div class="ProductQuickInfoPrice">
            <div id="Div1" class="ProductDetailsDefaultBlueTitleLabel" runat="server" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )  %>'>
                [$PriceTitle]</div>
            <div id="uxCallForPriceTR" runat="server" visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                class="ProductDetailsDefaultBlueOurPrice">
                [$CallForPrice]
            </div>
            <div id="uxRetailPriceTR" runat="server" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )  %>'
                class="ProductDetailsDefaultBlueRetailPrice">
                <asp:Label ID="uxRetailLabel" runat="server" Font-Strikeout="true" Text='<%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) ) %>'
                    CssClass="RetailPriceValue" />
                <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                    <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                        <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
            </div>
            <div id="uxPriceP" runat="server" visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) )  %>'
                class="ProductDetailsDefaultBlueOurPrice">
                <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPriceFromContainer( BindingContainer ) %>' />
                <asp:Label ID="uxTaxIncludeLabel" runat="server" Text="[$TaxIncluded]" CssClass="CommonValidateText"
                    Visible='<%# IsTaxInclude() %>' />
            </div>
        </div>
        <asp:Panel ID="uxRmaMessagePanel" runat="server" CssClass="ProductDetailsDefaultBlueRmaPanel"
            Visible="<%# IsRmaEmable() %>">
            <asp:Label ID="uxRmaMessageLabel" runat="server" Text='<%# RmaMessage( Eval( "ReturnTime" ) ) %>' />
        </asp:Panel>
        <div class="ProductQuickInfoStock">
            <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                class="FreeShippingLabel">
                <asp:Label ID="uxFreeShippingText" runat="server" Text="[$FreeShippingIcon]" />
            </div>
            <div id="uxRemainingQuantity" runat="server" visible='<%# ShowRemainingQuantity(Eval("UseInventory")) %>'
                class="ProductDetailsDefaultBlueRemainQuantityParagraph">
                [$Remaining Quantity] :
                <asp:Label ID="uxShowStockLabel" runat="server" Text='<%#RemainingStock(Eval("SumStock")) %>'></asp:Label>
            </div>
        </div>
        <uc12:QuantityDiscount ID="uxQuantityDiscount" runat="server" DiscountGroupID="<%# DiscountGroupID %>" />
        <div id="uxRecurringDiv" runat="server" visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'
            class="ProductDetailsDefaultBlueRecurringDiv">
            <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
                CssClass="ProductDetailsDefaultBlueRecurringAmountEveryPanel">
                <%#  "[$RecurringAmountEvery] " + ShowRecurringCycles()%>
            </asp:Panel>
            <uc14:RecurringSpecial ID="uxRecurringSpecial" runat="server" Visible='<%# IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>' />
        </div>
        <asp:UpdatePanel ID="uxGiftCertificateUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc11:GiftCertificateDetails ID="uxGiftCertificateDetails" runat="server" IsGiftCertificate='<%# Eval( "IsGiftCertificate" ) %>'
                    ProductID='<%# Eval( "ProductID" ).ToString() %>' />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="uxAddToCartImageButton" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="AddToCartDiv" id="uxAddCartDiv" runat="server" visible='<%# !CatalogUtilities.IsCatalogMode() && !Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean( Eval("UseInventory") ) ) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'>
            <div id="uxCustomPriceDiv" runat="server" class="ProductDetailsDefaultBlueCustomPriceDiv"
                visible='<%# Convert.ToBoolean(Eval ( "IsCustomPrice" ))%>'>
                <asp:Label ID="uxCustomPriceLabel" runat="server" CssClass="ProductDetailsDefaultBlueCustomPriceLabel">[$CustomPrice]</asp:Label>
                <asp:TextBox ID="uxEnterAmountText" runat="server" CssClass="CommonTextBox ProductDetailsDefaultBlueCustomPriceTextbox"
                    MaxLength="10" Text='<%# GetAmountText( StoreContext.Currency.FormatPrice(Eval( "ProductCustomPrice.DefaultPrice" )) )%>' />
                <div id="uxCustomPriceNote" runat="server" class="CustomPriceNote" visible='<%# Convert.ToInt32(Eval( "ProductCustomPrice.MinimumPrice" ))>0?true:false%>'>
                    <asp:Label ID="uxMinPriceLabel" runat="server" Text="[$MinimumPrice]" /><%# StoreContext.Currency.FormatPrice( Eval( "ProductCustomPrice.MinimumPrice" ) )%></div>
                <div class="CustomPriceRequiredNote">
                    <asp:RequiredFieldValidator ID="uxEnterAmountTextRequired" runat="server" ErrorMessage="Enter amount is required."
                        Display="Dynamic" ControlToValidate="uxEnterAmountText" ValidationGroup="ValidOption">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxEnterAmountTextCompare" runat="server" ErrorMessage="Enter amount must be equal or greater than minimum price."
                        Display="Dynamic" ValueToCompare='<%#Convert.ToDouble( Eval( "ProductCustomPrice.MinimumPrice" ) )%>'
                        Type="Double" Operator="GreaterThanEqual" ControlToValidate="uxEnterAmountText"
                        ValidationGroup="ValidOption">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="uxEnterAmountDataTypeCheck" runat="server" ControlToValidate="uxEnterAmountText"
                        ErrorMessage="Enter amount is invalid" Operator="DataTypeCheck" Type="Currency"
                        ValidationGroup="ValidOption" Display="Dynamic">
                    </asp:CompareValidator></div>
            </div>
            <div id="uxProductDetailsDefaultBlueQuantity" runat="server" class="ProductDetailsDefaultBlueQuantityDiv"
                visible='<%#VisibleQuantity( Convert.ToInt32( Eval( "SumStock" ) ), Convert.ToBoolean( Eval ( "UseInventory" ) ) ) %>'>
                <span id="uxQuantitySpan" runat="server" class="ProductDetailsDefaultBlueQuantitySpan">
                    [$Quantity]</span>
                <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Eval( "MinQuantity" )  %>'
                    CssClass="CommonTextBox ProductDetailsDefaultBlueQuantityText" />
                <asp:RequiredFieldValidator ID="uxQuantityRequiredValidator" runat="server" ControlToValidate="uxQuantityText"
                    ValidationGroup="ValidOption" Display="Dynamic" CssClass="CommonValidatorText ProductDetailsDefaultBlueValidatorText">
                    <div class="CommonValidateDiv ProductDetailsDefaultBlueValidateDiv"></div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is required.
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                    Operator="GreaterThan" ValueToCompare="0" Type="Integer" ValidationGroup="ValidOption"
                    Display="Dynamic" CssClass="CommonValidatorText ProductDetailsDefaultBlueValidatorText">
                    <div class="CommonValidateDiv ProductDetailsDefaultBlueValidateDiv"></div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is invalid.
                </asp:CompareValidator>
                <asp:UpdatePanel ID="uxMessageUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="uxMessageDiv" runat="server" class="CommonValidatorText ProductDetailsDefaultBlueValidatorText"
                            runat="server">
                            <div class="CommonValidateDiv ProductDetailsDefaultBlueValidateDiv">
                            </div>
                            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                            <asp:Label ID="uxMessage" runat="server"></asp:Label>
                        </div>
                        <div class="ProductDetailsDefaultBlueOutOfRangeQuantityParagraph">
                            <div id="uxMessageMinQuantityDiv" runat="server" class="CommonValidatorText ProductDetailsDefaultBlueValidatorText">
                                <div class="CommonValidateDiv ProductDetailsDefaultBlueValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxMinQuantityLabel" runat="server"></asp:Label>
                            </div>
                            <div id="uxMessageMaxQuantityDiv" runat="server" class="CommonValidatorText ProductDetailsDefaultBlueValidatorText">
                                <div class="CommonValidateDiv ProductDetailsDefaultBlueValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxMaxQuantityLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="uxAddToCartImageButton" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="uxMessageStockDiv" runat="server" class="CommonValidatorText ProductDetailsDefaultBlueValidatorTextOutOfStock"
                visible='<%# CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>'>
                <asp:Label ID="uxStockLabel" runat="server" Text="[$This product is out of stock]"></asp:Label>
            </div>
            <div class="ProductDetailsDefaultBlueAddToCart">
                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" ValidationGroup="ValidOption"
                    OnClick="uxAddToCartImageButton_Click" CssClass="BtnStyle1" Text="[$BtnAddToCart]"
                    Visible='<%# IsAuthorizedToViewPrice() && !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>' />
            </div>
        </div>
        <div class="ProductQuickInfoSocialButton">
            <ucLikeButton:LikeButton ID="uxLikeButton" runat="server" />
            <asp:Panel ID="uxAddThisPanel" runat="server" CssClass="AddThisButton">
                <ucAddThis:AddThis ID="uxAddThis" runat="server" />
            </asp:Panel>
        </div>
        <div class="ButtonDiv">
            <asp:LinkButton ID="uxTellFriend" runat="server" OnCommand="uxTellFriendButton_Command"
                CommandArgument='<%#Vevo.UrlManager.GetTellFriendUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                CssClass="BtnStyle5  TellFriendLinkButton" Text="[$BtnTellFriend]" />
            <uc10:AddtoWishListButton ID="uxAddtoWishListButton" runat="server" Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) && 
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                ValidationGroup="ValidOption" />
            <uc15:AddtoCompareListButton ID="uxAddtoCompareListButton" Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'
                runat="server" />
        </div>
        <div class="ProductDetailsDefaultBlueOptionDiv">
            <asp:UpdatePanel ID="uxOptionGroupUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:OptionGroupDetails ID="uxOptionGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'>
                    </uc1:OptionGroupDetails>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="uxAddToCartImageButton" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:Panel ID="uxProductKitPanel" runat="server">
            <div class="ProductDetailsDefaultBlueDescriptionDiv">
                <div class="ProductDetailsDefaultBlueDescriptionDivTitle">
                    <asp:Label ID="uxProductKitTitle" runat="server">[$ProductKitTitle]<%# Eval( "Name" ) %></asp:Label>
                </div>
                <div class="ProductDetailsDefaultBlueDescriptionLeft">
                    <div class="ProductDetailsDefaultBlueDescriptionRight">
                        <asp:UpdatePanel ID="uxProductKitUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ucProductKitGroupDetails:ProductKitGroupDetails ID="uxProductKitGroupDetails" runat="server"
                                    ProductID='<%# Eval( "ProductID" ) %>'></ucProductKitGroupDetails:ProductKitGroupDetails>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="uxAddToCartImageButton" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="ProductDetailsDefaultBlueDescriptionDiv" id="uxLongDescriptionDiv" runat="server"
            visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "LongDescription" ))) %>'>
            <div class="ProductDetailsDefaultBlueDescriptionDivTitle">
                [$ProductDetails]<%# Eval( "Name" ) %></div>
            <div class="ProductDetailsDefaultBlueDescriptionLongDiv">
                <asp:Label ID="uxLongDescriptionLabel" runat="server" Text='<%# Eval( "LongDescription" ) %>'
                    CssClass="ProductDetailsDefaultBlueDescriptionLongLabel" Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "LongDescription" ))) %>' /></div>
        </div>
    </div>
    <div class="ProductDetailsDefaultBlueSpecificationDiv" id="uxProductSpecificationPanel"
        runat="server">
        <div class="ProductDetailsDefaultBlueSpecificationDivTitle">
            <asp:Label ID="uxProductSpecificationLabel" runat="server">[$ProductSpecification]<%# Eval( "Name" ) %></asp:Label>
        </div>
        <div class="ProductDetailsDefaulSpecificationDetailsDiv">
            <ucProductSpecificationDetails:ProductSpecificationDetails ID="uxProductSpecificationDetails"
                runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
        </div>
    </div>
    <div id="uxRatingTabDIV" runat="server" class="ProductDetailsDefaultBlueRatingDiv">
        <div class="ProductDetailsDefaultBlueRatingDivTitle">
            [$RatingTitle]<%# Eval( "Name" ) %>
            <div id="uxWriteReviewDIV" runat="server" class="WriteReviewDiv">
                <asp:LinkButton ID="uxWriteReviewLinkButton" runat="server" Text="[$ReviewLink]"
                    OnCommand="uxAddReviewButton_Command" CommandArgument='<%#Vevo.UrlManager.GetCustomerReviewUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                    CssClass="CommonHyperLink" /></div>
        </div>
        <div class="ProductDetailsDefaultBlueRatingSummary" id="uxTabPanelAllRating" runat="server">
            <uc8:StarRatingSummary ID="uxStarRatingSummary" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
        </div>
        <div class="ProductDetailsDefaultBlueCustomerReview" id="uxTabPanelCustomerRating"
            runat="server">
            <uc7:CustomerReviews ID="uxCustomerReviews" runat="server" />
        </div>
    </div>
    <uc6:RelatedProducts ID="uxRelatedProducts" runat="server" />
</div>
<uxAddToCartNotification:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
