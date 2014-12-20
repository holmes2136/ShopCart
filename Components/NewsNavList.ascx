<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsNavList.ascx.cs" Inherits="Components_NewsNavList" %>
<%@ Register Src="VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<div class="NewsNavList">
    <div class="SidebarTop">
        <asp:Label ID="uxDepartmentNavTitle" runat="server" Text="[$LatestNews]" CssClass="SidebarTopTitle"></asp:Label>
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxList" runat="server" ShowFooter="true" CssClass="DepartmentNavNormalList">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# Vevo.UrlManager.GetNewsUrl( Eval( "NewsID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                        Text='<%# Eval("Topic") %>'>
                    </asp:HyperLink>
                </ItemTemplate>
                <FooterTemplate>
                    <div class="NewsEventMore" id="uxNewsMoreDiv" runat="server">
                        <a href="News.aspx" class="CssNoLine">
                            <asp:Label ID="uxMoreLabel" runat="server" Text="[$More]" CssClass="NewsEventMoreLabel" />
                        </a>
                    </div>
                </FooterTemplate>
            </asp:DataList>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <div class="Clear">
        </div>
    </div>
</div>
