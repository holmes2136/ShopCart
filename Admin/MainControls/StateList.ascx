<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StateList.ascx.cs" Inherits="AdminAdvanced_MainControls_StateList" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="bottomline">
        </div>
    </ValidationDenotesTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddLinkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonDelete CommonAdminButton"
            ShowText="true" OnClick="uxDeleteButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Are you sure to delete selected item(s)?"></asp:Label></div>
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
        <vevo:AdvanceButton ID="uxEnabledButton" runat="server" meta:resourcekey="uxEnabledButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonMarkSelected CommonAdminButton"
            ShowText="true" OnClick="uxEnabledButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
        <vevo:AdvanceButton ID="uxDisableButton" runat="server" meta:resourcekey="uxDisableButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonDisable CommonAdminButton"
            ShowText="true" OnClick="uxDisableButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
        <vevo:AdvanceButton ID="uxResetButton" runat="server" meta:resourcekey="uxResetkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonReset CommonAdminButton"
            ShowText="true" OnClick="uxResetButton_Click" OnClickGoTo="Top"></vevo:AdvanceButton>
        <asp:Button ID="uxResetDummyButton" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxResetConfirmButton" runat="server" TargetControlID="uxResetButton"
            ConfirmText="" DisplayModalPopupID="uxReSetConfirmModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxReSetConfirmModalPopup" runat="server" TargetControlID="uxResetButton"
            CancelControlID="uxResetCancelButton" OkControlID="uxResetOkButton" PopupControlID="uxResetConfirmPanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxResetConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="uxResetConfirmTitleLabel" runat="server" Text="Are you sure to reset all items"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxResetOkButton" runat="server" Text="OK" CssClassBegin="AdminButtonOK fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="AdminButtonOK" ShowText="false" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxResetCancelButton" runat="server" Text="Cancel" CssClassBegin="AdminButtonCancel fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="AdminButtonCancel" ShowText="false" OnClickGoTo="None">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="StateCode" OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand"
            OnRowUpdating="uxGrid_RowUpdating" AllowSorting="true" OnRowEditing="uxGrid_RowEditing"
            OnRowCancelingEdit="uxGrid_CancelingEdit" OnSorting="uxGrid_Sorting" ShowFooter="false"
            OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Name" SortExpression="StateName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxStateNameText" runat="server" Text='<%# Bind("StateName") %>'
                            Width="150px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxStateNameLabel" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxStateNameText" runat="server" Width="80%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Code" SortExpression="StateCode">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxStateCodeText" runat="server" Text='<%# Bind("StateCode") %>'
                            Width="90px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxStateCodeLabel" runat="server" Text='<%# Bind("StateCode") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxStateCodeText" runat="server" Width="80%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enabled" SortExpression="Enabled">
                    <EditItemTemplate>
                        <asp:CheckBox ID="uxEnabledCheck" runat="server" Checked='<%# Bind("Enabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxEnabledLabel" runat="server" Text='<%# Eval("Enabled") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="uxEnabledCheck" runat="server" Checked="true" />
                    </FooterTemplate>
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:PageFields, CommandUpdate %>" CommandArgument='<%# Eval("StateCode").ToString() %>'
                            CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:PageFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("StateCode").ToString() %>' Text="<%$ Resources:PageFields, CommandEdit %>"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
