<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="ContentMenuItem.aspx.cs"
    Inherits="ContentMenuItem" %>

<%@ Register Src="Components/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc5" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="System.Drawing" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="News">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle"></asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Label ID="uxLabel" runat="server" Visible="false" CssClass="CommonGridViewEmptyRowStyle"></asp:Label>
                    <asp:GridView ID="uxContentListGrid" runat="server" AutoGenerateColumns="False" GridLines="None"
                        CssClass="CommonGridView NewsGridView">
                        <Columns>
                            <asp:TemplateField HeaderText="[$Titie]">
                                <ItemTemplate>
                                    <asp:Image ID="uxContentMenuImage" runat="server" ImageUrl="~/Images/Design/Bullet/SmallPage.gif"
                                        Visible='<%# (Convert.ToInt32( Eval("ContentID")) != 0)? true : false %>' />
                                    <asp:Image ID="uxCategoryImage" runat="server" ImageUrl="~/Images/Design/Bullet/SmallCategory.gif"
                                        Visible='<%# (Convert.ToInt32( Eval("ContentID")) == 0)? true : false %>' />
                                    <asp:HyperLink ID="uxTopicLink" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# GetLink( Eval("ContentMenuItemID").ToString() )%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle CssClass="ContentMenuItemColumnTopicStyle" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
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
                runat="server" CssClass="CommonPageBottomImgLeft" />
            <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                runat="server" CssClass="CommonPageBottomImgRight" />
            <div class="Clear">
            </div>
        </div>
    </div>
</asp:Content>
