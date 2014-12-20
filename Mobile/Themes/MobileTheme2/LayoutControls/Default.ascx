<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Mobile_Themes_MobileTheme_LayoutControls_Default" %>
<%@ Register Src="../../../Components/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<%@ Register Src="../../../Components/ProductSpecial.ascx" TagName="ProductSpecial" TagPrefix="uc2" %>
<uc1:QuickSearch ID="uxQuickSearch" runat="server" />
<%--<uc2:ProductSpecial ID="ProductSpecial" runat="server" />--%>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <ul class="MobileMenuDefault">
            <li id="liPromotionLink" runat="server"><asp:HyperLink ID="uxPromotionLink" runat="server" CssClass="MobileHyperLink">[$Promotion]</asp:HyperLink></li>
            <li><a href="Catalog.aspx" class="MobileHyperLink">[$Category]</a> </li>
            <li><a href="AdvancedSearch.aspx" class="MobileHyperLink">[$Advanced Search]</a></li>
            <li><a href="Coupon.aspx" class="MobileHyperLink">[$Coupon]</a></li>
        </ul>
    </ContentTemplate>
</asp:UpdatePanel>