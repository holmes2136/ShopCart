<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArchiveNavNormalList.ascx.cs"
    Inherits="Blog_Components_ArchiveNavNormalList" %>
<asp:DataList ID="uxList" runat="server" ShowFooter="True" CssClass="BlogNavNormalList">
    <ItemTemplate>
        <asp:HyperLink ID="hyperLink" runat="server" Text='<%# GetTextName( Container.DataItem  ) %>'
            NavigateUrl='<%# Vevo.UrlManager.GetBlogListUrl( GetNavURL( Container.DataItem  ) ) %>' />
    </ItemTemplate>
    <ItemStyle CssClass="BlogNavNormalListLink" />
    <FooterTemplate>
        <asp:HyperLink ID="uxMoreLink" runat="server" NavigateUrl="~/Blog/ArchiveList.aspx"
            Text="[$More]" />
    </FooterTemplate>
    <FooterStyle CssClass="BlogNavNormalListMoreLink" />
</asp:DataList>