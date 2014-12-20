<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderTitle.ascx.cs" Inherits="AdminAdvanced_Components_Common_HeaderTitle" %>
<%@ Register Src="SiteMap.ascx" TagName="SiteMap" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Domain" %>
<div class="AdminHeaderBar">
    <div class="fl mgl10">
        <uc1:SiteMap ID="SiteMap1" runat="server" />
    </div>
    <div class="fr mgr10 c3 uln fb">
        <asp:Label ID="lcVersionLabel" runat="server" meta:resourcekey="lcVersionLabel" />
        <%= SystemConst.CurrentVevoCartVersionNumber() %>
    </div>
    <div class="Clear">
    </div>
</div>
<div class="Clear">
</div>
<div class="AdminTitleBar">
    <div class="AdminTitleText dn fb c3 fs14">
        <asp:Label ID="uxTitleLabel" runat="server"></asp:Label></div>
    <div class="AdminTitleSeparate dn">
    </div>
    <div class="fl mgl14 c3">
        <uc1:SiteMap ID="uxSiteMap" runat="server" />
    </div>
    <div class="Clear">
    </div>
</div>
