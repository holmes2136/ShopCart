<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleOptionMappingList.ascx.cs"
    Inherits="Admin_MainControls_GoogleOptionMappingList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc4" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc4:LanguageControl ID="uxLanguageControl" runat="server" ShowLanguageDescription="true" />
    </LanguageControlTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <FilterTemplate>
        <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:GoogleOptionMappingMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <uc1:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGoogleOptionMappingGrid" runat="server" CssClass="Gridview1"
            SkinID="DefaultGridView" DataKeyNames="GoogleFeedTagID" AllowSorting="True" OnSorting="uxGrid_Sorting"
            OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGoogleOptionMappingGrid')">
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center" Width="35px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                        <asp:HiddenField ID="uxOptionGroupID" runat="server" Value='<%# Bind("OptionGroupID") %>' />
                        <asp:HiddenField ID="uxGoogleFeedTagID" runat="server" Value='<%# Bind("GoogleFeedTagID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GoogleFeedTagName" SortExpression="TagName" HeaderText="<%$ Resources:GoogleOptionMappingFields, GoogleFeedTagName %>">
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="OptionGroupName" SortExpression="Name" HeaderText="<%$ Resources:GoogleOptionMappingFields, OptionGroupName %>">
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:GoogleOptionMappingFields, CommandEdit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" OnClick="ChangePage_Click"
                            ToolTip="" PageName="GoogleOptionMappingEdit.ascx" PageQueryString='<%# String.Format( "GoogleFeedTagID={0}&OptionGroupID={1}", Eval( "GoogleFeedTagID" ),Eval( "OptionGroupID" ) ) %>'
                            StatusBarText='<%# String.Format( "Google Option Mapping {0} Edit", Eval("OptionGroupName") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="<%$ Resources:CommonMessages, TableEmpty %>" />
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
