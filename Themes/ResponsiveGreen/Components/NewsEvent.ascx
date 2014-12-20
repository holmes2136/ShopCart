<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsEvent.ascx.cs" Inherits="Themes_ResponsiveGreen_Components_NewsEvent" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc" %>
<div id="uxNewsEventDiv" runat="server" class="NewsEvent">
    <div class="CenterBlockTop">
        <asp:Label ID="uxNewsEventTitle" runat="server" Text="[$NewsTitle]" CssClass="CenterBlockTopTitle" />
        <asp:Panel ID="uxBlogViewAllPanel" runat="server" class="NewsEventViewAll">
            <asp:HyperLink ID="uxViewAllNewsLink" runat="server" CssClass="BtnStyle1" Text="[$ViewAllBlog]"
                NavigateUrl="~/Blog/default.aspx" />
        </asp:Panel>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <div class="NewsEventList">
                <asp:Repeater ID="uxNewsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="NewsEventItemStyle">
                            <div class="NewsEventContent">
                                <div class="NewsEventImage">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# GetBlogUrl( Eval("BlogID"), Eval("URLName") ) %>'>
                                        <uc:CatalogImage ID="uxCatalogImage" runat="server" ImageUrl='<%# GetBlogImage( Eval("ImageFile") ) %>' />
                                    </asp:HyperLink>
                                </div>
                                <div class="NewsDate">
                                    <asp:Label ID="uxDateNewsText" runat="server" Text='<%# string.Format( "{0:MMM dd, yyyy}", (DateTime) Eval("CreateDate")) %>'>
                                    </asp:Label>
                                </div>
                                <div class="NewsTopic">
                                    <asp:HyperLink ID="uxNewsTopicLink" runat="server" NavigateUrl='<%# GetBlogUrl( Eval("BlogID"), Eval("URLName") ) %>'><%# LimitDisplayCharactor(Eval("BlogTitle"), 50)%>
                                    </asp:HyperLink>
                                </div>
                            </div>
                            <div class="NewsDescription">
                                <%# LimitDisplayCharactor(Eval("ShortContent"), 85)%>
                            </div>
                            <div class="NewsEventMore">
                                <asp:HyperLink ID="uxMoreNewsLink" runat="server" CssClass="NewsEventMoreLink" NavigateUrl='<%# GetBlogUrl( Eval("BlogID"), Eval("URLName") ) %>'
                                    Text="[$ReadMore]" />
                            </div>
                            <div class="Clear">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
