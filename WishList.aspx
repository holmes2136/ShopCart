<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="WishList.aspx.cs"
    Inherits="WishListPage" Title="WishList" %>

<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="WishList">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1"></uc1:Message>
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Wish List]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:PlaceHolder ID="uxWishListPlaceHolder" runat="server">
                        <asp:GridView ID="uxWishListGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID,Options,CartItemID"
                            CssClass="CommonGridView WishListGridView" CellPadding="4" CellSpacing="0" GridLines="None"
                            OnRowDeleting="uxWishListGrid_RowDeleting" OnRowUpdating="uxWishListGrid_RowUpdating">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="ImageItemDiv">
                                            <asp:HyperLink ID="uxItemImageLink" runat="server" NavigateUrl='<%# GetURL( Container.DataItem ) %>'>
                                                <asp:Image ID="uxItemImage" runat="server" ImageUrl='<%# GetItemImage( Container.DataItem ) %>'
                                                    Width="60" />
                                            </asp:HyperLink></div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="ImageHeader" />
                                    <ItemStyle CssClass="ImageItem" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Name]" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxNameLink" runat="server" Text='<%# GetName( Container.DataItem ) %>'
                                            NavigateUrl='<%# GetURL( Container.DataItem ) %>' CssClass="CommonHyperLink"></asp:HyperLink>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="NameItem" />
                                    <HeaderStyle CssClass="NameHeader" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Unit Price]" SortExpression="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="uxLabel" runat="server" Text='<%# GetUnitPriceText( Container.DataItem ) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PriceItem" />
                                    <HeaderStyle CssClass="PriceHeader" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Quantity]" SortExpression="Quantity">
                                    <ItemTemplate>
                                                 <asp:Label ID="uxQuantityLabel" runat="server" Text='<%# Bind("Quantity") %>' CssClass="Quantity" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="QuantityItem" />
                                    <HeaderStyle CssClass="QuantityHeader" />
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="uxAddToCartButton" runat="server" Text="[$BtnWishListAddToCart]"
                                            CssClass="BtnStyle1" CommandName="Update" Visible='<%# CheckWebsiteMode() %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="AddToCartHeader" />
                                    <ItemStyle CssClass="AddToCartItem" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Del">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="DeleteHeader" />
                                    <ItemStyle CssClass="DeleteItem" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="CommonGridViewHeaderStyle WishListGridViewFooterStyle" />
                            <RowStyle CssClass="CommonGridViewRowStyle WishListGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle WishListGridViewViewHeaderStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                        </asp:GridView>
                        <div class="WishListButtonDiv" id="uxButtonDiv" runat="server">
                            <asp:LinkButton ID="uxContinueButton" runat="server" Text="[$BtnWishListContinueShopping]"
                                OnClick="uxContinueButton_Click" CssClass="WishListContinueImageButton BtnStyle2" />
                            <asp:LinkButton ID="uxViewShoppingCartButton" runat="server" Text="[$BtnViewShoppingCart]"
                                OnClick="uxViewShoppingCartButton_Click" CssClass="WishListViewShoppingCartImageButton BtnStyle2" />
                        </div>
                    </asp:PlaceHolder>
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
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </div>
</asp:Content>
