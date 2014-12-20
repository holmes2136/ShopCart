<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressDetails.ascx.cs"
    Inherits="Mobile_Components_ShippingAddressDetails" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryState" TagPrefix="uc3" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Register Src="MobileMessage.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="~/Components/VevoHyperLink.ascx" TagName="VevoHyperLink" TagPrefix="ucHyperLink" %>
<div class="MobileCommonBox">
    <uc3:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
    <div class="MobileLoginNote">
        [$Required Fields]
    </div>
    <div class="MobileUserLoginControl">
        <asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="CustomerRegisterShippingInfoPanel">
            <div class="MobileCommonPageInnerTitle">
                [$ShippingInfo]</div>
            <div class="MobileCommonFormLabel">
                [$ShippAliasName]</div>
            <div class="MobileCommonFormData">
                <asp:TextBox ID="uxShippingAliasName" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                <span class="MobileCommonAsterisk">*</span>
                <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxShippingAliasName"
                    ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                    <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Alias Name
                </asp:RequiredFieldValidator>
            </div>
            <div class="MobileCommonFormLabel">
            </div>
            <div class="ValidShippingAddresssCheckBoxDiv">
                <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" Text="[$UseBill]" CssClass="ValidShippingAddresssCheckBox"
                    OnCheckedChanged="uxUseBillingAsShipping_CheckedChanged" AutoPostBack="true" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxShippingDetailsPanel" runat="server">
                <div class="MobileCommonFormLabel">
                    [$ShippFName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingFirstName" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingFirstNameRequired" runat="server" ControlToValidate="uxShippingFirstName"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping First Name
                    </asp:RequiredFieldValidator>
                </div>
                <div class="MobileCommonFormLabel">
                    [$ShippLName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingLastName" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingLastNameRequired" runat="server" ControlToValidate="uxShippingLastName"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Last Name
                    </asp:RequiredFieldValidator>
                </div>
                <div class="MobileCommonFormLabel">
                    [$ShippingCompany]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingCompany" runat="server" CssClass="MobileCommonTextBox CustomerRegisterLongTextBox"></asp:TextBox></div>
                <div class="MobileCommonFormLabel">
                    [$ShippingAddress]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingAddress1" runat="server" CssClass="MobileCommonTextBox CustomerRegisterLongTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingAddress1Required" runat="server" ControlToValidate="uxShippingAddress1"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Address
                    </asp:RequiredFieldValidator>
                </div>
                <div class="MobileCommonFormLabel">
                    &nbsp;</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingAddress2" runat="server" CssClass="MobileCommonTextBox CustomerRegisterLongTextBox"></asp:TextBox></div>
                <div class="MobileCommonFormLabel">
                    [$ShippingCity]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingCity" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingCityRequired" runat="server" ControlToValidate="uxShippingCity"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping City
                    </asp:RequiredFieldValidator>
                </div>
                <div class="MobileCommonFormLabel">
                    [$ShippingZip]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingZip" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingZipRequired" runat="server" ControlToValidate="uxShippingZip"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Zip
                    </asp:RequiredFieldValidator>
                </div>
                <uc3:CountryState ID="uxShippingCountryState" runat="server" IsRequiredCountry="true"
                    IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="CustomerRegisterCityStatePanel"
                    CssLabel="MobileCommonFormLabel" />
                <div id="uxShippingCountryStateDiv" runat="server" class="MobileCommonValidatorText CustomerRegisterPanelCountryValidatorText"
                    visible="false">
                    <div class="MobileCommonValidateDiv CountryAndStateValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxShippingCountryStateMessage" runat="server"></asp:Label>
                </div>
                <div class="MobileCommonFormLabel">
                    [$ShippingPhone]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingPhone" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
                <div class="MobileCommonFormLabel">
                    [$ShippingFax]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingFax" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
                <div class="Clear">
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="CustomerRegisterShippingResidentialPanel">
            <div class="MobileCommonFormLabel">
                [$ShippingResidential]</div>
            <div class="MobileCommonFormData">
                <asp:DropDownList ID="uxShippingResidentialDrop" runat="server">
                    <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
                    <asp:ListItem Value="False">[$No]</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="Clear">
            </div>
        </asp:Panel>
        <div class="CustomerRegisterLinkButtonDiv">
            <asp:LinkButton ID="uxAddButton" runat="server" ValidationGroup="ValidShippingAddress"
                Text="[$AddShippingAddressButton]" CssClass="MobileButton MobileCouponButton"
                OnClick="uxAddButton_Click" />
        </div>
        <div class="CustomerRegisterLinkButtonDiv">
            <asp:LinkButton ID="uxUpdateButton" runat="server" ValidationGroup="ValidShippingAddress"
                Text="[$UpdateShippingAddressButton]" CssClass="MobileButton MobileCouponButton"
                OnClick="uxUpdateButton_Click" />
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
