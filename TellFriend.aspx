<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="TellFriend.aspx.cs"
    Inherits="TellFriend" ValidateRequest="false" Title="[$Title]" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Src="Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="TellFriend">
        <div class="CommonPage">
                <div id="uxErrorMessageDiv" runat="server" class="CouponMessageDisplayError" visible="false">
                    <uc1:Message ID="uxErrorMessage" runat="server" />
                </div>
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$TellFriend]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="TellFriendFormDiv">
                        <div class="TellFriendMessageDiv">
                            <asp:Label ID="uxMessageLabel" runat="server" Text="" ForeColor="red"></asp:Label>
                        </div>
                        <div class="TellFriendFormLabel">
                            [$From]
                        </div>
                        <div class="TellFriendFormData">
                            <asp:TextBox ID="uxFromText" runat="server" ValidationGroup="NewsletterValid" MaxLength="50"
                                CssClass="CommonTextBox TellFriendTextBox">
                            </asp:TextBox>
                            <span class="CommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxFromText"
                                ValidationGroup="TellFriend" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv TellFriendValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> From is required
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="uxFromText" ValidationGroup="TellFriend" Display="Dynamic"
                                CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv TellFriendValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="TellFriendFormLabel">
                            [$To]
                        </div>
                        <div class="TellFriendFormData">
                            <asp:TextBox ID="uxToText" runat="server" MaxLength="50" ValidationGroup="NewsletterValid"
                                CssClass="CommonTextBox TellFriendTextBox">
                            </asp:TextBox>
                            <span class="CommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxToRequiredFieldValidator" runat="server" ControlToValidate="uxToText"
                                ValidationGroup="TellFriend" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv TellFriendValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Receipient is required
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="uxRegularToValidator" runat="server" ControlToValidate="uxToText"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TellFriend"
                                Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv TellFriendValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Wrong Email Format
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="TellFriendFormLabel">
                            [$Subject]
                        </div>
                        <div class="TellFriendFormData">
                            <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="TellFriendValid"
                                MaxLength="255" CssClass="CommonTextBox TellFriendLongTextBox">
                            </asp:TextBox>
                            <span class="CommonAsterisk">*</span>
                            <asp:RequiredFieldValidator ID="uxSubjectRequiredFieldValidator" runat="server" ControlToValidate="uxSubjectText"
                                ValidationGroup="TellFriend" Display="Dynamic" CssClass="CommonValidatorText">
                                <div class="CommonValidateDiv TellFriendValidateLong"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Subject is required
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="Clear">
                        </div>
                        <div class="TellFriendFormTextEditorDiv">
                            <asp:TextBox ID="uxEmailBodyText" runat="server" CssClass="TellFriendMessageTextBox"
                                TextMode="MultiLine" Rows="8" />
                        </div>
                        <div class="TellFriendCapchaDiv">
                            <div class="CommonPageInnerTitle">
                                <asp:Label ID="uxCaptchaLable" runat="server" Text="[$Anti-Spam Code]" CssClass="TellFriendCapchaLabel"></asp:Label></div>
                            <cc1:CaptchaControl ID="uxCaptchaControl" runat="server" LayoutStyle="Vertical" CssClass="TellFriendCaptchaStyle"
                                Text="[$Anti-Spam Message]" CaptchaMaxTimeout="300"></cc1:CaptchaControl>
                            <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="CaptchaValidation" />
                        </div>
                    </div>
                    <div class="TellFriendImageButtonDiv">
                        <asp:LinkButton ID="uxSubmit" runat="server" CssClass="BtnStyle1 TellFriendImageButton"
                            Text="[$BtnSubmit]" ValidationGroup="TellFriend" OnClick="uxSubmit_Click" />
                    </div>
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
