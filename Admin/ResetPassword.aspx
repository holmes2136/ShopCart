<%@ Page Language="C#" MasterPageFile="AdminLogin.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="AdminAdvanced_ResetPassword" Title="Reset Password" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxContentPlaceHolder" runat="Server">
    <div class="LoginControl">
        <uc1:Message ID="uxMessage" runat="server" />
        <div class="LoginInput">
            <asp:Label ID="lcTitle" runat="server" CssClass="fb" Text="Reset Your Password Confirmation" />
        </div>
        <div class="LoginInput">
            Please click submit button to reset your password.
        </div>
        <div class="LoginInput ar">
            <vevo:AdvanceButton ID="uxResetPasswordButton" runat="server" Text="Submit" CssClassBegin="fr mgr30"
                CssClassEnd="" OnClickGoTo="None" CssClass="ButtonLogin" OnClick="uxResetPasswordButton_Click" /></div>
    </div>
</asp:Content>
