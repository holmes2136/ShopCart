<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogCommentList.ascx.cs"
    Inherits="Admin_MainControls_BlogCommentList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:BlogCommentMessages, DeleteConfirmation %>"></asp:Label></div>
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
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridBlogComment" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="BlogCommentID" AllowSorting="True" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            OnRowEditing="uxGrid_RowEditing" OnRowCancelingEdit="uxGrid_CancelingEdit"
            OnRowUpdating="uxGrid_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGridBlog')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:BlogCommentFields, BlogCommentID %>"
                    SortExpression="BlogCommentID">
                    <ItemTemplate>
                        <asp:Label ID="uxBlogCommentID" runat="server" Text='<%# Eval("BlogCommentID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:BlogCommentFields, Comment %>" SortExpression="Comment">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxCommentText" runat="server" Text='<%# Eval("Comment") %>' CssClass="TextBox"
                            Width="400px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxCommentRequired" runat="server" ErrorMessage="Comment is required"
                            ControlToValidate="uxCommentText" ValidationGroup="VaildComment" Display="None">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxBlogCommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:BlogCommentFields, UserName %>" SortExpression="UserName">
                    <ItemTemplate>
                        <asp:Label ID="uxUsername" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:BlogCommentFields, CreatedDate %>" SortExpression="CreatedDate">
                    <ItemTemplate>
                        <asp:Label ID="uxBlogDateLabel" runat="server" Text='<%# BlogCommentDateMessage(Eval("CreatedDate").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:BlogCommentFields, IsEnabled %>" SortExpression="Enabled">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxIsEnabledCheck" runat="server" Checked='<%# Eval("Enabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxIsEnabledLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval("Enabled") ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <vevo:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:CommandUpdate %>" CssClass="UnderlineDashed" ValidationGroup="VaildComment" />
                        <vevo:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="<%$ Resources:CommandCancel %>" ValidationGroup="VaildComment"
                            CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <vevo:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:CommandEdit %>" Visible='<%# IsAdminModifiable() %>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:BlogCommentMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </BottomContentBoxTemplate>
</uc1:AdminContent>
