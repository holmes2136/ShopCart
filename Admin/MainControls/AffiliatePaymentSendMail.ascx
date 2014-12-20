<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePaymentSendMail.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliatePaymentSendMail" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcSendMail %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="AffiliateSendMailValid" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ContentTemplate>
        <div class="Container-Row">
            <div class="CommonRowStyle">
                <div class="Label">
                    From :
                </div>
                <div class="label fl">
                    <asp:Label ID="uxFromLabel" runat="server" />
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    To :
                </div>
                <asp:TextBox ID="uxToText" runat="server" ValidationGroup="AffiliateSendMailValid"
                    MaxLength="50" Width="252px" CssClass="TextBox fl"></asp:TextBox>
                <div class="validator1 fl mgl5">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxToRequiredValidator" runat="server" ControlToValidate="uxToText"
                    ValidationGroup="AffiliateSendMailValid" Display="Dynamic" CssClass="CommonValidatorText" >
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Recipient is required.
                    <div class="CommonValidateDiv CommonValidateDivAffiliateSendMailLong">
                    </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="uxToText"
                    ValidationGroup="AffiliateSendMailValid" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong email format.
                    <div class="CommonValidateDiv CommonValidateDivAffiliateSendMailLong">
                    </div>
                </asp:RegularExpressionValidator>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    Subject :
                </div>
                <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="AffiliateSendMailValid"
                    MaxLength="255" Width="252px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl mgl5">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxSubjectRequiredFieldValidator" runat="server" ControlToValidate="uxSubjectText"
                    ValidationGroup="AffiliateSendMailValid" Display="Dynamic" CssClass="CommonValidatorText">
                    <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Subject is required.
                    <div class="CommonValidateDiv CommonValidateDivAffiliateSendMailLong">
                    </div>
                </asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    Body :</div>
                <uc6:TextEditor ID="uxEmailBodyText" runat="server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
                <div class="Clear">
                </div>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxSendButton" runat="server" meta:resourcekey="uxSendButton"
                    CssClassBegin="fr mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSendButton_Click"
                    OnClickGoTo="Top" ValidationGroup="AffiliateSendMailValid"></vevo:AdvanceButton>
            </div>
            <div class="Clear" />
        </div>
    </ContentTemplate>
</uc1:AdminContent>
