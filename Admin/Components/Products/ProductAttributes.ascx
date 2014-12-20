<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAttributes.ascx.cs"
    Inherits="AdminAdvanced_Components_Products_ProductAttributes" %>
<%@ Register Src="../QuantityDiscount.ascx" TagName="QuantityDiscount" TagPrefix="uc5" %>
<%@ Register Src="../Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="ProductSeo.ascx" TagName="ProductSeo" TagPrefix="uc21" %>
<%@ Register Src="../CalendarPopup.ascx" TagName="CalendarPopUp" TagPrefix="uc7" %>
<%@ Register Src="InventoryAndOption.ascx" TagName="InventoryAndOption" TagPrefix="uc22" %>
<div class="ProductDetailsRowTitle mgt10">
    <asp:Label ID="lcProductAttribute" runat="server" meta:resourcekey="lcProductAttribute" />
</div>
<asp:Panel ID="uxCreateDatePanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcCreateDateTime" runat="server" meta:resourcekey="lcCreateDateTime"
        CssClass="Label" />
    <asp:Label ID="uxCreateDateTime" runat="server" Width="210px" CssClass="Label" />
    <uc7:CalendarPopUp ID="uxCrateDateCalendar" runat="server" Visible="false" TextBoxEnabled="false" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxViewCountPanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcViewCount" runat="server" meta:resourcekey="lcViewCount" CssClass="Label" />
    <asp:Label ID="uxViewCount" runat="server" Width="210px" CssClass="Label" />
    <div class="Clear">
    </div>
</asp:Panel>
<div class="ProductDetailsRow" id="uxIsEnabledTR" runat="server">
    <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
    <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked="true" CssClass="fl" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxIsDownloadableTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcDownloadableLabel" runat="server" meta:resourcekey="lcDownloadable"
        CssClass="Label" />
    <asp:CheckBox ID="uxDownloadableCheck" runat="server" CssClass="fl" AutoPostBack="true" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxDownloadPathTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcDownloadPathLabel" runat="server" meta:resourcekey="lcDownloadPath"
        CssClass="Label" />
    <asp:TextBox ID="uxDownloadPathText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxDownloadPathLinkButton" runat="server" CssClass="fl mgl5" OnClick="uxDownloadPathLinkButton_Click">Upload...</asp:LinkButton>
    <div class="Clear">
    </div>
</asp:Panel>
<uc6:Upload ID="uxDownloadPathUpload" runat="server" CssClass="ProductDetailsRow"
    MaxFileSize="100 MB" ShowControl="false" ButtonImage="SelectFiles.png" ButtonWidth="85"
    ButtonHeight="22" ShowText="false" />
<div class="ProductDetailsRow">
    <asp:Label ID="lcSku" runat="server" meta:resourcekey="lcSku" CssClass="Label" />
    <asp:TextBox ID="uxSkuText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxSkuRequiredValidator" runat="server" ControlToValidate="uxSkuText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> SKU is required.
        <div class="CommonValidateDiv CommonValidateDivProductSku">
        </div>                
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="uxBrandLabel" runat="server" meta:resourcekey="uxBrandLabel" CssClass="Label" />
    <asp:TextBox ID="uxBrandText" runat="server" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcModel" runat="server" meta:resourcekey="lcModel" CssClass="Label" />
    <asp:TextBox ID="uxModelText" runat="server" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow" id="AddImagesTR" runat="server">
    <asp:Label ID="lcImage" runat="server" meta:resourcekey="lcImage" CssClass="Label" />
    <vevo:AdvanceButton ID="uxAddImagesButton" runat="server" meta:resourcekey="uxAddImagesButton"
        CssClassBegin="fl" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddImagesButton_Click"
        OnClickGoTo="None"></vevo:AdvanceButton>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxOptionalImageTR" runat="server" CssClass="ProductDetailsRow dn">
    <asp:Label ID="lcOptionalImage" runat="server" meta:resourcekey="lcOptionalImage"
        CssClass="Label" />
    <asp:TextBox ID="uxImageSecondaryText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxOptionalUploadLinkButton" runat="server" CssClass="fl mgl5"
        OnClick="uxOptionalUploadLinkButton_Click">Upload...</asp:LinkButton>
    <div class="Clear">
    </div>
</asp:Panel>
<uc6:Upload ID="uxOptionalUpload" runat="server" CheckType="Image" CssClass="CommonRowStyle ProductDetailsRow"
    ShowControl="false" />
<div class="ProductDetailsRow">
    <asp:Label ID="Label2" runat="server" CssClass="Label">&nbsp;</asp:Label>
    <asp:Image ID="uxProductImage" runat="server" SkinID="EmptyImages" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxIsDynamicProductKitWeightPanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcIsDynamicWeight" runat="server" meta:resourcekey="lcIsDynamicWeight"
        CssClass="Label" />
    <asp:CheckBox ID="uxIsDynamicProductKitWeightCheck" runat="server" CssClass="fl" />
    <div class="Clear">
    </div>
</asp:Panel>
<div id="uxWeightTR" runat="server" class="ProductDetailsRow">
    <asp:Label ID="lcWeight" runat="server" meta:resourcekey="lcWeight" CssClass="Label" />
    <asp:TextBox ID="uxWeightText" runat="server" Width="70px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div id="uxProductDimensionsTR" runat="server" class="ProductDetailsRow">
    <asp:Label ID="uxProductDimensionsLabel" runat="server" meta:resourcekey="uxProductDimensionsLabel"
        CssClass="Label" />
    <div class="Clear">
    </div>
</div>
<div id="uxLengthTR" runat="server" class="ProductDetailsRow">
    <div class="BulletLabel">
        <asp:Label ID="uxLengthLabel" runat="server" meta:resourcekey="uxLengthLabel" CssClass="fl" />
    </div>
    <asp:TextBox ID="uxLengthText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:CompareValidator ID="uxLengthCompare" runat="server" ControlToValidate="uxLengthText"
        Operator="DataTypeCheck" Type="Double" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Length is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxWidthTR" runat="server" class="ProductDetailsRow">
    <div class="BulletLabel">
        <asp:Label ID="uxWidthLabel" runat="server" meta:resourcekey="uxWidthLabel" CssClass="fl" />
    </div>
    <asp:TextBox ID="uxWidthText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:CompareValidator ID="uxWidthCompare" runat="server" ControlToValidate="uxWidthText"
        Operator="DataTypeCheck" Type="Double" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Width is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxHeightTR" runat="server" class="ProductDetailsRow">
    <div class="BulletLabel">
        <asp:Label ID="uxHeightLabel" runat="server" meta:resourcekey="uxHeightLabel" CssClass="fl" />
    </div>
    <asp:TextBox ID="uxHeightText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:CompareValidator ID="uxHeightCompare" runat="server" ControlToValidate="uxHeightText"
        Operator="DataTypeCheck" Type="Double" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Height is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxIsFreeShippingCostTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcIsFreeShippingCostLabel" runat="server" meta:resourcekey="lcIsFreeShippingCost"
        CssClass="Label" />
    <asp:CheckBox ID="uxIsFreeShippingCostCheck" runat="server" CssClass="fl" AutoPostBack="true" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxFixedShippingCostTR" runat="server" CssClass="ProductDetailsRow">
    <div class="ProductDetailsRow">
        <asp:Label ID="lcFixed" runat="server" meta:resourcekey="lcFixed" CssClass="Label" />
        <asp:DropDownList ID="uxFixedShippingCostDrop" runat="server" AutoPostBack="true"
            CssClass="fl DropDown">
            <asp:ListItem Value="True">Yes</asp:ListItem>
            <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="uxShippingCostTR" runat="server" CssClass="ProductDetailsRow">
</asp:Panel>
<div class="ProductDetailsRow">
    <asp:Label ID="lcManufacturer" runat="server" meta:resourcekey="lcManufacturer" CssClass="Label" />
    <asp:DropDownList ID="uxManufacturerDrop" runat="server" CssClass="fl DropDown">
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcManufacturerPartNumber" runat="server" meta:resourcekey="lcManufacturerPartNumber"
        CssClass="Label" />
    <asp:TextBox ID="uxManufacturerPartNumberText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcUPC" runat="server" meta:resourcekey="lcUPC" CssClass="Label" />
    <asp:TextBox ID="uxUpcText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxCallForPricePanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcIsCallForPrice" runat="server" meta:resourcekey="lcIsCallForPrice"
        CssClass="Label" />
    <asp:CheckBox ID="uxIsCallForPriceCheck" runat="server" OnCheckedChanged="uxIsCallForPriceCheck_OnCheckedChanged"
        AutoPostBack="true" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxUseCustomPriceTR" runat="server" CssClass="ProductDetailsRow" Visible="true">
    <asp:Label ID="uxUseCustomPriceLabel" runat="server" meta:resourcekey="lcUseCustomPrice"
        CssClass="Label" />
    <asp:CheckBox ID="uxUseCustomPriceCheck" runat="server" OnCheckedChanged="uxUseCustomPriceCheck_OnCheckedChanged"
        AutoPostBack="true" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxIsDynamicProductKitPricePanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcIsDynamicProductKitPrice" runat="server" meta:resourcekey="lcIsDynamicPrice"
        CssClass="Label" />
    <asp:CheckBox ID="uxIsDynamicProductKitPriceCheck" runat="server" CssClass="fl" />
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxCustomPriceTR" runat="server" CssClass="ProductDetailsRow" Visible="false">
    <div class="BulletLabel">
        <asp:Label ID="uxDefaultPriceLabel" runat="server" meta:resourcekey="lcDefaultPrice"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxDefaultPriceText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:CompareValidator ID="uxDefaultPriceCompare" runat="server" ControlToValidate="uxDefaultPriceText"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Your default price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CompareValidator>
    <div class="Clear">
    </div>
    <div class="BulletLabel">
        <asp:Label ID="uxMinimumPriceLabel" runat="server" meta:resourcekey="lcMinimumPrice"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxMinimumPriceText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:CompareValidator ID="uxMinimumPriceCompare" runat="server" ControlToValidate="uxMinimumPriceText"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Your minimum price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CompareValidator>
    <asp:CustomValidator ID="uxMinimumPriceCompare1" runat="server" ControlToValidate="uxMinimumPriceText"
        OnServerValidate="CustomPriceValidator" Display="Dynamic" ValidationGroup="VaildProduct"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Minimum price should be less than or equal default price.
        <div class="CommonValidateDiv CommonValidateDivProductMinPrice">
        </div>
    </asp:CustomValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxWholesalePriceTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxWholesalePriceLabel" runat="server" meta:resourcekey="lcWholesalePrice"
        CssClass="Label" />
    <asp:TextBox ID="uxWholesalePriceText" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
        <asp:CheckBox ID="uxWholesaleCheck" runat="server" OnCheckedChanged="uxWholesaleCheck_OnCheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:Label ID="uxUseDefaultWholesaleLabel" runat="server" meta:resourcekey="uxUseDefaultWholesaleLabel"></asp:Label>
    <asp:CompareValidator ID="uxWholesalepriceCompare" runat="server" ControlToValidate="uxWholesalePriceText"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="uxWholesaleRequired" runat="server" ControlToValidate="uxWholesalePriceText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wholesale Price is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxWholesalePrice2TR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxWholesalePrice2Label" runat="server" meta:resourcekey="lcWholesalePrice2"
        CssClass="Label" />
    <asp:TextBox ID="uxWholesalePrice2Text" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
        <asp:CheckBox ID="uxWholesale2Check" runat="server" OnCheckedChanged="uxWholesaleCheck2_OnCheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:Label ID="uxUseDefaultWholesale2Label" runat="server" meta:resourcekey="uxUseDefaultWholesale2Label"></asp:Label>
    <asp:CompareValidator ID="uxWholesaleprice2Compare" runat="server" ControlToValidate="uxWholesalePrice2Text"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="uxWholesale2Required" runat="server" ControlToValidate="uxWholesalePrice2Text"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wholesale Price is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxWholesalePrice3TR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxWholesalePrice3Label" runat="server" meta:resourcekey="lcWholesalePrice3"
        CssClass="Label" />
    <asp:TextBox ID="uxWholesalePrice3Text" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
        <asp:CheckBox ID="uxWholesale3Check" runat="server" OnCheckedChanged="uxWholesaleCheck3_OnCheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:Label ID="uxUseDefaultWholesale3Label" runat="server" meta:resourcekey="uxUseDefaultWholesale3Label"></asp:Label>
    <asp:CompareValidator ID="uxWholesaleprice3Compare" runat="server" ControlToValidate="uxWholesalePrice3Text"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="uxWholesale3Required" runat="server" ControlToValidate="uxWholesalePrice3Text"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wholesale Price is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxPriceTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcPrice" runat="server" meta:resourcekey="lcPrice" CssClass="Label" />
    <asp:TextBox ID="uxPriceText" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
        <asp:CheckBox ID="uxPriceCheck" runat="server" OnCheckedChanged="uxPriceCheck_OnCheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:Label ID="uxIncludeVatLabel" runat="server" Font-Bold="True" ForeColor="Red"
        meta:resourcekey="lcIncludeVat" OnPreRender="uxIncludeVatLabel_PreRender"></asp:Label>
    <asp:Label ID="uxUseDefaultPriceLabel" runat="server" meta:resourcekey="uxUseDefaultPriceLabel"></asp:Label>
    <asp:CompareValidator ID="uxPriceCompare" runat="server" ControlToValidate="uxPriceText"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="uxPriceRequiredValidator" runat="server" ControlToValidate="uxPriceText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxRetailPriceTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcRetailPrice" runat="server" meta:resourcekey="lcRetailPrice" CssClass="Label" />
    <asp:TextBox ID="uxRetailPriceText" runat="server" Width="70px" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
        <asp:CheckBox ID="uxRetailPriceCheck" runat="server" OnCheckedChanged="uxRetailPriceCheck_OnCheckedChanged"
            AutoPostBack="true" />
    </div>
    <asp:Label ID="uxUseDefaultRetailLabel" runat="server" meta:resourcekey="uxUseDefaultRetailLabel"></asp:Label>
    <asp:CompareValidator ID="uxRetailPriceCompare" runat="server" ControlToValidate="uxRetailPriceText"
        Operator="DataTypeCheck" Type="Currency" ValidationGroup="VaildProduct" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Retail Price is invalid.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:CompareValidator>
    <asp:RequiredFieldValidator ID="uxRetailPriceRequiredValidator" runat="server" ControlToValidate="uxRetailPriceText"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Retail Price is required.
        <div class="CommonValidateDiv CommonValidateDivProductPrice">
        </div>   
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<div class="ProductDetailsRow">
    <asp:Label ID="lcKeywords" runat="server" meta:resourcekey="lcKeywords" CssClass="Label" />
    <asp:TextBox ID="uxKeywordsText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcIsTodaySpecial" runat="server" meta:resourcekey="lcIsTodaySpecial"
        CssClass="Label" />
    <asp:CheckBox ID="uxIsTodaySpecialCheck" runat="server" CssClass="fl" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxQuantityDiscountTR" runat="server" CssClass="ProductDetailsRow">
    <uc5:QuantityDiscount ID="uxQuantityDiscount" runat="server" />
</asp:Panel>
<asp:Panel ID="uxRmaTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxRmaLabel" runat="server" meta:resourcekey="uxRmaLabel" CssClass="Label" />
    <asp:TextBox ID="uxRmaText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:Label ID="uxRmaMessageLabel" runat="server" meta:resourcekey="uxRmaMessageLabel" />
    <asp:RangeValidator ID="uxRmaRangeValidator" ControlToValidate="uxRmaText" MinimumValue="0"
        MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="VaildProduct"
        Display="Dynamic" CssClass="CommonValidatorText CommonValidatorTextProductDetailRMA">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Natural Number.
        <div class="CommonValidateDiv CommonValidateDivProductRMA">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<uc22:InventoryAndOption ID="uxInventoryAndOption" runat="server" />
<div class="ProductDetailsRow">
    <asp:Label ID="uxSpecificationGroup" runat="server" Text="Specification Group" CssClass="Label" />
    <asp:DropDownList ID="uxSpecificationGroupDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxSpecificationGroupDrop_SelectedIndexChanged">
    </asp:DropDownList>
</div>
<asp:Panel ID="uxSpecificationItemTR" runat="server" CssClass="ProductDetailsRow">
</asp:Panel>
<div class="ProductDetailsRow">
    <asp:Label ID="lcMinimumQuantity" runat="server" meta:resourcekey="lcMinimumQuantity"
        CssClass="Label" />
    <asp:TextBox ID="uxMinimumQuantityText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:RangeValidator ID="uxMinimumQuantityValidator" ControlToValidate="uxMinimumQuantityText"
        MinimumValue="1" MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="VaildProduct"
        Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Natural Number and greater than 1.
        <div class="CommonValidateDiv CommonValidateDivProductMinQuantity">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcMaximumQuantity" runat="server" meta:resourcekey="lcMaximumQuantity"
        CssClass="Label" />
    <asp:TextBox ID="uxMaximumQuantityText" runat="server" Width="70px" CssClass="TextBox" />
    <asp:Label ID="lcMaximumQuantityLimit" runat="server" meta:resourcekey="lcMaximumQuantityLimit" />
    <asp:RangeValidator ID="uxMaximumQuantityValidator" ControlToValidate="uxMaximumQuantityText"
        MinimumValue="0" MaximumValue="2147483647" Type="Integer" runat="server" meta:resourcekey="uxMaximumQuantityValidator"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Natural Number and greater than minimum quantity or 0 for unlimited.
        <div class="CommonValidateDiv CommonValidateDivProductMaxQuantity">
        </div>
    </asp:RangeValidator>
    <asp:CustomValidator ID="uxQuantityValidator" runat="server" ControlToValidate="uxMaximumQuantityText"
        OnServerValidate="QuantityValidator" Display="Dynamic" ValidationGroup="VaildProduct"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be a Natural Number and greater than minimum quantity or 0 for unlimited.
        <div class="CommonValidateDiv CommonValidateDivProductMaxQuantity">
        </div>
    </asp:CustomValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxRelateProductTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcRelatedProducts" runat="server" meta:resourcekey="lcRelatedProducts"
        CssClass="Label" />
    <asp:TextBox ID="uxRelatedProducts" runat="server" Width="258px" CssClass="TextBox"></asp:TextBox>
    <asp:RegularExpressionValidator ID="uxRegularRelatedProductsValidator" runat="server"
        ValidationExpression="^\s*\d+\s*(,\s*\d+\s*)*$" ControlToValidate="uxRelatedProducts"
        ValidationGroup="VaildProduct" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Related products must be numbers seperated by commas (,).
        <div class="CommonValidateDiv CommonValidateDivProductRelated">
        </div>
    </asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxProductRatingRow" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="lcProductRating" runat="server" meta:resourcekey="lcProductRating"
        CssClass="Label" />
    <asp:TextBox ID="uxProductRating" runat="server" Width="90px" CssClass="TextBox"></asp:TextBox>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxTaxClassTR" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxTaxCalssIDLabel" runat="server" Text="Tax Class" CssClass="Label" />
    <asp:DropDownList ID="uxTaxClassDrop" runat="server" CssClass="fl DropDown">
    </asp:DropDownList>
</asp:Panel>
<uc21:ProductSeo ID="uxProductSeo" runat="server" />
<div class="ProductDetailsRow">
    <asp:Label ID="lcOtherOne" runat="server" meta:resourcekey="lcOtherOne" CssClass="Label" />
    <asp:TextBox ID="uxOtherOneText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcOtherTwo" runat="server" meta:resourcekey="lcOtherTwo" CssClass="Label" />
    <asp:TextBox ID="uxOtherTwoText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcOtherThree" runat="server" meta:resourcekey="lcOtherThree" CssClass="Label" />
    <asp:TextBox ID="uxOtherThreeText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcOtherFour" runat="server" meta:resourcekey="lcOtherFour" CssClass="Label" />
    <asp:TextBox ID="uxOtherFourText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="llcOtherFive" runat="server" meta:resourcekey="lcOtherFive" CssClass="Label" />
    <asp:TextBox ID="uxOtherFiveText" runat="server" Width="210px" CssClass="TextBox" />
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <asp:Label ID="lcIsAffiliate" runat="server" meta:resourcekey="lcIsAffiliate" CssClass="Label" />
    <asp:CheckBox ID="uxIsAffiliate" runat="server" CssClass="fl" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxProductLayoutOverridePanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxOverrideProductLayoutLabel" runat="server" meta:resourcekey="lcOverrideProductLayoutLabel"
        CssClass="Label" />
    <asp:DropDownList ID="uxOverrideProductLayoutDrop" runat="server" AutoPostBack="false"
        CssClass="fl DropDown">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Selected="True" Value="False">No</asp:ListItem>
    </asp:DropDownList>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxProductDetailsLayoutPathPanel" runat="server" CssClass="ProductDetailsRow">
    <asp:Label ID="uxProductDetailsLayoutPathLabel" runat="server" meta:resourcekey="lcProductDetailsLayoutPathLabel"
        CssClass="Label" />
    <asp:DropDownList ID="uxProductDetailsLayoutPathDrop" runat="server" AutoPostBack="false"
        CssClass="fl DropDown">
    </asp:DropDownList>
    <div class="Clear">
    </div>
</asp:Panel>
