<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreDropDownList.ascx.cs" Inherits="Admin_Components_StoreDropDownList" %>
<asp:DropDownList ID="uxStoreDrop" runat="server" Width="155px" OnSelectedIndexChanged="uxDrop_SelectedIndexChanged"
    >
</asp:DropDownList><span id="uxStar" runat="server" class="ValidateText">*</span>
<asp:HiddenField ID="uxSelectedStoreHidden" runat="server" />
