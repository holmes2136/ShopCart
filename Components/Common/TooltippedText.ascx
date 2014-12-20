<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TooltippedText.ascx.cs"
    Inherits="Components_Common_TooltippedText" %>
<div id="uxProductItemParentDiv" runat="server" class="ProductRecurringNotice">
    <%= GetMainText() %>
    <div id="uxProductItemDiv" runat="server" class="hidecallout" style="">
        <div class="shadow">
            <div class="content">
                <%= GetTooltipText() %>
            </div>
        </div>
    </div>
</div>
