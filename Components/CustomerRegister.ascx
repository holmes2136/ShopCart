<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRegister.ascx.cs"
    Inherits="Components_CustomerRegister" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryState" TagPrefix="uc3" %>
<%@ Register Src="VevoHyperLink.ascx" TagName="VevoHyperLink" TagPrefix="ucHyperLink" %>
<div class="CustomerRegister">
    <div class="CustomerRegisterNote">
        [$Required Fields]
    </div>
    <div class="CustomerRegisterDiv">
        <div id="uxUsernameValidDIV" class="CommonValidateText" runat="server">
            <asp:Literal ID="uxUsernameLiteral" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="uxRegisterPanel" runat="server" CssClass="CustomerRegisterPanel">
            <div class="CommonPageInnerTitle">
                [$Register]</div>
            <div class="CommonFormLabel">
                <ucHyperLink:VevoHyperLink ID="uxFacebookLink" runat="server" ThemeImageUrl="[$ImgFacebookConnect]"
                    Visible="false" />
            </div>
            <div class="CommonFormData">
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$FirstName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required First Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$LastName]
                </div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Last Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$UserName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredUserNameValidator" runat="server" ControlToValidate="uxUserName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required UserName
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Email]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxEmail" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Email Address
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxEmail" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                    </asp:RegularExpressionValidator></div>
            </div>
            <asp:HiddenField ID="uxFacebookID" runat="server" />
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Password]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxPassword" runat="server" TextMode="Password" ValidationGroup="ValidRegister"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredPasswordValidator" runat="server" ControlToValidate="uxPassword"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Password
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Confirm]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxTextBoxConfrim" runat="server" TextMode="Password" ValidationGroup="ValidRegister"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredTexBoxConfirmValidator" runat="server"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Confirm Password
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxCompareValidator" runat="server" ControlToCompare="uxPassword"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please enter the same password on both password fields
                    </asp:CompareValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxRegisAddressPanel" runat="server" CssClass="CustomerRegisterPanel">
            <div class="CommonPageInnerTitle">
                [$AddressInfo]</div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Company]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxCompany" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                </div>
            </div>
            <div class="CustomerRegisterRight">
                &nbsp;
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Address]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxAddress1" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Address
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Address2]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxAddress2" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$City]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidRegister" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Zipcode]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidRegister" MaxLength="9"
                        CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
                    </asp:RequiredFieldValidator></div>
            </div>
            <uc3:CountryState ID="uxCountryState" runat="server" IsRequiredCountry="true" IsRequiredState="true"
                IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
                CssLabel="CustomerRegisterLabel" />
            <div id="uxBillingCountryStateDiv" runat="server" class="CommonValidatorText CustomerRegisterPanelCountryValidatorText"
                visible="false">
                <div class="CommonValidateDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxBillingCountryStateMessage" runat="server"></asp:Label>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$Phone]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxPhone" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$Fax]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxFax" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterLeftLabel2">
                &nbsp;</div>
            <asp:CheckBox ID="uxSubscribeCheckBox" runat="server" ValidationGroup="ValidRegister"
                Text=" [$SubScribe]" Checked="true" CssClass="CustomerRegisterCheckBox" />
            <asp:Panel ID="uxUseBillingAsShippingPanel" runat="server" CssClass="CustomerRegisterUseBillingAsShippingPanel">
                <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" ValidationGroup="ValidRegister"
                    Text=" [$UseBill]" CssClass="CustomerRegisterCheckBox UseBillingCheckbox" Checked="true" />
                <div class="Clear">
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="CustomerRegisterShippingInfoPanel">
            <div class="CommonPageInnerTitle">
                [$ShippingInfo]</div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$ShippFName]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxShippingFirstName" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingFirstNameRequired" runat="server" ControlToValidate="uxShippingFirstName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping First Name
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$ShippLName]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxShippingLastName" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingLastNameRequired" runat="server" ControlToValidate="uxShippingLastName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Last Name
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$ShippingCompany]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxShippingCompany" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterRight">
                &nbsp;
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$ShippingAddress]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxShippingAddress1" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingAddress1Required" runat="server" ControlToValidate="uxShippingAddress1"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Address
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$ShippingAddress2]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxShippingAddress2" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterLeft">
                <div class="CustomerRegisterLeftLabel">
                    [$ShippingCity]</div>
                <div class="CustomerRegisterLeftData">
                    <asp:TextBox ID="uxShippingCity" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingCityRequired" runat="server" ControlToValidate="uxShippingCity"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping City
                    </asp:RequiredFieldValidator></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$ShippingZip]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxShippingZip" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox>
                    <span class="CommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingZipRequired" runat="server" ControlToValidate="uxShippingZip"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Zip
                    </asp:RequiredFieldValidator></div>
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
                    <asp:TextBox ID="uxShippingPhone" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="CustomerRegisterRight">
                <div class="CustomerRegisterRightLabel">
                    [$ShippingFax]</div>
                <div class="CustomerRegisterRightData">
                    <asp:TextBox ID="uxShippingFax" runat="server" CssClass="CommonTextBox CustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="Clear">
            </div>
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
        <asp:Panel ID="uxSaleTaxExemptPanel" runat="server" CssClass="CustomerRegisterShippingInfoPanel">
            <div class="CommonFormLabel">
            </div>
            <div class="CommonFormData">
                <asp:CheckBox ID="uxTaxExemptCheck" runat="server" Text="[$CustomerTaxExempt]" ValidationGroup="ValidRegister"
                    CssClass="CustomerRegisterCheckBox" />
            </div>
            <asp:Panel ID="uxCustomerTaxExemptPanel" runat="server" CssClass="CustomerTaxExemptPanel">
                <div class="CommonFormLabel">
                    [$TaxExemptID]:</div>
                <div class="CommonFormData">
                    <asp:TextBox ID="uxTaxExemptID" runat="server" Width="150px" CssClass="CheckoutTextBox">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="uxTaxExemptIDRequired" runat="server" ControlToValidate="uxTaxExemptID"
                        ValidationGroup="shippingAddress" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Tax Exempt ID
                    </asp:RequiredFieldValidator>
                </div>
                <uc3:CountryState ID="uxTaxExemptCountryAndState" runat="server" IsRequiredCountry="true"
                    IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="ClientCityStatePanel"
                    CssLabel="CommonFormLabel" />
                <div id="uxTaxExemptCountryStateDiv" runat="server" class="CommonValidatorText CustomerRegisterPanelCountryValidatorText"
                    visible="false">
                    <div class="CommonValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxTaxExemptCountryStateMessage" runat="server"></asp:Label>
                </div>
                <div class="Clear">
                </div>
            </asp:Panel>
        </asp:Panel>
        <div class="CustomerRegisterAddButtonDiv">
            <asp:Button ID="uxAddButton" runat="server" Text="Register" OnClick="uxAddButton_Click"
                Visible="False" ValidationGroup="ValidRegister" CssClass="CustomerRegisterAddButton" />
        </div>
        <asp:HiddenField ID="uxMessageHidden" runat="server" />
        <div class="CustomerRegisterLinkButtonDiv">
            <asp:LinkButton ID="uxLinkButton" runat="server" OnClick="uxAddButton_Click" ValidationGroup="ValidRegister"
                CssClass="CustomerRegisterLinkButtonImage BtnStyle1" Text="[$BtnRegister]" />
        </div>
        <div class="Clear">
        </div>
    </div>
</div>
