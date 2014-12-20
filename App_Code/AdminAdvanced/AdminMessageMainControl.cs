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
    /// Summary description for AdminMessageMainControl
    /// </summary>
    public static class AdminMessageMainControl
    {
        private static string _header;
        private static string _message;
        private static string _returnLink;

        public static string Header { get { return _header; } set { _header = value; } }
        public static string Message { get { return _message; } set { _message = value; } }
        public static string ReturnLink { get { return _returnLink; } set { _returnLink = value; } }

        public static void RedirectToMessagePage( 
            MainContext mainContext, 
            string header, 
            string message, 
            string returnLink )
        {
            Header = header;
            Message = message;
            ReturnLink = returnLink;

            mainContext.RedirectMainControl( "AdminMessage.ascx", String.Empty );
        }
    }

}
