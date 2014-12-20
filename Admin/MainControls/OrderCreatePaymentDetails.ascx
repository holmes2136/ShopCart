<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderCreatePaymentDetails.ascx.cs"
    Inherits="Admin_MainControls_OrderCreatePaymentDetails" %>
<%@ Register Src="../Components/Order/CheckOutPaymentDetails.ascx" TagName="CheckOutPaymentDetails"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <PlainContentTemplate>
        <asp:Panel ID="uxPaymentFailDetailsPanel" runat="server" Visible="false">
            <div class="CommonTitle">
                <asp:Label ID="uxTitle" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxMsgLabel" runat="server" />
            </div>
            <div class="CommonRowStyle">
                <%--<asp:HyperLink ID="uxGotoPageLink" runat="server"></asp:HyperLink>--%>
                <vevo:AdvancedLinkButton ID="uxGotoPageLink" runat="server" OnClick="ChangePage_Click">
                </vevo:AdvancedLinkButton>
            </div>
        </asp:Panel>
        <asp:Panel ID="uxPaymentDetails" runat="server">
            <uc2:CheckOutPaymentDetails ID="uxCheckOutPaymentDetails" runat="server"></uc2:CheckOutPaymentDetails>
        </asp:Panel>
    </PlainContentTemplate>
</uc1:AdminContent>
