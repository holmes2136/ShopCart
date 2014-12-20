<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Help.ascx.cs" Inherits="AdminAdvanced_MainControls_Help" %>
<%@ Register Src="../Components/Template/AdminContent.ascx" TagName="AdminContent"
    TagPrefix="uc1" %>
<%@ Register Src="../Components/BoxSet/BoxSet.ascx" TagName="BoxSet" TagPrefix="uc1" %>
<uc1:AdminContent ID="uxAdminContentControl" runat="server" HeaderText="<%$ Resources:lcHeader %>">
    <ContentTemplate>
        <uc1:BoxSet ID="uxBoxSet" runat="server" CssClass="BoxSet1 b6 c4">
            <ContentTemplate>
                <div class="AnchorList ac">
                    <div class="AnchorList1Left">
                        <div class="AnchorList1Right">
                            <asp:HyperLink ID="uxQuickStartGuide" runat="server" Target="_blank" NavigateUrl="http://www.vevocart.com/Document/VevoCart_QuickStart_Guide.zip"
                                Text="Quick Start Guide"></asp:HyperLink></div>
                    </div>
                    <div class="AnchorList1Left">
                        <div class="AnchorList1Right">
                            <asp:HyperLink ID="uxUserManual" runat="server" Target="_blank" NavigateUrl="http://www.vevocart.com/Document/VevoCart_Manual.zip"
                                Text="User Manual"></asp:HyperLink></div>
                    </div>
                    <div class="AnchorList1Left">
                        <div class="AnchorList1Right">
                            <asp:HyperLink ID="uxInstallationLink" runat="server" Target="_blank" NavigateUrl="http://www.vevocart.com/Document/VevoCart_Installation_Guide.zip"
                                Text="Installation Guide"></asp:HyperLink></div>
                    </div>
                    <div class="AnchorList1Left">
                        <div class="AnchorList1Right">
                            <asp:HyperLink ID="uxDesignGuideLink" runat="server" Target="_blank" NavigateUrl="http://www.vevocart.com/Document/VevoCart_Design_Guide.zip"
                                Text="Design Guide"></asp:HyperLink></div>
                    </div>
                    <div class="AnchorList1Left">
                        <div class="AnchorList1Right">
                            <asp:HyperLink ID="uxForumLink" runat="server" Target="_blank" NavigateUrl="http://forum.vevocart.com/"
                                Text="Forum"></asp:HyperLink></div>
                    </div>
                </div>
            </ContentTemplate>
        </uc1:BoxSet>
    </ContentTemplate>
</uc1:AdminContent>
