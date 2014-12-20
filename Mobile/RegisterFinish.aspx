<%@ Page Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="RegisterFinish.aspx.cs" Inherits="Mobile_RegisterFinish" Title="[$RegisterFinish]" %>

<%@ Register Src="~/Mobile/Components/MobileMessage.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
    </div>
    <div class="MobileShoppingCartMessage MobileCommonBox">
        <uc1:Message ID="uxMessage" runat="server" />
    </div>
</asp:Content>
