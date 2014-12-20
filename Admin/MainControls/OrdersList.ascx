<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrdersList.ascx.cs" Inherits="AdminAdvanced_MainControls_OrdersList" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.DataAccessLib.Cart" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <SpecialFilterTemplate>
        <uc2:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" FirstLineEnable="true" />
        <div class="Clear">
        </div>
        <div class="OrderListFilter">
            <asp:Label ID="lcShowOnly" runat="server" meta:resourcekey="lcShowOnly" CssClass="Label fb" />
            <asp:DropDownList ID="uxProcessedDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxProcessedDrop_SelectedIndexChanged"
                CssClass="DropDown">
                <asp:ListItem Value="ShowAll">(All Processing Status)</asp:ListItem>
                <asp:ListItem Selected="true" Value="ShowFalse">Unprocessed</asp:ListItem>
            </asp:DropDownList><asp:Label ID="lcAnd" runat="server" meta:resourcekey="lcAnd"
                CssClass="Label" />
            <asp:DropDownList ID="uxPaymentDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="uxPaymentDrop_SelectedIndexChanged"
                CssClass="DropDown">
                <asp:ListItem Selected="true" Value="ShowAll">(All Payment Status)</asp:ListItem>
                <asp:ListItem Value="ShowTrue">Payment Complete</asp:ListItem>
                <asp:ListItem Value="ShowFalse">No Payment</asp:ListItem>
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:OrdersMessages, DeleteConfirmation %>"></asp:Label></div>
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
          <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc3:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            ShowFooter="true" OnRowCreated="SetFooter" AllowSorting="True" OnSorting="uxGrid_Sorting"
            OnDataBound="uxGrid_DataBound" OnRowDataBound="uxGrid_RowDataBound">
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
                <asp:BoundField DataField="OrderID" HeaderText="<%$ Resources:OrdersFields, OrderID %>"
                    SortExpression="OrderID">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:CustomerFields, UserName %>" SortExpression="UserName">
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
                <asp:BoundField DataField="OrderDate" HeaderText="<%$ Resources:OrdersFields, OrderDate %>"
                    SortExpression="OrderDate">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Total %>" SortExpression="Total">
                    <ItemTemplate>
                        <asp:Label ID="uxTotalLabel" runat="server" Text='<%# AdminUtilities.FormatPrice( ConvertUtilities.ToDecimal( Eval("Total") ) ) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="ac" />
                    <ItemStyle HorizontalAlign="Right" CssClass="pdr15" />
                    <FooterStyle CssClass="fb b13 c2 pdr15" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Processed %>" SortExpression="Processed">
                    <ItemTemplate>
                        <asp:Label ID="uxLabel" runat="server" Text='<%# (bool) Eval("Processed") ? "Yes" : "Unprocessed" %>'
                            ForeColor='<%# (bool) Eval("Processed") ? Color.Black : Color.Red %>'> 
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, PaymentComplete %>" SortExpression="PaymentComplete">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertUtilities.ToYesNo( Eval("PaymentComplete") ) %>'
                            ForeColor='<%# (bool) Eval("PaymentComplete") ? Color.Black : Color.Red %>' />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OrdersFields,Avs %>">
                    <ItemTemplate>
                        <asp:Image ID="uxAvsStatus" runat="server" ImageUrl="~/Images/Design/space.gif" CssClass='<%# GetAvsImage( Eval("OrderID").ToString()) %>' />
                        <asp:Label ID="uxAvsStatusNALabel" runat="server" Text="N/A" Visible='<%# IsAvsStatusNA( Eval("OrderID").ToString()) %>'
                            CssClass="CssSymbolUnavailableLabel" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Cvv %>">
                    <ItemTemplate>
                        <asp:Image ID="uxCvvStatus" runat="server" ImageUrl="~/Images/Design/space.gif" CssClass='<%# GetCvvImage( Eval("OrderID").ToString()) %>' />
                        <asp:Label ID="uxCvvStatusNALabel" runat="server" Text="N/A" Visible='<%# IsCvvStatusNA( Eval("OrderID").ToString()) %>'
                            CssClass="CssSymbolUnavailableLabel" />
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" Text="<%$ Resources:OrdersFields, CommandEditView %>"
                            PageName="OrdersEdit.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Orders {0} Edit", Eval("OrderID") )%>'
                            CssClass="UnderlineDashed" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxTrackingLinkButton" runat="server" Text="<%$ Resources:OrdersFields, Tracking %>"
                            PageName="OrderTracking.ascx" PageQueryString='<%# String.Format( "OrderID={0}", Eval("OrderID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Tracking Orders {0}", Eval("OrderID") ) %>'
                            CssClass="UnderlineDashed" /></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:OrdersMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
