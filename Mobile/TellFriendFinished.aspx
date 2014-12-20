<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="TellFriendFinished.aspx.cs"
    Inherits="TellFriendFinished" Title="[$Title]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="MobileTellFriendFinished">
        <div class="MobileTitle">
            [$Title]
        </div>
        <div>
            <asp:Image ID="uxIconImage" runat="server" ImageUrl="Images/Design/Icon/ContactUsFinished.gif"
                AlternateText="" CssClass="TellFriendFinishedImageIcon" />
            <h4 class="TellFriendFinishedMessage">
                [$Sent]</h4>
        </div>
    </div>
</asp:Content>
