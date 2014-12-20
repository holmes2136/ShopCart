<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecificationGroupList.ascx.cs"
    Inherits="Admin_MainControls_SpecificationGroupList" %>
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
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="lcAddSpecification"
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
        <asp:Panel ID="uxConfirmPanel" runat="server" CssClass="ConfirmPanel1 b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductSpecificationMessages, DeleteGroupConfirmation %>"></asp:Label></div>
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
        <uc1:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxSpecificationGroupGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="SpecificationGroupID" AllowSorting="True" OnSorting="uxGrid_Sorting"
            OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxSpecificationGroupGrid')">
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center" Width="35px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SpecificationGroupID" SortExpression="SpecificationGroupID"
                    HeaderText="<%$ Resources:SpecificationGroupFields, SpecificationGroupID %>">
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="<%$ Resources:SpecificationGroupFields, Name %>">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:BoundField DataField="DisplayName" SortExpression="DisplayName" HeaderText="<%$ Resources:SpecificationGroupFields, DisplayName %>">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationGroupFields, CommandEdit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" OnClick="ChangePage_Click"
                            ToolTip="<%$ Resources:SpecificationGroupFields, CommandEdit %>" PageName="SpecificationGroupEdit.ascx"
                            PageQueryString='<%# String.Format( "SpecificationGroupID={0}", Eval( "SpecificationGroupID" ) ) %>'
                            StatusBarText='<%# String.Format( "Specification {0} Edit", Eval("Name") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SpecificationGroupFields, CommandReview %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxItemListLinkButton" runat="server" OnClick="ChangePage_Click"
                            ToolTip="<%$ Resources:SpecificationGroupFields, CommandSpecificationItems %>"
                            PageName="SpecificationItemList.ascx" PageQueryString='<%# String.Format( "SpecificationGroupID={0}", Eval("SpecificationGroupID") ) %>'
                            StatusBarText='<%# String.Format( "Specification {0} Item List", Eval("Name") ) %>'>
                            <asp:Image ID="uxReviewImage" runat="server" SkinID="IconReviewInGrid" />
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
