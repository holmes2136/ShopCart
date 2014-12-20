<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionUploadItem.ascx.cs"
    Inherits="Components_OptionUploadItem" %>
<asp:UpdatePanel ID="uxUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="OptionUploadItem">
            <asp:Label ID="uxUploadLabel" runat="server" CssClass="OptionUploadItemUploadLabel"></asp:Label>
            <div id="uxMessageDiv" class="CommonOptionItemValidator" visible="false" runat="server">
                <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" />
                <asp:Label ID="uxUploadMessage" runat="server" ForeColor="Red"></asp:Label>
                <div class="UploadValidateDiv">
                </div>
            </div>
            <div class="OptionUploadItemFileUploadDiv">
                <asp:FileUpload ID="uxUploadFile" runat="server" ClientIDMode="Static" CssClass="OptionUploadItemFileUploadWidth" />
                <asp:Button ID="uxUploadButton" runat="server" Text="Upload" OnClick="uxUploadButton_Click" />
            </div>
            <div class="OptionUploadItemFileUploadDiv">
                <asp:Label ID="uxUploadedFilename" runat="server" ForeColor="Orange"></asp:Label>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="uxUploadButton" />
    </Triggers>
</asp:UpdatePanel>
