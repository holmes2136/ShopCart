<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickBooksLog.ascx.cs"
    Inherits="AdminAdvanced_MainControls_QuickBooksLog" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <asp:Panel ID="ExportedDateTR" runat="server">
            <asp:Label ID="lcExportedDate" runat="server" meta:resourcekey="lcExportDate" />
            <asp:DropDownList ID="uxQBLogDrop" runat="server" AutoPostBack="true" CssClass="DropDown"
                OnSelectedIndexChanged="uxQBLogDrop_OnSelectedIndexChanged">
            </asp:DropDownList>
        </asp:Panel>
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" CssClass="AdminButtonDeleteLog fr mgr10"
            CssClassBegin="AdminButtonDeleteLog" CssClassEnd="Button1Right" ShowText="false"
            OnClickGoTo="Top" OnClick="uxDeleteButton_Click"></vevo:AdvanceButton>
        <asp:Button ID="uxDummyButton" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxDeleteConfirmButton" runat="server" TargetControlID="uxDeleteButton"
            ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxDeleteButton"
            CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" meta:resourcekey="uxDeleteConfirmation"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"  OnClickGoTo="None">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <GridTemplate>
        <div class="TopContent">
            <asp:Label ID="lcExportSummary" runat="server" meta:resourcekey="lcExportSummary" />
        </div>
        <asp:Panel ID="uxTopGridPanel" runat="server">
            <asp:GridView ID="uxGridDetail" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                AllowSorting="true" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="QBExportType" meta:resourcekey="QBExportType">
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SuccessCount" meta:resourcekey="Success">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FailureCount" meta:resourcekey="Failure">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="lcEmptyDetailList" runat="server" meta:resourcekey="lcEmptyDataList" />
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <div class="TopContent mgt20">
            <asp:Label ID="lcExportError" runat="server" meta:resourcekey="lcExportError" />
        </div>
        <asp:Panel ID="uxBottomGridPanel" runat="server">
            <asp:GridView ID="uxItemGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                AllowSorting="true" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="QBLogItemID" meta:resourcekey="QBLogItemID">
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QBExportType" meta:resourcekey="QBExportType">
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusCode" meta:resourcekey="StatusCode">
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusSeverity" meta:resourcekey="StatusSeverity">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusMessage" meta:resourcekey="StatusMessage">
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="lcEmptyItemList" runat="server" meta:resourcekey="lcEmptyDataList" />
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
