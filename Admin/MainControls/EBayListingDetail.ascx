<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayListingDetail.ascx.cs" Inherits="Admin_MainControls_EBayListingDetail" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ContentTemplate>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcListID" runat="server" meta:resourcekey="lcListID"
            CssClass="Label" />
            <asp:Label ID="lcListIDData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcItemName" runat="server" meta:resourcekey="lcItemName"
            CssClass="Label" />
            <asp:Label ID="lcItemNameData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcItemNumber" runat="server" meta:resourcekey="lcItemNumber"
            CssClass="Label" />
            <asp:Label ID="lcItemNumberData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcLastUpdate" runat="server" meta:resourcekey="lcLastUpdate"
            CssClass="Label" />
            <asp:Label ID="lcLastUpdateData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcListType" runat="server" meta:resourcekey="lcListType"
            CssClass="Label" />
            <asp:Label ID="lcListTypeData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcLastStatus" runat="server" meta:resourcekey="lcLastStatus"
            CssClass="Label" />
            <asp:Label ID="lcLastStatusData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcQtyLeft" runat="server" meta:resourcekey="lcQtyLeft"
            CssClass="Label" />
            <asp:Label ID="lcQtyLeftData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcBidAmount" runat="server" meta:resourcekey="lcBidAmount"
            CssClass="Label" />
            <asp:Label ID="lcBidAmountData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcBidPrice" runat="server" meta:resourcekey="lcBidPrice"
            CssClass="Label" />
            <asp:Label ID="lcBidPriceData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcBuyItNowPrice" runat="server" meta:resourcekey="lcBuyItNowPrice"
            CssClass="Label" />
            <asp:Label ID="lcBuyItNowPriceData" runat="server"
            CssClass="Label" />
        </div>
        <div class="EBayListDetailsRow">
            <asp:Label ID="lcOrder" runat="server" meta:resourcekey="lcOrder"
            CssClass="Label" />
            <asp:Panel ID="lcOrderLinkHolder" Runat="server"></asp:Panel>
        </div>
        
    </ContentTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />