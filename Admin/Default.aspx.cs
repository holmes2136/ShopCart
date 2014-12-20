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
using System.Web.Configuration;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Payments;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using nStuff.UpdateControls;
using Vevo.eBay;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_Default : AdminAdvancedBasePage
{
    private void LoadUserControl()
    {
        if (String.IsNullOrEmpty( MainContext.LastControl ))
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );
        else
            MainContext.LoadMainControl();
    }

    private void SSLVerifyAndRediect()
    {
        UrlPath urlPath = new UrlPath( Request.Url.AbsoluteUri );
        if (!urlPath.IsSslEnabled())
        {
            Response.Redirect( urlPath.CreateSslUrl() );
        }
    }

    private void ExtractFragmentData( string fragment, out string mainControl, out string queryString )
    {
        if (String.IsNullOrEmpty( fragment ))
        {
            mainControl = String.Empty;
            queryString = String.Empty;
        }
        else
        {
            string[] urlSplit = fragment.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

            mainControl = urlSplit[0] + ".ascx";

            if (urlSplit.Length == 1 ||
                (urlSplit.Length > 1 && String.IsNullOrEmpty( urlSplit[1] )))
            {
                queryString = String.Empty;
            }
            else
            {
                queryString = urlSplit[1];
            }
        }
    }

    private void UpdateAdminAdvancedFolder()
    {
        UrlPath adminPath = new UrlPath( Request.Url.AbsoluteUri );
        if (DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ).ToLower() != adminPath.ExtractFirstApplicationSubfolder().ToLower())
        {
            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["AdminAdvancedFolder"],
                adminPath.ExtractFirstApplicationSubfolder() );
            SystemConfig.Load();
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPaymentHyperLink.NavigateUrl = PaymentAppGateway.GetPaymentAppUrl( "/", UrlPath.StorefrontUrl );
        if (!WebConfiguration.AdminSSLDisabled &&
            DataAccessContext.Configurations.GetBoolValue( "EnableAdminSSL" ) == true)
        {
            SSLVerifyAndRediect();
        }
        UpdateAdminAdvancedFolder();

        uxScriptManager.Scripts.Add( new ScriptReference( "~/ClientScripts/JQuery/jquery-1.8.1.min.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/jquery.simplemodal-1.3.3.min.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/jquery.simpletip-1.3.1.pack.js" ) );

        uxScriptManager.Scripts.Add( new ScriptReference( "~/ClientScripts/JqueryUI/jquery-ui-1.9.1.custom.min.js" ) );

        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/Report.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/OpenFlashChart/swfobject.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/AdvanceAdminScript.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "ClientScripts/print.js" ) );

        uxScriptManager.Scripts.Add( new ScriptReference( "~/Components/Upload/Script/handlers.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "~/Components/Upload/Script/fileprogress.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "~/Components/Upload/Script/swfupload.queue.js" ) );
        uxScriptManager.Scripts.Add( new ScriptReference( "~/Components/Upload/swfupload/swfupload.js" ) );


        uxScriptManager.Scripts.Add( new ScriptReference( "~/ClientScripts/controls.js" ) );

        LoadUserControl();

        ConfigAjaxAsyncPostBackError();

        if (KeyUtilities.IsTrialLicense())
            uxTrialWarningPlaceHolder.Visible = true;
        else
            uxTrialWarningPlaceHolder.Visible = false;

        if (!MainContext.IsPostBack)
        {
            EBayOrderUpdater ebayOrderUpdater = new EBayOrderUpdater();
            ebayOrderUpdater.CreateUpdateOrdersFromEBayOrder();

            uxPaymentLink.Visible = DataAccessContext.Configurations.GetBoolValue( "VevoPayPADSSMode" );
            uxUpgradeLink.Visible = !KeyUtilities.IsDeluxeLicense(DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName);
        }
    }

    protected void OnUpdateHistoryNavigate( object sender, nStuff.UpdateControls.HistoryEventArgs e )
    {
        if (!String.IsNullOrEmpty( e.EntryName ))
        {
            string rawUrl = e.EntryName;

            string mainControl, queryString;
            ExtractFragmentData( rawUrl, out mainControl, out queryString );

            MainContext.RedirectMainControl( mainControl, queryString );
        }
        else
        {
            MainContext.RedirectMainControl( "Default.ascx" );
        }

        uxContentUpdatePanel.Update();
    }
    //ValidateRequest="false" fix "Sys.WebForms.PageRequestManagerServerErrorException: An unknown error occurred while processing the request on the server. The status code returned from the server was: 500"
    //Some page use input html tag in box

    //ViewStateEncryptionMode="Never" ifx "ViewState MAC Faield"

    private void ConfigAjaxAsyncPostBackError()
    {
        if (DataAccessContext.Configurations.GetBoolValueNoThrow( "ShowDetailAjaxErrorMessage" ))
        {
            uxScriptManager.AsyncPostBackError += uxScriptManager_AsyncPostBackError;
        }
    }

    protected void uxScriptManager_AsyncPostBackError( object sender, AsyncPostBackErrorEventArgs e )
    {
        uxScriptManager.AsyncPostBackErrorMessage =
            String.Format( "Exception Type: {0}\n\nMessage: {1}\n\nMethod: {2}\n\nStack Trace:\n{3}",
                e.Exception.GetType().ToString(),
                e.Exception.Message,
                e.Exception.TargetSite,
                e.Exception.StackTrace );
    }
}
