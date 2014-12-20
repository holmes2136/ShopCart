<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RmaActionList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_RmaActionList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidRmaAction" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <FilterTemplate>
        <uc5:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:RmaActionMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="RmaActionID" OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand"
            OnRowUpdating="uxGrid_RowUpdating" AllowSorting="true" OnSorting="uxGrid_Sorting"
            OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false" OnRowEditing="uxGrid_RowEditing"
            OnRowCancelingEdit="uxGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="RmaActionID" HeaderText="<%$ Resources:RmaActionFields, RmaActionID %>"
                    SortExpression="RmaActionID">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:RmaActionFields, Name %>" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("Name")%>' CssClass="TextBox"
                            Width="250px">
                        </asp:TextBox>
                        <asp:HiddenField ID="uxRmaActionIDHidden" Value='<%# Bind("RmaActionID") %>' runat="server" />
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="RMA action name is required"
                            ControlToValidate="uxNameText" ValidationGroup="ValidRmaAction" Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxNameLable" runat="server" Text='<%# Eval("Name") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="uxRmaActionIDHidden" Value='<%# Bind("RmaActionID") %>' runat="server" />
                    </ItemTemplate>
                    <ItemStyle CssClass="pdl15" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("Name") %>' CssClass="TextBox"
                            Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="RMA action name is required"
                            ControlToValidate="uxNameText" ValidationGroup="ValidRmaAction" Display="None">*</asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:RmaActionFields, Enabled %>" SortExpression="IsEnabled">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxEnabledCheck" runat="server" Checked='<%#  Eval("IsEnabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxIsEnabledLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval( "IsEnabled" ) ) %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="uxEnabledCheck" runat="server" Checked='true' />
                    </FooterTemplate>
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="true" CommandName="Update"
                            Text="Update" CssClass="UnderlineDashed" ValidationGroup="ValidRootDepartment" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="false"
                            CommandName="Cancel" Text="Cancel" ValidationGroup="ValidRootDepartment" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Edit" Visible='<%# IsAdminModifiable() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddLinkButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top" ValidationGroup="ValidRootDepartment" />
                    </FooterTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:RmaActionMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
