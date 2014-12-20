<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductQuickView.ascx.cs"
    Inherits="Components_ProductQuickView" %>
<%@ Register Src="~/Components/QuantityDiscount.ascx" TagName="QuantityDiscount"
    TagPrefix="uc12" %>
<%@ Register Src="~/Components/GiftCertificateDetails.ascx" TagName="GiftCertificateDetails"
    TagPrefix="uc11" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc9" %>
<%@ Register Src="~/Components/OptionGroupDetails.ascx" TagName="OptionGroupDetails"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc14" %>
<%@ Register Src="~/Components/ProductSpecificationDetails.ascx" TagName="ProductSpecificationDetails"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uc8" %>
<%@ Register Src="~/Components/ProductImage.ascx" TagName="ProductImage" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Panel ID="uxQuickViewButtonBorder" runat="server" CssClass="QuickViewButtonBorder">
    <asp:Panel ID="uxQuickViewButtonPanel" runat="server" CssClass="QuickViewButtonPanel">
        <asp:LinkButton ID="uxQuickViewButton" runat="server" Text="Quick View" CommandArgument="hide"
            CssClass="QuickViewButton" OnClick="uxQuickViewButton_Click" />
    </asp:Panel>
</asp:Panel>
<asp:UpdatePanel ID="uxUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:Panel ID="uxQuickViewPanel" runat="server">
            <div class="QuickView">
            </div>
            <div class="QuickViewPanel">
                <div class="Body">
                    <div class="ProductImage">
                        <div class="MainImage">
                            <uc2:ProductImage ID="uxCatalogImage" runat="server" MaximumWidth="300px" ProductID='<%# Eval( "ProductID" ) %>'>
                            </uc2:ProductImage>
                        </div>
                        <div class="ThumbnailIamge">
                            <asp:DataList ID="uxThumbnailDataList" runat="server" CssClass="ProductDetailsDefaultImageThumbnailDataList"
                                RepeatDirection="Horizontal" RepeatColumns="4" CellSpacing="2">
                                <ItemTemplate>
                                    <div class="ProductDetailsDefaultImageThumbnailDataListItemDiv">
                                        <asp:LinkButton ID="uxThumbBtn" runat="server" OnClick="uxThumbBtn_Click" CommandArgument='<%# Eval("ProductImageID").ToString() %>'
                                            ToolTip='<%# QuickViewImageTitleText( Eval("ProductImageID").ToString() ) %>'>
                                            <asp:Image ID="uxThumbImage" runat="server" ImageUrl='<%# String.Format("~/{0}", Eval("ThumbnailImage").ToString()) %>'
                                                Width="45px" AlternateText='<%# QuickViewImageAlternateText( Eval("ProductImageID").ToString() ) %>' /></asp:LinkButton></div>
                                </ItemTemplate>
                                <ItemStyle CssClass="ProductDetailsDefaultImageThumbnailDataListItemStyle" />
                            </asp:DataList>
                        </div>
                    </div>
                    <div class="ProductDetail">
                        <div class="ProductDetailsDefaultMessageDiv">
                            <uc9:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
                        </div>
                        <asp:LinkButton ID="uxCloseButton" runat="server" Text='<%# GetLanguageText( "BtnDelete" )%>'
                            CssClass="close" />
                        <div class="Title">
                            <asp:Label ID="uxHeaderLabel" runat="server" Text="<%# GetProductName() %>" />
                        </div>
                        <div class="DetailOverflowPosition">
                            <asp:DataList ID="uxList" runat="server">
                                <ItemTemplate>
                                    <div id="uxRecurringDiv" runat="server" visible='<%# ConvertUtilities.ToBoolean( Eval("IsRecurring") ) %>'
                                        class="ProductDetailsDefaultRecurringDiv">
                                        <asp:Panel ID="uxRecurringPeriodTR" runat="server" Visible='<%# IsVisibilityRecurringPeriod() %>'
                                            CssClass="ProductDetailsDefaultRecurringAmountEveryPanel">
                                            <%# "Recurring Amount Every " + ShowRecurringCycles()%>
                                        </asp:Panel>
                                        <uc14:RecurringSpecial ID="uxRecurringSpecial" runat="server" Visible='<%# IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) %>' />
                                    </div>
                                    <div id="uxCallForPriceTR" runat="server" visible='<%# IsCallForPrice( Eval( "IsCallForPrice" ) ) %>'
                                        class="CommonInfo">
                                        <%# GetLanguageText( "Call For Price" )%>
                                    </div>
                                    <div id="uxPriceP" runat="server" visible='<%# IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" ) )  %>'
                                        class="CommonInfo">
                                        <div class="CommonLabel">
                                            <%# GetLanguageText("Price") %>
                                        </div>
                                        <div class="PriceValue">
                                            <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPrice() %>' />
                                            <asp:Label ID="uxTaxIncludeLabel" runat="server" Text="Tax Included" CssClass="CommonValidateText"
                                                Visible='<%# IsTaxInclude() %>' />
                                        </div>
                                    </div>
                                    <div id="uxRetailPriceTR" runat="server" visible='<%# IsRetailPriceEnabled( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), GetRetailPrice( Eval( "ProductID" ) ), Eval( "IsCallForPrice" ) )  %>'
                                        class="InfoRetailPrice">
                                        <asp:Label ID="uxRetailLabel" runat="server" CssClass="RetailPriceValue" Text='<%# StoreContext.Currency.FormatPrice( GetRetailPrice( Eval( "ProductID" ) ) ) %>' />
                                        <asp:Panel ID="uxDiscountPercentLabel" runat="server" CssClass="ProductListDiscountPercent"
                                            Visible='<%# IsDiscount(Eval( "ProductID" )) && IsFixedPrice( Eval( "IsFixedPrice" ), Eval( "IsCustomPrice" ), Eval( "IsCallForPrice" )  )  %>'>
                                            <span class="PercentLabel">
                                                <%# GetLanguageText( "Percent" )%></span> <span class="PercentValue">
                                                    <%# GetDiscountPercent( Eval( "ProductID" ))%></span>
                                        </asp:Panel>
                                    </div>
                                    <div id="uxProductDetailsDefaultQuantity" runat="server" class="CommonInfo" visible='<%#VisibleQuantity( Convert.ToInt32( Eval( "SumStock" ) ), Convert.ToBoolean( Eval ( "UseInventory" ) ) ) %>'>
                                        <div id="uxQuantitySpan" runat="server" class="CommonLabel">
                                            <%# GetLanguageText( "Quantity" )%>
                                        </div>
                                        <div class="CommonValue">
                                            <asp:TextBox ID="uxQuantityText" runat="server" Text='<%# Eval( "MinQuantity" )  %>'
                                                Visible='<%# !HasUploadOrKit() %>' CssClass="CommonTextBox ProductDetailsDefaultQuantityText" />
                                            <asp:Label ID="uxStockLabel" runat="server" CssClass="StockValue" Visible='<%# Convert.ToBoolean( Eval ( "UseInventory" ) ) %>'
                                                Text='<%# CheckValidStock() %>' />
                                            <br />
                                            <asp:RequiredFieldValidator ID="uxQuantityTextRequired" runat="server" CssClass="CommonValidatorText"
                                                Visible='<%# !HasUploadOrKit() %>' Display="Dynamic" ControlToValidate="uxQuantityText"
                                                ValidationGroup='<%# GetValidGroup() %>'>
                                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> <%# GetLanguageText( "RequireAmount" ) %>
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="uxQuantityTextCompare" runat="server" CssClass="CommonValidatorText"
                                                Visible='<%# !HasUploadOrKit() %>' Display="Dynamic" ValueToCompare='<%# Convert.ToInt32( Eval( "MinQuantity" ) )%>'
                                                Type="Integer" Operator="GreaterThanEqual" ControlToValidate="uxQuantityText"
                                                ValidationGroup='<%# GetValidGroup() %>'>
                                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> <%# GetLanguageText( "CompareMinAmount" )%>
                                            </asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div id="uxCustomPriceDiv" runat="server" class="CommonInfo" visible='<%# Convert.ToBoolean(Eval ( "IsCustomPrice" ))%>'>
                                        <div class="CommonLabel">
                                            <asp:Label ID="uxCustomPriceLabel" runat="server" CssClass="CustomPriceLabel">Custom Price:</asp:Label></div>
                                        <div class="CommonValue">
                                            <asp:TextBox ID="uxEnterAmountText" runat="server" CssClass="CommonTextBox CustomPriceTextbox"
                                                MaxLength="10" Text='<%# GetAmountText( StoreContext.Currency.FormatPrice(Eval( "ProductCustomPrice.DefaultPrice" )) )%>' />
                                            <div class="CustomPriceRequiredNote">
                                                <asp:RequiredFieldValidator ID="uxEnterAmountTextRequired" runat="server" CssClass="CommonValidatorText"
                                                    Display="Dynamic" ControlToValidate="uxEnterAmountText" ValidationGroup='<%# GetValidGroup() %>'>
                                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> <%# GetLanguageText( "RequireAmount" ) %>
                                                </asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="uxEnterAmountTextCompare" runat="server" CssClass="CommonValidatorText"
                                                    Display="Dynamic" ValueToCompare='<%#Convert.ToDouble( Eval( "ProductCustomPrice.MinimumPrice" ) )%>'
                                                    Type="Double" Operator="GreaterThanEqual" ControlToValidate="uxEnterAmountText"
                                                    ValidationGroup='<%# GetValidGroup() %>'>
                                                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> <%# GetLanguageText( "CompareMinAmount" )%>
                                                </asp:CompareValidator>
                                            </div>
                                            <div id="uxCustomPriceNote" runat="server" class="CustomPriceNote" visible='<%# Convert.ToInt32(Eval( "ProductCustomPrice.MinimumPrice" ))>0?true:false%>'>
                                                <asp:Label ID="uxMinPriceLabel" runat="server" Text="Minimum Price" /><%# StoreContext.Currency.FormatPrice( Eval( "ProductCustomPrice.MinimumPrice" ) )%></div>
                                        </div>
                                    </div>
                                    <uc11:GiftCertificateDetails ID="uxGiftCertificateDetails" runat="server" IsGiftCertificate='<%# Eval( "IsGiftCertificate" ) %>'
                                        ProductID='<%# Eval( "ProductID" ).ToString() %>' />
                                    <uc12:QuantityDiscount ID="uxQuantityDiscount" runat="server" DiscountGroupID='<%# Eval( "DiscountGroupID" ) %>' />
                                    <div class="CommonInfo">
                                        <uc1:OptionGroupDetails ID="uxOptionGroupDetails" runat="server" ProductID='<%# Eval( "ProductID" ) %>'
                                            Visible='<%# !HasUploadOrKit() %>' ValidGroup='<%# GetValidGroup() %>' />
                                    </div>
                                    <div class="CommonInfo">
                                        <div class="CommonLabel">
                                            <%# GetLanguageText( "Description" )%>
                                        </div>
                                        <div class="ProductDescriptionValue">
                                            <asp:Label ID="uxShortDescriptionLabel" runat="server" Text='<%# GetDisplayString(Eval( "ShortDescription" ), 160) %>'
                                                Visible='<%# !String.IsNullOrEmpty( String.Format( "{0}", Eval( "ShortDescription" ))) %>' />
                                        </div>
                                    </div>
                                    <asp:Panel ID="uxProductSpecificationPanel" runat="server" class="CommonInfo" Visible="<%# ProductSpecificationVisible() %>">
                                        <div class="CommonLabel">
                                            <%# GetLanguageText( "Specification" )%>
                                        </div>
                                        <div class="ProductDescriptionValue">
                                            <uc5:ProductSpecificationDetails ID="uxProductSpecificationDetails" runat="server"
                                                ProductID='<%# Eval( "ProductID" ) %>' />
                                        </div>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div class="QuickViewAddCartButton">
                            <div class="ViewDetailButton">
                                <asp:LinkButton ID="uxViewDetailButton" runat="server" Text='<%# GetLanguageText( "View Full Product Detail" )%>'
                                    CssClass="ViewDetail" OnClick="uxViewDetailButton_Click" />
                            </div>
                            <div class="AddCartButton">
                                <asp:LinkButton ID="uxAddToCartImageButton" runat="server" Text='<%# GetLanguageText( "BtnAddToCart" )%>'
                                    CausesValidation="true" ValidationGroup='<%# GetValidGroup() %>' CssClass="AddCart"
                                    OnClick="uxAddToCartImageButton_Click" Visible='<%# !CatalogUtilities.IsCatalogMode() && !Vevo.CatalogUtilities.IsOutOfStock( Convert.ToInt32( Eval( "SumStock" ) ) , Convert.ToBoolean( Eval("UseInventory") ) ) && IsAuthorizedToViewPrice(Eval( "IsCallForPrice" )) && !HasUploadOrKit() %>' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxQuickViewButton" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="uxProductHidden" runat="server" />
<asp:HiddenField ID="uxImageHidden" runat="server" />
<asp:UpdatePanel ID="uxAddtoCartUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <uc8:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
