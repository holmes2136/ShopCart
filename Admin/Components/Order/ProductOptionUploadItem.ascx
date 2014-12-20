<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductOptionUploadItem.ascx.cs"
    Inherits="Admin_Components_Order_ProductOptionUploadItem" %>
<asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="OptionUploadItem">
            <asp:Label ID="uxUploadLabel" runat="server" CssClass="OptionUploadItemUploadLabel"></asp:Label>
            <div class="CommonOptionItemValidator">
                <div id="uxMessageDiv" visible="false" runat="server">
                    <asp:Label ID="uxUploadMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="OptionUploadItemFileUploadDiv">
                <asp:FileUpload ID="uxUploadFile" runat="server" CssClass="OptionUploadItemFileUploadWidth" />
                <asp:Button ID="uxUploadButton" runat="server" Text="Upload" OnClick="uxUploadButton_Click" />
            </div>
        </div>
        <asp:Label ID="uxUploadedFilename" runat="server" ForeColor="Orange"></asp:Label>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="uxUploadButton" />
    </Triggers>
</asp:UpdatePanel>
