<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryList.ascx.cs" Inherits="AdminAdvanced_MainControls_CountryList" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="bottomline">
        </div>
    </ValidationDenotesTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
            CssClass="AdminButtonDelete CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxDeleteButton_Click"></vevo:AdvanceButton>
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
            ShowText="true" OnClick="uxEnabledButton_Click" OnClickGoTo="Top" />
        <vevo:AdvanceButton ID="uxDisableButton" runat="server" meta:resourcekey="uxDisableButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonDisable CommonAdminButton"
            ShowText="true" OnClick="uxDisableButton_Click" OnClickGoTo="Top" />
        <vevo:AdvanceButton ID="uxResetButton" runat="server" meta:resourcekey="uxResetkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonReset CommonAdminButton"
            ShowText="true" OnClick="uxResetButton_Click" OnClickGoTo="Top" />
        <asp:Button ID="uxResetDummyButton" runat="server" Text="" CssClass="dn" />
        <ajaxToolkit:ConfirmButtonExtender ID="uxResetConfirmButton" runat="server" TargetControlID="uxResetButton"
            ConfirmText="" DisplayModalPopupID="uxReSetConfirmModalPopup">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="uxReSetConfirmModalPopup" runat="server" TargetControlID="uxResetButton"
            CancelControlID="uxResetCancelButton" OkControlID="uxResetOkButton" PopupControlID="uxReSetConfirmPanel"
            BackgroundCssClass="ConfirmBackground b7" DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxReSetConfirmPanel" runat="server" CssClass="ConfirmPanel b6 ac pdt10"
            SkinID="ConfirmPanel">
            <div class="ConfirmTitle">
                <asp:Label ID="Label1" runat="server" Text="Are you sure to reset all items?"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <vevo:AdvanceButton ID="uxResetOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxResetCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                </vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </ButtonCommandTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddLinkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataKeyNames="CountryCode" OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand"
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
                <asp:TemplateField HeaderText="Common Name" SortExpression="CommonName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxCommonNameText" runat="server" Text='<%# Bind("CommonName") %>'
                            Width="150px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxCommonNameLabel" runat="server" Text='<%# Bind("CommonName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxCommonNameText" runat="server" Width="90%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country Code" SortExpression="CountryCode">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxCountryCodeText" runat="server" Text='<%# Bind("CountryCode") %>'
                            Width="90px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxCountryCodeLabel" runat="server" Text='<%# Bind("CountryCode") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxCountryCodeText" runat="server" Width="90%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
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
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:PageFields, CommandUpdate %>" CommandArgument='<%# Eval("CountryCode").ToString() %>'
                            CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:PageFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("CountryCode").ToString() %>' Text="<%$ Resources:PageFields, CommandEdit %>"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxStateViewLinkButton" runat="server" PageName="StateList.ascx"
                            PageQueryString='<%# String.Format( "CountryCode={0}", Eval("CountryCode") ) %>'
                            Text="View States" OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "State in Country {0} List", Eval("CommonName") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lcEmptyCountryList" runat="server" meta:resourcekey="lcEmptyCountryList" />
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
