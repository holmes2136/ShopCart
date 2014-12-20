<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentConfiguration.ascx.cs"
    Inherits="AdminAdvanced_MainControls_PaymentConfiguration" %>
<%@ Register Src="../Components/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanguageLabelPlus"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxContentTemplate" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="PaymentGroup" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <FilterTemplate>
        <div class="c5 fb">
            <asp:Label ID="lcCurrencyHeader" runat="server" meta:resourcekey="lcCurrencyHeader" />
        </div>
        <div class="c5">
            <asp:Label ID="lcCurrencyDescription" runat="server" meta:resourcekey="lcCurrencyDescription" /></div>
        <div class="mgt5">
            <uc5:CurrencySelector ID="uxCurrencySelector" runat="server" />
        </div>
    </FilterTemplate>
    <GridTemplate>
        <div class="TopContent">
            <asp:Label ID="lcPaymentListHeader" runat="server" meta:resourcekey="lcPaymentListHeader" />
        </div>
        <asp:Panel ID="uxTopGridPanel" runat="server">
            <asp:GridView ID="uxPaymentListGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                DataKeyNames="Name" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="<%$ Resources:PaymentFields, Name %>"
                        SortExpression="Name">
                        <HeaderStyle HorizontalAlign="Left" Width="160px" CssClass="pdl15" />
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="<%$ Resources:PaymentFields, IsEnabled %>">
                        <ItemTemplate>
                            <asp:Label ID="uxIsEnabledLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval( "IsEnabled" ) ) %>'
                                Font-Bold='<%# (bool) Eval( "IsEnabled" ) %>' ForeColor='<%# GetEnabledColor( Eval( "IsEnabled" )) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sort Order">
                        <ItemTemplate>
                            <asp:TextBox ID="uxSortOrderText" runat="server" Width="60px" Text='<%# Eval( "SortOrder" ) %>'
                                CssClass="TextBox" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:PaymentFields, CommandEdit %>">
                        <ItemTemplate>
                            <vevo:AdvancedLinkButton ID="uxNormalPaymentEditLink" runat="server" PageName="PaymentEdit.ascx"
                                PageQueryString='<%# String.Format( "Name={0}", Eval("Name") ) %>' ToolTip="<%$ Resources:PaymentFields, CommandEdit %>"
                                OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                                <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                            </vevo:AdvancedLinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AdminMessage, TableEmpty  %>"></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <div class="TopContent mgt20">
            <asp:Label ID="lcButtonListHeader" runat="server" meta:resourcekey="lcButtonListHeader" /></div>
        <asp:Panel ID="uxBottomGridPanel" runat="server">
            <asp:GridView ID="uxButtonListGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                DataKeyNames="Name" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="<%$ Resources:PaymentFields, Name %>"
                        SortExpression="Name">
                        <HeaderStyle HorizontalAlign="Left" Width="160px" CssClass="pdl15" />
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="<%$ Resources:PaymentFields, IsEnabled %>">
                        <ItemTemplate>
                            <asp:Label ID="uxIsEnabledLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval( "IsEnabled" ) ) %>'
                                Font-Bold='<%# (bool) Eval( "IsEnabled" ) %>' ForeColor='<%# GetEnabledColor( Eval( "IsEnabled" )) %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sort Order">
                        <ItemTemplate>
                            <asp:TextBox ID="uxSortOrderText" runat="server" Width="60px" Text='<%# Eval( "SortOrder" ) %>'
                                CssClass="TextBox" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:PaymentFields, CommandEdit %>">
                        <ItemTemplate>
                            <vevo:AdvancedLinkButton ID="uxSpecialPaymentEditLink" runat="server" PageName="PaymentEdit.ascx"
                                PageQueryString='<%# String.Format( "Name={0}", Eval("Name") ) %>' ToolTip="<%$ Resources:PaymentFields, CommandEdit %>"
                                OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                                <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" /></vevo:AdvancedLinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AdminMessage, TableEmpty  %>"></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
        <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
            OnClickGoTo="Top" ValidationGroup="PaymentGroup"></vevo:AdvanceButton>
        <asp:Button ID="uxDummyButton" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxUpdatemButtonConfirm" runat="server" TargetControlID="uxUpdateButton"
            ConfirmText="" DisplayModalPopupID="uxConfirmModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxConfirmModalPopup" runat="server" TargetControlID="uxUpdateButton"
            CancelControlID="uxCancelButton" OkControlID="uxOkButton" PopupControlID="uxConfirmPanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:PaymentMessages, UpdateCurrencyConfirmation %>"></asp:Label></div>
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
    </BottomContentBoxTemplate>
</uc1:AdminContent>
