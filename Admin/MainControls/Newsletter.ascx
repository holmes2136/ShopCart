<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Newsletter.ascx.cs" Inherits="AdminAdvanced_MainControls_Newsletter" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="../Components/StoreDropDownList.ascx" TagName="StoreDropDownList"
    TagPrefix="uc10" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server"></uc1:Message>
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="NewsletterValid" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxNewsletterManagerLink" runat="server" CssClass="CommonAdminButtonIcon AdminButtonIconView fl"
            PageName="NewsletterManager.ascx" StatusBarText="Newsletter Email List" OnClick="ChangePage_Click"
            OnClickGoTo="None" meta:resourcekey="uxNewsletterManagerLink" /></ButtonEventTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="OrderEditRowTitle">
                You have
                <asp:Label ID="uxSubscribersLabel" runat="server"></asp:Label>
                newsletter subscribers.
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    From :</div>
                <asp:TextBox ID="uxFromText" runat="server" ValidationGroup="NewsletterValid" MaxLength="50"
                    Width="252px" CssClass="fl TextBox">
                </asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxFromText"
                    ValidationGroup="NewsletterValid" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Email is required.
                            <div class="CommonValidateDiv CommonValidateDivNewsletterLong">
                            </div>
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="uxFromText" ValidationGroup="NewsletterValid" CssClass="CommonValidatorText"
                    Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Wrong Email Format.
                            <div class="CommonValidateDiv CommonValidateDivNewsletterLong">
                            </div>
                </asp:RegularExpressionValidator>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    Subject :</div>
                <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="NewsletterValid"
                    MaxLength="255" Width="252px" CssClass="fl TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxSubjectRequiredFieldValidator" runat="server" ControlToValidate="uxSubjectText"
                    ValidationGroup="NewsletterValid" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Subject is required.
                            <div class="CommonValidateDiv CommonValidateDivNewsletterLong">
                            </div>
                </asp:RequiredFieldValidator>
            </div>
            <div id="uxStoreListLabel" class="CommonRowStyle" runat="server">
                <div class="Label">
                    Store :</div>
                <div class="fl">
                    <uc10:StoreDropDownList ID="uxStoreList" runat="server" AutoPostBack="True" OnBubbleEvent="uxStoreList_RefreshHandler"
                        FirstItemText="-- All Stores --" />
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    Send Newsletter :</div>
                <div class="fl">
                    <div class="fl">
                        From</div>
                    <asp:TextBox ID="uxMailStartText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    <div class="fl">
                        &nbsp;&nbsp;To</div>
                    <asp:TextBox ID="uxMailEndText" runat="server" Width="50px" MaxLength="5" CssClass="TextBox mgl10 mgr5">
                    </asp:TextBox>
                    &nbsp;of&nbsp;
                    <asp:Label ID="uxStoreSubscribersLabel" runat="server"></asp:Label>
                    &nbsp;subscribers.
                    <asp:CompareValidator ID="uxMailStartTextCompare" runat="server" ControlToValidate="uxMailStartText"
                        Operator="DataTypeCheck" Type="Integer" Display="None" ValidationGroup="NewsletterValid">
                        <div class="CommonValidatorTextNewsletterFrom">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Value is invalid.
                        <div class="OptionValidateDiv"></div>
                        </div>
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="uxMailEndTextCompare" runat="server" ControlToValidate="uxMailEndText"
                        Operator="DataTypeCheck" Type="Integer" Display="None" ValidationGroup="NewsletterValid">
                        <div class="CommonValidatorTextNewsletterFrom">
                        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Value is invalid.
                        <div class="OptionValidateDiv"></div>
                        </div>
                    </asp:CompareValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle mgt10">
                <div class="Label">
                    Body :</div>
                <uc6:TextEditor ID="uxEmailBodyText" runat="Server" PanelClass="freeTextBox1 fl"
                    TextClass="TextBox" />
                <div class="Clear">
                </div>
            </div>
            <div class="validator1 mgt20">
                <asp:Label ID="uxWarningLabel" runat="server" meta:resourcekey="uxWarningLabel"></asp:Label>
                <%--* Warning! For send too much Emails at one time the server may concern it's a spam. Please consult your hosting provider for how many Emails that server allow to send per hour.--%>
            </div>
            <div class="CommonRowStyleButton mgt10">
                <vevo:AdvanceButton ID="uxSendButton" runat="server" meta:resourcekey="uxSendButton"
                    CssClassBegin="fr mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxSendButton_Click"
                    OnClickGoTo="Top" ValidationGroup="NewsletterValid"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxTestSendButton" runat="server" meta:resourcekey="uxTestSendButton"
                    CssClassBegin="fr mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClickGoTo="Top"
                    OnClick="uxTestSendButton_Click"></vevo:AdvanceButton>
                <asp:Label ID="uxTestSendButtonLabel" runat="server" Text=""></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="uxTestSendButtonModalPopup" runat="server" TargetControlID="uxTestSendButtonLabel"
                    CancelControlID="uxTestSendCloseButton" PopupControlID="uxTestSendPanel" BackgroundCssClass="ConfirmBackground b7"
                    DropShadow="true" RepositionMode="None">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="uxTestSendPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
                    <div class="fb c10 ac">
                        <asp:Label ID="lcNewsletterTestSend" runat="server" meta:resourcekey="lcNewsletterTestSend" /></div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            From :</div>
                        <asp:TextBox ID="uxTestSendFromText" runat="server" Width="520px" CssClass="fl">
                        </asp:TextBox>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            To :</div>
                        <asp:TextBox ID="uxTestSendToText" runat="server" Width="520px" CssClass="fl">
                        </asp:TextBox>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            Subject :</div>
                        <asp:TextBox ID="uxTestSendSubjectText" runat="server" Width="520px" CssClass="fl">
                        </asp:TextBox>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            Body :</div>
                        <div class="freeTextBox1 fl" style="overflow: auto; width: 820px; height: 350px;">
                            <asp:Label ID="uxTestSendEmailBodyText" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <vevo:AdvanceButton ID="uxTestSendSendButton" runat="server" meta:resourcekey="uxSendButton"
                            CssClassBegin="fr mgt10 mgb10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClick="uxTestSendSendButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
                        <vevo:AdvanceButton ID="uxTestSendCloseButton" runat="server" meta:resourcekey="uxCloseButton"
                            CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                    </div>
                    <div class="mgt20 mgl30">
                        <uc1:Message ID="uxTestSendMessage" runat="server" />
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <vevo:AdvanceButton ID="uxPreviewButton" runat="server" meta:resourcekey="uxPreviewButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClickGoTo="Top"
                    OnClick="uxPreviewButton_Click"></vevo:AdvanceButton>
                <asp:Label ID="uxPreviewLabel" runat="server" Text=""></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="uxPreviewButtonModalPopup" runat="server" TargetControlID="uxPreviewLabel"
                    CancelControlID="uxCancelButton" PopupControlID="uxPreviewPanel" BackgroundCssClass="ConfirmBackground b7"
                    DropShadow="true" RepositionMode="None">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="uxPreviewPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
                    <div class="fb c10 ac">
                        <asp:Label ID="lcNewsletterPreview" runat="server" meta:resourcekey="lcNewsletterPreview" /></div>
                    <div class="CommonRowStyle dn">
                        <div class="Label">
                            &nbsp;</div>
                        <asp:Label ID="uxPreviewFromLabel" runat="server" CssClass="fl">
                        </asp:Label>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle dn">
                        <div class="Label">
                            &nbsp;</div>
                        <asp:Label ID="uxPreviewSubjectLabel" runat="server" CssClass="fl">
                        </asp:Label>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            &nbsp;</div>
                        <div class="border2 fl pd10" style="overflow: auto; width: 820px; height: 350px;">
                            <asp:Label ID="uxPreviewEmailBodyLabel" runat="server">
                            </asp:Label>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCloseButton"
                            CssClassBegin="fr mgt10 mgb10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClickGoTo="Top"></vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
