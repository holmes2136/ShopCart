<%@ Page Language="C#" MasterPageFile="AdminLogin.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="AdminAdvance_ForgotPassword" Title="Password Recovery" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxContentPlaceHolder" runat="Server">
    <div class="LoginControl">
        <uc1:Message ID="uxMessage" runat="server" />
        <div class="LoginInput">
                <asp:Label ID="lcTitle" runat="server" meta:resourcekey="lcTitle" CssClass="fb" />
        </div>
        <div class="LoginInput">
            <asp:Label ID="lcEnterUserName" runat="server" meta:resourcekey="lcEnterUserName" />
            <div class="LoginForgotUsername fl">
                <asp:TextBox ID="uxUserNameText" runat="server" CssClass="ForgotTextBox"></asp:TextBox>
            </div>
        </div>
        <div class="LoginInput ar">
            <vevo:AdvanceButton ID="uxSubmitButton" runat="server" CssClassBegin="fr" CssClassEnd=""
                CssClass="ButtonLogin mgr30" Text="Submit" OnClick="uxSubmitButton_Click" /></div>
        <div class="LoginInput ar">
            <a href="Login.aspx" class="LoginForgotPassword mgr30">Return to log in page</a></div>
    </div>
</asp:Content>
