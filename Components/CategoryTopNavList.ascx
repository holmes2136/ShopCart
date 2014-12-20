<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryTopNavList.ascx.cs"
    Inherits="Components_CategoryTopNavList" EnableViewState="False" %>
<%@ Register Src="CategorySubTopNavList.ascx" TagName="SubCategory" TagPrefix="uc1" %>
<asp:Panel ID="uxPanelSubCategory" runat="server" Visible="true" CssClass="CategoryPanel">
    <asp:DataList ID="uxListSubCategory" runat="server" RepeatDirection="Horizontal"
        CssClass="SubCategoryDataList" OnItemDataBound="uxListSubCategory_ItemDataBound">
        <ItemTemplate>
            <div class="SubCategoryListStyle">
                <div class="SubParentLinkDiv">
                    <asp:HyperLink ID="uxSubParentLink" runat="server" NavigateUrl="<%# GetURL(Container.DataItem) %>"
                        Text="<%# GetName(Container.DataItem) %>" CssClass="SubParentLink" />
                </div>
                <asp:Panel ID="uxSubCategoryPanel" runat="server" CssClass="SubCategoryPanel" Visible="<%# IsSubCategory(Container.DataItem) %>">
                    <uc1:SubCategory ID="uxList" runat="server" CategoryID='<%# Eval("CategoryID") %>' />
                </asp:Panel>
                <asp:HiddenField ID="uxSubCategoryHidden" runat="server" Value='<%# Eval("CategoryID") %>' />
                <div class="Clear">
                </div>
            </div>
        </ItemTemplate>
        <ItemStyle CssClass="SubCategoryItemData" />
    </asp:DataList>
</asp:Panel>
