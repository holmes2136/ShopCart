<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Right.ascx.cs" Inherits="Blog_Themes_ResponsiveGreen_LayoutControls_Right" %>
<%@ Register Src="~/Blog/Components/Archive.ascx" TagName="Archive" TagPrefix="uc1" %>
<%@ Register Src="~/Blog/Components/RecentPost.ascx" TagName="RecentPost" TagPrefix="uc2" %>
<%@ Register Src="~/Blog/Components/Search.ascx" TagName="Search" TagPrefix="uc3" %>
<%@ Register Src="~/Blog/Components/BlogCategory.ascx" TagName="BlogCategory" TagPrefix="uc4" %>

<uc3:Search ID="SearchBox" runat="server" />
<uc4:BlogCategory ID="ucBlogCategory" runat="server" />
<uc2:RecentPost ID="RecentPostList" runat="server" />
<uc1:Archive ID="ArchiveList" runat="server" />