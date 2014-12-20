<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="Content.aspx.cs"
    Inherits="ContentPage" ValidateRequest="false" %>

<%@ Register Src="Components/ContentLayout.ascx" TagName="ContentLayout" TagPrefix="uc1" %>

<%@ Import Namespace="Vevo.Domain" %>
<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div >
        <uc1:ContentLayout ID="uxContentLayout" runat="server" />
    </div>
</asp:Content>
