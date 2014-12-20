<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductList.ascx.cs" Inherits="AdminAdvanced_MainControls_ProductList" %>
<%@ Register Src="../Components/CategoryFilterDrop.ascx" TagName="CategoryFilterDrop"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Import Namespace="Vevo" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Assembly="Vevo.WebUI.ServerControls" Namespace="Vevo.WebUI.ServerControls"
    TagPrefix="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <LanguageControlTemplate>
        <uc5:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <ButtonEventInnerBoxTemplate>
        <Vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
            CssClassBegin="AdminButton" CssClassEnd="" CssClass="AdminButtonIconAdd CommonAdminButton"
            ShowText="true" OnClick="uxAddButton_Click" OnClickGoTo="Top" />
    </ButtonEventInnerBoxTemplate>
    <SpecialFilterTemplate>
        <uc1:CategoryFilterDrop ID="uxCategoryFilterDrop" runat="server" IsDisplayRootCategoryDrop="true"
            RootFirstLineEnable="true" />
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonCommandTemplate>
        <Vevo:AdvanceButton ID="uxDeleteButton" runat="server" meta:resourcekey="uxDeleteButton"
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
                <asp:Label ID="uxConfirmLabel" runat="server" Text="<%$ Resources:ProductMessages, DeleteConfirmation %>"></asp:Label></div>
            <div class="ConfirmButton mgt10">
                <Vevo:AdvanceButton ID="uxOkButton" runat="server" Text="OK" CssClassBegin="fl mgt10 mgb10 mgl10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="Top">
                </Vevo:AdvanceButton>
                <Vevo:AdvanceButton ID="uxCancelButton" runat="server" Text="Cancel" CssClassBegin="fr mgt10 mgb10 mgr10"
                    CssClassEnd="Button1Right" CssClass="ButtonOrange ButtonCenter" OnClickGoTo="None">
                </Vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
        <Vevo:AdvanceButton ID="uxSortButton" runat="server" Text="Sorting" CssClassBegin="AdminButton"
            CssClassEnd="" CssClass="AdminButtonSorting CommonAdminButton" ShowText="true"
            OnClick="uxSortButton_Click" OnClickGoTo="Top" />
        <Vevo:AdvanceButton ID="uxeBayListing" runat="server" meta:resourcekey="uxeBayListingButton"
            CssClass="AdminButtonEBay CommonAdminButton" CssClassBegin="AdminButton" CssClassEnd=""
            ShowText="true" OnClick="uxeBayListing_Click" />
    </ButtonCommandTemplate>
    <PageNumberTemplate>
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridProduct" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            AllowSorting="true" OnSorting="uxGrid_Sorting" OnDataBound="uxGridProduct_DataBound"
            OnRowDataBound="uxGridProduct_RowDataBound" ShowFooter="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAllCheckBoxes(this , 'uxGrid')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="uxCheck" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, CommandEdit %>">
                    <ItemTemplate>
                        <Vevo:AdvancedLinkButton ID="uxEditHyperLink" runat="server" ToolTip="<%$ Resources:ProductFields,  CommandEdit %>"
                            PageName="ProductEdit.ascx" PageQueryString='<%# String.Format( "ProductID={0}", Eval("ProductID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </Vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, CommandReview %>">
                    <ItemTemplate>
                        <Vevo:AdvancedLinkButton ID="uxReviewHyperLink" runat="server" ToolTip="<%$ Resources:ProductFields, CommandReview %>"
                            PageName="ProductReviewList.ascx" PageQueryString='<%# String.Format( "ProductID={0}", Eval("ProductID") ) %>'
                            OnClick="ChangePage_Click" StatusBarText='<%# String.Format( "{0} Reviews List", Eval("Name") ) %>'>
                            <asp:Image ID="uxReviewImage" runat="server" SkinID="IconReviewInGrid" />
                        </Vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
