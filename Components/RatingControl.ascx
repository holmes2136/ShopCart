<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RatingControl.ascx.cs"
    Inherits="Components_RatingControl" %>
<asp:Repeater ID="uxRatingRepeater" runat="server">
    <HeaderTemplate>
        <table cellpadding="0" cellspacing="0" class="RatingControlTable">
            <tr>
    </HeaderTemplate>
    <ItemTemplate>
        <td class="RatingControlColumn">
            <img src='<%# GetStarPicture(  Eval( "Vote" ).ToString() ) %>' alt="" class="RatingControlImage" />
        </td>
    </ItemTemplate>
    <FooterTemplate>
        </tr> </table>
    </FooterTemplate>
</asp:Repeater>
