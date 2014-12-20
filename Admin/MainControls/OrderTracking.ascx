<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderTracking.ascx.cs"
    Inherits="AdminAdvanced_MainControls_OrderTracking" %>
<%@ Register Src="../Components/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Src="../Components/MerchantNote.ascx" TagName="MerchantNote" TagPrefix="uc2" %>
<%@ Register Src="../Components/CustomerTracking.ascx" TagName="CustomerTracking"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContent" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <MessageTemplate>
        <uc3:Message ID="uxMessage" runat="server" />
    </MessageTemplate>
    <PlainContentTemplate>
        <div class="CommonTitle">
            <asp:Label ID="lcOrderID" runat="server" meta:resourcekey="lcOrderID" />
            <%= CurrentOrderID %>
        </div>
    </PlainContentTemplate>
    <ContentTemplate>
        <div class="Container-Box">
            <div class="CommonRowStyle mgt10 mgb10">
                <asp:Label ID="lcSelectTask" runat="server" meta:resourcekey="lcSelectTask" CssClass="Label" />
                <asp:DropDownList ID="uxSelectTaskDrop" runat="server" AutoPostBack="true" CssClass="fl DropDown">
                    <asp:ListItem Value="0" Selected="True" meta:resourcekey="lcDropItem0"></asp:ListItem>
                    <asp:ListItem Value="1" meta:resourcekey="lcDropItem1"></asp:ListItem>
                    <asp:ListItem Value="2" meta:resourcekey="lcDropItem2"></asp:ListItem>
                </asp:DropDownList>
                <div class="Clear">
            </div>
            </div>

            <uc1:CustomerTracking ID="uxCustomerTracking" runat="server" Visible="false" MessageControlID="uxMessage" />
            <uc2:MerchantNote ID="uxMerchantNote" runat="server" Visible="false" MessageControlID="uxMessage" />
            <div class="CommonTextTitle1">
                <asp:Label ID="lcCustomerTracking" runat="server" meta:resourcekey="lcCustomerTracking"></asp:Label>
            </div>
            <div class="mgt5">
                <asp:GridView ID="uxCustomerTrackingGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                    EnableViewState="false" OnRowDataBound="uxGrid_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="TrackingID" HeaderText="<%$ Resources:OrdersTrackingFields, TrackingID %>"
                            SortExpression="TrackingID">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreatedDate" HeaderText="<%$ Resources:OrdersTrackingFields, CreatedDate %>"
                            SortExpression="CreatedDate">
                            <ItemStyle Width="140px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SenderName" HeaderText="<%$ Resources:OrdersTrackingFields, SenderName %>"
                            SortExpression="SenderName">
                            <ItemStyle CssClass="al pdl15" />
                            <HeaderStyle CssClass="al pdl15" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SenderEmail" HeaderText="<%$ Resources:OrdersTrackingFields, SenderEmail %>"
                            SortExpression="SenderEmail">
                            <ItemStyle CssClass="al pdl15" />
                            <HeaderStyle CssClass="al pdl15" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Message" HeaderText="<%$ Resources:OrdersTrackingFields, Message %>"
                            SortExpression="Message">
                            <ItemStyle Width="40%" CssClass="al pdl15" />
                            <HeaderStyle CssClass="al pdl15" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="There is no Customer Tracking message."></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div class="CommonTextTitle1 mgt10">
                <asp:Label ID="lcMerchantNotes" runat="server" meta:resourcekey="lcMerchantNotes"></asp:Label>
            </div>
            <div class="mgt5">
                <asp:GridView ID="uxMerchantNoteGrid" runat="server" CssClass="Gridview1" SkinID="DefaultGridView"
                    EnableViewState="False" OnRowDataBound="uxGrid_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NoteID" HeaderText="<%$Resources:OrdersNoteFields, NoteID %>"
                            SortExpression="NoteID">
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:OrdersNoteFields, CreatedDate %>"
                            SortExpression="CreatedDate">
                            <ItemStyle Width="160px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Author" HeaderText="<%$Resources:OrdersNoteFields, Author %>"
                            SortExpression="Author">
                            <ItemStyle Width="180px" CssClass="al pdl15" />
                            <HeaderStyle CssClass="al pdl15" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrderNote" HeaderText="<%$Resources:OrdersNoteFields, OrderNote %>"
                            SortExpression="OrderNote">
                            <HeaderStyle CssClass="al pdl15" />
                            <ItemStyle CssClass="al pdl15" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="uxEmptyMessageLabel" runat="server" Text="There is no Merchant Note message."></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</uc1:AdminContent>
