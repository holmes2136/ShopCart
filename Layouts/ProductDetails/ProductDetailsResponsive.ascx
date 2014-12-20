<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetailsResponsive.ascx.cs"
    Inherits="Layouts_ProductDetails_ProductDetailsResponsive" %>
<%@ Register Src="~/Components/QuantityDiscount.ascx" TagName="QuantityDiscount"
    TagPrefix="uc12" %>
<%@ Register Src="~/Components/GiftCertificateDetails.ascx" TagName="GiftCertificateDetails"
    TagPrefix="uc11" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc10" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc15" %>
<%@ Register Src="~/Components/StarRatingSummary2.ascx" TagName="StarRatingSummary"
    TagPrefix="uc8" %>
<%@ Register Src="~/Components/CustomerReviews.ascx" TagName="CustomerReviews" TagPrefix="uc7" %>
<%@ Register Src="~/Components/RelatedProducts2.ascx" TagName="RelatedProducts" TagPrefix="uc6" %>
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
<div class="ProductDetailsResponsive">
    <div class="row">
        <%--Begin Name section--%>
        <div class="pdetail-sec-one columns">
            <div class="ProductQuickInfoName">
                <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval( "Name" ) %>' />
            </div>
        </div>
        <%--Begin Image section--%>
        <div class="pdetail-sec-two columns">
            <asp:UpdatePanel ID="uxImageUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="twelve columns">
                            <div class="ProductDetailsResponsiveImage">
                                <%--<asp:Panel ID="uxImagePanel" runat="server" CssClass="ProductDetailsResponsiveImagePanel">--%>
                                <table class="ProductDetailsResponsiveImage" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td valign="middle">
                                            <div class="ProductDetailsImageRowOverlayArea">
                                                <uc2:ProductImage ID="uxCatalogImage" runat="server" MaximumWidth="370px" ProductID='<%# Eval( "ProductID" ) %>' />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <%--</asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="twelve columns">
                            <div class="ProductDetailsResponsiveImageThumbnail">
                                <asp:Repeater ID="uxThumbnailDataList" runat="server">
                                    <ItemTemplate>
                                        <div class="ImageThumbnailItemStyle">
                                            <table class="CommonCategoryImage" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td valign="middle">
                                                        <asp:LinkButton ID="uxThumbBtn" runat="server" OnClick="uxThumbBtn_Click" CommandArgument='<%# Eval("ProductImageID").ToString() %>'
                                                            ToolTip='<%# ImageTitleText(BindingContainer,Eval("ProductImageID").ToString()) %>'>
                                                            <asp:Image ID="uxThumbImage" runat="server" ImageUrl='<%# ImageUrl(Eval("ThumbnailImage").ToString()) %>'
                                                                Width="80px" />
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--Begin QuickInfo section--%>
        <div class="pdetail-sec-three columns">
            <div class="ProductQuickInfoShortDescription">
                <asp:Label ID="uxShortDescriptionLabel" runat="server" Text='<%# Eval( "ShortDescription" ) %>'
                    Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "ShortDescription" ))) %>' />
            </div>
            <div id="uxSkuNumberTR" runat="server" visible='<%# DataAccessContext.Configurations.GetBoolValue( "ShowSkuMode" ) %>'
                class="ProductQuickInfoSku">
                [$SkuNumber]:
                <asp:Label class="ProductDetailsResponsiveSku" ID="uxSkuLabel" runat="server" Text='<%# Eval( "sku" ).ToString() %>' />
            </div>
            <div id="uxRemainingQuantity" runat="server" visible='<%# ShowRemainingQuantity(Eval("UseInventory")) %>'
                class="ProductQuickInfoStock">
                [$Remaining Quantity] :
                <asp:Label class="ProductDetailsResponsiveRemainQuantityParagraphDetail" ID="uxShowStockLabel"
                    runat="server" Text='<%#RemainingStock(Eval("SumStock")) %>'></asp:Label>
            </div>
            <div id="uxRatingCustomerDIV" runat="server" class="ProductQuickInfoRating">
                <div class="StarRating">
                    <uc4:RatingCustomer ID="uxRatingCustomer" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
                </div>
            </div>
            <asp:Panel ID="uxRmaMessagePanel" runat="server" CssClass="ProductQuickInfoRmaPanel"
                Visible="<%# IsRmaEmable() %>">
                <asp:Label ID="uxRmaMessageLabel" runat="server" Text='<%# RmaMessage( Eval( "ReturnTime" ) ) %>' />
            </asp:Panel>
            <div class="QuantityDiscountFreeShippingDiv">
                <uc12:QuantityDiscount ID="uxQuantityDiscount" runat="server" DiscountGroupID="<%# DiscountGroupID %>" />
                <div id="uxFreeShippingRow" runat="server" visible='<%# Convert.ToBoolean(Eval( "FreeShippingCost" ))%>'
                    class="FreeShippingLabel">
                    <asp:Label ID="uxFreeShippingText" runat="server" Text="[$FreeShippingIcon]" />
                </div>
            </div>
            <div class="ProductQuickInfoPrice">
                <div id="uxProductPriceTitleDiv" class="ProductQuickInfoPriceTitle" runat="server"
                    visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )  %>'>
                    [$PriceTitle]
                </div>
                <div id="uxCallForPriceTR" runat="server" visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                    class="ProductDetailsResponsiveOurPrice">
                    [$CallForPrice]
                </div>
                <div id="uxRetailPriceTR" runat="server" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )  %>'
                    class="ProductDetailsResponsiveRetailPrice">
                    <asp:Label ID="uxRetailLabel" runat="server" Font-Strikeout="true" Text='<%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) ) %>'
                        CssClass="RetailPriceValue" />
                    <span class="DiscountPercent" id="uxDiscountPercentLabel" runat="server" visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                        <span class="PercentLabel">[$Percent]</span> <span class="PercentValue">
                            <%# GetDiscountPercent( Eval( "ProductID" ))%></span> </span>
                </div>
                <div id="uxPriceP" runat="server" visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) )  %>'
                    class="ProductDetailsResponsiveOurPrice">
                    <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPriceFromContainer( BindingContainer ) %>' />
                    <asp:Label ID="uxTaxIncludeLabel" runat="server" Text="[$TaxIncluded]" CssClass="CommonValidateText"
                        Visible='<%# IsTaxInclude() %>' />
                </div>
            </div>
            <div id="uxRecurringDiv" runat="server" visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'
                class="ProductDetailsResponsiveRecurringDiv">
                <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
                    CssClass="ProductDetailsResponsiveRecurringAmountEveryPanel">
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
            <div class="ProductDetailsResponsiveOptionDiv">
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
                <div class="ProductDetailsResponsiveDescriptionDiv">
                    <div class="ProductDetailsResponsiveDescriptionDivTitle">
                        <asp:Label ID="uxProductKitTitle" runat="server">[$ProductKitTitle]<%# Eval( "Name" ) %></asp:Label>
                    </div>
                    <div class="ProductDetailsResponsiveDescription">
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
                </div>
            </asp:Panel>
            <div id="uxProductSpecificationPanel" runat="server" class="ProductDetailsDefaulSpecificationDetailsDiv">
                <div class="ProductDetailsResponsiveDescriptionDivTitle">
                    <asp:Label ID="uxProductSpecificationTitle" runat="server">[$ProductSpecification]<%# Eval( "Name" ) %></asp:Label>
                </div>
                <ucProductSpecificationDetails:ProductSpecificationDetails ID="uxProductSpecificationDetails"
                    runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
            </div>
            <div class="AddToCartDiv" id="uxAddCartDiv" runat="server" visible='<%# !CatalogUtilities.IsCatalogMode() && !Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean( Eval("UseInventory") ) ) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>'>
                <div id="uxCustomPriceDiv" runat="server" class="ProductDetailsResponsiveCustomPriceDiv"
                    visible='<%# Convert.ToBoolean(Eval ( "IsCustomPrice" ))%>'>
                    <asp:Label ID="uxCustomPriceLabel" runat="server" CssClass="ProductDetailsResponsiveCustomPriceLabel">[$CustomPrice]</asp:Label>
                    <asp:TextBox ID="uxEnterAmountText" runat="server" CssClass="CommonTextBox ProductDetailsResponsiveCustomPriceTextbox"
                        MaxLength="10" Text='<%# GetAmountText( StoreContext.Currency.FormatPrice(Eval( "ProductCustomPrice.DefaultPrice" )) )%>' />
                    <div id="uxCustomPriceNote" runat="server" class="CustomPriceNote" visible='<%# Convert.ToInt32(Eval( "ProductCustomPrice.MinimumPrice" ))>0?true:false%>'>
                        <asp:Label ID="uxMinPriceLabel" runat="server" Text="[$MinimumPrice]" /><%# StoreContext.Currency.FormatPrice( Eval( "ProductCustomPrice.MinimumPrice" ) )%>
                    </div>
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
                        </asp:CompareValidator>
                    </div>
                </div>
                <div id="uxProductDetailsResponsiveQuantity" runat="server" class="ProductDetailsResponsiveQuantityDiv"
                    visible='<%#VisibleQuantity( Convert.ToInt32( Eval( "SumStock" ) ), Convert.ToBoolean( Eval ( "UseInventory" ) ) ) %>'>
                    <span id="uxQuantitySpan" runat="server" class="ProductDetailsResponsiveQuantitySpan">
                        [$Quantity]</span>
                    <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Eval( "MinQuantity" )  %>'
                        CssClass="CommonTextBox ProductDetailsResponsiveQuantityText" />
                    <asp:RequiredFieldValidator ID="uxQuantityRequiredValidator" runat="server" ControlToValidate="uxQuantityText"
                        ValidationGroup="ValidOption" Display="Dynamic" CssClass="CommonValidatorText ProductDetailsResponsiveValidatorText">
                    <div class="CommonValidateDiv ProductDetailsResponsiveValidateDiv"></div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is required.
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                        Operator="GreaterThan" ValueToCompare="0" Type="Integer" ValidationGroup="ValidOption"
                        Display="Dynamic" CssClass="CommonValidatorText ProductDetailsResponsiveValidatorText">
                    <div class="CommonValidateDiv ProductDetailsResponsiveValidateDiv"></div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Quantity is invalid.
                    </asp:CompareValidator>
                    <asp:UpdatePanel ID="uxMessageUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="uxMessageDiv" runat="server" class="CommonValidatorText ProductDetailsResponsiveValidatorText" >
                                <div class="CommonValidateDiv ProductDetailsResponsiveValidateDiv">
                                </div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                <asp:Label ID="uxMessage" runat="server"></asp:Label>
                            </div>
                            <div class="ProductDetailsResponsiveOutOfRangeQuantityParagraph">
                                <div id="uxMessageMinQuantityDiv" runat="server" class="CommonValidatorText ProductDetailsResponsiveValidatorText">
                                    <div class="CommonValidateDiv ProductDetailsResponsiveValidateDiv">
                                    </div>
                                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                                    <asp:Label ID="uxMinQuantityLabel" runat="server"></asp:Label>
                                </div>
                                <div id="uxMessageMaxQuantityDiv" runat="server" class="CommonValidatorText ProductDetailsResponsiveValidatorText">
                                    <div class="CommonValidateDiv ProductDetailsResponsiveValidateDiv">
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
                <div id="uxMessageStockDiv" runat="server" class="CommonValidatorText ProductDetailsResponsiveValidatorTextOutOfStock"
                    visible='<%# CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>'>
                    <asp:Label ID="uxStockLabel" runat="server" Text="[$This product is out of stock]"></asp:Label>
                </div>
                <div class="ProductDetailsResponsiveAddToCart">
                    <asp:LinkButton ID="uxAddToCartImageButton" runat="server" ValidationGroup="ValidOption"
                        OnClick="uxAddToCartImageButton_Click" CssClass="BtnStyle1" Text="[$BtnAddToCart]"
                        Visible='<%# IsAuthorizedToViewPrice() && !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) %>' />
                </div>
                <div class="ButtonDiv">
                    <uc10:AddtoWishListButton ID="uxAddtoWishListButton" runat="server" Visible='<%# !Convert.ToBoolean( Eval( "IsGiftCertificate" ) ) && 
                    !CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean(Eval("UseInventory"))) && 
                    !Convert.ToBoolean( Eval( "IsCustomPrice" ) ) && !Convert.ToBoolean( Eval( "IsCallForPrice" ) ) && !Convert.ToBoolean( Eval( "IsProductKit" ) ) %>'
                        ValidationGroup="ValidOption" />
                    <uc15:AddtoCompareListButton ID="uxAddtoCompareListButton" Visible='<%# !Convert.ToBoolean( Eval( "IsProductKit" ) ) && !Convert.ToBoolean( Eval( "IsGiftCertificate" ) )  %>'
                        runat="server" />
                    <asp:LinkButton ID="uxTellFriend" runat="server" OnCommand="uxTellFriendButton_Command"
                        CommandArgument='<%#Vevo.UrlManager.GetTellFriendUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        CssClass="BtnStyle5  TellFriendLinkButton" Text="[$BtnTellFriend]" />
                </div>
            </div>
            <div class="ProductDetailsResponsiveSocialButtonDiv">
                <div class="SocialButton">
                    <ucLikeButton:LikeButton ID="uxLikeButton" runat="server" />
                </div>
                <div class="SocialButton">
                    <asp:Panel ID="uxAddThisPanel" runat="server" CssClass="AddThisButton">
                        <ucAddThis:AddThis ID="uxAddThis" runat="server" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div class="ProductDetailsButtom">
        <%--Begin Information section--%>
        <div class="pdetail-sec-four columns">
            <ajaxToolkit:TabContainer ID="uxTabContainerResponsive" runat="server" CssClass="ProductDetailsDefault2TabContainer">
                <ajaxToolkit:TabPanel ID="uxLongDescriptionDiv" runat="server" CssClass="ProductDetailsDefault2TabPanel"
                    Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "LongDescription" ))) %>'>
                    <ContentTemplate>
                        <div class="ProductDetailsResponsiveDescriptionLongDiv">
                            <asp:Label ID="uxLongDescriptionLabel" runat="server" Text='<%# Eval( "LongDescription" ) %>'
                                CssClass="ProductDetailsResponsiveDescriptionLongLabel" Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "LongDescription" ))) %>' />
                        </div>
                    </ContentTemplate>
                    <HeaderTemplate>
                        <div>
                            [$ProductDetails]
                        </div>
                    </HeaderTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="uxRatingTabDIV" runat="server" CssClass="ProductDetailsDefault2TabPanel">
                    <ContentTemplate>
                        <div id="uxWriteReviewDIV" runat="server" class="WriteReviewDiv">
                            <asp:LinkButton ID="uxWriteReviewLinkButton" runat="server" Text="[$ReviewLink]"
                                OnCommand="uxAddReviewButton_Command" CommandArgument='<%#Vevo.UrlManager.GetCustomerReviewUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                CssClass="CommonHyperLink" />
                        </div>
                        <div class="ProductDetailsResponsiveRatingSummary" id="uxTabPanelAllRating" runat="server">
                            <uc8:StarRatingSummary ID="uxStarRatingSummary" runat="server" ProductID='<%# Eval( "ProductID" ) %>' />
                        </div>
                        <div class="ProductDetailsResponsiveCustomerReview" id="uxTabPanelCustomerRating"
                            runat="server">
                            <uc7:CustomerReviews ID="uxCustomerReviews" runat="server" />
                        </div>
                    </ContentTemplate>
                    <HeaderTemplate>
                        <div>
                            [$RatingTitle]
                        </div>
                    </HeaderTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
        <%--Begin Related Product section--%>
        <div class="pdetail-sec-five columns">
            <uc6:RelatedProducts ID="uxRelatedProducts" runat="server" />
        </div>
    </div>
</div>
<uxAddToCartNotification:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
