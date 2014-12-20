using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Shared.DataAccess.Tester;
using Vevo.Shared.SystemServices;
using Vevo.WebAppLib;
using Vevo.WebUI;


namespace Vevo
{
    /// <summary>
    /// Summary description for DatabaseSetupModule
    /// </summary>
    public class DatabaseSetupModule : IHttpModule
    {
        #region Private

        private static bool _isConnectionOK = false;

        private string GetConnectionString()
        {
            return DataAccessHelper.CreateConnectionStringFromPrefix(
                WebConfiguration.DBConnectionStringPrefix,
                ConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ConnectionString,
                ConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ProviderName,
                HttpRuntime.AppDomainAppPath );
        }

        private bool TestConnection( out string errorMessage )
        {
            DatabaseTester tester = new DatabaseTester(
                GetConnectionString(),
                ConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ProviderName );

            return tester.TestConnection( out errorMessage );
        }

        private bool IsRequestingUnitTestPath( HttpRequest request )
        {
            // Match the word "UnitTest" with no more subfolder under UnitTest
            Regex regex = new Regex( @"[/\\]unittest[/\\]?([^/\\]*)$", RegexOptions.IgnoreCase );

            return regex.IsMatch( request.Url.AbsolutePath );
        }

        private bool IsAtPage( HttpApplication app, string pageName )
        {
            return app.Request.Url.AbsolutePath.ToLower().Contains( pageName.ToLower() );
        }

        private void PaymentModuleDatabaseConnected()
        {
            try
            {
                PaymentModuleSetup paymentModule = new PaymentModuleSetup();
                paymentModule.ProcessDatabaseConnected();
            }
            catch (Exception ex)
            {
                SaveLogFile.SaveLog( ex );
                // This error may occur if database is not upgraded to the latest version yet.
                // The ProcessedDatabaseConnected function will be called again after
                // database is upgraded to the latest version.
            }
        }

        private bool TestConfiguration()
        {
            try
            {
                IList<Vevo.Domain.Configurations.Configuration> list = DataAccessContext.ConfigurationRepository.GetAll();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Protected

        protected virtual void DatabaseSetupModule_BeginRequest( object sender, EventArgs e )
        {
            HttpApplication app = (HttpApplication) sender;

            if (!_isConnectionOK)
            {
                DataAccessHelper.SetUp(
                    GetConnectionString(),
                    ConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ProviderName,
                    false,
                    true,
                    null,
                    WebConfiguration.SecretKey,
                    SystemConst.DefaultUrlCultureName,
                    HttpContext.Current.Server.MapPath( SystemConst.LicenseFilePath ) );

                string message;
                if (!TestConnection( out message ) &&
                    !IsAtPage( app, "SystemError.aspx" ) &&
                    !IsAtPage( app, "GenericError.aspx" ) &&
                    !IsRequestingUnitTestPath( app.Request ))
                {
                    string errorHeader = "Database Connection Error";
                    string errorText = "There is an error while attempting to connect to the database. Please verify your connection string in the file";
                    WebUtilities.LogError( new Exception( errorHeader + "\n\n" + errorText + "\n" + message ) );
                    SystemErrorPage.RedirectToErrorPage(
                        errorHeader,"<p>" + errorText + "</p>");
                    
                }

                PaymentModuleDatabaseConnected();

                _isConnectionOK = true;

                if (TestConfiguration())
                {
                    DataAccessContext.EnableConfigurations( true );
                    ConfigurationHelper.ApplyConfigurations();
                }
                else
                {
                    DataAccessContext.EnableConfigurations( false );
                }
            }
        }

        #endregion


        #region Public Methods

        public virtual void Init( HttpApplication app )
        {
            app.BeginRequest += new EventHandler( this.DatabaseSetupModule_BeginRequest );
        }

        public virtual void Dispose()
        {
        }

        #endregion

    }
}
