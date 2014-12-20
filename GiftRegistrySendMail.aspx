<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistrySendMail.aspx.cs"
    Inherits="GiftRegistrySendMail" ValidateRequest="false" Title="[$GiftRegistrySendMail]" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Src="Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<%@ Register Src="Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistrySendMail">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$SendGiftRegistry]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Literal ID="uxErrorLiteral" runat="server" Visible="false">
                    You are not authorized to view this page.
                    </asp:Literal>
                    <asp:Panel ID="uxGiftRegistrySendMailPanel" runat="server" CssClass="GiftRegistrySendMailPanel">
                        <div class="GiftRegistrySendMailLabel">
                            [$From]
                        </div>
                        <div class="GiftRegistrySendMailData">
                            <asp:TextBox ID="uxFromText" runat="server" ValidationGroup="SendGiftRegistry" MaxLength="50"
                                ReadOnly="true" Width="150px" CssClass="CommonTextBox">
                            </asp:TextBox>
                        </div>
                        <div class="GiftRegistrySendMailLabel">
                            [$To]</div>
                        <div class="GiftRegistrySendMailData">
                            <asp:TextBox ID="uxToText" runat="server" ValidationGroup="SendGiftRegistry" TextMode="MultiLine"
                                Rows="3" Wrap="true" CssClass="CommonTextBox GiftRegistrySendMailTextbox">
                            </asp:TextBox>
                            <span class="CommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxToRequiredFieldValidator" runat="server" ControlToValidate="uxToText"
                                ValidationGroup="SendGiftRegistry" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftRegistrySendMailValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Recipient is required
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="uxRegularToValidator" runat="server" ControlToValidate="uxToText"
                                ValidationExpression="(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\;*\,*\ *)*"
                                ValidationGroup="SendGiftRegistry" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftRegistrySendMailValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                            </asp:RegularExpressionValidator></div>
                        <div class="GiftRegistrySendMailLabel">
                            [$Subject]</div>
                        <div class="GiftRegistrySendMailData">
                            <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="SendGiftRegistry"
                                MaxLength="255" CssClass="CommonTextBox GiftRegistrySendMailTextbox">
                            </asp:TextBox>
                            <span class="CommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxSubjectRequiredFieldValidator" runat="server" ControlToValidate="uxSubjectText"
                                ValidationGroup="SendGiftRegistry" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv GiftRegistrySendMailValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Subject is required
                            </asp:RequiredFieldValidator></div>
                        <div class="GiftRegistrySendMailLabel">
                            [$Comment]</div>
                        <div class="GiftRegistrySendMailData">
                            <asp:TextBox ID="uxEmailBodyText" runat="server" TextMode="MultiLine" Width="443px"
                                Height="300px"></asp:TextBox>
                        </div>
                        <div class="GiftRegistrySendMailCaptchaDiv">
                            <asp:Label ID="uxCaptchaLable" runat="server" Text="[$Anti-Spam Code]" CssClass="CommonPageInnerTitle">
                            </asp:Label>
                            <cc1:CaptchaControl ID="uxCaptchaControl" runat="server" LayoutStyle="Vertical" CssClass="GiftRegistrySendMailCaptchaStyle"
                                Text="[$Anti-Spam Message]" CaptchaMaxTimeout="300"></cc1:CaptchaControl>
                            <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="CaptchaValidation" />
                        </div>
                        <div class="GiftRegistrySendMailButtonDiv">
                            <asp:LinkButton ID="uxBackLink" runat="server" PostBackUrl="~/GiftRegistryList.aspx"
                                Text="[$BtnBackToList]" CssClass="GiftRegistrySendMailBackLink BtnStyle2" />
                            <asp:LinkButton ID="uxSubmit" runat="server" OnClick="uxSubmit_Click" ValidationGroup="SendGiftRegistry"
                                Text="[$BtnSubmit]" CssClass="GiftRegistrySendMailSubmitImageButton BtnStyle1" />
                        </div>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="uxEmailBodyHidden" runat="server" />
</asp:Content>
