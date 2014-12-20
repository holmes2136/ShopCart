<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRewardPointList.ascx.cs"
    Inherits="Admin_Components_CustomerRewardPointList" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc10" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <div class="fr">
            <uc10:StoreFilterDrop ID="uxStoreList" runat="server" AutoPostBack="True" OnBubbleEvent="uxStoreList_RefreshHandler"
                FirstItemVisible="false" />
        </div>
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
        <asp:Button ID="uxDummyAddButton" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ModalPopupExtender ID="uxAddModalPopup" runat="server" TargetControlID="uxAddButton"
            PopupControlID="uxAddPanel" BackgroundCssClass="ConfirmBackground b7" DropShadow="true"
            RepositionMode="None" CancelControlID="uxCloseButton">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxAddPanel" runat="server" CssClass=" b6 pd20" Width="300px" Height="150px">
            <div>
                <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
                    ValidationGroup="ValidPoint" CssClass="ValidationStyle" />
            </div>
            <div>
                <asp:Label ID="uxPointLabel" runat="server" Text="Points:" CssClass="Label" Width="100px"></asp:Label>
                <asp:TextBox ID="uxPointText" runat="server" CssClass="TextBox"></asp:TextBox>
                <asp:CompareValidator ID="uxPointCompare" runat="server" ErrorMessage="Point must be Number."
                    Display="Dynamic" ControlToValidate="uxPointText" Operator="DataTypeCheck" Type="Integer"
                    ValidationGroup="ValidPoint" CssClass="">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="uxPointRequired" runat="server" ErrorMessage="Point cannot be empty."
                    Display="Dynamic" ControlToValidate="uxPointText" ValidationGroup="ValidPoint"
                    CssClass="">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="uxReferenceLabel" runat="server" Text="Reference:" CssClass="Label"
                    Width="100px"></asp:Label>
                <asp:TextBox ID="uxReferenceText" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxCloseButton" runat="server" CssClassBegin="fr mgt10 mgb10 mgl10" meta:resourcekey="uxCloseButton"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxAddPointButton" runat="server" CssClassBegin="fr mgt10 mgb10 mgl10" meta:resourcekey="uxAddPointButton"
                    ValidationGroup="ValidPoint" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                     OnClick="uxAddPointButton_Click" OnClickGoTo="Top" />
            </div>
        </asp:Panel>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton fl" CssClassBegin="AdminButton"
            CssClassEnd="" ShowText="true" OnClick="uxDeleteButton_Click" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:CustomerRewardPointMessages, DeleteRewardPointConfirmation %>"></asp:Label></div>
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
        <asp:GridView ID="uxCustomerGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="RewardPointID" OnDataBound="uxCustomerGrid_DataBound" AllowSorting="false"
            OnRowDataBound="uxGrid_RowDataBound" OnRowCreated="SetFooter"
            ShowFooter="true">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxCustomerGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ReferenceDate" HeaderText="<%$ Resources:CustomerRewardPointFields, Date %>"
                    SortExpression="ReferenceDate">
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerRewardPointFields, Point %>"
                    SortExpression="Point">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxPointText" runat="server" Text='<%# Eval("Point") %>' CssClass="TextBox"
                            Width="250px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPointLabel" runat="server" Text='<%# Eval("Point") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerRewardPointFields, Reference %>"
                    SortExpression="Reference">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxReferenceText" runat="server" Text='<%# Eval("Reference") %>'
                            CssClass="TextBox" Width="250px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxReferenceLabel" runat="server" Text='<%# Eval("Reference") %>' Visible='<%# IsReferenceLabelVisible(Eval("RewardPointID")) %>'></asp:Label>
                        <vevo:AdvancedLinkButton ID="uxReferenceLinkButton" runat="server" Text='<%# Eval("Reference") %>'
                            PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Orders {0} Edit", Eval("OrderID") )%>'
                            CssClass="UnderlineDashed" Visible='<%# IsReferenceLinkVisible(Eval("RewardPointID")) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:BoundField DataField="RewardPointID" Visible="false" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="Empty"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
