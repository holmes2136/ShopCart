<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentPostNavNormalList.ascx.cs"
    Inherits="Blog_Components_RecentPostNavNormalList" %>
<%@ Register Src="~/Blog/Components/BlogImage.ascx" TagName="BlogImage" TagPrefix="uc2" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="True" CssClass="BlogNavNormalList">
    <ItemTemplate>
        <div class="ImageRecentPost" id="uxImageRecentPostDiv" runat="server" visible='<%# !String.IsNullOrEmpty( Eval( "ImageFile" ).ToString())%>'>
            <asp:HyperLink ID="uxBlogImageLink" runat="server" NavigateUrl='<%# GetNavURL( Container.DataItem ) %>'>
                <uc2:BlogImage ID="uxBlogImage" runat="server" ImageUrl='<%# Eval( "ImageFile" ) %>' />
            </asp:HyperLink>
        </div>
        <div class="TitleRecentPost">
            <asp:HyperLink ID="uxBlogTitleLink" runat="server" Text='<%# GetTextName( Container.DataItem  ) %>'
                NavigateUrl='<%# GetNavURL( Container.DataItem ) %>' />
            <div class="DateRecentPost">
                <asp:Label ID="uxBlogCreateDateLabel" runat="server" Text='<%# string.Format("On {0: dddd dd MMM, yyyy}", (DateTime)Eval("CreateDate"))%>' />
            </div>
        </div>
    </ItemTemplate>
    <ItemStyle CssClass="BlogNavNormalListLink" />
</asp:DataList>