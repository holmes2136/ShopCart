<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Themes_ResponsiveGreen_LayoutControls_Default" %>
<%@ Register Src="~/Components/StoreBanner.ascx" TagName="StoreBanner" TagPrefix="uc" %>
<%@ Register Src="../Components/NewArrival.ascx" TagName="NewArrival" TagPrefix="uc" %>
<%@ Register Src="../Components/RandomProduct.ascx" TagName="RandomProduct" TagPrefix="uc" %>
<%@ Register Src="../Components/PromotionGroup.ascx" TagName="PromotionGroup" TagPrefix="uc" %>
<%@ Register Src="../Components/ProductBestSelling.ascx" TagName="ProductBestSelling"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/IntroductionMessage.ascx" TagName="IntroductionMessage"
    TagPrefix="uc" %>
<%@ Register Src="../Components/NewsEvent.ascx" TagName="NewsEvent" TagPrefix="uc" %>
<div class="row">
    <div class="twelve columns">
        <uc:StoreBanner ID="uxStoreBanner" runat="server" />
    </div>
</div>
<div class="row">
    <div class="twelve columns">
        <uc:IntroductionMessage ID="uxIntroductionMessage" runat="server" />
    </div>
</div>
<div class="row">
    <div class="twelve columns">
        <uc:NewArrival ID="uxNewArrival" runat="server" />
    </div>
</div>
<uc:PromotionGroup ID="uxPromotionGroup" runat="server" />
<div class="row">
    <div class="twelve columns">
        <uc:RandomProduct ID="uxRandomProduct" runat="server" />
    </div>
</div>
<div class="row">
    <div class="twelve columns">
        <uc:ProductBestSelling ID="uxProductBestSelling" runat="server" />
    </div>
</div>
<div class="row">
    <div class="twelve columns">
        <uc:NewsEvent ID="uxNewsEvent" runat="server" />
    </div>
</div>
