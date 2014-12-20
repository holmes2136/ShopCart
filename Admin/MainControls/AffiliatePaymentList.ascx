<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliatePaymentList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliatePaymentList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcPaymentList %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxCommissionListLink" runat="server" meta:resourcekey="ViewCommission"
            OnClick="ChangePage_Click" StatusBarText="View Payments" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <FilterTemplate>
        <div class="AffiliatePaymentBox">
            <div class="AffiliateCommissionShowName">
                <asp:Literal ID="uxNoteLiteral" runat="server" meta:resourcekey="lcNote" />
            </div>
            <div class="AffiliateCommissionShowName">
                <asp:Label ID="lcAffiliateName" runat="server" meta:resourcekey="lcAffiliateName"
                    CssClass="Label" />
                <asp:Label ID="uxAffiliateNameLabel" runat="server" CssClass="Value" />
            </div>
            <div class="AffiliateCommissionShowName">
                <asp:Label ID="lcAffiliateUserName" runat="server" meta:resourcekey="lcAffiliateUserName"
                    CssClass="Label" />
                <asp:Label ID="uxAffiliateUserNameLabel" runat="server" CssClass="Value" />
            </div>
        </div>
        <div class="fr">
            <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
        </div>
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:AffiliatePaymentMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" Width="100%"
            CssClass="Gridview1" SkinID="DefaultGridView" ShowFooter="false" AllowSorting="True"
            OnSorting="uxGrid_Sorting">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="AffiliatePaymentID" HeaderText="<%$ Resources:AffiliatePaymentFields, AffiliatePaymentID %>"
                    SortExpression="AffiliatePaymentID">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pdr15" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="pdr15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePaymentFields, PaidDate %>"
                    SortExpression="PaidDate">
                    <ItemTemplate>
                        <asp:Label ID="uxPaidDateLabel" runat="server" Text='<%# ShowDate( Eval( "PaidDate" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePaymentFields, Amount %>" SortExpression="Amount">
                    <ItemTemplate>
                        <asp:Label ID="uxAmountLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice( Eval( "Amount" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePaymentFields, OrderID %>">
                    <ItemTemplate>
                        <asp:Label ID="uxOrderIDLabel" runat="server" Text='<%# GetAllOrderID( Eval( "AffiliatePaymentID" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="PaymentNote" HeaderText="<%$ Resources:AffiliatePaymentFields, Note %>"
                    SortExpression="PaymentNote">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePaymentFields, SendMail %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxAffiliatePaymentSendMailLink" runat="server" ToolTip="<%$ Resources:AffiliatePaymentFields, SendMail %>"
                            PageName="AffiliatePaymentSendMail.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}&AffiliatePaymentID={1}", Eval("AffiliateCode"), Eval("AffiliatePaymentID") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("AffiliatePaymentID" ) ) %>'
                            OnClick="ChangePage_Click" CssClass="UnderlineDashed">
                            <asp:Label ID="uxPayments" Text="Send Mail" runat="server" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliatePaymentFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxAffiliatePaymentEditLink" runat="server" ToolTip="<%$ Resources:AffiliatePaymentFields, EditCommand %>"
                            PageName="AffiliatePaymentEdit.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}&AffiliatePaymentID={1}", Eval("AffiliateCode"), Eval("AffiliatePaymentID") ) %>'
                            StatusBarText='<%# String.Format( "Send Mail {0}", Eval("AffiliatePaymentID" ) ) %>'
                            OnClick="ChangePage_Click">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AffiliateCommissionMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
