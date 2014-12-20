<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="Components_Upload_Upload" %>
<asp:Panel ID="uxUploadPanel" runat="server" CssClass="uxUploadPanelCss">
    <asp:Label ID="uxButtonPlaceHolderLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="uxFileProgressPanel" runat="server">
    </asp:Panel>
    <asp:Button ID="uxCancelButton" runat="server" Text="Cancel" /></asp:Panel>
<asp:HiddenField ID="uxHiddenFileList" runat="server" />
