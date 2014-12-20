<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayConfig.ascx.cs" 
    Inherits="AdminAdvanced_Components_SiteConfig_EBayConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxeBayConfigDevIDHelp" ConfigName="eBayConfigDevID" runat="server" />
    <asp:Label ID="lceBayConfigDevID" runat="server" meta:resourcekey="lceBayConfigDevID" CssClass="Label"></asp:Label>    
    <asp:TextBox ID="uxeBayConfigDevIDText" runat="server" CssClass="eBayTextBox"></asp:TextBox>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxeBayConfigAppIDHelp" ConfigName="eBayConfigAppID" runat="server" />
    <asp:Label ID="lceBayConfigAppID" runat="server" meta:resourcekey="lceBayConfigAppID" CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxeBayConfigAppIDText" runat="server" CssClass="eBayTextBox"></asp:TextBox>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxeBayConfigCertIDHelp" ConfigName="eBayConfigCertID" runat="server" />
    <asp:Label ID="lceBayConfigCertID" runat="server" meta:resourcekey="lceBayConfigCertID" CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxeBayConfigCertIDText" runat="server" CssClass="eBayTextBox"></asp:TextBox>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxeBayConfigTokenHelp" ConfigName="eBayConfigToken" runat="server" />
    <asp:Label ID="lceBayConfigToken" runat="server" meta:resourcekey="lceBayConfigToken" CssClass="Label"></asp:Label>
    <asp:TextBox ID="uxeBayConfigTokenText" runat="server" CssClass="eBayTextBox"></asp:TextBox>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxeBayConfigListingModeHelp" ConfigName="eBayConfigListingMode" runat="server" />
    <asp:Label ID="lceBayConfigListingMode" runat="server" meta:resourcekey="lceBayConfigListingMode" CssClass="Label"></asp:Label>
    <asp:DropDownList ID="uxeBayConfigListingMode" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="https://api.sandbox.ebay.com/wsapi" Text="Sandbox" />
        <asp:ListItem Value="https://api.ebay.com/wsapi" Text="Production" />
    </asp:DropDownList>
</div>
