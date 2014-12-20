<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponAdd.ascx.cs" Inherits="AdminAdvanced_MainControls_CouponAdd" %>
<%@ Register Src="../Components/CouponDetails.ascx" TagName="CouponDetails"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
   <PlainContentTemplate>
        <uc1:CouponDetails ID="uxCouponDetails" runat="server" />
   </PlainContentTemplate>
</uc1:AdminContent>
