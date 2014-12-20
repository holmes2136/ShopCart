<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailTemplateEdit.ascx.cs"
    Inherits="AdminAdvanced_MainControls_EmailTemplateEdit" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="../Components/StoreDropDownList.ascx" TagName="StoreDropDownList"
    TagPrefix="uc3" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidEmail" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
    </ValidationDenotesTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <FilterTemplate>
        <asp:Label ID="uxStoreViewLabel" runat="server" Text="Store View" CssClass="Label fb"></asp:Label>
        <uc3:StoreDropDownList ID="uxStoreList" runat="server" AutoPostBack="true" OnBubbleEvent="uxStoreList_RefreshHandler"
            FirstItemVisible="false" CurrentSelected="0" />
    </FilterTemplate>
    <TopContentBoxTemplate>
        <asp:Label ID="uxEmailTemplateName" runat="server" Text="Email Template Name: "></asp:Label>
        <%=Name %>
        <div class="RequiredLabel c6 fr">
            * Please do not remove keywords inside brackets ( [...] )</div>
    </TopContentBoxTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <asp:Panel ID="uxSubjectTextTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxSubjectLabel" runat="server" CssClass="Label">
                Subject :</asp:Label>
                <asp:TextBox ID="uxSubjectText" Style="width: 252px;" runat="server" TextMode="SingleLine"
                    CssClass="TextBox" />
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                </div>
                <asp:RequiredFieldValidator ID="uxRequiredSubjectValidator" runat="server" ControlToValidate="uxSubjectText"
                    ValidationGroup="ValidEmail" CssClass="CommonValidatorText" Display="Dynamic">
                            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Subject is required.
                            <div class="CommonValidateDiv CommonValidateDivCategoryLong">
                            </div>
                </asp:RequiredFieldValidator>
            </asp:Panel>
            <asp:Panel ID="uxBodyTextTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxBodyLabel" runat="server" CssClass="Label">
                Body :</asp:Label>
                <uc6:TextEditor ID="uxContentText" runat="server" PanelClass="freeTextBox1 fl" TextClass="TextBox" />
            </asp:Panel>
            <asp:Panel ID="uxLinkTextTR" runat="server" CssClass="CommonRowStyle">
                <asp:Label ID="uxLinkLabel" runat="server" CssClass="Label">
                Keyword :</asp:Label>
                <asp:DropDownList ID="uxEmailKeywordDrop" runat="server" CssClass="fl DropDown" OnSelectedIndexChanged="uxEmailKeywordDrop_SelectedIndexChanged">
                </asp:DropDownList>
                <vevo:AdvanceButton ID="uxInsertKeywordButton" runat="server" meta:resourcekey="uxInsertKeywordButton"
                    CssClassBegin="fl mgl5" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxInsertKeywordButton_Click" />
            </asp:Panel>
            <div class="CommonRowStyleButton">
                <asp:Label ID="uxPlianLabel" runat="server" CssClass="Label">
                &nbsp;</asp:Label>
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr mgt10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="ValidEmail">
                </vevo:AdvanceButton>
                <%--Test Send Button and panel--%>
                <vevo:AdvanceButton ID="uxTestSendButton" runat="server" meta:resourcekey="uxTestSendButton"
                    CssClassBegin="fr mgl10 mgt10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClickGoTo="Top" OnClick="uxTestSendButton_Click" ValidationGroup="ValidEmail">
                </vevo:AdvanceButton>
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
                        <uc2:Message ID="uxTestSendMessage" runat="server" />
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <%--Preview Button and panel--%>
                <vevo:AdvanceButton ID="uxPreviewButton" runat="server" meta:resourcekey="uxPreviewButton"
                    CssClassBegin="fr mgt10" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClickGoTo="Top"
                    OnClick="uxPreviewButton_Click" ValidationGroup="ValidEmail"></vevo:AdvanceButton>
                <asp:Label ID="uxPreviewLabel" runat="server" Text=""></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="uxPreviewButtonModalPopup" runat="server" TargetControlID="uxPreviewLabel"
                    CancelControlID="uxCancelButton" PopupControlID="uxPreviewPanel" BackgroundCssClass="ConfirmBackground b7"
                    DropShadow="true" RepositionMode="None">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="uxPreviewPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
                    <div class="fb c10 ac">
                        <asp:Label ID="uxEmailTemplatePreview" runat="server" Text="Email Template Preview" />
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            Subject :</div>
                        <div class="border2 fl pd10" style="overflow: auto; width: 820px;">
                            <asp:Label ID="uxPreviewSubjectLabel" runat="server">
                            </asp:Label>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label mgl30">
                            Body :</div>
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
