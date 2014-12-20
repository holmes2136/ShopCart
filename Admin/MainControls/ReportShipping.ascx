<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportShipping.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ReportShipping" %>
<%@ Register Src="../Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="Shipping Report">
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
        <vevo:AdvanceButton ID="uxRefreshButton" runat="server" meta:resourcekey="uxGenerateButton"
            CssClassBegin="fl mgt10 mgb10" CssClassEnd="Button1Right" CssClass="ButtonOrange"
            OnClick="uxRefreshButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
    </FilterTemplate>
    <PlainContentTemplate>
        <div id="uxDisplayChart" runat="server" class="bottomline">
            <div class="ReportGraph">
                <div id="my_chart">
                </div>
            </div>
            <div class="pdt30">
            </div>
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
            AllowSorting="True" OnSorting="uxGrid_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="Shipping Method" SortExpression="ShippingMethod">
                    <ItemTemplate>
                        <asp:Label ID="uxShippingMethodLabel" runat="server" Text='<%# GetShippingMethod(Eval("ShippingMethod").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="SumShippingMethod" HeaderText="Use Count" SortExpression="SumShippingMethod">
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
