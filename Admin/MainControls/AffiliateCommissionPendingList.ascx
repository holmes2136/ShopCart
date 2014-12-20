<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateCommissionPendingList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliateCommissionPendingList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcCommissionPendingList %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <asp:Literal ID="uxNoteLiteral" runat="server" meta:resourcekey="lcNote" /></div>
    </ValidationDenotesTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxProcessedButton" runat="server" meta:resourcekey="uxProcessedButton"
            CssClassBegin="AdminButton" CssClassEnd=""
            CssClass="AdminButtonMarkSelected CommonAdminButton" ShowText="true" OnClick="uxProcessedButton_Click"
            OnClickGoTo="Top"></vevo:AdvanceButton>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="Gridview1" SkinID="DefaultGridView" ShowFooter="false" AllowSorting="True"
            OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, AffiliateOrderID %>"
                    SortExpression="AffiliateOrderID">
                    <ItemTemplate>
                        <asp:Label ID="uxAffiliateOrderIDLabel" runat="server" Text='<%# Bind("AffiliateOrderID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, OrderID %>" SortExpression="OrderID">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCustomerLink" runat="server" Text='<%# String.Format( "{0}", Eval("OrderID") ) %>'
                            PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("OrderID") ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, Username %>"
                    SortExpression="af.UserName">
                    <ItemTemplate>
                        <asp:Label ID="uxUsernameLabel" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, OrderDate %>"
                    SortExpression="OrderDate">
                    <ItemTemplate>
                        <asp:Label ID="uxOrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, ProductCost %>"
                    SortExpression="o.SubTotal-o.CouponDiscount">
                    <ItemTemplate>
                        <asp:Label ID="uxProductCostLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Eval("ProductCost")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="180px" HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, Commission %>"
                    SortExpression="Commission">
                    <ItemTemplate>
                        <asp:Label ID="uxCommissionLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Eval("Commission")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AffiliatePaymentMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
