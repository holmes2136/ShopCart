<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AddShoppingCartNotComplete.aspx.cs"
    Inherits="AddShoppingCartNotComplete" Title="Add Product Not Complete" %>

<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AddShoppingCartNotComplete">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">Add Product to Shopping Cart not complete.</asp:Label>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Panel ID="giftRegistryMessagePanel" runat="server">
                        <asp:HyperLink ID="uxContinueLink" runat="server" CssClass="CommonHyperLink GoBackLinkStyle">Go to Gift Registry Product List</asp:HyperLink>
                    </asp:Panel>
                    <asp:Panel ID="uxProductFreeShippingCostPanel" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="uxProductNonFreeShippingCostPanel" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:HyperLink ID="uxGoHome" CssClass="CommonHyperLink GoBackLinkStyle" runat="server" Text="Go to the Home Page."
                        NavigateUrl="~/Default.aspx"></asp:HyperLink>
                </div>
            </div>
            <div class="CommonPageBottom">
            </div>
        </div>
    </div>
</asp:Content>
