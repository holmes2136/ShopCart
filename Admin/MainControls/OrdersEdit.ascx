<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrdersEdit.ascx.cs" Inherits="AdminAdvanced_MainControls_OrdersEdit" %>
<%@ Register Src="../Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc5" %>
<%@ Register Src="../Components/GiftCertificate.ascx" TagName="GiftCertificate" TagPrefix="uc4" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Common/CountryList.ascx" TagName="CountryList" TagPrefix="uc2" %>
<%@ Register Src="../Components/Common/StateList.ascx" TagName="StateList" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI.Users" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:AdvanceButton ID="uxEditTopButton" runat="server" meta:resourcekey="uxEditButton"
            ShowText="true" CssClass="CommonAdminButtonIcon AdminButtonIconEdit fl" CausesValidation="false"
            OnCommand="uxEditButton_Command" OnPreRender="uxEditButton_PreRender" OnClickGoTo="None" />
        <vevo:AdvanceButton ID="uxPrintTopButton" runat="server" meta:resourcekey="uxPrintButton"
            CssClass="CommonAdminButtonIcon AdminButtonIconPrint fl" CausesValidation="false"
            ShowText="true" OnLoad="uxPrintButton_Load" OnClickGoTo="None" />
        <vevo:AdvanceButton ID="uxPackingSlipButton" runat="server" meta:resourcekey="uxPackingSlipButton"
            CssClass="CommonAdminButtonIcon AdminButtonCreate fl" CausesValidation="false"
            OnClick="CreatePackingSlip_Click" />
        <vevo:AdvanceButton ID="uxInvoiceButton" runat="server" meta:resourcekey="uxInvoiceButton"
            CssClass="CommonAdminButtonIcon AdminButtonCreate fl" CausesValidation="false"
            OnClick="CreateInvoice_Click" />
        <vevo:AdvanceButton ID="uxTrackingLink" runat="server" meta:resourcekey="uxTrackingLink"
            CssClass="CommonAdminButtonIcon AdminButtonIconView fl" CausesValidation="false"
            OnClick="uxTrackingLink_Click" />
    </ButtonEventTemplate>
    <PlainContentTemplate>
        <div id="PrintArea">
            <div id="ButtonDeleteRemove">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="AdminButton" CssClassEnd="ButtonEvent" CssClass="AdminButtonIconAdd CommonAdminButton fl"
                    ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
                <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
                    CssClass="AdminButtonDelete CommonAdminButton fl mgl10" CssClassBegin="AdminButton"
                    CssClassEnd="ButtonEvent" ShowText="true" OnClick="uxDeleteButton_Click" />
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
                        <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:OrdersMessages, DeleteConfirmation %>" /></div>
                    <div class="ConfirmButton mgt10">
                        <vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                        </vevo:AdvanceButton>
                        <vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                        </vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTitle">
                <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" CssClass="fl mgl5" />
                <asp:Label ID="uxOrderIDLabel" runat="server" CssClass="fl mgl5 " />
                <vevo:AdvancedLinkButton ID="uxParentOrderLink" runat="server" PageName="OrdersEdit.ascx"
                    OnClick="ChangePage_Click" ToolTip="<%$ Resources:OrdersFields, EditCommand %>"
                    CssClass="mgl10 NormalLink"></vevo:AdvancedLinkButton>
                <asp:Label ID="uxParentOrderEndLabel" runat="server" CssClass="fl c10" />
                <div class="Clear">
                </div>
            </div>
            <asp:GridView ID="uxItemGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                DataSourceID="uxOrderItemsSource" OnRowDataBound="uxGrid_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxItemGrid')">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="uxHidden" runat="server" Value='<%# Eval( "OrderItemID" ) %>' />
                            <asp:CheckBox ID="uxCheck" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrderItemID" HeaderText="<%$ Resources:OrdersFields, OrderItem %>"
                        Visible="False">
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        <HeaderStyle Wrap="True" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources:OrdersFields, ProductID %>"
                        ReadOnly="True" SortExpression="ProductID">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Name %>" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="uxItemName" runat="server" Text='<%# (Eval("UploadFile").ToString() != "")? GenarateLink(uxOrderIDLabel.Text,Eval("ProductID").ToString(), Eval("Name").ToString(),Eval( "OrderItemID" ).ToString()): Eval("Name").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl10 al pdt5 pdb5" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="pdl10 al" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources:OrdersFields, Quantity %>"
                        SortExpression="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sku" HeaderText="<%$ Resources:OrdersFields, Sku %>" SortExpression="Sku">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, UnitPrice %>" SortExpression="UnitPrice">
                        <ItemTemplate>
                            <div style="width: 90px;" class="fr ar mgr10">
                                <%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval( "UnitPrice" ) ) ) %>
                            </div>
                            <div class="Clear">
                            </div>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, SubTotal %>">
                        <ItemTemplate>
                            <div style="width: 90px;" class="fr ar mgr10">
                                <%# AdminUtilities.FormatPrice( Convert.ToDecimal( Eval( "ItemSubtotal" ) ) ) %>
                            </div>
                            <div class="Clear">
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <vevo:AdvancedLinkButton ID="uxOrderItemEditLink" runat="server" PageName="OrderItemsEdit.ascx"
                                PageQueryString='<%# String.Format( "OrderID={0}&OrderItemID={1}", Eval("OrderID"), Eval("OrderItemID") ) %>'
                                StatusBarText='<%# String.Format( "Edit {0}", Eval("Name" ) ) %>' OnClick="ChangePage_Click"
                                Visible='<%# (ConvertUtilities.ToInt32( Eval("RecurringID") ) > 0 )? false : true %>'
                                ToolTip="<%$ Resources:OrdersFields, EditCommand %>">
                                    <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" /></vevo:AdvancedLinkButton>
                            <asp:LinkButton ID="uxCancelLinkButton" runat="server" CommandArgument='<%# Eval( "RecurringID" ) %>'
                                CommandName='<%# GetOrderUserNameByOrderID( Eval( "OrderID" ) ) %>' Visible='<%# IsActive( Eval( "OrderItemID" ) ) && GetRecurringEndDate( Eval("RecurringID") ) > DateTime.Today %>'
                                OnCommand="uxCancelLinkButton_Command" OnLoad="uxCancelLinkButton_Load">Cancel</asp:LinkButton>
                            <asp:Label ID="uxRecurringCancelLabel" runat="server" Text='<%# RecurringStatus( Eval("OrderItemID") ) %>'
                                Visible='<%# !IsActive( Eval( "OrderItemID") ) && ( ConvertUtilities.ToInt32( Eval("RecurringID") ) > 0 ) %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="35px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:OrderItemMessage, TableEmpty  %>" />
                </EmptyDataTemplate>
            </asp:GridView>
            <table cellpadding="0" cellspacing="0" class="OrderEditTotalBox">
                <tr>
                    <td>
                        <div class="SummaryRow">
                            <asp:Label ID="uxProductCostLabel" runat="server" CssClass="Value" /><asp:Label ID="uxProduct"
                                runat="server" Text="<%$ Resources:OrdersFields, Subtotal %>" CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="SummaryRow">
                            <asp:Label ID="uxDiscountLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxDiscount" runat="server" Text="<%$ Resources:OrdersFields, Discount %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <% if (DisplayPointDiscount())
                           { %>
                        <div class="SummaryRow">
                            <asp:Label ID="uxPointDiscountLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxPointDiscount" runat="server" Text="<%$ Resources:OrdersFields, PointDiscount %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <%} %>
                        <div class="SummaryRow">
                            <asp:Label ID="uxTaxLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxTax" runat="server" Text="<%$ Resources:OrdersFields, Tax %>" CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="SummaryRow">
                            <asp:Label ID="uxShippingCostLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxShippingCost" runat="server" Text="<%$ Resources:OrdersFields, ShippingCost %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <asp:Panel ID="UxHandlingFeeTR" runat="server" CssClass="SummaryRow">
                            <asp:Label ID="uxHandlindFeeLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="lcHandlindFeeLabel" runat="server" Text="<%$ Resources:OrdersFields, HandlingFee %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="uxGiftCertificateTR" runat="server" CssClass="SummaryRow">
                            <asp:Label ID="uxGiftCertificateLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="lcGiftCertificate" runat="server" Text="<%$ Resources:OrdersFields, GiftCertificate %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </asp:Panel>
                        <div class="SummaryRow">
                            <asp:Label ID="uxTotalLabel" runat="server" CssClass="Value fb" /><asp:Label ID="uxTotal"
                                runat="server" Text="<%$ Resources:OrdersFields, Total %>" CssClass="Label fb" />
                            <div class="Clear">
                            </div>
                        </div>
                    </td>
                    <td style="width: 36px;" class="SpaceCol">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <uc4:GiftCertificate ID="uxGiftCertificate" runat="server" />
            <asp:FormView ID="uxFormView" runat="server" DataSourceID="uxOrderDetailsSource"
                CssClass="FormView1 mgt10 Container-Row" OnDataBound="uxFormView_DataBound" CellSpacing="1">
                <ItemTemplate>
                    <%-----------------------------------------------------------------------------%>
                    <%-- ItemTemplate --%>
                    <%-----------------------------------------------------------------------------%>
                    <div id="topButtonRemove">
                        <vevo:AdvanceButton ID="uxSendTrackingNumberTopButton" runat="server" meta:resourcekey="uxSendTrackingNumberButton"
                            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" CausesValidation="false"
                            OnClick="SendTrackingNumber_Click" OnPreRender="uxSendTrackingNumberButton_PreRender"
                            OnClickGoTo="None" Visible='<%# HaveTrackingNo(Eval("TrackingNumber").ToString()) %>'>
                        </vevo:AdvanceButton>
                        <ajaxToolkit:ConfirmButtonExtender ID="uxSendTrackingNumberTopConfirmButton" runat="server"
                            TargetControlID="uxSendTrackingNumberTopButton" ConfirmText="" DisplayModalPopupID="uxSendTrackingNumberTopConfirmModalPopup">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="uxSendTrackingNumberTopConfirmModalPopup" runat="server"
                            TargetControlID="uxSendTrackingNumberTopButton" CancelControlID="uxSendTrackingNumberTopCancelButton"
                            OkControlID="uxSendTrackingNumberTopOkButton" PopupControlID="uxSendTrackingNumberTopConfirmPanel"
                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="uxSendTrackingNumberTopConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                            SkinID="ConfirmPanel">
                            <div class="ConfirmTitle">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:OrdersMessages, SendTrackingNumber %>" /></div>
                            <div class="ConfirmButton mgt10">
                                <vevo:AdvanceButton ID="uxSendTrackingNumberTopOkButton" runat="server" Text="OK"
                                    CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="Top"></vevo:AdvanceButton>
                                <vevo:AdvanceButton ID="uxSendTrackingNumberTopCancelButton" runat="server" Text="Cancel"
                                    CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="None"></vevo:AdvanceButton>
                                <div class="Clear">
                                </div>
                            </div>
                        </asp:Panel>
                        <vevo:AdvanceButton ID="uxSendGiftCertificateButton2" runat="server" meta:resourcekey="uxSendGiftCertificateButton"
                            CausesValidation="false" CssClassBegin="fr" CssClassEnd="Button1Right"
                            CssClass="ButtonOrange" OnClickGoTo="Top" Visible='<%# HaveGiftCertificate() %>'
                            OnClick="uxSendGiftCertificateButton_Click" OnPreRender="uxSendGiftCertificateButton_PreRender" />
                        <asp:Button ID="uxSendGiftCertificateDummyButton" runat="server" Text="" CssClass="dn" />
                        <ajaxToolkit:ConfirmButtonExtender ID="uxSendGiftCertificateConfirmButton" runat="server"
                            TargetControlID="uxSendGiftCertificateButton2" ConfirmText="" DisplayModalPopupID="uxSendGiftCertificateConfirmModalPopup">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="uxSendGiftCertificateConfirmModalPopup" runat="server"
                            TargetControlID="uxSendGiftCertificateButton2" CancelControlID="uxSendGiftCertificateCancelButton"
                            OkControlID="uxSendGiftCertificateOkButton" PopupControlID="uxSendGiftCertificateConfirmPanel"
                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="uxSendGiftCertificateConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                            SkinID="ConfirmPanel">
                            <div class="ConfirmTitle">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:OrdersMessages, SendGiftCertificate %>" /></div>
                            <div class="ConfirmButton mgt10">
                                <vevo:AdvanceButton ID="uxSendGiftCertificateOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                                </vevo:AdvanceButton>
                                <vevo:AdvanceButton ID="uxSendGiftCertificateCancelButton" runat="server" Text="Cancel"
                                    CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="None"></vevo:AdvanceButton>
                                <div class="Clear">
                                </div>
                            </div>
                        </asp:Panel>
                        <vevo:AdvanceButton ID="uxSendEGoodEmailButton" runat="server" meta:resourcekey="uxSendEGoodEmailButton"
                            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClickGoTo="Top"
                            Visible='<%# HaveEGood() %>' OnClick="uxSendEGoodEmailButton_Click"></vevo:AdvanceButton>
                        <asp:Button ID="uxSendEGoodDummyButton" runat="server" Text="" CssClass="dn" />
                        <ajaxToolkit:ConfirmButtonExtender ID="uxSendEGoodEmailConfirmButton" runat="server"
                            TargetControlID="uxSendEGoodEmailButton" ConfirmText="" DisplayModalPopupID="uxEGoodEmailConfirmModalPopup">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="uxEGoodEmailConfirmModalPopup" runat="server"
                            TargetControlID="uxSendEGoodEmailButton" CancelControlID="uxEGoodEmailCancelButton"
                            OkControlID="uxEGoodEmailOkButton" PopupControlID="uxSendEGoodEmailConfirmPanel"
                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="uxSendEGoodEmailConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                            SkinID="ConfirmPanel">
                            <div class="ConfirmTitle">
                                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:OrdersMessages, SendEGoodEmail %>" /></div>
                            <div class="ConfirmButton mgt10">
                                <vevo:AdvanceButton ID="uxEGoodEmailOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                                </vevo:AdvanceButton>
                                <vevo:AdvanceButton ID="uxEGoodEmailCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                                </vevo:AdvanceButton>
                                <div class="Clear">
                                </div>
                            </div>
                        </asp:Panel>
                        <vevo:AdvanceButton ID="uxSendSubscriptionMailButton" runat="server" meta:resourcekey="uxSendSubscriptionMailButton"
                            CausesValidation="false" CssClassBegin="fr" CssClassEnd="Button1Right"
                            CssClass="ButtonOrange" OnClickGoTo="Top" Visible='<%# HaveProductSubscription() %>'
                            OnClick="uxSendSubscriptionMailButton_Click" OnPreRender="uxSendSubscriptionMailButton_PreRender" />
                        <ajaxToolkit:ConfirmButtonExtender ID="uxSendSubscriptionConfirmButton" runat="server"
                            TargetControlID="uxSendSubscriptionMailButton" ConfirmText="" DisplayModalPopupID="uxSendSubscriptionMailConfirmModalPopup">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="uxSendSubscriptionMailConfirmModalPopup" runat="server"
                            TargetControlID="uxSendSubscriptionMailButton" CancelControlID="uxSendSubscriptionMailCancelButton"
                            OkControlID="uxSendSubscriptionMailOkButton" PopupControlID="uxSendSubscriptionMailConfirmPanel"
                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="uxSendSubscriptionMailConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                            SkinID="ConfirmPanel">
                            <div class="ConfirmTitle">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:OrdersMessages, SendSubscriptionMail %>" /></div>
                            <div class="ConfirmButton mgt10">
                                <vevo:AdvanceButton ID="uxSendSubscriptionMailOkButton" runat="server" Text="OK"
                                    CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="Top"></vevo:AdvanceButton>
                                <vevo:AdvanceButton ID="uxSendSubscriptionMailCancelButton" runat="server" Text="Cancel"
                                    CssClassBegin="fr mgt10 mgb10 mgr10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="None"></vevo:AdvanceButton>
                                <div class="Clear">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="OrderEditRowTitle">
                        <asp:Label ID="lcOrderStatus" runat="server" meta:resourcekey="lcOrderStatus" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcProcessed" runat="server" meta:resourcekey="lcProcessed" CssClass="Label pdt5" />
                        <asp:Label ID="uxProcessedLabel" runat="server" Text='<%# (bool) Eval("Processed") ? "Yes" : "Unprocessed" %>'
                            ForeColor='<%# (bool) Eval("Processed") ? System.Drawing.Color.Black : System.Drawing.Color.Red %>'
                            Font-Bold='<%# !(bool) Eval("Processed") %>' CssClass="fl al pdt5">
                        </asp:Label>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcStatus" runat="server" meta:resourcekey="lcStatus" CssClass="Label pdt5" />
                        <asp:Label ID="lcStatusValue" runat="server" Text='<%# Eval("Status") %>' CssClass="fl pdt5" /></div>
                    <div class="Clear">
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCancel" runat="server" meta:resourcekey="lcCancel" CssClass="Label pdt5" />
                        <asp:Label ID="lcCancelValue" runat="server" Text='<%# ConvertUtilities.ToYesNo( (bool) Eval("Cancelled") ) %>'
                            CssClass="fl pdt5" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcOrderDate" runat="server" meta:resourcekey="lcOrderDate" CssClass="Label pdt5" />
                        <asp:Label ID="uxOrderDate" runat="server" Text='<%# Convert.ToDateTime( Eval( "OrderDate" ) ).ToShortDateString() %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTrackingMethod" runat="server" meta:resourcekey="lcTrackingMethod"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcTrackingMethodValue" runat="server" Text='<%# Eval( "TrackingMethod" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTrackingNumber" runat="server" meta:resourcekey="lcTrackingNumber"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcTrackingNumberValue" runat="server" Text='<%# Eval( "TrackingNumber" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="OrderEditRowTitle mgt20">
                        <asp:Label ID="lcPaymentStatus" runat="server" meta:resourcekey="lcPaymentStatus" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPaymentMethod" runat="server" meta:resourcekey="lcPaymentMethod"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcPaymentMethodValue" runat="server" Text='<%# Eval("PaymentMethod") %>'
                            CssClass="fl pdt5" />
                        <asp:LinkButton ID="uxViewCreditCardLink" runat="server" CssClass="ViewCreditCardLink UnderlineDashed"
                            meta:resourcekey="uxViewCreditCardLink" OnLoad="uxViewCreditCardLink_Load"></asp:LinkButton>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle" id="divPONumber" runat="server">
                        <asp:Panel ID="uxPanelPONumber" runat="server">
                            <asp:Label ID="lcPONumber" runat="server" meta:resourcekey="lcPONumber" CssClass="Label pdt5" />
                            <asp:Label ID="uxPONumber" runat="server" Text='<%# Eval( "PONumber" ) %>' CssClass="fl pdt5" />
                        </asp:Panel>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPaymentComplete" runat="server" meta:resourcekey="lcPaymentComplete"
                            CssClass="Label pdt5" />
                        <asp:Label ID="uxPaymentCompleteLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( (bool) Eval("PaymentComplete") ) %>'
                            ForeColor='<%# (bool) Eval("PaymentComplete") ? System.Drawing.Color.Black : System.Drawing.Color.Red %>'
                            Font-Bold='<%# !(bool) Eval("PaymentComplete") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcAvsAddrLabel" runat="server" meta:resourcekey="lcAvsAddrLabel" CssClass="Label pdt5" />
                        <asp:Label ID="uxAvsAddrLabel" runat="server" Text='<%# ( Eval( "AvsAddrStatus" ).ToString() == "Unavailable" )? "Not Available":Eval( "AvsAddrStatus" ) %>'
                            CssClass="fl pdt5" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcAvsZipLabel" runat="server" meta:resourcekey="lcAvsZipLabel" CssClass="Label pdt5" />
                        <asp:Label ID="uxAvsZipLabel" runat="server" Text='<%# ( Eval( "AvsZipStatus" ).ToString() == "Unavailable" )? "Not Available":Eval( "AvsZipStatus" ) %>'
                            CssClass="fl pdt5" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCvvLabel" runat="server" meta:resourcekey="lcCvvLabel" CssClass="Label pdt5" />
                        <asp:Label ID="uxCvvLabel" runat="server" Text='<%# ( Eval( "CvvStatus" ).ToString() == "Unavailable" )? "Not Available":Eval( "CvvStatus" ) %>'
                            CssClass="fl pdt5" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcBaseCurrencyCode" runat="server" meta:resourcekey="lcBaseCurrencyCode"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcBaseCurrencyCodeValue" runat="server" Text='<%# Eval( "BaseCurrencyCode" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserCurrencyCode" runat="server" meta:resourcekey="lcUserCurrencyCode"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcUserCurrencyCodeValue" runat="server" Text='<%# Eval( "UserCurrencyCode" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserConversionRate" runat="server" meta:resourcekey="lcUserConversionRate"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcUserConversionRateValue" runat="server" Text='<%# Eval( "UserConversionRate" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="OrderEditRowTitle mgt20">
                        <asp:Label ID="lcAddress" runat="server" meta:resourcekey="lcAddress" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" CssClass="Label pdt5" />
                        <asp:Label ID="lcUserNameValue" runat="server" Text='<%# Eval("UserName") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcFirstName" runat="server" meta:resourcekey="lcFirstName" CssClass="Label pdt5" />
                        <asp:Label ID="lcFirstNameValue" runat="server" Text='<%# Eval("Billing.FirstName") %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label pdt5" />
                        <asp:Label ID="lcLastNameValue" runat="server" Text='<%# Eval("Billing.LastName") %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label pdt5" />
                        <asp:Label ID="lcCountryValue" runat="server" Text='<%#  AddressUtilities.GetCountryNameByCountryCode( Eval( "Billing.Country" ).ToString() ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label pdt5" />
                        <asp:Label ID="lcCompanyValue" runat="server" Text='<%# Eval("Billing.Company") %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label pdt5">:</asp:Label>
                        <asp:Label ID="uxAddressValue" runat="server" Text='<%# Eval("Billing.Address1") %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label pdt5">
                            &nbsp;</div>
                        <asp:Label ID="uxAddress2Value" runat="server" Text='<%# Eval("Billing.Address2") %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity" CssClass="Label pdt5" />
                        <asp:Label ID="lcCityValue" runat="server" Text='<%# Eval("Billing.City") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcState" runat="server" meta:resourcekey="lcState" CssClass="Label pdt5" />
                        <asp:Label ID="lcStateValue" runat="server" Text='<%# AddressUtilities.GetStateNameByStateCode( Eval("Billing.Country").ToString(), Eval("Billing.State").ToString() ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcZip" runat="server" meta:resourcekey="lcZip" CssClass="Label pdt5" />
                        <asp:Label ID="lcZipValue" runat="server" Text='<%# Eval("Billing.Zip") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label pdt5" />
                        <asp:Label ID="lcPhoneValue" runat="server" Text='<%# Eval("Billing.Phone") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcFax" runat="server" meta:resourcekey="lcFax" CssClass="Label pdt5" />
                        <asp:Label ID="lcFaxValue" runat="server" Text='<%# Eval("Billing.Fax") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail" CssClass="Label pdt5" />
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Email") %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxShippingAddressPanel" runat="server" Visible='<%# IsShippingAddressMode() %>'>
                        <div class="OrderEditRowTitle mgt20">
                            <asp:Label ID="lcShipping" runat="server" meta:resourcekey="lcShipping" />
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"
                                CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingFirstNameValue" runat="server" Text='<%# Eval("Shipping.FirstName") %>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingLastNameValue" runat="server" Text='<%# Eval( "Shipping.LastName" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingCountryValue" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode ( Eval( "Shipping.Country" ).ToString() )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingCompanyValue" runat="server" Text='<%# Eval( "Shipping.Company" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label pdt5">:</asp:Label>
                            <asp:Label ID="lcShippingAddressValue" runat="server" Text='<%# Eval( "Shipping.Address1" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <div class="Label pdt5">
                            </div>
                            <asp:Label ID="lcShippingAddress2Value" runat="server" Text='<%# Eval( "Shipping.Address2" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingCityValue" runat="server" Text='<%# Eval( "Shipping.City" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingState" runat="server" meta:resourcekey="lcState" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingStateValue" runat="server" Text='<%# AddressUtilities.GetStateNameByStateCode( Eval("Shipping.Country").ToString(), Eval( "Shipping.State" ).ToString() ) %>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingZip" runat="server" meta:resourcekey="lcZip" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingZipValue" runat="server" Text='<%# Eval( "Shipping.Zip" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingPhoneValue" runat="server" Text='<%# Eval( "Shipping.Phone" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingFax" runat="server" meta:resourcekey="lcFax" CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingFaxValue" runat="server" Text='<%# Eval( "Shipping.Fax" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="CommonRowStyle">
                            <asp:Label ID="lcShippingMethod" runat="server" meta:resourcekey="lcShippingMethod"
                                CssClass="Label pdt5" />
                            <asp:Label ID="lcShippingMethodValue" runat="server" Text='<%# Eval( "ShippingMethod" )%>'
                                CssClass="fl pdt5" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="OrderEditRowTitle mgt20">
                        <asp:Label ID="lcMiscellaneous" runat="server" meta:resourcekey="lcMiscellaneous" /></strong>
                    </div>
                    <asp:Panel ID="uxTaxExemptIDPanel" CssClass="CommonRowStyle" runat="server" Visible='<%# IsSaleTaxExemptVisible( true ) %>'>
                        <asp:Label ID="uxTaxExemptIDLabel" runat="server" Text="Tax Exempt ID" CssClass="Label pdt5" />
                        <asp:Label ID="uxTaxExemptIDValue" runat="server" Text='<%# Eval( "TaxExemptID" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxTaxExemptCountryPanel" CssClass="CommonRowStyle" runat="server"
                        Visible='<%# IsSaleTaxExemptVisible( true ) %>'>
                        <asp:Label ID="uxTaxExemptCountryLabel" runat="server" Text="Tax Exempt Country"
                            CssClass="Label pdt5" />
                        <asp:Label ID="uxTaxExemptCountryValue" runat="server" Text='<%# AddressUtilities.GetCountryNameByCountryCode( Eval( "TaxExemptCountry" ).ToString() ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxTaxExemptStatePanel" CssClass="CommonRowStyle" runat="server" Visible='<%# IsSaleTaxExemptVisible( true ) %>'>
                        <asp:Label ID="uxTaxExemptStateLabel" runat="server" Text="Tax Exempt State" CssClass="Label pdt5" />
                        <asp:Label ID="uxTaxExemptStateValue" runat="server" Text='<%# AddressUtilities.GetStateNameByStateCode( Eval( "TaxExemptCountry" ).ToString(), Eval( "TaxExemptState" ).ToString() ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxStoreNamePanel" CssClass="CommonRowStyle" runat="server" Visible='<%# Vevo.KeyUtilities.IsMultistoreLicense() %>'>
                        <asp:Label ID="uxStoreNameLabel" runat="server" Text="Store Name" CssClass="Label pdt5" />
                        <asp:Label ID="uxStoreNameValue" runat="server" Text='<%# GetStoreName( Eval( "StoreID" ).ToString()) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcIpAddress" runat="server" Text="IP Address" CssClass="Label pdt5" />
                        <asp:Label ID="lcIpAddressValue" runat="server" Text='<%# Eval( "IPAddress" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTax" runat="server" meta:resourcekey="lcTax" CssClass="Label pdt5" />
                        <asp:Label ID="lcTaxValue" runat="server" Text='<%# Eval( "Tax", "{0:n2}" ) %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCost" runat="server" meta:resourcekey="lcShippingCost" CssClass="Label pdt5" />
                        <asp:Label ID="lcShippingCostValue" runat="server" Text='<%# Eval( "ShippingCost", "{0:n2}" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCouponDiscount" runat="server" meta:resourcekey="lcCouponDiscount"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcCouponDiscountValue" runat="server" Text='<%# Eval( "CouponDiscount", "{0:n2}" ) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCouponID" runat="server" meta:resourcekey="lcCouponID" CssClass="Label pdt5" />
                        <asp:Label ID="lcCouponIDValue" runat="server" Text='<%# Eval( "CouponID" ) %>' CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcGiftCertificate" runat="server" meta:resourcekey="lcGiftCertificate"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcGiftCertificateValue" runat="server" Text='<%# Eval( "GiftCertificate", "{0:n2}" )%>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcGiftCertificateCode" runat="server" meta:resourcekey="lcGiftCertificateCode"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcGiftCertificateCodeValue" runat="server" Text='<%# Eval( "GiftCertificateCode" )%>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxHandlingFeePanel" runat="server" Visible='<%# IsHandlingFeeVisible() %>'
                        CssClass="CommonRowStyle">
                        <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee" CssClass="Label pdt5" />
                        <asp:Label ID="lcHandlingFeeValue" runat="server" Text='<%# Eval( "HandlingFee", "{0:n2}" )%>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxRewardPointPanel" CssClass="CommonRowStyle" runat="server" Visible='<%# IsRewardPointVisible() %>'>
                        <asp:Label ID="uxRewardPointLabel" runat="server" meta:resourcekey="uxRewardPointLabel"
                            CssClass="Label pdt5" />
                        <asp:Label ID="uxRewardPointValueLabel" runat="server" Text='<%# Eval( "PointEarned" ).ToString() %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCustomerComments" runat="server" meta:resourcekey="lcCustomerComments"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcCustomerCommentsValue" runat="server" Text='<%# WebUtilities.ReplaceNewLine( ConvertUtilities.ToString( Eval( "CustomerComments" ) ) )%>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxInvoiceNotesPanel" runat="server" Visible="false">
                        <asp:Label ID="lcInvoiceNotesLabel" runat="server" meta:resourcekey="lcInvoiceNotes"
                            CssClass="Label pdt5" />
                        <asp:Label ID="lcInvoiceNotesLabelValue" runat="server" Text='<%# WebUtilities.ReplaceNewLine( ConvertUtilities.ToString( Eval( "InvoiceNotes" ) ) )%>'
                            CssClass="fl pdt5" />
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                    <div id="BottomButtonRemove">
                        <vevo:AdvanceButton ID="uxSendTrackingNumberBottomButton" runat="server" meta:resourcekey="uxSendTrackingNumberButton"
                            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" CausesValidation="false"
                            OnClick="SendTrackingNumber_Click" OnPreRender="uxSendTrackingNumberButton_PreRender"
                            OnClickGoTo="None" Visible='<%# HaveTrackingNo(Eval("TrackingNumber").ToString()) %>'>
                        </vevo:AdvanceButton>
                        <ajaxToolkit:ConfirmButtonExtender ID="uxSendTrackingNumberBottomConfirmButton" runat="server"
                            TargetControlID="uxSendTrackingNumberBottomButton" ConfirmText="" DisplayModalPopupID="uxSendTrackingNumberBottomConfirmModalPopup">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <ajaxToolkit:ModalPopupExtender ID="uxSendTrackingNumberBottomConfirmModalPopup"
                            runat="server" TargetControlID="uxSendTrackingNumberBottomButton" CancelControlID="uxSendTrackingNumberBottomCancelButton"
                            OkControlID="uxSendTrackingNumberBottomOkButton" PopupControlID="uxSendTrackingNumberBottomConfirmPanel"
                            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="uxSendTrackingNumberBottomConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
                            SkinID="ConfirmPanel">
                            <div class="ConfirmTitle">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:OrdersMessages, SendTrackingNumber %>" /></div>
                            <div class="ConfirmButton mgt10">
                                <vevo:AdvanceButton ID="uxSendTrackingNumberBottomOkButton" runat="server" Text="OK"
                                    CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="Top"></vevo:AdvanceButton>
                                <vevo:AdvanceButton ID="uxSendTrackingNumberBottomCancelButton" runat="server" Text="Cancel"
                                    CssClassBegin="fl mgt10 mgb10 mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                    OnClickGoTo="Top"></vevo:AdvanceButton>
                                <div class="Clear">
                                </div>
                            </div>
                        </asp:Panel>
                        <vevo:AdvanceButton ID="uxPrintBottomButton" runat="server" meta:resourcekey="uxPrintButton"
                            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            CausesValidation="false" OnClickGoTo="None" OnLoad="uxPrintButton_Load">
                        </vevo:AdvanceButton>
                        <vevo:AdvanceButton ID="uxEditBottomButton" runat="server" meta:resourcekey="uxEditButton"
                            CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            CausesValidation="false" OnClickGoTo="None" CommandName="Edit"
                            OnPreRender="uxEditButton_PreRender"></vevo:AdvanceButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <%-----------------------------------------------------------------------------%>
                    <%-- EditItemTemplate --%>
                    <%-----------------------------------------------------------------------------%>
                    <div class="OrderEditRowTitle">
                        <asp:Label ID="lcOrderStatus" runat="server" meta:resourcekey="lcOrderStatus" /></strong>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcProcessed" runat="server" meta:resourcekey="lcProcessed" CssClass="Label" />
                        <asp:CheckBox ID="uxProcessedCheck" runat="server" Checked='<%# Eval( "Processed" ) %>'
                            CssClass="fl CheckBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcStatus" runat="server" meta:resourcekey="lcStatus" CssClass="Label" />
                        <asp:DropDownList ID="uxStatusDrop" runat="server" OnPreRender="uxStatusDrop_PreRender"
                            CssClass="fl DropDown">
                        </asp:DropDownList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCancel" runat="server" meta:resourcekey="lcCancel" CssClass="Label" />
                        <asp:CheckBox ID="uxCancelledCheck" runat="server" Checked='<%# Eval( "Cancelled" ) %>'
                            CssClass="fl CheckBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcOrderDate" runat="server" meta:resourcekey="lcOrderDate" CssClass="Label" />
                        <uc5:CalendarPopup ID="uxOrderDateCalendarPopup" runat="server" TextBoxEnabled="false"
                            SelectedDate='<%# Convert.ToDateTime( Eval( "OrderDate" ) ) %>' />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTrackingMethod" runat="server" meta:resourcekey="lcTrackingMethod"
                            CssClass="Label" />
                        <asp:DropDownList ID="uxTrackingMethodDrop" runat="server" OnDataBound="uxTrackingMethodDrop_DataBound"
                            CssClass="fl DropDown">
                            <asp:ListItem>UPS</asp:ListItem>
                            <asp:ListItem>FedEx</asp:ListItem>
                            <asp:ListItem>USPS</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTrackingNumber" runat="server" meta:resourcekey="lcTrackingNumber"
                            CssClass="Label" />
                        <asp:TextBox ID="uxTrackingNumerText" runat="server" Text='<%# Eval( "TrackingNumber" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="OrderEditRowTitle mgt20">
                        <asp:Label ID="lcPaymentStatus" runat="server" meta:resourcekey="lcPaymentStatus" /></strong>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPaymentMethod" runat="server" meta:resourcekey="lcPaymentMethod"
                            CssClass="Label" />
                        <asp:TextBox ID="uxPaymentMethodText" runat="server" Text='<%# Eval("PaymentMethod") %>'
                            CssClass="fl TextBox" />
                        <asp:LinkButton ID="uxViewCreditCardLink" runat="server" CssClass="ViewCreditCardLink UnderlineDashed"
                            meta:resourcekey="uxViewCreditCardLink" OnLoad="uxViewCreditCardLink_Load"></asp:LinkButton>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Panel ID="uxPanelPONumberEdit" runat="server">
                            <asp:Label ID="lcPONumber" runat="server" meta:resourcekey="lcPONumber" CssClass="Label" />
                            <asp:TextBox ID="uxPONumberText" runat="server" Text='<%# Eval("PONumber") %>' CssClass="fl TextBox" />
                        </asp:Panel>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPaymentComplete" runat="server" meta:resourcekey="lcPaymentComplete"
                            CssClass="Label" />
                        <asp:CheckBox ID="uxPaymentCompleteCheck" runat="server" Checked='<%# Eval( "PaymentComplete" ) %>'
                            CssClass="fl CheckBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcAvsAddrLabel" runat="server" meta:resourcekey="lcAvsAddrLabel" CssClass="Label pdt5" />
                        <asp:DropDownList ID="uxAvsAddrDrop" runat="server" CssClass="fl DropDown" OnPreRender="uxAvsAddrDrop_PreRender">
                            <asp:ListItem Value="">None</asp:ListItem>
                            <asp:ListItem Value="Pass">Pass</asp:ListItem>
                            <asp:ListItem Value="Fail">Fail</asp:ListItem>
                            <asp:ListItem Value="Unavailable">Not Available</asp:ListItem>
                        </asp:DropDownList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcAvsZipLabel" runat="server" meta:resourcekey="lcAvsZipLabel" CssClass="Label pdt5" />
                        <asp:DropDownList ID="uxAvsZipDrop" runat="server" CssClass="fl DropDown" OnPreRender="uxAvsZipDrop_PreRender">
                            <asp:ListItem Value="">None</asp:ListItem>
                            <asp:ListItem Value="Pass">Pass</asp:ListItem>
                            <asp:ListItem Value="Fail">Fail</asp:ListItem>
                            <asp:ListItem Value="Unavailable">Not Available</asp:ListItem>
                        </asp:DropDownList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCvvLabel" runat="server" meta:resourcekey="lcCvvLabel" CssClass="Label pdt5" />
                        <asp:DropDownList ID="uxCvvDrop" runat="server" CssClass="fl DropDown" OnPreRender="uxCvvDrop_PreRender">
                            <asp:ListItem Value="">None</asp:ListItem>
                            <asp:ListItem Value="Pass">Pass</asp:ListItem>
                            <asp:ListItem Value="Fail">Fail</asp:ListItem>
                            <asp:ListItem Value="Unavailable">Not Available</asp:ListItem>
                        </asp:DropDownList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcBaseCurrencyCode" runat="server" meta:resourcekey="lcBaseCurrencyCode"
                            CssClass="Label" />
                        <asp:TextBox ID="uxBaseCodeText" runat="server" Text='<%# Eval( "BaseCurrencyCode" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserCurrencyCode" runat="server" meta:resourcekey="lcUserCurrencyCode"
                            CssClass="Label" />
                        <asp:TextBox ID="uxUserCurrencyCodeText" runat="server" Text='<%# Eval( "UserCurrencyCode" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserConversionRateText" runat="server" meta:resourcekey="lcUserConversionRate"
                            CssClass="Label" />
                        <asp:TextBox ID="uxConversionRateText" runat="server" Text='<%# Eval( "UserConversionRate" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="OrderEditRowTitle mgt10">
                        <asp:Label ID="lcAddress" runat="server" meta:resourcekey="lcAddress" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcUserName" runat="server" meta:resourcekey="lcUserName" CssClass="Label" />
                        <asp:TextBox ID="uxUserNameText" runat="server" Text='<%# Eval("UserName") %>' CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcFirstName" runat="server" meta:resourcekey="lcFirstName" CssClass="Label" />
                        <asp:TextBox ID="uxFirstNameText" runat="server" Text='<%# Eval("Billing.FirstName") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                        <asp:TextBox ID="uxLastNameText" runat="server" Text='<%# Eval("Billing.LastName") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label" />
                        <uc2:CountryList ID="uxCountryList" runat="server" CurrentSelected='<%# Eval("Billing.Country").ToString() %>'
                            OnBubbleEvent="uxState_RefreshHandler"></uc2:CountryList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label" />
                        <asp:TextBox ID="uxCompanyText" runat="server" Text='<%# Eval("Billing.Company") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label">:</asp:Label>
                        <asp:TextBox ID="uxAddress1Text" runat="server" Text='<%# Eval("Billing.Address1") %>'
                            CssClass="fl TextBox" /><div class="Clear">
                            </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            &nbsp;</div>
                        <asp:TextBox ID="uxAddress2Text" runat="server" Text='<%# Eval("Billing.Address2") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                        <asp:TextBox ID="uxCityText" runat="server" Text='<%# Eval("Billing.City") %>' CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcState" runat="server" meta:resourcekey="lcState" CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc3:StateList ID="uxStateList" runat="server" CountryCode='<%# Eval("Billing.Country").ToString() %>'
                                CurrentSelected='<%# Eval("Billing.State") %>'></uc3:StateList>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcZip" runat="server" meta:resourcekey="lcZip" CssClass="Label" />
                        <asp:TextBox ID="uxZipText" runat="server" Text='<%# Eval("Billing.Zip") %>' CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label" />
                        <asp:TextBox ID="uxPhoneText" runat="server" Text='<%# Eval("Billing.Phone") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcFax" runat="server" meta:resourcekey="lcFax" CssClass="Label" />
                        <asp:TextBox ID="uxFaxText" runat="server" Text='<%# Eval("Billing.Fax") %>' CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail" CssClass="Label" />
                        <asp:TextBox ID="uxEmailText" runat="server" Text='<%# Eval("Email") %>' CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="OrderEditRowTitle mgt10">
                        <asp:Label ID="lcShipping" runat="server" meta:resourcekey="lcShipping" />
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingFirstName" runat="server" meta:resourcekey="lcFirstName"
                            CssClass="Label" />
                        <asp:TextBox ID="uxShippingFirstNameText" runat="server" Text='<%# Eval("Shipping.FirstName") %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingLastName" runat="server" meta:resourcekey="lcLastName" CssClass="Label" />
                        <asp:TextBox ID="uxShippingLastNameText" runat="server" Text='<%# Eval( "Shipping.LastName" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCountry" runat="server" meta:resourcekey="lcCountry" CssClass="Label" />
                        <uc2:CountryList ID="CountryListShipping" runat="server" CurrentSelected='<%#  Eval( "Shipping.Country" )%>'
                            OnBubbleEvent="uxShippingState_RefreshHandler"></uc2:CountryList>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCompany" runat="server" meta:resourcekey="lcCompany" CssClass="Label" />
                        <asp:TextBox ID="uxShippingCompanyText" runat="server" Text='<%# Eval( "Shipping.Company" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingAddress" runat="server" meta:resourcekey="lcAddress" CssClass="Label">:</asp:Label>
                        <asp:TextBox ID="uxShippingAddress1Text" runat="server" Text='<%# Eval( "Shipping.Address1" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <div class="Label">
                            &nbsp;</div>
                        <asp:TextBox ID="uxShippingAddress2Text" runat="server" Text='<%# Eval( "Shipping.Address2" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCity" runat="server" meta:resourcekey="lcCity" CssClass="Label" />
                        <asp:TextBox ID="uxShippingCityText" runat="server" Text='<%# Eval( "Shipping.City" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingState" runat="server" meta:resourcekey="lcState" CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc3:StateList ID="StateListShipping" runat="server" CountryCode='<%# Eval("Shipping.Country") %>'
                                CurrentSelected='<%# Eval( "Shipping.State" )%>'></uc3:StateList>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingZip" runat="server" meta:resourcekey="lcZip" CssClass="Label" />
                        <asp:TextBox ID="uxShippingZipText" runat="server" Text='<%# Eval( "Shipping.Zip" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingPhone" runat="server" meta:resourcekey="lcPhone" CssClass="Label" />
                        <asp:TextBox ID="uxShippingPhoneText" runat="server" Text='<%# Eval( "Shipping.Phone" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingFax" runat="server" meta:resourcekey="lcFax" CssClass="Label" />
                        <asp:TextBox ID="uxShippingFaxText" runat="server" Text='<%# Eval( "Shipping.Fax" )%>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingMethod" runat="server" meta:resourcekey="lcShippingMethod"
                            CssClass="Label" />
                        <asp:TextBox ID="uxShippingMethodText" runat="server" Text='<%# Eval( "ShippingMethod" )%>'
                            CssClass="fl TextBox"></asp:TextBox>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="OrderEditRowTitle mgt10">
                        <asp:Label ID="lcMiscellaneous" runat="server" meta:resourcekey="lcMiscellaneous" />
                    </div>
                    <asp:Panel ID="uxTaxExepmtIDEditPanel" CssClass="CommonRowStyle" runat="server" Visible='<%# IsSaleTaxExemptVisible( Eval( "IsTaxExempt" ) ) %>'>
                        <asp:Label ID="uxTaxExepmtIDEditLabel" runat="server" Text="Tax Exepmt ID:" CssClass="Label" />
                        <asp:TextBox ID="uxTaxExepmtIDText" runat="server" Text='<%# Eval( "TaxExemptID" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxTaxExemptCountryEditPanel" CssClass="CommonRowStyle" runat="server"
                        Visible='<%# IsSaleTaxExemptVisible( Eval( "IsTaxExempt" ) ) %>'>
                        <asp:Label ID="uxTaxExemptCountryEditLabel" runat="server" Text="Tax Exempt Country:"
                            CssClass="Label" />
                        <uc2:CountryList ID="uxTaxExemptCountryList" runat="server" CurrentSelected='<%#  Eval( "TaxExemptCountry" ) %>'
                            OnBubbleEvent="uxTaxExemptStateList_RefreshHandler"></uc2:CountryList>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxTaxExemptStateEditPanel" CssClass="CommonRowStyle" runat="server"
                        Visible='<%# IsSaleTaxExemptVisible( Eval( "IsTaxExempt" ) ) %>'>
                        <asp:Label ID="uxTaxExemptStateEditLabel" runat="server" Text="Tax Exempt State:"
                            CssClass="Label" />
                        <div class="CountrySelect fl">
                            <uc3:StateList ID="uxTaxExemptStateList" runat="server" CountryCode='<%# Eval("TaxExemptCountry") %>'
                                CurrentSelected='<%# Eval( "TaxExemptState" )%>'></uc3:StateList>
                        </div>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxStoreNameEditPanel" CssClass="CommonRowStyle" runat="server" Visible='<%# Vevo.KeyUtilities.IsMultistoreLicense() %>'>
                        <asp:Label ID="uxStoreNameEditLabel" runat="server" Text="Store Name" CssClass="Label pdt5" />
                        <asp:Label ID="uxStoreNameEditValue" runat="server" Text='<%# GetStoreName( Eval( "StoreID" ).ToString()) %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="CommonRowStyle">
                        <asp:Label ID="uxIPAddressEditLabel" runat="server" Text="IP Address:" CssClass="Label" />
                        <asp:TextBox ID="uxIPAddressText" runat="server" Text='<%# Eval( "IPAddress" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcTax" runat="server" meta:resourcekey="lcTax" CssClass="Label" />
                        <asp:TextBox ID="uxTaxText" runat="server" Text='<%# Eval( "Tax", "{0:n2}" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcShippingCost" runat="server" meta:resourcekey="lcShippingCost" CssClass="Label" />
                        <asp:TextBox ID="uxShippingCostText" runat="server" Text='<%# Eval( "ShippingCost", "{0:n2}" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCouponDiscount" runat="server" meta:resourcekey="lcCouponDiscount"
                            CssClass="Label" />
                        <asp:TextBox ID="uxCouponDiscountText" runat="server" Text='<%# Eval( "CouponDiscount", "{0:n2}" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCouponID" runat="server" meta:resourcekey="lcCouponID" CssClass="Label" />
                        <asp:TextBox ID="uxCouponIDText" runat="server" Text='<%# Eval( "CouponID" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcGiftCertificate" runat="server" meta:resourcekey="lcGiftCertificate"
                            CssClass="Label" />
                        <asp:TextBox ID="uxGiftCertificateText" runat="server" Text='<%# Eval( "GiftCertificate", "{0:n2}" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcGiftCertificateCode" runat="server" meta:resourcekey="lcGiftCertificateCode"
                            CssClass="Label" />
                        <asp:TextBox ID="uxGiftCertificateCodeText" runat="server" Text='<%# Eval( "GiftCertificateCode" ) %>'
                            CssClass="fl TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <asp:Panel ID="uxHandlingFeeEditPanel" runat="server" Visible='<%# IsHandlingFeeVisible() %>'
                        CssClass="CommonRowStyle">
                        <asp:Label ID="lcHandlingFee" runat="server" meta:resourcekey="lcHandlingFee" CssClass="Label" />
                        <asp:TextBox ID="uxHandlingFeeText" runat="server" Text='<%# Eval( "HandlingFee", "{0:n2}" ) %>'
                            CssClass="fl TextBox">
                        </asp:TextBox>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="uxRewardPointEditPanel" CssClass="CommonRowStyle" runat="server" Visible='<%# IsRewardPointVisible() %>'>
                        <asp:Label ID="uxRewardPointEditLabel" runat="server" Text="Point Earned" CssClass="Label pdt5" />
                        <asp:Label ID="uxRewardPointValueEditLabel" runat="server" Text='<%# Eval( "PointEarned" ).ToString() %>'
                            CssClass="fl pdt5" />
                        <div class="Clear">
                        </div>
                    </asp:Panel>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcCustomerComments" runat="server" meta:resourcekey="lcCustomerComments"
                            CssClass="Label" />
                        <asp:TextBox ID="uxCommentText" TextMode="MultiLine" Rows="5" runat="server" Width="250px"
                            Text='<%# Eval( "CustomerComments" ) %>' CssClass="TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="CommonRowStyle">
                        <asp:Label ID="lcInvoiceNotesLabel" runat="server" meta:resourcekey="lcInvoiceNotes"
                            CssClass="Label" />
                        <asp:TextBox ID="uxInvoiceNotesText" TextMode="MultiLine" Rows="5" Width="250px"
                            runat="server" Text='<%# Eval( "InvoiceNotes" ) %>' CssClass="TextBox" />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                    <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCancelButton"
                        CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        CausesValidation="false" OnClickGoTo="Top" CommandName="Cancel">
                    </vevo:AdvanceButton>
                    <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                        CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                        CausesValidation="false" OnClickGoTo="Top" OnCommand="uxUpdateButton_Command"
                        CommandArgument='<%# Eval( "CustomerID" ) %>'></vevo:AdvanceButton>
                    <div class="Clear">
                    </div>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxOrderDetailsSource" runat="server" TypeName="Vevo.Domain.DataSources.OrdersDataSource"
    SelectMethod="OrdersGetOne"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxOrderItemsSource" runat="server" TypeName="Vevo.Domain.DataSources.OrdersDataSource"
    SelectMethod="OrderItemGetByOrderID"></asp:ObjectDataSource>
