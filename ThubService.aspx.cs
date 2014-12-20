using System;
using System.Web;
using System.Web.Util;
using System.Web.SessionState;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using System.Web.Mail;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Reflection;
using System.Security.Principal;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.WebAppLib;
using Vevo.Deluxe.WebUI.QuickBooks;

public partial class Admin_ThubService : Vevo.AdminBasePage
{
    private void logging( string str )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "ThubLogEnabled" ))
        {
            StreamWriter streamWriter = null;
            string path = Server.MapPath( "logs/" );
            string filename = "ThubService_" + DateTime.Today.Month + "_" + DateTime.Today.Day +
                "_" + DateTime.Today.Year + "_log.txt";

            streamWriter = File.AppendText( path + filename );
            streamWriter.WriteLine( "Request : " );
            streamWriter.WriteLine( '\r' + '\r' + Request.Form["request"].Replace( "><", ">" + '\r' + "<" ) + '\r' + '\r' );
            streamWriter.WriteLine( "Response : " );
            streamWriter.WriteLine( str );
            streamWriter.Close();
        }
    }

    private void ProcessRequest()
    {
        string request = Request.Form["request"];
        request = request.Replace( "><", "> <" );

        THubQuickBooks thubQuickBooks = new THubQuickBooks();
        String response = thubQuickBooks.ProcessRequest( request );
        Response.Write( response );
        logging(  response.Replace( "><", ">" + '\r' + "<" ) + '\r' + '\r' );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        Response.CacheControl = "private";
        Response.Expires = 0;
        Response.AddHeader( "pragma", "no-cache" );
        ProcessRequest();
        Response.End();
    }

    protected void Page_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

}
