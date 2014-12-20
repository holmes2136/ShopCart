using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Users;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminBasePage
    /// </summary>
    public class AdminBasePage : Page
    {
        public AdminBasePage()
        {
            PreInit += new EventHandler( AdminBasePage_PreInit );
            Load += new EventHandler( AdminBasePage_Load );
        }

        private bool IsVerified
        {
            get
            {
                bool verified = false;

                if (Session["BaseLayoutIsVerified"] != null)
                    verified = (bool) Session["BaseLayoutIsVerified"];

                if (!verified)
                {
                    if (KeyUtilities.Verify( NamedConfig.DomainRegistrationKey ) ||
                        KeyUtilities.VerifyLicenseFile( Server.MapPath( SystemConst.LicenseFilePath ) ))
                    {
                        verified = true;
                    }
                    Session["BaseLayoutIsVerified"] = verified;
                }
                return verified;
            }
        }

        protected void AdminBasePage_PreInit( object sender, EventArgs e )
        {
            Page.Theme = "Admin";
        }

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo( AdminConfig.CurrentCultureName );

            base.InitializeCulture();
        }

        protected bool IsAdminModifiable()
        {
            AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
            return admin.CanModifyPage( Request.Url.AbsoluteUri );
        }

        public void RegisterNormalScriptInclude( string pathName, string scriptName )
        {
            String csname = scriptName;
            String csurl = pathName;
            Type cstype = this.GetType();

            //// Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            //// Check to see if the include script exists already.
            if (!cs.IsClientScriptIncludeRegistered( cstype, csname ))
            {
                cs.RegisterClientScriptInclude( cstype, csname, ResolveClientUrl( csurl ) );
            }
        }

        public void RegisterAdminScriptInclude( string fileName, string scriptName )
        {
            UrlPath urlPath = new UrlPath( HttpContext.Current.Request.Url.AbsoluteUri );
            RegisterNormalScriptInclude( String.Format( "~/{0}/{1}", urlPath.ExtractFirstApplicationSubfolder(), fileName ), scriptName );
        }

        public new void RegisterOnSubmitStatement(
            string key, string script )
        {

            ClientScript.RegisterOnSubmitStatement( typeof( Page ), key, script );

        }

        //[Obsolete]
        //public override void RegisterStartupScript( string key, string script )
        //{
        //    base.RegisterStartupScript( key, script );

        //}

        private void AdminBasePage_Load( object sender, EventArgs e )
        {
            if (!IsVerified)
                Response.Redirect( "~/KeyValidationError.aspx" );
        }

    }
}
