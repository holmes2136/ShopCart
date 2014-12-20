<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductKitItemDetails.ascx.cs"
    Inherits="Components_ProductKitItemDetails" %>
<%@ Register Src="ProductKitDropDownItem.ascx" TagName="ProductKitDropDownItem" TagPrefix="uc1" %>
<%@ Register Src="ProductKitRadioItem.ascx" TagName="ProductKitRadioItem" TagPrefix="uc2" %>
<%@ Register Src="ProductKitCheckboxItem.ascx" TagName="ProductKitCheckboxItem" TagPrefix="uc3" %>
<div class="OptionItemDetails">
    <div class="OptionItemDetailsLeft">
        <table class="OptionItemDetailsTable">
            <tr id="DropdownTR" runat="server" visible="false">
                <td class="OptionItemDetailsDrop">
                    <uc1:ProductKitDropDownItem ID="uxProductKitDropDownItem" runat="server" />
                </td>
            </tr>
            <tr id="RadioTR" runat="server" visible="false">
                <td>
                    <uc2:ProductKitRadioItem id="uxProductKitRadioItem" runat="server" />
                </td>
            </tr>
            <tr id="CheckboxTR" runat="server" visible="false">
                <td class="OptionItemDetailsInputList">
                    <uc3:ProductKitCheckboxItem ID="uxProductKitCheckboxItem" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="OptionItemDetailsBottom">
        <div class="OptionItemDetailsBottomLeft">
            <div class="OptionItemDetailsBottomRight">
            </div>
        </div>
    </div>
</div>
