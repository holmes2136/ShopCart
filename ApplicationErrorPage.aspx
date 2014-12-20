<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ApplicationErrorPage.aspx.cs"
    Inherits="ApplicationErrorPage" Title="Error" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ApplicationErrorPage">
        <h4 class="ApplicationErrorPageHeader">
            <asp:Literal ID="uxHeaderLiteral" runat="server" />
        </h4>
        <div class="ApplicationErrorPageContent">
            <asp:Literal ID="uxMessageLiteral" runat="server" />
        </div>
    </div>
</asp:Content>
