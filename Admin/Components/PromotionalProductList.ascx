<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionalProductList.ascx.cs"
    Inherits="Admin_Components_PromotionalProductList" %>
<%@ Register Src="../Components/CategoryFilterDrop.ascx" TagName="CategoryFilterDrop"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/CurrencyControl.ascx" TagName="CurrencyControl" TagPrefix="uc10" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminTabContent.ascx" TagName="AdminTabContent"
    TagPrefix="uc1" %>
<%@ Register Src="../../Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<%@ Register Src="../Components/PromotionProductDetails.ascx" TagName="PromotionProductDetails"
    TagPrefix="uc15" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Register Assembly="Vevo.WebUI.ServerControls" Namespace="Vevo.WebUI.ServerControls"
    TagPrefix="Vevo" %>
<uc1:AdminTabContent ID="uxAdminTabContent" runat="server" SiteMapVisible="false">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc10:CurrencyControl ID="uxCurrencyControl" runat="server" />
    </LanguageControlTemplate>
    <SpecialFilterTemplate>
        <uc1:CategoryFilterDrop ID="uxCategoryFilterDrop" runat="server" IsDisplayRootCategoryDrop="true"
            RootFirstLineEnable="true" />
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <PageNumberTemplate>
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <div class="CommonTitle">
            <asp:Label ID="Label2" runat="server" Text="Product List" CssClass="fl mgl5" />
            <div class="Clear">
            </div>
        </div>
        <asp:GridView ID="uxGridProduct" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnRowDataBound="uxGridProduct_RowDataBound"
            ShowFooter="false">
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
                        <Vevo:AdvanceButton ID="uxAddItemButton" runat="server" Text="Add"
                            CssClassBegin="mgCenter" CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter"
                            CausesValidation="false" OnClickGoTo="Top" OnCommand="uxAddItemButton_Command"
                            CommandArgument='<%# Eval( "ProductID" ) %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="65px" />
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
            <div class="mgt10">
                <div style="width: 850px; height: 500px; margin: auto; overflow: auto">
                    <uc15:PromotionProductDetails ID="uxPromotionProductDetails" runat="server" />
                    <div class="Clear">
                    </div>
                    <div class="CommonRowStyle">
                        <Vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" 
                            CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClickGoTo="None">
                        </Vevo:AdvanceButton>
                        <Vevo:AdvanceButton ID="uxAddItemButton" runat="server" Text="Add Product Item" 
                            CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                            OnClick="uxAddItemButton_Click" OnClickGoTo="Top" ValidationGroup="OrderItemsDetails">
                        </Vevo:AdvanceButton>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
        </asp:Panel>
    </GridTemplate>
</uc1:AdminTabContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
