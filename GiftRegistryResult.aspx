<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistryResult.aspx.cs"
    Inherits="GiftRegistryResult" Title="[$GiftRegistryResult]" %>

<%@ Register Src="~/Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistryResult">
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
                    <asp:Panel ID="uxNoResultPanel" runat="server" Visible="false" CssClass="GiftRegistryResultPanel">
                        <div class="CommonGridViewEmptyRowStyle GiftRegistryResultEmpty">
                            [$NoResult]
                        </div>
                        <div class="GiftRegistryResultBackLinkDiv">
                            <asp:HyperLink ID="uxBackLink" runat="server" NavigateUrl="~/GiftRegistrySearch.aspx"
                                CssClass="CommonHyperLink">[$BackToSearch]</asp:HyperLink></div>
                    </asp:Panel>
                    <div id="uxResultTable" runat="server" class="GiftRegistryResultTable">
                        <div class="CommonGridViewPageItemDiv">
                            <div class="CommonGridViewPagingDiv">
                                <uc3:PagingControl ID="uxPagingControl" runat="server" />
                            </div>
                        </div>
                        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            GridLines="None" AllowSorting="false" CssClass="CommonGridView GiftRegistryResultGridView">
                            <Columns>
                                <asp:TemplateField HeaderText="[$FirstName]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxFirstName" Text='<%# Eval("ShippingAddress.FirstName").ToString() %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GiftRegistryResultFirstNameHeaderStyle" />
                                    <ItemStyle CssClass="GiftRegistryResultFirstNameItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$LastName]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxLastName" Text='<%# Eval("ShippingAddress.LastName").ToString() %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GiftRegistryResultLastNameHeaderStyle" />
                                    <ItemStyle CssClass="GiftRegistryResultLastNameItemStyle" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="[$EventName]" DataField="EventName" />
                                <asp:TemplateField HeaderText="[$EventDate]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxEventDate" runat="server" Text='<%# FormatDate( Eval( "EventDate" ) ) %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GiftRegistryResultEventDateHeaderStyle" />
                                    <ItemStyle CssClass="GiftRegistryResultEventDateItemStyle" />
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="GiftRegistryID" Text="[$Detail]" DataNavigateUrlFormatString="GiftRegistryItem.aspx?GiftRegistryID={0}"
                                    ControlStyle-CssClass="CommonHyperLink" HeaderStyle-CssClass="GiftRegistryResultLinkHeaderStyle"
                                    ItemStyle-CssClass="GiftRegistryResultLinkItemStyle" />
                            </Columns>
                            <RowStyle CssClass="CommonGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                        </asp:GridView>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
