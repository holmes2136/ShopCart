<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductItem.ascx.cs"
    Inherits="Components_PromotionProductItem" %>
<%@ Register Src="OptionGroupDetails.ascx" TagName="OptionGroupDetails" TagPrefix="uc1" %>
<div class="PromotionProductItem">
    <div class="ProductImage">
        <asp:HyperLink ID="uxImageLink" runat="server">
            <asp:Image ID="uxProductImage" runat="server" Width="70px" ImageAlign="Middle" />
        </asp:HyperLink>
    </div>
    <div class="ProductQuantity">
        <asp:Label ID="uxSignText" runat="server" Text="x " CssClass="ProductQuantitySign" />
        <asp:Label ID="uxQuantityText" runat="server" />
    </div>
    <div class="Clear">
    </div>
    <div class="ProductName">
        <asp:HyperLink ID="uxNameLink" runat="server">
            <asp:Label ID="uxNameText" runat="server" />
        </asp:HyperLink>
        <asp:Panel ID="uxFixOptionPanel" runat="server" CssClass="FixProductOption">
            <asp:Label ID="uxFixOption" runat="server" />
        </asp:Panel>
    </div>
    <div class="ProductPrice">
        <asp:Label ID="uxPriceText" runat="server" />
    </div>
    <asp:HiddenField ID="uxOptionHidden" runat="server" />
    <asp:Label ID="uxOptionItemHiddenText" runat="server" Visible="false"></asp:Label>
    <asp:Panel ID="uxPopupPanel" runat="server">
        <div id="uxOptionButton" runat="server" class="ProductOption">
            <div class="BtnStyle3">
                [$BtnSpecific]</div>
        </div>
        <ajaxToolkit:ModalPopupExtender ID="uxOptionPopup" runat="server" TargetControlID="uxOptionButton"
            CancelControlID="uxCancelButton" PopupControlID="uxOptionPanel" RepositionMode="None"
            BackgroundCssClass="ModalBackground OptionPopup">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="uxOptionPanel" runat="server" CssClass="OptionPanel">
            <uc1:OptionGroupDetails ID="uxOptionGroupDetails" runat="server"></uc1:OptionGroupDetails>
            <div class="OptionButton">
                <asp:LinkButton ID="uxOkButton" runat="server" Text="[$BtnPromotionOK]" CssClass="BtnStyle1"
                    OnClick="uxOkButton_Click" ValidationGroup="ValidOption" />
                <asp:LinkButton ID="uxCancelButton" runat="server" Text="[$BtnPromotionCancel]" CssClass="BtnStyle2" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <div class="ProductSelect">
        <asp:RadioButton ID="uxProductSelect" runat="server" AutoPostBack="true" OnCheckedChanged="uxProductSelect_CheckedChanged" />
    </div>
</div>
