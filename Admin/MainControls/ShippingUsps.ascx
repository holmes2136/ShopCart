<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingUsps.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingUsps" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server"></uc1:Message>
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="ShippingValid"
            CssClass="ValidationStyle" meta:resourcekey="uxValidationSummary" />
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
                <asp:Label ID="lcSetEnabled" runat="server" meta:resourcekey="lcSetEnabled" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:DropDownList ID="uxIsEnabledDrop" runat="server" CssClass="fl DropDown" AutoPostBack="true"
                    OnSelectedIndexChanged="uxIsEnabledDrop_SelectedIndexChanged">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="uxUspsLabel" runat="server" Text="USPS Configuration" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcUserID" runat="server" meta:resourcekey="lcUserID" CssClass="Label" />
                <asp:TextBox ID="uxUserIDText" runat="server" ValidationGroup="ShippingValid" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxUserIDRequired" runat="server" ErrorMessage="UserID is Required."
                        ControlToValidate="uxUserIDText" ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantZip" runat="server" meta:resourcekey="lcMerchantZip" CssClass="Label" />
                <asp:TextBox ID="uxUspsMerchantZipText" runat="server" ValidationGroup="ShippingValid"
                    CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxMerchantZipRequired" runat="server" ControlToValidate="uxUspsMerchantZipText"
                        ErrorMessage="Merchant Zip is Required." ValidationGroup="ShippingValid"><--</asp:RequiredFieldValidator></div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMerchantCountry" runat="server" meta:resourcekey="lcMerchantCountry"
                    CssClass="Label" />
                <asp:Label ID="lcMerchantCountryValue" runat="server" Text="US" CssClass="fl" />
            </div>
            <%-- Hide drop down ot choose USPS Url destination since only "Production Non-secure" works --%>
            <div id="uxServerUrlDiv" runat="server" visible="false" class="CommonRowStyle">
                <asp:Label ID="lcUrlPost" runat="server" meta:resourcekey="lcUrlPost" CssClass="Label" />
                <asp:DropDownList ID="uxUrlDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="http://production.shippingapis.com/ShippingAPI.dll">Production Non-secure</asp:ListItem>
                    <asp:ListItem Value="https://secure.shippingapis.com/ShippingAPI.dll">Production Secure (SSL)</asp:ListItem>
                    <asp:ListItem Value="http://testing.shippingapis.com/ShippingAPITest.dll">Test Non-secure</asp:ListItem>
                    <asp:ListItem Value="https://secure.shippingapis.com/ShippingAPITest.dll">Test Secure (SSL)</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMailType" runat="server" meta:resourcekey="lcMailType" CssClass="Label" />
                <asp:DropDownList ID="uxMailTypeDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem>Package</asp:ListItem>
                    <asp:ListItem>Postcards or aerogrammes</asp:ListItem>
                    <asp:ListItem>Matter for the blind</asp:ListItem>
                    <asp:ListItem>Envelope</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcServiceWarning" runat="server" meta:resourcekey="lcServiceWarning" />
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcServiceEnabled" runat="server" meta:resourcekey="lcServiceEnabled" />
            </div>
            <div class="CommonRowStyle">
                <asp:CheckBoxList ID="uxServiceEnabledCheckList" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsFreeShipping" runat="server" meta:resourcekey="lcIsFreeShipping"
                    CssClass="Label" />
                <asp:DropDownList ID="uxIsFreeShippingCostDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFreeShippingCost" runat="server" meta:resourcekey="lcFreeShippingCost"
                    CssClass="Label" />
                <asp:TextBox ID="uxFreeShippingCostText" runat="server" Width="50px" CssClass="TextBox" />
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcServiceFreeShipping" runat="server" meta:resourcekey="lcServiceFreeShipping"
                    CssClass="Label" />
            </div>
            <div class="CommonRowStyle">
                <asp:CheckBoxList ID="uxServiceFreeShippingCheckList" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcUseMinWeight" runat="server" meta:resourcekey="lcUseMinWeight" CssClass="Label" />
                <asp:DropDownList ID="uxIsMinWeightDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMinOrder" runat="server" meta:resourcekey="lcMinOrder" CssClass="BulletLabel" />
                <asp:TextBox ID="uxMinWeightOrderText" runat="server" Width="50px" CssClass="TextBox" />
                <asp:CompareValidator ID="uxMinimumWeightCompareValidator" runat="server" ControlToValidate="uxMinWeightOrderText"
                    meta:resourcekey="uxMinimumWeightCompareValidator" Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="ShippingValid"> * </asp:CompareValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcMarkUpPrice" runat="server" meta:resourcekey="lcMarkUpPrice" CssClass="Label" />
                <asp:TextBox ID="uxMarkUpPriceText" runat="server" Width="50px" CssClass="TextBox" />
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
                <asp:CheckBoxList ID="uxUspsAllowedSetFreeCheckList" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </div>
            <div class="Clear">
            </div>
            <div class="CommonTextTitle1 mgt20 mgb10">
                <asp:Label ID="lcAllowUseFreeCoupon" runat="server" meta:resourcekey="lcAllowUseFreeCoupon" />
            </div>
            <div class="mgl5">
                <asp:CheckBoxList ID="uxUspsAllowedUseFreeCouponCheckList" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </div>
            <div class="Clear">
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxSubmitButton" runat="server" meta:resourcekey="uxSubmitButton"
                    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxSubmitButton_Click" OnClickGoTo="Top" ValidationGroup="ShippingValid">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
