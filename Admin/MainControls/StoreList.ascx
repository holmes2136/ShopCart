<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreList.ascx.cs" Inherits="Admin_MainControls_StoreList" %>
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
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="VaildStore" CssClass="ValidationStyle" />
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
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:StoreMessages, DeleteConfirmation %>"></asp:Label></div>
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
            DataKeyNames="StoreID" OnRowDataBound="uxGrid_RowDataBound" OnRowCommand="uxGrid_RowCommand"
            OnRowUpdating="uxGrid_RowUpdating" AllowSorting="true" OnSorting="uxGrid_Sorting"
            ShowFooter="false" OnRowEditing="uxGrid_RowEditing" OnDataBound="uxGrid_DataBound"
            OnRowCancelingEdit="uxGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Enabled='<%# (Eval("StoreID").ToString() == "1") ? false: true%>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:StoreFields, StoreID %>" SortExpression="StoreID">
                    <EditItemTemplate>
                        <asp:Label ID="uxStoreIDLabel" runat="server" Text='<%# Eval("StoreID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxStoreIDLabel" runat="server" Text='<%# Eval("StoreID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:StoreFields, StoreName %>" SortExpression="StoreName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("StoreName") %>' CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="Store name is required"
                            ControlToValidate="uxNameText" ValidationGroup="VaildStore" Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval("StoreName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("StoreName") %>' CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxNameRequired" runat="server" ErrorMessage="Store name is required"
                            ControlToValidate="uxNameText" ValidationGroup="VaildStore" Display="None">*</asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:StoreFields, StoreUrl %>" SortExpression="UrlName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxUrlNameText" runat="server" Text='<%# Eval("UrlName") %>' CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxUrlNameRequired" runat="server" ErrorMessage="Store url is required"
                            ControlToValidate="uxUrlNameText" ValidationGroup="VaildStore" Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxUrlNameLabel" runat="server" Text='<%# Eval("UrlName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxUrlNameText" runat="server" Text='<%# Eval("UrlName") %>' CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxUrlNameRequired" runat="server" ErrorMessage="Store url is required"
                            ControlToValidate="uxUrlNameText" ValidationGroup="VaildStore" Display="None">*</asp:RequiredFieldValidator>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:StoreFields, IsEnabled %>">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked='<%#  Eval("IsEnabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxIsEnabledLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval( "IsEnabled" ) ) %>'
                            Font-Bold='<%# (bool) Eval( "IsEnabled" ) %>' ForeColor='<%# GetEnabledColor( Eval( "IsEnabled" )) %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked='true' />
                    </FooterTemplate>
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <FooterStyle Width="70px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:CommandUpdate %>" CssClass="UnderlineDashed" ValidationGroup="VaildStore" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="<%$ Resources:CommandCancel %>" ValidationGroup="VaildStore"
                            CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:CommandEdit %>" Visible='<%# IsAdminModifiable() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddLinkButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top" ValidationGroup="VaildStore" />
                    </FooterTemplate>
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Config">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxStoreEditLink" runat="server" ToolTip="<%$ Resources:StoreFields, ConfigCommand %>"
                            PageName="StoreConfig.ascx" PageQueryString='<%# String.Format( "StoreID={0}", Eval("StoreID") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("StoreName" ) ) %>' OnClick="ChangePage_Click">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Preview">
                    <ItemTemplate>
                        <asp:HyperLink ID="uxHomeHyperLink" runat="server" Target="_blank" NavigateUrl='<%# "http://" + Eval( "UrlName" ) %>'
                            ToolTip="<%$ Resources:StoreFields, PreviewCommand %>">
                            <asp:Image ID="uxReviewImage" runat="server" SkinID="IconReviewInGrid" /></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:StoreMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
