<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightProduct.ascx.cs"
    Inherits="Themes_ResponsiveGreen_LayoutControls_RightProduct" %>
<%@ Register Src="~/Components/ProductSpecial.ascx" TagName="ProductSpecial" TagPrefix="uc1" %>
<%@ Register Src="~/Components/VerifyCoupon.ascx" TagName="VerifyCoupon" TagPrefix="uc2" %>
<%@ Register Src="~/Components/ContentMenuNavList.ascx" TagName="ContentMenuNavList"
    TagPrefix="uc3" %>
<%@ Register Src="~/Components/FindGiftRegistry.ascx" TagName="FindGiftRegistry"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/FeaturedMerchants.ascx" TagName="FeaturedMerchants"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/LikeBox.ascx" TagName="LikeBox" TagPrefix="uc7" %>

<%@ Register Src="~/Components/NewArrivalCategory.ascx" TagName="NewArrivalCategory"
    TagPrefix="uc1" %>

<uc1:NewArrivalCategory ID="uxNewArrivalList" runat="server" />

<uc2:VerifyCoupon ID="uxVerifyCoupon" runat="server" />
<uc3:ContentMenuNavList ID="uxContentMenuNavList" runat="server" />
<uc5:FindGiftRegistry ID="uxFindGiftRegistry" runat="server" />
<uc7:LikeBox ID="uxLikeBox" runat="server" />
