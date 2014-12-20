<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Canonicalization.ascx.cs" Inherits="Admin_Components_SiteConfig_Canonicalization" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>

<div id="uxCanonicalizationEnabledTR" runat="server" class="ConfigRow">
    <uc1:HelpIcon ID="uxCanonicalizationEnabledHelp" ConfigName="CanonicalizationLayout" runat="server" />
    <div class="Label">
        <asp:Label ID="lcCanonicalizationEnabledDisplay" runat="server" meta:resourcekey="lcCanonicalizationEnabledDisplay" 
            CssClass="fl" />
    </div>
    <asp:DropDownList ID="uxCanonicalizationEnabledDrop" runat="server" CssClass="fl DropDown"
        AutoPostBack="true" OnSelectedIndexChanged="uxCanonicalizationEnabledDrop_SelectedIndexChanged">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
<asp:Panel ID="uxCanonicalizationURLTR" runat="server" CssClass="ConfigRow">
    <uc1:HelpIcon ID="uxCanonicalizationURLHelp" ConfigName="CanonicalizationStore" runat="server" />
    <asp:Label ID="lcCanonicalizationURLDisplay" runat="server" meta:resourcekey="lcCanonicalizationURLDisplay"
        CssClass="BulletLabel"></asp:Label>
    <asp:DropDownList ID="uxCanonicalizationURLDrop" runat="server" CssClass="fl DropDown">
    </asp:DropDownList>
</asp:Panel>