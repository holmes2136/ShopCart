<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderCreateSelectingCustomer.ascx.cs"
    Inherits="Admin_MainControls_OrderCreateSelectingCustomer" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <SpecialFilterTemplate>
        <uc2:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" FirstLineEnable="false" IsDisplayUrl="true" />
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridCustomer" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:BoundField DataField="CustomerID" HeaderText="<%$ Resources:CustomerFields, CustomerID %>"
                    SortExpression="CustomerID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:CustomerFields, UserName %>"
                    SortExpression="UserName">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, FirstName %>" SortExpression="FirstName">
                    <ItemTemplate>
                        <asp:Label ID="uxFirstNameLabel" runat="server" Text='<%# Eval("BillingAddress.FirstName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, LastName %>" SortExpression="LastName">
                    <ItemTemplate>
                        <asp:Label ID="uxLastNameLabel" runat="server" Text='<%# Eval("BillingAddress.LastName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="Email" HeaderText="<%$ Resources:CustomerFields, Email %>"
                    SortExpression="Email">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, SelectCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCustomerSelectButton" runat="server" ToolTip="<%$ Resources:CustomerFields, SelectCommand %>"
                            PageName="OrderCreateCartItemList.ascx" PageQueryString='<%# GetPageQuery (Eval("CustomerID"))  %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("UserName" ) ) %>' OnClick="ChangePage_Click"
                            Text="<%$ Resources:CustomerFields, SelectCommand %>" CssClass="UnderlineDashed">
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CustomerMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
