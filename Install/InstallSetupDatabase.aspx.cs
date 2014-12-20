using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Shared.DataAccess.Tester;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Install_InstallSetupDatabase : System.Web.UI.Page
{
    #region Private

    private const string _sql_providerName = "System.Data.SqlClient";
    private const string _access_providerName = "System.Data.OleDb";

    private bool TestConnection( string connectionString, string providerName, out string errMessage )
    {
        DatabaseTester dbTest = new DatabaseTester( connectionString, providerName );
        return dbTest.TestConnection( out errMessage );
    }

    private void ConnectDatabase()
    {
        DatabaseConnectionService connectionService = new DatabaseConnectionService();
        connectionService.TryConnect();
    }

    private string ConnectionString( string databaseName, ConnectionString connectionString )
    {
        string newConnectionString;
        string tagname = StoreContext.ConnectionStringsTagName;
        if (ConvertUtilities.ToBoolean( uxIsSQLAuthenticationDrop.SelectedValue ))
            newConnectionString = connectionString.CreateConnectionStringSQLServerAuthenticationText(
                uxServerNameText.Text,
                databaseName,
                uxDatabaseUserNameText.Text,
                uxDatabasePassWordText.Text, tagname );
        else
            newConnectionString = connectionString.CreateConnectionStringWindowsAuthenticationText(
                uxServerNameText.Text, databaseName, tagname );

        return newConnectionString;
    }

    private string GetNextCommand( StreamReader reader )
    {
        StringBuilder sb = new StringBuilder();
        string textline = String.Empty; ;
        while (true)
        {
            textline = reader.ReadLine();
            if (textline == null)
            {
                if (sb.Length > 0)
                    return sb.ToString();
                else
                    return null;
            }

            if (textline.TrimEnd().ToUpper() == "GO")
                break;

            sb.Append( textline + Environment.NewLine );
        }
        return sb.ToString();
    }

    //private string GetRedirectQueryString()
    //{
    //    return "?DBOption=" + uxCreateDatabaseRadioButton.SelectedValue;
    //}

    private void ShowAccessPanel()
    {
        uxAccessPanel.Visible = true;
        uxSQLServerlPanel.Visible = false;
    }

    private void ShowSQLServerPanel()
    {
        uxAccessPanel.Visible = false;
        uxSQLServerlPanel.Visible = true;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        string providerName
            = ConfigurationManager.ConnectionStrings[StoreContext.ConnectionStringsTagName].ProviderName;
        if (providerName == _access_providerName)
        {
            ShowAccessPanel();
        }
        else if (providerName == _sql_providerName)
        {
            ShowSQLServerPanel();
        }
        else
        {
            throw new Exception( "Unknown database provider name" );
        }
    }

    protected void uxNextAccessButton_Click( object sender, EventArgs e )
    {
        ConnectionString connectionString = new ConnectionString();
        string newConnectionString
            = connectionString.CreateConnectionStringAccess( Server.MapPath( uxMSAccessPathText.Text ) );

        string errMessage;
        if (!TestConnection( newConnectionString, _access_providerName, out errMessage ))
        {
            uxMessage.DisplayError( Resources.InstallMessage.SetupTestDBConnectionError );
            return;
        }

        string filePath = "~/" + SystemConst.ConnectionStringAccessFilePath;
        bool writeFileSuccess = connectionString.WriteConnectionStringWithoutEncrypt(
            new FileManager(),
            filePath,
            uxMSAccessPathText.Text,
            _access_providerName,
            StoreContext.ConnectionStringsTagName,
            out errMessage );

        if (!writeFileSuccess)
        {
            uxMessage.DisplayError( Server.HtmlEncode( errMessage ) );
            return;
        }
        Response.Redirect( "setupconfig.aspx" );
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        ConnectionString connectionString = new ConnectionString();

        string newConnectionString;

        string databaseName = uxDatabaseNameText.Text;

        newConnectionString = ConnectionString( databaseName, connectionString );
        if ((databaseName.Length) > SystemConst.MaxConnectionStringLength)
        {
            uxMessage.DisplayError( Resources.InstallMessage.SetupConnectionStringError );
            return;
        }

        if (uxCreateDatabaseRadioButton.SelectedValue == "Create")
            newConnectionString = ConnectionString( "", connectionString );

        string errMessage;
        if (!TestConnection( newConnectionString, _sql_providerName, out errMessage ))
        {
            uxMessage.DisplayError( Resources.InstallMessage.SetupTestDBConnectionError );
            return;
        }

        try
        {
            string filePath = "~/" + SystemConst.ConnectionStringSQLFilePath;
            string sqlCommandFileName = "VevoCart_Database_SQL.sql";

            if (uxCreateDatabaseRadioButton.SelectedValue == "Create")
            {
                // 1. Create Database
                using (SqlConnection conn = new SqlConnection( newConnectionString ))
                {
                    conn.Open();
                    SqlCommand command
                        = new SqlCommand( String.Format( "CREATE DATABASE {0}", uxDatabaseNameText.Text ), conn );
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }

            newConnectionString = ConnectionString( uxDatabaseNameText.Text, connectionString );
            if (!TestConnection( newConnectionString, _sql_providerName, out errMessage ))
            {
                uxMessage.DisplayError( Resources.InstallMessage.SetupTestDBConnectionError );
                return;
            }

            // This command can cause the application to restart
            bool writeFileSuccess = connectionString.WriteConnectionStringWithoutEncrypt(
                new FileManager(),
                filePath,
                newConnectionString,
                _sql_providerName,
                StoreContext.ConnectionStringsTagName,
                out errMessage );

            if (!writeFileSuccess)
            {
                uxMessage.DisplayError( Server.HtmlEncode( errMessage ) );
                return;
            }

            // Connect the database if connection string is OK
            ConnectDatabase();

            if (uxCreateDatabaseRadioButton.SelectedValue == "Create"
                || uxCreateDatabaseRadioButton.SelectedValue == "Populate")
            {
                List<string> allExcuteCommands = new List<string>();
                using (Stream sourceStream
                    = File.OpenRead( Server.MapPath( "../App_Data/" + sqlCommandFileName ) ))
                using (StreamReader reader = new StreamReader( sourceStream ))
                {
                    string statement = string.Empty;
                    while ((statement = GetNextCommand( reader )) != null)
                    {
                        allExcuteCommands.Add( statement );
                    }
                }
                using (SqlConnection conn = new SqlConnection( newConnectionString ))
                {
                    conn.Open();
                    foreach (string commandString in allExcuteCommands)
                    {
                        SqlCommand command = new SqlCommand( commandString, conn );
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }

            if (KeyUtilities.IsMultistoreLicense())
            {
                CacheDependencyUtility cache = new CacheDependencyUtility();
                cache.CreateSqlCacheTableForNotifications( newConnectionString );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
            return;
        }

        Response.Redirect( "setupconfig.aspx" );
    }

    protected void uxIsSQLAuthenticationDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (ConvertUtilities.ToBoolean( uxIsSQLAuthenticationDrop.SelectedValue ))
            uxLoginDetailPanel.Visible = true;
        else
            uxLoginDetailPanel.Visible = false;
    }

    #endregion
}
