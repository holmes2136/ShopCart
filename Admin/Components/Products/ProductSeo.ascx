<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSeo.ascx.cs" Inherits="AdminAdvanced_Components_Products_ProductSeo" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc1" %>
<div class="ProductDetailsRow">
    <div class="Label" style="line-height: 18px;">
        <asp:Label ID="lcMetaKeyword" runat="server" meta:resourcekey="lcMetaKeyword"></asp:Label>
        <asp:Label ID="lcMetaKeywordComment" runat="server" meta:resourcekey="lcMetaKeywordComment"></asp:Label></div>
    <asp:TextBox ID="uxMetaKeywordText" runat="server" Width="210px" CssClass="TextBox"
        TextMode="MultiLine" Rows="1" />
    <uc1:LanaguageLabelPlus ID="LanaguageLabelPlus1" runat="server" />
    <asp:CheckBox ID="uxMetaKeywordCheck" runat="server" OnCheckedChanged="uxMetaKeywordCheck_OnCheckedChanged"
        AutoPostBack="true" />
    <asp:Label ID="uxMetaKeywordCheckLabel" runat="server" meta:resourcekey="uxMetaKeywordCheckLabel"></asp:Label>
    <div class="Clear">
    </div>
</div>
<div class="ProductDetailsRow">
    <div class="Label" style="line-height: 18px;">
        <asp:Label ID="lcMetaDescription" runat="server" meta:resourcekey="lcMetaDescription"></asp:Label>
        <asp:Label ID="lcMetaDescriptionComment" runat="server" meta:resourcekey="lcMetaDescriptionComment"></asp:Label>
    </div>
    <asp:TextBox ID="uxMetaDescriptionText" runat="server" Width="210px" CssClass="TextBox"
        TextMode="MultiLine" Rows="4" />
    <uc1:LanaguageLabelPlus ID="LanaguageLabelPlus2" runat="server" />
    <asp:CheckBox ID="uxMetaDescriptionCheck" runat="server" OnCheckedChanged="uxMetaDescriptionCheck_OnCheckedChanged"
        AutoPostBack="true" />
    <asp:Label ID="uxMetaDesciptionCheckLabel" runat="server" meta:resourcekey="uxMetaDesciptionCheckLabel"></asp:Label>
    <div class="Clear">
    </div>
</div>
