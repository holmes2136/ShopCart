<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayCategorySelect.ascx.cs"
    Inherits="Admin_Components_EBayCategorySelect" %>
<asp:Panel ID="uxPrimaryCategoryListPanel" runat="server" CssClass="EbayCategoryListPanel"
    Height="350" ScrollBars="Horizontal" Wrap="False" Visible="false">
    <div id="uxPrimaryCategoryList1Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory1RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory1RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <div id="uxPrimaryCategoryList2Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory2RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory2RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <div id="uxPrimaryCategoryList3Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory3RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory3RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <div id="uxPrimaryCategoryList4Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory4RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory4RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <div id="uxPrimaryCategoryList5Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory5RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory5RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <div id="uxPrimaryCategoryList6Panel" runat="server" style="width: 250px; height: 300px;
        display: inline-block; position: relative; overflow: auto">
        <asp:RadioButtonList ID="uxCategory6RadioList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="uxCategory6RadioList_SelectedIndexChanged">
        </asp:RadioButtonList>
    </div>
    <asp:HiddenField ID="uxListSiteHidden" runat="server" />
</asp:Panel>
