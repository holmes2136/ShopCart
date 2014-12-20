<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftProduct.ascx.cs" Inherits="Themes_ResponsiveGreen_LayoutControls_LeftProduct" %>
<%@ Register Src="~/Components/CategoryNavList.ascx" TagName="CategoryNavList" TagPrefix="uc" %>
<%@ Register Src="~/Components/DepartmentNavList.ascx" TagName="DepartmentNavList"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/FacetSearch.ascx" TagName="FacetSearch" TagPrefix="uc" %>
<%@ Register Src="~/Components/ManufacturerNavList.ascx" TagName="ManufacturerNavList"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/ComparisonListBox.ascx" TagName="CompararionListBox"
    TagPrefix="uc4" %>
    <%@ Register Src="~/Components/ShoppingCartDetails.ascx" TagName="ShoppingCart" TagPrefix="uc8" %>
<%@ Register Src="../Components/RecentlyViewProduct.ascx" TagName="RecentlyViewed"
    TagPrefix="uc3" %>    
<%@ Register Src="~/Components/VerifyCoupon.ascx" TagName="VerifyCoupon" TagPrefix="uc" %>
<uc:CategoryNavList ID="uxCategoryNavList" runat="server" />
<uc:DepartmentNavList ID="uxDepartmentNavList" runat="server" />
<uc:ManufacturerNavList ID="uxManufacturerNavList" runat="server" />
<uc:FacetSearch ID="FacetSearch" runat="server" />
<uc:VerifyCoupon ID="uxVerifyCoupon" runat="server" />
<asp:UpdatePanel ID="uxMiniCartUpdatePanel" runat="server">
    <ContentTemplate>
        <uc8:ShoppingCart ID="uxShoppingCart" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="uxRecentlyViewedUpdatePanel" runat="server">
    <ContentTemplate>
        <uc3:RecentlyViewed ID="uxRecentlyViewed" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="uxCompararionUpdatePanel" runat="server">
    <ContentTemplate>
        <uc4:CompararionListBox ID="uxCompararionListBox" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>