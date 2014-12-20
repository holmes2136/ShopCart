<%@ Page Title="[$Title]" Language="C#" MasterPageFile="~/Blog/Blog.master" AutoEventWireup="true"
    CodeFile="CategoryList.aspx.cs" Inherits="Blog_CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="BlogList">
        <div class="BlogListTop">
            <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                CssClass="BlogListTopImgLeft" />
            <asp:Label ID="uxDefaultTitle" runat="server" CssClass="BlogDetailsTopTitle" Text="[$CategoryHeader]"></asp:Label>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                runat="server" CssClass="BlogListTopImgRight" />
            <div class="Clear">
            </div>
        </div>
        <div class="BlogListLeft">
            <div class="BlogListRight">
                <asp:Panel ID="uxBlogListPanel" runat="server">
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="BlogListBottom">
            <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                runat="server" CssClass="BlogListBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                runat="server" CssClass="BlogListBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
</asp:Content>
