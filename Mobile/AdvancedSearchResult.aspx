<%@ Page Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true" 
    CodeFile="AdvancedSearchResult.aspx.cs" Inherits="Mobile_AdvancedSearchResult" Title="[$Title]" %>

<%@ Register Src="~/Mobile/Components/SearchResult.ascx" TagName="SearchResult" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" Runat="Server">

<div class="MobileTitle">
    <asp:Label ID="uxDefaultTitle" runat="server">
        [$AdvancedSearchResult] </asp:Label>
</div>
<div>
    <uc1:SearchResult ID="uxSearchResult" runat="server" />
</div>

</asp:Content>

