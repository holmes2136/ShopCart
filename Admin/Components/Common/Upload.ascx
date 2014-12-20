<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="AdminAdvanced_Components_Common_Upload" %>
<asp:Panel ID="uxUploadPanel" runat="server">
    <asp:Panel ID="uxRowDestinationPanel" runat="server">
        <asp:Panel ID="uxRowDestinationLabel" runat="server" CssClass="CommonRowStyle">
            <asp:Label ID="lcDestination" runat="server" meta:resourcekey="lcDestination" CssClass="uploadBulletLabel"></asp:Label>
            <asp:TextBox ID="uxPathDestinationText" runat="server" CssClass="TextBox"></asp:TextBox>
            <div class="fl mgl10">
                <asp:Label ID="uxButtonPlaceHolderLabel" runat="server" Text=""></asp:Label></div>
        </asp:Panel>
        <div class="Clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="uxRowProgressPanel" runat="server" Style="display: none;">
        <asp:Panel ID="uxFileProgressPanelLabel" runat="server" CssClass="Label">
            &nbsp;
        </asp:Panel>
        <asp:Panel ID="uxFileProgressPanel" runat="server" CssClass="fl">
        </asp:Panel>
        <div class="Clear">
        </div>
    </asp:Panel>
</asp:Panel>
