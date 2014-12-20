<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectPaymentList.ascx.cs"
    Inherits="Admin_Components_Order_SelectPaymentList" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc1" %>
<asp:Panel ID="uxPaymentListPanel" runat="server">
    <uc1:Message ID="uxMessage" runat="server" NumberOfNewLines="2" FontSize="1em" />
    <div id="uxPaymentMethodMessageDiv" runat="server" visible="false">
        <img src="../Images/Design/Bullet/RequiredFillBullet_Down.gif" />
        <asp:Label ID="uxPaymentMethodMessageLabel" runat="server" ForeColor="Red"></asp:Label>
        <div class="CommonValidateDiv CommonValidateDivCheckoutPaymentMethod">
        </div>
    </div>
    <asp:DataList ID="uxPaymentList" CssClass="PaymentDataList" runat="server" DataKeyField="Name"
        OnItemDataBound="uxPaymentList_ItemDataBound">
        <ItemTemplate>
            <div class="CommonRowStyle" style='<%# (IsRecurring( Eval("Name") )? "display:block": "display:none") %>'>
                <div class="PaymentItemNameDiv">
                    <div class="PaymentItemOptionsDiv">
                        <asp:RadioButton ID="uxRadio" runat="server" Text='<%# Eval( "DisplayName" ) %>'
                            GroupName="PaymentGroup" OnDataBinding="uxRadio_DataBinding" Enabled='<%# IsRecurring( Eval("Name") ) %>'
                            CssClass="Radio" OnCheckedChanged="uxRadio_CheckedChanged" AutoPostBack="true" />
                        <asp:DropDownList ID="uxDrop" runat="server" Visible="false" CssClass="DropDown" />
                        <asp:HiddenField ID="uxPaymentNameHidden" runat="server" Value='<%# Eval("Name") %>' />
                        <div class="Clear">
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
            </div>
        </ItemTemplate>
        <ItemStyle CssClass="PaymentDataListItemStyle" />
    </asp:DataList>
    <asp:Panel ID="uxPOpanel" runat="server">
        <div class="CommonPageInnerTitle">
            Please input Purchase Order (PO) Number</div>
        <div class="BulletLabel">
            <asp:Label ID="uxPONumberLabel" runat="server" Text="PO Number : "></asp:Label>
            <asp:TextBox ID="uxPONumberText" runat="server" MaxLength="255" CssClass="CommonTextBox"></asp:TextBox>
            <asp:Label ID="uxPORequire" runat="server" Text=" *" ForeColor="Red"></asp:Label>
        </div>
    </asp:Panel>
</asp:Panel>
