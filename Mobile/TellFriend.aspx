<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="TellFriend.aspx.cs"
    Inherits="TellFriend" ValidateRequest="false" Title="[$Title]" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTellFriend">
        <div class="MobileTitle">
            [$TellFriend]
        </div>
        <uc1:Message ID="uxErrorMessage" runat="server"></uc1:Message>
        <div class="MobileCommonBox">
            <div class="TellFriendFormDiv">
                <div class="TellFriendMessageDiv">
                    <asp:Label ID="uxMessageLabel" runat="server" Text="" ForeColor="red"></asp:Label>
                </div>
                <div class="MobileCommonFormLabel TellFriendFormLabel">
                    [$From]
                </div>
                <div class="TellFriendFormText">
                    <asp:TextBox ID="uxSenderText" runat="server" ValidationGroup="NewsletterValid" MaxLength="50"
                        CssClass="MobileCommonTextBox">
                    </asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxSenderText"
                        ValidationGroup="TellFriend" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div>
                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Sender Email is required.
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ErrorMessage="Wrong Email Format"
                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxSenderText" ValidationGroup="TellFriend" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div>
                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                    </asp:RegularExpressionValidator>
                </div>
                <div class="MobileCommonFormLabel TellFriendFormLabel">
                    [$To]
                </div>
                <div class="TellFriendFormText">
                    <asp:TextBox ID="uxRecipientText" runat="server" MaxLength="50" ValidationGroup="NewsletterValid"
                        CssClass="MobileCommonTextBox">
                    </asp:TextBox>
                    <span class="MobileCommonAsterisk">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uxRecipientText"
                        ValidationGroup="TellFriend" Display="Dynamic" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div>
                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Recipient Email is required.
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Wrong Email Format"
                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="uxRecipientText" ValidationGroup="TellFriend" CssClass="MobileCommonValidatorText">
                        <div class="MobileCommonValidateDiv"></div>
                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                    </asp:RegularExpressionValidator>
                </div>
                <div class="MobileCommonFormLabel TellFriendFormLabel">
                    [$Message]
                </div>
                <div class="TellFriendFormText">
                    <asp:TextBox ID="uxMessage" runat="server" Width="304px" Height="100px" PanelClass="MobileCommonTextEditorPanel"
                        ToolbarSet="StoreFront" TextMode="MultiLine" />
                </div>
            </div>
            <div class="TellFriendCapchaDiv">
                <div class="MobileCommonFormLabel">
                    <asp:Label ID="uxCaptchaLable" runat="server" Text="[$Anti-Spam Code]" CssClass="MobileCommonFormLabel TellFriendCaptcha"></asp:Label>
                </div>
                <cc1:CaptchaControl ID="uxCaptchaControl" runat="server" LayoutStyle="Vertical" CssClass="TellFriendCaptchaStyle"
                    Text="[$Anti-Spam Message]" CaptchaMaxTimeout="300"></cc1:CaptchaControl>
                <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="MobileCaptchaValidation" />
            </div>
            <div class="TellFriendButtonDiv">
                <asp:LinkButton ID="uxSubmit" runat="server" Text="Submit Query" ValidationGroup="TellFriend"
                    OnClick="uxSubmit_Click" CssClass="MobileButton MobileTellFriendButton" />
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
</asp:Content>
