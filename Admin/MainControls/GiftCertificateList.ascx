<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificateList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_GiftCertificateList" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <PageNumberTemplate>
        <uc1:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridGiftCertificate" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, GiftCertificateCode %>"
                    SortExpression="GiftCertificateCode">
                    <ItemTemplate>
                        <%# GetUpper( Eval( "GiftCertificateCode" ) )%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, UserName %>" SortExpression="UserName">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCustomerLink" runat="server" Text='<%# String.Format( "{0}", Eval("UserName") ) %>'
                            PageName="CustomerEdit.ascx" PageQueryString='<%# String.Format( "CustomerID={0}", Eval("CustomerID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("UserName") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, OrderNo %>" SortExpression="UserName">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxOrderIDLink" runat="server" Text='<%# String.Format( "{0}", Eval("OrderID") ) %>'
                            PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("OrderID") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="<%$ Resources:GiftCertificateFields, Name %>"
                    SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, GiftValue %>"
                    SortExpression="GiftValue">
                    <ItemTemplate>
                        <%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval( "GiftValue" ) ) )%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, RemainValue %>"
                    SortExpression="RemainValue">
                    <ItemTemplate>
                        <%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval( "RemainValue" ) ) )%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <%--                <asp:BoundField DataField="Recipient" HeaderText="<%$ Resources:GiftCertificateFields, Recipient %>"
                    SortExpression="Recipient">
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, Recipient %>"
                    SortExpression="Recipient">
                    <ItemTemplate>
                        <asp:Label ID="uxRecipientLabel" runat="server" Text='<%# Eval( "Recipient" ).ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, ExpireDate %>"
                    SortExpression="ExpireDate">
                    <ItemTemplate>
                        <asp:Label ID="uxExpireDateLabel" runat="server" Text='<%# GetExpireDate( Eval( "IsExpirable" ).ToString(), Eval( "ExpireDate" ) ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:GiftCertificateFields, CommandEdit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" OnClick="ChangePage_Click"
                            PageName="GiftCertificateEdit.ascx" PageQueryString='<%# String.Format("GiftCertificateCode={0}", Eval("GiftCertificateCode") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>' ToolTip="<%$ Resources:GiftCertificateFields, CommandEdit %>">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:GiftCertificateMessage, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
