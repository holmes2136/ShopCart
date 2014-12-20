<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuantityDiscount.ascx.cs"
    Inherits="Components_QuantityDiscount" %>
<%@ Import Namespace="Vevo.Domain.Discounts" %>
<asp:Panel ID="uxQuantityDiscountPanel" runat="server" CssClass="QuantityDiscountPanel">
    <div class="QuantityDiscountTop">
        <asp:Label ID="uxQuantityDiscount" runat="server" CssClass="DiscountLabel" Text="[$DiscountIcon]" />
        <asp:LinkButton ID="uxHideLink" runat="server" CssClass="QuantityDiscountHideLink">
            <asp:Image ID="uxHideImage" runat="server" ImageUrl="~/Images/Design/Icon/HideDiscount.gif"
                CssClass="QuantityDiscountHideLinkImage" />
        </asp:LinkButton>
        <asp:LinkButton ID="uxShowLink" runat="server" CssClass="QuantityDiscountShowLink">
            <asp:Image ID="uxShowImage" runat="server" ImageUrl="~/Images/Design/Icon/ShowDiscount.gif"
                CssClass="QuantityDiscountShowLinkImage" />
        </asp:LinkButton>
    </div>
    <div class="QuantityDiscountLeft">
        <div class="QuantityDiscountRight">
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
        </div>
    </div>
    <div class="QuantityDiscountBottom">
        <div class="QuantityDiscountBottomLeft">
        </div>
        <div class="QuantityDiscountBottomright">
        </div>
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
