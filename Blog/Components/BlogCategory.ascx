<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogCategory.ascx.cs"
    Inherits="Blog_Components_BlogCategory" %>
<%@ Import Namespace="Vevo" %>
<div class="BlogNavList">
    <div class="BlogCategoryBox">
        <div class="BlogSidebarTop">
            <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/BlogTopLeft.gif" runat="server"
                CssClass="BlogSidebarTopImgLeft" />
            <asp:Label ID="uxRecentPostNavTitle" runat="server" Text="[$BlogCategoryTitle]" CssClass="BlogSidebarTopTitle"></asp:Label>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/BlogTopRight.gif" runat="server"
                CssClass="BlogSidebarTopImgRight" />
            <div class="Clear">
            </div>
        </div>
        <div class="BlogSidebarLeft">
            <div class="BlogSidebarRight">
                <asp:DataList ID="uxBlogCategoryList" runat="server" CssClass="BlogNavNormalList">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxCategoryLink" Text='<%# GetBlogCategoryName ( Eval( "Name" ).ToString(), Eval("BlogCategoryID").ToString() ) %>'
                            NavigateUrl='<%# UrlManager.GetBlogCategoryUrl( Eval( "UrlName" ).ToString() ) %>'
                            runat="server"></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle CssClass="BlogNavNormalListLink" />
                </asp:DataList>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="BlogSidebarBottom">
            <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/BlogBottomLeft.gif" runat="server"
                CssClass="BlogSidebarBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/BlogBottomRight.gif"
                runat="server" CssClass="BlogSidebarBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
