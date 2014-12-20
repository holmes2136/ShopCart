<%@ Page Title="[$Title]" Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true"
    CodeFile="ShippingAddressBook.aspx.cs" Inherits="ShippingAddressBook" %>

<%@ Register Src="Components/ShippingAddressItemDetails.ascx" TagName="ShippingAddressDetails"
    TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="ShippingAddressBook">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxShippingAddressBookTitle" runat="server" Text="[$Head]" CssClass="CommonPageTopTitle">
                </asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="ShippingAddressBookDiv">
                        <asp:Panel ID="uxShippingPanel" runat="server" CssClass="ShippingAddressBookPanel">
                            <div class="MyAccountTitle">
                                [$Shipping]
                            </div>
                            <asp:DataList ID="uxList" CssClass="ShippingAddressBookList" runat="server" ShowFooter="false"
                                ShowHeader="false">
                                <ItemTemplate>
                                    <uc1:ShippingAddressDetails ID="uxItem" runat="server" ShippingAddressID='<%# (Eval("ShippingAddressID").ToString()) %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="ShippingAddressBookListItem" />
                            </asp:DataList>
                            <div class="ShippingAddressBookButtonDiv">
                                <asp:LinkButton ID="uxAddNewShippingAddress" runat="server" OnClick="uxAddNewShippingAddress_Click"
                                    CssClass="CustomerRegisterLinkButtonImage BtnStyle1" Text="[$BtnAddShippingAddress]" />
                            </div>
                        </asp:Panel>
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
