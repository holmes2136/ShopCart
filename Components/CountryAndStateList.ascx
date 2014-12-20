<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryAndStateList.ascx.cs"
    Inherits="Components_CountryAndStateList" %>
<asp:Panel ID="uxContryStatePanel" runat="server" CssClass="CountryAndStateList">
    <asp:Label ID="uxCountryLabel" runat="server" Text="[$Country]" CssClass="CountryAndStateListLabel"></asp:Label>
    <asp:DropDownList ID="uxCountryDrop" runat="server" CssClass="CountryAndStateListDropDown">
    </asp:DropDownList>
    <asp:TextBox ID="uxCountryText" runat="server" CssClass="CountryAndStateListCountryTextBox"></asp:TextBox>
    <asp:Panel ID="uxValidationCountryPanel" runat="server" CssClass="CountryAndStateListValidationPanel">
        <div id="uxCountryAsteriskDiv" runat="server">
            <span class="CommonAsterisk">*</span>
        </div>
    </asp:Panel>
    <div class="Clear">
    </div>
    <asp:Label ID="uxStateLabel" runat="server" Text="[$State]" CssClass="CountryAndStateListLabel"></asp:Label>
    <asp:DropDownList ID="uxStateDrop" runat="server" CssClass="CountryAndStateListDropDown">
    </asp:DropDownList>
    <asp:TextBox ID="uxStateText" runat="server" CssClass="CountryAndStateListStateTextBox"></asp:TextBox>
    <asp:Panel ID="uxValidationStatePanel" runat="server" CssClass="CountryAndStateListValidationPanel">
        <div id="uxStatesAsteriskDiv" runat="server">
            <span class="CommonAsterisk">*</span>
        </div>
    </asp:Panel>
    <div class="Clear">
    </div>
</asp:Panel>
<%--<asp:HiddenField ID="uxCountryHidden" runat="server" />--%>
<%--<asp:HiddenField ID="uxStateHidden" runat="server" />--%>
