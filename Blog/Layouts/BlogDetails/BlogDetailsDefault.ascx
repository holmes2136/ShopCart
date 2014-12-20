<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogDetailsDefault.ascx.cs"
    Inherits="Layouts_BlogDetails_BlogDetailsDefault" %>
<%@ Register Src="~/Blog/Components/BlogImage.ascx" TagName="BlogImage" TagPrefix="uc2" %>
<div class="BlogImage">
    <uc2:BlogImage ID="uxBlogImage" runat="server" ImageUrl='<%# Eval( "ImageFile" ) %>'
        Visible='<%# IsImageVisible( Eval( "ImageFile" ) ) %>' />
</div>
<div class="BlogDetailsDefaultPublisherDiv">
    [$PostedBy] <span class="BlogPublisher">
        <%# Eval( "Publisher" )%></span> [$PostedOn]
    <asp:Label ID="uxCreateDate" runat="server" Font-Italic="true" Text='<%# String.Format( "{0:ddd dd MMM, yyyy}", Eval( "CreateDate" ) ) %>'></asp:Label>
</div>
<div class="BlogDetailsSocialButton">
    <div class="BlogDetailsFacebookDiv">
        <iframe id="uxFBLikeButtonFrame" runat="server" src='<%# CreateFacebookLikeUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
            scrolling="no" frameborder="0" allowtransparency="true" style="border: none;
            overflow: hidden; width: 100px; height: 21px"></iframe>
    </div>
    <div class="BlogDetailsGooglePlusDiv">
        <!-- Place this tag in your head or just before your close body tag. -->
        <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
        <!-- Place this tag where you want the +1 button to render. -->
        <div class="g-plusone" data-size="medium" data-href='<%# CreateGooglePlusUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
        </div>
    </div>
</div>
<div class="BlogDetailsDefaultBlogContentDiv">
    <asp:Label ID="uxBlogContent" runat="server" Text='<%# Eval( "BlogContent" ) %>'
        CssClass="BlogDetailsDefaultBlogContentLabel" />
</div>
<div id="uxTagsCategoryDiv" runat="server" class="BlogDetailsDefaultDiv">
    <asp:Panel ID="uxCategoryPanel" runat="server" CssClass="BlogDetailsDefaultCatPanel">
        <asp:Label ID="lblCategory" runat="server" Text="[$Categories]" CssClass="CategoryLabel"></asp:Label>
        <asp:Panel ID="uxCategoryLinkPanel" runat="server" CssClass="BlogDetailsDefaultCatDiv"/>
    </asp:Panel>
    <asp:Panel ID="uxTagsPanel" runat="server" CssClass="BlogDetailsDefaultTagsPanel">
        <asp:Label ID="lblTag" runat="server" Text="[$Tags]" CssClass="TagsLabel"></asp:Label>
        <asp:Panel ID="uxTagsLinkPanel" runat="server" CssClass="BlogDetailsDefaultTagsDiv"/>
    </asp:Panel>
</div>
