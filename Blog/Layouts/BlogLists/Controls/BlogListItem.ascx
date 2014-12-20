<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogListItem.ascx.cs"
    Inherits="Layouts_BlogLists_Controls_BlogListItem" %>
<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="~/Blog/Components/BlogImage.ascx" TagName="BlogImage" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo" %>
<div class="BlogListItem">
    <table class="BlogListItemTable">
        <tr>
            <td class="BlogListItemDetailsColumn">
                <div class="BlogListItemDetailsDiv">
                    <div class="BlogImage">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="BlogListItemNameLink" NavigateUrl='<%# UrlManager.GetBlogUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                            <uc2:BlogImage ID="uxBlogImage" runat="server" ImageUrl='<%# Eval( "ImageFile" ) %>' />
                        </asp:HyperLink>
                    </div>
                    <div class="BlogListItemBlogTitleDiv">
                        <asp:HyperLink ID="uxBlogTitle" runat="server" CssClass="BlogListItemNameLink" NavigateUrl='<%# UrlManager.GetBlogUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>                            
                            <%# Eval("BlogTitle") %>
                        </asp:HyperLink>
                        <asp:HiddenField ID="uxBlogID" runat="server" Value='<%# Eval("BlogID") %> ' />
                    </div>
                    <div class="BlogListItemPublisherDiv">
                        <div class="BlogListItemPublisher">
                            [$PostedBy] <span class="BlogPublisher">
                                <%# Eval( "Publisher" )%></span> [$PostedOn]
                            <asp:Label ID="uxCreateDate" runat="server" Font-Italic="true" Text='<%# String.Format( "{0:ddd dd MMM, yyyy}", Eval( "CreateDate" ) ) %>'
                                CssClass="
                        BlogPublishDate"></asp:Label></div>
                        <div class="BlogListItemSocialButton">
                            <div class="BlogListItemFacebookDiv">
                                <iframe id="uxFBLikeButtonFrame" runat="server" src='<%# CreateFacebookLikeUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                    scrolling="no" frameborder="0" allowtransparency="true" style="border: none;
                                    overflow: hidden; width: 100px; height: 21px"></iframe>
                            </div>
                            <div class="BlogListItemGooglePlusDiv">
                                <!-- Place this tag in your head or just before your close body tag. -->
                                <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
                                <!-- Place this tag where you want the +1 button to render. -->
                                <div class="g-plusone" data-size="medium" data-href='<%# CreateGooglePlusUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="BlogListItemShortContentDiv">
                        <%# LimitDisplayCharactor(Eval("ShortContent"), 255)%>
                    </div>
                    <div id="uxTagsCategoryDiv" runat="server" class="BlogDetailsDefaultDiv">
                        <asp:Panel ID="uxCategoryListPanel" runat="server" CssClass="BlogDetailsDefaultCatPanel">
                            <asp:Label ID="lblCategory" runat="server" Text="[$Categories]" CssClass="CategoryLabel"></asp:Label>
                            <asp:Panel ID="uxCategoryLinkPanel" runat="server" CssClass="BlogDetailsDefaultCatDiv" />
                        </asp:Panel>
                        <asp:HiddenField ID="uxCategoryIDsHidden" Value='<%# GetCatagoryIDList ( Eval( "BlogCategoryIDs" ) ) %> '
                            runat="server" />
                        <asp:Panel ID="uxTagsListPanel" runat="server" CssClass="BlogDetailsDefaultTagsPanel">
                            <asp:Label ID="lblTag" runat="server" Text="[$Tags]" CssClass="TagsLabel"></asp:Label>
                            <asp:Panel ID="uxTagsLinkPanel" runat="server" CssClass="BlogDetailsDefaultTagsDiv" />
                        </asp:Panel>
                        <asp:HiddenField ID="uxTagsHidden" Value='<%# Eval( "Tags" ) %> ' runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HyperLink ID="uxReadMoreLink" runat="server" CssClass="BlogListItemReadmore"
        NavigateUrl='<%# UrlManager.GetBlogUrl( Eval( "BlogID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
        Text="[$More]" />
    <div class="BlogListItemMessageDiv">
        <uc1:Message ID="uxMessage" runat="server" />
    </div>
</div>
