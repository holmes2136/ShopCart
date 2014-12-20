<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreDisplayConfig.ascx.cs"
    Inherits="Admin_Components_StoreConfig_StoreDisplayConfig" %>
<%@ Register Src="../SiteConfig/LayoutThemeSelect.ascx" TagName="DefaultThemeSelect"
    TagPrefix="uc24" %>
<%@ Register Src="../SiteConfig/LayoutMobileThemeSelect.ascx" TagName="DefaultMobileThemeSelect"
    TagPrefix="uc30" %>
<%@ Register Src="../SiteConfig/LayoutCategoryListSelect.ascx" TagName="DefaultCategoryListSelect"
    TagPrefix="uc25" %>
<%@ Register Src="../SiteConfig/LayoutProductListSelect.ascx" TagName="DefaultProductListSelect"
    TagPrefix="uc26" %>
<%@ Register Src="../SiteConfig/LayoutProductDetailsSelect.ascx" TagName="DefaultProductDetailsSelect"
    TagPrefix="uc27" %>
<%@ Register Src="../SiteConfig/LayoutDepartmentListSelect.ascx" TagName="DefaultDepartmentListSelect"
    TagPrefix="uc28" %>
<%@ Register Src="../CountryList.ascx" TagName="DefaultCountry" TagPrefix="uc29" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="../SiteSetup/WebsiteName.ascx" TagName="SiteName" TagPrefix="uc8" %>
<%@ Register Src="../SiteSetup/DefaultWebsiteLanguage.ascx" TagName="DefaultWebsiteLanguage"
    TagPrefix="uc9" %>
<%@ Register Src="../SiteSetup/LogoImage.ascx" TagName="LogoImage" TagPrefix="uc11" %>
<%@ Register Src="../SiteConfig/WidgetDetails.ascx" TagName="WidgetConfig" TagPrefix="uc30" %>
<%@ Register Src="../SiteConfig/GoogleAnalytics.ascx" TagName="GoogleAnalytics"
    TagPrefix="uc14" %>
<div class="CommonConfigTitle  mgt0">
<asp:Label ID="uxLogoUploadLabel" runat="server" meta:resourcekey="lcLogoUpload" /></div>
<uc11:LogoImage ID="uxLogoImage" runat="server" />
<div class="CommonConfigTitle">
<asp:Label ID="lcStoreHeader" runat="server" meta:resourcekey="lcStoreHeader" /></div>
<uc8:SiteName ID="uxSiteName" runat="server" />
<uc9:DefaultWebsiteLanguage ID="uxDefaultwebsiteLanguage" runat="server" />
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxStoreDefaultCountryHelp" ConfigName="StoreDefaultCountry" runat="server" />
    <div class="Label">
        <asp:Label ID="uxStoreDefaultCountryLabel" runat="server" meta:resourcekey="uxStoreDefaultCountryLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <uc29:DefaultCountry ID="uxStoreDefaultCountryDropDown" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSearchModeHelp" ConfigName="SearchMode" runat="server" />
    <div class="Label">
        <asp:Label ID="uxSearchModeLabel" runat="server" meta:resourcekey="uxSearchModeLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxSearchModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Text="All words" Value="All words" />
        <asp:ListItem Text="Any words" Value="Any words" />
        <asp:ListItem Text="Exact phase" Value="Exact phase" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxCategoryConfigurationPanel" runat="server">
    <div class="CommonConfigTitle">
        <asp:Label ID="uxStoreAccessHeaderTitle" runat="server" meta:resourcekey="lcStoreCofigurationLabel" /></div>
    <div id="uxRestrictAccessToShopTR" runat="server" class="ConfigRow">
        <uc1:HelpIcon ID="uxRestrictAccessToShopHelp" ConfigName="RestrictAccessToShop" runat="server" />
        <div class="Label">
            <asp:Label ID="lcRestrictAccessToShop" runat="server" meta:resourcekey="lcRestrictAccessToShop"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxRestrictAccessToShopDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div id="uxPriceRequireLoginTR" runat="server" class="ConfigRow">
        <uc1:HelpIcon ID="uxPriceRequireLoginHelp" ConfigName="PriceRequireLogin" runat="server" />
        <div class="Label">
            <asp:Label ID="lcPriceRequireLogin" runat="server" meta:resourcekey="lcPriceRequireLogin"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxPriceRequireLoginDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div id="uxCheckoutModeTR" runat="server" class="ConfigRow">
        <uc1:HelpIcon ID="uxCheckoutModeHelp" ConfigName="CheckoutMode" runat="server" />
        <div class="Label">
            <asp:Label ID="uxCheckoutModeLabel" runat="server" meta:resourcekey="uxCheckoutModeLabel"
                CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxCheckoutModeDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="Normal" Text="Normal" />
            <asp:ListItem Value="One Page" Text="One Page" />
        </asp:DropDownList>
    </div>
    <div class="CommonConfigTitle">
        <asp:Label ID="uxCategoryConfigurationLabel" runat="server" meta:resourcekey="uxCategoryConfigurationLabel" /></div>
    <asp:Panel ID="uxRootCategorySettingPanel" runat="server" CssClass="ConfigRow">
        <uc1:HelpIcon ID="uxRootCategoryHelp" ConfigName="RootCategory" runat="server" />
        <div class="Label">
            <asp:Label ID="uxRootCategoryLabel" runat="server" meta:resourcekey="uxRootCategoryLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxRootCategoryDrop" runat="server" CssClass="fl DropDown">
        </asp:DropDownList>
    </asp:Panel>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxCategoryDynamicDropDownDisplayHelp" ConfigName="CategoryDynamicDropDownDisplay"
            runat="server" />
        <div class="Label">
            <asp:Label ID="uxCategoryDynamicDropDownDisplay" runat="server" meta:resourcekey="uxCategoryDynamicDropDownDisplay"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxCategoryDynamicDropDownDisplayDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxCategoryDynamicDropDownLevelHelp" ConfigName="CategoryDynamicDropDownLevel"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxCategoryDynamicDropDownLevelLabel" runat="server" meta:resourcekey="uxCategoryDynamicDropDownLevel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:TextBox ID="uxCategoryDynamicDropDownLevelText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxCategoryDynamicDropDownLevelRequired" runat="server"
            ControlToValidate="uxCategoryDynamicDropDownLevelText" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Category DropDown Menu Level is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxCategoryDynamicDropDownLevelTextCompare" runat="server"
            ControlToValidate="uxCategoryDynamicDropDownLevelText" Operator="GreaterThan"
            ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxCategoryMenuTypeHelp" ConfigName="CategoryMenuType" runat="server" />
        <div class="Label">
            <asp:Label ID="lcCategoryMenuType" runat="server" meta:resourcekey="lcCategoryMenuType"
                CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxCategoryMenuTypeDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="default" Text="Default" />
            <asp:ListItem Value="cascade" Text="Cascade" />
            <asp:ListItem Value="treeview" Text="Tree View" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxCategoryMenuLevelHelp" ConfigName="CategoryMenuLevel" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="lcCategoryMenuLevel" runat="server" meta:resourcekey="lcCategoryMenuLevel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:TextBox ID="uxCategoryMenuLevelText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxCategoryMenuLevelTextRequired" runat="server" ControlToValidate="uxCategoryMenuLevelText"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Category Menu Level is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxCategoryMenuLevelTextCompare" runat="server" ControlToValidate="uxCategoryMenuLevelText"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxCategoryShowProductListHelp" ConfigName="CategoryShowProductList"
            runat="server" />
        <div class="Label">
            <asp:Label ID="lcCategoryShowProductList" runat="server" meta:resourcekey="lcCategoryShowProductList"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxCategoryShowProductListDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
</asp:Panel>
<asp:Panel ID="uxDepartmentConfigurationPanel" runat="server">
<div class="CommonConfigTitle">
    <asp:Label ID="uxDepartmentConfigurationLabel" runat="server" meta:resourcekey="uxDepartmentConfigurationLabel"/></div>
    <asp:Panel ID="uxRootDepartmentSettiongPanel" runat="server" CssClass="ConfigRow">
        <uc1:HelpIcon ID="HelpIcon1" ConfigName="RootDepartment" runat="server" />
        <div class="Label">
            <asp:Label ID="uxRootDepartmentLabel" runat="server" meta:resourcekey="uxRootDepartmentLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxRootDepartmentDrop" runat="server" CssClass="fl DropDown">
        </asp:DropDownList>
    </asp:Panel>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDepartmentDynamicDropDownDisplayHelp" ConfigName="DepartmentDynamicDropDownDisplay"
            runat="server" />
        <div class="Label">
            <asp:Label ID="uxDepartmentDynamicDropDownDisplay" runat="server" meta:resourcekey="uxDepartmentDynamicDropDownDisplay"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxDepartmentDynamicDropDownDisplayDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDepartmentDynamicDropDownLevelHelp" ConfigName="DepartmentDynamicDropDownLevel"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxDepartmentDynamicDropDownLevelLabel" runat="server" meta:resourcekey="uxDepartmentDynamicDropDownLevel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:TextBox ID="uxDepartmentDynamicDropDownLevelText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uxDepartmentDynamicDropDownLevelText"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Department DropDown Menu Level is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxDepartmentDynamicDropDownLevelTextCompare" runat="server"
            ControlToValidate="uxDepartmentDynamicDropDownLevelText" Operator="GreaterThan"
            ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDepartmentMenuTypeHelp" ConfigName="DepartmentMenuType" runat="server" />
        <div class="Label">
            <asp:Label ID="uxDepartmentMenuType" runat="server" meta:resourcekey="uxDepartmentMenuType"
                CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxDepartmentMenuTypeDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="default" Text="Default" />
            <asp:ListItem Value="cascade" Text="Cascade" />
            <asp:ListItem Value="treeview" Text="Tree View" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDepartmentMenuLevelHelp" ConfigName="DepartmentMenuLevel" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="lcDepartmentMenuLevel" runat="server" meta:resourcekey="uxDepartmentMenuLevel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:TextBox ID="uxDepartmentMenuLevelText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxDepartmentMenuLevelTextRequired" runat="server"
            ControlToValidate="uxDepartmentMenuLevelText" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Department Menu Level is require.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxDepartmentMenuLevelTextCompare" runat="server" ControlToValidate="uxDepartmentMenuLevelText"
            Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxDepartmentShowProductListHelp" ConfigName="DepartmentShowProductList"
            runat="server" />
        <div class="Label">
            <asp:Label ID="lcDepartmentShowProductList" runat="server" meta:resourcekey="lcDepartmentShowProductList"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxDepartmentShowProductListDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
</asp:Panel>
<asp:Panel ID="uxManufacturerConfigurationPanel" runat="server">
<div class="CommonConfigTitle">
    <asp:Label ID="uxManufacturerConfigurationLabel" runat="server" meta:resourcekey="uxManufacturerConfigurationLabel"/></div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxManufacturerDynamicDropDownDisplayHelp" ConfigName="ManufacturerDynamicDropDownDisplay"
            runat="server" />
        <div class="Label">
            <asp:Label ID="uxManufacturerDynamicDropDownDisplayLabel" runat="server" meta:resourcekey="uxManufacturerDynamicDropDownDisplayLabel"
                CssClass="fl">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxManufacturerDynamicDropDownDisplayDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxManufacturerMenuTypeHelp" ConfigName="ManufacturerMenuType" runat="server" />
        <div class="Label">
            <asp:Label ID="uxManufacturerMenuTypeLabel" runat="server" meta:resourcekey="uxManufacturerMenuTypeLabel"
                CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxManufacturerMenuTypeDrop" runat="server" CssClass="fl DropDown">
            <asp:ListItem Value="default" Text="Default" />
            <asp:ListItem Value="dropdown" Text="Drop Down" />
        </asp:DropDownList>
    </div>
</asp:Panel>
<div class="CommonConfigTitle">
<asp:Label ID="uxAdvancedSearcConfigurationLabel" runat="server" meta:resourcekey="uxAdvancedSearchConfigurationLabel"/></div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAdvancedSearchModeHelp" runat="server" ConfigName="AdvancedSearchMode" />
    <div class="Label">
        <asp:Label ID="uxAdvancedSearchModeLabel" runat="server" meta:resourcekey="uxAdvancedSearchModeLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxAdvancedSearchModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="Classic" Text="Classic" />
        <asp:ListItem Value="Enhance" Text="Enhance" />
    </asp:DropDownList>
</div>
<div class="CommonConfigTitle">
<asp:Label ID="uxWidgetAddThisTitle" runat="server" meta:resourcekey="uxWidgetAddThisTitleLabel"/></div>
<uc30:WidgetConfig ID="uxWidgetAddThisConfig" WidgetStyle="AddThis" ParameterName="AddThis Username"
    runat="server" ValidationGroup="SiteConfigValid" />
<div class="CommonConfigTitle">
<asp:Label ID="uxWidgetLivePersonTitle" runat="server" meta:resourcekey="uxWidgetAddLivePersonLabel"/></div>
<uc30:WidgetConfig ID="uxWidgetLivePersonConfig" WidgetStyle="LivePerson" ParameterName="LivePerson Account"
    runat="server" ValidationGroup="SiteConfigValid" />
<uc14:GoogleAnalytics ID="uxGoogleAnalyticsConfig" runat="server" />
<div class="CommonConfigTitle">
<asp:Label ID="uxThemeLayoutSettingLabel" runat="server" meta:resourcekey="lcThemeLayoutSettingLabel" /></div>
<uc24:DefaultThemeSelect ID="uxThemeSelect" runat="server" />
<uc30:DefaultMobileThemeSelect ID="uxMobileThemeSelect" runat="server" />
<uc25:DefaultCategoryListSelect ID="uxCategorySelect" runat="server" />
<uc28:DefaultDepartmentListSelect ID="uxDepartmentSelect" runat="server" />
<uc26:DefaultProductListSelect ID="uxProductListSelect" runat="server" />
<uc27:DefaultProductDetailsSelect ID="uxProductDetailsSelect" runat="server" />
<div id="uxProductColumnTR" runat="server" class="ConfigRow mgt20">
    <uc1:HelpIcon ID="uxNumberOfProductColumnHelp" ConfigName="NumberOfProductColumn"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfProductColumnLabel" CssClass="fl" runat="server" meta:resourcekey="uxNumberOfProductColumnLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxNumberOfProductColumnText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireProductColumnValidator" runat="server" ControlToValidate="uxNumberOfProductColumnText"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Column Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxProductColumnValidator" runat="server" ControlToValidate="uxNumberOfProductColumnText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxCategoryColumnTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfCategoryColumnHelp" ConfigName="NumberOfCategoryColumn"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfCategoryColumnLabel" CssClass="fl" runat="server" meta:resourcekey="uxNumberOfCategoryColumnLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxNumberOfCategoryColumnText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireCategoryColumnValidator" runat="server"
        ControlToValidate="uxNumberOfCategoryColumnText" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Category Column Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxCategoryColumnValidator" runat="server" ControlToValidate="uxNumberOfCategoryColumnText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxDepartmentColumnTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfDepartmentColumnHelp" ConfigName="NumberOfDepartmentColumn"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfDepartmentColumnLabel" CssClass="fl" runat="server" meta:resourcekey="uxNumberOfDepartmentColumnLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxNumberOfDepartmentColumnText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uxNumberOfDepartmentColumnText"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Department Column Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxDepartmentColumnValidator" runat="server" ControlToValidate="uxNumberOfDepartmentColumnText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxBundlePromotionColumnTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxBundlePromotionColumnHelp" ConfigName="BundlePromotionColumn"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxBundlePromotionColumnLabel" CssClass="fl" runat="server" meta:resourcekey="uxBundlePromotionColumnLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxBundlePromotionColumnText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxBundlePromotionColumnValidator" runat="server"
        ControlToValidate="uxBundlePromotionColumnText" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Bundle Promotion Column Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxBundlePromotionColumnRange" runat="server" ControlToValidate="uxBundlePromotionColumnText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxNumberOfProductTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfProductHelp" ConfigName="ProductItemsPerPage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcNumberOfProduct" runat="server" CssClass="fl" meta:resourcekey="lcNumberOfProduct" />
    </div>
    <asp:TextBox ID="uxNumberOfProduct" runat="server" CssClass="fl TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireProductValidator" runat="server" ControlToValidate="uxNumberOfProduct"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Product Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="uxRegExProductValidate" runat="server" ValidationExpression="\d{1,3}(,\d{1,3})*"
        ControlToValidate="uxNumberOfProduct" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div></asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxNumberOfCategoryTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfCategoryHelp" ConfigName="CategoryItemsPerPage" runat="server" />
    <div class="Label">
        <asp:Label ID="lcNumberOfCategory" runat="server" meta:resourcekey="lcNumberOfCategory"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberOfCategory" runat="server" CssClass="fl TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireCategoryValidator" runat="server" ControlToValidate="uxNumberOfCategory"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Category Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="uxRegExCategoryValidate" runat="server" ControlToValidate="uxNumberOfCategory"
        Display="Dynamic" ValidationExpression="\d{1,3}(,\d{1,3})*" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div></asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxNumberOfDepartmentTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfDepartmentHelp" ConfigName="DepartmentItemsPerPage" runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfDepartmentLabel" runat="server" meta:resourcekey="uxNumberOfDepartmentLabel"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberOfDepartment" runat="server" CssClass="fl TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireDepartmentValidator" runat="server" ControlToValidate="uxNumberOfDepartment"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Department Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="uxRegExDepartmentValidate" runat="server" ControlToValidate="uxNumberOfDepartment"
        Display="Dynamic" ValidationExpression="\d{1,3}(,\d{1,3})*" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div></asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxBundlePromotionDisplayTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxBundlePromotionDisplayHelp" ConfigName="BundlePromotionDisplay"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxBundlePromotionDisplayLabel" runat="server" meta:resourcekey="uxBundlePromotionDisplayLabel"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxBundlePromotionDisplayText" runat="server" CssClass="fl TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxBundlePromotionDisplayValidator" runat="server"
        ControlToValidate="uxBundlePromotionDisplayText" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Bundle Promotion Item Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="uxBundlePromotionDisplayRegular" runat="server"
        ControlToValidate="uxBundlePromotionDisplayText" Display="Dynamic" ValidationExpression="\d{1,3}(,\d{1,3})*"
        ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div></asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<div id="uxTopCategoryMenuColumnTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTopCategoryMenuColumnHelp" ConfigName="NumberOfSubCategoryMenuColumn"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxTopCategoryMenuColumnLabel" CssClass="fl" runat="server" meta:resourcekey="uxTopCategoryMenuColumnLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxTopCategoryMenuColumnText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxTopCategoryMenuColumnRequired" runat="server" ControlToValidate="uxTopCategoryMenuColumnText"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Top Category Menu Item Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxTopCategoryMenuColumnRange" runat="server" ControlToValidate="uxTopCategoryMenuColumnText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<div id="uxTopCategoryMenuItemTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxTopCategoryMenuItemHelp" ConfigName="NumberOfSubCategoryMenuItem"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxTopCategoryMenuItemLabel" CssClass="fl" runat="server" meta:resourcekey="uxTopCategoryMenuItemLabel"></asp:Label>
    </div>
    <asp:TextBox ID="uxTopCategoryMenuItemText" runat="server" CssClass="fl TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxTopCategoryMenuItemRequired" runat="server" ControlToValidate="uxTopCategoryMenuItemText"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Top Sub Category Menu Item Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxTopCategoryMenuItemRange" runat="server" ControlToValidate="uxTopCategoryMenuItemText"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxNumberOfManufacturerTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfManufacturerHelp" ConfigName="ManufacturerItemsPerPage"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxNumberOfManufacturerLabel" runat="server" meta:resourcekey="uxNumberOfManufacturerLabel"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberOfManufacturer" runat="server" CssClass="fl TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireManufacturerValidator" runat="server" ControlToValidate="uxNumberOfManufacturer"
        Display="Dynamic" ErrorMessage="Required Manufacturer Number" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Manufacturer Column Number is required
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="uxRegExManufacValidate" runat="server" ControlToValidate="uxNumberOfManufacturer"
        Display="Dynamic" ValidationExpression="\d{1,3}(,\d{1,3})*" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div></asp:RegularExpressionValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<div class="CommonConfigTitle">
<asp:Label ID="uxOtherHeaderLabel" runat="server" meta:resourcekey="uxOtherHeaderLabel"/></div>
<asp:Panel ID="uxBundlePromotionShowTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxBundlePromotionShowHelp" ConfigName="BundlePromotionShow" runat="server" />
    <div class="Label">
        <asp:Label ID="lcBundlePromotionShow" runat="server" meta:resourcekey="lcBundlePromotionShow"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxBundlePromotionShowText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxBundlePromotionShowValidator" runat="server" ControlToValidate="uxBundlePromotionShowText"
        ErrorMessage="Required Bundle Promotion Number" ValidationGroup="SiteConfigValid"
        Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Bundle Promotion Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxBundlePromotionShowRange" runat="server" Type="Integer"
        ControlToValidate="uxBundlePromotionShowText" MinimumValue="1" MaximumValue="999"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxRandomNumberHelp" ConfigName="RandomProductShow" runat="server" />
    <div class="Label">
        <asp:Label ID="lcRandomNumber" runat="server" meta:resourcekey="lcRandomNumber" CssClass="fl" />
    </div>
    <asp:TextBox ID="uxRandomNumberText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredRandomValidator" runat="server" ControlToValidate="uxRandomNumberText"
        ValidationGroup="SiteConfigValid" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Random Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxRangeRandomValidator" runat="server" Type="Integer" ControlToValidate="uxRandomNumberText"
        MinimumValue="1" MaximumValue="999" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
</div>
<asp:Panel ID="uxBestSellingTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfBestSellingHelp" ConfigName="ProductBestSellingShow"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcNumberOfBestSelling" runat="server" meta:resourcekey="lcNumberOfBestSelling"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberBestSelling" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireBestSellingValidator" runat="server" ControlToValidate="uxNumberBestSelling"
        Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Best Selling Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxBestSellingValidator" runat="server" ControlToValidate="uxNumberBestSelling"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<asp:Panel ID="uxRecentlyViewedTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfRecentlyViewedHelp" ConfigName="RecentlyViewedProductShow"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcNumberOfRecentlyViewed" runat="server" meta:resourcekey="lcNumberOfRecentlyViewed"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberRecentlyViewed" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireRecentlyViewedValidator" runat="server"
        ControlToValidate="uxNumberRecentlyViewed" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Recently Viewed Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxRecentlyViewedValidator" runat="server" ControlToValidate="uxNumberRecentlyViewed"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<asp:Panel ID="uxCompareProductShowTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxNumberOfCompareProductHelp" ConfigName="CompareProductShow" runat="server" />
    <div class="Label">
        <asp:Label ID="lcNumberOfCompareProduct" runat="server" meta:resourcekey="lcNumberOfCompareProduct"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxNumberCompareProduct" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequireCompareProductValidator" runat="server"
        ControlToValidate="uxNumberCompareProduct" Display="Dynamic" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Compare Number is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxCompareProductValidator" runat="server" ControlToValidate="uxNumberCompareProduct"
        Display="Dynamic" Type="Integer" MaximumValue="999" MinimumValue="1" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be an Integer and greater that zero(0).
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSSLHelp" ConfigName="EnableSSL" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSSL" runat="server" meta:resourcekey="lcSSL" CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxSSLDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxAddToCartHelp" ConfigName="EnableAddToCartNotification" runat="server" />
    <div class="Label">
        <asp:Label ID="uxAddToCartLabel" runat="server" meta:resourcekey="uxAddToCartLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxAddToCartDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxQuickViewHelp" ConfigName="EnableQuickView" runat="server" />
    <div class="Label">
        <asp:Label ID="uxQuickViewLabel" runat="server" meta:resourcekey="uxQuickViewLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxQuickViewDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSaleTaxExemptHelp" ConfigName="SaleTaxExempt" runat="server" />
    <div class="Label">
        <asp:Label ID="uxSaleTaxExemptLabel" runat="server" meta:resourcekey="uxSaleTaxExemptLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxSaleTaxExemptDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxMobileViewHelp" ConfigName="MobileView" runat="server" />
    <div class="Label">
        <asp:Label ID="uxMobileViewLabel" runat="server" meta:resourcekey="uxMobileViewLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxMobileViewDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxReviewPerCulture" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxReviewPerCultureHelp" ConfigName="EnableReviewPerCulture" runat="server" />
    <div class="Label">
        <asp:Label ID="uxReviewPerCultureLabel" runat="server" meta:resourcekey="uxReviewPerCultureLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxReviewPerCultureDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxRmaTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxRmaHelp" ConfigName="EnableRMA" runat="server" />
    <div class="Label">
        <asp:Label ID="uxRmaLabel" runat="server" meta:resourcekey="uxRmaLabel" CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxRmaDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
