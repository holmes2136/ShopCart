<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguageTextSearch.ascx.cs"
    Inherits="AdminAdvanced_MainControls_LanguageTextSearch" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PageNumberTemplate>
        <uc2:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <FilterTemplate>
        <div class="LanguageSearch">
            <div class="CommonRowStyle">
                <asp:Label ID="lcSearch" runat="server" meta:resourcekey="lcSearch" CssClass="Label fb"
                    Width="80px" />
                <asp:TextBox ID="uxSearchText" runat="server" CssClass="TextBox" />
                <asp:Label ID="lcIn" runat="server" meta:resourcekey="lcIn" CssClass="fl mgr10 pdl10" />
                <asp:DropDownList ID="uxSearchFieldsDrop" runat="server" CssClass="fl DropDown">
                    <asp:ListItem Value="TextData">Text</asp:ListItem>
                    <asp:ListItem Value="KeyName">Keyword</asp:ListItem>
                </asp:DropDownList>
                <vevo:AdvanceButton ID="uxSearchButton" runat="server"  meta:resourcekey="uxSearchButton" CssClassBegin="mgl5 fl mgb10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClickGoTo="Top" OnClick="uxSearchButton_Click" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="lcLanguage" runat="server" meta:resourcekey="lcLanguage" CssClass="Label fb"
                    Width="80px" />
                <asp:DropDownList ID="uxSearchCultureIDDrop" runat="server" DataSourceID="uxCultureSource"
                    CssClass="fl DropDown" DataTextField="DisplayName" DataValueField="CultureID"
                    OnDataBound="uxSearchCultureIDDrop_PreRender">
                </asp:DropDownList>
            </div>
            <div class="Clear">
            </div>
        </div>
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
        <asp:GridView ID="uxSearchGrid" runat="server" AutoGenerateColumns="False" CssClass="Gridview1"
            DataKeyNames="PageID,CultureID,KeyName" SkinID="DefaultGridView" AllowSorting="true"
            OnSorting="uxGrid_Sorting" OnRowUpdating="uxSearchGrid_RowUpdating" OnRowEditing="uxSearchGrid_RowEditing"
            OnRowCancelingEdit="uxSearchGrid_RowCancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxSearchGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                        <asp:HiddenField ID="uxPageIDHidden" runat="server" Value='<%# Bind("PageID") %>' />
                        <asp:HiddenField ID="uxOldCultureIDHidden" runat="server" Value='<%# Bind("CultureID") %>' />
                        <asp:HiddenField ID="uxOldKeyNameHidden" runat="server" Value='<%# Bind("KeyName") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="uxPageIDHidden" runat="server" Value='<%# Bind("PageID") %>' />
                        <asp:HiddenField ID="uxOldCultureIDHidden" runat="server" Value='<%# Bind("CultureID") %>' />
                        <asp:HiddenField ID="uxOldKeyNameHidden" runat="server" Value='<%# Bind("KeyName") %>' />
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="35px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:PageFields, Path %>" SortExpression="Path">
                    <EditItemTemplate>
                        <%# GetResultPath( Eval( "Path" ) ) %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# GetResultPath( Eval( "Path" ) ) %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:LanguageTextFields, KeyName %>" SortExpression="KeyName">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxKeyNameText" runat="server" Text='<%# Bind("KeyName") %>' Width="80px"
                            CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxKeyNameLabel" runat="server" Text='<%# Bind("KeyName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="pdl10" />
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
                    <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="pdl10" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:LanguageTextFields, TextData %>" SortExpression="TextData">
                    <EditItemTemplate>
                        <asp:TextBox ID="uxTextDataText" runat="server" Text='<%# Bind("TextData") %>' TextMode="MultiLine"
                            Width="85%" Height="100px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="uxTextDataLabel" runat="server" Text='<%# Server.HtmlEncode( Eval("TextData").ToString() ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
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
                            Text="<%$ Resources:LanguageTextFields, CommandEdit %>" OnPreRender="uxEditLinkButton_PreRender" CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CommonMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:ObjectDataSource ID="uxCultureSource" runat="server" SelectMethod="GetAll" TypeName="Vevo.Domain.DataSources.CultureDataSource">
</asp:ObjectDataSource>
