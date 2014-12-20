<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportSale.ascx.cs" Inherits="AdminAdvanced_MainControls_ReportSale" %>
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
        <vevo:AdvanceButton ID="uxRefreshButton" runat="server" meta:resourcekey="uxGenerateButton" CssClassBegin="fl mgt10 mgb10"
            CssClassEnd="Button1Right" CssClass="ButtonOrange"
            OnClick="uxRefreshButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>

    </FilterTemplate>
    <PlainContentTemplate>
        <div class="ReportGraph">
            <div id="my_chart">
            </div>
        </div>
    </PlainContentTemplate>
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
            AllowSorting="True" OnSorting="uxGrid_Sorting" ShowFooter="true" OnRowCreated="SetFooter"><%----%>
            <Columns>
                <asp:TemplateField HeaderText="Date/Time" SortExpression="Period">
                    <ItemTemplate>
                        <asp:Label ID="uxDateTime" runat="server" Text='<%# GetDateTimeText(Eval("Period")) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" Width="15%"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Total" SortExpression="Total">
                    <ItemTemplate>
                        <asp:Label ID="uxTotalLabel" runat="server" Text='<%#AdminUtilities.FormatPrice(Convert.ToDecimal(Eval("Total"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" Width="15%"/>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:BoundField DataField="NumberOfOrder" HeaderText="Number Of Order" SortExpression="NumberOfOrder">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="15%"/>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Average" SortExpression="Average">
                    <ItemTemplate>
                        <asp:Label ID="uxAverageLabel" runat="server" Text='<%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval("Average"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" Width="15%"/>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:BoundField DataField="Quantity" HeaderText="Number Of Items Sold" SortExpression="Quantity">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AvgQuantity" HeaderText="Average Items Per Order" SortExpression="AvgQuantity">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20%"/>
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
    </GridTemplate>
</uc1:AdminContent>
