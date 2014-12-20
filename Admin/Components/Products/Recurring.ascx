<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Recurring.ascx.cs" Inherits="AdminAdvanced_Components_Products_Recurring" %>
<div>
    <asp:Panel ID="uxRecuringTR" runat="server" CssClass="ProductDetailsRowTitle mgt10">
        <asp:Label ID="uxRecurringHeader" runat="server" meta:resourcekey="lcRecurringHeader" />
    </asp:Panel>
    <div class="ProductDetailsRow">
        <asp:Label ID="uxIsRecurringLabel" runat="server" meta:resourcekey="lcIsRecuringLabel"
            CssClass="Label" />
        <asp:DropDownList ID="uxIsRecurringDrop" runat="server" CssClass="fl DropDown" AutoPostBack="true">
            <asp:ListItem Value="True">Yes</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Panel ID="uxRecurringDetailsPanel" runat="server" CssClass="ProductDetailsRow">
        <div class="ProductDetailsRow">
            <asp:Label ID="uxIntervalLabel" runat="server" meta:resourcekey="lcIntervalLabel"
                CssClass="Label" />
            <asp:TextBox ID="uxIntervalText" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:DropDownList ID="uxIntervalUnitDrop" runat="server" CssClass="fl DropDown mgl10">
                <asp:ListItem>Day</asp:ListItem>
                <asp:ListItem>Week</asp:ListItem>
                <asp:ListItem>Month</asp:ListItem>
                <asp:ListItem>Year</asp:ListItem>
            </asp:DropDownList>
            <div class="validator1 fl">
                <span class="Asterisk">*</span>
            </div>
            <asp:RequiredFieldValidator ID="uxIntervalRequired" runat="server" ControlToValidate="uxIntervalText"
                ValidationGroup="VaildProduct" Display="Dynamic">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Interval is required.
                <div class="CommonValidateDiv CommonValidateDivProductRecurring">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="uxIntervalCompare" runat="server" ControlToValidate="uxIntervalText"
                Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="VaildProduct">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Interval must be an Integer and greater than zero(0).
                <div class="CommonValidateDiv CommonValidateDivProductRecurring">
                </div>
            </asp:CompareValidator>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="uxIntervalNumberCycleLabel" runat="server" meta:resourcekey="lcIntervalNumberCycle"
                CssClass="Label" />
            <asp:TextBox ID="uxIntervalNumberCycleText" runat="server" CssClass="TextBox"></asp:TextBox>
            <div class="validator1 fl">
                <span class="Asterisk">* </span>(not including trial period)
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uxIntervalNumberCycleText"
                ValidationGroup="VaildProduct" Display="Dynamic">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Number of cycle is required.
                <div class="CommonValidateDiv CommonValidateDivProductRecurring">
                </div>
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="uxIntervalNumberCycleText"
                Operator="GreaterThan" ValueToCompare="0" Type="Integer" Display="Dynamic" ValidationGroup="VaildProduct">
                <img src="../Images/Design/Bullet/RequiredFillBullet.gif" /> Must be an Integer and greater than zero(0).
                <div class="CommonValidateDiv CommonValidateDivProductRecurring">
                </div>
            </asp:CompareValidator>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="uxTrialNumberCycleLabel" runat="server" meta:resourcekey="lcTrialNumberCycle"
                CssClass="Label" />
            <asp:TextBox ID="uxTrialNumberCycleText" runat="server" CssClass="TextBox"></asp:TextBox>
            <div class="validator1 fl">
                <span class="Asterisk">&nbsp;</span>
                <asp:CompareValidator ID="uxTrialNumberCycleCompare" runat="server" ErrorMessage="Recurring Number Cycles must be Integer."
                    Display="Dynamic" ControlToValidate="uxTrialNumberCycleText" Operator="DataTypeCheck"
                    Type="Integer" ValidationGroup="VaildProduct"><--</asp:CompareValidator>
            </div>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="uxTrialAmountLabel" runat="server" meta:resourcekey="lcTrialAmountLabel"
                CssClass="Label" />
            <asp:TextBox ID="uxTrialAmountText" runat="server" CssClass="TextBox"></asp:TextBox>
            <div class="validator1 fl">
                <span class="Asterisk">&nbsp;</span>
                <asp:CompareValidator ID="uxTrialAmountCompare" runat="server" ErrorMessage="Recurring Number Cycles must be Integer."
                    Display="Dynamic" ControlToValidate="uxTrialAmountText" Operator="DataTypeCheck"
                    Type="Currency" ValidationGroup="VaildProduct"><--</asp:CompareValidator>
            </div>
        </div>
        <div class="ProductDetailsRow">
            <asp:Label ID="Label1" runat="server" CssClass="Label">&nbsp;</asp:Label>
            <asp:Label ID="uxRecurringNoticeLabel" runat="server" meta:resourcekey="lcRecurringNoticeLabel"
                ForeColor="Red" />
            <div class="Clear">
            </div>
        </div>
    </asp:Panel>
    <div class="Clear">
    </div>
</div>
