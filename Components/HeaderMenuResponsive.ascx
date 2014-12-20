<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderMenuResponsive.ascx.cs"
    Inherits="Components_HeaderMenuResponsive" %>
<nav class="top-bar">
    <ul class="title-area">
        <!-- Title Area -->
        <!-- Remove the class "menu-icon" to get rid of menu icon. Take out "Menu" to just have icon alone -->
        <li class="toggle-topbar"><a href=""><span>Menu</span></a></li>
    </ul>
    <section class="top-bar-section">
        <asp:Panel ID="uxMenuPanel" runat="server"></asp:Panel>
    </section>
</nav>
