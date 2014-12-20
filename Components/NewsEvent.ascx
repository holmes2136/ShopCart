<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsEvent.ascx.cs" Inherits="Components_NewsEvent" %>
<%@ Register Src="CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<div id="uxNewsEventDiv" runat="server" class="NewsEvent">
    <div class="CenterBlockTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/NewsEventTopLeft.gif" runat="server"
            CssClass="CenterBlockTopImgLeft" />
        <asp:Label ID="uxNewsEventTitle" runat="server" Text="[$News And Announcements]"
            CssClass="CenterBlockTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/NewsEventTopRight.gif" runat="server"
            CssClass="CenterBlockTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <div class="NewsEventRowStyle">
                <asp:Repeater ID="uxNewsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="NewsEventItemStyle">
                            <div class="NewsEventImage">
                                <uc1:CatalogImage ID="uxCatalogImage" runat="server" ImageUrl='<%# Eval("ImageFile") %>' />
                            </div>
                            <div class="NewsEventContent">
                                <div class="NewsDate" style="text-align: left">
                                    <asp:HyperLink ID="uxDateLink" runat="server" Text='<%# string.Format( "{0:d}", (DateTime) Eval("NewsDate")) %>'
                                        NavigateUrl='<%# GetNewsUrl( Eval("NewsID"), Eval("URLName") ) %>'>
                                    </asp:HyperLink>
                                </div>
                                <div class="NewsText" style="text-align: left">
                                    <asp:HyperLink ID="uxNewsFopicLink" runat="server" Text='<%# Eval("Topic") %>' NavigateUrl='<%# GetNewsUrl( Eval("NewsID"), Eval("URLName") ) %>'>
                                    </asp:HyperLink>
                                </div>
                                <div class="Clear">
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="NewsEventMore" id="uxNewsMoreDiv" runat="server">
                <a href="News.aspx" class="CssNoLine">
                    <asp:Label ID="uxMoreLabel" runat="server" Text="[$More]" CssClass="NewsEventMoreLabel" />
                </a>
                <ucHyperLink:HyperLink ID="uxMoreImage" runat="server" ThemeImageUrl="Images/Design/Button/More.jpg" CssClass="NewsEventMoreImage" NavigateUrl="News.aspx" />
            </div>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/NewsEventBottomLeft.gif"
            runat="server" CssClass="CenterBlockImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/NewsEventBottomRight.gif"
            runat="server" CssClass="CenterBlockImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
