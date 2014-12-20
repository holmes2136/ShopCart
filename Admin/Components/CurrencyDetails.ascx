<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_CurrencyDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<uc2:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server"></uc3:Message>
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="CurrencyDetails" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxCurrencyLink" runat="server" PageName="CurrencyList.ascx"
            CssClass="CommonAdminButtonIcon AdminButtonIconView fl" meta:resourcekey="lcCurrencyLink"
            OnClick="ChangePage_Click" StatusBarText="Currency List" />
    </ButtonEventTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <span class="Asterisk">*</span>
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="lcCurrencyCodeText" runat="server" meta:resourcekey="lcCurrencyCode"
                    CssClass="Label" />
                <asp:TextBox ID="uxCurrencyCodeText" runat="server" MaxLength="10" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxCurrencyCodeRequiredValid" runat="server" ControlToValidate="uxCurrencyCodeText"
                    Display="Dynamic" ValidationGroup="CurrencyDetails" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Currency Code is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcSymbolText" runat="server" meta:resourcekey="lcSymbol" CssClass="Label" />
                <asp:TextBox ID="uxSymbolText" runat="server" MaxLength="10" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxSymbolRequiredValid" runat="server" ControlToValidate="uxSymbolText"
                    Display="Dynamic" ValidationGroup="CurrencyDetails" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Currency Symbol is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" CssClass="Label" />
                <asp:TextBox ID="uxNameText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequireNameValid" runat="server" ControlToValidate="uxNameText"
                    Display="Dynamic" ValidationGroup="CurrencyDetails" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Name is required.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcConversionRate" runat="server" meta:resourcekey="lcConversionRate"
                    CssClass="Label" />
                <asp:TextBox ID="uxConversionText" runat="server" CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequireConversionRateValid" runat="server" ControlToValidate="uxConversionText"
                    Display="Dynamic" ValidationGroup="CurrencyDetails" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Conversion Rate is requiered.
                            <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                            </div>
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="uxRequireConversionRateCompare" runat="server"
                    ControlToValidate="uxConversionText" Operator="DataTypeCheck"
                    Type="Double" Display="Dynamic" ValidationGroup="CurrencyDetails" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Conversion Rate is invalid.
                    <div class="CommonValidateDiv CommonValidateDivProductOptionDetails">
                    </div>
                </asp:CompareValidator>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcCurrencyPosition" runat="server" meta:resourcekey="lcCurrencyPosition"
                    CssClass="Label" />
                <asp:DropDownList ID="uxCurrencyPositionDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="Before" Text="Before the Price" />
                    <asp:ListItem Value="After" Text="After the Price" />
                </asp:DropDownList>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcEnabled" runat="server" meta:resourcekey="lcEnabled" CssClass="Label" />
                <asp:CheckBox ID="uxIsEnabledCheck" runat="server" CssClass="fl" />
            </div>
            <div class="CommonRowStyle CurrencyWarning" id="uxEditWarningDiv" runat="server"
                visible="false">
                <asp:Literal ID="lcEditWarning" runat="server" meta:resourcekey="lcEditWarning"></asp:Literal>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="lcAddCurrency"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="CurrencyDetails" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="lcEditCurrency"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="CurrencyDetails" />
                <div class="Clear" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminUserControlContent>
