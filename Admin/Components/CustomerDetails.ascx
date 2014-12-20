<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_CustomerDetails" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Common/StateList.ascx" TagName="StateList" TagPrefix="uc1" %>
<%@ Register Src="Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
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
            ValidationGroup="ValidCustomer" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonTextTitle1 mgt0 mgb10">
                <asp:Label ID="lcBillingDetails" runat="server" meta:resourcekey="lcBilling" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" CssClass="Label" />
                <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredUserNameValidator" runat="server" ControlToValidate="uxUserName"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Username is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <asp:Panel ID="PaasswordTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" CssClass="Label" />
                <asp:TextBox ID="uxPasswordText" runat="server" ValidationGroup="ValidCustomer" TextMode="Password"
                    Width="150px" OnPreRender="uxPasswordText_PreRender" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxPasswordRequiredValid" runat="server" ControlToValidate="uxPasswordText"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Password is required.
                    <div class="CommonValidateDiv"></div>
                </asp:RequiredFieldValidator>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcFirstName" runat="server" meta:resourcekey="lcFirstName" CssClass="Label" />
                <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> First Name is required.
                    <div class="CommonValidateDiv"></div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Last Name is required.
                    <div class="CommonValidateDiv"></div>
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
                <asp:TextBox ID="uxAddress1" runat="server" Width="300px" ValidationGroup="ValidCustomer"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Address is required.
                    <div class="CommonValidateDiv CommonValidateDivCustomerLong"></div>
                </asp:RequiredFieldValidator></div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxBlankLabel" runat="server" CssClass="Label">&nbsp;</asp:Label>
                <asp:TextBox ID="uxAddress2" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> City is required.
                    <div class="CommonValidateDiv"></div>
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
                <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Code Zip is required.
                    <div class="CommonValidateDiv"></div>
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
                    ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
                    <div class="CommonValidateDiv"></div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="uxEmail" ValidationGroup="ValidCustomer" Display="Dynamic"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email format.
                    <div class="CommonValidateDiv"></div>
                </asp:RegularExpressionValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcRegisterDate" runat="server" meta:resourcekey="lcRegisterDate" CssClass="Label" />
                <uc4:CalendarPopup ID="uxRegisterDateCalendarPopup" runat="server" TextBoxEnabled="false" />
            </div>
            <asp:Panel ID="IsWholesaleTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="lcIsWholesaleLabel" runat="server" meta:resourcekey="lcIsWholesale"
                    CssClass="Label" />
                <asp:CheckBox ID="uxIsWholesaleCheck" runat="server" OnCheckedChanged="uxIsWholesaleCheck_CheckedChanged"
                    AutoPostBack="true" CssClass="fl CheckBox" />
            </asp:Panel>
            <asp:Panel ID="WholesaleLevelsTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxWholesaleLevelsLabel" runat="server" meta:resourcekey="lcWholesaleLevels"
                    CssClass="Label" />
                <asp:DropDownList ID="uxWholesaleLevelsDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" CssClass="CheckBox" Checked="true"
                    OnCheckedChanged="uxIsEnabledCheck_CheckedChanged" AutoPostBack="true" />
            </div>
            <div class="CommonRowStyle fb" id="uxUseBillingAsShippingPanel" runat="server">
                <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" AutoPostBack="True" OnCheckedChanged="uxUseBillingAsShipping_CheckedChanged"
                    meta:resourcekey="uxUseBillingAsShipping" CssClass="CheckBox mgl10" />
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxPanelBillingAsShipping" runat="server" CssClass="mgt10">
                <div class="CommonTextTitle1 mgt20 mgb10">
                    <asp:Label ID="lcShipping" runat="server" meta:resourcekey="lcShipping" />
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"
                        CssClass="Label" />
                    <asp:TextBox ID="uxShippingFirstName" runat="server" ValidationGroup="ValidCustomer"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingFirstnameValidator" runat="server"
                        ControlToValidate="uxShippingFirstName" ValidationGroup="ValidCustomer" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping First Name is required.
                        <div class="CommonValidateDiv"></div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                    <asp:TextBox ID="uxShippingLastName" runat="server" ValidationGroup="ValidCustomer"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingLastNameValidator" runat="server"
                        ControlToValidate="uxShippingLastName" ValidationGroup="ValidCustomer" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Last Name is required.
                        <div class="CommonValidateDiv"></div>
                    </asp:RequiredFieldValidator>
                </div>
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
                    <asp:TextBox ID="uxShippingAddress1" runat="server" Width="300px" ValidationGroup="ValidCustomer"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingAddressValidator" runat="server"
                        ControlToValidate="uxShippingAddress1" ValidationGroup="ValidCustomer" Display="Dynamic"
                        CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping address is required.
                        <div class="CommonValidateDiv CommonValidateDivCustomerLong"></div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="Label1" runat="server" CssClass="Label">&nbsp;</asp:Label>
                    <asp:TextBox ID="uxShippingAddress2" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                    <asp:TextBox ID="uxShippingCity" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingCityValidator" runat="server" ControlToValidate="uxShippingCity"
                        ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping city is required.
                        <div class="CommonValidateDiv"></div>
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
                    <asp:TextBox ID="uxShippingZip" runat="server" ValidationGroup="ValidCustomer" Width="150px"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingZipValidator" runat="server" ControlToValidate="uxShippingZip"
                        ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Zip Code is required.
                        <div class="CommonValidateDiv"></div>
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
            <div class="mgt10" id="uxStoreListDiv" runat="server">
                <div class="CommonTextTitle1 mgb10">
                    Enable for Stores
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxStoreListLabel" runat="server" meta:resourcekey="uxStoreListLabel"
                        CssClass="Label" />
                    <asp:GridView ID="uxStoreGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                        DataKeyNames="StoreID" Width="360px">
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:TemplateField HeaderText="Enable">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="uxEnableStoreCheck" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="StoreName" HeaderText="Store">
                                <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="mgt10">
                <div class="CommonTextTitle1 mgb10">
                    Miscellaneous
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="uxIsTaxExemptLabel" runat="server" meta:resourcekey="uxIsTaxExemptLabel"
                        CssClass="Label" />
                    <asp:CheckBox ID="uxIsTaxExemptCheck" runat="server" CssClass="CheckBox" Checked="false"
                        OnCheckedChanged="uxIsTaxExemptCheck_CheckedChanged" AutoPostBack="true" />
                </div>
                <asp:Panel ID="uxCustomerTaxExemptPanel" runat="server">
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxTaxExemptIDLabel" runat="server" meta:resourcekey="uxTaxExemptIDLabel"
                            CssClass="Label" />
                        <asp:TextBox ID="uxTaxExemptID" runat="server" Width="150px" CssClass="fl TextBox"></asp:TextBox>
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxRequiredTaxExemptIDValidator" runat="server" ControlToValidate="uxTaxExemptID"
                            ValidationGroup="ValidCustomer" Display="Dynamic" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Tax Exempt ID is required.
                            <div class="CommonValidateDiv"></div>
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
                    <asp:Label ID="lcMerchantNotes" runat="server" meta:resourcekey="lcMerchantNotes"
                        CssClass="Label" />
                    <asp:TextBox ID="uxMerchantNotes" runat="server" Width="300px" Height="80px" TextMode="MultiLine"
                        CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="Clear" />
                <div class="mgt20">
                    <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                        CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidCustomer" />
                    <vevo:AdvanceButton ID="uxAddSendMailButton" runat="server" meta:resourcekey="uxAddSendMailButton"
                        CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        OnClick="uxAddSendMailButton_Click" OnClickGoTo="Top" ValidationGroup="ValidCustomer" />
                    <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                        CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidCustomer" />
                    <vevo:AdvanceButton ID="uxUpdateSendMailButton" runat="server" meta:resourcekey="uxUpdateSendMailButton"
                        CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        OnClick="uxUpdateSendMailButton_Click" OnClickGoTo="Top" ValidationGroup="ValidCustomer"
                        Visible="false" />
                    <div class="Clear" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
