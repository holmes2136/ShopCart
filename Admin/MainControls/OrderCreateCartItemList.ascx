<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderCreateCartItemList.ascx.cs"
    Inherits="Admin_MainControls_OrderCreateCartItemList" %>
<%@ Register Src="../Components/CategoryFilterDrop.ascx" TagName="CategoryFilterDrop"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/CurrencyControl.ascx" TagName="CurrencyControl" TagPrefix="uc10" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../../Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Register Src="../Components/Order/ProductItemDetails.ascx" TagName="ProductItemDetails"
    TagPrefix="uc15" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Register Assembly="Vevo.WebUI.ServerControls" Namespace="Vevo.WebUI.ServerControls"
    TagPrefix="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc5:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
        <uc10:CurrencyControl ID="uxCurrencyControl" runat="server" />
    </LanguageControlTemplate>
    <SpecialFilterTemplate>
        <uc1:CategoryFilterDrop ID="uxCategoryFilterDrop" runat="server" IsDisplayRootCategoryDrop="false" />
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <asp:Label ID="Label2" runat="server" Text="Product List" CssClass="fl mgl5 CommonTitle2" />
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridProduct" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnDataBound="uxGridProduct_DataBound"
            OnRowDataBound="uxGridProduct_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources:ProductFields, ProductID %>"
                    SortExpression="ProductID">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="<%$ Resources:ProductFields, Name %>"
                    SortExpression="Name">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="165px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                </asp:BoundField>
                <asp:BoundField DataField="ShortDescription" HeaderText="<%$ Resources:ProductFields, ShortDescription %>"
                    SortExpression="ShortDescription">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                </asp:BoundField>
                <asp:BoundField DataField="Sku" HeaderText="<%$ Resources:ProductFields, Sku %>"
                    SortExpression="Sku">
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl10" Width="75px" />
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl10" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, Stock %>" SortExpression="SumStock">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxStockLabel" Text='<%# GetStockText( Eval( "SumStock" ).ToString(), Eval( "IsGiftCertificate" ),Eval("UseInventory") ) %>' />
                        <itemstyle horizontalalign="Right" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, Price %>" SortExpression="Price">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxPriceLabel" Text='<%# String.Format( "{0:f2}",GetPrice( Eval( "ProductID" ) )) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, RetailPrice %>" SortExpression="RetailPrice">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="uxRetailPriceLabel" Text='<%# String.Format( "{0:f2}",GetRetailPrice( Eval( "ProductID" ) )) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" CssClass="pdl5 pdr5" />
                    <HeaderStyle Width="75px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <Vevo:AdvanceButton ID="uxAddItemButton" runat="server" meta:resourcekey="uxAddItemButton"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CausesValidation="false" OnClickGoTo="Top" OnCommand="uxAddItemButton_Command"
                            CommandArgument='<%# Eval( "ProductID" ) %>'></Vevo:AdvanceButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="uxAddItemButtonLabel" runat="server" Text=""></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="uxAddItemButtonModalPopup" runat="server" TargetControlID="uxAddItemButtonLabel"
            CancelControlID="uxCancelButton" PopupControlID="uxAddItemPanel" BackgroundCssClass="ConfirmBackground b7"
            DropShadow="true" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxAddItemPanel" runat="server" CssClass="b6 pdl10 pdr10 pdb10 pdt10">
            <div style="width: 850px; margin: auto;">
                <uc15:ProductItemDetails ID="uxProductItemDetails" runat="server" SetValidGroup="OrderItemsDetails" />
                <div class="Clear">
                </div>
                <div class="CommonRowStyle">
                    <Vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgl10"
                        CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                    </Vevo:AdvanceButton>
                    <Vevo:AdvanceButton ID="uxAddItemButton" runat="server" Text="Add Order Item" CssClassBegin="fr mgl10"
                        CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClick="uxAddItemButton_Click"
                        OnClickGoTo="Top" ValidationGroup="OrderItemsDetails"></Vevo:AdvanceButton>
                    <Vevo:AdvanceButton ID="uxUpdateItemButton" runat="server" Text="Update Order Item"
                        CssClassBegin="fr mgl10" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                        OnClick="uxUpdateItemButton_Click" OnClickGoTo="Top" ValidationGroup="OrderItemsDetails">
                    </Vevo:AdvanceButton>
                </div>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <asp:Panel ID="uxShoppingCartPanel" runat="server" CssClass="mgt10">
            <div id="ButtonDeleteRemove" class="border3">
                <Vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
                    CssClass="AdminButtonDelete CommonAdminButton fl mgl10" CssClassBegin="AdminButton"
                    CssClassEnd="ButtonEvent" ShowText="true" OnClick="uxDeleteButton_Click" />
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
                        <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:OrdersMessages, DeleteConfirmation %>" /></div>
                    <div class="ConfirmButton mgt10">
                        <Vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                        </Vevo:AdvanceButton>
                        <Vevo:AdvanceButton ID="AdvanceButton1" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                            CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                        </Vevo:AdvanceButton>
                        <div class="Clear">
                        </div>
                    </div>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonTitle">
                <asp:Label ID="lcShoppingCartItems" runat="server" Text="Shopping Cart Items" CssClass="fl mgl5" />
                <asp:Panel ID="uxTaxIncludeMsgPanel" runat="server" CssClass="fr">
                    <asp:Label ID="uxTaxIncludeMsgLabel" runat="server" CssClass="UnderlineDashed" Text="Tax Included"
                        ToolTip='<%# GetTaxTooltipText() %>'></asp:Label>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <asp:GridView ID="uxCartItemGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                AutoGenerateColumns="false" OnRowDataBound="uxCartItemGrid_RowDataBound" DataKeyNames="CartItemID,ProductID,Options,GiftDetails,OptionCombinationID">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxCartItemGrid')">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="uxHidden" runat="server" Value='<%# Eval( "CartItemID" ) %>' />
                            <asp:CheckBox ID="uxCheck" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductID" HeaderText="<%$ Resources:OrdersFields, ProductID %>"
                        ReadOnly="True" SortExpression="ProductID">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Name %>" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="uxItemName" runat="server" Text='<%# GetName( Container.DataItem ) %>' />
                            <br />
                            <asp:Label ID="uxRecurringBilling" runat="server" CssClass="UnderlineDashed" Text="Recurring Billing"
                                Visible='<%# IsRecurringTooltipVisible( Container.DataItem ) %>' ToolTip='<%# GetTooltipText( Container.DataItem ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" CssClass="pdl10 pdr5" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="pdl10 al" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, Quantity %>" SortExpression="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="uxQuantityLabel" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="pdl10 pdr5" />
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, UnitPrice %>" SortExpression="UnitPrice">
                        <ItemTemplate>
                            <asp:Label ID="uxLabel" runat="server" Text='<%# GetUnitPriceText( Container.DataItem ) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="right" CssClass="pdl5 pdr10" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:OrdersFields, SubTotal %>">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# GetSubtotalText( Container.DataItem ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" CssClass="pdl5 pdr10" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <Vevo:AdvanceButton ID="uxEditItemButton" runat="server" meta:resourcekey="uxUpdateButton"
                                CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                                CausesValidation="false" OnClickGoTo="Top" OnCommand="uxEditItemButton_Command"
                                CommandArgument='<%# Eval( "CartItemID" ) %>'></Vevo:AdvanceButton>
                        </ItemTemplate>
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:OrderItemMessage, TableEmpty  %>" />
                </EmptyDataTemplate>
            </asp:GridView>
            <table cellpadding="0" cellspacing="0" class="OrderEditTotalBox">
                <tr>
                    <td>
                        <div class="SummaryRow" id="uxDiscountTR" runat="server">
                            <asp:Label ID="uxDiscountAmountLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxDiscountLabel" runat="server" Text="<%$ Resources:OrdersFields, Discount %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                        <div class="SummaryRow">
                            <asp:Label ID="uxTotalAmountLabel" runat="server" CssClass="Value" />
                            <asp:Label ID="uxTotal" runat="server" Text="<%$ Resources:OrdersFields, Total %>"
                                CssClass="Label" />
                            <div class="Clear">
                            </div>
                        </div>
                    </td>
                    <td class="SpaceCol" style="width: 91px;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="uxCartStatusHidden" runat="server" />
            <div class="Clear">
            </div>
            <div class="CommonRowStyle">
                <Vevo:AdvanceButton ID="uxNextButton" runat="server" meta:resourcekey="uxNextButton"
                    CssClassBegin="mgr10 mgt20 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxNextButton_Click" OnClickGoTo="Top" ValidationGroup="VaildShipping">
                </Vevo:AdvanceButton>
            </div>
        </asp:Panel>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
