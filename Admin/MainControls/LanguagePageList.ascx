<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguagePageList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_LanguagePageList" EnableViewState="true" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc2" %>
<uc2:AdminContent ID="uxAdminContentFilter" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:Button ID="uxCultureLink" runat="server" meta:resourcekey="uxCultureLink" OnClick="uxCultureLinkButton_Click"
            CssClass="CommonAdminButtonIcon AdminButtonIconView" OnClickGoTo="None" />
        <vevo:Button ID="uxSearchButton" runat="server" meta:resourcekey="uxSearchLink" OnClick="uxSearchLinkButton_Click"
            CssClass="CommonAdminButtonIcon AdminButtonIconView" OnClickGoTo="None" />
    </ButtonEventTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddLinkButton"
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Are you sure to delete selected item(s)?"></asp:Label></div>
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
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataSourceID="uxPageSource" DataKeyNames="PageID" OnDataBound="uxGrid_DataBound"
            OnRowCommand="uxGrid_RowCommand" OnRowUpdating="uxGrid_RowUpdating" AllowSorting="true"
            OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" Enabled='<%# (Eval("PageID").ToString() == "0") ? false: true%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:PageFields, PageID %>" SortExpression="PageID">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxPageIDText" runat="server" Text='<%# Bind("PageID") %>' Width="80%"
                            CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPageIDLabel" runat="server" Text='<%# Bind("PageID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxPageIDText" runat="server" Width="80%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:PageFields, Path %>" SortExpression="Path">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxPathText" runat="server" Text='<%# Bind("Path") %>' Width="80%"
                            CssClass="TextBox mgl10"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxPathLabel" runat="server" Text='<%# Bind("Path") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxPathText" runat="server" Width="90%" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:PageFields, CommandUpdate %>" CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:PageFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:PageFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender"
                            Visible='<%# (Eval("PageID").ToString() == "0")? false : true %>' CssClass="UnderlineDashed"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxPageDetailsLinkButton" runat="server" PageName="LanguagePageDetails.ascx"
                            PageQueryString='<%# String.Format( "PageID={0}", Eval("PageID") ) %>' OnClick="ChangePage_Click"
                            Text="<%$ Resources:PageFields, CommandDetails %>" StatusBarText='<%# String.Format( "{0} Details", Eval("Path") ) %>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc2:AdminContent>
<asp:ObjectDataSource ID="uxPageSource" runat="server" SelectMethod="GetAll" InsertMethod="Create"
    UpdateMethod="Update" TypeName="Vevo.DataAccessLib.PageAccess" OnSelected="uxPageSource_Selected">
    <SelectParameters>
        <asp:Parameter Name="sortBy" DefaultValue="PageID" />
    </SelectParameters>
</asp:ObjectDataSource>
