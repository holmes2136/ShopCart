<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryNavTabMenuList.ascx.cs"
    Inherits="Components_CategoryNavTabMenuList" %>
<%@ Register Src="CategoryTopNavList.ascx" TagName="CategoryTopNavList" TagPrefix="uc1" %>
<div class="CategoryNavTabMenu">
    <div class="nav-container">
        <ul class="menu">
            <asp:DataList ID="uxList" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" >
                <ItemTemplate>
                    <li>
                        <div class="HeaderMenuNavItemLeft">
                            <div class="HeaderMenuNavItemRight">
                                <asp:HyperLink ID="uxLinkHeader" runat="server" class="HyperLink" NavigateUrl="<%# GetURL(Container.DataItem) %>">
                                    <asp:Label ID="uxLinkHeaderLabel" runat="server" Text='<%# Eval("Name") %>' />
                                    <asp:Image ID="uxSubCategoryDropImage" runat="server" ImageUrl="~/Images/Design/Icon/category-narrow-drop.png"
                                        Visible="<%# !IsLeaf(Container.DataItem) %>" />
                                    <uc1:CategoryTopNavList ID="uxCategoryTopNavList" runat="server" CategoryID='<%# Eval("CategoryID") %>'
                                        Visible="<%# !IsLeaf(Container.DataItem) %>" />
                                </asp:HyperLink>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:DataList>
        </ul>
    </div>
    <div class="Clear">
    </div>
</div>
