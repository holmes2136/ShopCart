<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingTracking.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingTracking" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<uc1:AdminContent ID="uxContentTemplate" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <TopContentBoxTemplate>
        <asp:Panel ID="uxTrackingUrlTR" runat="server" CssClass="fb c10 ac">
            <asp:Label ID="lcTrackingUrl" runat="server" meta:resourcekey="lcTrackingUrl"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="uxUpsTrackingUrlTR" runat="server" CssClass="mgt10">
            <div class="fb c10 mgl30">
                <asp:Label ID="lcUpsTrackUrl" runat="server" meta:resourcekey="lcUpsTrackUrl" CssClass="bullet1"></asp:Label></div>
            <asp:TextBox ID="uxUpsTrackUrlText" runat="server" CssClass="w80p TextBox mgl40" />
        </asp:Panel>
        <asp:Panel ID="uxFedExTrackingUrlTR" runat="server" CssClass="mgt10">
            <div class="fb c10 mgl30">
                <asp:Label ID="lcFedExTrackUrl" runat="server" meta:resourcekey="lcFedExTrackUrl"
                    CssClass="bullet1"></asp:Label></div>
            <asp:TextBox ID="uxFedExTrackUrlText" runat="server" CssClass="w80p TextBox mgl40" />
        </asp:Panel>
        <asp:Panel ID="uxUspsTrackingUrlTR" runat="server" CssClass="mgt10">
            <div class="fb c10 mgl30">
                <asp:Label ID="lcUspsTrackUrl" runat="server" meta:resourcekey="lcUspsTrackUrl" CssClass="bullet1"></asp:Label></div>
            <asp:TextBox ID="uxUspsTrackUrlText" runat="server" CssClass="w80p TextBox mgl40">
            </asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="uxOtherTrackingUrlTR" runat="server" CssClass="mgt10">
            <div class="fb c10 mgl30">
                <asp:Label ID="lcOtherTrackUrl" runat="server" meta:resourcekey="lcOtherTrackUrl"
                    CssClass="bullet1"></asp:Label></div>
            <asp:TextBox ID="uxOtherTrackUrlText" runat="server" CssClass="w80p TextBox mgl40" />
        </asp:Panel>
        <asp:Panel ID="uxTrackingWarningTR" runat="server" CssClass="mgb20 it mgl40 mgt10">
            <asp:Label ID="lcTrackingUrlWarning" runat="server" meta:resourcekey="lcTrackingUrlWarning">
            </asp:Label>
        </asp:Panel>
        <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton" CssClassBegin="fr mgt20 mgr10"
            CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
            OnClickGoTo="Top"></vevo:AdvanceButton>
    </TopContentBoxTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
