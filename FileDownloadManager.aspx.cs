using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;
using Vevo.Domain;
using Vevo;
using Vevo.WebUI;
using Vevo.Domain.Orders;
using Vevo.Shared.WebUI;
using System.Text;

public partial class FileDownloadManager : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string ProductID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["ProductID"] ))
                return "0";
            else
                return Request.QueryString["ProductID"];
        }
    }

    private string Key
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["Key"] ))
                return "0";
            else
                return Request.QueryString["Key"];
        }
    }

    private string OrderItemID
    {
        get
        {
            if (String.IsNullOrEmpty( Request.QueryString["OrderItemID"] ))
                return "0";
            else
                return Request.QueryString["OrderItemID"];
        }
    }

    private bool IsAnonymousUser()
    {
        string errorMessage;
        DownloadKey.VerifyStatus status = VerifyDownloadInfoAnonymous( out errorMessage );

        if (status != DownloadKey.VerifyStatus.InvalidUser)
            return true;
        else
            return false;
    }

    private DownloadKey.VerifyStatus VerifyDownloadInfoAnonymous( out string errorMessage )
    {
        DownloadKey downloadKey = new DownloadKey( ProductID, SystemConst.AnonymousUser, OrderItemID );

        if (VerifyDownloadInfoAnonymous( out errorMessage, SystemConst.AnonymousUser ) != DownloadKey.VerifyStatus.InvalidUser)
        {
            return VerifyDownloadInfoAnonymous( out errorMessage, SystemConst.AnonymousUser );
        }
        else
        {
            return VerifyDownloadInfoAnonymous( out errorMessage, SystemConst.UnknownUser );
        }

    }

    private DownloadKey.VerifyStatus VerifyDownloadInfoAnonymous( out string errorMessage, string userName )
    {
        DownloadKey downloadKey = new DownloadKey( ProductID, userName, OrderItemID );
        return downloadKey.VerifyKey( Key, out errorMessage );
    }

    private DownloadKey.VerifyStatus VerifyDownloadInfo( out string errorMessage )
    {
        errorMessage = String.Empty;
        if (Roles.IsUserInRole( "Customers" ))
        {
            DownloadKey downloadKey = new DownloadKey( ProductID, Page.User.Identity.Name, OrderItemID );
            return downloadKey.VerifyKey( Key, out errorMessage );
        }
        else
        {
            return DownloadKey.VerifyStatus.InvalidUser;
        }
    }

    private bool IsCorrectLogin()
    {
        string errorMessage;
        if (VerifyDownloadInfoAnonymous( out errorMessage ) == DownloadKey.VerifyStatus.OK)
            return true;

        if (VerifyDownloadInfoAnonymous( out errorMessage ) == DownloadKey.VerifyStatus.Expired)
            return true;

        if (VerifyDownloadInfo( out errorMessage ) == DownloadKey.VerifyStatus.InvalidUser)
            return false;
        else
            return true;
    }

    private bool VerifyDownloadParameters( out string errorMessage )
    {
        if (IsAnonymousUser())
            return VerifyDownloadInfoAnonymous( out errorMessage )
                == DownloadKey.VerifyStatus.OK;
        else
            return VerifyDownloadInfo( out errorMessage )
                == DownloadKey.VerifyStatus.OK;
    }

    private string GetDownloadRemainind()
    {
        if (OrderItemID == "0")
            return "0";

        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( OrderItemID );
        int remaining
            = DataAccessContext.Configurations.GetIntValue( "NumberOfDownloadCount" ) - orderItem.DownloadCount;

        if (remaining < 0)
            remaining = 0;

        return remaining.ToString();
    }


    private void RegisterJavaScript()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine( "var start=new Date();" );
        sb.AppendLine( "start=Date.parse(start)/1000;" );
        sb.AppendLine( "var counts=0;" );
        sb.AppendLine( "function CountDown()" );
        sb.AppendLine( "{" );
        sb.AppendLine( "var now=new Date();" );
        sb.AppendLine( "now=Date.parse(now)/1000;" );
        sb.AppendLine( "var x=parseInt(counts-(now-start),10);" );
        sb.AppendLine( "document.getElementById('" + uxDownloadLink.ClientID + "').Enable = 'False';" );
        sb.AppendLine( "if(x>=0)" );
        sb.AppendLine( "{" );
        sb.AppendLine( "timerID=setTimeout('CountDown()', 100);" );
        sb.AppendLine( "}" );
        sb.AppendLine( "else" );
        sb.AppendLine( "{" );
        sb.AppendLine( "document.getElementById('" + uxDownloadLink.ClientID + "').Enable = 'True';" );
        sb.AppendLine( "}" );
        sb.AppendLine( "}" );

        Page.ClientScript.RegisterStartupScript( typeof( Page ), "CountTimerScript", sb.ToString(), true );
    }

    private void StartDownload()
    {
        HtmlGenericControl Keywords = new HtmlGenericControl( "meta" );
        Keywords.Attributes.Add( "http-equiv", "refresh" );
        Keywords.Attributes.Add( "content", "5;url=FileDownload.aspx?ProductID=" +
                                HttpContext.Current.Server.UrlEncode( ProductID ) +
                                "&OrderItemID=" + HttpContext.Current.Server.UrlEncode( OrderItemID ) +
                                "&Key=" + HttpContext.Current.Server.UrlEncode( Key ) );
        this.Page.Header.Controls.Add( Keywords );
        RegisterJavaScript();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsCorrectLogin())
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

        string errorMessage;
        if (VerifyDownloadParameters( out errorMessage ))
        {
            uxDefaultTitle.Text = "Download Link";
            uxDownloadMessageLabel.Text
                = "<br/>Please wait your download should start automatically<br/>" +
                  "If you are not start download within 10 seconds,";

            if (DataAccessContext.Configurations.GetBoolValue( "IsUnlimitDownload" ) || OrderItemID == "0")
                uxRemainingMessageLabel.Text = String.Empty;
            else
                uxRemainingMessageLabel.Text = String.Format(
                    "You have {0} download times remaining.", GetDownloadRemainind() );

            StartDownload();

            uxDownloadLink.NavigateUrl
                = "FileDownload.aspx?ProductID=" + HttpContext.Current.Server.UrlEncode( ProductID )
                                + "&OrderItemID=" + HttpContext.Current.Server.UrlEncode( OrderItemID )
                                + "&Key=" + HttpContext.Current.Server.UrlEncode( Key );
        }
        else
        {
            uxDefaultTitle.Text = "Download Failed";
            uxMessageLabel.Text = errorMessage;
            uxRemainingMessageLabel.Visible = false;
            uxDownloadLink.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

}
