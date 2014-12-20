<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionUploadRequireItem.ascx.cs"
    Inherits="Components_OptionUploadRequireItem" %>
<asp:UpdatePanel ID="uxUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="OptionUploadRequireItem">
            <asp:Label ID="uxUploadRQLabel" runat="server" CssClass="OptionUploadRequireItemUploadRQLabel"></asp:Label>
        </div>
        <div class="OptionUploadRequireItem">
            <div id="uxMessageDiv" class="CommonOptionItemValidator" visible="false" runat="server">
                <img src="Images/Design/Bullet/RequiredFillBullet_Down.gif" /><asp:Label ID="uxUploadRQMessage"
                    runat="server" ForeColor="Red"></asp:Label>
                <div class="UploadValidateDiv">
                </div>
            </div>
        </div>
        <div class="OptionUploadRequireItem">
            <div class="OptionUploadRequireItemFileUploadDiv">
                <asp:FileUpload ID="uxUploadRQFile" runat="server" CssClass="OptionUploadRequireItemFileUploadWidth" />
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
