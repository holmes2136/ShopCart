<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutDetails.ascx.cs"
    Inherits="Admin_Components_Order_CheckoutDetails" %>
<%@ Register Src="../CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Common/StateList.ascx" TagName="StateList" TagPrefix="uc1" %>
<%@ Register Src="../Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<%@ Register Src="../CurrencyControl.ascx" TagName="CurrencyControl" TagPrefix="uc10" %>
<%@ Register Src="../LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc5" %>
<%@ Register Src="CouponAndGiftDatails.ascx" TagName="CouponAndGift" TagPrefix="uc6" %>
<%@ Register Src="SelectShippingList.ascx" TagName="SelectShippingList" TagPrefix="uc7" %>
<%@ Register Src="SelectPaymentList.ascx" TagName="SelectPaymentList" TagPrefix="uc8" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <LanguageControlTemplate>
        <uc5:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
        <uc10:CurrencyControl ID="uxCurrencyControl" runat="server" />
    </LanguageControlTemplate>
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
            </div>
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidDetails" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <PlainContentTemplate>
        <div class="Container-Box">
            <div class="CommonTextTitle1 mgt0 mgb10">
                <asp:Label ID="lcCouponGiftDetails" runat="server" meta:resourcekey="lcCouponGift" />
            </div>
            <uc6:CouponAndGift ID="uxCouponGiftDetails" runat="server" />
            <div class="Clear">
            </div>
            <asp:Panel ID="uxAddressPanel" runat="server">
                <div class="CommonTextTitle1 mgt10 mgb10">
                    <asp:Label ID="lcBillingDetails" runat="server" meta:resourcekey="lcBilling" />
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" CssClass="Label" />
                    <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidDetails" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredUserNameValidator" runat="server" ControlToValidate="uxUserName"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Username is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcFirstName" runat="server" meta:resourcekey="lcFirstName" CssClass="Label" />
                    <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidDetails" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> First Name is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                    <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidDetails" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Last Name is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label" />
                    <div class="CountrySelect fl">
                        <uc2:CountryList ID="uxCountryList" runat="server" />
                        <div class="Clear">
                        </div>
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label" />
                    <asp:TextBox ID="uxCompany" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label" />
                    <asp:TextBox ID="uxAddress1" runat="server" Width="300px" ValidationGroup="ValidDetails"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Address is required.
                        <div class="CommonValidateDiv CommonValidateDivCustomerLong">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxBlankLabel" runat="server" CssClass="Label">&nbsp;</asp:Label>
                    <asp:TextBox ID="uxAddress2" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                    <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidDetails" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> City is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcState" runat="server" meta:resourcekey="lcState" CssClass="Label" />
                    <div class="CountrySelect fl">
                        <uc1:StateList ID="uxStateList" runat="server"></uc1:StateList>
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcZip" runat="server" meta:resourcekey="lcZip" CssClass="Label" />
                    <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidDetails" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Zip Code is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label" />
                    <asp:TextBox ID="uxPhone" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcFax" runat="server" meta:resourcekey="lcFax" CssClass="Label" />
                    <asp:TextBox ID="uxFax" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail" CssClass="Label" />
                    <asp:TextBox ID="uxEmail" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                        ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxEmail" ValidationGroup="ValidDetails" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email forma.
                        <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                        </div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="CommonRowStyle fb">
                    <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" AutoPostBack="True" OnCheckedChanged="uxUseBillingAsShipping_CheckedChanged"
                        meta:resourcekey="uxUseBillingAsShipping" CssClass="CheckBox mgl10" />
                    <div class="Clear">
                    </div>
                </div>
                <asp:Panel ID="uxPanelBillingAsShipping" runat="server" CssClass="mgt10">
                    <div class="CommonTextTitle1 mgt10 mgb10">
                        <asp:Label ID="lcShipping" runat="server" meta:resourcekey="lcShipping" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxShippingAliasName" runat="server" meta:resourcekey="lcShippingAliasName"
                            CssClass="Label" />
                        <asp:DropDownList ID="uxShippingAddressDrop" runat="server" DataTextField="AliasName"
                            AutoPostBack="true" DataValueField="ShippingAddressID" OnSelectedIndexChanged="uxShippingAliasNameDrop_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"
                            CssClass="Label" />
                        <asp:TextBox ID="uxShippingFirstName" runat="server" ValidationGroup="ValidDetails"
                            Width="150px" CssClass="TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredShippingFirstnameValidator" runat="server"
                            ControlToValidate="uxShippingFirstName" Display="Dynamic" CssClass="CommonValidatorText"
                            ValidationGroup="ValidDetails">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping first name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                        <asp:TextBox ID="uxShippingLastName" runat="server" ValidationGroup="ValidDetails"
                            Width="150px" CssClass="TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredShippingLastNameValidator" runat="server"
                            ControlToValidate="uxShippingLastName" Display="Dynamic" CssClass="CommonValidatorText"
                            ValidationGroup="ValidDetails">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping last name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                        </asp:RequiredFieldValidator></div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc2:CountryList ID="uxShippingCountryList" runat="server" />
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label" />
                        <asp:TextBox ID="uxShippingCompany" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label" />
                        <asp:TextBox ID="uxShippingAddress1" runat="server" Width="300px" ValidationGroup="ValidDetails"
                            CssClass="TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredShippingAddressValidator" runat="server"
                            ControlToValidate="uxShippingAddress1" Display="Dynamic" CssClass="CommonValidatorText"
                            ValidationGroup="ValidDetails">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping address is required.
                            <div class="CommonValidateDiv CommonValidateDivCustomerLong">
                            </div>
                        </asp:RequiredFieldValidator></div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="Label1" runat="server" CssClass="Label">&nbsp;</asp:Label>
                        <asp:TextBox ID="uxShippingAddress2" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                        <asp:TextBox ID="uxShippingCity" runat="server" ValidationGroup="ValidDetails" Width="150px"
                            CssClass="TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredShippingCityValidator" runat="server" ControlToValidate="uxShippingCity"
                            Display="Dynamic" CssClass="CommonValidatorText" ValidationGroup="ValidDetails">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping city is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingState" runat="server" meta:resourcekey="lcState" CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc1:StateList ID="uxShippingStateList" runat="server" />
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingZip" runat="server" meta:resourcekey="lcZip" CssClass="Label" />
                        <asp:TextBox ID="uxShippingZip" runat="server" ValidationGroup="ValidDetails" Width="150px"
                            CssClass="TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredShippingZipValidator" runat="server" ControlToValidate="uxShippingZip"
                            Display="Dynamic" CssClass="CommonValidatorText" ValidationGroup="ValidDetails">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Zip Code is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label" />
                        <asp:TextBox ID="uxShippingPhone" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingFax" runat="server" meta:resourcekey="lcFax" CssClass="Label" />
                        <asp:TextBox ID="uxShippingFax" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="CommonRowStyle">
                    <asp:Label ID="lcShippingResidential" runat="server" meta:resourcekey="lcShippingResidential"
                        CssClass="Label" />
                    <asp:DropDownList ID="uxShippingResidentialDrop" runat="server" CssClass="fl DropDown">
                        <asp:ListItem Value="True" Selected="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="mgt10">
                <div class="CommonTextTitle1 mgt10 mgb10">
                    Miscellaneous
                </div>
                <asp:Panel ID="uxIsTaxExemptPanel" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxIsTaxExemptLabel" runat="server" meta:resourcekey="uxIsTaxExemptLabel"
                            CssClass="Label" />
                        <asp:CheckBox ID="uxIsTaxExemptCheck" runat="server" CssClass="CheckBox" Checked="false"
                            OnCheckedChanged="uxIsTaxExemptCheck_CheckedChanged" AutoPostBack="true" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="uxCustomerTaxExemptPanel" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxTaxExemptIDLabel" runat="server" meta:resourcekey="uxTaxExemptIDLabel"
                            CssClass="Label" />
                        <asp:TextBox ID="uxTaxExemptID" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredTaxExemptIDValidator" runat="server" ControlToValidate="uxTaxExemptID"
                            ValidationGroup="ValidDetails" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Tax Exempt ID is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxTaxExemptCountryLabel" runat="server" meta:resourcekey="uxTaxExemptCountryLabel"
                            CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc2:CountryList ID="uxTaxExemptCountryList" runat="server" />
                            <div class="Clear">
                            </div>
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxTaxExemptStateLabel" runat="server" meta:resourcekey="uxTaxExemptStateLabel"
                            CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc1:StateList ID="uxTaxExemptStateList" runat="server"></uc1:StateList>
                        </div>
                    </div>
                </asp:Panel>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcSpecialRequest" runat="server" meta:resourcekey="lcSpecialRequest"
                        CssClass="Label" />
                    <asp:TextBox ID="uxCustomerComments" runat="server" Width="300px" Height="80px" TextMode="MultiLine"
                        CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10" id="uxShippingListDiv" runat="server">
                <div class="CommonTextTitle1 mgt10 mgb10">
                    Shipping Methods
                </div>
                <div class="CommonRowStyle">
                    <asp:LinkButton ID="uxSelectShippingMethodButton" runat="server" OnClick="uxSelectShippingMethodButton_Click"
                        CssClass="fl mgl5" ValidationGroup="ValidDetails">Select Shipping Method</asp:LinkButton>
                </div>
                <div class="CommonRowStyle">
                    <asp:LinkButton ID="uxChangeShippingAddrButton" runat="server" OnClick="uxChangeShippingAddrButton_Click"
                        CssClass="fl mgl5" Visible="false">Change Shipping Address</asp:LinkButton>
                </div>
                <asp:Panel ID="uxShippingListPanel" runat="server" Visible="false" CssClass="CommonRowStyle">
                    <uc7:SelectShippingList ID="uxSelectShippingList" runat="server"></uc7:SelectShippingList>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <div class="CommonTextTitle1 mgt10 mgb10">
                    Payment Methods
                </div>
                <div class="CommonRowStyle">
                    <asp:LinkButton ID="uxSelectPaymentMethodButton" runat="server" OnClick="uxSelectPaymentMethodButton_Click"
                        CssClass="fl mgl5" ValidationGroup="ValidDetails">Select Payment Method</asp:LinkButton>
                </div>
                <div class="CommonRowStyle">
                    <asp:LinkButton ID="uxChangeCheckOutDatailsButton" runat="server" OnClick="uxChangeCheckOutDatailsButton_Click"
                        CssClass="fl mgl5" Visible="false">Change Check out details</asp:LinkButton>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxPaymentNote" runat="server" CssClass="Label" Text="No req payment"
                        Visible="false" />
                </div>
                <asp:Panel ID="uxPaymentListPanel" runat="server" Visible="false" CssClass="CommonRowStyle">
                    <uc8:SelectPaymentList ID="uxSelectPaymentList" runat="server"></uc8:SelectPaymentList>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <div class="CommonRowStyle">
                    <vevo:AdvanceButton ID="uxNextButton" runat="server" meta:resourcekey="uxNextButton"
                        CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxNextButton_Click"
                        OnClickGoTo="Top" ValidationGroup="ValidDetails"></vevo:AdvanceButton>
                </div>
                <div class="Clear">
                </div>
            </div>
    </PlainContentTemplate>
</uc2:AdminContent>
