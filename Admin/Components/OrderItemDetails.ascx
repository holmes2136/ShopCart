<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderItemDetails.ascx.cs"
    Inherits="AdminAdvanced_Components_OrderItemDetails" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="BoxSet/Boxset.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<%@ Register Src="Template/AdminUserControlContent.ascx" TagName="AdminContent" TagPrefix="uc2" %>
<uc2:AdminContent ID="uxAdminContent" runat="server">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <ButtonEventTemplate>
        <vevo:AdvanceButton ID="uxOrderLink" runat="server" meta:resourcekey="uxOrderLink"
            CssClass="CommonAdminButtonIcon AdminButtonIconView fl" OnClick="uxOrderLink_Click" />
    </ButtonEventTemplate>
    <ValidationSummaryTemplate>
        <asp:ValidationSummary ID="uxValidationSummary" runat="server" meta:resourcekey="uxValidationSummary"
            ValidationGroup="OrderItemsDetails" CssClass="ValidationStyle" />
    </ValidationSummaryTemplate>
    <ValidationDenotesTemplate>
        <div class="RequiredLabel c6">
            *
            <asp:Label ID="lcRequiredFieldSymbol" runat="server" meta:resourcekey="lcRequiredFieldSymbol" /></div>
    </ValidationDenotesTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="OrderEditRowTitle">
                <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" />
                <asp:Label ID="uxOrderIDdisplayLable" runat="server"></asp:Label></div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcName" runat="server" meta:resourcekey="lcName" />
                </div>
                <asp:TextBox ID="uxNameText" runat="server" Width="300px" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireNameValid" runat="server" ControlToValidate="uxNameText"
                        Display="Dynamic" meta:resourcekey="uxNameRequireValidator" ValidationGroup="OrderItemsDetails"><--</asp:RequiredFieldValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcQuantity" runat="server" meta:resourcekey="lcQuantity"></asp:Label></div>
                <asp:TextBox ID="uxQuantityText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireQuantityValid" runat="server" ControlToValidate="uxQuantityText"
                        Display="Dynamic" meta:resourcekey="uxQuantityRequireValidator" ValidationGroup="OrderItemsDetails"><--</asp:RequiredFieldValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcSku" runat="server" meta:resourcekey="lcSku" /></div>
                <asp:TextBox ID="uxSkuText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <div class="Label">
                    <asp:Label ID="lcUnitPrice" runat="server" meta:resourcekey="lcUnitPrice" /></div>
                <asp:TextBox ID="uxUnitPriceText" runat="server" CssClass="TextBox"></asp:TextBox>
                <div class="validator1 fl">
                    <span class="Asterisk">*</span>
                    <asp:RequiredFieldValidator ID="uxRequireUnitPriceValid" runat="server" ControlToValidate="uxUnitPriceText"
                        Display="Dynamic" meta:resourcekey="uxUnitpriceRequireValidator" ValidationGroup="OrderItemsDetails"><--</asp:RequiredFieldValidator>
                </div>
                <div class="Clear">
                </div>
            </div>
            <div class="CommonRowStyle">
                <asp:Panel ID="DownloadCountPanel" runat="server">
                    <div class="Label">
                        <asp:Label ID="lcDownloadCount" runat="server" meta:resourcekey="lcDownloadCount" />
                    </div>
                    <asp:TextBox ID="uxDownloadCountText" runat="server" CssClass="TextBox"></asp:TextBox>
                    <div class="validator1 fl">
                        <span runat="server" class="Asterisk">*</span>
                        <asp:CustomValidator ID="uxRequiredDownloadCountValid" runat="server" ControlToValidate="uxDownloadCountText"
                            Display="Dynamic" meta:resourcekey="uxDownloadCountRequireValidator" OnServerValidate="checkDownloadCount"
                            ValidationGroup="OrderItemsDetails" EnableClientScript="false"><--</asp:CustomValidator>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
            </div>
            <div class="Clear" />
            <div class="mgt10">
                <vevo:AdvanceButton ID="uxAddButton" runat="server" meta:resourcekey="uxAddButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxAddButton_Click" OnClickGoTo="Top" ValidationGroup="OrderItemsDetails" />
                <vevo:AdvanceButton ID="uxUpdateButton" runat="server" meta:resourcekey="uxUpdateButton"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxUpdateButton_Click" OnClickGoTo="Top" ValidationGroup="OrderItemsDetails" />
            </div>
        </div>
    </ContentTemplate>
</uc2:AdminContent>
