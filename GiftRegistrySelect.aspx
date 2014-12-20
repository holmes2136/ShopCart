<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistrySelect.aspx.cs"
    Inherits="GiftRegistrySelect" Title="[$SelectGiftRegistry]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistrySelect">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$GiftRegistry]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <asp:Label ID="uxNoItemLabel" runat="server" CssClass="GiftRegistrySelectNoItemLabel"></asp:Label>
                    <asp:Panel ID="uxPanalGiftRegistry" runat="server" CssClass="GiftRegistrySelectPanel">
                        <asp:RadioButtonList ID="uxGiftRegistryRadioList" runat="server" CssClass="GiftRegistrySelectRadioButtonList">
                        </asp:RadioButtonList>
                        <div class="Clear">
                        </div>
                        <asp:RequiredFieldValidator ID="GiftRegistryRequired" runat="server" ErrorMessage="Please select gift registry"
                            CssClass="GiftRegistrySelectValidator" ControlToValidate="uxGiftRegistryRadioList">
                        </asp:RequiredFieldValidator>
                        <div class="GiftRegistrySelectButtonDiv">
                            <asp:LinkButton  ID="uxAddItemImageButton" runat="server" OnClick="uxAddItemImageButton_Click"
                                Text="[$BtnAddNewItem]" CssClass="GiftRegistrySelectImageButton BtnStyle1" />
                        </div>
                    </asp:Panel>
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
