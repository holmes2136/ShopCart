<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportCustomer.ascx.cs"
    Inherits="AdminAdvanced_Components_ExportCustomer" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="ExportFilter"
    TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div>
            <div class="ExportNotice">
                <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice" CssClass="CommonTextTitle" />
            </div>
            <div class="ExportFilterPanel">
                <div class="ExportFilterDefault">
                    <asp:Label ID="uxSortByLabel" runat="Server" meta:resourcekey="uxSortByLabel" CssClass="Label" />
                    <asp:DropDownList ID="uxSortByDrop" runat="server" CssClass="DropDown" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="ExportFilterPanel">
                <uc2:ExportFilter ID="uxExportFilter" runat="server" IsExportFilter="true" />
            </div>
            <div>
                <vevo:AdvanceButton ID="uxGenerateButton" runat="server" meta:resourcekey="uxGenerateButton" 
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange" 
                    OnClick="uxGenerateButton_Click" OnClickGoTo="top" />
            </div>
            <div class="Clear">
            </div>
        </div>
        <div class="mgt10">
            <div class="ExportNotice">
                <asp:Label ID="uxMessageNotice2" runat="server" meta:resourcekey="uxMessageNotice2" CssClass="CommonTextTitle" />
            </div>
            <div class="ExportFilterPanel">
                <div class="ExportFilterDefault">
                    <asp:Label ID="uxShippingSortByLabel" runat="Server" meta:resourcekey="uxShippingSortByLabel" CssClass="Label" />
                    <asp:DropDownList ID="uxShippingSoryByDrop" runat="server" CssClass="DropDown" />
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div>
                <vevo:AdvanceButton ID="uxExportShippingAddress" runat="server" meta:resourcekey="uxExportShippingAddress"
                    CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                    OnClick="uxExportShippingAddress_Click" OnClickGoTo="top" />
            </div>
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
