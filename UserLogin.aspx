<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs"
    Inherits="UserLogin" Title="[$LoginTitle]" %>

<%@ Register Src="Components/VevoHyperLink.ascx" TagName="VevoHyperLink" TagPrefix="ucHyperLink" %>
<%@ Register Src="Components/CheckoutIndicator.ascx" TagName="CheckoutIndicator"
    TagPrefix="uc1" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<asp:Content ID="uxTopContent" ContentPlaceHolderID="uxTopPlaceHolder" runat="Server">
    <uc1:CheckoutIndicator ID="uxCheckoutIndicator" runat="server" />
</asp:Content>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc2:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <div class="UserLogin" id="uxUserLogin" runat="server">
        <div class="CommonLoginPage">
            <div class="CommonLoginPageTop" id="uxTopPage" runat="server">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/UserLoginBoxTopLeft.gif"
                    runat="server" CssClass="CommonLoginPageTopImgLeft" />
                <asp:Label ID="uxTitle" runat="server" CssClass="CommonLoginPageTitle">
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/UserLoginBoxTopRight.gif"
                    runat="server" CssClass="CommonLoginPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonLoginPageLeft">
                <div class="CommonLoginPageRight">
                    <%--Start--%>
                    <asp:Login ID="uxLogin" runat="server" OnLoggedIn="uxLogin_LoggedIn" OnLoginError="uxLogin_Error"
                        CssClass="CommonUserLoginControl" OnLoggingIn="uxLogin_LoggingIn">
                        <LayoutTemplate>
                            <div class="CommonUserLoginControlInner">
                                <div class="CommonUserLoginError">
                                    <asp:Label ID="uxErrorLabel" runat="server" />
                                </div>
                                <div class="CommonUserLoginLoginPanel">
                                    <div class="CommonUserLoginPanelTitle">
                                        [$Login]</div>
                                    <div class="CommonUserLoginPanelDescription">
                                        [$EnterPassword]</div>
                                    <div class="CommonUserLoginPanelUserName">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">[$User Name]:</asp:Label>
                                        <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox>
                                        <div class="Clear">
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        Display="Dynamic" ValidationGroup="uxLogin" CssClass="CommonValidatorText CommonUserLoginValidatorText">
                                        <div class="CommonValidateDiv CommonUserLoginValidateDiv"></div>
                                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$User Name is required]
                                    </asp:RequiredFieldValidator>
                                    <div class="CommonUserLoginPanelPassword">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">[$Password]:</asp:Label>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                        <div class="Clear">
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ValidationGroup="uxLogin" CssClass="CommonValidatorText CommonUserLoginValidatorText"
                                        Display="Dynamic">
                                        <div class="CommonValidateDiv CommonUserLoginValidateDiv"></div>
                                        <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> [$Password is required]
                                    </asp:RequiredFieldValidator>
                                    <div class="CommonUserLoginPanelRemember">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="[$Remember]" />
                                        <div class="Clear">
                                        </div>
                                    </div>
                                    <div class="CommonUserLoginPanelButton">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="uxLogin"
                                            Visible="False" />
                                        <asp:LinkButton ID="uxLoginImageButton" runat="server" CommandName="Login" ValidationGroup="uxLogin"
                                            Text="[$BtnLogin]" CssClass="BtnStyle2" />
                                    </div>
                                    <div class="CommonUserLoginForgotPasswordDiv">
                                        <asp:HyperLink ID="uxForgotPasswordLink" runat="server" NavigateUrl="ForgotPassword.aspx"
                                            CssClass="CommonHyperLink">[$ForgotPassword]</asp:HyperLink></div>
                                    <div class="Clear">
                                    </div>
                                    <div class="CommonUserLoginPanelButton">
                                        <ucHyperLink:VevoHyperLink ID="uxFacebookLink" runat="server" ThemeImageUrl="[$ImgFacebookSync]"
                                            Visible="false" />
                                    </div>
                                </div>
                                <div class="CommonUserLoginRegisterPanel">
                                    <div class="CommonUserLoginRegisterTitle">
                                        [$New User]</div>
                                    <div class="CommonUserLoginRegisterDescription">
                                        [$RegisterText]</div>
                                    <div class="CommonUserLoginRegisterLink">
                                        <asp:LinkButton ID="uxRegisterLink" runat="server" Text="[$BtnRegister]" CssClass="BtnStyle1"
                                            OnLoad="uxRegisterLink_Load" />
                                    </div>
                                    <asp:Panel ID="uxSkipLoginPanel" runat="server" CssClass="CommonUserLoginSkipLoginPanel">
                                        <div class="CommonUserLoginRegisterTitle">
                                            [$Anonymous User]</div>
                                        <div class="CommonUserLoginRegisterDescription">
                                            [$AnonymousText]</div>
                                        <div class="CommonUserLoginRegisterLink">
                                            <asp:LinkButton ID="uxSkiploginButton" runat="server" Text="[$BtnSkipLogin]" CssClass="BtnStyle1"
                                                OnClick="uxSkiploginButton_Click" />
                                        </div>
                                        <div class="CommonUserLoginMessageFailure">
                                            <asp:Label ID="uxSkiploginMessageLabel" runat="server" Visible="false"> [$AnonymousMessage]</asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                    <%--End--%>
                </div>
            </div>
            <div class="CommonLoginPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/UserLoginBoxBottomLeft.gif"
                    runat="server" CssClass="CommonLoginPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/UserLoginBoxBottomRight.gif"
                    runat="server" CssClass="CommonLoginPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
