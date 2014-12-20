<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingUps.ascx.cs" Inherits="AdminAdvanced_MainControls_ShippingUps" %>
<%@ Register Src="../Components/Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server"></uc1:Message>
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="ShippingValid"
            meta:resourcekey="uxValidationSummary" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
        </div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonTextTitle1 mgb10">
                <asp:Label ID="lcSetEnabled" runat="server" meta:resourcekey="lcSetEnabled"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:DropDownList ID="uxIsEnabledDrop" runat="server" CssClass="DropDown" AutoPostBack="true"
                    OnSelectedIndexChanged="uxIsEnabledDrop_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcUPSShipping" runat="server" meta:resourcekey="lcUPSShipping"></asp:Label>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantZip" runat="server" meta:resourcekey="lcMerchantZip" CssClass="Label" />
                <asp:TextBox ID="uxMerchantZipText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxMerchantZipRequiredValidator" runat="server" ControlToValidate="uxMerchantZipText"
                        meta:resourcekey="uxMerchantZipRequiredValidator" ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantCountry" runat="server" meta:resourcekey="lcMerchantCountry"
                    CssClass="Label" />
                <div class="fl">
                    <uc2:CountryList ID="uxCountryList" runat="server" IsCountryWithOther="false" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcUserID" runat="server" meta:resourcekey="lcUserID" CssClass="Label" />
                <asp:TextBox ID="uxUserIDText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxUserIDRequiredValidator" runat="server" ControlToValidate="uxUserIDText"
                        meta:resourcekey="uxUserIDRequiredValidator" ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" CssClass="Label" />
                <asp:TextBox ID="uxPasswordText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxPasswordRequiredValidator" runat="server" ControlToValidate="uxPasswordText"
                        meta:resourcekey="uxPasswordRequiredValidator" ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcAccessLicenseNumber" runat="server" meta:resourcekey="lcAccessLicenseNumber"
                    CssClass="Label" />
                <asp:TextBox ID="uxAccessLicenseNumberText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxAccessLicenseRequiredValidator" runat="server"
                        ControlToValidate="uxAccessLicenseNumberText" meta:resourcekey="uxAccessLicenseRequiredValidator"
                        ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcTestMode" runat="server" meta:resourcekey="lcTestMode" CssClass="Label" />
                <asp:DropDownList ID="uxUpsTestModeDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxNegotiatedRatesIndicator" runat="server" meta:resourcekey="uxNegotiatedRatesIndicator"
                    CssClass="Label" />
                <asp:DropDownList ID="uxNegotiatedRatesIndicatorDrop" runat="server" CssClass="DropDown"
                    OnSelectedIndexChanged="uxNegotiatedRatesIndicatorDrop_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="uxShipperNumberDiv" runat="server" class="CommonRowStyle">
                <asp:Label ID="uxShipperNumber" runat="server" meta:resourcekey="uxShipperNumber"
                    CssClass="BulletLabel" />
                <asp:TextBox ID="uxShipperNumberText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShipperNumberRequiredValidator" runat="server"
                        ControlToValidate="uxShipperNumberText" meta:resourcekey="uxShipperNumberRequiredValidator"
                        ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcServiceWarning" runat="server" meta:resourcekey="lcServiceWarning" />
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcUpsService" runat="server" meta:resourcekey="lcUpsService"></asp:Label>
            </div>
            <div class="mgl5">
                <asp:CheckBoxList ID="uxUPSServiceCheckList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                    Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="CommonRowStyle mgt5">
                <asp:Label ID="lcFreeShipping" runat="server" meta:resourcekey="lcFreeShipping" CssClass="Label" />
                <asp:DropDownList ID="uxUpsFreeShippingDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFreeShippingCost" runat="server" meta:resourcekey="lcFreeShippingCost"
                    CssClass="Label" />
                <asp:TextBox ID="uxUpsFreeShippingCostText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <asp:CompareValidator ID="uxFreeShippingCompareValidator" runat="server" ControlToValidate="uxUpsFreeShippingCostText"
                        meta:resourcekey="uxFreeShippingCompareValidator" Operator="DataTypeCheck" Type="Double"
                        ValidationGroup="ShippingValid">*</asp:CompareValidator></div>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcFreeForServices" runat="server" meta:resourcekey="lcFreeForServices" />
            </div>
            <div class="mgl5">
                <asp:CheckBoxList ID="uxUpsFreeShippingServiceCheckList" runat="server" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="mgt5">
                <div class="CommonRowStyle">
                    <asp:Label ID="lcPickUpType" runat="server" meta:resourcekey="lcPickUpType" CssClass="Label" />
                    <asp:DropDownList ID="uxPickupTypeDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="01">Daily Pickup</asp:ListItem>
                        <asp:ListItem Value="03">Customer Counter</asp:ListItem>
                        <asp:ListItem Value="06">One Time Pickup</asp:ListItem>
                        <asp:ListItem Value="07">On Call Air</asp:ListItem>
                        <asp:ListItem Value="19">Letter Center</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcMinWeight" runat="server" meta:resourcekey="lcMinWeight" CssClass="Label" />
                    <asp:DropDownList ID="uxMinPackageDrop" runat="server" CssClass="DropDown">
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcMinWeightLess" runat="server" meta:resourcekey="lcMinWeightLess"
                        CssClass="BulletLabel" />
                    <asp:TextBox ID="uxMinOrderWieghtText" runat="server" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <asp:CompareValidator ID="uxMinOrderWeightCompareValidator" runat="server" ControlToValidate="uxMinOrderWieghtText"
                            meta:resourcekey="uxMinOrderWeightCompareValidator" Operator="DataTypeCheck"
                            Type="Double" ValidationGroup="ShippingValid">*</asp:CompareValidator>
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcMarkup" runat="server" meta:resourcekey="lcMarkup" CssClass="Label" />
                    <asp:TextBox ID="uxMarkupText" runat="server" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <asp:CompareValidator ID="uxMarkupCompareValidator" runat="server" ControlToValidate="uxMarkupText"
                            Operator="DataTypeCheck" Type="Double" ValidationGroup="ShippingValid">*</asp:CompareValidator>
                    </div>
                </div>
                <asp:Panel ID="uxHandlingFeeTR" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee" CssClass="Label" />
                    <asp:TextBox ID="uxHandlingFeeText" runat="server" CssClass="TextBox" />
                    <div class="validator1 fl">
                        <asp:CompareValidator ID="uxHandlingFeeCompareValidator" runat="server" ControlToValidate="uxHandlingFeeText"
                            Operator="DataTypeCheck" Type="Double" ValidationGroup="ShippingValid">*</asp:CompareValidator>
                    </div>
                </asp:Panel>
                <div class="Clear">
                </div>
                <div class="CommonTextTitle1 mgt20 mgb10">
                    <asp:Label ID="lcAllowSetFree" runat="server" meta:resourcekey="lcAllowSetFree" />
                </div>
                <div class="mgl5">
                    <asp:CheckBoxList ID="uxUpsAllowedSetFreeCheckList" runat="server" RepeatColumns="3"
                        RepeatDirection="Horizontal" Width="100%">
                    </asp:CheckBoxList>
                </div>
                <div class="CommonTextTitle1 mgt20 mgb10">
                    <asp:Label ID="lcAllowUseFreeCoupon" runat="server" meta:resourcekey="lcAllowUseFreeCoupon" />
                </div>
                <div class="mgl5">
                    <asp:CheckBoxList ID="uxUpsAllowedUseFreeCouponCheckList" runat="server" RepeatColumns="3"
                        RepeatDirection="Horizontal" Width="100%">
                    </asp:CheckBoxList>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxUpdateConfigButton" runat="server" meta:resourcekey="uxUpdateConfigButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateConfigButton_Click"
                    OnClickGoTo="Top" ValidationGroup="ShippingValid"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
