<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportSearch.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ReportSearch" %>
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
        <div class="mgb7">
            <div style="width: 100px; float: left;">
                Top:
            </div>
            <div class="fl">
                <asp:DropDownList ID="uxNumberItemsDrop" runat="server" CssClass="fs11">
                </asp:DropDownList>
            </div>
            <div class="fl">
                &nbsp;Items
            </div>
        </div>
        <div class="Clear">
        </div>
        <vevo:AdvanceButton ID="uxRefreshButton" runat="server" meta:resourcekey="uxGenerateButton"
            CssClassBegin="fl mgt10 mgb10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
            OnClick="uxRefreshButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
    </FilterTemplate>
    <PlainContentTemplate>
        <div class="ReportTitle bottomline">
            <asp:Label ID="uxTitleLabel" runat="server"></asp:Label>
        </div>
    </PlainContentTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxExportButton" runat="server" meta:resourcekey="uxExportButton" CssClassEnd="mgl20 mgt5"
            CssClassBegin="fl" CssClass="ButtonOrange" OnClick="uxExportButton_Click" OnClickGoTo="Top">
        </vevo:AdvanceButton>
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxGrid_Sorting">
            <Columns>
                <asp:BoundField DataField="Keyword" HeaderText="Keyword" SortExpression="Keyword">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:BoundField DataField="Found" HeaderText="Found" SortExpression="Found">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NotFound" HeaderText="Not Found" SortExpression="NotFound">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Percent" SortExpression="Percentage">
                    <ItemTemplate>
                        <asp:Label ID="uxPercentLabel" runat="server" Text='<%# String.Format( "{0:n2} %", Eval("Percentage") ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
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
