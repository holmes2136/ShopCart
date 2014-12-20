<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComparisonListBox.ascx.cs"
    Inherits="Components_ComparisonListBox" %>
<div class="CompareProductBoxList">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxTitle" runat="server" Text="[$ComparisonProductTitle]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:DataList ID="uxList" runat="server" ShowFooter="False" CssClass="CompareProductList"
                OnDeleteCommand="uxList_DeleteCommand">
                <ItemTemplate>
                    <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl=' <%# Vevo.UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                        <asp:Image ID="uxItemImage" runat="server" ImageUrl='<%# GetItemImage( Container.DataItem ) %>'
                            Width="25" Height="25" ImageAlign="Middle" />
                        <asp:Label ID="uxProductLink" runat="server" Text='<%# GetNavName( Container.DataItem ) %>' />
                    </asp:HyperLink>
                    <div class="CompareProductDelete">
                         <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                            CommandName="Delete" CommandArgument='<%# Eval( "ProductID" ).ToString() %>' />
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div>
                <asp:LinkButton ID="uxViewAllButton" runat="server" PostBackUrl="~/ComparisonList.aspx"
                    Text='View All' CssClass="CompareListBoxViewAll" />
                <asp:LinkButton ID="uxDeleteAllButton" runat="server" Text="Clear All" CssClass="CompareListBoxClearAll"
                    OnClick="uxDeleteAllButton_Click" />
            </div>
            <div class="CompareListButtonDiv">
            <asp:LinkButton ID="uxCompareButton" runat="server" CssClass="CompareLinkButton BtnStyle1"
                Text="[$BtnCompareProduct]" /></div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/CategoryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/CategoryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
