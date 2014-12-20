<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailSetup.ascx.cs" Inherits="AdminAdvanced_Components_SiteSetup_EmailSetup" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="CommonConfigTitle  mgt0">
<asp:Label ID="uxEmailSettingTitle" runat="server" meta:resourcekey="uxEmailSettingTitle" ></asp:Label></div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSmtpServerHelp" ConfigName="SmtpServer" runat="server" />
    <asp:Label ID="lcSmtpServer" runat="server" meta:resourcekey="lcSmtpServer" CssClass="Label" />
    <asp:TextBox ID="uxSmtpServerText" runat="server" CssClass="TextBox" />
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxRequiredEmailValidator" runat="server" ControlToValidate="uxSmtpServerText"
        ValidationGroup="SiteConfigValid" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> SMTP is required.
        <div class="CommonValidateDiv">
        </div>
    </asp:RequiredFieldValidator>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSmtpPortHelp" ConfigName="SmtpPort" runat="server" />
    <asp:Label ID="lcSmtpPort" runat="server" meta:resourcekey="lcSmtpPort" CssClass="Label" />
    <asp:TextBox ID="uxSmtpPortText" runat="server" Width="60px" CssClass="TextBox"></asp:TextBox>
    <div class="validator1 fl">
        <span class="Asterisk">*</span>
    </div>
    <asp:RequiredFieldValidator ID="uxSmtpPortRequired" runat="server" ValidationGroup="SiteConfigValid"
        ControlToValidate="uxSmtpPortText" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> SMTP Port is required.
        <div class="CommonValidateDiv CommonValidateDivSmtpPort">
        </div>
    </asp:RequiredFieldValidator>
    <asp:CompareValidator ID="uxSmtpPortCompare" runat="server" ValidationGroup="SiteConfigValid"
        ControlToValidate="uxSmtpPortText" Operator="DataTypeCheck" Type="Integer" Display="Dynamic"
        CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Value is invalid.
        <div class="CommonValidateDiv CommonValidateDivSmtpPort">
        </div>
    </asp:CompareValidator>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSmtpAuthenRequiredHelp" ConfigName="SmtpAuthenRequired" runat="server" />
    <asp:Label ID="lcSmtpAuthenRequired" runat="server" meta:resourcekey="lcSmtpAuthenRequired"
        CssClass="Label" />
    <asp:DropDownList ID="uxSmtpAuthenRequiredDrop" runat="server" AutoPostBack="True"
        CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxSmtpUserNameTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxSmtpUserNameHelp" ConfigName="SmtpUserName" runat="server" />
    <asp:Label ID="lcSmtpUserName" runat="server" meta:resourcekey="lcSmtpUserName" CssClass="BulletLabel"></asp:Label>
    <asp:TextBox ID="uxSmtpUserNameText" runat="server" CssClass="TextBox" />
    <asp:RequiredFieldValidator ID="uxSmtpUserNameRequired" runat="server" ValidationGroup="SiteConfigValid"
        ControlToValidate="uxSmtpUserNameText" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> SMTP UserName is required.
        <div class="CommonValidateDiv CommonValidateDivSmtpUserName">
        </div>
    </asp:RequiredFieldValidator>
    <div class="Clear">
    </div>
</asp:Panel>
<asp:Panel ID="uxSmtpPasswordTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxSmtpPasswordHelp" ConfigName="SmtpPassword" runat="server" />
    <asp:Label ID="lcSmtpPassword" runat="server" meta:resourcekey="lcSmtpPassword" CssClass="BulletLabel"></asp:Label>
    <asp:TextBox ID="uxSmtpPassword" runat="server" TextMode="Password" Width="143px"
        Text="****" CssClass="TextBox" />
    <asp:RequiredFieldValidator ID="uxSmtpPasswordRequired" runat="server" ValidationGroup="SiteConfigValid"
        ControlToValidate="uxSmtpPassword" Display="Dynamic" CssClass="CommonValidatorText">
        <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> SMTP Password is required.
        <div class="CommonValidateDiv CommonValidateDivSmtpPassword">
        </div>
    </asp:RequiredFieldValidator>
    <asp:HiddenField ID="uxHiddenPassword" runat="server"></asp:HiddenField>
</asp:Panel>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxRequireEmailSslHelp" ConfigName="SmtpSslRequired" runat="server" />
    <asp:Label ID="uxRequireEmailSslLabel" runat="server" meta:resourcekey="uxRequireEmailSslLabel"
        CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxRequireEmailSslDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
