<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="GenericError.aspx.cs"
    Inherits="GenericError" Title="[$Title]" %>

<asp:Content ID="uxContent" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="GenericError">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">Sorry!</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="GenericErrorContent">
                        <div class="GenericErrorContentDiv">
                            We were unable to process your request. 
                        </div>
                        <div class="GenericErrorContentDiv">
                            Please try again.
                        </div>
                        <div>
                            <ul>
                                <li>
                                    <asp:HyperLink ID="uxGoHome" runat="server" Text="Go to the Home Page." NavigateUrl="~/Default.aspx"></asp:HyperLink></li>
                                <li>
                                    <asp:HyperLink ID="uxBack" runat="server" Text="Go Back"></asp:HyperLink>
                                    to the previous page.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div class="CommonPageBottom">
                <asp:Image ID="uxBottomLeft" ImageUrl="~/Images/Design/Box/DefaultBoxBottomLeft.gif"
                    runat="server" CssClass="CommonPageBottomImgLeft" />
                <asp:Image ID="uxBottomRight" ImageUrl="~/Images/Design/Box/DefaultBoxBottomRight.gif"
                    runat="server" CssClass="CommonPageBottomImgRight" />
                <div class="Clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
