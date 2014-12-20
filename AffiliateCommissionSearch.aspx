<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="AffiliateCommissionSearch.aspx.cs"
    Inherits="AffiliateCommissionSearch" Title="[$SearchCommission]" %>

<%@ Register Src="Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc2" %>
<%@ Register Src="Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="AffiliateCommissionSearch">
        <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$SearchCommission]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="AffiliateCommissionSearchContentDiv">
                        <div class="AffiliateCommissionSearchLabel">
                            [$OrderID]
                        </div>
                        <div class="AffiliateCommissionSearchData">
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxStartOrderID" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxStartOrderIDCompare" runat="server" ControlToValidate="uxStartOrderID"
                                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                    <div class="CommonValidateDiv"></div>
                                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> ID is invalid.
                                </asp:CompareValidator></div>
                            <div class="Label">
                                [$To]</div>
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxEndOrderID" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxEndOrderIDCompare" runat="server" ControlToValidate="uxEndOrderID"
                                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid" Display="Dynamic"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                    <div class="CommonValidateDiv"></div>
                                    <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> ID is invalid.
                                </asp:CompareValidator>
                            </div>
                        </div>
                        <div class="AffiliateCommissionSearchLabel">
                            [$Amount]
                        </div>
                        <div class="AffiliateCommissionSearchData">
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxStartAmount" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxStartAmountCompare" runat="server" ControlToValidate="uxStartAmount"
                                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                <div class="CommonValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Amount is invalid.
                                </asp:CompareValidator></div>
                            <div class="Label">
                                [$To]</div>
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxEndAmount" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxEndAmountCompare" runat="server" ControlToValidate="uxEndAmount"
                                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                <div class="CommonValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Amount is invalid.
                                </asp:CompareValidator></div>
                        </div>
                        <div class="AffiliateCommissionSearchLabel">
                            [$Commission]
                        </div>
                        <div class="AffiliateCommissionSearchData">
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxStartCommission" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxStartCommissionCompare" runat="server" ControlToValidate="uxStartCommission"
                                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                <div class="CommonValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Commission is invalid.
                                </asp:CompareValidator></div>
                            <div class="Label">
                                [$To]</div>
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <asp:TextBox ID="uxEndCommission" runat="server" CssClass="CommonTextBox" />
                                <asp:CompareValidator ID="uxEndCommissionCompare" runat="server" ControlToValidate="uxEndCommission"
                                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="SearchValid"
                                    CssClass="CommonCommissionSearchValidateDiv">
                                <div class="CommonValidateDiv"></div>
                                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" /> Commission is invalid.
                                </asp:CompareValidator></div>
                        </div>
                        <div class="AffiliateCommissionSearchLabel">
                            [$OrderDate]
                        </div>
                        <div class="AffiliateCommissionSearchData">
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <uc2:CalendarPopup ID="uxStartDateCalendarPopup" runat="server" />
                            </div>
                            <div class="Label">
                                [$To]</div>
                            <div class="AffiliateCommissionSearchValidateDiv">
                                <uc2:CalendarPopup ID="uxEndDateCalendarPopup" runat="server" />
                            </div>
                        </div>
                        <div class="AffiliateCommissionSearchLabel">
                            [$PaymentStatus]
                        </div>
                        <div class="AffiliateCommissionSearchData">
                            <asp:DropDownList ID="uxStatus" runat="server" CssClass="Dropdown">
                                <asp:ListItem Value="" Text="-- Please Select --">
                                </asp:ListItem>
                                <asp:ListItem Value="False" Text="No">
                                </asp:ListItem>
                                <asp:ListItem Value="True" Text="Yes">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="AffiliateCommissionSearchButtonDiv">
                        <asp:LinkButton ID="uxSearchImageButton" runat="server" Text="[$BtnSearch]" OnClick="uxSearchImageButton_Click"
                            CssClass="AffiliateCommissionSearchImageButton BtnStyle2" ValidationGroup="SearchValid" />
                    </div>
                    <div class="Clear">
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
    </div>
</asp:Content>
