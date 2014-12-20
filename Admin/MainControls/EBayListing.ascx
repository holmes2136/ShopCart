<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayListing.ascx.cs" Inherits="Admin_MainControls_EBayListing" %>
<%@ Import Namespace="Vevo" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Assembly="Vevo.WebUI.ServerControls" Namespace="Vevo.WebUI.ServerControls"
    TagPrefix="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventInnerBoxTemplate>
        <Vevo:AdvanceButton ID="uxRefreshButton" runat="server" meta:resourcekey="uxRefreshButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonReset CommonAdminButton"
            ShowText="true" OnClick="uxRefreshButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <Vevo:AdvanceButton ID="uxEndListingButton" runat="server" meta:resourcekey="uxEndListingButton"
            CssClass="AdminButtonDisable CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxEndListingButton_Click" />
        <asp:Button ID="uxDummyButton_2" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxEndListingConfirmButton" runat="server"
            TargetControlID="uxEndListingButton" ConfirmText="" DisplayModalPopupID="uxEndListingConfirmModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxEndListingConfirmModalPopup" runat="server"
            TargetControlID="uxEndListingButton" CancelControlID="uxEndListingCancelButton"
            OkControlID="uxEndListingOkButton" PopupControlID="uxEndListingConfirmPanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxEndListingConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:EBayMessages, EndListingConfirmation %>"></asp:Label>
            </div>
            <div class="ConfirmTitle">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:EBayMessages, EndListingReason %>"></asp:Label>
                <asp:DropDownList ID="uxDropDownReason" runat="server">
                </asp:DropDownList>
            </div>
            <div class="ConfirmButton mgt10">
                <Vevo:AdvanceButton ID="uxEndListingOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                     CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </Vevo:AdvanceButton>
                <Vevo:AdvanceButton ID="uxEndListingCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                   CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"  OnClickGoTo="None">
                </Vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
        <Vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:EBayMessages, DeleteConfirmation %>"></asp:Label></div>
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
    <PageNumberTemplate>
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGirdEBayList" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGridEBayList_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGirdEBayList')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ListID" HeaderText="<%$ Resources:EBayListFields, ListID %>"
                    SortExpression="ListID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemName" HeaderText="<%$ Resources:EBayListFields, ItemName %>"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:EBayListFields, ItemNumber %>">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxHyperlinkItemNumber" runat="server" Target="_blank" NavigateUrl='<%# GetViewURL( Eval("ItemNumber")) %>'
                            Text='<%# Bind("ItemNumber") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="ListDate" HeaderText="<%$ Resources:EBayListFields, ListDate %>"
                    SortExpression="ListDate">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="140px" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl10" />
                </asp:BoundField>
                <asp:BoundField DataField="LastStatus" HeaderText="<%$ Resources:EBayListFields, LastStatus %>"
                    SortExpression="LastStatus">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="55px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:BoundField>
                <asp:BoundField DataField="ListType" HeaderText="<%$ Resources:EBayListFields, ListType %>"
                    SortExpression="ListType">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="55px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:EBayListFields, BidAmount %>" SortExpression="BidAmount">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxBidAmountLabel" Text='<%# GetBidAmount( Eval("ListID")) %>' />
                        <itemstyle horizontalalign="Right" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                    <HeaderStyle Width="75px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:EBayListFields, BidPrice %>" SortExpression="BidPrice">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxBidPriceLabel" Text='<%# String.Format( "{0:f2}",GetBidPrice( Eval( "ListID" ) )) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                    <HeaderStyle Width="115px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:EBayListFields, BuyItNowPrice %>" SortExpression="BuyItNowPrice">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxBuyItNowPriceLabel" Text='<%# String.Format( "{0:f2}",GetBuyItNowPrice( Eval( "ListID" ) )) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                    <HeaderStyle Width="115px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Currency" SortExpression="BuyItNowPrice">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxCurrencyLabel" Text='<%# GetCurrencySymbol(Eval("ListID" )) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:EBayListFields, CommandDetail %>">
                    <ItemTemplate>
                        <Vevo:AdvancedLinkButton ID="uxDetailHyperLink" runat="server" ToolTip="<%$ Resources:EBayListFields,  CommandDetail %>"
                            PageName="EBayListingDetail.ascx" PageQueryString='<%# String.Format( "ListID={0}", Eval("ListID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Detail {0}", Eval("ItemName") ) %>'>
                            <asp:Image ID="uxDetailImage" runat="server" SkinID="IConEditInGrid" />
                        </Vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:EBayMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
