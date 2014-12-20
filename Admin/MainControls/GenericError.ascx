<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GenericError.ascx.cs"
    Inherits="Advanced_MainControls_GenericError" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <ContentTemplate>
        <div>
            <h3>
                Error</h3>
            There is an error occuring while we are processing your request. Please try again.
        </div>
    </ContentTemplate>
</uc1:AdminContent>
