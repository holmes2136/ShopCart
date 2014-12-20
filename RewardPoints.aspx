<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="RewardPoints.aspx.cs"
    Inherits="RewardPoints" Title="[$Title]" %>

<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="RewardPoints">
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
                    <div class="CommonGridViewPageItemDiv">
                        <div class="CommonGridViewItemsPerPageDiv">
                            <uc3:ItemsPerPageDrop ID="uxItemsPerPageDrop" runat="server"></uc3:ItemsPerPageDrop>
                        </div>
                        <div class="CommonGridViewPagingDiv">
                            <uc2:PagingControl ID="uxPagingControl" runat="server" />
                        </div>
                    </div>
                    <div class="RewardPointsGridViewDiv">
                        <asp:GridView ID="uxRewardPointsGrid" runat="server" AutoGenerateColumns="false"
                            CellPadding="4" CssClass="CommonGridView RewardPointGridView" GridLines="None"
                            AllowSorting="false" ShowFooter="true" OnRowCreated="SetFooter">
                            <RowStyle CssClass="CommonGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle" />
                            <FooterStyle CssClass="CommonGridViewHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="uxDateLabel" runat="server" Text='<%# Eval("ReferenceDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="RewardPointGridOrderIDHeaderStyle" />
                                    <ItemStyle CssClass="RewardPointGridOrderIDItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Points">
                                    <ItemTemplate>
                                        <asp:Label ID="uxPointsLabel" runat="server" Text='<%# Eval("Point") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="RewardPointGridOrderDateHeaderStyle" />
                                    <ItemStyle CssClass="RewardPointGridPointItemStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:Label ID="uxReferenceLabel" runat="server" Text='<%# Eval("Reference") %>' Visible='<%# !IsHasLink(Eval("OrderID")) %>'></asp:Label>
                                        <asp:HyperLink ID="uxReferenceLink" runat="server" Text='<%# Eval("Reference") %>'
                                            NavigateUrl='<%# "CheckoutComplete.aspx?showorder=true&OrderID=" + Eval( "OrderID" ) %>'
                                            Visible='<%# IsHasLink(Eval("OrderID")) %>' CssClass="CommonHyperLink"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="RewardPointGridReferenceHeaderStyle" />
                                    <ItemStyle CssClass="RewardPointGridReferenceItemStyle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="Clear">
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
