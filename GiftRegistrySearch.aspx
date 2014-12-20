<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GiftRegistrySearch.aspx.cs"
    Inherits="GiftRegistrySearch" Title="[$SearchGiftRegistry]" %>

<%@ Register Src="Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc2" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GiftRegistrySearch">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$FindGiftRegistry]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="GiftRegistrySearchContent">
                        <div class="GiftRegistrySearchLabel">
                            [$FirstName]
                        </div>
                        <div class="GiftRegistrySearchData">
                            <asp:TextBox ID="uxFirstName" runat="server" Width="150px" CssClass="CommonTextBox" />
                        </div>
                        <div class="GiftRegistrySearchLabel">
                            [$LastName]
                        </div>
                        <div class="GiftRegistrySearchData">
                            <asp:TextBox ID="uxLastName" runat="server" Width="150px" CssClass="CommonTextBox" />
                        </div>
                        <div class="GiftRegistrySearchLabel">
                            [$EventName]
                        </div>
                        <div class="GiftRegistrySearchData">
                            <asp:TextBox ID="uxEventName" runat="server" Width="150px" CssClass="CommonTextBox" />
                        </div>
                        <div class="GiftRegistrySearchLabel">
                            [$EventDate]
                        </div>
                        <div class="GiftRegistrySearchData">
                            <uc2:CalendarPopup ID="uxStartDateCalendarPopup" runat="server" />
                            To
                            <uc2:CalendarPopup ID="uxEndDateCalendarPopup" runat="server" />
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="GiftRegistrySearchButton">
                        <asp:LinkButton ID="uxFindImageButton" runat="server" Text="[$BtnFindGiftRegistry]"
                            CssClass="BtnStyle2" OnClick="uxFindImageButton_Click" />
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
