<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponCondition.ascx.cs"
    Inherits="AdminAdvanced_Components_CouponCondition" %>
<div class="CommonTextTitle">
    <asp:Label ID="uxConditionTitleLabel" runat="server" Text="This coupon apply to "></asp:Label>
</div>
<div style="border: dashed 1px #C1C1C1;" class="pd10">
    <div class="OrderEditRowTitle">
        <asp:Label ID="uxByProductLabel" runat="server" Text="Products"></asp:Label>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxAllProductRadio" runat="server" Text="All Products" GroupName="ByProducts"
                Checked="true" />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxProductIDRadio" runat="server" Text="Product IDs" GroupName="ByProducts" /></div>
        <asp:TextBox ID="uxProductIDText" runat="server" Width="250" MaxLength="255" CssClass="TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxCategoryIDRadio" runat="server" Text="Category IDs" GroupName="ByProducts" /></div>
        <asp:TextBox ID="uxCategoryText" runat="server" Width="250" MaxLength="255" CssClass="TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            &nbsp;
        </div>
        <asp:Label ID="uxProductExamLabel" runat="server" Text="(Ex : 1,2,3,...)" CssClass="fl it"></asp:Label>
        <div class="Clear">
        </div>
    </div>
</div>
<div style="border: dashed 1px #C1C1C1; padding: 10px;" class="mgt10">
    <div class="OrderEditRowTitle">
        <asp:Label ID="uxByCustomerLabel" runat="server" Text="Customers"></asp:Label>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxAllCustomerRadio" runat="server" Text="All Customers" GroupName="ByCustomers"
                Checked="true" CssClass="fl" /></div>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxCustomerIDRadio" runat="server" Text="UserNames" GroupName="ByCustomers" /></div>
        <asp:TextBox ID="uxCustomerNameText" runat="server" Width="250" MaxLength="255" CssClass="TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            &nbsp;</div>
        <asp:Label ID="Label1" runat="server" Text="(Ex : User1,User2,User3,...)" CssClass="fl it"></asp:Label>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            <asp:RadioButton ID="uxCustomerOnceOnly" runat="server" Text="UserNames (One time usage)"
                GroupName="ByCustomers" /></div>
        <asp:TextBox ID="uxCustomerOnceOnlyText" runat="server" Width="250" MaxLength="255"
            CssClass="TextBox"></asp:TextBox>
        <div class="Clear">
        </div>
    </div>
    <div class="CommonRowStyle">
        <div class="Label">
            &nbsp;</div>
        <asp:Label ID="Label2" runat="server" Text="(Ex : User1,User2,User3,...)" CssClass="fl it"></asp:Label>
        <div class="Clear">
        </div>
    </div>
    </div>
