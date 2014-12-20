<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutShippingEstimator.ascx.cs"
    Inherits="Components_CheckoutShippingEstimator" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="CountryAndStateList.ascx" TagName="CountryAndState" TagPrefix="uc2" %>
<%@ Import Namespace="Vevo" %>
<asp:UpdatePanel ID="uxShippingEstimatorUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="ShoppingCartShippingEstimator">
            <uc2:CountryAndState ID="uxCountryAndState" runat="server" IsRequiredCountry="false"
                IsRequiredState="false" IsCountryWithOther="true" IsStateWithOther="false" CssLabel="ShoppingCartShippingEstimatorLabel" />
            <div class="ShoppingCartShippingEstimatorLabel">
                [$Zip]
            </div>
            <div class="ShoppingCartShippingEstimatorInput">
                <asp:TextBox ID="uxZip" runat="server" MaxLength="9" CssClass="CommonTextBox"></asp:TextBox>
            </div>
            <div class="ShoppingCartShippingEstimatorButton">
                <asp:LinkButton ID="uxEstimateButton" runat="server" OnClick="uxEstimateButton_Click"
                    Text="[$EstimateButton]" CssClass="BtnStyle2" />
            </div>
            <div id="uxMessageDiv" runat="server" class="ShoppingCartShippingEstimatorMessage">
                <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="1" />
            </div>
            <div id="uxShippingMethodListDiv" runat="server" class="ShoppingCartShippingEstimatorShippingList">
                <asp:RadioButtonList ID="uxShippingRadioList" runat="server">
                </asp:RadioButtonList>
            </div>
            <div id="uxMessageNotSelectDiv" runat="server" class="CommonValidatorText ShippingEstimatorValidatorText"
                visible="false">
                <div class="CommonValidateDiv ShippingEstimatorValidateDiv">
                </div>
                <img src="Images/Design/Bullet/RequiredFillBullet_Up.gif" />
                <asp:Label ID="uxNotSelectShippingMessage" runat="server"></asp:Label>
            </div>
            <div class="ShoppingCartShippingEstimatorButton">
                <asp:LinkButton ID="uxSelectShippingButton" runat="server" OnClick="uxSelectShippingButton_Click"
                    Text="[$SelectShippingButton]" CssClass="BtnStyle2" Visible="false" />
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uxEstimateButton" />
    </Triggers>
</asp:UpdatePanel>
