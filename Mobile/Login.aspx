<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Mobile_UserLogin" %>

<%@ Register Src="Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="~/Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileUserLoginPanelTitle">
        [$Login]</div>
    <%--Start--%>
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
    <asp:Login ID="uxLogin" runat="server" TitleText="Please enter your user name and password."
        OnLoggedIn="uxLogin_LoggedIn" OnLoginError="uxLogin_Error" CssClass="MobileUserLoginControlPanel"
        OnLoggingIn="uxLogin_LoggingIn">
        <LayoutTemplate>
            <div class="MobileCommonBox">
                <div class="MobileUserLoginControlPanel">
                    <div class="MobileUserLoginControl">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="MobileUserLoginLabel">[$User Name]:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="MobileUserLoginText"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxRequiredFirstNameValidator" runat="server" ControlToValidate="UserName"
                            ValidationGroup="login" Display="Dynamic" CssClass="MobileCommonValidatorText">
                            <div class="MobileCommonValidateDiv LoginValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required User Name
                        </asp:RequiredFieldValidator>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="MobileUserLoginControl">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="MobileUserLoginLabel">[$Password]:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="MobileUserLoginText"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Password"
                            ValidationGroup="login" Display="Dynamic" CssClass="MobileCommonValidatorText">
                            <div class="MobileCommonValidateDiv LoginValidateDiv"></div><img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required Password
                        </asp:RequiredFieldValidator>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="MobileUserLoginControlPanel">
                        <asp:LinkButton ID="uxLoginImageButton" runat="server" Text="Login" CommandName="Login"
                            CssClass="MobileButton MobileCouponButton" ValidationGroup="login" />
                    </div>
                    <div class="MobileUserLoginControlPanel">
                        <asp:HyperLink ID="uxRegisterLink" runat="server" OnLoad="uxRegisterLink_Load" Text="[$CreateUser]"
                            CssClass="MobileButton MobileCouponButton" />
                    </div>
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <%--End--%>
</asp:Content>
