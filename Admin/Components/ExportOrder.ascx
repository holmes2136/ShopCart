<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportOrder.ascx.cs" Inherits="AdminAdvanced_Components_ExportOrder" %>
<%@ Register Src="../Components/Template/AdminUserControlContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../Components/SearchFilter/SearchFilter.ascx" TagName="ExportFilter"
    TagPrefix="uc2" %>
<%@ Register Src="StoreFilterDrop.ascx" TagName="StoreFilterDrop" TagPrefix="uc2" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server">
    <MessageTemplate>
        <uc1:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="ExportNotice">
            <asp:Label ID="uxMessageNotice" runat="server" meta:resourcekey="uxMessageNotice" CssClass="CommonTextTitle" />
        </div>
        <asp:Panel ID="uxStoreFilterPanel" CssClass="ExportFilterPanel" runat="server">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxStoreFilterLabel" runat="Server" meta:resourcekey="uxStoreFilterLabel" CssClass="Label" />
                <uc2:StoreFilterDrop ID="uxStoreFilterDrop" runat="server" FirstLineEnable="true"
                    DisplayLabel="false" />
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxSortByLabel" runat="Server" meta:resourcekey="uxSortByLabel" CssClass="Label" />
                <asp:DropDownList ID="uxSortByDrop" runat="server" CssClass="DropDown" />
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxProcessingStautsLabel" runat="server" meta:resourcekey="uxProcessingStautsLabel" CssClass="Label" />
                <asp:DropDownList ID="uxProcessedDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Selected="true" Value="ShowAll">(All Processing Status)</asp:ListItem>
                    <asp:ListItem Value="ShowTrue">Processed</asp:ListItem>
                    <asp:ListItem Value="ShowFalse">Unprocessed</asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <div class="ExportFilterDefault">
                <asp:Label ID="uxPaymentStatusLabel" runat="server" meta:resourcekey="uxPaymentStatusLabel" CssClass="Label" />
                <asp:DropDownList ID="uxPaymentDrop" runat="server" CssClass="DropDown">
                    <asp:ListItem Selected="true" Value="ShowAll">(All Payment Status)</asp:ListItem>
                    <asp:ListItem Value="ShowTrue">Payment Complete</asp:ListItem>
                    <asp:ListItem Value="ShowFalse">Not Complete</asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
                </div>
            </div>
        </div>
        <div class="ExportFilterPanel">
            <uc2:ExportFilter ID="uxExportFilter" runat="server" IsExportFilter="true" />
        </div>
        <div>
            <vevo:AdvanceButton ID="uxGenerateOrderItemButton" runat="server" meta:resourcekey="uxGenerateOrderItemButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenerateOrderItemButton_Click" OnClickGoTo="top" />
            <vevo:AdvanceButton ID="uxGenerateOrderButton" runat="server" meta:resourcekey="uxGenerateOrderButton"
                CssClassBegin="mgt10 mgl10 fr" CssClassEnd="Button1Right" CssClass="ButtonOrange"
                OnClick="uxGenerateOrderButton_Click" OnClickGoTo="top" />
        </div>
    </PlainContentTemplate>
</uc1:AdminContent>
