<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WholesaleList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_WholesaleList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            <asp:Label ID="lcDisabledInRed" runat="server" meta:resourcekey="lcDisabledInRed" /></div>
    </ValidationDenotesTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:CustomerMessages, DeleteConfirmation %>"></asp:Label></div>
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
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridCustomer" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGridCustomer')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                        <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Visible="false" Checked='<%# ConvertUtilities.ToBoolean( Eval( "IsEnabled" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="CustomerID" HeaderText="<%$ Resources:CustomerFields, CustomerID %>"
                    SortExpression="CustomerID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:CustomerFields, UserName %>"
                    SortExpression="UserName">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, FirstName %>" SortExpression="FirstName">
                    <ItemTemplate>
                        <asp:Label ID="uxFirstNameLabel" runat="server" Text='<%# Eval("BillingAddress.FirstName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, LastName %>" SortExpression="LastName">
                    <ItemTemplate>
                        <asp:Label ID="uxLastNameLabel" runat="server" Text='<%# Eval("BillingAddress.LastName").ToString() %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="Email" HeaderText="<%$ Resources:CustomerFields, Email %>"
                    SortExpression="Email">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:BoundField DataField="WholesaleLevel" HeaderText="<%$ Resources:CustomerFields, WholesaleLevel %>"
                    SortExpression="WholesaleLevel">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pdl10 pdr10" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="pdl10 pdr10" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxWholeSaleEditLink" runat="server" PageName="WholesaleEdit.ascx"
                            ToolTip="<%$ Resources:CustomerFields, EditCommand %>" PageQueryString='<%# String.Format( "CustomerID={0}", Eval("CustomerID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("UserName") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CustomerMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
