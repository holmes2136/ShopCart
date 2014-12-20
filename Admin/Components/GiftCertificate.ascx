<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCertificate.ascx.cs"
    Inherits="AdminAdvanced_Components_GiftCertificate" %>
<%@ Import Namespace="Vevo" %>
<asp:GridView ID="uxGiftCertificateGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
    DataSourceID="uxGiftCertificateSource" OnRowDataBound="uxGrid_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Name %>" SortExpression="Name">
            <ItemTemplate>
                <asp:Label ID="uxItemName" runat="server" Text='<%# Eval("Name") %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, GiftCertificateCode %>"
            SortExpression="GiftCertificateCode">
            <ItemTemplate>
                <asp:Label ID="uxExpireDateLabel" runat="server" Text='<%# GetGiftCertificateCode( Eval( "GiftCertificateCode" ) ) %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="Recipient" HeaderText="<%$ Resources:OrdersFields, Recipient %>"
            SortExpression="Recipient">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, RemainValue %>" SortExpression="RemainValue">
            <ItemTemplate>
                <%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval( "RemainValue" ) ) )%>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, ExpireDate %>">
            <ItemTemplate>
                <asp:Label ID="uxExpireDateLabel" runat="server" Text='<%# GetExpireDate( Eval( "IsExpirable" ).ToString(), Eval( "ExpireDate" ) ) %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, NeedPhysical %>">
            <ItemTemplate>
                <asp:Label ID="uxNeesPhysicalLabel" runat="server" Text='<%# GetNeedPhysical( Eval( "NeedPhysical" ) ) %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Print %>">
            <ItemTemplate>
                <asp:HyperLink ID="uxPrintGiftLink" runat="server" Text="Print" Target="_blank" NavigateUrl='<%# GetPrintUrl(Eval( "GiftCertificateCode" ).ToString()) %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="uxGiftCertificateSource" runat="server" SelectMethod="GetAllByOrderID"
    TypeName="Vevo.Domain.DataSources.GiftCertificateDataSource">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="0" Direction="Input" Name="orderID" QueryStringField="OrderID" />
    </SelectParameters>
</asp:ObjectDataSource>
