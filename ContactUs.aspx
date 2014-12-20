<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs"
    Inherits="ContactUs" Title="[$Title]" ValidateRequest="false" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ContactUs">
        <uc1:Message ID="uxErrorMessage" runat="server" NumberOfNewLines="0" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">
                    [$Contact Us]
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="ContactMap" id="ContactMapDiv" runat="server">
                    </div>
                    <div class="ContactUsCompanyInfo">
                        <div class="CommonPageInnerTitle">
                            [$Company Information]</div>
                        <div class="ContactUsLabel">
                            [$Company]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxCompanyLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$Address]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxAddressLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$City]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxCityLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$State]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxStateLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$Zip]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxZipLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$Country]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxCountryLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="ContactUsContact">
                        <div class="CommonPageInnerTitle">
                            [$Contact]</div>
                        <div class="ContactUsLabel">
                            [$Phone]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxPhoneLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$Fax]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxFaxLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                        <div class="ContactUsLabel">
                            [$Email]
                        </div>
                        <div class="ContactUsData">
                            <asp:Label ID="uxEmailLabel" runat="server" Text="" CssClass="ContactUsDataLabel"></asp:Label>
                        </div>
                    </div>
                    <div class="ContactUsBlock">
                        <div class="CommonPageInnerTitle">
                            [$Form]</div>
                        <table cellpadding="0" cellspacing="2">
                            <tr>
                                <td align="left">
                                    [$Name]
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="uxNameText" runat="server" ValidationGroup="ContactValid" CssClass="CommonTextBox ContactUsTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="uxNameRequiredValidator" runat="server" ControlToValidate="uxNameText"
                                        ValidationGroup="ContactValid" Display="Dynamic" CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ContactUsValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$NameRequired]
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    [$Email]
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="uxEmailText" runat="server" ValidationGroup="ContactValid" CssClass="CommonTextBox ContactUsTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="uxEmailRequiredValidator" runat="server" ControlToValidate="uxEmailText"
                                        ValidationGroup="ContactValid" Display="Dynamic" CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ContactUsValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$EmailRequired]
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="uxRegularEmailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="uxEmailText" ValidationGroup="ContactValid" Display="Dynamic"
                                        CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ContactUsValidate"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$WrongMailFormat]
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    [$Subject]
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="uxSubjectText" runat="server" ValidationGroup="ContactValid" CssClass="CommonTextBox ContactUsTextBoxLong">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="uxSubjectRequiredValidator" runat="server" ControlToValidate="uxSubjectText"
                                        ValidationGroup="ContactValid" Display="Dynamic" CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ContactUsValidateLong"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$SubjectRequired]
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    [$Comment]
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="uxCommentText" runat="server" TextMode="MultiLine" ValidationGroup="ContactValid"
                                        CssClass="CommonTextBox ContactUsCommentBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="uxCommentRequiredValidator" runat="server" ControlToValidate="uxCommentText"
                                        ValidationGroup="ContactValid" Display="Dynamic" CssClass="CommonValidatorText">
                                    <div class="CommonValidateDiv ContactUsValidateLong"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$CommentRequired]
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <div id="uxButtonDiv" runat="server" class="ContactUsCaptcha">
                            <div>
                                <div class="ContactUsLabel">
                                    <asp:Label ID="uxCaptchaLable" runat="server" Text="[$Anti-Spam Code]"></asp:Label></div>
                                <cc1:CaptchaControl ID="uxCaptchaControl" runat="server" LayoutStyle="Vertical" CssClass="ContactUsCaptchaStyle"
                                    Text="[$Anti-Spam Message]" CaptchaMaxTimeout="300"></cc1:CaptchaControl>
                                <asp:ValidationSummary ID="uxValidationSummary" runat="server" CssClass="CaptchaValidation" />
                            </div>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="ContactUsButtonDiv">
                        <asp:LinkButton ID="uxSubmitImageButton" runat="server" Text="[$BtnSubmit]" CssClass="BtnStyle1"
                            OnClick="uxSubmitButton_Click" ValidationGroup="ContactValid" />
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
    </div>
</asp:Content>
