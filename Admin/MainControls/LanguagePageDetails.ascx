<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguagePageDetails.ascx.cs"
    Inherits="AdminAdvanced_MainControls_LanguagePageDetails" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationDenotesTemplate>
        <div class="topline">
        </div>
    </ValidationDenotesTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddLinkButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="Are you sure to delete language keyword(s)?"></asp:Label></div>
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
    <TopContentBoxTemplate>
        <asp:Panel ID="uxPathPanel" runat="server">
            <asp:Label ID="lcPage" runat="server" meta:resourcekey="lcPage" Font-Bold="true" />
            <asp:Label ID="uxPathLabel" runat="server" Text="" Font-Bold="true" />
        </asp:Panel>
    </TopContentBoxTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            DataSourceID="uxLanguageTextSource" DataKeyNames="PageID,CultureID,KeyName" OnRowUpdating="uxGrid_RowUpdating"
            OnDataBound="uxGrid_DataBound" OnRowCommand="uxGrid_RowCommand" AllowSorting="true"
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
                <asp:TemplateField HeaderText="<%$ Resources:LanguageTextFields, KeyName %>" SortExpression="KeyName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxKeyNameText" runat="server" Text='<%# Bind("KeyName") %>' Width="150px"
                            CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxKeyNameLabel" runat="server" Text='<%# Bind("KeyName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxKeyNameText" runat="server" Width="150px" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:LanguageTextFields, LanguageName %>"
                    SortExpression="LanguageName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="uxCultureDrop" runat="server" DataSourceID="uxCultureSource"
                            DataTextField="DisplayName" DataValueField="CultureID" OnPreRender="uxCultureDrop_PreRender">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxCultureLabel" runat="server" Text='<%# Bind("LanguageName") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="uxCultureDrop" runat="server" DataSourceID="uxCultureSource"
                            DataTextField="DisplayName" DataValueField="CultureID">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <FooterStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:LanguageTextFields, TextData %>" SortExpression="TextData">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxTextDataText" runat="server" Text='<%# Bind("TextData") %>' TextMode="MultiLine"
                            Width="300px" Height="100px" CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTextDataLabel" runat="server" Text='<%# Server.HtmlEncode( Eval("TextData").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="uxTextDataText" runat="server" TextMode="MultiLine" Width="300px"
                            Height="100px" CssClass="TextBox mgl10" />
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="uxUpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:LanguageTextFields, CommandUpdate %>" CssClass="UnderlineDashed" />
                        <asp:LinkButton ID="uxCancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:LanguageTextFields, CommandCancel %>" CssClass="UnderlineDashed" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="uxEditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:LanguageTextFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender"
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CommandName="Add" OnClickGoTo="Top"></vevo:AdvanceButton>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </BottomContentBoxTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxLanguageTextSource" runat="server" SelectMethod="GetTextByPageAll"
    UpdateMethod="Update" InsertMethod="Create" TypeName="Vevo.DataAccessLib.LanguageTextAccess"
    OnSelected="uxLanguageTextSource_Selected"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
