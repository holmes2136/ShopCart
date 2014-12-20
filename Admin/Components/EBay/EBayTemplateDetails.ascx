<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayTemplateDetails.ascx.cs"
    Inherits="Admin_Components_EBayTemplateDetails" %>
<%@ Register Src="../Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Common/StateList.ascx" TagName="StateList" TagPrefix="uc1" %>
<%@ Register Src="../Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="EBayCategorySelect.ascx" TagName="EBayCategorySelect" TagPrefix="uc4" %>
<%@ Register Src="EBayDomesticShippingDrop.ascx" TagName="EBayDomesticShipping" TagPrefix="uc5" %>
<%@ Register Src="EBayInternationalShippingDrop.ascx" TagName="EBayInternationalShipping"
    TagPrefix="uc6" %>
<%@ Register Src="EBayInternationalShippingCountryDrop.ascx" TagName="EBayInternationalShippingCountry"
    TagPrefix="uc7" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="uxRequiredFieldSymbol" runat="server" meta:resourcekey="uxRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidEBayTemplate" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonTextTitle1 mgb10">
                <asp:Label ID="uxGeneralTemplateDetails" runat="server" meta:resourcekey="uxGeneralTemplateDetails" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayTemplateListSiteLabel" runat="server" Text="List Site" CssClass="Label" />
                <asp:DropDownList ID="uxEBayTemplateListSiteDrop" runat="server" CssClass="DropDown"
                    OnSelectedIndexChanged="uxEBayTemplateListSiteDrop_SelectedIndexchanged" AutoPostBack="true">
                    <asp:ListItem Value="US">US</asp:ListItem>
                    <asp:ListItem Value="UK">UK</asp:ListItem>
                    <asp:ListItem Value="Germany">Germany</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayTemplateNameLabel" runat="server" meta:resourcekey="uxEBayTemplateNameLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxEBayTemplateNameText" runat="server" ValidationGroup="ValidEBayTemplate"
                    Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredEBayTemplateNameValidator" runat="server"
                    ControlToValidate="uxEBayTemplateNameText" ValidationGroup="ValidEBayTemplate"
                    Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Template Name is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsPrivateListLabel" runat="server" meta:resourcekey="uxIsPrivateListLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsPrivateListCheck" runat="server" CssClass="CheckBox" Checked="false" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayCategories" runat="server" meta:resourcekey="uxEBayCategories" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxPrimaryEBayCategoryLabel" runat="server" meta:resourcekey="uxPrimaryEBayCategoryLabel"
                    CssClass="Label" />
                <asp:Label ID="uxPrimaryEBayCategoryNameLabel" runat="server" CssClass="Label" />
                <asp:LinkButton ID="uxChangedPrimaryCategoryLink" runat="server" CssClass="fl mgl5"
                    OnClick="uxChangedPrimaryCategoryLink_Click">Change...</asp:LinkButton>
                <asp:HiddenField ID="uxPrimaryEBayCategoryIDHidden" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <uc4:EBayCategorySelect ID="uxPrimaryCategorySelect" runat="server" />
                <asp:Button ID="uxPrimaryCategoryCancelButton" runat="server" OnClick="uxPrimaryCategoryCancelButton_Click"
                    Text="Cancel" Visible="false" />
                <asp:Button ID="uxPrimaryCategoryOkButton" runat="server" OnClick="uxPrimaryCategoryOkButton_Click"
                    Text="OK" Visible="false" />
                <uc3:Message ID="uxPrimaryCategoryMessage" runat="server" Visible="false" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxSecondaryEBayCateogryLabel" runat="server" meta:resourcekey="uxSecondaryEBayCateogryLabel"
                    CssClass="Label" />
                <asp:Label ID="uxSecondaryEBayCategoryNameLabel" runat="server" CssClass="Label" />
                <asp:LinkButton ID="uxChangedSecondaryCategoryLink" runat="server" CssClass="fl mgl5"
                    OnClick="uxChangedSecondaryCategoryLink_Click">Change...</asp:LinkButton>
                <asp:LinkButton ID="uxRemoveSecondaryCategoryLink" runat="server" CssClass="fl mgl5"
                    OnClick="uxRemoveSecondaryCategoryLink_Click">Remove</asp:LinkButton>
                <asp:HiddenField ID="uxSecondaryEBayCategoryIDHidden" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <uc4:EBayCategorySelect ID="uxSecondaryCategorySelect" runat="server" />
                <asp:Button ID="uxSecondaryCategoryCancelButton" runat="server" OnClick="uxSecondaryCategoryCancelButton_Click"
                    Text="Cancel" Visible="false" />
                <asp:Button ID="uxSecondaryCategoryOkButton" runat="server" OnClick="uxSecondaryCategoryOkButton_Click"
                    Text="OK" Visible="false" />
                <uc3:Message ID="uxSecondaryCategoryMessage" runat="server" Visible="false" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle" id="uxProductConditionDev" runat="server" visible="false">
                <asp:Label ID="uxProductConditionLabel" runat="server" CssClass="Label" meta:resourcekey="uxProductConditionLabel" />
                <asp:DropDownList ID="uxProductConditionDrop" runat="server" CssClass="DropDown">
                </asp:DropDownList>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBaySellingMethod" runat="server" meta:resourcekey="uxEBaySellingMethod" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBaySellingMethodLabel" runat="server" meta:resourcekey="uxEBaySellingMethodLabel"
                    CssClass="Label" />
                <asp:RadioButtonList ID="uxEBaySellingMethodRadioList" runat="server" OnSelectedIndexChanged="uxEBaySellingMethodRadioList_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="Online Auction" Selected="True">Online Auction</asp:ListItem>
                    <asp:ListItem Value="Fixed Price">Fixed Price</asp:ListItem>
                </asp:RadioButtonList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBaySellingMethodSetting" runat="server" Text="Online Auction Setting" />
            </div>
            <asp:Panel runat="server" ID="uxEBayReservePricePanel">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayIsUseReservePriceLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayIsUseReservePriceLabel"></asp:Label>
                    <asp:CheckBox ID="uxEBayIsUseReservePriceCheck" runat="server" CssClass="Check" Checked="false"
                        OnCheckedChanged="uxEBayIsUseReservePriceCheck_CheckedChanged" AutoPostBack="true" />
                </div>
                <asp:Panel runat="server" ID="uxEBayReservePriceSettingPanel" Visible="false">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayReservePriceTypeLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayReservePriceTypeLabel"></asp:Label>
                        <asp:RadioButtonList ID="uxEBayReservePriceTypeRadioList" runat="server" OnSelectedIndexChanged="uxEBayReservePriceTypeRadioList_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Value="ProductPrice" Selected="True">Use the product price</asp:ListItem>
                            <asp:ListItem Value="PricePlusAmount">Use the product price (plus|minus) amount</asp:ListItem>
                            <asp:ListItem Value="PricePlusPercentage">Use the product price (plus|minus) percentage</asp:ListItem>
                            <asp:ListItem Value="CustomPrice">Use the custom price</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="CommonRowStyle" runat="server" id="uxReservePriceValueDiv" visible="false">
                        <asp:Label ID="uxEBayReservePriceValueLabel" runat="server" CssClass="BulletLabel"
                            meta:resourcekey="uxEBayReservePriceValueLabel"></asp:Label>
                        <asp:TextBox ID="uxEBayReservePriceValueText" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="uxEBayStartingPricePanel">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayStartingPriceTypeLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayStartingPriceTypeLabel"></asp:Label>
                    <asp:RadioButtonList ID="uxEBayStartingPriceTypeRadioList" runat="server" OnSelectedIndexChanged="uxEBayStartingPriceTypeRadioList_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="ProductPrice" Selected="True">Use the product price</asp:ListItem>
                        <asp:ListItem Value="PricePlusAmount">Use the product price (plus|minus) amount</asp:ListItem>
                        <asp:ListItem Value="PricePlusPercentage">Use the product price (plus|minus) percentage</asp:ListItem>
                        <asp:ListItem Value="CustomPrice">Use the custom price</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="CommonRowStyle" runat="server" id="uxStartingPriceValueDiv" visible="false">
                    <asp:Label ID="uxEBayStartingPriceValueLabel" runat="server" CssClass="BulletLabel"
                        meta:resourcekey="uxEBayStartingPriceValueLabel"></asp:Label>
                    <asp:TextBox ID="uxEBayStartingPriceValueText" runat="server" CssClass="TextBox"
                        Width="150px"></asp:TextBox>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="uxEBayBuyItNowPricePanel">
                <div class="CommonRowStyle" id="uxEBayIsUseBuyItNowPriceDiv" runat="server">
                    <asp:Label ID="uxEBayIsUseBuyItNowPriceLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayIsUseBuyItNowPriceLabel"></asp:Label>
                    <asp:CheckBox ID="uxEBayIsUseBuyItNowPriceCheck" runat="server" CssClass="Check"
                        Checked="false" OnCheckedChanged="uxEBayIsUseBuyItNowPriceCheck_CheckedChanged"
                        AutoPostBack="true" />
                </div>
                <asp:Panel runat="server" ID="uxEBayBuyItNowPriceSettingPanel" Visible="false">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayBuyItNowPriceTypeLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayBuyItNowPriceTypeLabel"></asp:Label>
                        <asp:RadioButtonList ID="uxEBayBuyItNowPriceTypeRadioList" runat="server" OnSelectedIndexChanged="uxEBayBuyItNowPriceTypeRadioList_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Value="ProductPrice" Selected="True">Use the product price</asp:ListItem>
                            <asp:ListItem Value="PricePlusAmount">Use the product price (plus|minus) amount</asp:ListItem>
                            <asp:ListItem Value="PricePlusPercentage">Use the product price (plus|minus) percentage</asp:ListItem>
                            <asp:ListItem Value="CustomPrice">Use the custom price</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="CommonRowStyle" runat="server" id="uxEBayBuyItNowPriceValueDiv" visible="false">
                        <asp:Label ID="uxEBayBuyItNowPriceValueLabel" runat="server" CssClass="BulletLabel"
                            meta:resourcekey="uxEBayBuyItNowPriceValueLabel"></asp:Label>
                        <asp:TextBox ID="uxEBayBuyItNowPriceValueText" runat="server" CssClass="TextBox"
                            Width="150px"></asp:TextBox>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayListingDurationLabel" runat="server" CssClass="Label" meta:resourcekey="uxEBayListingDurationLabel"></asp:Label>
                <asp:DropDownList ID="uxEBayListingDurationDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Text="3 days" Value="3"></asp:ListItem>
                    <asp:ListItem Text="5 days" Value="5"></asp:ListItem>
                    <asp:ListItem Text="7 days" Value="7"></asp:ListItem>
                    <asp:ListItem Text="10 days" Value="10"></asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayItemDetails" runat="server" meta:resourcekey="uxEBayItemDetails" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxQuantityLable" runat="server" meta:resourcekey="uxQuantityLable"
                    CssClass="Label" />
                <asp:TextBox ID="uxQuantityText" runat="server" CssClass="TextBox" Width="150px"
                    ValidationGroup="ValidEBayTemplate" Enabled="false" Text="1"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredEBayTemplateQuantityValidator" runat="server"
                    ControlToValidate="uxQuantityText" ValidationGroup="ValidEBayTemplate" Display="Dynamic"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Quantity is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayQuantityNoticeLabel1" runat="server" CssClass="Label">&nbsp;</asp:Label>
                <div class="EbayWarningMessage fl">
                    If you would like to sell more than one item on eBay( Allow for Fixed Price listings
                    only), you must
                    <ul>
                        <li>Accept PayPal payment method and have feedback rating more than 15 OR</li>
                        <li>Have feedback rating more than 30 and registered eBay more than 14 days OR</li>
                        <li>eBay ID have been verified</li>
                    </ul>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsDisplayImageLabel" runat="server" meta:resourcekey="uxIsDisplayImageLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsDisplayImageCheck" runat="server" CssClass="CheckBox" Checked="true" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayItemLocation" runat="server" meta:resourcekey="uxEBayItemLocation"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxCountryLabel" runat="server" meta:resourcekey="uxCountryLabel" CssClass="Label"></asp:Label>
                <div class="CountrySelect fl">
                    <uc2:CountryList ID="uxCountryList" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxStateLabel" runat="server" meta:resourcekey="uxStateLabel" CssClass="Label"></asp:Label>
                <div class="CountrySelect fl">
                    <uc1:StateList ID="uxStateList" runat="server"></uc1:StateList>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayZipLabel" runat="server" meta:resourcekey="uxEBayZipLabel" CssClass="Label"></asp:Label>
                <asp:TextBox ID="uxEBayZipText" runat="server" ValidationGroup="ValidEBayTemplate"
                    Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredEBayTemplateZipValidator" runat="server"
                    ControlToValidate="uxEBayZipText" ValidationGroup="ValidEBayTemplate" Display="Dynamic"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Zip Code is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayPaymentDetailsLabel" runat="server" meta:resourcekey="uxEBayPaymentDetailsLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayPaymentMethodLabel" runat="server" meta:resourcekey="uxEBayPaymentMethodLabel"
                    CssClass="Label" />
                <asp:CheckBoxList ID="uxEBayPaymentMethodCheckList" runat="server" CssClass="CheckBox">
                    <asp:ListItem Value="AmEx">American Express</asp:ListItem>
                    <asp:ListItem Value="PayOnPickup">Payment On Delivery</asp:ListItem>
                    <asp:ListItem Value="Discover">Discover Card</asp:ListItem>
                    <asp:ListItem Value="IntegratedMerchantCreditCard">Integrated Merchant Credit Card</asp:ListItem>
                    <asp:ListItem Value="Moneybookers">Moneybookers</asp:ListItem>
                    <asp:ListItem Selected="True" Enabled="false" Value="PayPal">PayPal</asp:ListItem>
                    <asp:ListItem Value="Paymate">Paymate</asp:ListItem>
                    <asp:ListItem Value="ProPay">ProPay</asp:ListItem>
                    <asp:ListItem Value="VisaMC">Visa or Mastercard</asp:ListItem>
                </asp:CheckBoxList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayPayPalEmailAddressLabel" runat="server" meta:resourcekey="uxEBayPayPalEmailAddressLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxEBayPayPalEmailAddressText" runat="server" CssClass="TextBox"
                    ValidationGroup="ValidEBayTemplate" Width="150px" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredPayPalEmailValidator" runat="server" ControlToValidate="uxEBayPayPalEmailAddressText"
                    ValidationGroup="ValidEBayTemplate" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> PayPal Email Address is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularPayPalEmailValidator" runat="server"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="uxEBayPayPalEmailAddressText"
                    ValidationGroup="ValidEBayTemplate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email Format.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RegularExpressionValidator>
                <div class="Clear">
                </div>
            </div>
            <%--shipping details--%>
            <%--domestic--%>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayDomesticShippingDetailsLabel" runat="server" meta:resourcekey="uxEBayDomesticShippingDetailsLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayDomesticShippingTypeLabel" runat="server" meta:resourcekey="uxEBayDomesticShippingTypeLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxEBayDomesticShippingTypeDrop" runat="server" CssClass="DropDown"
                    AutoPostBack="true" OnSelectedIndexChanged="uxEBayDomesticShippingTypeDrop_SelectedIndexChanged">
                    <asp:ListItem Value="Flat">Flat</asp:ListItem>
                    <asp:ListItem Value="Calculate">Calculate</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle" runat="server" id="uxEBayDomesticPackageTypeDiv" visible="false">
                <asp:Label ID="uxEBayDomesticPackageTypeLabel" runat="server" meta:resourcekey="uxEBayDomesticPackageTypeLabel"
                    CssClass="Label"></asp:Label>
                <asp:DropDownList ID="uxEBayDomesticPackageTypeDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="Letter">Letter</asp:ListItem>
                    <asp:ListItem Value="LargeEnvelope">Large Envelope</asp:ListItem>
                    <asp:ListItem Value="PackageThickEnvelope">Package</asp:ListItem>
                    <asp:ListItem Value="ExtraLargePack">Large Package</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayDomesticShippingMethod1Label" runat="server" meta:resourcekey="uxEBayDomesticShippingMethod1Label"
                    CssClass="Label" />
                <uc5:EBayDomesticShipping ID="uxEBayDomesticShippingMethod1" runat="server" />
            </div>
            <asp:Panel ID="uxEBayDomesticShippingCostItemPanel1" runat="server">
                <div class="CommonRowStyle" runat="server">
                    <asp:Label ID="uxEBayDomesticShippingFirstItemLabel1" runat="server" meta:resourcekey="uxEBayDomesticShippingFirstItemLabel1"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingFirstItemText1" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredEBayDomesticShippingFirstItemText1Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingFirstItemText1" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                    <div class="CommonValidateDiv">
                    </div>
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxEBayDomesticShippingFirstItemText1Compare" runat="server"
                        ControlToValidate="uxEBayDomesticShippingFirstItemText1" Operator="DataTypeCheck"
                        Type="Integer" Display="Dynamic" ValidationGroup="ValidEBayTemplate" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is invalid.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:CompareValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayDomesticShippingNextItemLabel1" runat="server" meta:resourcekey="uxEBayDomesticShippingNextItemLabel1"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingNextItemText1" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequireduxEBayDomesticShippingNextItemText1Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingNextItemText1" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxEBayDomesticShippingNextItemText1Compare" runat="server"
                        ControlToValidate="uxEBayDomesticShippingNextItemText1" Operator="DataTypeCheck"
                        Type="Integer" Display="Dynamic" ValidationGroup="ValidEBayTemplate" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is invalid.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:CompareValidator>
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxIsFreeDomesticShippingLabel" runat="server" meta:resourcekey="uxIsFreeDomesticShippingLabel"
                    CssClass="BulletLabel" />
                <asp:CheckBox ID="uxIsFreeDomesticShippingCheck" runat="server" CssClass="CheckBox" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayDomesticShippingMethod2Label" runat="server" meta:resourcekey="uxEBayDomesticShippingMethod2Label"
                    CssClass="Label" />
                <uc5:EBayDomesticShipping ID="uxEBayDomesticShippingMethod2" runat="server" />
            </div>
            <asp:Panel ID="uxEBayDomesticShippingCostItemPanel2" runat="server">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayDomesticShippingFirstItemLabel2" runat="server" meta:resourcekey="uxEBayDomesticShippingFirstItemLabel2"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingFirstItemText2" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredEBayDomesticShippingFirstItemText2Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingFirstItemText2" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayDomesticShippingNextItemLabel2" runat="server" meta:resourcekey="uxEBayDomesticShippingNextItemLabel2"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingNextItemText2" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequireduxEBayDomesticShippingNextItemText2Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingNextItemText2" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayDomesticShippingMethod3Label" runat="server" meta:resourcekey="uxEBayDomesticShippingMethod3Label"
                    CssClass="Label" />
                <uc5:EBayDomesticShipping ID="uxEBayDomesticShippingMethod3" runat="server" />
            </div>
            <asp:Panel ID="uxEBayDomesticShippingCostItemPanel3" runat="server">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayDomesticShippingFirstItemLabel3" runat="server" meta:resourcekey="uxEBayDomesticShippingFirstItemLabel3"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingFirstItemText3" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredEBayDomesticShippingFirstItemText3Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingFirstItemText3" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayDomesticShippingNextItemLabel3" runat="server" meta:resourcekey="uxEBayDomesticShippingNextItemLabel3"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayDomesticShippingNextItemText3" runat="server" CssClass="TextBox"
                        ValidationGroup="ValidEBayTemplate" />
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequireduxEBayDomesticShippingNextItemText3Validator"
                        runat="server" ControlToValidate="uxEBayDomesticShippingNextItemText3" ValidationGroup="ValidEBayTemplate"
                        Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayDomesticIsGetItFastLabel" runat="server" meta:resourcekey="uxEBayDomesticIsGetItFastLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxEBayDomesticIsGetItFastCheck" runat="server" CssClass="CheckBox" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle" id="uxEBayDomesticHandlingCostDiv" runat="server" visible="false">
                <asp:Label ID="uxEBayDomesticHandlingCostLabel" runat="server" meta:resourcekey="uxEBayDomesticHandlingCostLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxEBayDomesticHandlingCostText" runat="server" CssClass="TextBox"
                    Width="150px" />
                <div class="Clear">
                </div>
            </div>
            <%--international--%>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayInternationalShippingDetailsLabel" runat="server" meta:resourcekey="uxEBayInternationalShippingDetailsLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayIsInterantionalShippingEnableLabel" runat="server" meta:resourcekey="uxEBayIsInterantionalShippingEnableLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxEBayIsInterantionalShippingEnableCheck" runat="server" CssClass="CheckBox"
                    OnCheckedChanged="uxEBayIsInterantionalShippingEnableCheck_CheckedChanged" AutoPostBack="true" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxInternationalShippingDetailsPanel" runat="server" Visible="false">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShippingTypeLabel" runat="server" meta:resourcekey="uxEBayInternationalShippingTypeLabel"
                        CssClass="Label" />
                    <asp:DropDownList ID="uxEBayInternationalShippingTypeDrop" runat="server" CssClass="DropDown"
                        AutoPostBack="true" OnSelectedIndexChanged="uxEBayInternationalShippingTypeDrop_SelectedIndexChanged">
                        <asp:ListItem Value="Flat">Flat</asp:ListItem>
                        <asp:ListItem Value="Calculate">Calculate</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle" runat="server" id="uxEBayInternationalPackageTypeDiv"
                    visible="false">
                    <asp:Label ID="uxEBayInternationalPackageTypeLabel" runat="server" meta:resourcekey="uxEBayInternationalPackageTypeLabel"
                        CssClass="Label"></asp:Label>
                    <asp:DropDownList ID="uxEBayInternationalPackageTypeDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="Letter">Letter</asp:ListItem>
                        <asp:ListItem Value="LargeEnvelope">Large Envelope</asp:ListItem>
                        <asp:ListItem Value="PackageThickEnvelope">Package</asp:ListItem>
                        <asp:ListItem Value="ExtraLargePack">Large Package</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShipTo1Label" runat="server" meta:resourcekey="uxEBayInternationalShipTo1Label"
                        CssClass="Label" />
                    <uc7:EBayInternationalShippingCountry ID="uxEBayInternationalShipTo1" runat="server" />
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShippingMethod1Label" runat="server" meta:resourcekey="uxEBayInternationalShippingMethod1Label"
                        CssClass="Label" />
                    <uc6:EBayInternationalShipping ID="uxEBayInternationalShippingMethod1" runat="server" />
                </div>
                <asp:Panel ID="uxEBayInternationalShippingCostItemPanel1" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingFirstItemLabel1" runat="server" meta:resourcekey="uxEBayInternationalShippingFirstItemLabel1"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingFirstItemText1" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingFirstItemText1Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingFirstItemText1"
                            ValidationGroup="ValidEBayTemplate" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingNextItemLabel1" runat="server" meta:resourcekey="uxEBayInternationalShippingNextItemLabel1"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingNextItemText1" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingNextItemText1Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingNextItemText1" ValidationGroup="ValidEBayTemplate"
                            Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShipTo2Label" runat="server" meta:resourcekey="uxEBayInternationalShipTo2Label"
                        CssClass="Label" />
                    <uc7:EBayInternationalShippingCountry ID="uxEBayInternationalShipTo2" runat="server" />
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShippingMethod2Label" runat="server" meta:resourcekey="uxEBayInternationalShippingMethod2Label"
                        CssClass="Label" />
                    <uc6:EBayInternationalShipping ID="uxEBayInternationalShippingMethod2" runat="server" />
                </div>
                <asp:Panel ID="uxEBayInternationalShippingCostItemPanel2" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingFirstItemLabel2" runat="server" meta:resourcekey="uxEBayInternationalShippingFirstItemLabel2"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingFirstItemText2" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingFirstItemText2Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingFirstItemText2"
                            ValidationGroup="ValidEBayTemplate" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingNextItemLabel2" runat="server" meta:resourcekey="uxEBayInternationalShippingNextItemLabel2"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingNextItemText2" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingNextItemText2Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingNextItemText2" ValidationGroup="ValidEBayTemplate"
                            Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShipTo3Label" runat="server" meta:resourcekey="uxEBayInternationalShipTo3Label"
                        CssClass="Label" />
                    <uc7:EBayInternationalShippingCountry ID="uxEBayInternationalShipTo3" runat="server" />
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayInternationalShippingMethod3Label" runat="server" meta:resourcekey="uxEBayInternationalShippingMethod3Label"
                        CssClass="Label" />
                    <uc6:EBayInternationalShipping ID="uxEBayInternationalShippingMethod3" runat="server" />
                </div>
                <asp:Panel ID="uxEBayInternationalShippingCostItemPanel3" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingFirstItemLabel3" runat="server" meta:resourcekey="uxEBayInternationalShippingFirstItemLabel3"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingFirstItemText3" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingFirstItemText3Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingFirstItemText3"
                            ValidationGroup="ValidEBayTemplate" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for First Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxEBayInternationalShippingNextItemLabel3" runat="server" meta:resourcekey="uxEBayInternationalShippingNextItemLabel3"
                            CssClass="BulletLabel" />
                        <asp:TextBox ID="uxEBayInternationalShippingNextItemText3" runat="server" CssClass="TextBox"
                            ValidationGroup="ValidEBayTemplate" />
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredEBayInternationalShippingNextItemText3Validator"
                            runat="server" ControlToValidate="uxEBayInternationalShippingNextItemText3" ValidationGroup="ValidEBayTemplate"
                            Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Cost for Next Item is required.
                            <div class="CommonValidateDiv">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <div class="CommonRowStyle" id="uxEBayInternationalHandlingCostDiv" runat="server"
                    visible="false">
                    <asp:Label ID="uxEBayInternationalHandlingCostLabel" runat="server" meta:resourcekey="uxEBayInternationalHandlingCostLabel"
                        CssClass="Label" />
                    <asp:TextBox ID="uxEBayInternationalHandlingCostText" runat="server" CssClass="TextBox"
                        Width="150px" />
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayAdditionShipping" runat="server" meta:resourcekey="uxEBayAdditionShipping" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayHandlingTimeLabel" runat="server" meta:resourcekey="uxEBayHandlingTimeLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxEBayHandlingTimeDrop" runat="server">
                    <asp:ListItem Value="1">1 Day</asp:ListItem>
                    <asp:ListItem Value="2">2 Days</asp:ListItem>
                    <asp:ListItem Value="3">3 Days</asp:ListItem>
                    <asp:ListItem Value="4">4 Days</asp:ListItem>
                    <asp:ListItem Value="5">5 Days</asp:ListItem>
                    <asp:ListItem Value="10">10 Days</asp:ListItem>
                    <asp:ListItem Value="15">15 Days</asp:ListItem>
                    <asp:ListItem Value="20">20 Days</asp:ListItem>
                    <asp:ListItem Value="30">30 Days</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBaySalesTaxLabel" runat="server" meta:resourcekey="uxEBaySalesTaxLabel"
                    CssClass="Label"></asp:Label>
                <asp:DropDownList ID="uxEBaySalesTaxDrop" runat="server" OnSelectedIndexChanged="uxEBaySalesTaxDrop_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="Not Charge">Not Charge</asp:ListItem>
                    <asp:ListItem Value="Charge">Charge For One State</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="uxEBaySalesTaxDiv" runat="server" visible="false">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBaySalesTaxStateLabel" runat="server" meta:resourcekey="uxEBaySalesTaxStateLabel"
                        CssClass="BulletLabel"></asp:Label>
                    <div class="CountrySelect fl">
                        <uc1:StateList ID="uxEBaySalesTaxStateList" runat="server" CountryCode="US"></uc1:StateList>
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBaySalesTaxValueLabel" runat="server" meta:resourcekey="uxEBaySalesTaxValueLabel"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBaySalesTaxValueText" runat="server" Width="150px" CssClass="TextBox"
                        Text="0">
                    </asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayIsTaxableShippingLabel" runat="server" meta:resourcekey="uxEBayIsTaxableShippingLabel"
                        CssClass="BulletLabel" />
                    <asp:CheckBox ID="uxEBayIsTaxableShippingCheck" runat="server" CssClass="CheckBox" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <%--shipping details--%>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayCheckoutReturnLabel" runat="server" meta:resourcekey="uxEBayCheckoutReturnLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayCheckoutInstructionLabel" runat="server" meta:resourcekey="uxEBayCheckoutInstructionLabel"
                    CssClass="Label" />
                <asp:TextBox ID="uxEBayCheckoutIstructionText" runat="server" Height="40px" TextMode="MultiLine"
                    Width="150px" CssClass="TextBox">
                </asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayIsAcceptReturnLabel" runat="server" meta:resourcekey="uxEBayIsAcceptReturnLabel"
                    CssClass="Label" />
                <asp:CheckBox ID="uxEBayIsAcceptReturnCheck" runat="server" CssClass="CheckBox" OnCheckedChanged="uxEBayIsAcceptReturnCheck_CheckedChanged"
                    AutoPostBack="true" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxAcceptReturnDetailsPanel" runat="server" Visible="false">
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayReturnOfferLabel" runat="server" meta:resourcekey="uxEBayReturnOfferLabel"
                        CssClass="BulletLabel" />
                    <asp:DropDownList ID="uxEBayReturnOfferDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="MoneyBack">Money Back</asp:ListItem>
                        <asp:ListItem Value="Exchange">Exchange</asp:ListItem>
                        <asp:ListItem Value="MerchandiseCredit">Merchandise Credit</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayReturnPeriodLabel" runat="server" meta:resourcekey="uxEBayReturnPeriodLabel"
                        CssClass="BulletLabel" />
                    <asp:DropDownList ID="uxEBayReturnPeriodDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="3">3 Days</asp:ListItem>
                        <asp:ListItem Value="7">7 Days</asp:ListItem>
                        <asp:ListItem Value="14">14 Days</asp:ListItem>
                        <asp:ListItem Value="30">30 Days</asp:ListItem>
                        <asp:ListItem Value="60">60 Days</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayReturnShippingPaidLabel" runat="server" meta:resourcekey="uxEBayReturnShippingPaidLabel"
                        CssClass="BulletLabel" />
                    <asp:DropDownList ID="uxEBayReturnShippingPaidDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="Buyer">Buyer</asp:ListItem>
                        <asp:ListItem Value="Seller">Seller</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxEBayAddionalPolicyLabel" runat="server" meta:resourcekey="uxEBayAddionalPolicyLabel"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxEBayAddionalPolicyText" runat="server" Height="40px" TextMode="MultiLine"
                        Width="150px" CssClass="TextBox">
                    </asp:TextBox>
                    <div class="Clear">
                    </div>
                </div>
            </asp:Panel>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayVisitorCounterLabel" runat="server" meta:resourcekey="uxEBayVisitorCounterLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayCounterStyleLabel" runat="server" meta:resourcekey="uxEBayCounterStyleLabel"
                    CssClass="Label" />
                <asp:RadioButtonList ID="uxEBayCounterStyleRadioList" runat="server" CssClass="Checkbox">
                    <asp:ListItem Value="None">No counter</asp:ListItem>
                    <asp:ListItem Value="Hidden">Hide counter(only merchant can view)</asp:ListItem>
                    <asp:ListItem Selected="True" Value="BasicStyle">Basic counter</asp:ListItem>
                    <asp:ListItem Value="RetroStyle">Retro style counter</asp:ListItem>
                </asp:RadioButtonList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayPaidListingOptionLabel" runat="server" meta:resourcekey="uxEBayPaidListingOptionLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxGalleryOptionLabel" runat="server" meta:resourcekey="uxGalleryOptionLabel"
                    CssClass="Label" />
                <asp:RadioButtonList ID="uxGalleryOptionRadioList" runat="server" CssClass="Radio"
                    OnSelectedIndexChanged="uxGalleryOptionRadioList_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="None">No Gallery</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Gallery">Gallery</asp:ListItem>
                    <asp:ListItem Value="Plus">Gallery Plus</asp:ListItem>
                    <asp:ListItem Value="Featured">Featured Gallery</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="CommonRowStyle" runat="server" id="uxGalleryDurationDiv" visible="false">
                <asp:Label ID="uxGalleryDurationLabel" runat="server" meta:resourcekey="uxGalleryDurationLabel"
                    CssClass="BulletLabel" />
                <asp:DropDownList ID="uxGalleryDurationDrop" runat="server">
                    <asp:ListItem Value="Days_7">7 Days</asp:ListItem>
                    <asp:ListItem Value="Lifetime">Life Time</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxEBayListingFeatureLabel" runat="server" meta:resourcekey="uxEBayListingFeatureLabel"
                    CssClass="Label" />
                <asp:CheckBoxList ID="uxEBayListringFeatureCheckList" runat="server" CssClass="CheckBox">
                    <asp:ListItem Value="BoldTitle">Bold Title</asp:ListItem>
                </asp:CheckBoxList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxEBayOtherSettingLabel" runat="server" meta:resourcekey="uxEBayOtherSettingLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxLotSizeLabel" runat="server" meta:resourcekey="uxLotSizeLabel" CssClass="Label" />
                <asp:TextBox ID="uxLotSizeText" runat="server" CssClass="TextBlx"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidEBayTemplate"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ValidEBayTemplate"></vevo:AdvanceButton>
            </div>
            <div class="Clear">
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
