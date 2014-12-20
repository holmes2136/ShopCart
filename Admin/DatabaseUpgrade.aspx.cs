using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Widget;

public partial class AdminAdvanced_DatabaseUpgrade : System.Web.UI.Page
{
    private void DisplatError( string message )
    {
        uxMessageLabel.ForeColor = System.Drawing.Color.Red;
        uxMessageLabel.Text = "<strong>Error : </storng>" + message;
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageLabel.Text = String.Empty;
    }

    protected void uxReadfileButton_Click( object sender, EventArgs e )
    {
        string pathFile = uxPathFileText.Text;

        try
        {
            using (StreamReader readText = File.OpenText( Server.MapPath( "~/App_Data/" + pathFile ) ))
            {
                string strExecute = readText.ReadToEnd();
                uxExecuteText.Text = strExecute;
            }
        }
        catch (Exception ex)
        {
            DisplatError( ex.Message );
        }
    }

    protected void uxExecuteButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (uxExecuteText.Text != "")
            {
                AdminUtilities.RemoveAllCacheInMemory();

                DatabaseConverter databaseConverter = new DatabaseConverter();

                databaseConverter.OnScriptExecuting();
                DataAccess.ExecuteNonQueryNoParameter( uxExecuteText.Text.Trim() );
                databaseConverter.OnScriptExecuted();

                // Update configurations
                DataAccessContext.ClearConfigurationCache();
                ConfigurationHelper.ApplyConfigurations();

                databaseConverter.Convert();

                AdminUtilities.RemoveAllCacheInMemory();

                // Set up PaymentModule
          //      PaymentModuleSetup paymentModule = new PaymentModuleSetup();
          //      paymentModule.ProcessDatabaseConnected();

                uxMessageLabel.ForeColor = System.Drawing.Color.Green;
                uxMessageLabel.Text = "<strong>Upgrade Completed</storng>";
            }
            else
            {
                DisplatError( "No command to execute." );
            }
        }
        catch (Exception ex)
        {
            DisplatError( ex.Message );
        }
    }

    protected void uxUpdateConfigOnlyButton_Click( object sender, EventArgs e )
    {
        WidgetDirector widgetDirector = new WidgetDirector();
        SystemConfig.UpdateNewConfigValue( widgetDirector .WidgetConfigurationCollection);
        DatabaseConverter convert = new DatabaseConverter();
        convert.UpdateStoreConfigurations();
        AdminUtilities.ClearAllCache();
        uxMessageLabel.ForeColor = System.Drawing.Color.Green;
        uxMessageLabel.Text = "<strong>Update Config Completed</storng>";
    }
}
