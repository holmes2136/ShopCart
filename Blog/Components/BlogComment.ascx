<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogComment.ascx.cs" Inherits="Blog_Components_BlogComment" %>
<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<%@ Register Src="~/Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop"
    TagPrefix="uc3" %>
<div class="BlogComment" id="uxBlogCommentDiv" runat="server" visible="false">
    <asp:HiddenField ID="metakeywords" runat="server" Value="#" />
    <asp:HiddenField ID="metadescription" runat="server" Value="#" />
    <div class="CommonPage">
        <div class="CommonPageTop">
        </div>
        <div class="CommonPageLeft">
            <div class="CommonPageRight">
                <div class="BlogCommentPagingBox">
                    <div class="BlogCommentPagingDiv">
                        <div class="BlogCommentTitle">
                            <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">
                            </asp:Label>
                        </div>
                        <uc1:PagingControl ID="uxPagingControl" runat="server" />
                    </div>
                </div>
                <div class="BlogCommentDiv">
                    <asp:DataList ID="uxBlogCommentGrid" runat="server" CssClass="BlogCommentGrid" CellSpacing="0"
                        CellPadding="0" ShowHeader="false">
                        <ItemTemplate>
                            <div class="BlogCommentUserName">
                                [$PostedBy]
                                <asp:Label ID="uxBlogCommentUserNameLabel" runat="server" Font-Bold="true" Text='<%# Eval("UserName") %>'></asp:Label>
                            </div>
                            <div class="BlogCommentCreateDate">
                                 <asp:Label ID="uxBlogCommentCreateDateLabel" runat="server" Font-Italic="true" Text='<%# String.Format( "{0:ddd dd MMM, yyyy}", Eval("CreatedDate") ) %>'></asp:Label>
                            </div>
                            <div class="BlogCommentPost">
                                <asp:Label ID="uxBlogCommentPostLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle CssClass="BlogCommentListStyle" />
                        <AlternatingItemStyle CssClass="BlogCommentListAlterStyle" />
                    </asp:DataList>
                </div>
                <div class="BlogCommentPostDiv">
                    <div class="BlogCommentPostLabel">
                        <asp:Label ID="uxBlogPostCommentLabel" Text='<%# GetLanguageText( "PostComment" )%>'
                            runat="server" />
                    </div>
                    <div>
                        <asp:TextBox ID="uxBlogPostCommentArea" runat="server" TextMode="MultiLine" CssClass="BlogCommentTextArea" />
                        <asp:RequiredFieldValidator ID="uxSubjectRequiredValidator" runat="server" ControlToValidate="uxBlogPostCommentArea"
                            ValidationGroup="CommentValid" Display="Dynamic" CssClass="CommonValidatorText">
                        <div class="CommonValidateDiv CommonValidateLong"></div><img src="Images/Design/RequiredFillBullet_Up.gif" />&nbsp;[$BlankComment]
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="BlogCommentPostButton">
                        <asp:LinkButton ID="uxBlogPostButton" runat="server" Text='<%# GetLanguageText( "SubmitComment" )%>'
                            CssClass="BtnStyle2" OnClick="uxBlogPostButton_OnClick" ValidationGroup="CommentValid" />
                    </div>
                </div>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="CommonPageBottom">
            <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                runat="server" CssClass="CommonPageBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                runat="server" CssClass="CommonPageBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
</div>
