<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AffiliateList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_AffiliateList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:AdvancedLinkButton ID="uxCommissionPendingListLink" runat="server" meta:resourcekey="uxCommissionPendingListLink"
            OnClick="ChangePage_Click" StatusBarText="View Commission Pending" CssClass="CommonAdminButtonIcon AdminButtonIconView fl" />
    </ButtonEventTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <asp:Label ID="lcDisabledInRed" runat="server" meta:resourcekey="lcDisabledInRed"
                ForeColor="Red" /></div>
    </ValidationDenotesTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:AffiliateMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" AllowSorting="true"
            OnSorting="uxGrid_Sorting" CssClass="Gridview1" SkinID="DefaultGridView">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                        <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Visible="false" Checked='<%# ConvertUtilities.ToBoolean( Eval( "IsEnabled" ) ) %>' />
                        <asp:HiddenField ID="uxAffiliateCodeHidden" runat="server" Value='<%# Eval("AffiliateCode") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Username" HeaderText="<%$ Resources:AffiliateFields, Username %>"
                    SortExpression="Username">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, FirstName %>" SortExpression="FirstName">
                    <ItemTemplate>
                        <asp:Label ID="uxFirstName" runat="server" Text='<%# Eval("ContactAddress.FirstName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, LastName %>" SortExpression="LastName">
                    <ItemTemplate>
                        <asp:Label ID="uxLastName" runat="server" Text='<%# Eval("ContactAddress.LastName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="CommissionRate" HeaderText="<%$ Resources:AffiliateFields, CommissionRate %>"
                    SortExpression="CommissionRate">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, IsEnabled %>" SortExpression="IsEnabled">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval("IsEnabled") ) %>'
                            ForeColor='<%# (bool) Eval("IsEnabled") ? Color.Black : Color.Red %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, Commissions %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCommissionLink" runat="server" Text="<%$ Resources:AffiliateFields, CommandEditView %>"
                            PageName="AffiliateCommissionList.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}", Eval("AffiliateCode") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0} Edit", Eval("AffiliateCode") )%>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, Payments %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxPaymentsLink" runat="server" Text="<%$ Resources:AffiliateFields, CommandEditView %>"
                            PageName="AffiliatePaymentList.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}", Eval("AffiliateCode") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("AffiliateCode" ) ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AffiliateFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxAffiliateEditLink" runat="server" ToolTip="<%$ Resources:AffiliateFields, EditCommand %>"
                            PageName="AffiliateEdit.ascx" PageQueryString='<%# String.Format( "AffiliateCode={0}", Eval("AffiliateCode") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("UserName" ) ) %>' OnClick="ChangePage_Click">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:AffiliateMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
