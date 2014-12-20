<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePayCommissionList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliatePayCommissionList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcPaymentList %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <div class="fl">
            <asp:Label ID="lcShowCommission" runat="server" meta:resourcekey="lcShowCommission"
                CssClass="AffiliateCommissionFilterLabel" />
            <asp:DropDownList ID="uxPeriodDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxPeriodDrop_SelectedIndexChanged"
                CssClass="AffiliateCommissionFilterDropDown">
                <asp:ListItem Selected="true" Value="LastMonth">Until Last Month</asp:ListItem>
                <asp:ListItem Value="ThisMonth">Until This Month</asp:ListItem>
                <asp:ListItem Value="Custom">Custom</asp:ListItem>
            </asp:DropDownList>
            <asp:Panel ID="uxCustomDatePanel" runat="server" CssClass="AffiliateCustomDate" Visible="false">
                <div class="fl">
                    <uc2:CalendarPopup ID="uxStartDate" runat="server" TextBoxWidth="80px" TextBoxEnabled="false"  />
                </div>
                <div class="fl">
                    <uc2:CalendarPopup ID="uxEndDate" runat="server" TextBoxWidth="80px"  TextBoxEnabled="false" />
                </div>
                <div class="fl">
                    <vevo:AdvanceButton ID="uxDateRangeButton" runat="server" Text="Search" CssClass="ButtonOrange" OnClick="uxDateRangeButton_Click"
                        OnClickGoTo="None"></vevo:AdvanceButton></div>
            </asp:Panel>
            <div class="AffiliateCommissionShowName">
                <asp:Literal ID="uxNoteLiteral" runat="server" meta:resourcekey="lcNote" /></div>
            <div class="AffiliateCommissionShowName">
                <asp:Literal ID="uxLastMonthNoteLiteral" runat="server" meta:resourcekey="lcLastMonthNote" />
                <asp:Literal ID="uxTodayNoteLiteral" runat="server" meta:resourcekey="lcTodayNote" />
                <asp:Literal ID="uxCustomNoteLiteral" runat="server" meta:resourcekey="lcCustomNote" /></div>
        </div>
        <div class="fr" style="clear: right;">
            <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
        </div>
    </FilterTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            ShowFooter="True" OnSorting="uxGrid_Sorting" AllowSorting="True" OnRowCreated="SetFooter">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="<%$ Resources:AffiliatePayCommissionFields, Username %>"
                    SortExpression="Username">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePayCommissionFields, FirstName %>"
                    SortExpression="FirstName">
                    <ItemTemplate>
                        <asp:Label ID="uxFirstName" runat="server" Text='<%# Eval("ContactAddress.FirstName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePayCommissionFields, LastName %>"
                    SortExpression="LastName">
                    <ItemTemplate>
                        <asp:Label ID="uxLastName" runat="server" Text='<%# Eval("ContactAddress.LastName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePayCommissionFields, Balance %>"
                    SortExpression="Balance">
                    <ItemTemplate>
                        <asp:Label ID="uxBalanceLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Eval( "Balance" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="CommonGridTotal pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePayCommissionFields, Commissions %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCommissionLink" runat="server" Text="<%$ Resources:AffiliatePayCommissionFields, CommandEditView %>"
                            PageName="AffiliateCommissionList.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}", Eval("AffiliateCode") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0} Edit", Eval("AffiliateCode") )%>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePayCommissionFields, Payments %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxPaymentsLink" runat="server" Text="<%$ Resources:AffiliatePayCommissionFields, CommandEditView %>"
                            PageName="AffiliatePaymentList.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}", Eval("AffiliateCode") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("AffiliateCode" ) ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AffiliateMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
