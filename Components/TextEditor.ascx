<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextEditor.ascx.cs" Inherits="Components_TextEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Panel ID="uxTextEditorPanel" runat="server">   
    <cc1:Editor ID="uxAjaxTextEditor" runat="server" NoUnicode="True" />
    <asp:TextBox ID="uxTextBox" runat="server" TextMode="multiLine"></asp:TextBox>
</asp:Panel>
