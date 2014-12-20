<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabularView.ascx.cs" Inherits="AdminAdvanced_Components_Snapshot_TabularView" %>
<%@ Register Src="../PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Template/AdminUserControlContent.ascx" TagName="AdminUserControlContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<uc1:AdminUserControlContent ID="uxAdminUserControlContent" runat="server">
    <PlainContentTemplate>
        <div class="DefaultFilter">
            <div class="SearchFilter">
                <asp:Label ID="uxFilterText" runat="server" Text="Select Range :"/>
                <asp:DropDownList ID="uxPeriodDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxPeriodDrop_SelectedIndexChanged"
                    DataTextField="Text" DataValueField="Value">
                </asp:DropDownList>
            </div>
        </div>
        <%--report table begin--%>
        <div class="DefaultPageControlDiv1">
                <div class="DefaultPageControlDiv">
                    <uc3:PagingControl ID="uxPagingControl" runat="server" />
                    <div class="Clear">
                    </div>
                </div>
            <div class="DefaultGridDiv">
                <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                    OnSorting="uxGrid_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Date/Time" SortExpression="Period">
                            <ItemTemplate>
                                <asp:Label ID="uxDateTime" runat="server" Text='<%# GetDateTimeText(Eval("Period")) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Total" SortExpression="Total">
                            <ItemTemplate>
                                <asp:Label ID="uxTotalLabel" runat="server" Text='<%#AdminUtilities.FormatPrice(Convert.ToDecimal(Eval("Total"))) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="ac" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumberOfOrder" HeaderText="Number Of Order" SortExpression="NumberOfOrder">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Average" SortExpression="Average">
                            <ItemTemplate>
                                <asp:Label ID="uxAverageLabel" runat="server" Text='<%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval("Average"))) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="ac" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quantity" HeaderText="Number Of Items Sold" SortExpression="Quantity">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        </asp:BoundField>
                        <%--   <asp:BoundField DataField="AvgQuantity" HeaderText="Average Items Per Order" SortExpression="AvgQuantity">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Average Items Per Order" SortExpression="AvgQuantity">
                            <ItemTemplate>
                                <asp:Label ID="uxAvgQuantityLabel" runat="server" Text='<%# Convert.ToDecimal( Eval("AvgQuantity")).ToString( "F", System.Globalization.CultureInfo.InvariantCulture ) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="ac" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                            <FooterStyle CssClass="fb b13 c2 pdr15" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="No Data" />
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="GridFooterStyle" />
                    <RowStyle CssClass="GridRowStyle" />
                    <EditRowStyle CssClass="GridEditStyle" />
                    <SelectedRowStyle CssClass="GridSelectStyle" />
                    <PagerStyle CssClass="GridPageStyle" />
                    <HeaderStyle CssClass="GridHeadStyle" />
                    <AlternatingRowStyle CssClass="GridAlternatingRowStyle" />
                </asp:GridView>
            </div>
        </div>
        <%--report table end--%>
        <%--Summary Revenue,tax,shipping,quantity section Begin--%>
        <asp:Panel ID="uxSumPanel" runat="server">
            <div class="DefaultGraphSummary">
                <div class="Section">
                    <asp:Label ID="Label5" runat="server" CssClass="Label">Revenue</asp:Label>
                    <asp:Label ID="uxRevenueValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label7" runat="server" CssClass="Label">Tax</asp:Label>
                    <asp:Label ID="uxTaxValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label9" runat="server" CssClass="Label">Shipping</asp:Label>
                    <asp:Label ID="uxShippingValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Section">
                    <asp:Label ID="Label11" runat="server" CssClass="Label">Quantity</asp:Label>
                    <asp:Label ID="uxQuantityValueLabel" runat="server" CssClass="Value"></asp:Label>
                </div>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
        <%--Summary Revenue,tax,shipping,quantity section End--%>
    </PlainContentTemplate>
</uc1:AdminUserControlContent>
