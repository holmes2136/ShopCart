<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerSubscriptionList.ascx.cs"
    Inherits="Admin_MainControls_CustomerSubscriptionList" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <uc2:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <PageNumberTemplate>
        <uc1:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <ButtonEventInnerBoxTemplate>
        <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" AccessKey="a" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:CustomerMessages, DeleteCustomerSubscriptionConfirmation %>"></asp:Label></div>
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
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false" DataKeyNames="SubscriptionLevelID">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                        <asp:CheckBox ID="uxIsExpiredCheck" runat="server" Visible="false" Checked='<%# IsExpired( Eval( "EndDate" ) ) %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, SubscriptionLevel %>">
                    <ItemTemplate>
                        <asp:Label ID="uxLevelLable" runat="server" Text=' <%# GetSubscriptionLevel( Eval( "SubscriptionLevelID" ))%>'>
                        </asp:Label>
                        <asp:HiddenField ID="uxSubscriptionLevelIDHidden" Value='<%# Bind("SubscriptionLevelID") %>'
                            runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, StartDate %>" SortExpression="StartDate">
                    <ItemTemplate>
                        <asp:Label ID="uxStartDateLabel" runat="server" Text='<%# GetDisplayDate( Eval( "StartDate" ) ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, ExpireDate %>" SortExpression="EndDate">
                    <ItemTemplate>
                        <asp:Label ID="uxExpireDateLabel" runat="server" Text='<%#GetDisplayDate( Eval( "EndDate" ) )%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="IsActive" HeaderText="<%$ Resources:CustomerFields, IsActive %>"
                    SortExpression="IsActive">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, EditCommand %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" OnClick="ChangePage_Click"
                            PageName="CustomerSubscriptionEdit.ascx" PageQueryString='<%# String.Format("CustomerID={0}&SubscriptionLevelID={1}", Eval("CustomerID"),Eval("SubscriptionLevelID")  ) %>'
                            ToolTip="<%$ Resources:CustomerFields, EditCommand %>">
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:CustomerMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
