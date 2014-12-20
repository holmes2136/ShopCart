<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FindGiftRegistry.ascx.cs"
    Inherits="Components_FindGiftRegistry" %>
<div class="FindGiftRegistry">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/FindGiftRegistryTopLeft.gif"
            runat="server" CssClass="SidebarTopImgLeft" />
        <asp:Label ID="uxFindGiftRegistryTitle" runat="server" Text="[$GiftRegistry]" CssClass="SidebarTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/FindGiftRegistryTopRight.gif"
            runat="server" CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <div class="FindGiftRegistryDiv">
                <asp:HyperLink ID="uxCreateGiftRegistryLink" runat="server" NavigateUrl="~/GiftRegistryList.aspx"
                    CssClass="FindGiftRegistryCreateLink" Text="[$CreateGiftRegistryText]">
                </asp:HyperLink></div>
            <div class="FindGiftRegistryDiv">
                <asp:HyperLink ID="uxFindGiftRegistryLink" runat="server" NavigateUrl="~/GiftRegistrySearch.aspx"
                    CssClass="FindGiftRegistryFindLink" Text="[$FindGiftRegistryText]">
                </asp:HyperLink></div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="SidebarBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/FindGiftRegistryBottomLeft.gif"
            runat="server" CssClass="SidebarBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/FindGiftRegistryBottomRight.gif"
            runat="server" CssClass="SidebarBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
