using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Shared.DataAccess.Tester;
using Vevo.WebAppLib;

public partial class DatabaseTesterPage : System.Web.UI.Page
{
    private bool IsTestConfigFile()
    {
        if (uxFileTestDrop.SelectedValue == "Custom")
            return false;
        else
            return true;
    }

    private string AlertMessage( string message )
    {
        return String.Format( "<script type=\"text/javascript\">alert('{0}');</script>", message );
    }

    private string DisplayErrorMessage( string message )
    {
        StringBuilder sb = new StringBuilder();
        sb.Append( "<strong style=\"color: red\">The test has failed!<br/>Error Message</strong><br/><br/>" );
        sb.Append( WebUtilities.ReplaceNewLine( message ) );
        return sb.ToString();
    }

    private DatabaseTester CreateConnectionString()
    {
        DatabaseTester dbTest;
        if (IsTestConfigFile())
        {
            if (WebConfiguration.DbProviderName == "System.Data.OleDb")
            {
                dbTest = NewConnectionString(
                    WebConfiguration.DBConnectionStringPrefix, WebConfiguration.DbConnectionString,
                    WebConfiguration.DbProviderName );
            }
            else
            {
                dbTest = NewConnectionString(
                    WebConfiguration.DbConnectionString,
                    WebConfiguration.DbProviderName );
            }
        }
        else
        {
            if (uxProviderDrop.SelectedValue == "System.Data.OleDb")
            {
                dbTest = NewConnectionString(
                    uxConnectionStringPrefixDIV.InnerText, uxConnectionStringText.Text,
                    uxProviderDrop.SelectedValue );
            }
            else
            {
                dbTest = NewConnectionString(
                    uxConnectionStringText.Text,
                    uxProviderDrop.SelectedValue );
            }
        }
        return dbTest;
    }

    private DatabaseTester NewConnectionString(
        string connectionString, string provider )
    {
        return new DatabaseTester( connectionString, provider );
    }

    private DatabaseTester NewConnectionString(
        string connectionStringPrefix, string connectionString, string provider )
    {
        return new DatabaseTester( connectionStringPrefix, connectionString, provider, HttpRuntime.AppDomainAppPath );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxFileTestDrop.Attributes.Add( "onchange", "if( this.selectedIndex == 0 ){" +
            "document.getElementById('" + uxConnectionStringText.ClientID + "').disabled = true;" +
            "document.getElementById('" + uxProviderDrop.ClientID + "').disabled = true;}" +
            "else{" +
            "document.getElementById('" + uxConnectionStringText.ClientID + "').disabled = false;" +
            "document.getElementById('" + uxProviderDrop.ClientID + "').disabled = false;}" );

        uxProviderDrop.Attributes.Add( "onchange", "if(this.selectedIndex == 0){" +
            "document.getElementById('" + uxConnectionStringPrefixDIV.ClientID +
            "').innerHTML = '" + WebConfiguration.DBConnectionStringPrefix + "';}" +
            "else{" +
            "document.getElementById('" + uxConnectionStringPrefixDIV.ClientID + "').innerHTML = ''};}" );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (uxFileTestDrop.SelectedIndex == 0)
        {
            uxConnectionStringText.Enabled = false;
            uxProviderDrop.Enabled = false;
        }
        else
        {
            uxConnectionStringText.Enabled = true;
            uxProviderDrop.Enabled = true;
        }

        if (uxProviderDrop.SelectedIndex == 0)
            uxConnectionStringPrefixDIV.InnerText = WebConfiguration.DBConnectionStringPrefix;
        else
            uxConnectionStringPrefixDIV.InnerText = "";
    }

    protected void uxTestConnectionStringButton_Click( object sender, EventArgs e )
    {
        try
        {
            DatabaseTester dbTest = CreateConnectionString();
            string message;
            if (dbTest.TestConnection( out message ))
                uxResultLiteral.Text = AlertMessage( "Test ConnectionString Successful" );
            else
                uxResultLiteral.Text = DisplayErrorMessage( message );
        }
        catch (Exception ex)
        {
            uxResultLiteral.Text = DisplayErrorMessage( ex.Message );
        }
    }

    protected void uxTestWriteButton_Click( object sender, EventArgs e )
    {
        try
        {
            DatabaseTester dbTest = CreateConnectionString();
            string message;
            if (dbTest.TestCreateTable( "LanguageText", out message ))
                uxResultLiteral.Text = AlertMessage( "Test Write Permission Successful" );
            else
                uxResultLiteral.Text = DisplayErrorMessage( message );
        }
        catch (Exception ex)
        {
            uxResultLiteral.Text = DisplayErrorMessage( ex.Message );
        }
    }

    protected void uxTestReadButton_Click( object sender, EventArgs e )
    {
        try
        {
            DatabaseTester dbTest = CreateConnectionString();
            string message;
            if (dbTest.TestSelectCommand( "LanguageText", out message ))
                uxResultLiteral.Text = AlertMessage( "Test Read Permission Successful" );
            else
                uxResultLiteral.Text = DisplayErrorMessage( message );
        }
        catch (Exception ex)
        {
            uxResultLiteral.Text = DisplayErrorMessage( ex.Message );
        }
    }
}
