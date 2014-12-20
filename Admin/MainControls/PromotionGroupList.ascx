<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionGroupList.ascx.cs"
    Inherits="Admin_MainControls_PromotionGroupList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc5" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$Resources:lcHeader%>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc5:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:PromotionGroupMessage, DeleteConfirmation %>"></asp:Label>
            </div>
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
        <vevo:AdvanceButton ID="uxPromotionSubGroupList" runat="server" meta:resourcekey="uxPromotionSubGroupList"
            CssClass="AdminButtonPromotionSubGroup CommonAdminButton" CssClassBegin="AdminButton"
            CssClassEnd="" ShowText="true" OnClick="uxPromotionSubGroupList_Click" />
        <vevo:AdvanceButton ID="uxPromotionSubGroupSorting" runat="server" Text="SubGroup Sorting"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton"
            ShowText="true" OnClick="uxPromotionSubGroupSorting_Click" OnClickGoTo="Top" />
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxPromotionGroupGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this ,'uxPromotionGroupGrid')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="PromotionGroupID" HeaderText="<%$Resources:PromotionGroupFields, PromotionGroupID  %>"
                    SortExpression="PromotionGroupID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="<%$Resources: PromotionGroupFields, Name %>"
                    SortExpression="Name">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$Resources: PromotionGroupFields, Price %>" SortExpression="Price">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxPriceLabel" Text='<%# String.Format( "{0:f2}",GetPrice( Eval( "Price" )) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:PromotionGroupFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxPromotionGroupEditLink" runat="server" ToolTip="<%$ Resources:PromotionGroupFields, EditCommand %>"
                            PageName="PromotionGroupEdit.ascx" PageQueryString='<%# String.Format( "PromotionGroupID={0}", Eval("PromotionGroupID") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("Name" ) ) %>' OnClick="ChangePage_Click">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:PromotionGroupMessage, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
