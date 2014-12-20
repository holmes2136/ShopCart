<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistryItem.aspx.cs"
    Inherits="GiftRegistryItemPage" Title="[$GiftRegistryItem]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistryItem">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$GiftRegistryItem]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="GiftRegistryItemContent">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ProductListValid" />
                        <asp:Label ID="uxMessageLabel" runat="server" ForeColor="Red" CssClass="GiftRegistryItemMessageLabel" />
                        <div class="GiftRegistryItemLabel">
                            [$EventName]</div>
                        <div class="GiftRegistryItemData">
                            <asp:Label ID="uxEventNameLable" runat="server"></asp:Label></div>
                        <div class="GiftRegistryItemLabel">
                            [$EventDate]</div>
                        <div class="GiftRegistryItemData">
                            <asp:Label ID="uxEventDateLabel" runat="server"></asp:Label>
                        </div>
                        <div class="GiftRegistryItemGridViewDiv">
                            <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="GiftregistryItemID, ProductID"
                                CssClass="CommonGridView" CellPadding="4" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="[$Name]">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="uxNameLink" runat="server" Text='<%# Eval("ProductName") %>' NavigateUrl='<%# GetUrl (Eval("ProductID").ToString()) %>'
                                                OnPreRender="uxNameLink_PreRender" CssClass="CommonHyperLink"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemProductNameHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemProductNameItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Unit Price]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxLabel" runat="server" Text='<%# GetPrice( Eval( "GiftregistryItemID" ).ToString() ) %>'></asp:Label>
                                            <asp:HiddenField ID="uxOptionHidden" runat="server" Value='<%# Eval( "OptionItems" ) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemUnitPriceHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemUnitPriceItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Want]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxWantQuantityLabel" runat="server" Text='<%# Eval( "WantQuantity" ) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemWantHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemWantItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Has]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxHasQuantityLabel" runat="server" Text='<%# Eval( "HasQuantity" ) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemHasHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemHasItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Quantity]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxCannotBuyLabel" runat="server" />
                                            <asp:TextBox ID="uxQuantityText" runat="server" Width="50px" ValidationGroup="ProductListValid"
                                                CssClass="CommonTextBox" />
                                            <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxQuantityText"
                                                ErrorMessage='<%# Eval("ProductName", "{0} quantity is invalid") %>' Operator="DataTypeCheck"
                                                Type="Integer" ValidationGroup="ProductListValid">*</asp:CompareValidator>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemQuantityHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemQuantityItemStyle" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                 <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="CommonGridViewRowStyle" />
                                <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                                <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle"/>
                            </asp:GridView>
                        </div>
                        <div class="GiftRegistryItemButtonDiv">
                            <asp:LinkButton ID="uxAddToCartImageButton" runat="server" OnClick="uxAddToCartImageButton_Click"
                                ValidationGroup="ProductListValid" CssClass="GiftRegistryItemAddToCartButton BtnStyle1"
                                Text="[$BtnAddtoCart]" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
