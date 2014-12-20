<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchBox.ascx.cs" Inherits="AdminAdvanced_Components_SearchBox" %>
<asp:Label ID="Label13" runat="server" CssClass="AdminLeftSearchLable" Text="Search :" />
<div class="AdminLeftSearchValue">
    <asp:TextBox ID="uxSearchText" runat="server" CssClass="AdminLeftSearchTextbox" EnableViewState="false"
        Width="125px" />
    <asp:RequiredFieldValidator ID="uxSearchRequiredValidator" runat="server" ControlToValidate="uxSearchText"
        Display="Dynamic" CssClass="CommonValidatorText">
            <div class="CommonValidateDiv CommonValidateDivItemBox">
            </div>
            <img src="../Images/Design/Bullet/RequiredFillBullet_Up.gif" />Search word is required.
    </asp:RequiredFieldValidator>
</div>
<asp:Panel ID="uxTextPanel" runat="server" CssClass="fl" Style="display: none;">
    <asp:TextBox ID="uxValueText" runat="server" CssClass="AdminLeftSearchTextbox" />
</asp:Panel>
<asp:Label ID="Label11" runat="server" CssClass="AdminLeftSearchLable" Text="From :" />
<div class="AdminLeftSearchValue">
    <asp:DropDownList ID="uxFilterDrop" runat="server" CssClass="AdminLeftSearchDrop">
    </asp:DropDownList>
</div>
<vevo:AdvanceButton ID="uxSearchButton" runat="server" meta:resourcekey="uxSearchButton"
    CssClassBegin="mgt10 fr" CssClassEnd="Button1Right" CssClass="ButtonBlue" OnClick="uxSearchButton_Click"
    OnClickGoTo="None" />
<asp:HiddenField ID="uxHiddenValueSearch" runat="server" />
<asp:HiddenField ID="uxHiddenFilterSearch" runat="server" />
<div class="Clear">
</div>
