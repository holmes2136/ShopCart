<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleAnalytics.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_GoogleAnalytics" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="CommonConfigTitle">
    <asp:Label ID="lcGoogleAnalytics" runat="server" meta:resourcekey="lcGoogleAnalytics" />
</div>
<asp:Panel ID="uxGoogleAnalyticsEnabledTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxGoogleAnalyticsEnabledHelp" ConfigName="GoogleAnalyticsEnabled"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGoogleAnalyticsEnabled" runat="server" meta:resourcekey="lcGoogleAnalyticsEnabled"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxGoogleAnalyticsEnabledDrop" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="uxGoogleAnalyticsEnabledDrop_SelectedIndexChanged" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes">
        </asp:ListItem>
        <asp:ListItem Value="False" Text="No">
        </asp:ListItem>
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxGoogleAnalyticsAccountTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxGoogleAnalyticsCustomCodeEnabledHelp" ConfigName="GoogleAnalyticsCustomCodeEnabled"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGoogleAnalyticsCustomCodeEnabled" runat="server" meta:resourcekey="lcGoogleAnalyticsCustomCodeEnabled"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxGoogleAnalyticsCustomCodeEnabledDrop" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="uxGoogleAnalyticsCustomCodeEnabledDrop_SelectedIndexChanged"
        CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes">
        </asp:ListItem>
        <asp:ListItem Value="False" Text="No">
        </asp:ListItem>
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxGoogleAnalyticsAccount" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxGoogleAnalyticsAccountHelp" ConfigName="GoogleAnalyticsAccount"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGoogleAnalyticsAccount" runat="server" meta:resourcekey="lcGoogleAnalyticsAccount"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxGoogleAnalyticsAccountText" runat="server" Style="width: 250px;"
        CssClass="TextBox" />
</asp:Panel>
<asp:Panel ID="uxGoogleAnalyticsCustomCode" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxGoogleAnalyticsCustomCodeHelp" ConfigName="GoogleAnalyticsCustomCode"
        runat="server" />
    <div class="Label">
        <asp:Label ID="lcGoogleAnalyticsCustomCode" runat="server" meta:resourcekey="lcGoogleAnalyticsCustomCode"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:TextBox ID="uxGoogleAnalyticsCustomCodeText" runat="server" Style="width: 400px;"
        TextMode="MultiLine" CssClass="TextBox" />
</asp:Panel>
