<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressDetails.ascx.cs"
    Inherits="Components_ShippingAddressDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryState" TagPrefix="uc3" %>
<div class="CustomerRegister">
    <div class="CustomerRegisterNote">
        [$Required Fields]
    </div>
    <div class="CustomerRegisterDiv">
        <asp:Label ID="uxMessageSuccess" runat="server" ForeColor="Red"></asp:Label>
        <div id="uxSummaryValidDIV" class="CommonValidateText">
            <asp:Literal ID="uxSummaryLiteral" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="CustomerRegisterShippingInfoPanel">
            <div class="Title">
                [$ShippingInfo]
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$ShippAliasName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxShippingAliasName" runat="server" ValidationGroup="ValidShippingAddress"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingAliasNameRequired" runat="server" ControlToValidate="uxShippingAliasName"
                        ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Alias Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                </div>
                <div class="AccountDetailsCheckBoxDiv">
                    <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" Text="[$UseBill]" CssClass="AccountDetailsCheckBox"
                        OnCheckedChanged="uxUseBillingAsShipping_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <asp:Panel ID="uxShippingDetailsPanel" runat="server">
                <div class="CustomerRegisterLeft">
                    <div class="CustomerRegisterLeftLabel">
                        [$ShippFName]</div>
                    <div class="CustomerRegisterLeftData">
                        <asp:TextBox ID="uxShippingFirstName" runat="server" ValidationGroup="ValidShippingAddress"
                            CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxShippingFirstNameRequired" runat="server" ControlToValidate="uxShippingFirstName"
                            ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping First Name
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="CustomerRegisterRight">
                    <div class="CustomerRegisterRightLabel">
                        [$ShippLName]
                    </div>
                    <div class="CustomerRegisterRightData">
                        <asp:TextBox ID="uxShippingLastName" runat="server" ValidationGroup="ValidShippingAddress"
                            CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxShippingLastNameRequired" runat="server" ControlToValidate="uxShippingLastName"
                            ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Last Name
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="CustomerRegisterLeft">
                    <div class="CustomerRegisterLeftLabel">
                        [$ShippingCompany]</div>
                    <div class="CustomerRegisterLeftData">
                        <asp:TextBox ID="uxShippingCompany" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="CustomerRegisterLeft">
                    <div class="CustomerRegisterLeftLabel">
                        [$ShippingAddress]</div>
                    <div class="CustomerRegisterLeftData">
                        <asp:TextBox ID="uxShippingAddress1" runat="server" ValidationGroup="ValidShippingAddress"
                            CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxShippingAddress1Required" runat="server" ControlToValidate="uxShippingAddress1"
                            ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Address
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="CustomerRegisterRight">
                    <div class="CustomerRegisterRightLabel">
                        [$ShippingAddress2]
                    </div>
                    <div class="CustomerRegisterRightData">
                        <asp:TextBox ID="uxShippingAddress2" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="CustomerRegisterLeft">
                    <div class="CustomerRegisterLeftLabel">
                        [$ShippingCity]</div>
                    <div class="CustomerRegisterLeftData">
                        <asp:TextBox ID="uxShippingCity" runat="server" ValidationGroup="ValidShippingAddress"
                            CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxShippingCityRequired" runat="server" ControlToValidate="uxShippingCity"
                            ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping City
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="CustomerRegisterRight">
                    <div class="CustomerRegisterRightLabel">
                        [$ShippingZip]
                    </div>
                    <div class="CustomerRegisterRightData">
                        <asp:TextBox ID="uxShippingZip" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                        <span class="CommonAsterisk">*</span>
                        <asp:RequiredFieldValidator ID="uxShippingZipRequired" runat="server" ControlToValidate="uxShippingZip"
                            ValidationGroup="ValidShippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Zip
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <uc3:CountryState ID="uxShippingCountryState" runat="server" IsRequiredCountry="true"
                    IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="CustomerRegisterCityStatePanel"
                    CssLabel="CustomerRegisterLabel" />
                <div id="uxShippingCountryStateDiv" runat="server" class="CommonValidatorText CustomerRegisterPanelCountryValidatorText"
                    visible="false">
                    <div class="CommonValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxShippingCountryStateMessage" runat="server"></asp:Label>
                </div>
                <div class="CustomerRegisterLeft">
                    <div class="CustomerRegisterLeftLabel">
                        [$ShippingPhone]</div>
                    <div class="CustomerRegisterLeftData">
                        <asp:TextBox ID="uxShippingPhone" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="CustomerRegisterRight">
                    <div class="CustomerRegisterRightLabel">
                        [$ShippingFax]
                    </div>
                    <div class="CustomerRegisterRightData">
                        <asp:TextBox ID="uxShippingFax" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="CustomerRegisterShippingResidentialPanel">
            <div class="CommonFormLabel">
                [$ShippingResidential]</div>
            <div class="CommonFormData">
                <asp:DropDownList ID="uxShippingResidentialDrop" runat="server">
                    <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
                    <asp:ListItem Value="False">[$No]</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="Clear">
            </div>
        </asp:Panel>
        <div class="CustomerRegisterLinkButtonDiv">
            <asp:LinkButton ID="uxAddButton" runat="server" OnClick="uxAddButton_Click" ValidationGroup="ValidShippingAddress"
                CssClass="CustomerRegisterLinkButtonImage BtnStyle1" Text="[$BtnAddShippingAddress]" />
        </div>
        <div class="CustomerRegisterLinkButtonDiv">
            <asp:LinkButton ID="uxUpdateButton" runat="server" OnClick="uxUpdateButton_Click"
                ValidationGroup="ValidShippingAddress" CssClass="CustomerRegisterLinkButtonImage BtnStyle2"
                Text="[$BtnUpdateShippingAddress]" />
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
