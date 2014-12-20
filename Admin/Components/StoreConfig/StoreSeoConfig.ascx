<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreSeoConfig.ascx.cs"
    Inherits="Admin_Components_StoreConfig_StoreSeoConfig" %>
<%@ Register Src="../Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="../LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus" TagPrefix="uc5" %>
<div class="CommonConfigTitle  mgt0">
<asp:Label ID="uxProductSeoTitle" runat="server" meta:resourcekey="uxProductSeoTitle"/></div>
<div class="ConfigRow">
    <div class="Commonsubtitle">
        <asp:Label ID="uxProductVariablesNoteLabel" runat="server" meta:resourcekey="uxProductVariablesNoteLabel" />
    </div>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxDefaultProductPageTitleHelp" ConfigName="DefaultProductPageTitle"
        runat="server" />
    <asp:Label ID="uxDefaultProductPageTitle" runat="server" meta:resourcekey="uxDefaultProductPageTitle"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultProductPageTitleText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus1" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultProductMetaKeywordHelp" ConfigName="DefaultProductMetaKeyword"
        runat="server" />
    <asp:Label ID="uxDefaultProductMetaKeyword" runat="server" meta:resourcekey="uxDefaultProductMetaKeyword"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultProductMetaKeywordText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus2" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultProductMetaDescriptionHelp" ConfigName="DefaultProductMetaDescription"
        runat="server" />
    <asp:Label ID="uxDefaultProductMetaDescription" runat="server" meta:resourcekey="uxDefaultProductMetaDescription"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultProductMetaDescriptionText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus3" runat="server" />
</div>
<div class="CommonConfigTitle">
<asp:Label ID="uxCategorySeoTitle" runat="server" meta:resourcekey="uxCategorySeoTitle" /></div>
<div class="ConfigRow">
    <div class="Commonsubtitle">
        <asp:Label ID="uxCategoryVariablesNoteLabel" runat="server" meta:resourcekey="uxCategoryVariablesNoteLabel" />
    </div>
</div>
<div class="ConfigRow">
    <uc1:HelpIcon ID="uxDefaultCategoryPageTitleHelp" ConfigName="DefaultCategoryPageTitle"
        runat="server" />
    <asp:Label ID="uxDefaultCategoryPageTitle" runat="server" meta:resourcekey="uxDefaultCategoryPageTitle"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultCategoryPageTitleText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultCategoryMetaKeywordHelp" ConfigName="DefaultCategoryMetaKeyword"
        runat="server" />
    <asp:Label ID="uxDefaultCategoryMetaKeyword" runat="server" meta:resourcekey="uxDefaultCategoryMetaKeyword"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultCategoryMetaKeywordText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus5" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultCategoryMetaDescriptionHelp" ConfigName="DefaultCategoryMetaDescription"
        runat="server" />
    <asp:Label ID="uxDefaultCategoryMetaDescription" runat="server" meta:resourcekey="uxDefaultCategoryMetaDescription"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultCategoryMetaDescriptionText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus6" runat="server" />
</div>
<div class="CommonConfigTitle">
<asp:Label ID="uxDepartmentSeoTitle" runat="server" meta:resourcekey="uxDepartmentSeoTitle" /></div>
<div class="ConfigRow">
    <div class="Commonsubtitle">
        <asp:Label ID="uxDepartmentVariablesNoteLabel" runat="server" meta:resourcekey="uxDepartmentVariablesNoteLabel" />
    </div>
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultDepartmentPageTitleHelp" ConfigName="DefaultDepartmentPageTitle"
        runat="server" />
    <asp:Label ID="uxDefaultDepartmentPageTitle" runat="server" meta:resourcekey="uxDefaultDepartmentPageTitle"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultDepartmentPageTitleText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus7" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultDepartmentMetaKeywordHelp" ConfigName="DefaultDepartmentMetaKeyword"
        runat="server" />
    <asp:Label ID="uxDefaultDepartmentMetaKeyword" runat="server" meta:resourcekey="uxDefaultDepartmentMetaKeyword"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultDepartmentMetaKeywordText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus8" runat="server" />
</div>
<div class="ConfigRow mgt10">
    <uc1:HelpIcon ID="uxDefaultDepartmentMetaDescriptionHelp" ConfigName="DefaultDepartmentMetaDescription"
        runat="server" />
    <asp:Label ID="uxDefaultDepartmentMetaDescription" runat="server" meta:resourcekey="uxDefaultDepartmentMetaDescription"
        CssClass="Label" />
    <asp:TextBox ID="uxDefaultDepartmentMetaDescriptionText" runat="server" CssClass="TextBox mgt10"
        Width="210px" Height="40px" TextMode="MultiLine" />
    <uc5:LanaguageLabelPlus ID="uxPlus9" runat="server" />
</div>
