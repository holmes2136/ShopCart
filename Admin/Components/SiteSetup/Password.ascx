<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Password.ascx.cs" Inherits="AdminAdvanced_Components_SiteSetup_Password" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxOldPassHelp" HelpKeyName="YourOldPassword" runat="server" />
    <asp:Label ID="uxOldPasswordLabel" runat="server" meta:resourcekey="lcOldPass" CssClass="Label" />
    <asp:TextBox ID="uxOldPassText" runat="server" TextMode="Password" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxNewPassHelp" HelpKeyName="YourNewPassword" runat="server" />
    <asp:Label ID="uxNewPasswordLabel" runat="server" meta:resourcekey="lcNewPass" CssClass="Label" />
    <asp:TextBox ID="uxNewPassText" runat="server" TextMode="Password" CssClass="TextBox" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxConfirmPassHelp" HelpKeyName="ConfirmNewPassword" runat="server" />
    <asp:Label ID="uxConfirmPasswordLabel" runat="server" meta:resourcekey="lcConPass"
        CssClass="Label" />
    <asp:TextBox ID="uxConPassText" runat="server" TextMode="Password" CssClass="TextBox" />
    <div class="validator1 fl">
        <asp:CompareValidator ID="uxCompareValidator" runat="server" ErrorMessage="Confirm Password is not matched."
            ControlToCompare="uxNewPassText" ControlToValidate="uxConPassText" Display="Dynamic"
            ValidationGroup="ValidSiteSetup"></asp:CompareValidator>
    </div>
</div>
