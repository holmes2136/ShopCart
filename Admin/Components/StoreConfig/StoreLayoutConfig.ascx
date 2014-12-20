<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreLayoutConfig.ascx.cs"
    Inherits="Admin_Components_StoreConfig_StoreLayoutConfig" %>
<%@ Register Src="../Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxStoreBannerModuleDisplayHelp" ConfigName="StoreBannerModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcStoreBannerModuleDisplay" runat="server" meta:resourcekey="lcStoreBannerModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxStoreBannerModuleDisplayDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxStoreBannerModuleDisplayDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxStoreBannerEffectModeTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxStoreBannerEffectModeHelp" runat="server" ConfigName="StoreBannerEffectMode" />
    <div class="BulletLabel">
        <asp:Label ID="uxStoreBannerEffectModeLabel" runat="server" meta:resourcekey="uxStoreBannerEffectModeLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxStoreBannerEffectModeDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxStoreBannerEffectModeDrop_SelectedIndexChanged">
    </asp:DropDownList>
</div>
<div id="uxStoreBannerSlideSpeedTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxStoreBannerSlideSpeedHelp" ConfigName="StoreBannerSlideSpeed"
        runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxStoreBannerSlideSpeedLabel" runat="server" meta:resourcekey="uxStoreBannerSlideSpeedLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxStoreBannerSlideSpeedText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxStoreBannerSlideSpeedRequired" runat="server" ControlToValidate="uxStoreBannerSlideSpeedText"
        CssClass="CommonValidatorText" Display="Dynamic" ValidationGroup="CouponVaild">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Length Of Sliding Display is required
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxStoreBannerSlideSpeedCompare" runat="server" ControlToValidate="uxStoreBannerSlideSpeedText"
        Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</div>
<div id="uxStoreBannerEffectPeriodTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxStoreBannerEffectPeriodHelp" ConfigName="StoreBannerEffectPeriod"
        runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxStoreBannerEffectPeriodLabel" runat="server" meta:resourcekey="uxStoreBannerEffectPeriodLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxStoreBannerEffectPeriodText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxStoreBannerEffectPeriodRequired" runat="server"
        ControlToValidate="uxStoreBannerEffectPeriodText" CssClass="CommonValidatorText"
        Display="Dynamic" ValidationGroup="CouponVaild">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Length Of Banner Effect is required
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxStoreBannerEffectPeriodCompare" runat="server" ControlToValidate="uxStoreBannerEffectPeriodText"
        Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxCategoryListModuleDisplayHelp" ConfigName="CategoryListModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcCategoryListModuleDisplay" runat="server" meta:resourcekey="lcCategoryListModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxCategoryListModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxDepartmentListModuleDisplayHelp" ConfigName="DepartmentListModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxDepartmentListModuleDisplayLabel" runat="server" meta:resourcekey="uxDepartmentListModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxDepartmentListModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxDepartmentHeaderMenuDisplayHelp" ConfigName="DepartmentHeaderMenuDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxDepartmentHeaderMenuDisplayLabel" runat="server" meta:resourcekey="uxDepartmentHeaderMenuDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxDepartmentHeaderMenuDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableManufacturerHelp" ConfigName="EnableManufacturer" runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableManufacturerLabel" runat="server" meta:resourcekey="uxEnableManufacturerLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableManufacturerDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxManufacturerHeaderMenuDisplayHelp" ConfigName="ManufacturerHeaderMenuDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxManufacturerHeaderMenuDisplayLabel" runat="server" meta:resourcekey="uxManufacturerHeaderMenuDisplayLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxManufacturerHeaderMenuDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxHeaderMenuStyleHelp" ConfigName="HeaderMenuStyle" runat="server" />
    <div class="Label">
        <asp:Label ID="uxHeaderMenuStyleDisplayLabel" runat="server" meta:resourcekey="uxHeaderMenuStyleDisplayLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxHeaderMenuStyleDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="default" Text="Default" />
        <asp:ListItem Value="group" Text="Group dropdown" />
        <asp:ListItem Value="cascade" Text="Cascade dropdown" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxTodaySpecialModuleDisplayHelp" ConfigName="TodaySpecialModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcTodaySpecialModuleDisplay" runat="server" meta:resourcekey="lcTodaySpecialModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxTodaySpecialModuleDisplayDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxTodaySpecialModuleDisplayDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxTodaySpecialModuleEffectTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTodaySpecialModuleEffectHelp" ConfigName="ProductSpecialEffectMode"
        runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxTodaySpecialModuleEffectLabel" runat="server" meta:resourcekey="lcTodaySpecialModuleEffect"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxTodaySpecialModuleEffectDrop" runat="server" CssClass="fl DropDown">
    </asp:DropDownList>
</div>
<div id="uxTodaySpecialModuleSpeedTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTodaySpecialModuleSpeedHelp" ConfigName="ProductSpecialTransitionSpeed"
        runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxTodaySpecialModuleSpeedLabel" runat="server" meta:resourcekey="lcTodaySpecialModuleSpeed"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxTodaySpecialModuleSpeedText" runat="server" CssClass="TextBox" />
    <asp:CompareValidator ID="uxTodaySpecialModuleSpeedCompare" runat="server" ControlToValidate="uxTodaySpecialModuleSpeedText"
        Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
        <div class="CommonValidateDiv CommonValidateDivTodaySpecial">
        </div>
    </asp:CompareValidator>
</div>
<div id="uxTodaySpecialModuleWaitTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTodaySpecialModuleWaitHelp" ConfigName="ProductSpecialEffectWaitTime"
        runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="uxTodaySpecialModuleWaitLabel" runat="server" meta:resourcekey="lcTodaySpecialModuleWait"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxTodaySpecialModuleWaitText" runat="server" CssClass="TextBox" />
    <asp:CompareValidator ID="uxTodaySpecialModuleWaitCompare" runat="server" ControlToValidate="uxTodaySpecialModuleWaitText"
        Operator="DataTypeCheck" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer.
        <div class="CommonValidateDiv CommonValidateDivTodaySpecial">
        </div>
    </asp:CompareValidator>
</div>
<div id="uxCurrencyTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxCurrencyModuleDisplayHelp" ConfigName="CurrencyModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcCurrencyModuleDisplay" runat="server" meta:resourcekey="lcCurrencyModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxCurrencyModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<div id="uxDisplayCurrencyCodeTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxDisplayCurrencyHelp" ConfigName="DefaultDisplayCurrencyCode"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxDisplayCurrencyCodeLabel" runat="server" meta:resourcekey="uxDisplayCurrencyCodeLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxDisplayCurrencyCodeDrop" runat="server" DataTextField="Name"
        DataValueField="CurrencyCode" CssClass="fl DropDown">
    </asp:DropDownList>
</div>
<div id="uxNewsletterModuleDisplayTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxNewsletterModuleDisplayHelp" ConfigName="NewsletterModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcNewsletterModuleDisplay" runat="server" meta:resourcekey="lcNewsletterModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxNewsletterModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSpecialOfferModuleDisplayHelp" ConfigName="SpecialOfferModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcSpecialOfferModuleDisplay" runat="server" meta:resourcekey="lcSpecialOfferModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxSpecialOfferModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSpecialOfferModuleImageHelp" ConfigName="SpecialOfferImage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSpecialOfferModuleImage" runat="server" meta:resourcekey="lcSpecialOfferModuleImage"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxSpecialOfferImageText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxSpecialOfferUploadLinkButton" runat="server" OnClick="uxSpecialOfferUploadLinkButton_Click"
        CssClass="fl mgl5">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxSpecialOfferUpload" runat="server" ShowControl="false" CssClass="ConfigRow"
    CheckType="Image" ShowText="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxPaymentLogoModuleDisplayHelp" ConfigName="PaymentLogoModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcPaymentLogoModuleDisplay" runat="server" meta:resourcekey="lcPaymentLogoModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxPaymentLogoModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxPaymentLogoModuleImageHelp" ConfigName="PaymentLogoImage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcPaymentLogoModuleImage" runat="server" meta:resourcekey="lcPaymentLogoModuleImage"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxPaymentLogoImageText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxPaymentLogoUploadLinkButton" runat="server" OnClick="uxPaymentLogoUploadLinkButton_Click"
        CssClass="fl mgl5">Upload...</asp:LinkButton></div>
<uc6:Upload ID="uxPaymentLogoUpload" runat="server" ShowControl="false" CssClass="ConfigRow"
    CheckType="Image" ShowText="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSearchModuleDisplayHelp" ConfigName="SearchModuleDisplay" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSearchModuleDisplay" runat="server" meta:resourcekey="lcSearchModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxSearchModuleDisplayDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxSearchModuleDisplayDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxSearchModuleOptionPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDisplayCategoryInQuickSearchHelp" ConfigName="DisplayCategoryInQuickSearch"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxDisplayCategoryInQuickSearchLabel" runat="server" meta:resourcekey="uxDisplayCategoryInQuickSearchLabel"
                CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxDisplayCategoryInQuickSearchDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
</asp:Panel>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxMiniCartModuleDisplayHelp" ConfigName="MiniCartModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcMiniCartModuleDisplay" runat="server" meta:resourcekey="lcMiniCartModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxMiniCartModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxCouponModuleDisplayTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxCouponModuleDisplayHelp" ConfigName="CouponModuleDisplay" runat="server" />
    <div class="Label">
        <asp:Label ID="lcCouponModuleDisplay" runat="server" meta:resourcekey="lcCouponModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxCouponModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantModuleDisplayHelp" ConfigName="FeaturedMerchantModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantModuleDisplay" runat="server" meta:resourcekey="lcFeaturedMerchantModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFeaturedMerchantModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantCountHelp" ConfigName="FeaturedMerchantCount"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantCount" runat="server" meta:resourcekey="lcFeaturedMerchantCount"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFeaturedMerchantCountDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Text="1" />
        <asp:ListItem Text="2" />
        <asp:ListItem Text="3" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantModuleImage1Help" ConfigName="FeaturedMerchantImage1"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantModuleImage1" runat="server" meta:resourcekey="lcFeaturedMerchantModuleImage1"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxFeaturedMerchantImage1Text" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxFeaturedMerchantUpload1LinkButton" runat="server" OnClick="uxFeaturedMerchantUpload1LinkButton_Click"
        CssClass="fl mgl5">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxFeaturedMerchantUpload1" ShowControl="false" CssClass="ConfigRow"
    runat="server" CheckType="Image" ShowText="false" ButtonImage="SelectImages.png"
    ButtonWidth="105" ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantModuleImage2Help" ConfigName="FeaturedMerchantImage2"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantModuleImage2" runat="server" meta:resourcekey="lcFeaturedMerchantModuleImage2"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxFeaturedMerchantImage2Text" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxFeaturedMerchantUpload2LinkButton" runat="server" CssClass="fl mgl5"
        OnClick="uxFeaturedMerchantUpload2LinkButton_Click">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxFeaturedMerchantUpload2" ShowControl="false" CssClass="ConfigRow"
    runat="server" CheckType="Image" ShowText="false" ButtonImage="SelectImages.png"
    ButtonWidth="105" ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantModuleImage3Help" ConfigName="FeaturedMerchantImage3"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantModuleImage3" runat="server" meta:resourcekey="lcFeaturedMerchantModuleImage3"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxFeaturedMerchantImage3Text" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxFeaturedMerchantUpload3LinkButton" runat="server" CssClass="fl mgl5"
        OnClick="uxFeaturedMerchantUpload3LinkButton_Click">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxFeaturedMerchantUpload3" ShowControl="false" CssClass="ConfigRow"
    runat="server" CheckType="Image" ShowText="false" ButtonImage="SelectImages.png"
    ButtonWidth="105" ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantUrl1Help" ConfigName="FeaturedMerchantUrl1" runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantUrl1" runat="server" meta:resourcekey="lcFeaturedMerchantUrl1"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxFeaturedMerchantUrl1Text" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantUrl2Help" ConfigName="FeaturedMerchantUrl2" runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantUrl2" runat="server" meta:resourcekey="lcFeaturedMerchantUrl2"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxFeaturedMerchantUrl2Text" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedMerchantUrl3Help" ConfigName="FeaturedMerchantUrl3" runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedMerchantUrl3" runat="server" meta:resourcekey="lcFeaturedMerchantUrl3"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxFeaturedMerchantUrl3Text" runat="server" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFreeShippingModuleDisplayHelp" ConfigName="FreeShippingModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFreeShippingModuleDisplay" runat="server" meta:resourcekey="lcFreeShippingModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFreeShippingModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFreeShippingModuleImageHelp" ConfigName="FreeShippingImage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcFreeShippingModuleImage" runat="server" meta:resourcekey="lcFreeShippingModuleImage"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxFreeShippingImageText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxFreeShippingUploadLinkButton" runat="server" CssClass="fl mgl5"
        OnClick="uxFreeShippingUploadLinkButton_Click">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxFreeShippingUpload" runat="server" ShowControl="false" CssClass="ConfigRow"
    CheckType="Image" ShowText="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSecureModuleDisplayHelp" ConfigName="SecureModuleDisplay" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSecureModuleDisplay" runat="server" meta:resourcekey="lcSecureModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxSecureModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSecureModuleImageHelp" ConfigName="SecureImage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSecureModuleImage" runat="server" meta:resourcekey="lcSecureModuleImage"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxSecureImageText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxSecureUploadLinkButton" runat="server" CssClass="fl mgl5" OnClick="uxSecureUploadLinkButton_Click">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxSecureUpload" ShowControl="false" CssClass="ConfigRow" runat="server"
    CheckType="Image" ShowText="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" />
<div id="uxBundlePromotionDisplayTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxBundlePromotionDisplayHelp" ConfigName="EnableBundlePromo" runat="server" />
    <div class="Label">
        <asp:Label ID="lcBundlePromotionDisplay" runat="server" meta:resourcekey="lcBundlePromotionDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxBundlePromotionDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxBestsellersModuleDisplayTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxBestsellersModuleDisplayHelp" ConfigName="BestsellersModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcBestsellersModuleDisplay" runat="server" meta:resourcekey="lcBestsellersModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxBestsellersModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxFeaturedProductModuleDisplayHelp" ConfigName="FeaturedProductModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcFeaturedProductModuleDisplay" runat="server" meta:resourcekey="lcFeaturedProductModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFeaturedProductModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxNewsTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxNewsModuleDisplayHelp" ConfigName="NewsModuleDisplay" runat="server" />
    <div class="Label">
        <asp:Label ID="lcNewsModuleDisplay" runat="server" meta:resourcekey="lcNewsModuleDisplay"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxNewsModuleDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxWishListTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxWishListDisplayHelp" ConfigName="WishListEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcWishListDisplay" runat="server" meta:resourcekey="lcWishListDisplay"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxWishListDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxCompareListTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxCompareListDisplayHelp" ConfigName="CompareListEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcCompareListDisplay" runat="server" meta:resourcekey="lcCompareListDisplay"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxCompareListDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxTellAFriendTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTellAFriendHelp" ConfigName="TellAFriendEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcTellAFriendDisplay" runat="server" meta:resourcekey="lcTellAFriendDisplay"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxTellAFriendDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxGiftRegistryTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxGiftRegistryDisplayHelp" ConfigName="GiftRegistryModuleDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGiftRegistryDisplay" runat="server" meta:resourcekey="lcGiftRegistryDisplay"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxGiftRegistryDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxRecentlyViewedProductTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxRecentlyViewedProductHelp" ConfigName="RecentlyViewedProductDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcRecentlyViewedProduct" runat="server" meta:resourcekey="lcRecentlyViewedProduct"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxRecentlyViewedProductDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow mgt20">
    <uc1:HelpIcon ID="uxFacetedSearchEnabledHelp" ConfigName="FacetedSearchEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="uxFacetedSearchEnabledLabel" runat="server" meta:resourcekey="uxFacetedSearchEnabledLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxFacetedSearchEnabledDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxFacetedSearchEnabledDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxFacetedSearchDetailsPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxPriceNavigationStepHelp" ConfigName="PriceNavigationStep" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxPriceNavigationStepLabel" runat="server" meta:resourcekey="uxPriceNavigationStepLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxPriceNavigationStepTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxPriceNavigationStepRequired" runat="server" ControlToValidate="uxPriceNavigationStepTextbox"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Price Search Step is required.
            <div class="CommonValidateDiv">
            </div>                
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxPriceNavigationStepCompare" runat="server" ControlToValidate="uxPriceNavigationStepTextbox"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxMaximunIntervalHelp" ConfigName="MaximunInterval" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxMaximunIntervalLabel" runat="server" meta:resourcekey="uxMaximunIntervalLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxMaximunIntervalTextbox" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxMaximunIntervalRequired" runat="server" ControlToValidate="uxMaximunIntervalTextbox"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Max Interval is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxMaximunIntervalCompare" runat="server" ControlToValidate="uxMaximunIntervalTextbox"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
</asp:Panel>
<div class="ConfigRow mgt20">
    <uc1:HelpIcon ID="uxEnableNewArrivalProductHelp" ConfigName="EnableNewArrivalProduct"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableNewArrivalProductLabel" runat="server" meta:resourcekey="uxEnableNewArrivalProductLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableNewArrivalProductDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxEnableNewArrivalProductDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxEnableNewArrivalProductPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxProductNewArrivalNumberHelp" ConfigName="ProductNewArrivalNumber"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxProductNewArrivalNumberLabel" runat="server" meta:resourcekey="uxProductNewArrivalNumberLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxProductNewArrivalNumberText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxProductNewArrivalNumberRequired" runat="server"
            ControlToValidate="uxProductNewArrivalNumberText" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> New Arrival Per Page is required.
            <div class="CommonValidateDiv">
            </div>         
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxProductNewArrivalNumberCompare" runat="server" ControlToValidate="uxProductNewArrivalNumberText"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxMaximumDisplayProductNewArrivalHelp" ConfigName="MaximumDisplayProductNewArrival"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxMaximumDisplayProductNewArrivalLabel" runat="server" meta:resourcekey="uxMaximumDisplayProductNewArrivalLabel"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxMaximumDisplayProductNewArrivalText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxMaximumDisplayProductNewArrivalRequired" runat="server"
            ControlToValidate="uxMaximumDisplayProductNewArrivalText" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Max New Arrival Product is required.
            <div class="CommonValidateDiv">
            </div>         
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxMaximumDisplayProductNewArrivalCompareToItemPerPage"
            runat="server" ControlToCompare="uxProductNewArrivalNumberText" ControlToValidate="uxMaximumDisplayProductNewArrivalText"
            Operator="GreaterThan" Type="Integer" Display="Dynamic" ValueToCompare="0" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Maximum Number must be greater than Number of New Arrival Product Per Page.
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
</asp:Panel>
