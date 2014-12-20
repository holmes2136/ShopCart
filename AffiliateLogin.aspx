<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AffiliateLogin.aspx.cs"
    Inherits="AffiliateLogin" Title="[$TitlePage]" %>

<%@ Register Src="Components/ContentLayout.ascx" TagName="ContentLayout" TagPrefix="uc1" %>
<%@ Register Src="Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc2:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
    <div class="AffiliateLogin">
        <div class="CommonLoginPage">
            <div class="CommonLoginPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/UserLoginBoxTopLeft.gif"
                    runat="server" CssClass="CommonLoginPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonLoginPageTitle">
                    [$Affiliate]
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/UserLoginBoxTopRight.gif"
                    runat="server" CssClass="CommonLoginPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonLoginPageLeft">
                <div class="CommonLoginPageRight">
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
                                        Display="Dynamic" ValidationGroup="uxLogin" CssClass="CommonValidatorText CommonUserLoginValidatorText">
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
                                            Text="[$BtnLogin]" CssClass="BtnStyle2" /></div>
                                    <div class="CommonUserLoginForgotPasswordDiv">
                                        <asp:HyperLink ID="uxForgotPasswordLink" runat="server" NavigateUrl="ForgotPassword.aspx"
                                            CssClass="CommonHyperLink">[$ForgotPassword]</asp:HyperLink></div>
                                </div>
                                <div class="CommonUserLoginRegisterPanel">
                                    <div class="CommonUserLoginRegisterTitle">
                                        [$New Affiliate]</div>
                                    <div class="CommonUserLoginRegisterDescription">
                                        [$RegisterText]</div>
                                    <div class="CommonUserLoginRegisterLink">
                                        <asp:LinkButton ID="uxRegisterLink" runat="server" Text="[$BtnJoinAffiliateProgram]"
                                            PostBackUrl="AffiliateRegister.aspx" CssClass="BtnStyle1" /></div>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
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
        <div class="AffiliateLoginInformation">
            <uc1:ContentLayout ID="uxContentLayout" runat="server" />
            <div class="AffiliateLoginJoinAffiliateProgramDiv">
                <asp:LinkButton ID="uxJoinAffiliateProgramLink" runat="server" Text="[$BtnJoinAffiliateProgram]"
                    PostBackUrl="AffiliateRegister.aspx" CssClass="BtnStyle1" />
            </div>
        </div>
    </div>
</asp:Content>
