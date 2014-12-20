<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailNotification.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_EmailNotification" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxNewRegistrationHelp" ConfigName="NotifyNewRegistration" runat="server" />
    <div class="Label">
        <asp:Label ID="lcNewRegistration" runat="server" meta:resourcekey="lcNewRegistration"
            CssClass="fl"></asp:Label>
    </div>
    <asp:DropDownList ID="uxNewRegistrationDrop" runat="server" CssClass="fl DropDown">
        <asp:ListItem Value="True" Text="Yes" />
        <asp:ListItem Value="False" Text="No" />
    </asp:DropDownList>
</div>
