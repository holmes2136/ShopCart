<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="PromotionDetail.aspx.cs" Inherits="Mobile_PromotionDetail" %>

<%@ Register Src="~/Mobile/Components/PromotionItem.ascx" TagName="PromotionItem" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:PromotionItem ID="uxItem" runat="server" PromotionGroupID="1" />
</asp:Content>
