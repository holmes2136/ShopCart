<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManufacturerList.ascx.cs"
    Inherits="Admin_MainControls_ManufacturerList" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc6" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="ValidRootCategory" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc7:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ManufacturerMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <vevo:AdvanceButton ID="uxSortButton" runat="server" Text="Sorting" CssClassBegin="AdminButton"
            CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton" ShowText="true"
            OnClick="uxSortButton_Click" OnClickGoTo="Top" />
    </ButtonCommandTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="ManufacturerID" OnDataBound="uxGrid_DataBound" AllowSorting="true"
            OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
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
                <asp:TemplateField HeaderText="<%$ Resources:ManufacturerFields, Name %>" SortExpression="Name">
                    <ItemTemplate>
                        <asp:Label ID="uxNameLable" runat="server" Text='<%# Eval("Name") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="uxManufacturerIDHidden" Value='<%# Bind("ManufacturerID") %>'
                            runat="server" />
                    </ItemTemplate>
                    <ItemStyle CssClass="pdl15" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ManufacturerFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditHyperLink" runat="server" ToolTip="<%$ Resources:ManufacturerFields,  EditCommand %>"
                            PageName="ManufacturerEdit.ascx" PageQueryString='<%# String.Format( "ManufacturerID={0}", Eval("ManufacturerID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ManufacturerMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
