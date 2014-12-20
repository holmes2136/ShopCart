<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecificationItemList.ascx.cs"
    Inherits="Admin_MainControls_SpecificationItemList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/LanguageLabelPlus.ascx" TagName="LanaguageLabelPlus"
    TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildSpecificationItem" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
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
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel1 b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductSpecificationMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="SpecificationItemID" OnRowDataBound="uxGrid_RowDataBound" OnRowCommand="uxGrid_RowCommand"
            OnRowUpdating="uxGrid_RowUpdating" AllowSorting="true" OnSorting="uxGrid_Sorting"
            ShowFooter="false" OnRowEditing="uxGrid_RowEditing" OnDataBound="uxGrid_DataBound"
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
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, SpecificationItemID  %>"
                    SortExpression="SpecificationItemID">
                    <EditItemTemplate>
                        <asp:Label ID="uxSpecificationItemIDLabel" runat="server" Text='<%# Bind("SpecificationItemID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxSpecificationItemIDLabel" runat="server" Text='<%# Bind("SpecificationItemID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, Name %>" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("Name") %>' CssClass="fl mgl5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="Name is required"
                            ControlToValidate="uxNameText" ValidationGroup="VaildSpecificationItem" Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" Width="180px" />
                    <FooterTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("Name") %>' CssClass="fl mgl5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="Name is required"
                            ControlToValidate="uxNameText" ValidationGroup="VaildSpecificationItem" Display="None">*</asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, DisplayName %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxDisplayNameText" runat="server" Text='<%# Bind("DisplayName") %>'
                            Width="150px" CssClass="fl mgl5"></asp:TextBox>
                        <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
                        <asp:RequiredFieldValidator ID="uxDisplayNameRequired" runat="server" ErrorMessage="Display name is required"
                            ControlToValidate="uxDisplayNameText" ValidationGroup="VaildSpecificationItem"
                            Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxDisplayNameText" runat="server" Text='<%# Bind("DisplayName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxDisplayNameText" runat="server" Text='<%# Bind("DisplayName") %>'
                            Width="150px" CssClass="fl mgl5"></asp:TextBox>
                        <uc5:LanaguageLabelPlus ID="uxPlus4" runat="server" />
                        <asp:RequiredFieldValidator ID="uxDisplayNameRequired" runat="server" ErrorMessage="Display name is required"
                            ControlToValidate="uxDisplayNameText" ValidationGroup="VaildSpecificationItem"
                            Display="None">*</asp:RequiredFieldValidator>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, Description %>">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxDescriptionText" runat="server" Text='<%# Bind("Description") %>'
                            Height="40px" TextMode="MultiLine" Width="252px" CssClass="fl mgl5"></asp:TextBox>
                        <uc5:LanaguageLabelPlus ID="uxPlus3" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxDescriptionText" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxDescriptionText" runat="server" Text='<%# Bind("Description") %>'
                            Height="40px" TextMode="MultiLine" Width="252px" CssClass="fl mgl5"></asp:TextBox>
                        <uc5:LanaguageLabelPlus ID="uxPlus5" runat="server" />
                    </FooterTemplate>
                    <HeaderStyle Width="270px" HorizontalAlign="Center" />
                    <ItemStyle Width="270px" HorizontalAlign="Center" />
                    <FooterStyle Width="270px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, Type %>">
                    <EditItemTemplate>
                        <asp:Label ID="uxTypeText" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTypeText" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="uxTypeDrop" runat="server">
                            <asp:ListItem Text="Textbox" Value="Textbox"></asp:ListItem>
                            <asp:ListItem Text="DropDownList" Value="DropDownList"></asp:ListItem>
                            <asp:ListItem Text="MultiSelect" Value="MultiSelect"></asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                    <HeaderStyle Width="270px" HorizontalAlign="Center" />
                    <ItemStyle Width="270px" HorizontalAlign="Center" />
                    <FooterStyle Width="270px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationItemFields, UseInFaceted %>">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxUseInFacetedSearchCheck" runat="server" Checked='<%# Bind("UseInFacetedSearch") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxUseInFacetSearchText" runat="server" Text='<%# Bind("UseInFacetedSearch") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="uxUseInFacetedSearchCheck" runat="server" Checked='<%# Bind("UseInFacetedSearch") %>' />
                    </FooterTemplate>
                    <HeaderStyle Width="270px" HorizontalAlign="Center" />
                    <ItemStyle Width="270px" HorizontalAlign="Center" />
                    <FooterStyle Width="270px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:CommandUpdate %>" CssClass="UnderlineDashed" ValidationGroup="VaildSpecificationItem" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="<%$ Resources:CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:CommandEdit %>" Visible='<%# IsAdminModifiable() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddLinkButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top" ValidationGroup="VaildSpecificationItem" />
                    </FooterTemplate>
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationGroupFields, CommandReview %>">
                    <ItemTemplate>
                        <asp:Panel ID="uxViewItemPanel" runat="server" Visible='<%# !IsTypeTextbox( Eval( "SpecificationItemID" )) %>'>
                            <vevo:AdvancedLinkButton ID="uxItemListLinkButton" runat="server" OnClick="ChangePage_Click"
                                ToolTip="<%$ Resources:SpecificationGroupFields, CommandSpecificationItems %>"
                                PageName="SpecificationItemValueList.ascx" PageQueryString='<%# PageQueryString( Eval("SpecificationItemID").ToString() ) %>'
                                StatusBarText='<%# String.Format( "Specification {0} Item List", Eval("Name") ) %>'>
                            <asp:Image ID="uxReviewImage" runat="server" SkinID="IconReviewInGrid" />
                            </vevo:AdvancedLinkButton>
                        </asp:Panel>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductSpecificationMessages, EmptyItemList  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
