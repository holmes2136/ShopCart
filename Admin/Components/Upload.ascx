<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="Components_Upload" %>
<asp:Panel ID="uxFileUploadPanel" runat="server">
    <asp:Panel ID="uxRowUploadFilePanel" runat="server" CssClass="CommonRowStyle">
        <asp:Panel ID="uxRowUploadLabel" runat="server" CssClass="Label">
            <asp:Label ID="lcFile" runat="server" meta:resourcekey="lcFile"></asp:Label>
        </asp:Panel>
        <asp:FileUpload ID="uxImageFileUpload" runat="server" CssClass="fl" />
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxRowDestinationPanel" runat="server" CssClass="CommonRowStyle">
        <asp:Panel ID="uxRowDestinationLabel" runat="server" CssClass="Label">
            <asp:Label ID="lcDestination" runat="server" meta:resourcekey="lcDestination"></asp:Label>
        </asp:Panel>
        <asp:TextBox ID="uxDestinationText" runat="server" CssClass="fl TextBox"></asp:TextBox>
        <vevo:AdvanceButton ID="uxUploadButton" runat="server" meta:resourcekey="uxUploadButton"
            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClickGoTo="None"
            ValidationGroup="ValidSiteSetup"></vevo:AdvanceButton>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxRowMessageErrorPanel" runat="server" CssClass="CommonRowStyle" Visible="false">
        <asp:Panel ID="uxMessageErrorLabel" runat="server" CssClass="Label">
            &nbsp;
        </asp:Panel>
        <asp:Label ID="uxErrorLabel" runat="server" CssClass="validator1 fl"></asp:Label>
        <div class="Clear">
        </div>
    </asp:Panel>
</asp:Panel>
