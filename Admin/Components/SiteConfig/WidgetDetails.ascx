<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetDetails.ascx.cs"
    Inherits="Admin_Components_SiteConfig_WidgetDetails" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc6" %>
<div id="uxWidgetDetailsEnabledTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxWidgetEnabledHelp" runat="server" />
    <div class="Label">
        <asp:Label ID="lcWidgetEnabled" runat="server" CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxWidgetEnableDrop" runat="server" CssClass="fl DropDown mgr5"
        AutoPostBack="true" OnSelectedIndexChanged="uxWidgetEnableDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" Selected="True" />
    </asp:DropDownList>
</div>
<div id="uxWidgetTypeTR" runat="server" class="ConfigRow">
    <asp:Panel ID="uxWidgetParameterPanel" runat="server">
        <uc1:HelpIcon ID="uxWidgetTypeHelp" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="lcWidgetType" runat="server" CssClass="fl"></asp:Label>
        </div>
        <asp:DropDownList ID="uxWidgetTypeDrop" runat="server" CssClass="fl DropDown" AutoPostBack="true"
            OnSelectedIndexChanged="uxWidgetTypeDrop_SelectedIndexChanged">
            <asp:ListItem Value="Default" Text="Default" />
            <asp:ListItem Value="Custom" Text="Custom" />
        </asp:DropDownList>
    </asp:Panel>
</div>
<div id="uxWidgetParameterTR" runat="server" class="ConfigRow">
    <asp:Panel ID="uxWidgetDefaultPanel" runat="server">
        <uc1:HelpIcon ID="uxWidgetDefaultCodeHelp" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxWidgetParameter" runat="server" meta:resourcekey="lcWidgetParameter" />
        </div>
        <asp:TextBox ID="uxWidgetParameterText" runat="server" CssClass="TextBox" />
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
        <asp:RequiredFieldValidator ID="uxWidgetParameterRequiredValidator" runat="server"
            ControlToValidate="uxWidgetParameterText" Display="Dynamic" CssClass="CommonValidatorText">
            <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Value is required.
            <div class="CommonValidateDiv">
            </div>
        </asp:RequiredFieldValidator>
    </asp:Panel>
</div>
<div id="uxWidgetCustomCodeTR" runat="server" class="ConfigRow">
    <asp:Panel ID="uxWidgetCustomPanel" runat="server">
        <uc1:HelpIcon ID="uxWidgetCustomCodeHelp" runat="server" />
        <div class="BulletLabel">
            <asp:Label ID="uxWidgetCustomCode" runat="server" meta:resourcekey="lcWidgetCustomCode" />
        </div>
        <asp:TextBox ID="uxWidgetCustomCodeText" runat="server" TextMode="MultiLine" Height="120px"
            Width="400px" CssClass="TextBox" />
    </asp:Panel>
</div>
