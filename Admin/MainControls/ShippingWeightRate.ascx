<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingWeightRate.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingWeightRate" %>
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
            AllowSorting="True" DataKeyNames="ShippingWeightRateID" OnRowEditing="uxGrid_RowEditing"
            OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand" OnRowUpdating="uxGrid_RowUpdating"
            OnRowDataBound="uxGrid_RowDataBound" OnSorting="uxGrid_Sorting" OnRowCancelingEdit="uxGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:HiddenField ID="uxShippigRateIDLabel" runat="server" Value='<%# Eval( "ShippingWeightRateID" ) %>' />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Visible='<%# CheckVisibleFromToWeight( Eval("ToWeight").ToString() ) %>' />
                        <asp:HiddenField ID="uxShippigRateIDLabel" runat="server" Value='<%# Eval( "ShippingWeightRateID" ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ToWeight" HeaderText="<%$ Resources:ShippingWeightRateFields, FromWeight %>">
                    <ItemTemplate>
                        <asp:Label ID="uxFromItemsLable" runat="server" Text='<%# ShowFromWeight( Eval("ToWeight") ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ToWeight" HeaderText="<%$ Resources:ShippingWeightRateFields, ToWeight %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxToWeightText" runat="server" Text='<%# Eval("ToWeight") %>' Width="100px"
                            Visible='<%# CheckVisibleFromToWeight( Eval("ToWeight").ToString() ) %>' CssClass="TextBox"></asp:TextBox>
                        <asp:Label ID="uxToWeightLable" runat="server" Text='<%# LastToWeight ( Eval("ToWeight").ToString() ) %>'
                            Visible='<%# !CheckVisibleFromToWeight( Eval("ToWeight").ToString() ) %>'></asp:Label>
                        <asp:RequiredFieldValidator ID="uxToWeightRequired" runat="server" ErrorMessage="To weight is required"
                            ControlToValidate="uxToWeightText" ValidationGroup="VaildShipping" Display="None"
                            Visible='<%# CheckVisibleFromToWeight( Eval("ToWeight").ToString() ) %>'>*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToWeightCompare" runat="server" ErrorMessage="To weight needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxToWeightText" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxToWeightText" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxToWeightRequired" runat="server" ErrorMessage="To weight is required"
                            ControlToValidate="uxToWeightText" ValidationGroup="VaildShipping" Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxToWeightCompare" runat="server" ErrorMessage="To weight needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxToWeightText" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxToWeightLable" runat="server" Text='<%# LastToWeight ( Eval("ToWeight").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ShippingWeightRate" HeaderText="<%$ Resources:ShippingWeightRateFields, ShippingRate %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxShippingWeightRate" runat="server" Width="100px" Text='<%# Eval("WeightRate", "{0:f2}") %>'
                            CssClass="TextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxShippingWeightRateRequired" runat="server" ErrorMessage="Shipping weight rate is required"
                            ControlToValidate="uxShippingWeightRate" ValidationGroup="VaildShipping" Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxShippingWeightRateCompare" runat="server" ErrorMessage="Shipping weight rate needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxShippingWeightRate" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="uxAmountLabel" runat="server" Text='<%# Eval( "WeightRate", "{0:n2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxShippingWeightRate" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxShippingWeightRateRequired" runat="server" ControlToValidate="uxShippingWeightRate"
                            ErrorMessage="Shipping weight rate is required" ValidationGroup="VaildShipping"
                            Display="None">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="uxShippingWeightRateCompare" runat="server" ErrorMessage="Shipping weight rate needs to be number"
                            Operator="DataTypeCheck" ControlToValidate="uxShippingWeightRate" ValidationGroup="VaildShipping"
                            Type="Double" Display="None"></asp:CompareValidator>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:ShippingWeightRateFields, CommandUpdate %>" ValidationGroup="VaildShipping"
                            CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:ShippingWeightRateFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:ShippingWeightRateFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" Text="<%$ Resources:ShippingWeightRateFields, CommandAdd %>"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top" ValidationGroup="VaildShipping"></vevo:AdvanceButton>
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
