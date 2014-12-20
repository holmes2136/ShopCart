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
    /// Summary description for AdminErrorPage
    /// </summary>
    public static class AdminErrorPage
    {
        private static string _header;
        private static string _message;

        public static string Header { get { return _header; } set { _header = value; } }
        public static string Message { get { return _message; } set { _message = value; } }

        public static void RedirectToErrorPage( string header, string message )
        {
            Header = header;
            Message = message;
            HttpContext.Current.Response.Redirect( "~/Admin/AdminError.aspx", false );
        }

    }
}
