<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogoImage.ascx.cs" Inherits="AdminAdvanced_Components_SiteSetup_LogoImage" %>
<%@ Register Src="../Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxLogoImageHelp" ConfigName="LogoImage" runat="server" />
    <asp:Label ID="lcLogoImageLabel" runat="server" meta:resourcekey="lcLogoImageLabel"
        CssClass="Label" />
    <asp:TextBox ID="uxLogoImageText" runat="server" Width="210px" CssClass="TextBox" />
    <asp:LinkButton ID="uxPrimaryUploadLinkButton" runat="server" OnClick="uxPrimaryUploadLinkButton_Click"
        CssClass="fl mgl5">Upload...</asp:LinkButton>
</div>
<uc6:Upload ID="uxLogoImageUpload" runat="server" CheckType="Image" AutoUpdateContentPanel="true"
    CssClass="ConfigRow" ShowControl="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" ShowText="false" />
<div class="ConfigRow">
    <div class="Label">
        &nbsp;</div>
    <div class="input1 fl">
        <vevo:Image ID="uxLogoImage" runat="server" MaximumWidth="400px" MaximumHeight="400px" />
    </div>
    <div class="Clear">
    </div>
</div>
