<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingFedEx.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingFedEx" %>
<%@ Register Src="../Components/Common/StateList.ascx" TagName="StateList" TagPrefix="uc3" %>
<%@ Register Src="../Components/Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="FedExRealTime"
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
                <asp:Label ID="lcSetEnabled" runat="server" meta:resourcekey="lcSetEnabled" CssClass="Label" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:DropDownList ID="uxIsEnabledDrop" runat="server" CssClass="DropDown fl" AutoPostBack="true"
                    OnSelectedIndexChanged="uxIsEnabledDrop_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcHeader" runat="server" Text="FedEx Configuration" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantZip" runat="server" meta:resourcekey="lcMerchantZip" CssClass="Label" />
                <asp:TextBox ID="uxMerchantZipText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxMerchantZipRequiredValidator" runat="server" ControlToValidate="uxMerchantZipText"
                        meta:resourcekey="uxMerchantZipRequiredValidator" ValidationGroup="FedExRealTime"><--</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantCountry" runat="server" meta:resourcekey="lcMerchantCountry"
                    CssClass="Label" />
                <div class="CountrySelect fl">
                    <uc2:CountryList ID="uxCountryList" runat="server" IsCountryWithOther="false"></uc2:CountryList>
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantState" runat="server" meta:resourcekey="lcMerchantState"
                    CssClass="Label" />
                <div class="CountrySelect fl">
                    <uc3:StateList ID="uxStateList" runat="server" IsStateWithOther="false" />
                </div>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcKey" runat="server" meta:resourcekey="lcKey" CssClass="Label" />
                <asp:TextBox ID="uxKeyText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxKeyRequiredValidator" runat="server" ControlToValidate="uxKeyText"
                        meta:resourcekey="uxKeyRequiredValidator" ValidationGroup="FedExRealTime"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" CssClass="Label" />
                <asp:TextBox ID="uxPasswordText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxPasswordRequiredValidator" runat="server" ControlToValidate="uxAccountNumberText"
                        meta:resourcekey="uxPasswordRequiredValidator" ValidationGroup="FedExRealTime"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcAccountNumber" runat="server" meta:resourcekey="lcAccountNumber"
                    CssClass="Label" />
                <asp:TextBox ID="uxAccountNumberText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxAccountNumberRequiredValidator" runat="server"
                        ControlToValidate="uxAccountNumberText" meta:resourcekey="uxAccountNumberRequiredValidator"
                        ValidationGroup="FedExRealTime"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMeterNumber" runat="server" meta:resourcekey="lcMeterNumber" CssClass="Label" />
                <asp:TextBox ID="uxMeterNumberText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxMeterNumberRequiredValidator" runat="server" ControlToValidate="uxMeterNumberText"
                        meta:resourcekey="uxMeterNumberRequiredValidator" ValidationGroup="FedExRealTime"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcAccountType" runat="server" meta:resourcekey="lcAccountType" CssClass="Label" />
                <asp:DropDownList ID="uxFedExTestModeDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="https://ws.fedex.com:443/web-services/rate">Production</asp:ListItem>
                    <asp:ListItem Value="https://gatewaybeta.fedex.com/web-services">Test</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxRateRequestLabel" runat="server" meta:resourcekey="uxRateRequestLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxRateRequestDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="account" Text="Account"></asp:ListItem>
                    <asp:ListItem Value="list" Text="List"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxInsuranceValueLabel" runat="server" meta:resourcekey="uxInsuranceValueLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxInsuranceValueDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxCustomsLabel" runat="server" meta:resourcekey="uxCustomsLabel"
                    CssClass="Label" />
                <asp:DropDownList ID="uxCustomsDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcServiceWarning" runat="server" meta:resourcekey="lcServiceWarning" />
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcFedExService" runat="server" meta:resourcekey="lcFedExService" Font-Bold="true" />
            </div>
            <div class="CommonRowStyle mgt10">
                <asp:CheckBoxList ID="uxFedExServiceCheckList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                    Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcFreeShipping" runat="server" meta:resourcekey="lcFreeShipping" Font-Bold="true" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFreeShippingEnabled" runat="server" meta:resourcekey="lcFreeShippingEnabled"
                    CssClass="Label" />
                <asp:DropDownList ID="uxFreeShippingDrop" runat="server" CssClass="DropDown fl">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFreeShippingCost" runat="server" meta:resourcekey="lcFreeShippingCost"
                    CssClass="Label" />
                <asp:TextBox ID="uxFreeShippingCostText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <asp:CompareValidator ID="uxFreeShippingCostCompareValidator" runat="server" ControlToValidate="uxFreeShippingCostText"
                        meta:resourcekey="uxFreeShippingCostCompareValidator" Operator="DataTypeCheck"
                        Type="Currency" ValidationGroup="FedExRealTime">*</asp:CompareValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFreeShippingFor" runat="server" meta:resourcekey="lcFreeShippingFor" />
            </div>
            <div>
                <asp:CheckBoxList ID="uxFreeShippingServiceCheckList" runat="server" RepeatColumns="2"
                    RepeatDirection="Horizontal" Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcDropoffType" runat="server" meta:resourcekey="lcDropoffType" CssClass="Label" />
                <asp:DropDownList ID="uxDropoffTypeDrop" runat="server" CssClass="DropDown fl">
                    <asp:ListItem Value="REGULAR_PICKUP">Regular Pickup</asp:ListItem>
                    <asp:ListItem Value="REQUEST_COURIER">Request Courier</asp:ListItem>
                    <asp:ListItem Value="DROP_BOX">Drop Box</asp:ListItem>
                    <asp:ListItem Value="BUSINESS_SERVICE_CENTER">Business Service Center</asp:ListItem>
                    <asp:ListItem Value="STATION">Station</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcPackingType" runat="server" meta:resourcekey="lcPackingType" CssClass="Label" />
                <asp:DropDownList ID="uxPickupTypeDrop" runat="server" CssClass="DropDown fl">
                    <asp:ListItem Value="FEDEX_ENVELOPE">FedEx Envelope</asp:ListItem>
                    <asp:ListItem Value="FEDEX_PAK">FedEx Pak</asp:ListItem>
                    <asp:ListItem Value="FEDEX_BOX">FedEx Box</asp:ListItem>
                    <asp:ListItem Value="FEDEX_TUBE">FedEx Tube</asp:ListItem>
                    <asp:ListItem Value="FEDEX_10KG_BOX">FedEx 10Kg Box</asp:ListItem>
                    <asp:ListItem Value="FEDEX_25KG_BOX">FedEx 25Kg Box</asp:ListItem>
                    <asp:ListItem Value="YOUR_PACKAGING">Your Packaging</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMinimumWeight" runat="server" meta:resourcekey="lcMinimumWeight"
                    CssClass="Label" />
                <asp:DropDownList ID="uxMinimumWeightDrop" runat="server" CssClass="DropDown fl">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMinimumWeightDetails" runat="server" meta:resourcekey="lcMinimumWeightDetails"
                    CssClass="BulletLabel" />
                <asp:TextBox ID="uxMinimumWeightText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <asp:CompareValidator ID="uxMinimumWeightCompareValidator" runat="server" ControlToValidate="uxMinimumWeightText"
                        meta:resourcekey="uxMinimumWeightCompareValidator" Operator="DataTypeCheck" Type="Double"
                        ValidationGroup="FedExRealTime">*</asp:CompareValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMarkup" runat="server" meta:resourcekey="lcMarkup" CssClass="Label" />
                <asp:TextBox ID="uxMarkupText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <asp:CompareValidator ID="uxMarkupCompareValidator" runat="server" ControlToValidate="uxMarkupText"
                        meta:resourcekey="uxMarkupCompareValidator" Operator="DataTypeCheck" Type="Currency"
                        ValidationGroup="FedExRealTime">*</asp:CompareValidator></div>
            </div>
            <asp:Panel ID="uxHandlingFeeTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee" CssClass="Label" />
                <asp:TextBox ID="uxHandlingFeeText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <asp:CompareValidator ID="uxHandlingFeeCompareValidator" runat="server" ControlToValidate="uxHandlingFeeText"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="FedExRealTime">*</asp:CompareValidator></div>
            </asp:Panel>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcAllowSetFree" runat="server" meta:resourcekey="lcAllowSetFree" />
            </div>
            <div class="mgl5">
                <asp:CheckBoxList ID="uxFedExAllowedSetFreeCheckList" runat="server" RepeatColumns="2"
                    RepeatDirection="Horizontal" Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcAllowUseFreeCoupon" runat="server" meta:resourcekey="lcAllowUseFreeCoupon" />
            </div>
            <div class="mgl5">
                <asp:CheckBoxList ID="uxFedExAllowedUseFreeCouponCheckList" runat="server" RepeatColumns="2"
                    RepeatDirection="Horizontal" Width="100%">
                </asp:CheckBoxList>
            </div>
            <div class="Clear">
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxUpdateConfigButton" runat="server" meta:resourcekey="uxUpdateConfigButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateConfigButton_Click"
                    OnClickGoTo="Top" ValidationGroup="FedExRealTime"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
