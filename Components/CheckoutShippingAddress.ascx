<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutShippingAddress.ascx.cs"
    Inherits="Components_CheckoutShippingAddress" %>
<%@ Register Src="CountryAndStateList.ascx" TagName="CountryAndState" TagPrefix="uc1" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc2" %>
<div class="CommonValidateText">
    <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red"></asp:Label>
</div>
<asp:Panel ID="uxBillingInfoPanel" runat="server" CssClass="CheckoutBillingInfoPanel">
    <div class="SidebarTop">
        <asp:Label ID="uxHeaderCheckoutLabel" runat="server" Text="[$Bill]" CssClass="CheckoutAddressTitle"></asp:Label>
    </div>
    <div class="CheckoutInnerTitle">
        [$Intro]
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$First Name]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingFirstName" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingFirstNameRequired" runat="server" ControlToValidate="uxBillingFirstName"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing First Name
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Last Name]:
        </div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxBillingLastName" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingLastNameRequired" runat="server" ControlToValidate="uxBillingLastName"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing Last Name
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Email]</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingEmail" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingEmailRequired" runat="server" ControlToValidate="uxBillingEmail"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing Email
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="uxBillingEmailRegular" runat="server" ControlToValidate="uxBillingEmail"
                CssClass="CommonValidatorText" ValidationGroup="shippingAddress" Display="Dynamic"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Billing Email Format
            </asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Company]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingCompany" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Address]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingAddress1" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingAddress1Required" runat="server" ControlToValidate="uxBillingAddress1"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing Address
            </asp:RequiredFieldValidator></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Address]2:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxBillingAddress2" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$City]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingCity" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingCityRequired" runat="server" ControlToValidate="uxBillingCity"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing City
            </asp:RequiredFieldValidator></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Zip]:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxBillingZip" runat="server" ValidationGroup="shippingAddress" MaxLength="9"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxBillingZipRequired" runat="server" ControlToValidate="uxBillingZip"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Billing ZipCode
            </asp:RequiredFieldValidator></div>
    </div>
    <uc1:CountryAndState ID="uxCountryAndState" runat="server" IsRequiredCountry="true"
        IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
        CssLabel="CheckoutAddressLabel" />
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
        </div>
        <div class="CheckoutAddressLeftData">
            <div id="uxBillingCountryStateDiv" runat="server" class="CommonValidatorText" visible="false">
                <div class="CommonValidateDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxBillingCountryStateMessage" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Phone]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxBillingPhone" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Fax]:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxBillingFax" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressLong">
        <div class="CheckoutAddressLeftLabel">
            &nbsp;</div>
        <div class="CheckoutAddressLeftDataCheckbox">
            <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" Text="[$UseBillingAsShipping]" CssClass="CustomerRegisterCheckBox UseBillingCheckbox"
                Visible="False" />
        </div>
    </div>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxPreferGiftAddressPanel" runat="server" CssClass="CheckoutGiftShippingInfoPanel">
    <div class="SidebarTop">
        <div class="CheckoutAddressTitle">
            [$Shipping Details]
        </div>
    </div>
    <div class="ShippingAddressList">
        <asp:RadioButton ID="uxPreferShippingAddressRadio" runat="server" Text="[$PreferredAddress]"
            GroupName="address" ValidationGroup="shippingAddress" />
        <asp:Panel ID="uxPreferAddressPanel" runat="server" CssClass="CheckoutPreferredAddress">
            <asp:Literal ID="uxPreferAddressLiteral" runat="server"></asp:Literal>
        </asp:Panel>
        <asp:RadioButton ID="uxAnotherShippingAddressRadio" runat="server" Text="[$AnotherAddress]"
            GroupName="address" ValidationGroup="shippingAddress" /></div>
</asp:Panel>
<asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="CheckoutShippingInfoPanel">
    <div class="SidebarTop">
        <div class="CheckoutAddressTitle">
            [$ShippingDetail]
        </div>
        <asp:Panel ID="uxShippingAliasNamePanel" runat="server">
            <div class="CheckoutAddressLeftDataSelectAddress">
                <asp:DropDownList ID="uxShippingAddressDrop" runat="server" DataTextField="AliasName"
                    AutoPostBack="true" DataValueField="ShippingAddressID" OnSelectedIndexChanged="uxShippingAddressDrop_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </asp:Panel>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$First Name]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxShippingFirstName" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxShippingFirstNameRequired" runat="server" ControlToValidate="uxShippingFirstName"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping First Name
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Last Name]:
        </div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxShippingLastName" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxShippingLastNameRequired" runat="server" ControlToValidate="uxShippingLastName"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Last Name
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Company]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxShippingCompany" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressRight">
        &nbsp;
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Address]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxShippingAddress1" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxShippingAddress1Required" runat="server" ControlToValidate="uxShippingAddress1"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Address
            </asp:RequiredFieldValidator></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Address]2:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxShippingAddress2" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$City]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxShippingCity" runat="server" ValidationGroup="shippingAddress"
                CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxShippingCityRequired" runat="server" ControlToValidate="uxShippingCity"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping City
            </asp:RequiredFieldValidator></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Zip]:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxShippingZip" runat="server" ValidationGroup="shippingAddress"
                MaxLength="9" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
            <span class="CommonAsterisk">*</span>
            <asp:RequiredFieldValidator ID="uxShippingZipRequired" runat="server" ControlToValidate="uxShippingZip"
                ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping ZipCode
            </asp:RequiredFieldValidator></div>
    </div>
    <uc1:CountryAndState ID="uxCountryState" runat="server" IsRequiredCountry="true"
        IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
        CssLabel="CheckoutAddressLabel" />
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
        </div>
        <div class="CheckoutAddressLeftData">
            <div id="uxShippingCountryStateDiv" runat="server" class="CommonValidatorText" visible="false">
                <div class="CommonValidateDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxShippingCountryStateMessage" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="CheckoutAddressLeft">
        <div class="CheckoutAddressLeftLabel">
            [$Phone]:</div>
        <div class="CheckoutAddressLeftData">
            <asp:TextBox ID="uxShippingPhone" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div class="CheckoutAddressRight">
        <div class="CheckoutAddressRightLabel">
            [$Fax]:</div>
        <div class="CheckoutAddressRightData">
            <asp:TextBox ID="uxShippingFax" runat="server" CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox></div>
    </div>
    <div id="uxShippingResidentialLabelDiv" runat="server" class="CheckoutShippingLabel">
        [$Residential]:</div>
    <div id="uxShippingResidentialDataDiv" runat="server" class="CheckoutShippingData">
        <asp:DropDownList ID="uxShippingResidentialDrop" runat="server">
            <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
            <asp:ListItem Value="False">[$No]</asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Panel>
<asp:Panel ID="uxSaleTaxExemptPanel" runat="server" CssClass="CheckoutSaleTaxExemptPanel">
    <div class="CheckoutAddressLong">
        <div class="CheckoutAddressLeftData">
            <asp:CheckBox ID="uxTaxExemptCheck" runat="server" Text="[$CustomerTaxExempt]" />
        </div>
    </div>
    <asp:Panel ID="uxCustomerTaxExemptPanel" runat="server" CssClass="CheckoutTaxExemptPanel">
        <div class="CheckoutAddressLeft">
            <div class="CheckoutAddressLeftLabel">
                [$TaxExemptID]:</div>
            <div class="CheckoutAddressLeftData">
                <asp:TextBox ID="uxTaxExemptID" runat="server" ValidationGroup="shippingAddress"
                    CssClass="CommonTextBox CheckoutAddressTextBox"></asp:TextBox>
                <span class="CommonAsterisk">*</span>
                <asp:RequiredFieldValidator ID="uxTaxExemptIDRequired" runat="server" ControlToValidate="uxTaxExemptID"
                    ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                    <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Tax Exempt ID
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <uc1:CountryAndState ID="uxTaxExemptCountryAndState" runat="server" IsRequiredCountry="true"
            IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
            CssLabel="CheckoutAddressLabel" />
        <div class="CheckoutAddressLeft">
            <div class="CheckoutAddressLeftLabel">
            </div>
            <div class="CheckoutAddressLeftData">
                <div id="uxTaxExemptCountryStateDiv" runat="server" class="CommonValidatorText" visible="false">
                    <div class="CommonValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxTaxExemptCountryStateMessage" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="Clear">
        </div>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="uxSpecialPanel" runat="server" CssClass="CheckoutSpecialRequestPanel">
    <div class="CheckoutAddressLong">
        <div class="CheckoutAddressLeftDataCheckbox">
            <asp:CheckBox ID="uxSpecialCheck" runat="server" Text="[$SpecialRequest]" Checked="false" />
            <asp:TextBox ID="uxCustomerComments" runat="server" TextMode="MultiLine" Rows="5"
                CssClass="GiftCouponDetailSpecialRequestTextBox" />
        </div>
    </div>
</asp:Panel>
