<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplayConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_DisplayConfig" %>
<%@ Register Src="paymentappurl.ascx" TagName="PaymentAppText" TagPrefix="uc28" %>
<%@ Register Src="PolicyAgreement.ascx" TagName="DefaultPolicyAgreementSelect" TagPrefix="uc29" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="Canonicalization.ascx" TagName="Canonicalization" TagPrefix="uc31" %>
<div id="uxLanguageMenuTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxLanguageMenuDisplayHelp" configname="LanguageMenuDisplayMode"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcLanguageMenuDisplay" runat="server" meta:resourcekey="lcLanguageMenuDisplay"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxLanguageMenuDisplayDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="None">(None)</asp:ListItem>
        <asp:ListItem Value="Horizontal" Selected="True">Horizontal</asp:ListItem>
        <asp:ListItem Value="DropDown">Drop-Down</asp:ListItem>
    </asp:DropDownList>
</div>
<asp:Panel ID="uxUseInventoryTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxUseInventoryControlHelp" configname="UseStockControl" runat="server" />
    <div class="Label">
        <asp:Label ID="lcUseInventoryControl" runat="server" meta:resourcekey="lcUseInventoryControl"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxUseInventoryControlDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxOutofStockTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxOutOfStockValueHelp" configname="OutOfStockValue" runat="server" />
    <div class="Label">
        <asp:Label ID="lcOutOfStockValue" runat="server" meta:resourcekey="lcOutOfStockValue"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxOutOfStockValueText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxOutOfStockRequired" runat="server" ControlToValidate="uxOutOfStockValueText"
        ValidationGroup="SiteConfigValid" Display="Dynamic" CssClass="CommonValidatorText">
<img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Out-Of-Stock Quantity is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxOutOfStockCompare" runat="server" Display="Dynamic" ControlToValidate="uxOutOfStockValueText"
        Operator="GreaterThanEqual" ValueToCompare="0" Type="Integer" ValidationGroup="SiteConfigValid"
        CssClass="CommonValidatorText">
<img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Out-Of-Stock Quantity must be an Integer.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</asp:Panel>
<asp:Panel ID="uxRemainingQuantityTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxDisplayRemainingQuantityHelp" configname="ShowQuantity" runat="server" />
    <div class="Label">
        <asp:Label ID="lcDisplayRemainingQuantityLabel" runat="server" meta:resourcekey="lcDisplayRemainingQuantityLabel"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxDisplayRemainingQuantityDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxModeCatalogTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxModeCatalogHelp" configname="IsCatalogMode" runat="server" />
    <div class="Label">
        <asp:Label ID="uxModeCatalogLabel" runat="server" meta:resourcekey="lcModeCatalog"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxModeCatalogDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True">Catalog</asp:ListItem>
        <asp:ListItem Value="False">Shopping Cart</asp:ListItem>
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxSkuShowTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxShowSkuModeHelp" configname="ShowSkuMode" runat="server" />
    <div class="Label">
        <asp:Label ID="uxShowSkuModeLabel" runat="server" meta:resourcekey="lcShowSkuModeLabel"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxShowSkuModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxRetailPriceTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxRetailPriceModeHelp" configname="RetailPriceMode" runat="server" />
    <div class="Label">
        <asp:Label ID="uxRetailPriceModeLabel" runat="server" meta:resourcekey="lcRetailMode"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxRetailPriceModeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="Tr1" runat="server" Visible="false" CssClass="ConfigRow">
    <uc1:helpicon id="uxLogoInvoiceImageHelp" configname="LogoInvoiceImage" runat="server" />
    <div class="Label">
        <asp:Label ID="uxLogoInvoiceImageLabel" runat="server" meta:resourcekey="lcLogoInvoiceImage"
            CssClass="fl"></asp:Label>
    </div>
    <asp:TextBox ID="uxLogoInvoiceImageText" runat="server" CssClass="TextBox" />
</asp:Panel>
<asp:Panel ID="uxShippingAddressTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxShippingAddressHelp" configname="ShippingAddressMode" runat="server" />
    <div class="Label">
        <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcShippingAddressMode"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxShippingAddDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxDisplayOptionTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxDisplayOptionHelp" configname="IsDisplayOptionInProductList"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcDisplayOption" runat="server" meta:resourcekey="lcDisplayOption"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxDisplayOptionDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxDiscountAllCustomerTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxDiscountAllCustomerHelp" configname="IsDiscountAllCustomer" runat="server" />
    <div class="Label">
        <asp:Label ID="lcIsDiscountAllCustomer" runat="server" meta:resourcekey="lcIsDiscountAllCustomers"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxIsDiscountAllCustomerDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxAdminSSLAlertTR" runat="server" CssClass="ConfigRow">
    <asp:Label ID="lcAdminSSLAlert" runat="server" meta:resourcekey="lcAdminSSLAlert"
        CssClass="Label"></asp:Label>
</asp:Panel>
<div class="ConfigRow">
    <uc1:helpicon id="uxAdminSSLHelp" configname="EnableAdminSSL" runat="server" />
    <div class="Label">
        <asp:Label ID="lcAdminSSL" runat="server" meta:resourcekey="lcAdminSSL" CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxAdminSSLDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxSiteMapTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxSiteMapEnabledHelp" configname="SiteMapEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcSiteMapEnabled" runat="server" meta:resourcekey="lcSiteMapEnabled"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxSiteMapEnabledDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxSiteMapEnabledDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxSiteMapTypeTR" runat="server" CssClass="ConfigRow">
    <uc1:helpicon id="uxSiteMapTypeHelp" configname="SiteMapDisplayType" runat="server" />
    <asp:Label ID="uxSiteMapTypeLabel" runat="server" Text="SiteMap Display Type" CssClass="BulletLabel">
    </asp:Label>
    <asp:DropDownList ID="uxSiteMapTypeDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="showall" Text="Show All" />
        <asp:ListItem Value="showcategoriesonly" Text="Show Categories Only" />
    </asp:DropDownList>
</asp:Panel>
<div id="uxWeightUnitTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxWeightUnitHelp" configname="WeightUnit" runat="server" />
    <div class="Label">
        <asp:Label ID="lcWeightUnit" runat="server" meta:resourcekey="lcWeightUnit" CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxWeightUnitDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="LB" Text="LB" />
        <asp:ListItem Value="KG" Text="KG" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxIgnoreProductFixedShippingTR" runat="server" Visible="false" CssClass="ConfigRow">
    <uc1:helpicon id="uxIgnoreProductFixedShippingHelp" configname="RTShippingIgnoreFixedShippingCost"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcIgnoreProductFixedShipping" runat="server" meta:resourcekey="lcIgnoreProductFixedShipping"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxIgnoreProductFixedShippingDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<div id="uxQuantityDiscountTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxQuantityDiscountHelp" configname="QuantityDiscountSeparateOption"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcQuantityDiscount" runat="server" meta:resourcekey="lcQuantityDiscount"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxQuantityDiscountDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxHandlingFeeTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxHandlingFeeEnabledHelp" configname="HandlingFeeEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcHandlingFeeEnabled" runat="server" meta:resourcekey="lcHandlingFeeEnabled"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxHandlingFeeEnabledDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxCouponEnabledTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxCouponEnabledHelp" configname="CouponEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="lcCouponEnabled" runat="server" meta:resourcekey="lcCouponEnabled"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxCouponEnabledDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<uc29:defaultpolicyagreementselect id="uxPolicyAgreementControl" runat="server" />
<uc31:canonicalization id="uxCanonicalizationControl" runat="server" />
<div id="uxCustomerAutoApproveTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxCustomerAutoApproveHelp" configname="CustomerAutoApprove" runat="server" />
    <div class="Label">
        <asp:Label ID="lcCustomerAutoApprove" runat="server" meta:resourcekey="lcCustomerAutoApprove"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxCustomerAutoApproveDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="CommonConfigTitle">
    <asp:Label ID="uxFaceBookTitleLabel" runat="server" meta:resourcekey="uxFaceBookTitleLabel" /></div>
<div id="uxFaceBookLikeButtonTR" runat="server" class="ConfigRow">
    <uc1:helpicon id="uxFaceBookLikeButtonHelp" configname="FBLikeButton" runat="server" />
    <div class="Label">
        <asp:Label ID="uxFaceBookLikeButtonLabel" runat="server" meta:resourcekey="uxFaceBookLikeButtonLabel"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxFaceBookLikeButtonDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="CommonConfigTitle">
    <asp:Label ID="uxPaymentConfiguration" runat="server" meta:resourcekey="lcPaymentConfigurationLabel" /></div>
<div class="ConfigRow">
    <uc1:helpicon id="uxVevoPayPADSSModeHelp" configname="VevoPayPADSSMode" runat="server" />
    <div class="Label">
        <asp:Label ID="lcVevoPayPADSSMode" CssClass="fl" runat="server" meta:resourcekey="lcVevoPayPADSSMode">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxVevoPayPADSSModeDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxVevoPayPADSSModeDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxVevoPayConfigPanel" runat="server" Visible="false">
    <div class="ConfigRow">
        <uc1:helpicon id="uxPaymentSSLEnabledHelp" configname="PaymentSSLEnabled" runat="server" />
        <div class="Label">
            <asp:Label ID="lcPaymentSSLEnabled" CssClass="fl" runat="server" meta:resourcekey="lcPaymentSSLEnabled">
            </asp:Label>
        </div>
        <asp:DropDownList ID="uxPaymentSSLEnabledDrop" runat="Server" CssClass="fl DropDown">
            <asp:ListItem Value="True" Text="Yes" />
            <asp:ListItem Value="False" Text="No" />
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </div>
    <uc28:paymentapptext id="uxPaymentAppText" runat="server" />
</asp:Panel>
