<%@ Page Language="C#" MasterPageFile="AdminLogin.master" AutoEventWireup="true"
    CodeFile="PasswordRecoveryFinished.aspx.cs" Inherits="AdminAdvance_PasswordRecoveryFinished"
    Title="Password Reset Success" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxContentPlaceHolder" runat="Server">
    <div class="LoginControl">
        <div class="LoginInput">
                Please check your email for the password reset confirmation link.
        </div>
        <div class="LoginInput ar">
            <a href="Login.aspx" class="LoginForgotPassword">Return to log in page</a></div>
    </div>
</asp:Content>
