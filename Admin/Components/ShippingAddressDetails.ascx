<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingAddressDetails.ascx.cs"
    Inherits="Admin_Components_ShippingAddressDetails" %>
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
            ValidationGroup="ValidShippingAddress" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <asp:Panel ID="uxPanelBillingAsShipping" runat="server" CssClass="mgt10">
                <div class="CommonRowStyle fb" id="uxUseBillingAsShippingPanel" runat="server">
                    <asp:CheckBox ID="uxUseBillingAsShipping" runat="server" AutoPostBack="True" OnCheckedChanged="uxUseBillingAsShipping_CheckedChanged"
                        meta:resourcekey="uxUseBillingAsShipping" CssClass="CheckBox mgl10" />
                    <div class="Clear">
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingAliasName" runat="server" meta:resourcekey="lcAliasName"
                        CssClass="Label" />
                    <asp:TextBox ID="uxShippingAliasName" runat="server" ValidationGroup="ValidShippingAddress"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingAliasNameValidator" runat="server"
                        ControlToValidate="uxShippingAliasName" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Alias name is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"
                        CssClass="Label" />
                    <asp:TextBox ID="uxShippingFirstName" runat="server" ValidationGroup="ValidShippingAddress"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingFirstnameValidator" runat="server"
                        ControlToValidate="uxShippingFirstName" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping First Name is required.
                        <div class="CommonValidateDiv">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                    <asp:TextBox ID="uxShippingLastName" runat="server" ValidationGroup="ValidShippingAddress"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingLastNameValidator" runat="server"
                        ControlToValidate="uxShippingLastName" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Last Name is required.
                        <div class="CommonValidateDiv">
                        </div>
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
                    <asp:TextBox ID="uxShippingAddress1" runat="server" Width="300px" ValidationGroup="ValidShippingAddress"
                        CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingAddressValidator" runat="server"
                        ControlToValidate="uxShippingAddress1" Display="Dynamic" CssClass="CommonValidatorText"
                        ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping address is required.
                        <div class="CommonValidateDiv CommonValidateDivShippingAddressLong">
                        </div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="Label1" runat="server" CssClass="Label">&nbsp;</asp:Label>
                    <asp:TextBox ID="uxShippingAddress2" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                    <asp:TextBox ID="uxShippingCity" runat="server" ValidationGroup="ValidShippingAddress"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingCityValidator" runat="server" ControlToValidate="uxShippingCity"
                        Display="Dynamic" CssClass="CommonValidatorText" ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping city is required.
                        <div class="CommonValidateDiv">
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
                    <asp:TextBox ID="uxShippingZip" runat="server" ValidationGroup="ValidShippingAddress"
                        Width="150px" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span class="Asterisk">*</span>
                    </div>
                    <asp:RequiredFieldValidator ID="uxRequiredShippingZipValidator" runat="server" ControlToValidate="uxShippingZip"
                        Display="Dynamic" CssClass="CommonValidatorText" ValidationGroup="ValidShippingAddress">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Shipping Zip Code is required.
                        <div class="CommonValidateDiv">
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
            <div class="mgt10">
                <div class="CommonRowStyle">
                    <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                        CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxAddButton_Click"
                        OnClickGoTo="Top" ValidationGroup="ValidShippingAddress"></vevo:AdvanceButton>
                    <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                        CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                        OnClickGoTo="Top" ValidationGroup="ValidShippingAddress"></vevo:AdvanceButton>
                </div>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
