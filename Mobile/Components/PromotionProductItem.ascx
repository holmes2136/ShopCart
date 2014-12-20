<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionProductItem.ascx.cs"
    Inherits="Mobile_Components_PromotionProductItem" %>
<%@ Register Src="~/Components/OptionGroupDetails.ascx" TagName="OptionGroupDetails"
    TagPrefix="uc1" %>
<div id="uxMobilePromotionProductItemDiv" runat="server" class="MobilePromotionProductItem">
    <table cellpadding="10" cellspacing="0" border="0" width="100%">
        <tr>
            <td width="10%">
                <div class="ProductSelect">
                    <asp:RadioButton ID="uxProductSelect" runat="server" AutoPostBack="true" OnCheckedChanged="uxProductSelect_CheckedChanged" />
                </div>
            </td>
            <td width="20%">
                <div class="ProductImage">
                    <asp:HyperLink ID="uxImageLink" runat="server">
                        <asp:Image ID="uxProductImage" runat="server" Width="85px" ImageAlign="Middle" />
                    </asp:HyperLink>
                </div>
            </td>
            <td class="TableProductName">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <div class="ProductName">
                                <asp:HyperLink ID="uxNameLink" runat="server">
                                    <asp:Label ID="uxNameText" runat="server" />
                                </asp:HyperLink><asp:Label ID="uxSignText" runat="server" Text="x " CssClass="ProductQuantitySign" />
                                <asp:Label ID="uxQuantityText" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="uxFixOptionPanel" runat="server" CssClass="FixProductOption">
                                <asp:Label ID="uxFixOption" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="ProductPrice">
                                <asp:Label ID="uxPriceText" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="TableProductOption">
                <asp:HiddenField ID="uxOptionHidden" runat="server" />
                <asp:Label ID="uxOptionItemHiddenText" runat="server" Visible="false"></asp:Label>
                <asp:Panel ID="uxPopupPanel" runat="server">
                    <div class="ProductOption">
                        <asp:HyperLink ID="uxOptionButton" runat="server" CssClass="ProductOptionHyperLink">[$BtnOption]</asp:HyperLink></div>
                    <ajaxToolkit:ModalPopupExtender ID="uxOptionPopup" runat="server" TargetControlID="uxOptionButton"
                        CancelControlID="uxCancelButton" PopupControlID="uxOptionPanel" RepositionMode="None"
                        BackgroundCssClass="OptionPopup" DropShadow="false">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="uxOptionPanel" runat="server" CssClass="OptionPanel">
                        <div>
                            <uc1:OptionGroupDetails ID="uxOptionGroupDetails" runat="server"></uc1:OptionGroupDetails>
                        </div>
                        <div class="OptionButton">
                            <asp:Button ID="uxOkButton" runat="server" Text="[$OK]" OnClick="uxOkButton_Click"
                                CssClass="BtnStyle1" />
                            <asp:Button ID="uxCancelButton" runat="server" Text="[$Cancel]" CssClass="BtnStyle2" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
