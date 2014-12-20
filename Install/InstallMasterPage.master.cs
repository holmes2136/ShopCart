using System;
using System.Text;
using System.Web.UI;
using Vevo;
using Vevo.Domain;

public partial class Install_InstallMasterPage : System.Web.UI.MasterPage
{
    #region Private

    private void RegisterJavaScript()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "$().ready(function() {webappPrepare;});" );
        sb.AppendLine( "window.onload = webappPrepare;" );
        sb.AppendLine( "window.onresize = webappPrepare;" );
        sb.AppendLine( "function webappPrepare(){" );
        sb.AppendLine( "    $('.WizardContentBox').height($(window).height()-120);" );
        sb.AppendLine( "    $('.WizardContentBox').width($(window).width()-70);" );
        sb.AppendLine( "    if ( $(window).width() < 620 )" );
        sb.AppendLine( "        $('WizardContentBox').css('overflow-x', 'auto');" );
        sb.AppendLine( "    else $('WizardContentBox').css('overflow-x', 'hidden');}" );
        this.Page.ClientScript.RegisterStartupScript( typeof( Page ), "staticfunction", sb.ToString(), true );
    }

    #endregion


    #region Protected

    protected void Page_Init( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            // Clear configuration every time a page is loaded for Installation Wizard. 
            // This is in case that the customer has changed the PaymentSettings.config.
            DataAccessContext.ClearConfigurationCache();

            if (DataAccessContext.ApplicationSettings.InstallCompleted)
            {
                Response.Redirect( "../Default.aspx" );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxScriptManager.Scripts.Add( new ScriptReference( "~/ClientScripts/JQuery/jquery-1.8.1.min.js" ) );
        RegisterJavaScript();

        if (KeyUtilities.IsTrialLicense())
            uxTrialWarningPlaceHolder.Visible = true;
        else
            uxTrialWarningPlaceHolder.Visible = false;
    }

    #endregion
}
