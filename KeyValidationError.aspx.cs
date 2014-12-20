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
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.WebAppLib;
using Vevo.WebUI;
using System.Reflection;
using Vevo.Shared.Domain.SystemServices;
using Vevo.Shared.SystemServices;
using Vevo.Shared.DataAccess;

public partial class KeyValidationError : System.Web.UI.Page
{
    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
             DataAccessContext.Configurations["DomainRegistrationKey"],
            uxDomainKeyText.Text.Trim() );

        HttpRuntime.UnloadAppDomain();

        Response.Redirect( Request.Url.ToString() );
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ((KeyUtilities.Verify( NamedConfig.DomainRegistrationKey ) ||
            KeyUtilities.VerifyLicenseFile( Server.MapPath( SystemConst.LicenseFilePath ) )) && (KeyUtilities.VerifyLinkRemoval( NamedConfig.DomainRegistrationKey )))
        {
            Response.Redirect( "Default.aspx" );
        }
        else
        {
            if (!KeyUtilities.VerifyLinkRemoval( NamedConfig.DomainRegistrationKey ))
            {
                uxDomainKeyPlaceHolder.Visible = false;
                uxLicensePlaceHolder.Visible = false;
                uxLinkRemovalKeyPlaceHolder.Visible = true;
            }
            else if (KeyUtilities.IsServerLicense())
            {
                uxDomainKeyPlaceHolder.Visible = false;
                uxLicensePlaceHolder.Visible = true;
                uxLinkRemovalKeyPlaceHolder.Visible = false;
            }
            else
            {
                uxDomainKeyPlaceHolder.Visible = true;
                uxLicensePlaceHolder.Visible = false;
                uxLinkRemovalKeyPlaceHolder.Visible = false;
            }
            IFileManager fileManager = new FileManager();
            string path = fileManager.GetFullPath( "~/Bin/VevoLib.dll" );
            Assembly assembly = Assembly.LoadFrom( path );
            Version ver = assembly.GetName().Version;

            uxVersion.Text = SystemConst.CurrentVevoCartVersionNumber() + "<br/> Assembly version: " + ver.ToString();
        }
    }

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }


}
