<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingTracking.ascx.cs"
    Inherits="AdminAdvanced_Components_SiteConfig_ShippingTracking" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<asp:Panel ID="uxTrackingWarningTR" runat="server">
    <div class="ConfigRow">
        <asp:Label ID="lcTrackingUrlWarning" runat="server" meta:resourcekey="lcTrackingUrlWarning" CssClass="CommentLabel">
        </asp:Label></div>
</asp:Panel>
<asp:Panel ID="uxUpsTrackingUrlTR" runat="server">
    <div class="ConfigRow">
     <uc1:HelpIcon ID="uxUpsTrackUrlHelp" ConfigName="UpsTrackingUrl" runat="server" />
        <asp:Label ID="lcUpsTrackUrl" runat="server" meta:resourcekey="lcUpsTrackUrl" CssClass="Label"></asp:Label>
        <asp:TextBox ID="uxUpsTrackUrlText" runat="server" CssClass="TrackingTextBox" /></div>
</asp:Panel>
<asp:Panel ID="uxFedExTrackingUrlTR" runat="server">
    <div class="ConfigRow">
     <uc1:HelpIcon ID="uxFedExTrackUrlHelp" ConfigName="FedExTrackingUrl" runat="server" />
        <asp:Label ID="lcFedExTrackUrl" runat="server" meta:resourcekey="lcFedExTrackUrl"
            CssClass="Label"></asp:Label>
        <asp:TextBox ID="uxFedExTrackUrlText" runat="server" CssClass="TrackingTextBox" /></div>
</asp:Panel>
<asp:Panel ID="uxUspsTrackingUrlTR" runat="server">
    <div class="ConfigRow">
     <uc1:HelpIcon ID="uxUspsTrackUrlHelp" ConfigName="UspsTrackingUrl" runat="server" />
        <asp:Label ID="lcUspsTrackUrl" runat="server" meta:resourcekey="lcUspsTrackUrl" CssClass="Label"></asp:Label>
        <asp:TextBox ID="uxUspsTrackUrlText" runat="server" CssClass="TrackingTextBox" /></div>
</asp:Panel>
<asp:Panel ID="uxOtherTrackingUrlTR" runat="server">
    <div class="ConfigRow">
     <uc1:HelpIcon ID="uxOtherTrackUrlHelp" ConfigName="OtherTrackingUrl" runat="server" />
        <asp:Label ID="lcOtherTrackUrl" runat="server" meta:resourcekey="lcOtherTrackUrl"
            CssClass="Label"></asp:Label>
        <asp:TextBox ID="uxOtherTrackUrlText" runat="server" CssClass="TrackingTextBox" /></div>
</asp:Panel>

<asp:HiddenField ID="uxStatusHidden" runat="server" />
