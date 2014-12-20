<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuAccording.ascx.cs"
    Inherits="AdminAdvanced_Components_Menu_MenuAccording" %>
<div class="MenuAccordingFullTitle b4 c2 uln fb">
    <vevo:AdvancedLinkButton ID="uxMenuTitleLinkButton" runat="server" Text=""
                        CssClassBegin="MenuAccordingTitle fl mgl5" OnClickGoTo="None"
                        ValidationGroup="uxLogin" CssClass="c2 fb"></vevo:AdvancedLinkButton>
<%--    <asp:LinkButton ID="uxMenuTitleLinkButton" runat="server" CssClass="MenuAccordingTitle fl mgl5"></asp:LinkButton>--%>
    <asp:Image ID="uxShowImage" runat="server" SkinId="MenuShowImage" />
    <asp:Image ID="uxHideImage" runat="server" SkinId="MenuHideImage" />
    <div class="Clear">
    </div>
</div>
<asp:Panel ID="uxMenuPanel" runat="server" CssClass="MenuAccordingList b2 c3 uln fb">
</asp:Panel>
