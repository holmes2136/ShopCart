<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentMenuItemList.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ContentMenuItemList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="System.Drawing" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <TopContentBoxTemplate>
            <asp:Label ID="uxBreadcrumbLabel" runat="server"></asp:Label>
    </TopContentBoxTemplate>
    <LanguageControlTemplate>
        <uc7:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ButtonEventTemplate>
        <vevo:Button ID="uxSortButton" runat="server" meta:resourcekey="uxSortButton" OnClick="uxSortButton_Click"
            CssClass="CommonAdminButtonIcon AdminButtonIconView" OnClickGoTo="None" />
    </ButtonEventTemplate>
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
            AllowSorting="false" OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, Name %>">
                    <ItemTemplate>
                        <div id="uxSpaceDiv" runat="server" style='<%#  GetSpaceStyle( Eval("level") ) %>'>
                            &nbsp;
                        </div>
                        <asp:Image ID="uxContentMenuImage" runat="server" SkinID="SmallPage" Visible='<%# (Convert.ToInt32( Eval("ContentID")) != 0)? true : false %>' />
                        <asp:Image ID="uxCategoryImage" runat="server" SkinID="SmallCategory" Visible='<%# (Convert.ToInt32( Eval("ContentID")) == 0)? true : false %>' />
                        <asp:Label ID="uxNameLable" runat="server" Text='<%# GetName(Eval("Name").ToString(), Eval("ContentMenuItemID").ToString(), Eval("Level").ToString()) %>'>
                        </asp:Label>
                        <asp:HiddenField ID="uxContentMenuItemIDHidden" Value='<%# Bind("ContentMenuItemID") %>'
                            runat="server" />
                        <asp:HiddenField ID="uxContentMenuIDHidden" Value='<%# Bind("ContentMenuID") %>'
                            runat="server" />
                    </ItemTemplate>
                    <ItemStyle CssClass="pdl15" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, Enabled %>">
                    <ItemTemplate>
                        <asp:Label ID="uxStatusVisibleLabel" runat="server" Text='<%# ConvertUtilities.ToYesNo(Eval("IsEnabled").ToString()) %>'
                            ForeColor='<%# (bool) Eval("IsEnabled") ? Color.Black : Color.Red %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ContentMenuItemFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditHyperLink" runat="server" ToolTip="<%$ Resources:ContentMenuItemFields, EditCommand %>"
                            PageName="ContentMenuItemEdit.ascx" SkinID="EditLinkButton" 
                            PageQueryString='<%# PageQueryString(Eval("ContentMenuItemID").ToString()) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format("ContentMenuItemID {0} Edit",Eval("Name") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ContentMenuItemMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
