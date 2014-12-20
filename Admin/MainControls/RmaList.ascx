<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RmaList.ascx.cs" Inherits="AdminAdvanced_MainControls_RmaList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <SpecialFilterTemplate>
        <uc2:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" FirstLineEnable="true" />
        <div class="Clear">
        </div>
        <div class="RmaListFilter">
            <asp:Label ID="lcShowOnly" runat="server" meta:resourcekey="lcShowOnly" CssClass="Label fb" />
            <asp:DropDownList ID="uxProcessedDrop" runat="server" AutoPostBack="true" CssClass="DropDown"
                OnSelectedIndexChanged="uxProcessedDrop_SelectedIndexChanged">
                <asp:ListItem Value="ShowAll">(All Processing Status)</asp:ListItem>
                <asp:ListItem Value="New" Selected="true">New</asp:ListItem>
                <asp:ListItem Value="Processing">Processing</asp:ListItem>
                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                <asp:ListItem Value="Returned">Returned</asp:ListItem>
            </asp:DropDownList>
            <div class="Clear">
            </div>
        </div>
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc4:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <vevo:AdvanceButton ID="uxProcessedButton" runat="server" meta:resourcekey="uxProcessedButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonMarkSelected CommonAdminButton"
            ShowText="true" OnClick="uxProcessedButton_Click" OnClickGoTo="Top" />
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:RmaMessages, DeleteConfirmation %>"></asp:Label></div>
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
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="True" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGrid_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="RmaID" HeaderText="RMA Number" SortExpression="RmaID">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Customer" SortExpression="UserName">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxCustomerLink" runat="server" Text='<%# String.Format( "{0}", Eval("UserName") ) %>'
                            PageName="CustomerEdit.ascx" PageQueryString='<%# String.Format( "CustomerID={0}", Eval("CustomerID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("UserName") ) %>'
                            CssClass="UnderlineDashed" Visible='<%# ( Eval("CustomerID").ToString() != "0" )? true:false  %>' />
                        <asp:Label ID="uxCustomerNameLabel" runat="server" Text='<%# String.Format( "{0}", Eval("UserName") ) %>'
                            CssClass="" Visible='<%# ( Eval("CustomerID").ToString() == "0" )? true:false  %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order" SortExpression="OrderID">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxOrderLink" runat="server" Text='<%# Eval("OrderID") %>'
                            PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Open {0} Details", Eval("OrderID") )%>'
                            CssClass="UnderlineDashed" Visible='<%# ( Eval("OrderID").ToString() != "0" )? true:false  %>' />
                        <asp:Label ID="uxOrderLabel" runat="server" Text='<%# Eval("OrderID") %>' CssClass=""
                            Visible='<%# ( Eval("OrderID").ToString() == "0" )? true:false  %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RequestStatus" HeaderText="Status" SortExpression="RequestStatus">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" Text="View/Edit" PageName="RmaEdit.ascx"
                            PageQueryString='<%# String.Format( "RmaID={0}", Eval("RmaID") ) %>' OnClick="ChangePage_Click"
                            StatusBarText='<%# String.Format( "RMA {0} Edit", Eval("RmaID") )%>' CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:RmaMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
