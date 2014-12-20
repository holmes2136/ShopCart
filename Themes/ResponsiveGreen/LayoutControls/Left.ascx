<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="Themes_ResponsiveGreen_LayoutControls_Left" %>
<%@ Register Src="~/Components/CategoryNavList.ascx" TagName="CategoryNavList" TagPrefix="uc" %>
<%@ Register Src="~/Components/DepartmentNavList.ascx" TagName="DepartmentNavList"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/ContentMenuNavList.ascx" TagName="ContentMenuNavList"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/FacetSearch.ascx" TagName="FacetSearch" TagPrefix="uc" %>
<%@ Register Src="~/Components/SpecialOffer.ascx" TagName="SpecialOffer" TagPrefix="uc" %>
<%@ Register Src="~/Components/FreeShippingAd.ascx" TagName="FreeShippingAd" TagPrefix="uc" %>
<%@ Register Src="~/Components/LivePerson.ascx" TagName="LivePerson" TagPrefix="uc" %>
<%@ Register Src="~/Components/ManufacturerNavList.ascx" TagName="ManufacturerNavList"
    TagPrefix="uc" %>
<%@ Register Src="~/Components/NewsEvent.ascx" TagName="NewsEvent" TagPrefix="uc" %>
<%@ Register Src="~/Components/LikeBox.ascx" TagName="LikeBox" TagPrefix="uc" %>
<%@ Register Src="~/Components/ProductSpecial.ascx" TagName="ProductSpecial" TagPrefix="uc" %>
<%@ Register Src="~/Components/SecureShoppingAd.ascx" TagName="SecureShoppingAd"
    TagPrefix="uc" %>
<uc:ProductSpecial ID="uxProductSpecial" runat="server" />
<uc:CategoryNavList ID="CategoryNavList" runat="server" />
<uc:FacetSearch ID="FacetSearch" runat="server" />
<uc:ManufacturerNavList ID="uxManufacturerNavList" runat="server" />
<uc:DepartmentNavList ID="DepartmentNavList" runat="server" />
<uc:ContentMenuNavList ID="uxContentMenuNavList" runat="server" />
<uc:NewsEvent ID="uxNewEvent" runat="server" />
<uc:SpecialOffer ID="uxSpecialOffer" runat="server" />
<uc:FreeShippingAd ID="uxFreeShippingAd" runat="server" />
<uc:SecureShoppingAd ID="uxSecureShoppingAd" runat="server" />
<uc:LivePerson ID="uxLivePerson" runat="server" />
<uc:LikeBox ID="uxLikeBox" runat="server" />
