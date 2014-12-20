<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponList.ascx.cs" Inherits="AdminAdvanced_MainControls_CouponList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%--<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>--%>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc2" %>
<uc2:AdminContent ID="AdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <FilterTemplate>
        <uc1:SearchFilter ID="uxSearchFilter" runat="server" />
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
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound"
            ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid' )">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="center" Width="35px"/>
                </asp:TemplateField>
                <asp:BoundField DataField="CouponID" SortExpression="CouponID" HeaderText="<%$ Resources:CouponFields, CouponID %>">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CouponFields, DiscountType %>" SortExpression="DiscountType">
                    <ItemTemplate>
                        <asp:Label ID="uxDiscountTypeLabel" runat="server" Text='<%# GetDiscountTypeText( Eval( "DiscountType" ) ) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CouponFields, Amount %>" SortExpression="DiscountAmount">
                    <ItemTemplate>
                        <asp:Label ID="uxAmountLabel" runat="server" Text='<%# GetAmountText( Eval( "DiscountType" ), Eval( "DiscountAmount" ), Eval( "Percentage" ) ) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ExpirationType" SortExpression="ExpirationType" HeaderText="<%$ Resources:CouponFields, ExpirationType %>">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CouponFields, ExpiredOnDate %>" SortExpression="ExpirationDate">
                    <ItemTemplate>
                        <asp:Label ID="uxExpiredDateLabel" runat="server" Text='<%# ExpirationMessage(Eval("ExpirationType").ToString(),Eval("ExpirationDate").ToString(), Eval("ExpirationQuantity").ToString(), "Date" ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CouponFields, ExpiredOnQuantity %>"
                    SortExpression="ExpirationDate">
                    <ItemTemplate>
                        <asp:Label ID="uxExpiredQuantityLabel" runat="server" Text='<%# ExpirationMessage(Eval("ExpirationType").ToString(),Eval("ExpirationDate").ToString(), Eval("ExpirationQuantity").ToString(), "Quantity" ) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="CurrentQuantity" SortExpression="CurrentQuantity" HeaderText="<%$ Resources:CouponFields, CurrentQuantity %>">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CouponFields, Edit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" PageName="CouponEdit.ascx"
                            PageQueryString='<%# String.Format( "CouponID={0}", Eval("CouponID") ) %>' ToolTip="<%$ Resources:CouponFields, Edit %>"
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit Coupon ID {0}", Eval("CouponID") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyLabel" runat="server" Font-Bold="true" Text="<%$ Resources:CommonMessages, TableEmpty %>" />
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc2:AdminContent>
