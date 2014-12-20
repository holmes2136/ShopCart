<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentSubscription.aspx.cs"
    MasterPageFile="~/Front.master" Inherits="ContentSubscription" Title="[$Title]" %>

<%@ Register Src="Components/SearchFilterNew.ascx" TagName="SearchFilter" TagPrefix="uc4" %>
<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="Components/ItemsPerPageDrop.ascx" TagName="ItemsPerPageDrop" TagPrefix="uc3" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ContentSubscription">
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
                    <uc4:SearchFilter ID="uxSearchFilter" runat="server"></uc4:SearchFilter>
                    <div class="CommonGridViewPageItemDiv">
                        <div class="CommonGridViewItemsPerPageDiv">
                            <uc3:ItemsPerPageDrop ID="uxItemsPerPageDrop" runat="server"></uc3:ItemsPerPageDrop>
                        </div>
                        <div class="CommonGridViewPagingDiv">
                            <uc2:PagingControl ID="uxPagingControl" runat="server" />
                        </div>
                    </div>
                    <div class="ContentSubscriptionGridviewDiv">
                        <asp:GridView ID="uxGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            CssClass="CommonGridView ContentSubscriptionGridView" GridLines="None" AllowSorting="True" OnSorting="uxGrid_Sorting">
                            <RowStyle CssClass="CommonGridViewRowStyle" />
                            <HeaderStyle CssClass="CommonGridViewHeaderStyle" />
                            <AlternatingRowStyle CssClass="CommonGridViewAlternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="CommonGridViewEmptyRowStyle"/>
                            <Columns>
                                <asp:TemplateField HeaderText="[$SubscriptionLevel]">
                                    <ItemTemplate>
                                        <asp:Label ID="uxSubscriptionLevelLabel" runat="server" Text='<%# GetSubscriptionLevel( Eval( "SubscriptionLevelID" )) %>'>                             </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$StartDate]" SortExpression="StartDate">
                                    <ItemTemplate>
                                        <asp:Label ID="uxStartDateLabel" runat="server" Text='<%# GetDisplayDate( Eval( "StartDate" ) ) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$EndDate]" SortExpression="EndDate">
                                    <ItemTemplate>
                                        <asp:Label ID="uxEndDateLabel" runat="server" Text='<%# GetDisplayDate( Eval( "EndDate" ) ) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="[$IsActive]" SortExpression="IsActive">
                                    <ItemTemplate>
                                        <asp:Label ID="uxIsActiveLabel" runat="server" Text='<%# Eval( "IsActive" )  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                 <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="[$NoData]"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
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
