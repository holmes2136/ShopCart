<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="PromotionDetail.aspx.cs"
    Inherits="PromotionDetail" Title="[$Title]" %>

<%@ Register Src="~/Components/PromotionItem.ascx" TagName="PromotionItem" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <uc1:PromotionItem ID="uxItem" runat="server" PromotionGroupID="1" />
</asp:Content>
