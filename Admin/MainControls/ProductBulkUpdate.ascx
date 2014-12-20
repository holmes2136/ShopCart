<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductBulkUpdate.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ProductBulkUpdate" %>
<%@ Register Src="../Components/CategoryFilterDrop.ascx" TagName="CategoryFilterDrop"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc7" %>
<%@ Register Src="../Components/LanguageControl.ascx" TagName="LanguageControl" TagPrefix="uc5" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="SearchFilter"
    TagPrefix="uc6" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/StoreDropDownList.ascx" TagName="StoreDropDownList"
    TagPrefix="uc10" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc4:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" ValidationGroup="BulkUpdate"
            meta:resourcekey="uxValidationSummary" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <LanguageControlTemplate>
        <uc5:LanguageControl ID="uxLanguageControl" runat="server" ShowTitle="true" />
    </LanguageControlTemplate>
    <SpecialFilterTemplate>
        <uc1:CategoryFilterDrop ID="uxCategoryFilterDrop" runat="server" IsDisplayRootCategoryDrop="true"
            RootFirstLineEnable="true" />
    </SpecialFilterTemplate>
    <FilterTemplate>
        <uc6:SearchFilter ID="uxSearchFilter" runat="server" />
    </FilterTemplate>
    <ButtonEventInnerBoxTemplate>
        <div id="uxStoreView" runat="server" class="CommonTextTitle">
            <asp:Label ID="uxStoreViewLabel" runat="server" meta:resourcekey="uxStoreViewLabel"></asp:Label>
            <uc10:StoreDropDownList ID="uxStoreList" runat="server" AutoPostBack="True" OnBubbleEvent="uxStoreList_RefreshHandler"
                FirstItemText="Default Value" FirstItemValue="0" />
        </div>
    </ButtonEventInnerBoxTemplate>
    <PageNumberTemplate>
        <uc7:PagingControl ID="uxPagingControl" runat="server" />
    </PageNumberTemplate>
    <GridTemplate>
        <asp:GridView ID="uxGridProduct" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
            OnRowDataBound="uxGridProduct_RowDataBound" OnRowUpdating="uxGridProduct_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxSkuHeadLabel" runat="server" Text="<%$ Resources:ProductFields, Sku %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15"></ItemStyle>
                    <HeaderStyle Width="80px" HorizontalAlign="Left" CssClass="pdl15"></HeaderStyle>
                    <ItemTemplate>
                        <asp:HiddenField ID="uxProductIDHidden" runat="server" Value='<%# Bind("ProductID") %>'>
                        </asp:HiddenField>
                        <asp:TextBox ID="uxSkuText" runat="server" Text='<%# Bind("Sku") %>' Width="70px"
                            CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxSkuRequiredValidator" runat="server" ControlToValidate="uxSkuText"
                            ValidationGroup="BulkUpdate" Display="Dynamic" CssClass="CommonValidatorText">
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdateSku">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required.
                        </asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxProductNameHeadLabel" runat="server" Text="<%$ Resources:ProductFields, Name %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Left" CssClass="pdl15"></HeaderStyle>
                    <ItemTemplate>
                        <asp:TextBox Width="140PX" ID="uxProductNameText" runat="server" Text='<%# Bind("Name")  %>'
                            CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxProductNameRequiredValidator" runat="server" ControlToValidate="uxProductNameText"
                            ValidationGroup="BulkUpdate" Display="Dynamic" CssClass="CommonValidatorText">
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdateName">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required.
                        </asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxShortDescriptionHeadLabel" runat="server" Text="<%$ Resources:ProductFields, ShortDescription %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15"></ItemStyle>
                    <HeaderStyle Width="40%" HorizontalAlign="Left" CssClass="pdl15"></HeaderStyle>
                    <ItemTemplate>
                        <asp:TextBox Width="230px" ID="uxShortDescriptionText" runat="server" Text='<%# Bind("ShortDescription") %>'
                            CssClass="TextBox"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxStockHeadLabel" runat="server" Text="<%$ Resources:ProductFields, Stock %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15"></ItemStyle>
                    <HeaderStyle Width="50px" HorizontalAlign="Left" CssClass="pdl15"></HeaderStyle>
                    <ItemTemplate>
                        <asp:TextBox CssClass="CssTextNumber TextBox" ID="uxStockText" MaxLength="5" Width="30px"
                            Visible='<%# ShowStock(Eval("UseInventory"),HasOptionStock( Eval( "ProductID" ).ToString() )) %>'
                            runat="server" Text='<%# Bind("SumStock") %>' ValidationGroup="BulkUpdate"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxStockRequiredValidator" runat="server" ControlToValidate="uxStockText"
                            ValidationGroup="BulkUpdate" Display="Dynamic" CssClass="CommonValidatorText">
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdateStock">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required.
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="uxStockText" Type="Integer" Operator="DataTypeCheck"
                            ID="uxStockCompareValidator" runat="server" Display="Dynamic" CssClass="CommonValidatorText"
                            ValidationGroup="BulkUpdate" Visible='<%# ShowStock(Eval("UseInventory"),HasOptionStock( Eval( "ProductID" ).ToString() )) %>'>
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdateStock">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid.
                        </asp:CompareValidator>
                        <asp:Label ID="uxNoStockLabel" runat="server" Text="-" Visible='<%# !ShowStock(Eval("UseInventory"),HasOptionStock( Eval( "ProductID" ).ToString() )) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="uxPriceHeadLabel" runat="server" Text="<%$ Resources:ProductFields, Price %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" CssClass="pdl15"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Left" Width="100px" CssClass="pdl15"></HeaderStyle>
                    <ItemTemplate>
                        <asp:TextBox CssClass="CssTextNumber TextBox" ID="uxPriceText" Width="80px" runat="server"
                            Text='<%# String.Format( "{0:f2}",GetPrice( Eval( "ProductID" ) )) %>' Enabled='<%# !IsUseDefaultValue(Eval("ProductID")) %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="uxPriceRequiredValidator" runat="server" ControlToValidate="uxPriceText"
                            ValidationGroup="BulkUpdate" Display="Dynamic" CssClass="CommonValidatorText">
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdatePrice">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Required.
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="uxPriceText" Type="Currency" Operator="DataTypeCheck"
                            ID="uxPriceCompareValidator" runat="server" Display="Dynamic" ValidationGroup="BulkUpdate"
                            CssClass="CommonValidatorText">
                            <div class="CommonValidateDiv CommonValidateDivProductBulkUpdatePrice">
                            </div>
                            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid.
                        </asp:CompareValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:ProductFields, CommandEdit %>">
                    <ItemTemplate>
                        <vevo:AdvancedLinkButton ID="uxEditLinkButton" runat="server" OnClick="ChangePage_Click"
                            PageName="ProductEdit.ascx" ToolTip="<%$ Resources:ProductFields, CommandEdit %>"
                            PageQueryString='<%# String.Format( "ProductID={0}", Eval("ProductID") ) %>'
                            StatusBarText='<%# String.Format( "Edit {0}", Eval("Name") ) %>'>
                            <asp:Image ID="uxEditImage" runat="server" SkinID="IConEditInGrid" />
                        </vevo:AdvancedLinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="pd0" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="<%$ Resources:ProductMessages, TableEmpty  %>"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </GridTemplate>
    <BottomContentBoxTemplate>
        <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
            CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
            OnClickGoTo="Top" ValidationGroup="BulkUpdate"></vevo:AdvanceButton>
        <div class="Clear">
        </div>
    </BottomContentBoxTemplate>
</uc1:AdminContent>
<asp:HiddenField ID="uxStatusHidden" runat="server" />
