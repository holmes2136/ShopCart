<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RmaEdit.ascx.cs" Inherits="AdminAdvanced_MainControls_RmaEdit" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="CommonTitle">
            <asp:Label ID="lcRmaID" runat="server" meta:resourcekey="lcRmaID" CssClass="fl mgl5" />
            <asp:Label ID="uxRmaIDLabel" runat="server" CssClass="fl mgl5" />
        </div>
        <div class="Container-Box">
            <div class="CommonRowStyle">
                <asp:Label ID="uxStatusLabel" runat="server" meta:resourcekey="uxStatusLabel" CssClass="Label pdt5" />
                <asp:DropDownList ID="uxStatusDrop" runat="server" AutoPostBack="false" CssClass="fl DropDown mgt5">
                    <asp:ListItem Value="New">New</asp:ListItem>
                    <asp:ListItem Value="Processing">Processing</asp:ListItem>
                    <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                    <asp:ListItem Value="Returned">Returned</asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <asp:Panel ID="uxStorePanel" CssClass="CommonRowStyle" runat="server" Visible='<%# Vevo.KeyUtilities.IsMultistoreLicense() %>'>
                <asp:Label ID="uxStoreLabel" runat="server" meta:resourcekey="uxStoreLabel" CssClass="Label" />
                <asp:Label ID="uxStoreText" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <div class="CommonRowStyle">
                <asp:Label ID="uxUserNameLabel" runat="server" meta:resourcekey="uxUserNameLabel"
                    CssClass="Label" />
                <asp:Label ID="uxUserNameText" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxOrderLabel" runat="server" meta:resourcekey="uxOrderLabel" CssClass="Label" />
                <asp:Label ID="uxOrderText" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxRmaDateLabel" runat="server" meta:resourcekey="uxDateLabel" CssClass="Label" />
                <asp:Label ID="uxRmaDateText" runat="server" CssClass="fl" />
                <div class="Clear">
                </div>
            </div>
            <div class="RmaRowTitle mgt10">
                <asp:Label ID="uxProductDetailLabel" runat="server" meta:resourcekey="uxProductDetailLabel" />
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxProductLabel" runat="server" meta:resourcekey="uxProductLabel" CssClass="Label pdt5" />
                <asp:Label ID="uxProductText" runat="server" CssClass="fl mgt5" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxQuantityLabel" runat="server" meta:resourcekey="uxQuantityLabel"
                    CssClass="Label pdt5" />
                <asp:TextBox ID="uxQuantityText" runat="server" CssClass="fl TextBox mgt5" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxReasonLabel" runat="server" meta:resourcekey="uxReasonLabel" CssClass="Label pdt5" />
                <asp:TextBox ID="uxReasonText" runat="server" TextMode="MultiLine" Rows="5" Width="250px"
                    CssClass="TextBox mgt5" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxActionLabel" runat="server" meta:resourcekey="uxActionLabel" CssClass="Label pdt5" />
                <asp:DropDownList ID="uxActionDrop" runat="server" AutoPostBack="false" CssClass="fl DropDown mgt5">
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Label ID="uxNoteLabel" runat="server" meta:resourcekey="uxNoteLabel" CssClass="Label pdt5" />
                <asp:TextBox ID="uxNoteText" runat="server" TextMode="MultiLine" Rows="5" Width="250px"
                    CssClass="fl TextBox mgt5" />
                <div class="Clear">
                </div>
            </div>
            <div class="Clear">
            </div>
            <vevo:AdvanceButton ID="uxCancelButton" runat="server" meta:resourcekey="uxCancelButton"
                CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                CausesValidation="false" OnClickGoTo="Top" CommandName="Cancel">
            </vevo:AdvanceButton>
            <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                CausesValidation="false" OnClickGoTo="Top" OnCommand="uxUpdateButton_Command">
            </vevo:AdvanceButton>
            <div class="Clear">
            </div>
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
