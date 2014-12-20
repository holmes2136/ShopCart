<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuantityDiscountRuleList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_QuantityDiscountRuleList" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
        </div>
    </ValidationDenotesTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="CheckInput"
            meta:resourcekey="uxValidationSummary" CssClass="ValidationStyle" />
        <asp:ValidationSummary ID="uxUpdateValidationSummary" runat="server" ValidationGroup="CheckUpdate"
            CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="lcAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <TopContentBoxTemplate>
        <div class="fb">
            Group Name:
            <asp:Label ID="uxGroupNameLabel" runat="server"></asp:Label></div>
    </TopContentBoxTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Are you sure to delete selected item(s)?"></asp:Label></div>
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
    <GridTemplate>
        <asp:GridView ID="uxDiscountGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="false" DataKeyNames="DiscountRuleID" OnDataBound="uxDiscountGrid_DataBound"
            OnRowCommand="uxDiscountGrid_RowCommand" OnRowUpdating="uxDiscountGrid_RowUpdating"
            OnRowDataBound="uxGrid_RowDataBound" OnRowEditing="uxDiscountGrid_RowEditing"
            OnRowCancelingEdit="uxDiscountGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:HiddenField ID="uxDiscountRuleIDLabel" runat="server" Value='<%# Eval( "DiscountRuleID" ) %>' />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxDiscountGrid')">
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Visible='<%# CheckVisibleFromToItems( Eval("ToItems").ToString() ) %>' />
                        <asp:HiddenField ID="uxDiscountRuleIDLabel" runat="server" Value='<%# Eval( "DiscountRuleID" ) %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DiscountRuleFields, FromItems %>">
                    <ItemTemplate>
                        <asp:Label ID="uxFromItemsLable" runat="server" Text='<%# ShowFromItem( Eval("ToItems").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DiscountRuleFields, ToItems %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxToItemsText" runat="server" Text='<%# Eval("ToItems") %>' Width="100px"
                            Visible='<%# CheckVisibleFromToItems( Eval("ToItems").ToString() ) %>' CssClass="TextBox"></asp:TextBox>
                        <asp:Label ID="uxToItemsLable" runat="server" Text='<%# LastToItems ( Eval("ToItems").ToString() ) %>'
                            Visible='<%# !CheckVisibleFromToItems( Eval("ToItems").ToString() ) %>'></asp:Label>
                        <asp:RequiredFieldValidator ID="uxToItemsTextRequired" runat="server" ErrorMessage="To items is required"
                            ControlToValidate="uxToItemsText" ValidationGroup="CheckInput" Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToItemsCompare" runat="server" ErrorMessage="To items needs to be number"
                            ControlToValidate="uxToItemsText" Operator="DataTypeCheck" Type="Integer" ValidationGroup="CheckUpdate"
                            Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxToItemsText" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxToItemsTextRequired" runat="server" ErrorMessage="To items is required"
                            ControlToValidate="uxToItemsText" ValidationGroup="CheckInput" Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToItemsCompare" runat="server" ErrorMessage="To items needs to be number"
                            ControlToValidate="uxToItemsText" Operator="DataTypeCheck" Type="Integer" ValidationGroup="CheckUpdate"
                            Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxToItemsLable" runat="server" Text='<%# LastToItems ( Eval("ToItems").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DiscountRuleFields, Discount %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxAmountText" runat="server" Width="100px" Text='<%# GetEditAmount(Eval("Percentage"), Eval( "Amount")) %>'
                            CssClass="TextBox">
                        </asp:TextBox>
                        <asp:CompareValidator ID="uxAmountTextCompare" runat="server" ErrorMessage="Discount needs to be number"
                            ControlToValidate="uxAmountText" Operator="DataTypeCheck" Type="Double" ValidationGroup="CheckUpdate"
                            Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxAmountLabel" runat="server" Text='<%# GetAmount(Eval("Percentage"), Eval( "Amount")) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxAmountText" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:CompareValidator ID="uxAmountTextCompare" runat="server" ErrorMessage="Discount needs to be number"
                            ControlToValidate="uxAmountText" Operator="DataTypeCheck" Type="Double" ValidationGroup="CheckInput"
                            Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:DiscountRuleFields, CommandUpdate %>" ValidationGroup="CheckUpdate"
                            CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:DiscountRuleFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:DiscountRuleFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" Text="<%$ Resources:DiscountRuleFields, CommandAdd %>"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" CommandName="Add" OnClickGoTo="Top" ValidationGroup="CheckInput">
                        </vevo:AdvanceButton>
                    </FooterTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </BottomContentBoxTemplate>
</uc1:AdminContent>
