<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Seo.ascx.cs" Inherits="AdminAdvanced_Components_SiteConfig_Seo" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxUseSimpleCatalogUrl" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxUseSimpleCatalogUrlHelp" ConfigName="UseSimpleCatalogUrl" runat="server" />
    <div class="Label">
        <asp:Label ID="lcUseSimpleCatalogUrl" runat="server" meta:resourcekey="lcUseSimpleCatalogUrl"
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxUseSimpleCatalogUrlDrop" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="uxUseSimpleCatalogUrlDrop_SelectedIndexChanged" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</asp:Panel>
<asp:Panel ID="uxUrlCultureNameTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxUrlCultureNameHelp" ConfigName="UrlCultureName" runat="server" />
    <div class="BulletLabel">
        <asp:Label ID="lcUrlCultureName" runat="server" meta:resourcekey="lcUrlCultureName"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxUrlCultureNameDrop" runat="server" DataTextField="DisplayName"
        DataValueField="Name" CssClass="DropDown fl">
    </asp:DropDownList>
    <asp:Label ID="lcUrlCultureNameComment" runat="server" meta:resourcekey="lcUrlCultureNameComment"
        CssClass="CommentLabel mgl5" />
</asp:Panel>
