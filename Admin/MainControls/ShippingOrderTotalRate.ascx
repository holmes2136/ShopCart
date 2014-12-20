<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingOrderTotalRate.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingOrderTotalRate" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.DataAccessLib.Cart" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildShipping" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="lcAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click"></vevo:AdvanceButton>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ShippingMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" DataKeyNames="ShippingOrderTotalRateID" OnRowEditing="uxGrid_RowEditing"
            OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand" OnRowUpdating="uxGrid_RowUpdating"
            OnRowDataBound="uxGrid_RowDataBound" OnSorting="uxGrid_Sorting" OnRowCancelingEdit="uxGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:HiddenField ID="uxShippingRateIDLabel" runat="server" Value='<%# Eval( "ShippingOrderTotalRateID" ) %>' />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Visible='<%# CheckVisibleFromToOrderTotal( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>' />
                        <asp:HiddenField ID="uxShippigRateIDLabel" runat="server" Value='<%# Eval( "ShippingOrderTotalRateID" ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ToOrderTotal" HeaderText="<%$ Resources:ShippingOrderTotalRateFields, FromOrderTotal %>">
                    <ItemTemplate>
                        <asp:Label ID="uxFromItemsLable" runat="server" Text='<%# ShowFromOrderTotal( Eval("ToOrderTotal", "{0:f2}") ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ToOrderTotal" HeaderText="<%$ Resources:ShippingOrderTotalRateFields, ToOrderTotal %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxToOrderTotalText" runat="server" Text='<%# Eval("ToOrderTotal", "{0:f2}") %>'
                            Width="100px" Visible='<%# CheckVisibleFromToOrderTotal( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>'
                            CssClass="TextBox"></asp:TextBox>
                        <asp:Label ID="uxToOrderTotalLable" runat="server" Text='<%# LastToOrderTotal ( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>'
                            Visible='<%# !CheckVisibleFromToOrderTotal( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>'></asp:Label>
                        <asp:RequiredFieldValidator ID="uxToOrderTotalRequired" runat="server" ErrorMessage="To Order Total is required"
                            ControlToValidate="uxToOrderTotalText" ValidationGroup="VaildShipping" Display="None"
                            Visible='<%# CheckVisibleFromToOrderTotal( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>'>*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToOrderTotalCompare" runat="server" ErrorMessage="To Order Total needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxToOrderTotalText" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxToOrderTotalText" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxToOrderTotalRequired" runat="server" ErrorMessage="To Order Total is required"
                            ControlToValidate="uxToOrderTotalText" ValidationGroup="VaildShipping" Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToOrderTotalCompare" runat="server" ErrorMessage="To Order Total needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxToOrderTotalText" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxToOrderTotalLable" runat="server" Text='<%# LastToOrderTotal ( Eval("ToOrderTotal", "{0:f2}").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ShippingOrderTotalRate" HeaderText="<%$ Resources:ShippingOrderTotalRateFields, ShippingRate %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxShippingOrderTotalRate" runat="server" Width="100px" Text='<%# Eval("OrderTotalRate", "{0:f2}") %>'
                            CssClass="TextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxShippingOrderTotalRateRequired" runat="server"
                            ErrorMessage="Shipping Order Total rate is required" ControlToValidate="uxShippingOrderTotalRate"
                            ValidationGroup="VaildShipping" Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxShippingOrderTotalRateCompare" runat="server" ErrorMessage="Shipping Order Total rate needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxShippingOrderTotalRate" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxAmountLabel" runat="server" Text='<%# Eval( "OrderTotalRate", "{0:n2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxShippingOrderTotalRate" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxShippingOrderTotalRateRequired" runat="server"
                            ControlToValidate="uxShippingOrderTotalRate" ErrorMessage="Shipping Order Total rate is required"
                            ValidationGroup="VaildShipping" Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxShippingOrderTotalRateCompare" runat="server" ErrorMessage="Shipping Order Total rate needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxShippingOrderTotalRate" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:ShippingOrderTotalRateFields, CommandUpdate %>" ValidationGroup="VaildShipping"
                            CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:ShippingOrderTotalRateFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:ShippingOrderTotalRateFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" Text="<%$ Resources:ShippingOrderTotalRateFields, CommandAdd %>"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" CommandName="Add" OnClickGoTo="Top" ValidationGroup="VaildShipping">
                        </vevo:AdvanceButton>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
