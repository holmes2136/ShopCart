<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionItemDetails.ascx.cs"
    Inherits="Components_OptionItemDetails" %>
<%@ Register Src="OptionDropDownItem.ascx" TagName="OptionDropDownItem" TagPrefix="uc1" %>
<%@ Register Src="OptionRadioItem.ascx" TagName="OptionRadioItem" TagPrefix="uc2" %>
<%@ Register Src="OptionTextItem.ascx" TagName="OptionTextItem" TagPrefix="uc3" %>
<%@ Register Src="OptionInputListItem.ascx" TagName="OptionInputListItem" TagPrefix="uc4" %>
<%@ Register Src="OptionUploadItem.ascx" TagName="OptionUploadItem" TagPrefix="uc5" %>
<%@ Register Src="OptionUploadRequireItem.ascx" TagName="OptionUploadRequireItem"
    TagPrefix="uc6" %>
<div class="OptionItemDetails">
    <div class="OptionItemDetailsTop">
        <div class="OptionItemDetailsTopLeft">
            <div class="OptionItemDetailsTopRight">
                <asp:Label ID="uxOptionGroupLabel" runat="server" CssClass="OptionDisplayText"></asp:Label>
            </div>
        </div>
    </div>
    <div class="OptionItemDetailsLeft">
        <div class="OptionItemDetailsRight">
            <div class="OptionItemDetailsErrorMessage">
                <asp:Label ID="uxErrorMessageLabel" runat="server" Text="Please select" Visible="false"></asp:Label>
            </div>
            <table class="OptionItemDetailsTable">
                <tr id="DropdownTR" runat="server" visible="false">
                    <td class="OptionItemDetailsDrop">
                        <uc1:OptionDropDownItem ID="uxOptionDropDownItem" runat="server" />
                    </td>
                </tr>
                <tr id="RadioTR" runat="server" visible="false">
                    <td>
                        <uc2:OptionRadioItem ID="uxOptionRadioItem" runat="server" />
                    </td>
                </tr>
                <tr id="TextTR" runat="server" visible="false">
                    <td class="OptionItemDetailsText">
                        <asp:HiddenField ID="uxOptionIDHidden" runat="server" />
                        <uc3:OptionTextItem ID="uxOptionTextItem" runat="server" />
                    </td>
                </tr>
                <tr id="InputListTR" runat="server" visible="false">
                    <td class="OptionItemDetailsInputList">
                        <uc4:OptionInputListItem ID="uxOptionInputListItem" runat="server" />
                    </td>
                </tr>
                <tr id="UploadTR" runat="server" visible="false">
                    <td class="OptionItemDetailsUpload">
                        <asp:HiddenField ID="uxUploadHiddenField" runat="server" />
                        <uc5:OptionUploadItem ID="uxOptionUploadItem" runat="server" />
                    </td>
                </tr>
                <tr id="UploadRQTR" runat="server" visible="false">
                    <td class="OptionItemDetailsUploadRequire">
                        <asp:HiddenField ID="uxUploadRQHiddenField" runat="server" />
                        <uc6:OptionUploadRequireItem ID="uxOptionUploadRequireItem" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="OptionItemDetailsBottom">
        <div class="OptionItemDetailsBottomLeft">
            <div class="OptionItemDetailsBottomRight">
            </div>
        </div>
    </div>
</div>
