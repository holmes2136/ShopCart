<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadCountConfig.ascx.cs"
    Inherits="Admin_Components_SiteConfig_DownloadCountConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxDownloadCountHelp" ConfigName="IsUnlimitDownload" runat="server" />
    <div class="Label">
        <asp:Label ID="uxDownloadCountLabel" runat="server" meta:resourcekey="lcDownloadCount"
            CssClass="fl" /></div>
    <asp:DropDownList ID="uxDownloadCountUnlimitDrop" runat="server" CssClass="fl DropDown"
        OnSelectedIndexChanged="uxDownloadCountUnlimitDrop_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="True" Text="Unlimited" />
        <asp:ListItem Value="False" Text="Limit" />
    </asp:DropDownList>
</div>
<div class="ConfigRow">
    <asp:Panel ID="uxNumberOfDownloadCountPanel" runat="server">
        <uc1:HelpIcon ID="uxNumberOfDownloadCountHelp" ConfigName="NumberOfDownloadCount"
            runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxNumberOfDownloadCountLabel" runat="server" meta:resourcekey="lcNumberOfDownloadCount"
                CssClass="fl" />
        </div>
        <asp:TextBox ID="uxNumberOfDownloadCountText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxNumberOfDownloadCountRequired" runat="server" ControlToValidate="uxNumberOfDownloadCountText"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number Of Download Count is required.
            <div class="CommonValidateDiv"></div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxNumberOfDownloadCountCompareWithZero" runat="server"
            ControlToValidate="uxNumberOfDownloadCountText" Display="Dynamic" Type="Integer"
            Operator="GreaterThan" ValueToCompare="0" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number Of Download Count must be an Integer and greater than zero(0).
            <div class="CommonValidateDiv"></div>
        </asp:CompareValidator>
    </asp:Panel>
</div>
