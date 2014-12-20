<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="RmaDetail.aspx.cs"
    Inherits="RmaDetail" Title="[$Title]" %>

<asp:Content ID="Content1" ContentPlaceHolderID="uxPlaceHolder" runat="Server">
    <div class="RmaDetail">
        <div class="CommonPage">
            <div class="CommonPageTop">
                <asp:Image ID="uxTopLeft" ImageUrl="~/Images/Design/Box/DefaultBoxTopLeft.gif" runat="server"
                    CssClass="CommonPageTopImgLeft" />
                <asp:Label ID="uxDefaultTitle" runat="server" CssClass="CommonPageTopTitle">[$Head]</asp:Label>
                <asp:Image ID="uxTopRight" ImageUrl="~/Images/Design/Box/DefaultBoxTopRight.gif"
                    runat="server" CssClass="CommonPageTopImgRight" />
                <div class="Clear">
                </div>
            </div>
            <div class="CommonPageLeft">
                <div class="CommonPageRight">
                    <div class="RmaDetailDiv">
                        <div class="CommonFormLabel">
                            [$RmaID]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxRmaIDLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$OrderID]</div>
                        <div class="CommonFormData">
                            <asp:LinkButton ID="uxOrderIDLink" runat="server" CssClass="RmaDetailOrderIDLink" />
                        </div>
                        <div class="CommonFormLabel">
                            [$ProductName]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxProductNameLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$Quantity]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxQuantityLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$RequestDate]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxRequestDateLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$ReturnReason]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxReturnReasonLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$RmaAction]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxRmaActionLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$RequestStatus]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxRequestStatusLabel" runat="server" />
                        </div>
                        <div class="CommonFormLabel">
                            [$RmaNote]</div>
                        <div class="CommonFormData">
                            <asp:Label ID="uxRmaNoteLabel" runat="server" />
                        </div>
                        
                        
                        <div class="Clear">
                        </div>
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
