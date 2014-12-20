<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistryItemList.aspx.cs"
    Inherits="GiftRegistryItemList" Title="[$GiftRegistryItemList]" %>

<%@ Register Src="Components/VevoHyperLink.ascx" TagName="HyperLink" TagPrefix="ucHyperLink" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistryItemList">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="0" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$GiftRegistryItemList]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Literal ID="uxErrorLiteral" runat="server" Visible="false">
                        <h4>
                            You are not authorized to view this page.
                        </h4>
                    </asp:Literal>
                    <asp:Panel ID="uxGiftRegistryItemListPanel" runat="server" CssClass="GiftRegistryItemListContent">
                        <div class="GiftRegistryItemListButtonDiv">
                            <asp:LinkButton ID="uxAddNewItemButton" runat="server" OnClick="uxAddNewItemButton_Click"
                                Text="[$BtnAddNewItem]" CssClass="BtnStyle1" ValidationGroup="ProductListValid" />
                        </div>
                        <div class="GiftRegistryItemListNoteDiv">
                            <span class="GiftRegistryItemListHilight">Note:</span> Purchased gift registries
                            cannot be deleted.
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ProductListValid" />
                        <div class="GiftRegistryItemListLabel">
                            [$EventName]:
                        </div>
                        <div class="GiftRegistryItemListData">
                            <asp:Label ID="uxEventNameLable" runat="server"></asp:Label>
                        </div>
                        <div class="GiftRegistryItemListLabel">
                            [$EventDate]:
                        </div>
                        <div class="GiftRegistryItemListData">
                            <asp:Label ID="uxEventDateLabel" runat="server"></asp:Label>
                        </div>
                        <div class="Clear">
                        </div>
                        <div class="GiftRegistryItemListTitle">
                            [$ProductList]
                        </div>
                        <div class="GiftRegistryItemListGridViewDiv">
                            <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="GiftregistryItemID"
                                OnRowDeleting="uxGrid_RowDeleting" CssClass="CommonGridView" GridLines="None"
                                CellPadding="4">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CssClass="ButtonDelete"
                                                CommandName="Delete" Visible='<%# IsVisble( Eval( "GiftregistryItemID" ).ToString()) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemListDeleteHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemListDeleteItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Name]">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="uxNameLink" runat="server" Text='<%# Eval("ProductName") %>' NavigateUrl='<%# GetUrl (Eval("ProductID").ToString()) %>'
                                                OnPreRender="uxNameLink_PreRender" CssClass="CommonHyperLink"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemListProductNameHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemListProductNameItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$Unit Price]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxLabel" runat="server" Text='<%# GetPrice( Eval( "GiftregistryItemID" ).ToString() ) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemListUnitPriceHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemListUnitPriceStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$WantQuantity]">
                                        <ItemTemplate>
                                            <asp:TextBox ID="uxWantQuantityText" runat="server" Text='<%# Bind( "WantQuantity" ) %>'
                                                Width="50px" CssClass="CommonTextBox" ValidationGroup="ProductListValid" />
                                            <asp:CompareValidator ID="uxQuantityCompare" runat="server" ControlToValidate="uxWantQuantityText"
                                                Operator="DataTypeCheck" Type="Integer" Display="Dynamic" CssClass="CommonValidatorText"
                                                ValidationGroup="ProductListValid">
                                                <div class="CommonValidateDiv"></div>
                                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Invalid
                                            </asp:CompareValidator>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemListWantQuantityHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemListWantQuantityItemStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="[$HasQuantity]">
                                        <ItemTemplate>
                                            <asp:Label ID="uxHasQuantityLabel" runat="server" Text='<%# Eval( "HasQuantity" ) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryItemListHasQuantityHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryItemListHasQuantityItemStyle" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="CommonGridViewRowStyle" />
                                <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                                <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                            </asp:GridView>
                        </div>
                        <div class="GiftRegistryItemListButtonBottomDiv">
                            <asp:LinkButton ID="uxBackLink" runat="server" PostBackUrl="~/GiftRegistryList.aspx"
                                Text="[$BtnBackToList]" CssClass="GiftRegistryItemListBackLink BtnStyle2" />
                            <asp:LinkButton ID="uxUpdateQuantityButton" runat="server" OnClick="uxUpdateQuantityButton_Click"
                                Text="[$BtnUpdateQuantity]" CssClass="GiftRegistryItemListUpdateQuantityImageButton BtnStyle2"
                                ValidationGroup="ProductListValid" />
                        </div>
                        <div class="Clear">
                        </div>
                    </asp:Panel>
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
