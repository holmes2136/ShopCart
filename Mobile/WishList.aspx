<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.master" AutoEventWireup="true"
    CodeFile="WishList.aspx.cs" Inherits="Mobile_WishList" %>

<%@ Register Src="~/Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTitle">
        [$Header]
    </div>
    <uc1:Message ID="uxMessage" runat="server"></uc1:Message>
    <asp:PlaceHolder ID="uxWishListPlaceHolder" runat="server">
        <table class="MobileWishListTable" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:GridView ID="uxWishListGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID,Options,CartItemID"
                        CssClass="MobileWishListGridView" CellPadding="4" GridLines="None" OnRowDeleting="uxWishListGrid_RowDeleting"
                        OnRowUpdating="uxWishListGrid_RowUpdating">
                        <RowStyle CssClass="MobileWishListGridRowStyle" />
                        <AlternatingRowStyle CssClass="MobileWishListGridAlternatingRowStyle" />
                        <FooterStyle CssClass="MobileCommonGridViewFooterStyle WishListGridViewFooterStyle" />
                        <HeaderStyle CssClass="MobileCommonGridViewHeaderStyle MobileWishListGridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="x" CommandName="Delete"
                                        CssClass="MobileWishListDeleteButton" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="MobileWishListGridViewDeleteHeaderStyle" />
                                <ItemStyle CssClass="MobileWishListGridViewDeleteItemStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Name]">
                                <ItemTemplate>
                                    <asp:HyperLink ID="uxNameLink" runat="server" Text='<%# GetName( Container.DataItem ) %>'
                                        NavigateUrl='<%# GetURL( Container.DataItem ) %>' CssClass="CommonHyperLink"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle CssClass="MobileWishListGridViewNameItemStyle" />
                                <HeaderStyle CssClass="MobileWishListGridViewNameHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$UnitPrice]">
                                <ItemTemplate>
                                    <asp:Label ID="uxUnitPriceLabel" runat="server" Text='<%# GetUnitPriceText( Container.DataItem ) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="MobileWishListGridViewUnitPriceHeaderStyle" />
                                <ItemStyle CssClass="MobileWishListGridViewUnitPriceItemStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Quantity]">
                                <ItemTemplate>
                                    <asp:Label ID="uxQuantityLabel" runat="server" Text='<%# Bind("Quantity") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="MobileWishListGridViewQuantityHeaderStyle" />
                                <ItemStyle CssClass="MobileWishListGridViewQuantityItemStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="[$Subtotal]">
                                <ItemTemplate>
                                    <asp:Label ID="uxSubTotalLabel" runat="server" Text='<%# GetSubtotalText( Container.DataItem ) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="MobileWishListGridViewSubtotalHeaderStyle" />
                                <ItemStyle CssClass="MobileWishListGridViewSubtotalItemStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <vevo:ImageButton ID="uxAddToCartButton" runat="server" ThemeImageUrl="Images/Design/Icon/shopping_cart.png"
                                        CommandName="Update" Visible='<%# CheckWebsiteMode() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle CssClass="MobileWishListGridEmptyRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div class="WishListButtonDiv">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="MobileWishListButtonDiv">
                        <asp:LinkButton ID="uxContinueButton" runat="server" Text="Continue Shopping" OnClick="uxContinueButton_Click"
                            CssClass="MobileButton MobileWishListButton" />
                    </td>
                    <td class="MobileWishListButtonDiv">
                        <asp:LinkButton ID="uxViewShoppingCartButton" runat="server" Text="View Shopping Cart"
                            OnClick="uxViewShoppingCartButton_Click" CssClass="MobileButton MobileWishListButton" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:PlaceHolder>
    <asp:HiddenField ID="uxStatusHidden" runat="server" />
</asp:Content>
