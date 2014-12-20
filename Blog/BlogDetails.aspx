<%@ Page Language="C#" MasterPageFile="~/Blog/Blog.master" AutoEventWireup="true"
    CodeFile="BlogDetails.aspx.cs" Inherits="Blog_BlogDetails" ValidateRequest="false" %>

<%@ Register Src="~/Blog/Components/BlogComment.ascx" TagName="BlogComment" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="BlogDetails">
        <div class="BlogDetailsTop">
            <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/ContentLayoutTopLeft.gif"
                runat="server" CssClass="BlogDetailsTopImgLeft" />
            <asp:Label ID="uxDefaultTitle" runat="server" CssClass="BlogDetailsTopTitle">
                <asp:Literal ID="uxTitleLiteral" runat="server"></asp:Literal></asp:Label>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/ContentLayoutTopRight.gif"
                runat="server" CssClass="BlogDetailsTopImgRight" />
            <div class="Clear">
            </div>
        </div>
        <div class="BlogDetailsLeft">
            <div class="BlogDetailsRight">
                <div>
                    <asp:FormView ID="uxBlogFormView" runat="server" DataSourceID="uxBlogDetailsSource"
                        OnDataBinding="uxBlogFormView_DataBinding" OnDataBound="uxBlogFormView_DataBound"
                        CssClass="BlogFormView" OnItemCreated="BlogItemCreate">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:FormView>
                </div>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="BlogDetailsBottom">
            <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/ContentLayoutBottomLeft.gif"
                runat="server" CssClass="BlogDetailsBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/ContentLayoutBottomRight.gif"
                runat="server" CssClass="BlogDetailsBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="uxBlogDetailsSource" runat="server" SelectMethod="GetOne"
        TypeName="Vevo.Data.BlogRepository" OnSelecting="uxBlogDetailsSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="BlogID" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:BlogComment ID="uxBlogComment" runat="server" />
    <asp:Panel ID="uxFacebookCommentBoxPanel" runat="server" CssClass="FacebookCommentBox">
        <div id="uxFacebookCommentBox" runat="server" class="fb-comments" 
            data-num-posts='<%# GetNoOfPosts() %>' data-width="770">
        </div>
    </asp:Panel>
</asp:Content>
