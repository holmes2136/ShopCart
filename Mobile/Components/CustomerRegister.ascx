<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRegister.ascx.cs"
    Inherits="Mobile_Components_CustomerRegister" %>
<%@ Register Src="~/Components/CountryAndStateList.ascx" TagName="CountryState" TagPrefix="uc3" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Register Src="GiftCouponDetail.ascx" TagName="GiftCouponDetail" TagPrefix="uc4" %>
<%@ Register Src="MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<div class="MobileTitle">
    [$RegisterTitle]
</div>
<uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
<div class="MobileCommonBox">
    <div class="MobileLoginNote">
        [$Required Fields]
    </div>
    <div class="MobileUserLoginControl">
        <div id="uxUsernameValidDIV" class="MobileCommonValidateText" runat="server">
            <asp:Literal ID="uxUsernameLiteral" runat="server"></asp:Literal>
        </div>
        <asp:Panel ID="uxRegisterPanel" runat="server" CssClass="MobileUserLoginControlPanel">
            <div class="MobileCommonPageInnerTitle">
                [$Register]</div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$FirstName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required First Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$LastName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Last Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$UserName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredUserNameValidator" runat="server" ControlToValidate="uxUserName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required UserName
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Password]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxPassword" runat="server" TextMode="Password" ValidationGroup="ValidRegister"
                        CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredPasswordValidator" runat="server" ControlToValidate="uxPassword"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Password
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Confirm]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxTextBoxConfrim" runat="server" TextMode="Password" ValidationGroup="ValidRegister"
                        CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredTexBoxConfirmValidator" runat="server"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Confirm Password
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="uxCompareValidator" runat="server" ControlToCompare="uxPassword"
                        ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Please enter the same password on both password fields
                    </asp:CompareValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Email]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxEmail" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Email Address
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxEmail" ValidationGroup="ValidRegister" Display="Dynamic"
                        CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                    </asp:RegularExpressionValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxRegisAddressPanel" runat="server" CssClass="MobileUserLoginControlPanel">
            <div class="MobileCommonPageInnerTitle">
                [$AddressInfo]</div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Company]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxCompany" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Address]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxAddress1" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Address
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Address2]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxAddress2" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$City]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidRegister" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required City
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Zipcode]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidRegister" MaxLength="9"
                        CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Zip
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <uc3:CountryState ID="uxCountryState" runat="server" IsRequiredCountry="true" IsRequiredState="true"
                    IsCountryWithOther="true" IsStateWithOther="true" CssLabel="MobileCommonFormLabel" />
                <div id="uxBillingCountryStateDiv" runat="server" class="MobileCommonValidatorText CustomerRegisterPanelCountryValidatorText"
                    visible="false">
                    <div class="MobileCommonValidateDiv CountryAndStateValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxBillingCountryStateMessage" runat="server"></asp:Label>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Phone]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxPhone" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$Fax]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxFax" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileCustomerRegisterCheckBox MobileUserLoginControl">
                <asp:CheckBox ID="uxSubscribeCheckBox" runat="server" ValidationGroup="ValidRegister"
                    Text=" [$SubScribe]" Checked="true" CssClass="MobileCustomerRegisterCheckBox" />
            </div>
            <div class="MobileCustomerRegisterCheckBox MobileUserLoginControl">
                <asp:Panel ID="uxUseBillingAsShippingPanel" runat="server">
                    <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" ValidationGroup="ValidRegister"
                        Text=" [$UseBill]" CssClass="MobileCustomerRegisterCheckBox" Checked="true" /></asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxShippingInfoPanel" runat="server" CssClass="MobileUserLoginControlPanel">
            <div class="MobileCommonPageInnerTitle">
                [$ShippingInfo]</div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippFName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingFirstName" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingFirstNameRequired" runat="server" ControlToValidate="uxShippingFirstName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping First Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippLName]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingLastName" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingLastNameRequired" runat="server" ControlToValidate="uxShippingLastName"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Last Name
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingCompany]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingCompany" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingAddress]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingAddress1" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingAddress1Required" runat="server" ControlToValidate="uxShippingAddress1"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Address
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingAddress2]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingAddress2" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterLongTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingCity]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingCity" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingCityRequired" runat="server" ControlToValidate="uxShippingCity"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping City
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingZip]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingZip" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxShippingZipRequired" runat="server" ControlToValidate="uxShippingZip"
                        ValidationGroup="ValidRegister" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Shipping Zip
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <uc3:CountryState ID="uxShippingCountryState" runat="server" IsRequiredCountry="true"
                    IsRequiredState="true" IsCountryWithOther="true" IsStateWithOther="true" CssPanel="MobileCustomerRegisterCityStatePanel"
                    CssLabel="MobileCommonFormLabel" />
                <div id="uxShippingCountryStateDiv" runat="server" class="MobileCommonValidatorText CustomerRegisterPanelCountryValidatorText"
                    visible="false">
                    <div class="MobileCommonValidateDiv CountryAndStateValidateDiv">
                    </div>
                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                    <asp:Label ID="uxShippingCountryStateMessage" runat="server"></asp:Label>
                </div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingPhone]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingPhone" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
            </div>
            <div class="MobileUserLoginControl">
                <div class="MobileCommonFormLabel">
                    [$ShippingFax]</div>
                <div class="MobileCommonFormData">
                    <asp:TextBox ID="uxShippingFax" runat="server" CssClass="MobileCommonTextBox MobileCustomerRegisterTextBox"></asp:TextBox></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxShippingResidentialPanel" runat="server" CssClass="MobileUserLoginControlPanel">
            <div class="MobileCommonFormLabel">
                [$ShippingResidential]</div>
            <div class="MobileCommonFormData">
                <asp:DropDownList ID="uxShippingResidentialDrop" runat="server">
                    <asp:ListItem Value="True" Selected="True">[$Yes]</asp:ListItem>
                    <asp:ListItem Value="False">[$No]</asp:ListItem>
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <div class="MobileUserLoginControlPanel">
            <asp:Button ID="uxAddButton" runat="server" Text="Register" OnClick="uxAddButton_Click"
                Visible="False" ValidationGroup="ValidRegister" CssClass="MobileCustomerRegisterAddButton" />
        </div>
        <asp:HiddenField ID="uxMessageHidden" runat="server" />
        <uc4:GiftCouponDetail ID="uxGiftCouponDetail" runat="server" />
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxLinkButton" runat="server" Text="[$RegisterButton]" ValidationGroup="ValidRegister"
                OnClick="uxAddButton_Click" CssClass="MobileButton" />
        </div>
    </div>
</div>
