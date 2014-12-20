<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistryList.aspx.cs"
    Inherits="GiftRegistryList" Title="[$GiftRegistryList]" %>

<asp:Content ID="UxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistryList">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="GiftRegistryListDiv">
                        <div class="GiftRegistryListButtonDiv">
                            <asp:LinkButton ID="uxAddGiftRegistryImageButton" runat="server" Text="[$BtnAddNewGiftRegistry]"
                                OnClick="uxAddGiftRegistryImageButton_OnClick" CssClass="GiftRegistryListAddGiftImageButton BtnStyle1" />
                        </div>
                        <div class="GiftRegistryListNoteDiv">
                            <span class="GiftRegistryListHilight">Note:</span> [$Purchasedgift]
                        </div>
                        <div class="GiftRegistryListGridViewDiv">
                            <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" CssClass="CommonGridView"
                                CellPadding="4" GridLines="none" AllowSorting="false" OnRowDeleting="uxGrid_RowDeleting"
                                DataKeyNames="GiftRegistryID">
                                <Columns>
                                    <asp:TemplateField SortExpression="Del">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="uxDeleteImageButton" runat="server" Text="[$BtnDelete]" CommandName="Delete"
                                                Visible='<%# IsVisible( Eval("GiftRegistryID").ToString() ) %>' CssClass="GiftRegistryListDeleteImageButton ButtonDelete" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryListDeleteHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryListDeleteItemStyle" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="[$EventName]" DataField="EventName">
                                        <HeaderStyle CssClass="GiftRegistryListEventNameHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryListEventNameItemStyle" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="[$EventDate]">
                                        <ItemTemplate>
                                            <asp:Label ID="EventDateLable" Text='<%# GetDate( Eval("EventDate") ) %>' runat="server"
                                                CssClass="GiftRegistryListEventDateLabel" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GiftRegistryListEventDateHeaderStyle" />
                                        <ItemStyle CssClass="GiftRegistryListEventDateItemStyle" />
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="GiftRegistryID" Text="[$ProductList]"
                                        DataNavigateUrlFormatString="GiftRegistryItemList.aspx?GiftRegistryID={0}" HeaderStyle-CssClass="GiftRegistryListProductListLinkHeaderStyle"
                                        ItemStyle-CssClass="GiftRegistryListProductListLinkItemStyle" ControlStyle-CssClass="CommonHyperLink" />
                                    <asp:HyperLinkField DataNavigateUrlFields="GiftRegistryID" Text="[$EditEvent]" DataNavigateUrlFormatString="GiftRegistryEdit.aspx?GiftRegistryID={0}"
                                        HeaderStyle-CssClass="GiftRegistryListEditEventLinkHeaderStyle" ItemStyle-CssClass="GiftRegistryListEditEventLinkItemStyle"
                                        ControlStyle-CssClass="CommonHyperLink" />
                                    <asp:HyperLinkField DataNavigateUrlFields="GiftRegistryID" Text="[$SendEmail]" DataNavigateUrlFormatString="GiftRegistrySendMail.aspx?GiftRegistryID={0}"
                                        HeaderStyle-CssClass="GiftRegistryListSendEmailLinkHeaderStyle" ItemStyle-CssClass="GiftRegistryListSendEmailLinkItemStyle"
                                        ControlStyle-CssClass="CommonHyperLink" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="CommonGridViewRowStyle" />
                                <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                                <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                            </asp:GridView>
                            <asp:HiddenField ID="uxStatusHidden" runat="server" />
                        </div>
                        <div class="Clear">
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
