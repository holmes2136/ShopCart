<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateCommissionList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliateCommissionList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcCommissionList %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <asp:Label ID="lcShowOnly" runat="server" meta:resourcekey="lcShowOnly" CssClass="AffiliateCommissionFilterLabel" />
        <asp:DropDownList ID="uxPaidDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxPaidDrop_SelectedIndexChanged"
            CssClass="AffiliateCommissionFilterDropDown">
            <asp:ListItem Value="All">(All Commission Status)</asp:ListItem>
            <asp:ListItem Value="Paid">Commission Paid</asp:ListItem>
            <asp:ListItem Selected="true" Value="UnPaid">Commission Unpaid</asp:ListItem>
        </asp:DropDownList>
        <div class="Clear">
        </div>
    </LanguageControlTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton  ID="uxPaymentListLink" runat="server" meta:resourcekey="ViewPayment"
            OnClick="ChangePage_Click" StatusBarText="View Payments" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <FilterTemplate>
        <div class="fl">
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
        <vevo:AdvanceButton ID="uxProcessedButton" runat="server" meta:resourcekey="uxProcessedButton"
            CssClass="AdminButtonMarkSelected CommonAdminButton" CssClassBegin="AdminButton"
            CssClassEnd="" ShowText="true" OnClick="uxProcessedButton_Click" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:AffiliateCommissionMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" CssClass="Gridview1"
            SkinID="DefaultGridView" ShowFooter="false" AllowSorting="True" OnSorting="uxGrid_Sorting"
            OnRowCancelingEdit="uxGrid_CancelingEdit" OnRowEditing="uxGrid_RowEditing" OnRowUpdating="uxGrid_RowUpdating"
            OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Enabled='<%# CanPayCommission( Eval("PaymentStatus"), Eval("Pending")) %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, AffiliateOrderID %>"
                    SortExpression="AffiliateOrderID">
                    <EditItemTemplate>
                        <asp:Label ID="uxAffiliateOrderIDLabel" runat="server" Text='<%# Bind("AffiliateOrderID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxAffiliateOrderIDLabel" runat="server" Text='<%# Bind("AffiliateOrderID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, OrderID %>" SortExpression="OrderID">
                    <EditItemTemplate>
                        <asp:Label ID="uxOrderIDLabel" runat="server" Text='<%# Bind("OrderID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxOrderIDLabel" runat="server" Text='<%# Bind("OrderID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px"  />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, OrderDate %>"
                    SortExpression="OrderDate">
                    <EditItemTemplate>
                        <asp:Label ID="uxOrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxOrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, ProductCost %>"
                    SortExpression="o.SubTotal-o.CouponDiscount">
                    <EditItemTemplate>
                        <asp:Label ID="uxProductCostLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Eval("ProductCost")) %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxProductCostLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Eval("ProductCost")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, Commission %>"
                    SortExpression="Commission">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxCommissionText" runat="server" Text='<%# Vevo.Domain.Currency.RoundAmount(Eval("Commission")) %>' Width="70px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxCommissionLabel" runat="server" Text='<%# StoreContext.Currency.FormatPrice(Eval("Commission")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, PaidCommission %>"
                    SortExpression="Commission">
                    <EditItemTemplate>
                        <asp:Label ID="uxPaidCommissionLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("PaymentStatus")) %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPaidCommissionLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("PaymentStatus")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateOrdersFields, Pending %>" SortExpression="Pending">
                    <ItemTemplate>
                        <asp:Label ID="uxPendingLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("Pending")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="uxPendingHidden" runat="server" Value='<%# Eval("Pending") %>' />
                        <asp:DropDownList ID="uxPendingDrop" runat="server" OnPreRender="uxPendingDrop_PreRender">
                            <asp:ListItem Value="False">No</asp:ListItem>
                            <asp:ListItem Value="True">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:AffiliateOrdersFields, UpdateCommand %>" CssClass="UnderlineDashed" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="<%$ Resources:AffiliateOrdersFields, CancelCommand %>"
                            ValidationGroup="VaildTaxClass" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:AffiliateOrdersFields, EditCommand %>" Visible='<%# SetEditLinkButtonVisible() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"  Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AffiliatePaymentMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
