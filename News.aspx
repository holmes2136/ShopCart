<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="News.aspx.cs"
    Inherits="NewsPage" Title="[$Title]" %>

<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="News">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$News]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="NewsDiv">
                        <asp:DataList ID="uxNewsGrid" runat="server" CssClass="NewsGrid" RepeatColumns="1"
                            RepeatDirection="Horizontal" CellSpacing="0" CellPadding="0" ShowHeader="false">
                            <ItemTemplate>
                                <div class="Topic">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# GetNewsUrl( Eval("NewsID"), Eval("URLName") ) %>'>
                                        <asp:Label ID="uxNewsTopicText" runat="server" Text='<%# Eval("Topic")%>' />
                                    </asp:HyperLink>
                                </div>
                                <div class="Description">
                                    <div class="NewsListImage">
                                        <uc2:CatalogImage ID="uxCatalogImage" runat="server" MaximumWidth="120px" Visible='<%# CheckImageExist(Eval("ImageFile"))%>'
                                            ImageUrl='<%# Eval("ImageFile")%>' />
                                    </div>
                                    <%# CreatePreviewDescritpion(Eval("Description"))%>
                                </div>
                                <div class="FooterItem">
                                    <div class="PostedDate">
                                        Posted :
                                        <%# string.Format("{0:ddd, MMM d, yyyy}", (DateTime)Eval("NewsDate"))%>
                                    </div>
                                    <div class="Button">
                                        <div class="continueBtn">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# GetNewsUrl( Eval("NewsID"), Eval("URLName") ) %>'
                                                CssClass="BtnLink">
                                                     Read more
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <ItemStyle CssClass="NewsListRowStyle" />
                        </asp:DataList>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="NewsPagingBox">
                        <div class="NewsLinkToTopDiv">
                            <asp:LinkButton ID="uxGoToTopLink" runat="server" CssClass="ProductLinkToTop" Text="[$Gototop]" />
                        </div>
                        <div class="NewsPagingDiv">
                            <uc1:PagingControl ID="uxPagingControl" runat="server" MaximumLinks="3" />
                        </div>
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
    </div>
</asp:Content>
