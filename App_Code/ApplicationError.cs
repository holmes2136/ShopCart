using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Vevo
{
    /// <summary>
    /// Summary description for ApplicationErrorPage
    /// </summary>
    public static class ApplicationError
    {
        public static string Header = String.Empty;
        public static string Message = String.Empty;

        public static void RedirectToErrorPage( string header, string message )
        {
            Header = header;
            Message = message;

            HttpContext.Current.Response.Redirect( "~/ApplicationErrorPage.aspx" );
        }
    }
}
