<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBayShowFee.ascx.cs" Inherits="AdminAdvanced_Components_EBay_EBayShowFee" %>
<%@ Register Src="../Message.ascx" TagName="Message" TagPrefix="uc3" %>
<div>
    <asp:Panel ID="uxShowFeeTop" runat="server" CssClass="ProductDetailsRowTitle mgt0">
        <asp:Label ID="lcShowFee" runat="server" meta:resourcekey="lcShowFee" />
    </asp:Panel>
    <div>
        <div class="EBayListingProcessRow">
            <div class="fl">
                <asp:GridView ID="uxFeeDetailGrid" runat="server" CssClass="EBayGridview1" SkinID="DefaultGridView"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="feeName" HeaderText="Fee detail" />
                        <asp:BoundField DataField="feeCost" HeaderText="Cost">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="EBayListingProcessRow">
            <asp:Label ID="uxWarningText" runat="server" meta:resourcekey="lcWarningText" ForeColor="Blue" />
        </div>
    </div>
</div>
