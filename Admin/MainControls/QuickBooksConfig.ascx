<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickBooksConfig.ascx.cs"
    Inherits="AdminAdvanced_MainControls_QuickBooksConfig" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Common/HelpIcon.ascx" TagName="HelpIcon" TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc2:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="CommonConfigTitle mgt0">
            <asp:Label ID="uxTitleLabel" runat="server" meta:resourcekey="uxTitleLabel"></asp:Label></div>
    </PlainContentTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <asp:Panel ID="uxAccountNameTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxAccountNameHelp" ConfigName="QBIncomeAccountName" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcAccountName" runat="server" meta:resourcekey="lcAccountName"></asp:Label>
                </div>
                <asp:TextBox ID="uxAccountNameText" runat="server" CssClass="fl TextBox" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxAdminAccountTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxAdminAccountHelp" ConfigName="QBVevoAdminUser" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcAdminAccount" runat="server" meta:resourcekey="lcAdminAccount"></asp:Label>
                </div>
                <asp:TextBox ID="uxAdminAccountText" runat="server" CssClass="fl TextBox" />
            </asp:Panel>
            <asp:Panel ID="uxTaxItemTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxTaxItemHelp" ConfigName="QBTaxItem" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcTaxItem" runat="server" meta:resourcekey="lcTaxItem"></asp:Label>
                </div>
                <asp:TextBox ID="uxTaxItemText" runat="server" CssClass="fl TextBox" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxDiscountItemTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxDiscountItemHelp" ConfigName="QBDiscountItem" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcDiscountItem" runat="server" meta:resourcekey="lcDiscountItem"></asp:Label>
                </div>
                <asp:TextBox ID="uxDiscountItemText" runat="server" CssClass="fl TextBox" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxShippingItemTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxShippingItemHelp" ConfigName="QBShippingItem" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcShippingItem" runat="server" meta:resourcekey="lcShippingItem"></asp:Label>
                </div>
                <asp:TextBox ID="uxShippingItemText" runat="server" CssClass="f1 TextBox" />
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxCustomerNameTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxCustomerNameHelp" ConfigName="QBCustomerNameFormat" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcCustomerName" runat="server" meta:resourcekey="lcCustomerName"></asp:Label>
                </div>
                <asp:DropDownList ID="uxCustomerNameDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="firstlast">Firstname Lastname</asp:ListItem>
                    <asp:ListItem Value="lastfirst">Lastname, Firstname</asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </asp:Panel>
            <asp:Panel ID="uxStoreTR" runat="server" CssClass="ConfigRow">
                <uc1:HelpIcon ID="uxStoreHelp" ConfigName="QBSelectStoreExport" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcStore" runat="server" meta:resourcekey="lcStore"></asp:Label>
                </div>
                <asp:DropDownList ID="uxStoreDrop" runat="server" CssClass="DropDown">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="uxEnableCustomerModifyTR" runat="server" CssClass="ConfigRow" Visible="false">
                <uc1:HelpIcon ID="uxEnableCustomerModifyHelp" ConfigName="QBEnableCustomerModify"
                    runat="server" />
                <div class="Label">
                    <asp:Label ID="lcEnableCustomerModify" runat="server" meta:resourcekey="lcEnableCustomerModify"></asp:Label>
                </div>
                <asp:DropDownList ID="uxEnableCustomerModifyDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="uxEnableProductModifyTR" runat="server" CssClass="ConfigRow" Visible="false">
                <uc1:HelpIcon ID="uxEnableProductModifyHelp" ConfigName="QBEnableProductModify" runat="server" />
                <div class="Label">
                    <asp:Label ID="lcEnableProductModify" runat="server" meta:resourcekey="lcEnableProductModify"></asp:Label>
                </div>
                <asp:DropDownList ID="uxEnableProductModifyDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False">No</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
            <div class="ConfigRow">
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxUpdateButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <vevo:AdvanceButton ID="uxViewLogButton" runat="server" meta:resourcekey="uxViewLogButton"
                    CssClassBegin="fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" OnClick="uxViewLogButton_Click"
                    OnClickGoTo="Top"></vevo:AdvanceButton>
                <div class="Clear">
                </div>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
