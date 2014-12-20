<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionSubGroupList.ascx.cs"
    Inherits="Admin_MainControls_PromotionSubGroupList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$Resources:lcHeader%>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:PromotionSubGroupMessage, DeleteConfirmation %>"></asp:Label>
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
        <vevo:AdvanceButton ID="uxPromotionProductSorting" runat="server" Text="Product Sorting"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton"
            ShowText="true" OnClick="uxPromotionProductSorting_Click" OnClickGoTo="Top" />
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxPromotionSubGroupGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="PromotionSubGroupID" OnDataBound="uxPromotionSubGroupGrid_DataBound"
            OnRowCommand="uxPromotionSubGroupGrid_RowCommand" OnRowUpdating="uxPromotionSubGroupGrid_RowUpdating"
            AllowSorting="True" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false" OnRowEditing="uxPromotionSubGroupGrid_RowEditing" OnRowCancelingEdit="uxPromotionSubGroupGrid_CancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this ,'uxPromotionSubGroupGrid')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%--<asp:BoundField DataField="PromotionSubGroupID" HeaderText="<%$Resources:PromotionSubGroupFields, PromotionSubGroupID  %>"
                    SortExpression="PromotionSubGroupID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="<%$ Resources:PromotionSubGroupFields, PromotionSubGroupID %>"
                    SortExpression="PromotionSubGroupID">
                    <EditItemTemplate>
                        <asp:Label ID="uxPromotionSubGroupIDLabel" runat="server" Text='<%# Eval("PromotionSubGroupID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPromotionSubGroupIDLabel" runat="server" Text='<%# Eval("PromotionSubGroupID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                <%-- <asp:BoundField DataField="Name" HeaderText="<%$Resources: PromotionSubGroupFields, Name %>"
                    SortExpression="Name">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="<%$Resources: PromotionSubGroupFields, Name %>" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" Text='<%# Eval("Name") %>' CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxNameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxNameText" runat="server" CssClass="TextBox"></asp:TextBox>
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <FooterStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:PromotionSubGroupFields, CommandUpdate %>" CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:PromotionSubGroupFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            ToolTip="<%$ Resources:PromotionSubGroupFields, CommandEdit %>" Text="<%$ Resources:PromotionSubGroupFields, CommandEdit %>"
                            OnPreRender="uxEditLinkButton_PreRender" CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" Text="<%$ Resources:PromotionSubGroupFields, CommandAdd %>"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxViewPromotionProductLinkButton" runat="server" PageName="PromotionSubGroupEdit.ascx"
                            PageQueryString='<%# String.Format( "PromotionSubGroupID={0}", Eval("PromotionSubGroupID") )%>'
                            ToolTip="<%$ Resources:PromotionSubGroupFields, EditCommand %>" Text="<%$ Resources:PromotionSubGroupFields, ViewPromotionProduct %>"
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Promotion Product of {0} list.", Eval("Name") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:PromotionSubGroupMessage, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
