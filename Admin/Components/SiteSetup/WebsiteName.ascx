<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebsiteName.ascx.cs" Inherits="AdminAdvanced_Components_SiteSetup_WebsiteName" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc4" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="~/Components/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSiteNameHelp" ConfigName="SiteName" runat="server" />
    <asp:Label ID="lcSiteName" runat="server" meta:resourcekey="lcSiteName" CssClass="Label" />
    <asp:TextBox ID="uxSiteNameText" runat="server" CssClass="TextBox" />
    <uc4:LanaguageLabelPlus ID="uxPlus1" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxWebsiteTitleHelp" HelpKeyName="WebsiteTitle" runat="server" />
    <asp:Label ID="lcWebsiteTitle" runat="server" meta:resourcekey="lcWebsiteTitle" CssClass="Label" />
    <asp:TextBox ID="uxWebsiteTitleText" runat="server" CssClass="TextBox" />
    <uc4:LanaguageLabelPlus ID="uxPlus6" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSiteDescriptionHelp" ConfigName="SiteDescription" runat="server" />
    <asp:Label ID="lcSiteDescription" runat="server" meta:resourcekey="lcSiteDescription"
        CssClass="Label" />
    <asp:TextBox ID="uxSiteDescriptionText" runat="server" CssClass="TextBox"></asp:TextBox>
    <uc4:LanaguageLabelPlus ID="uxPlus2" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxSiteKeywordHelp" ConfigName="SiteKeyword" runat="server" />
    <asp:Label ID="lcSiteKeyword" runat="server" meta:resourcekey="lcSiteKeyword" CssClass="Label" />
    <asp:TextBox ID="uxSiteKeywordText" runat="server" Height="40px" TextMode="MultiLine"
        CssClass="TextBox">
    </asp:TextBox>
    <uc4:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableSiteGreetingTextHelp" ConfigName="EnableSiteGreetingText"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableSiteGreetingTextLabel" runat="server" meta:resourcekey="uxEnableSiteGreetingTextLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableSiteGreetingTextDrop" AutoPostBack="true" OnSelectedIndexChanged="uxEnableSiteGreetingTextDrop_SelectedIndexChanged"
        runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxSiteGreetingTextDiv" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxSiteGreetingTextHelp" ConfigName="SiteGreetingText" runat="server" />
    <asp:Label ID="uxSiteGreetingTextLabel" runat="server" meta:resourcekey="uxSiteGreetingTextLabel"
        CssClass="Label" />
    <uc2:TextEditor ID="uxSiteGreetingTextbox" runat="Server" PanelClass="freeTextBox1 fl"
        TextClass="TextBox" Height="300px" />
    <uc4:LanaguageLabelPlus ID="uxSiteGreetingTextLanguageLabel" runat="server" />
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxEnableAccountGreetingTextHelp" ConfigName="EnableAccountGreetingText"
        runat="server" />
    <div class="Label">
        <asp:Label ID="uxEnableAccountGreetingTextLabel" runat="server" meta:resourcekey="uxEnableAccountGreetingTextLabel"
            CssClass="fl">
        </asp:Label>
    </div>
    <asp:DropDownList ID="uxEnableAccountGreetingTextDrop" AutoPostBack="true" OnSelectedIndexChanged="uxEnableAccountGreetingTextDrop_SelectedIndexChanged"
        runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<div id="uxAccountGreetingTextDiv" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxAccountGreetingTextHelp" ConfigName="AccountGreetingText" runat="server" />
    <asp:Label ID="uxAccountGreetingTextLabel" runat="server" meta:resourcekey="uxAccountGreetingTextLabel"
        CssClass="Label" />
    <uc2:TextEditor ID="uxAccountGreetingTextbox" runat="Server" PanelClass="freeTextBox1 fl"
        TextClass="TextBox" Height="300px" />
    <uc4:LanaguageLabelPlus ID="uxAccountGreetingTextLanguageLabel" runat="server" />
</div>
