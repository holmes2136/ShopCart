<%@ Page Language="C#" MasterPageFile="~/Blog/Blog.master" AutoEventWireup="true"
    CodeFile="ArchiveList.aspx.cs" Inherits="Blog_ArchiveList" Title="[$Title]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="BlogList">
        <div class="BlogListTop">
            <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                CssClass="BlogListTopImgLeft" />
            <asp:Label ID="uxDefaultTitle" runat="server" CssClass="BlogDetailsTopTitle">[$ArchiveListTitle]</asp:Label>
            <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                runat="server" CssClass="BlogListTopImgRight" />
            <div class="Clear">
            </div>
        </div>
        <div class="BlogListLeft">
            <div class="BlogListRight">
                <asp:GridView ID="uxBlogGrid" runat="server" AutoGenerateColumns="False" GridLines="None"
                    CssClass="CommonGridView ArchiveListGridView">
                    <Columns>
                        <asp:TemplateField HeaderText="Monthly Archive">
                            <ItemStyle CssClass="ArchiveListTopicItemColumnTopicStyle" />
                            <ItemTemplate>
                                <asp:HyperLink ID="uxDateLink" runat="server" Text='<%# GetTextName( Container.DataItem  ) %>'
                                    NavigateUrl='<%# Vevo.UrlManager.GetBlogListUrl( GetNavURL( Container.DataItem  ) ) %>' />
                                <asp:Label ID="uxAmountLabel" runat="server" Text='<%# GetAmount( Container.DataItem  ) %>'
                                    CssClass="ArchiveListDateItemsColumnDateStyle"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="ArchiveListDateHeaderColumnDateStyle" />
                    <RowStyle CssClass="CommonGridViewRowStyle ArchiveListRowStyle" />
                    <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle ArchiveListAlternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle ArchiveListEmptyRowStyle" />
                </asp:GridView>
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
