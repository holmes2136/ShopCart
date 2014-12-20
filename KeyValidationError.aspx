<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeyValidationError.aspx.cs"
    Inherits="KeyValidationError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Key Validation Failure</title>
</head>
<body class="DomainKeyError">
    <form id="aspnetForm" runat="server">
        <asp:PlaceHolder ID="uxDomainKeyPlaceHolder" runat="server">
            <h3>
                Invalid Domain Key
            </h3>
            <p>
                Your Domain Key is not valid. You can obtain a new Domain Key as follows:
            </p>
            <div>
                <ul>
                    <li><strong><span style="color: #990000">Commercial Version license:</span></strong>
                        visit <a href="http://www.vevocart.com/DomainKeyFullSource.aspx">http://www.vevocart.com/DomainKeyFullSource.aspx
                        </a>.
                        <br />
                        Please note that you need a serial number (e.g. 123456789) that was emailed to you
                        when you <a href="http://www.vevocart.com/Products.aspx">purchase the product</a>.
                    </li>
                    <li><strong><span style="color: #990000">Free Version license:</span></strong> visit
                        <a href="http://www.vevocart.com/DomainKeyRequest.aspx">http://www.vevocart.com/DomainKeyRequest.aspx
                        </a>.
                        <br />
                        You may obtain a serial number by following the download link for <a href="http://www.vevocart.com/DownloadGreeting.aspx">
                            VevoCart Free Commercial Version here</a>.</li>
                </ul>
            </div>
            <div style="display: none;">
                <h3>
                    Enter Your Domain key
                </h3>
                <p>
                    Once you have requested the Domain Key, please enter it below. 
                </p>
                <p>
                    <label class="Label">Domain Key:</label>
                    <asp:TextBox ID="uxDomainKeyText" runat="server" Width="300px" />
                </p>
                <p>
                    <label class="Label">&nbsp;</label>
                    <asp:Button ID="uxUpdateButton" runat="server" Text="Submit" Width="150px" OnClick="uxUpdateButton_Click" />
                </p>
            </div>
            <div>
                <p>
                    Please find the following section in your web.config file and enter the correct
                    value for DomainRegistrationKey.
                    <br />
                    <br />
                </p>
                <code>
                    <pre style="font-style: italic; font-size: 1.1em;">    
    &lt;appSettings&gt;
        ...
        &lt;add key="DomainRegistrationKey" value="<strong>[Enter your Domain Key here]</strong>" /&gt;
        ...
    &lt;/appSettings&gt;
                </pre>
                </code>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="uxLicensePlaceHolder" runat="server">
            <h3 style="color: Black; margin-top: 25px; margin-bottom: 20px;">
                Invalid License
            </h3>
            <p>
                Your license is not valid. Please contact support@vevocart.com to obtain a proper
                license key.
            </p>
                    </asp:PlaceHolder><br />
        <asp:PlaceHolder ID="uxLinkRemovalKeyPlaceHolder" runat="server">
            <h3 style="color: Black; margin-top: 25px; margin-bottom: 20px;">
                Link removal license key is not valid.
            </h3>
            <p>
                Your Link Removal Key is not valid, expired, or not existing. Please contact support@vevocart.com to obtain a proper
                license key. 
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="uxHiddenVersion" runat="server">
            <p style="color: White; margin-top: 25px; margin-bottom: 20px;">
                <asp:Label ID="uxVersion" runat="server" Text=""></asp:Label>
            </p>
        </asp:PlaceHolder>
    </form>
</body>
</html>
