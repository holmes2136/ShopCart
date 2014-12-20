<%@ Page Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="Mobile_ContactUs" Title="[$Title]" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        <asp:Label ID="uxDefaultTitle" runat="server">
        [$Contact Us]
        </asp:Label>
    </div>
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
    <div class="MobileCommonBox">
        <div class="MobileContactUsLabel">
            [$Name]</div>
        <asp:TextBox ID="uxNameText" runat="server" ValidationGroup="ContactValid" CssClass="MobileContactUsInput">
        </asp:TextBox>
        <span class="MobileCommonAsterisk">*</span>
        <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
            ValidationGroup="ContactValid" Display="Dynamic" CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$NameRequired]
        </asp:RequiredFieldValidator>
        <div class="MobileContactUsLabel">
            [$Email]
        </div>
        <asp:TextBox ID="uxEmailText" runat="server" ValidationGroup="ContactValid" CssClass="MobileContactUsInput">
        </asp:TextBox>
        <span class="MobileCommonAsterisk">*</span>
        <asp:RequiredFieldValidator ID="uxEmailRequiredValidator" runat="server" ControlToValidate="uxEmailText"
            ValidationGroup="ContactValid" Display="Dynamic" CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$EmailRequired]
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ControlToValidate="uxEmailText" ValidationGroup="ContactValid" CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$WrongMailFormat]
        </asp:RegularExpressionValidator>
        <div class="MobileContactUsLabel">
            [$Subject]
        </div>
        <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="ContactValid" CssClass="MobileContactUsInput">
        </asp:TextBox>
        <span class="MobileCommonAsterisk">*</span>
        <asp:RequiredFieldValidator ID="uxSubjectRequiredValidator" runat="server" ControlToValidate="uxSubjectText"
            ValidationGroup="ContactValid" Display="Dynamic" CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$SubjectRequired]
        </asp:RequiredFieldValidator>
        <div class="MobileContactUsLabel">
            [$Comment]
        </div>
        <asp:TextBox ID="uxCommentText" runat="server" Height="100px" TextMode="MultiLine"
            ValidationGroup="ContactValid" CssClass="MobileContactUsInput">
        </asp:TextBox>
        <span class="MobileCommonAsterisk">*</span>
        <asp:RequiredFieldValidator ID="uxCommentRequiredValidator" runat="server" ControlToValidate="uxCommentText"
            ValidationGroup="ContactValid" Display="Dynamic" CssClass="MobileCommonValidatorText">
            <div class="MobileCommonValidateDiv"></div>
            <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$CommentRequired]
        </asp:RequiredFieldValidator>
        <div id="uxButtonDiv" runat="server">
            <asp:Label ID="uxCaptchaLable" runat="server" Text="[$Anti-Spam Code]" CssClass="MobileContactUsLabel" />
            <cc1:CaptchaControl ID="uxCaptchaControl" runat="server" LayoutStyle="Vertical" CssClass="MobileContactUsCaptchaStyle"
                Text="[$Anti-Spam Message]" CaptchaMaxTimeout="300"></cc1:CaptchaControl>
            <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="MobileCaptchaValidation" />
        </div>
        <div class="MobileUserLoginControlPanel">
            <asp:LinkButton ID="uxSubmitImageButton" runat="server" Text="[$ContactSubmit]" OnClick="uxSubmitButton_Click"
                CssClass="MobileButton MobileCouponButton" ValidationGroup="ContactValid" />
        </div>
    </div>
</asp:Content>
