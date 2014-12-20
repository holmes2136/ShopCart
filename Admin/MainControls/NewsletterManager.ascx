<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsletterManager.ascx.cs"
    Inherits="AdminAdvanced_MainControls_NewsletterManager" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/StoreDropDownList.ascx" TagName="StoreDropDownList"
    TagPrefix="uc10" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
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
    <PageNumberTemplate>
        <uc5:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridNewsletter" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowEditing="uxGridNewsletter_RowEditing"
            OnRowCancelingEdit="uxGridNewsletter_RowCancelingEdit" OnRowUpdating="uxGridNewsletter_RowUpdating"
            OnRowDataBound="uxGrid_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGridNewsletter')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:NewsletterList, Email %>" SortExpression="Email">
                    <ItemTemplate>
                        <asp:Label ID="uxEmailLabel" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="uxEmailText" runat="server" Text='<%# Bind("Email") %>' Width="150px"
                            CssClass="TextBox"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle CssClass="al pdl15" />
                    <ItemStyle CssClass="al pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:NewsletterList, UserName %>" SortExpression="UserName">
                    <ItemTemplate>
                        <asp:Label ID="uxEmailLabelItem" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="uxEmailLabelEdit" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemStyle CssClass="al pdl15" />
                    <HeaderStyle CssClass="al pdl15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:NewsletterList, StoreName %>" SortExpression="StoreName">
                    <ItemTemplate>
                        <asp:Label ID="uxStoreNameLabelItem" runat="server" Text='<%# GetStoreNameFromStoreID(Eval("StoreID").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <uc10:StoreDropDownList ID="uxStoreList" runat="server" CurrentSelected='<%# Eval("StoreID").ToString() %>' />
                    </EditItemTemplate>
                    <ItemStyle CssClass="al pdl15" />
                    <HeaderStyle CssClass="al pdl15" />
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
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:NewsletterManager, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Panel ID="uxAddEmailUserPanel" runat="server" Visible="false" Style="margin: 10px">
            <div class="mgr10 fl">
                <asp:Label ID="lcEmail" runat="server" meta:resourcekey="lcEmail"></asp:Label></div>
            <asp:TextBox ID="uxEmailText" runat="server" Text="" Width="150px" CssClass="fl TextBox">
            </asp:TextBox>
            <div id="uxStoreListDiv" runat="server" class="mgr10 fl">
                &nbsp;
                <asp:Label ID="lcStore" runat="server" meta:resourcekey="lcStore"></asp:Label>&nbsp;
                <uc10:StoreDropDownList ID="uxStoreList" runat="server" />
            </div>
            <vevo:AdvanceButton ID="uxAddEmailButton" runat="server" Text="Add" CssClassBegin="AdminButtonAdd fl mgl10"
                CssClassEnd="Button1Right" CssClass="AdminButtonAdd" ShowText="false" OnClick="uxAddEmailButton_Click"
                OnClickGoTo="Top"></vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </asp:Panel>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:HiddenField ID="uxStatusHidden" runat="server" />
    </BottomContentBoxTemplate>
</uc1:AdminContent>
