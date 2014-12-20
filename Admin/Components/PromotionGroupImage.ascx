<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionGroupImage.ascx.cs"
    Inherits="Admin_Components_PromotionGroupImage" %>
<%@ Register Src="Common/Upload.ascx" TagName="Upload" TagPrefix="uc6" %>
<asp:Label ID="uxPromotionImageLabel" runat="server" meta:resourcekey="uxPromotionImageLabel"
    CssClass="Label" />
<asp:TextBox ID="uxPromotionImageText" runat="server" Width="210px" CssClass="TextBox" />
<asp:LinkButton ID="uxPrimaryUploadLinkButton" runat="server" OnClick="uxPrimaryUploadLinkButton_Click"
    CssClass="fl mgl5">Upload...</asp:LinkButton>
<uc6:Upload ID="uxPromotionImageUpload" runat="server" CheckType="Image" AutoUpdateContentPanel="true"
    CssClass="ConfigRow" ShowControl="false" ButtonImage="SelectImages.png" ButtonWidth="105"
    ButtonHeight="22" ShowText="false" />
<div class="ConfigRow">
    <div class="Label">
        &nbsp;</div>
    <div class="input1 fl">
        <vevo:Image ID="uxPromotionImage" runat="server" MaximumWidth="200px" MaximumHeight="200px" />
    </div>
    <div class="Clear">
    </div>
</div>