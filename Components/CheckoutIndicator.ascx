<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutIndicator.ascx.cs"
    Inherits="Components_CheckoutIndicator" %>
<asp:Panel ID="uxCheckoutIndicatorPanel" runat="server" CssClass="CheckoutIndicatorPanel">
    <asp:Table ID="uxCheckoutIndicatorTable" runat="server" CssClass="CheckoutIndicatorTable"
        CellPadding="0" CellSpacing="0">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="5" CssClass="CurrentPageTitle">
                <asp:Label ID="uxCurrentPageLabel" runat="server" />
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:HyperLink ID="uxLoginLink" runat="server" Text="LOGIN" CssClass="IndLink" Enabled="false"
                    NavigateUrl="~/userlogin.aspx?returnurl=checkout.aspx" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:HyperLink ID="uxAddressLink" runat="server" Text="ADDRESS" CssClass="IndLink"
                    Enabled="false" NavigateUrl="~/checkout.aspx" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:HyperLink ID="uxShippingLink" runat="server" Text="SHIPPING" CssClass="IndLink"
                    Enabled="false" NavigateUrl="~/shipping.aspx" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:HyperLink ID="uxPaymentLink" runat="server" Text="PAYMENT" CssClass="IndLink"
                    Enabled="false" NavigateUrl="~/payment.aspx" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:HyperLink ID="uxSummaryLink" runat="server" Text="SUMMARY" CssClass="IndLink"
                    Enabled="false" NavigateUrl="~/ordersummary.aspx" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Panel>
