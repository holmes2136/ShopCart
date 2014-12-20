<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionUploadRequireItem.ascx.cs"
    Inherits="Admin_Components_Order_ProductOptionUploadRequireItem" %>
<asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="OptionUploadRequireItem">
            <asp:Label ID="uxUploadRQLabel" runat="server" CssClass="OptionUploadRequireItemUploadRQLabel"></asp:Label>
            <div class="CommonOptionItemValidator">
                <div id="uxMessageDiv" visible="false" runat="server">
                    <asp:Label ID="uxUploadRQMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="OptionUploadRequireItemFileUploadDiv">
                <asp:FileUpload ID="uxUploadRQFile" runat="server" CssClass="OptionUploadRequireItemFileUploadWidth" />
                <asp:Button ID="uxUploadButton" runat="server" Text="Upload" OnClick="uxUploadButton_Click" />
            </div>
            <asp:Label ID="uxUploadedFilename" runat="server" ForeColor="Orange"></asp:Label>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="uxUploadButton" />
    </Triggers>
</asp:UpdatePanel>
