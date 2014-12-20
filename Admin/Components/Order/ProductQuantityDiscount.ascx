<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductQuantityDiscount.ascx.cs"
    Inherits="Admin_Components_Order_ProductQuantityDiscount" %>
<%@ Import Namespace="Vevo.Domain.Discounts" %>
<asp:Panel ID="uxQuantityDiscountPanel" runat="server" CssClass="QuantityDiscountPanel">
    <asp:Panel ID="uxQuantityDiscountGridViewPanel" runat="server">
        <asp:GridView ID="uxQuantityDiscountView" runat="server" AutoGenerateColumns="false"
            CssClass="QuantityDiscountGridView" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxFromLabel" runat="server" Text="From"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxFromValueLabel" runat="server" Text='<%# ShowFromItem( Eval("ToItems") ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="QuantityDiscountFromItemStyle" />
                    <HeaderStyle CssClass="QuantityDiscountFromHeaderStyle" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxToLabel" runat="server" Text="To"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxToValueLabel" runat="server" Text='<%# LastToItems ( Eval("ToItems").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="QuantityDiscountToHeaderStyle" />
                    <ItemStyle CssClass="QuantityDiscountToItemStyle" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxDiscountTypeLabel" runat="server" Text="Discount By"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxDiscountValueLabel" runat="server" Text='<%# ((DiscountRule) Container.DataItem).FormatAmountText() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="QuantityDiscountByHeaderStyle" />
                    <ItemStyle CssClass="QuantityDiscountByItemStyle" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="QuantityDiscountGridViewHeaderStyle" />
            <RowStyle CssClass="QuantityDiscountGridViewRowStyle" />
        </asp:GridView>
    </asp:Panel>
</asp:Panel>
