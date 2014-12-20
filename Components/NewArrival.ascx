<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewArrival.ascx.cs" Inherits="Components_NewArrival" %>
<%@ Register Src="~/Components/NewArrivalItem.ascx" TagName="NewArrivalItem" TagPrefix="uc1" %>
<%@ Import Namespace="Vevo" %>
<%@ Import Namespace="Vevo.Shared.Utilities" %>
<%@ Import Namespace="Vevo.WebUI" %>
<asp:Panel ID="uxNewArrivalPanel" runat="server" CssClass="NewArrival">
    <div class="CenterBlockTop">
        <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/RandomProductTopLeft.gif"
            runat="server" CssClass="CenterBlockTopImgLeft" />
        <asp:Label ID="uxRandomProductTitle" runat="server" Text="[$NewArrivalTitle]" CssClass="CenterBlockTopTitle" />
        <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/RandomProductTopRight.gif"
            runat="server" CssClass="CenterBlockTopImgRight" />
        <div class="Clear">
        </div>
    </div>
    <div class="CenterBlockLeft">
        <div class="CenterBlockRight">
            <asp:UpdatePanel ID="uxNewArrialUpdate" runat="server" UpdateMode="Conditional" 
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div id="uxNewArrialCarousel" runat="server" class="jcarousel-skin-tango">
                        <ul>
                            <asp:Repeater ID="uxNewArrialRepeater" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="NewArrivalItemStyle">
                                            <asp:Panel ID="uxNewArrivalLabel" runat="server" CssClass="NewArrivalLabel" />
                                            <uc1:NewArrivalItem ID="uxItem" runat="server" />
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="jcarousel-scroll">
                            <asp:LinkButton ID="uxPreviousPageButton" runat="server" CssClass="prev" Height="70"
                                Width="25" ToolTip="Arrow Prev" PostBackUrl="~/">
                                <asp:Image ID="uxPrevImage" runat="server" ImageUrl="~/Images/Icon/Prev.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="uxNextPageButton" runat="server" CssClass="next" Height="70"
                                Width="25" ToolTip="Arrow Next" PostBackUrl="~/">
                                <asp:Image ID="uxNextImage" runat="server" ImageUrl="~/Images/Icon/Next.png" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uxPreviousPageButton" />
                    <asp:AsyncPostBackTrigger ControlID="uxNextPageButton" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="CenterBlockBottom">
        <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/RandomProductBottomLeft.gif"
            runat="server" CssClass="CenterBlockBottomImgLeft" />
        <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/RandomProductBottomRight.gif"
            runat="server" CssClass="CenterBlockBottomImgRight" />
        <div class="Clear">
        </div>
    </div>
</asp:Panel>
