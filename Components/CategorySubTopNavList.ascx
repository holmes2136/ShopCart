<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategorySubTopNavList.ascx.cs"
    Inherits="Components_CategorySubTopNavList" EnableViewState="False" %>
<asp:DataList ID="uxSubCategoryList" runat="server" RepeatDirection="Vertical">
    <ItemTemplate>
        <div class="SubCategoryName">
            <asp:HyperLink ID="uxSubCategoryLink" runat="server" NavigateUrl="<%# GetURL(Container.DataItem) %>"
                Text="<%# GetName(Container.DataItem) %>" CssClass="LeafCategoryLink" />
        </div>
        <asp:HiddenField ID="uxHiddenCategoryID" runat="server" Value='<%# Eval("CategoryID") %>' />
    </ItemTemplate>
</asp:DataList>
<asp:Panel ID="uxViewMorePanel" runat="server" CssClass="ViewMorePanel">
    <asp:HyperLink ID="uxViewMoreLink" runat="server" CssClass="ViewMoreLink" Text="View More"
        NavigateUrl="<%# GetMoreURL() %>" />
</asp:Panel>
