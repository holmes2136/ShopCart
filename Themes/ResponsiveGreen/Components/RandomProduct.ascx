<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RandomProduct.ascx.cs"
    Inherits="Themes_ResponsiveGreen_Components_RandomProduct" %>
<%@ Register Src="~/Components/CatalogImage.ascx" TagName="CatalogImage" TagPrefix="uc1" %>
<%@ Register Src="~/Components/RecurringSpecial.ascx" TagName="RecurringSpecial"
    TagPrefix="uc5" %>
<%@ Register Src="~/Components/AddToCartNotification.ascx" TagName="AddToCartNotification"
    TagPrefix="uxAddToCartNotification" %>
<%@ Register Src="~/Components/ProductQuickView.ascx" TagName="ProductQuickView"
    TagPrefix="uc6" %>
<%@ Register Src="~/Components/RatingCustomer.ascx" TagName="RatingCustomer" TagPrefix="uc1" %>
<%@ Register Src="~/Components/AddtoWishListButton.ascx" TagName="AddtoWishListButton"
    TagPrefix="uc4" %>
<%@ Register Src="~/Components/AddtoCompareListButton.ascx" TagName="AddtoCompareListButton"
    TagPrefix="uc7" %>
<%@ Register Src="Controls/RandomProductControl.ascx" TagName="RandomProductControl"
    TagPrefix="uc8" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Products" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<div class="RandomProduct" id="uxRandomProductDiv" runat="server">
    <div class="CenterBlockTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/RandomProductTopLeft.gif"
            runat="server" CssClass="CenterBlockTopImgLeft" />
        <asp:Label ID="uxRandomProductTitle" runat="server" Text="[$RandomShow]" CssClass="CenterBlockTopTitle"></asp:Label>
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/RandomProductTopRight.gif"
            runat="server" CssClass="CenterBlockTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <asp:UpdatePanel ID="uxRandomListUpdate" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="RandomProductDataList">
                        <asp:Repeater ID="uxRandomList" runat="server">
                            <ItemTemplate>
                                <uc8:RandomProductControl ID="uxItem" runat="server" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/RandomProductBottomLeft.gif"
            runat="server" CssClass="CenterBlockBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/RandomProductBottomRight.gif"
            runat="server" CssClass="CenterBlockBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</div>
