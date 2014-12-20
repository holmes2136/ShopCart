<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderItemDetails.ascx.cs" Inherits="Components_OrderItemDetails" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:GridView ID="uxItemDetailGrid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" >
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:BoundField DataField="ProductID" HeaderText="ProductID">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Name">
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="UnitPrice">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Convert.ToDecimal( Eval("UnitPrice") ) ) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cost">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Convert.ToDecimal( Eval("ItemSubtotal") ) ) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="GridOrderRowStyle" ForeColor="#333333" />
    <EditRowStyle BackColor="#999999" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle CssClass="GridHeadStyle" />
    <AlternatingRowStyle BackColor="#eeeeee" />
</asp:GridView>
