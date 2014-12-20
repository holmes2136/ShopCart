<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="Catalog.aspx.cs" Inherits="Mobile_Catalog" %>
<%@ Register Src="~/Mobile/Components/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle" id="uxMobileCurrentCategoryMenu" runat="server" visible="false">
        <asp:Label ID="uxMobileCurrentCategoryName" runat="server"></asp:Label>
    </div>
<uc1:QuickSearch ID="uxQuickSearch" runat="server" />
    <asp:Panel ID="uxMobileCatalogControlPanel" runat="server">
    </asp:Panel>
</asp:Content>
