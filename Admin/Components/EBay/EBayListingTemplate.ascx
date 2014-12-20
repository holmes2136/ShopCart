<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayListingTemplate.ascx.cs"
    Inherits="AdminAdvanced_Components_EBay_EBayListingTemplate" %>
<%@ Register Src="~/Components/CalendarPopup.ascx" TagName="CalendarPopup" TagPrefix="uc6" %>
<div>
    <asp:Panel ID="uxListingTemplateTopPanel" runat="server" CssClass="ProductDetailsRowTitle mgt0">
        <asp:Label ID="lcListingTemplateTop" runat="server" meta:resourcekey="lcListingTemplateTop" />
    </asp:Panel>
    <div class="ProductDetailsRow">
        <asp:Label ID="lcSelectTemplate" runat="server" meta:resourcekey="lcSelectTemplate"
            CssClass="Label" />
        <asp:DropDownList ID="uxListingTemplateDrop" runat="server" CssClass="fl DropDown" >
        </asp:DropDownList>
        <div class="validator1 fl">
            <span class="Asterisk">*</span>
        </div>
    </div>
    <div>
        <div class="ProductDetailsRow">
            <asp:Label ID="lcSchedule" runat="server" meta:resourcekey="lcSchedule" CssClass="Label" />
            <div class="fl">
                <div>
                    <asp:RadioButton ID="uxListNowRadio" runat="server" Checked="true" AutoPostBack="true" 
                        GroupName="ScheduleTime" meta:resourcekey="uxListNowRadio"/>
                    <div class="Clear">
                    </div>
                </div>
                <div>
                    <asp:RadioButton ID="uxListOnRadio" runat="server" AutoPostBack="true" 
                        GroupName="ScheduleTime" meta:resourcekey="uxListOnRadio"/>
                    <asp:Panel ID="uxCalendarTD" runat="server" CssClass="validator1 fr">
                        <uc6:CalendarPopup ID="uxFixedDateCalendarPopup" runat="server" />
                        <span class="Asterisk">*</span>
                        <asp:DropDownList ID="uxHourDrop" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="uxMinDrop" runat="server">
                        </asp:DropDownList>
                    </asp:Panel>
                    <div class="Clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
