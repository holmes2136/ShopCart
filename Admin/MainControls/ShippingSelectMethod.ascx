<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingSelectMethod.ascx.cs"
    Inherits="AdminAdvanced_MainControls_ShippingSelectMethod" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <TopContentBoxTemplate>
        <asp:Label ID="uxTitleText" runat="server" meta:resourcekey="lcShippingMethodTitle" /></div>
    </TopContentBoxTemplate>
    <GridTemplate>
        <div class="Container-Box">
            <asp:RadioButtonList ID="uxShippingOptionRadioButtonList" runat="server" CellPadding="5"
                CellSpacing="10" CssClass="fl">
                <asp:ListItem Value="Fixed" Text="Fixed Cost"></asp:ListItem>
                <asp:ListItem Value="PerItem" Text="By Quantity"></asp:ListItem>
                <asp:ListItem Value="ByWeight" Text="By Weight"></asp:ListItem>
                <asp:ListItem Value="ByOrderTotal" Text="By Order Total"></asp:ListItem>
            </asp:RadioButtonList>
            <div class="Clear">
            </div>
            <div class="validator1 fl">
                <asp:RequiredFieldValidator ID="uxShippingOptionRequired" runat="server" meta:resourcekey="lcShippingMethodRequired"
                    ControlToValidate="uxShippingOptionRadioButtonList" ValidationGroup="VaildShipping"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="CommonRowStyleButton">
                <vevo:AdvanceButton ID="uxNextButton" runat="server" meta:resourcekey="uxNextButton"
                    CssClassBegin="mgr10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxNextButton_Click" OnClickGoTo="Top" ValidationGroup="VaildShipping">
                </vevo:AdvanceButton></div>
        </div>
   </GridTemplate>
</uc1:AdminContent>
