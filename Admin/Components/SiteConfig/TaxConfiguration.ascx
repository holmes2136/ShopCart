<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxConfiguration.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_TaxConfiguration" %>
<%@ Register Src="../Common/StateList.ascx" TagName="StateList" TagPrefix="uc3" %>
<%@ Register Src="../Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxTaxableWholesalerHelp" ConfigName="IsTaxableWholesaler" runat="server" />
    <asp:Label ID="lcTaxableWholesaler" runat="server" meta:resourcekey="lcWholesaleTaxLabel"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxTaxableWholesalerDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxShippingCostTaxHelp" ConfigName="IsTaxableShippingCost" runat="server" />
    <asp:Label ID="lcShippingCostTax" runat="server" meta:resourcekey="lcShippingTaxLabel"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxTaxableShippingCostDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxTaxableShippingCostDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxShippingTaxClassTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxShippingTaxClassHelp" ConfigName="ShippingTaxClass" runat="server" />
    <asp:Label ID="uxShippingTaxClassLabel" runat="server" Text="Shipping Tax Class"
        CssClass="BulletLabel"></asp:Label>
    <asp:DropDownList ID="uxShippingTaxClassDrop" runat="server" CssClass="fl DropDown">
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxTaxIncludedInPriceTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxTaxIncludedInPriceHelp" ConfigName="TaxIncludedInPrice" runat="server" />
    <asp:Label ID="lcTaxIncludedInPrice" runat="server" meta:resourcekey="lcTaxIncludedInPrice"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxTaxIncludedInPriceDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxTaxIncludedInPriceDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxTaxPercentageIncludedInPriceTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxTaxPercentageIncludedInPriceHelp" ConfigName="TaxPercentageIncludedInPrice"
        runat="server" />
    <asp:Label ID="uxTaxPercentageIncludedInPriceLabel" runat="server" Text="Tax Percentage Include in price (%)"
        CssClass="BulletLabel"></asp:Label>
    <asp:TextBox ID="uxTaxPercentageIncludedInPriceText" runat="server" MaxLength="6"
        CssClass="TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxTaxPercentageValueRequired" runat="server" ControlToValidate="uxTaxPercentageIncludedInPriceText"
        ValidationGroup="SiteConfigValid" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Tax Percentage is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="uxTaxPercentageIncludedInPriceRange" runat="server" ControlToValidate="uxTaxPercentageIncludedInPriceText"
        Display="Dynamic" MaximumValue="1000" MinimumValue="0" ValidationGroup="SiteConfigValid"
        Type="Double" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Tax Percentage must be between 0 and 1000.
        <div class="CommonValidateDiv">
        </div>
    </asp:RangeValidator>
</asp:Panel>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxTaxPriceAfterDiscountHelp" ConfigName="TaxPriceAfterDiscount"
        runat="server" />
    <asp:Label ID="lcTaxPriceAfterDiscount" runat="server" meta:resourcekey="lcTaxPriceAfterDiscount"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxTaxPriceAfterDiscountDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
