<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorePointSystemConfig.ascx.cs"
    Inherits="Admin_Components_StoreConfig_StorePointSystemConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="CommonConfigTitle  mgt0">
<asp:Label ID="uxPointSystemTitle" runat="server" Text="Point System"/></div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnablePointSystemHelp" ConfigName="PointSystemEnabled" runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnablePointSystemLable" runat="server" Text="Enabled Point System"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxEnablePointSystemDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxPointSystemDetailsPanel" runat="server">
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxRewardPointsHelp" ConfigName="RewardPoints" runat="server" />
        <div class="Label">
            <asp:Label ID="uxRewardPointsLabel" runat="server" Text="Reward Points" CssClass="fl" />
        </div>
        <asp:TextBox ID="uxRewardPointsText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxRewardPointsRequiredValidator" runat="server" ControlToValidate="uxRewardPointsText"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Reward Points is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxRewardPointsCompare" runat="server" ControlToValidate="uxRewardPointsText"
            Operator="GreaterThanEqual" ValueToCompare="0" Type="Double" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be positive.
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
    <div class="ConfigRow">
        <uc1:HelpIcon ID="uxPointValueHelp" ConfigName="PointValue" runat="server" />
        <div class="Label">
            <asp:Label ID="uxPointValueLabel" runat="server" Text="Point Value" CssClass="fl" />
        </div>
        <asp:TextBox ID="uxPointValueText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxPointValueRequiredValidator" runat="server" ControlToValidate="uxPointValueText"
            Display="Dynamic" ValidationGroup="SiteConfigValid" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Point Value is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="uxPointValueCompare" runat="server" ControlToValidate="uxPointValueText"
            Operator="GreaterThanEqual" ValueToCompare="0" Type="Double" Display="Dynamic" ValidationGroup="SiteConfigValid"
            CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number must be positive.
            <div class="CommonValidateDiv">
            </div>
        </asp:CompareValidator>
    </div>
</asp:Panel>
