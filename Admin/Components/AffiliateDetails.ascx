<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_AffiliateDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Common/StateList.ascx" TagName="StateList" TagPrefix="uc1" %>
<%@ Register Src="Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc4" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidAffiliate" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <span class="Asterisk">*</span>
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" />
        </div>
    </ValidationDenotesTemplate>
    <ButtonEventTemplate>
        <div id="uxLinkTR" runat="server">
            <vevo:AdvancedLinkButton ID="uxCommissionListLink" runat="server" meta:resourcekey="ViewCommission"
                OnClick="ChangePage_Click" StatusBarText="View Payments" CssClass="CommonAdminButtonIcon AdminButtonIconView fl">
            </vevo:AdvancedLinkButton>
            <vevo:AdvancedLinkButton ID="uxPaymentListLink" runat="server" meta:resourcekey="ViewPayment"
                OnClick="ChangePage_Click" StatusBarText="View Commissions" CssClass="CommonAdminButtonIcon AdminButtonIconView fl">
            </vevo:AdvancedLinkButton></div>
    </ButtonEventTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" />
                </div>
                <asp:TextBox ID="uxUserName" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uxUserName"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Username is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="PasswordTR" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcPassword" runat="server" meta:resourcekey="lcPassword" />
                </div>
                <asp:TextBox ID="uxPasswordText" runat="server" ValidationGroup="ValidAffiliate"
                    TextMode="Password" Width="150px" OnPreRender="uxPasswordText_PreRender" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxPasswordRequiredValid" runat="server" ControlToValidate="uxPasswordText"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Password is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxConfirmPasswordTR" runat="server" CssClass="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcConfirm" runat="server" meta:resourcekey="lcConfirm" />
                </div>
                <asp:TextBox ID="uxTextBoxConfrim" runat="server" TextMode="Password" Width="150px"
                    CssClass="TextBox" OnPreRender="uxTextBoxConfrim_PreRender"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredTexBoxConfirmValidator" runat="server"
                    ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText"
                    Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Password Confirm is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxCompareValidator" runat="server" ControlToCompare="uxPasswordText"
                    ControlToValidate="uxTextBoxConfrim" ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText"
                    Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Please enter the same password on both password fields
                    <div class="CommonValidateDiv">
                    </div>
                </asp:CompareValidator>
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcRegisterDate" runat="server" meta:resourcekey="lcRegisterDate" />
                </div>
                <uc4:CalendarPopup ID="uxCalendarPopup" runat="server" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcFirstName" runat="server" meta:resourcekey="lcFirstName" />
                </div>
                <asp:TextBox ID="uxFirstName" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="uxFirstName"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> First Name is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcLastName" runat="server" meta:resourcekey="lcLastName" />
                </div>
                <asp:TextBox ID="uxLastName" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredLastNameValidator" runat="server" ControlToValidate="uxLastName"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Last Name is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcWebSite" runat="server" meta:resourcekey="lcWebSite" />
                </div>
                <asp:TextBox ID="uxWebSite" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredWebSiteValidator" runat="server" ControlToValidate="uxWebSite"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Website is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularWebSiteValidator" runat="server" ValidationExpression="(?:w{3}(\.\w+)+|\w+(\.\w+)+)"
                    ControlToValidate="uxWebSite" ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText"
                    Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Website format.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RegularExpressionValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcCompany" runat="server" meta:resourcekey="lcCompany" />
                </div>
                <asp:TextBox ID="uxCompany" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcAddress" runat="server" meta:resourcekey="lcAddress" />
                </div>
                <asp:TextBox ID="uxAddress1" runat="server" Width="280px" ValidationGroup="ValidAffiliate"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredAddressValidator" runat="server" ControlToValidate="uxAddress1"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Address is required.
                    <div class="CommonValidateDiv CommonValidateDivAffiliateLong">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    &nbsp;</div>
                <asp:TextBox ID="uxAddress2" runat="server" Width="280px" CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity" />
                </div>
                <asp:TextBox ID="uxCity" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredCityValidator" runat="server" ControlToValidate="uxCity"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> City is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcCountry" runat="server" meta:resourcekey="lcCountry" />
                </div>
                <div class="CountrySelect fl">
                    <uc2:CountryList ID="uxCountryList" runat="server"></uc2:CountryList>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcState" runat="server" meta:resourcekey="lcState" />
                </div>
                <div class="CountrySelect fl">
                    <uc1:StateList ID="uxStateList" runat="server"></uc1:StateList>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcZip" runat="server" meta:resourcekey="lcZip" />
                </div>
                <asp:TextBox ID="uxZip" runat="server" ValidationGroup="ValidAffiliate" Width="150px"
                    CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredZipValidator" runat="server" ControlToValidate="uxZip"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Zip Cod is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcPhone" runat="server" meta:resourcekey="lcPhone" />
                </div>
                <asp:TextBox ID="uxPhone" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcFax" runat="server" meta:resourcekey="lcFax" />
                </div>
                <asp:TextBox ID="uxFax" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail" CssClass="Label" />
                <asp:TextBox ID="uxEmail" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxEmail"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="uxEmail" ValidationGroup="ValidAffiliate" Display="Dynamic"
                    CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email format.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RegularExpressionValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcCommission" runat="server" meta:resourcekey="lcCommission" />
                </div>
                <asp:TextBox ID="uxCommissionText" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxCommissionTextRequired" runat="server" ControlToValidate="uxCommissionText"
                    ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText" Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Commission Rate is required.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxCommissionCompare" runat="server" ControlToValidate="uxCommissionText"
                    Operator="DataTypeCheck" Type="Double" ValidationGroup="ValidAffiliate" CssClass="CommonValidatorText"
                    Display="Dynamic">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Commission Rate is invalid.
                    <div class="CommonValidateDiv">
                    </div>
                </asp:CompareValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcEnabled" meta:resourcekey="lcIsEnabled" runat="server" />
                </div>
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked="true" OnCheckedChanged="uxIsEnabledCheck_CheckedChanged"
                    AutoPostBack="true" />
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="ValidAffiliate">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidAffiliate">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxAddSendMailButton" runat="server" meta:resourcekey="uxAddSendMailButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddSendMailButton_Click" OnClickGoTo="Top" ValidationGroup="ValidAffiliate" />
                <vevo:AdvanceButton ID="uxUpdateSendMailButton" runat="server" meta:resourcekey="uxSendMailButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateSendMailButton_Click" OnClickGoTo="Top" ValidationGroup="ValidAffiliate" />
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminUserControlContent>
