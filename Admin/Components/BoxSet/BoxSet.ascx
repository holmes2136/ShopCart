<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BoxSet.ascx.cs" Inherits="AdminAdvanced_Components_BoxSet_BoxSet" %>
<asp:Panel ID="uxBoxSetPanel" runat="server">
    <div class="TopBar fs0">
        <div class="TopImgLeft fl">
        </div>
        <div class="TopBarTitle fl">
            <asp:PlaceHolder ID="uxTitlePlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
        <div class="TopImgRight fr">
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="CenterLeft">
        <div class="CenterRight">
            <asp:Panel ID="uxContentPanel" runat="server">
                <asp:PlaceHolder ID="uxContentPlaceHolder" runat="server"></asp:PlaceHolder>
            </asp:Panel>
            <div class="Clear">
            </div>
        </div>
    </div>
    <div class="BottomBar fs0">
        <div class="BottomImgLeft fl">
        </div>
        <div class="BottomImgRight fr">
        </div>
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
