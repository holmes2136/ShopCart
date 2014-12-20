﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogCategoryItemList.ascx.cs" 
Inherits="Admin_Components_BlogCategoryItemList" %>
<asp:HiddenField ID="uxBlogCategoryIDHidden" runat="server" Value="" />
<table class="MultiStoreBackground">
    <tr>
        <td style="width: 230px">
            <div id="uxFromDiv" class="MultiStoreListBox">
                <div>
                    <asp:ListBox ID="uxBlogCategoryFromList" runat="server" SelectionMode="Multiple" CssClass="MultiStoreInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
        <td style="text-align: center; width: 60px;">
            <div>
                <vevo:AdvanceButton ID="uxFromButton" runat="server" CssClassBegin="AdminButton"
                    CssClassEnd="" CssClass="AdminButtonFrom CommonAdminButton" ShowText="false"
                    OnLoad="uxFromButton_Load" Width="20px" /><br />
            </div>
            <div>
                <vevo:AdvanceButton ID="uxToButton" runat="server" CssClassBegin="AdminButton" CssClassEnd=""
                    CssClass="AdminButtonTo CommonAdminButton" ShowText="false" OnLoad="uxToButton_Load" Width="20px" />
            </div>
        </td>
        <td style="width: 230px">
            <div id="uxToDiv" class="MultiStoreListBox">
                <div>
                    <asp:ListBox ID="uxBlogCategoryToList" runat="server" SelectionMode="Multiple" CssClass="MultiStoreInnerListBox">
                    </asp:ListBox>
                </div>
            </div>
        </td>
    </tr>
</table>