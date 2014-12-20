<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportCustomer.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ReportCustomer" %>
<%@ Register Src="../Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
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
            <asp:Label ID="uxHeaderLabel" runat="server" Text="Select Period and Report Type" /></div>
        <div class="mgb7">
            <div style="width: 100px; float: left;">
                Report Type:
            </div>
            <asp:DropDownList ID="uxReportDrop" runat="server" CssClass="fs11" DataTextField="Text"
                DataValueField="Value">
            </asp:DropDownList>
        </div>
        <div class="Clear">
        </div>
        <div class="mgb7">
            <div style="width: 100px; float: left;">
                Period:
            </div>
            <asp:DropDownList ID="uxPeriodDrop" runat="server" CssClass="fs11" DataTextField="Text"
                DataValueField="Value">
            </asp:DropDownList>
        </div>
        <div class="Clear">
        </div>
        <div id="uxSetDate" runat="server" style="display: none;" class="mgb7">
            <div style="width: 100px; float: left;">
                Start Date:
            </div>
            <uc1:CalendarPopup ID="uxStartDateCalendarPopup" runat="server" CssClass="fs11" TextBoxHeight="11px"
                ImageButtonCssClass="none" TextBoxEnabled="false" />
            &nbsp;&nbsp; End Date:
            <uc1:CalendarPopup ID="uxEndDateCalendarPopUp" runat="server" CssClass="fs11" TextBoxHeight="11px"
                ImageButtonCssClass="none" TextBoxEnabled="false" />
            <div class="Clear">
            </div>
        </div>
        <div class="Clear">
        </div>
        <div id="uxSetItems" runat="server" style="display: none;" class="mgb7">
            <div style="width: 100px; float: left;">
                Top:
            </div>
            <asp:DropDownList ID="uxNumberItemsDrop" runat="server" CssClass="fs11">
            </asp:DropDownList>
            &nbsp; Items
        </div>
        <div class="Clear">
        </div>
        <vevo:AdvanceButton ID="uxRefreshButton" runat="server" meta:resourcekey="uxGenerateButton"
            CssClassBegin="fl mgt10 mgb10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
            OnClick="uxRefreshButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
    </FilterTemplate>
    <PlainContentTemplate>
        <div class="ReportGraph">
            <div id="my_chart">
            </div>
        </div>
        <div id="uxPadding" runat="server" class="pdt40">
        </div>
        <div class="ReportTitle bottomline">
            <asp:Label ID="uxTitleLabel" runat="server"></asp:Label>
        </div>
        <div class="Clear">
        </div>
    </PlainContentTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxExportButton" runat="server" meta:resourcekey="uxExportButton"
            CssClassBegin="fl" CssClass="ButtonOrange" CssClassEnd="mgl20 mgt5" OnClick="uxExportButton_Click"
            OnClickGoTo="Top"></vevo:AdvanceButton>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxGrid_Sorting" Visible="False" ShowFooter="true"
            OnRowCreated="SetFooter">
            <Columns>
                <asp:TemplateField HeaderText="Date/Time" SortExpression="RegisterPeriod">
                    <ItemTemplate>
                        <asp:Label ID="uxDateTime" runat="server" Text='<%# GetDateTimeText(Eval("RegisterPeriod")) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="RegisterCustomer" HeaderText="Register" SortExpression="RegisterCustomer">
                    <ItemStyle HorizontalAlign="center" />
                    <HeaderStyle HorizontalAlign="center" />
                </asp:BoundField>
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
        <asp:GridView ID="uxTopCustomerGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxTopCustomer_Sorting" Visible="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:BoundField DataField="RegisterDate" HeaderText="Register Date" SortExpression="RegisterDate">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Total" SortExpression="Total">
                    <ItemTemplate>
                        <asp:Label ID="uxTotalLabel" runat="server" Text='<%#AdminUtilities.FormatPrice(Convert.ToDecimal(Eval("Total"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:BoundField DataField="NumberOfOrder" HeaderText="Total Order" SortExpression="NumberOfOrder">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Average" SortExpression="Average">
                    <ItemTemplate>
                        <asp:Label ID="uxAverageLabel" runat="server" Text='<%#AdminUtilities.FormatPrice(Convert.ToDecimal(Eval("Average"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:BoundField DataField="WholeSaleLevel" HeaderText="WholeSale Level" SortExpression="WholeSaleLevel">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyLabel1" runat="server" Font-Bold="true" Text="No Data" />
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
