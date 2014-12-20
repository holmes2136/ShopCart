<%@ Page Language="C#" MasterPageFile="AdminLogin.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="AdminAdvance_Login" Title="VevoCart Administrator Login" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxContentPlaceHolder" runat="Server">
    <div class="mgt10 pdr10 dn">
        <asp:DropDownList ID="uxPageThemeDrop" runat="server" OnSelectedIndexChanged="uxPageThemeDrop_SelectedIndexChanged"
            AutoPostBack="true" CssClass="fr mgr10">
            <asp:ListItem Text="AdminBlueTheme" Value="AdminBlueTheme"></asp:ListItem>
            <asp:ListItem Text="AdminGreen" Value="AdminGreenTheme"></asp:ListItem>
        </asp:DropDownList>
        <div class="fb ar fr" style="width: 150px; padding-right: 2px;">
            <asp:Label ID="uxPageThemeLabel" runat="server" Text="Theme : "></asp:Label></div>
        <div class="Clear">
        </div>
    </div>
    <div class="LoginControl">
        <asp:Login ID="uxLogin" runat="server" TitleText="Please enter your username and password."
            OnLoginError="uxLogin_Error" OnLoggingIn="uxLogin_LoggingIn" Width="100%">
            <LayoutTemplate>
                <div style="margin-bottom: 10px; color: Blue;">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </div>
                <div class="Clear">
                </div>
                <div class="LoginInput">
                    <div class="LoginLabel">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Login Name</asp:Label>
                    </div>
                    <div class="LoginUsername">
                        <asp:TextBox ID="UserName" runat="server" CssClass="TextBox"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="uxLogin">
                        *
                    </asp:RequiredFieldValidator>
                </div>
                <div class="LoginInput">
                    <div class="LoginLabel">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                    </div>
                    <div class="LoginPassword">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="TextBox"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password
                                is required." ToolTip="Password is required." ValidationGroup="uxLogin">*</asp:RequiredFieldValidator>
                </div>
                <div class="LoginInput ar">
                    <a href="ForgotPassword.aspx" class="LoginForgotPassword">Forgot Password?</a></div>
                <div class="LoginInput ar">
                    <vevo:AdvanceButton ID="LoginButton" runat="server" CommandName="Login" CssClassBegin="fr"
                        CssClass="ButtonLogin" Text="Log In" OnClickGoTo="None"
                        ValidationGroup="uxLogin"></vevo:AdvanceButton>
                    <div class="Clear">
                    </div>
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>
</asp:Content>
