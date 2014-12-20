<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultWebsiteLanguage.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteSetup_DefaultWebsiteLanguage" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
<uc1:HelpIcon ID="uxDefaultWebsiteLanguageHelp" ConfigName="DefaultWebsiteLanguage" runat="server" />
    <asp:Label ID="lcDefaultWebsiteLanguage" runat="server" meta:resourcekey="lcDefaultWebsiteLanguage"
        CssClass="Label" />
    <asp:DropDownList ID="uxDefaultLanguageDrop" runat="server" DataTextField="DisplayName"
        DataValueField="Name" CssClass="fl DropDown">
    </asp:DropDownList>
</div>
<div class="ConfigRow">
<uc1:HelpIcon ID="uxLanguageKeywordBaseCultureHelp" ConfigName="LanguageKeywordBaseCulture" runat="server" />
    <asp:Label ID="lcLanguageKeywordBaseCulture" runat="server" meta:resourcekey="lcLanguageKeywordBaseCulture"
        CssClass="Label" />
    <asp:DropDownList ID="uxLanguageKeywordBaseCultureDrop" runat="server" DataTextField="DisplayName"
        DataValueField="Name" CssClass="fl DropDown" AppendDataBoundItems="true">
        <asp:ListItem Value="">(None)</asp:ListItem>
    </asp:DropDownList>
</div>
