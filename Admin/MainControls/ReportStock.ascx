<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportStock.ascx.cs" Inherits="Admin_MainControls_ReportStock" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="Sale Report">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <uc1:Message ID="uxMessage1" runat="server" Visible="false" />
        <div class="CommonDownloadLink">
            <asp:HyperLink ID="uxFileNameLink" runat="server"></asp:HyperLink>
        </div>
    </ButtonEventTemplate>
    <FilterTemplate>
        <div class="fb">
            <asp:Label ID="uxHeaderLabel" runat="server" Text="Select Report Type" /></div>
        <div class="mgb7">
            <div style="width: 100px; float: left;">
                Report Type:
            </div>
            <asp:DropDownList ID="uxReportDrop" runat="server" CssClass="fs11" 
                OnSelectedIndexChanged="uxReportDrop_SelectIndexedChange" AutoPostBack="true" >
                <asp:ListItem>Low Stock</asp:ListItem>
                <asp:ListItem>All Products</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="Clear">
        </div>
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxExportButton" runat="server" meta:resourcekey="uxExportButton" CssClassBegin="fl"
            CssClass="ButtonOrange" CssClassEnd="mgl20 mgt5"
            OnClick="uxExportButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxGrid_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="Product ID" SortExpression="ProductID">
                    <ItemTemplate>
                        <asp:Label ID="uxProductID" runat="server" Text='<%# Eval("ProductID").ToString() %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemTemplate>
                        <asp:Label ID="uxProductName" runat="server" Text='<%# Eval("Name").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SKU" SortExpression="SKU">
                    <ItemTemplate>
                        <asp:Label ID="uxSKU" runat="server" Text='<%# Eval("SKU").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" Width="15%"/>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Option" SortExpression="Option">
                    <ItemTemplate>
                        <asp:Label ID="uxSKU" runat="server" Text='<%# Eval("Option").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" Width="25%"/>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stock" SortExpression="Stock">
                    <ItemTemplate>
                        <asp:Label ID="uxStock" runat="server" Text='<%# Eval("Stock").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" Width="10%"/>
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
    </GridTemplate>
</uc1:AdminContent>