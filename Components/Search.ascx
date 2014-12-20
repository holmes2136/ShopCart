<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Themes_ResponsiveGreen_Components_Search" %>
<%@ Register Src="QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc" %>
<div class="Search" id="uxSearchDiv" runat="server">
    <div class="SearchTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/SearchTopLeft.gif" runat="server"
            CssClass="SearchTopImgLeft" />
        <asp:Label ID="uxSearchTitle" runat="server" Text="[$Search]" CssClass="SearchTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/SearchTopRight.gif" runat="server"
            CssClass="SearchTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SearchLeft">
        <div class="SearchRight">
            <uc:QuickSearch ID="uxQuickSearch" runat="server" />
            <asp:HyperLink ID="uxAdvanceSearchLink" runat="server" NavigateUrl="~/AdvancedSearch.aspx"
                CssClass="SearchAdvancedLink" Text="[$Advanced Search]" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SearchBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/SearchBottomLeft.gif"
            runat="server" CssClass="SearchBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/SearchBottomRight.gif"
            runat="server" CssClass="SearchBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
