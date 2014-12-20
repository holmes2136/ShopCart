<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickSearch.ascx.cs" Inherits="Mobile_Components_QuickSearch" %>
<%@ Register Src="~/Components/VevoLinkButton.ascx" TagName="LinkButton" TagPrefix="ucLinkButton" %>
<div class="MobileQuickSearch">
    <table cellpadding="0" cellspacing="2" border="0" width="100%">
        <tr>
            <td>
                <asp:TextBox ID="uxSearchText" runat="server" CssClass="MobileQuickSearchText" />
              <asp:ImageButton ID="uxSearchButton" runat="server" ImageUrl="[$ImgButton]" OnClick="uxSearchButton_Click"
                    CssClass="MobileQuickSearchButton" />
            </td>
        </tr>
    </table>
</div>
