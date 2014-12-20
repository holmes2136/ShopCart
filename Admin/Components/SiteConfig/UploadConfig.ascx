<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadConfig.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_UploadConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxUploadExtensionTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxUploadExtensionHelp" ConfigName="UploadExtension" runat="server" />
    <div class="Label">
        <asp:Label ID="lcUploadExtension" runat="server" meta:resourcekey="lcUploadExtension"
            CssClass="fl" />
    </div>
    <asp:TextBox ID="uxUploadExtension" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="RequiredUploadExtension" runat="server" ControlToValidate="uxUploadExtension"
        ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText" Display="Dynamic">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> File Extension is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
</asp:Panel>
<asp:Panel ID="uxUploadSizeTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxUploadSizeHelp" ConfigName="UploadSize" runat="server" />
    <div class="Label">
        <asp:Label ID="lcUploadSize" runat="server" meta:resourcekey="lcUploadSize" CssClass="fl" />
    </div>
    <asp:TextBox ID="uxUploadSize" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="RequiredUploadSize" runat="server" ControlToValidate="uxUploadSize"
        ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText" Display="Dynamic">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Upload Max Size is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxUploadCompareValidator" runat="server" ControlToValidate="uxUploadSize"
        Type="Integer" Operator="DataTypeCheck" ValidationGroup="SiteConfigValid" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Size is invalid.
        <div class="CommonValidateDiv">
        </div>
    </asp:CompareValidator>
</asp:Panel>
