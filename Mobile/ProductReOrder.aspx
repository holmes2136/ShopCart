<%@ Page Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="ProductReOrder.aspx.cs" Inherits="Mobile_ProductReOrder" Title="[$Title]" %>

<%@ Register Src="Components/ProductReOrder.ascx" TagName="ProductReOrder" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        <asp:Label ID="uxDefaultTitle" runat="server">
        [$Head]</asp:Label>
    </div>
    <div class="MobileCommonBox">
        <uc1:ProductReOrder ID="uxProductReorder" runat="server" />
    </div>
</asp:Content>
