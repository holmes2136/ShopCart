<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewArrivalCategory.ascx.cs"
    Inherits="Components_NewArrivalCategory" %>
<%@ Register Src="~/Components/NewArrivalCategoryItem.ascx" TagName="NewArrivalCategoryItem"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="NewArrivalCategory" id="uxNewArrivalCategory" runat="server">
    <div class="SidebarTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/CategoryTopLeft.gif" runat="server"
            CssClass="SidebarTopImgLeft" />
        <asp:Panel ID="uxPageControlTR" runat="server" CssClass="SidebarTopTitle">
            [$NewArrivalTitle]
            <asp:Label ID="uxNewArrivalCategoryName" runat="server" Text="Example"></asp:Label>
        </asp:Panel>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/CategoryTopRight.gif" runat="server"
            CssClass="SidebarTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="SidebarLeft">
        <div class="SidebarRight">
            <asp:Panel ID="uxDataListPanel" runat="server">
                <div class="NewArrivalCategoryList">
                    <asp:Repeater ID="uxNewArrivalList" runat="server">
                        <ItemTemplate>
                            <uc1:NewArrivalCategoryItem ID="uxItem" runat="server" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
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
