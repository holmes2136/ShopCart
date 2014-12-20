<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_PaymentDetails" %>
<%@ Register Src="CurrencySelector.ascx" TagName="CurrencySelector" TagPrefix="uc2" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="Template/AdminContent.ascx" TagName="AdminUserControlContent" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<uc2:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="PaymentValidation" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
            <asp:Panel ID="uxPermissionWarningPanel" runat="server" CssClass="PaymentDetailsPermissionWarning">
                <strong>Warning:</strong> Please verify that your <strong>'App_Data\System'</strong>
                folder has 'Write' and 'Modify' permission for necessary ASP.NET task (ASPNET or
                NETWORK SERVICE).
            </asp:Panel>
            <div class="RequiredLabel c6">
                <span class="Asterisk">*</span>
                <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
            <asp:Label ID="lcFieldMutipleLanguage" runat="server" meta:resourcekey="lcFieldMutipleLanguage"
                CssClass="Label" /></div>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ContentTemplate>
        <div class="PaymentDetailTopLabel">
            <asp:Label ID="uxPaymentNameLabel" runat="server" />
        </div>
        <div class="PaymentSetCurrencyUnit c5">
            <div class="c5 fb">
                <asp:Label ID="lcCurrencyHeader" runat="server" meta:resourcekey="lcCurrencyHeader" />
            </div>
            <div class="c5">
                <asp:Label ID="lcCurrencyDescription" runat="server" meta:resourcekey="lcCurrencyDescription" /></div>
            <div class="mgt5">
                <uc2:CurrencySelector ID="uxCurrencySelector" runat="server" />
            </div>
            <div class="Clear">
            </div>
        </div>
        <div class="Container-Box">
            <div>
                <div class="PaymentCurrencyDescription">
                    <asp:Label ID="uxCurrencyDescription2" runat="server" meta:resourcekey="lcCurrencyDescription2" /></div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle mgt20">
                <asp:Label ID="lcIsEnabled" runat="server" meta:resourcekey="lcIsEnabled" CssClass="Label" />
                <asp:DropDownList ID="uxIsEnabledDrop" runat="server" AutoPostBack="true" CssClass="DropDown fl">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Panel ID="uxLanguageDependentPlaceHolder" runat="server">
                <div class="CommonRowStyle">
                    <asp:Label ID="lcDisplayName" runat="server" meta:resourcekey="lcDisplayName" CssClass="Label" />
                    <asp:TextBox ID="uxDisplayNameText" runat="server" Width="270px" ValidationGroup="PaymentValidation"
                        CssClass="TextBox" />
                    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
                    <div class="validator1 fl">
                        <div class="validator1 fl">
                            <span class="Asterisk">*</span>
                        </div>
                        <asp:RequiredFieldValidator ID="uxDisplayNameRequiredValidator" runat="server" ControlToValidate="uxDisplayNameText"
                            Display="Dynamic" ValidationGroup="PaymentValidation" CssClass="CommonValidatorText">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Display name is required.
                            <div class="CommonValidateDiv CommonValidateDivPaymentDisplayNameLong">
                            </div>
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="CommonRowStyle">
                    <asp:Label ID="lcDescription" runat="server" meta:resourcekey="lcDescription" CssClass="Label" />
                    <asp:TextBox ID="uxDescriptionText" runat="server" Height="70px" TextMode="MultiLine"
                        Width="270px" CssClass="TextBox" />
                    <uc5:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxImageTR" runat="server">
                <div class="CommonRowStyle">
                    <asp:Label ID="lcPaymentImage" runat="server" meta:resourcekey="lcPaymentImage" CssClass="Label" />
                    <asp:TextBox ID="uxPaymentImageText" runat="server" Width="250px" CssClass="TextBox" />
                    <asp:LinkButton ID="uxPaymentImageLinkButton" runat="server" OnClick="uxPaymentImageLinkButton_Click"
                        CssClass="fl mgl5">Upload...</asp:LinkButton></div>
            </asp:Panel>
            <uc6:Upload ID="uxPaymentImageUpload" runat="server" ShowControl="false" CssClass="CommonRowStyle"
                CheckType="Image" PathDestination="Images/Gateway/" ButtonImage="SelectImages.png"
                ButtonWidth="105" ButtonHeight="22" ShowText="false" />
            <asp:Panel ID="uxPaymentPanel" runat="server">
            </asp:Panel>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="PaymentValidation" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminUserControlContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
