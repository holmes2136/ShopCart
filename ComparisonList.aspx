<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Front.master" CodeFile="ComparisonList.aspx.cs"
    Inherits="ComparisonList" Title="[$Title]" %>

<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uxAddToCartNotification" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="CompareList">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$ProductCompareListTitle]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Label ID="uxMessage" runat="server" Visible="false"></asp:Label>
                    <asp:PlaceHolder ID="uxCompareListPlaceHolder" runat="server">
                        <asp:GridView ID="uxCompareListGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                            CssClass="CommonGridView CompareListGridView" CellPadding="4" GridLines="None"
                            OnRowDeleting="uxCompareListGrid_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="ImageItemDiv">
                                            <asp:HyperLink ID="uxItemImageLink" runat="server" NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'>
                                                <asp:Image ID="uxItemImage" runat="server" ImageUrl='<%# Eval( "ImageSecondary" ).ToString() %>' Width="60" />
                                            </asp:HyperLink></div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="ImageHeader" />
                                    <ItemStyle CssClass="ImageItem" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Name]" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="uxNameLink" runat="server" Text='<%#  Eval( "Name" )%>' NavigateUrl='<%# UrlManager.GetProductUrl( Eval( "ProductID" ).ToString(), Eval( "UrlName" ).ToString() ) %>'
                                            CssClass="CommonHyperLink"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="NameItem" />
                                    <HeaderStyle CssClass="NameHeader" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$Unit Price]" SortExpression="Price" >
                                    <ItemTemplate>
                                        <asp:Label ID="uxPriceLabel" runat="server" Text='<%# GetFormattedPrice( Eval( "ProductID" ).ToString() )%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PriceItem" />
                                    <HeaderStyle CssClass="PriceHeader" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnCommand="uxAddToCartImageButton_Command"
                                            CommandName='<%# Eval( "UrlName" ) %>' CommandArgument='<%# Eval( "ProductID" ) %>'
                                            Visible='<%# IsBuyButtonVisible(Eval( "ProductID" )) %>' Text="[$BtnAddtoCart]"
                                            CssClass="BtnStyle1" />
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
                            <FooterStyle CssClass="CommonGridViewHeaderStyle CompareListGridViewFooterStyle" />
                            <RowStyle CssClass="CommonGridViewRowStyle CompareListGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle CompareListGridViewHeaderStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                        </asp:GridView>
                        <div class="CompareListButtonDiv">
                            <asp:LinkButton ID="uxCompareButton" runat="server" CssClass="CompareLinkButton BtnStyle1"
                                Text="[$BtnCompareProduct]" />
                            <div class="Clear">
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <div class="Clear">
                    </div>
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
    <uxAddToCartNotification:AddToCartNotification ID="uxAddToCartNotification" runat="server" />
</asp:Content>
