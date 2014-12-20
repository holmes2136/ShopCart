<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataFeedList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_DataFeedList" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="DataFeedGoogle.ascx" TagName="DataFeedGoogle" TagPrefix="uc2" %>
<%@ Register Src="DataFeedPriceGrabber.ascx" TagName="DataFeedPriceGrabber" TagPrefix="uc3" %>
<%@ Register Src="DataFeedShoppingDotCom.ascx" TagName="DataFeedShoppingDotCom" TagPrefix="uc4" %>
<%@ Register Src="DataFeedYahooShopping.ascx" TagName="DataFeedYahooShopping" TagPrefix="uc5" %>
<%@ Register Src="DataFeedAmazon.ascx" TagName="DataFeedAmazon" TagPrefix="uc6" %>
<%@ Register Src="DataFeedShopzilla.ascx" TagName="DataFeedShopzilla" TagPrefix="uc7" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc8" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <FilterTemplate>
        <asp:Label ID="uxFeedLabel" runat="server" Text="Product Feed : " CssClass="fb mgr10"></asp:Label>
        <asp:DropDownList ID="uxDataFeedDrop" runat="server" OnSelectedIndexChanged="uxDataFeedDrop_OnSelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem Text="Google Feed" Value="GoogleFeed"></asp:ListItem>
            <asp:ListItem Text="Price Grabber" Value="PriceGrabber"></asp:ListItem>
            <asp:ListItem Text="Shopping.Com" Value="ShoppingDotCom"></asp:ListItem>
            <asp:ListItem Text="Yahoo Shopping" Value="YahooShopping"></asp:ListItem>
            <asp:ListItem Text="Amazon" Value="Amazon"></asp:ListItem>
            <asp:ListItem Text="Shopzilla" Value="Shopzilla"></asp:ListItem>
        </asp:DropDownList>
    </FilterTemplate>
    <GridTemplate>
        <div class="CommonAdminBorder Container-Box">
            <uc2:DataFeedGoogle ID="uxDataFeedGoogle" runat="server" />
            <uc3:DataFeedPriceGrabber ID="uxDataFeedPriceGrabber" runat="server" />
            <uc4:DataFeedShoppingDotCom ID="uxDataFeedShoppingDotCom" runat="server" />
            <uc5:DataFeedYahooShopping ID="uxDataFeedYahooShopping" runat="server" />
            <uc6:DataFeedAmazon ID="uxDataFeedAmazon" runat="server" />
            <uc7:DataFeedShopzilla ID="uxDataFeedShopzilla" runat="server" />
        </div>
    </GridTemplate>
</uc1:AdminContent>
