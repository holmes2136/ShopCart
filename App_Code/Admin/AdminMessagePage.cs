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
    /// Summary description for AdminMessagePage
    /// </summary>
    public static class AdminMessagePage
    {
        public static string Title = String.Empty;
        public static string Header = String.Empty;
        public static string Message = String.Empty;
        public static string ReturnLink = String.Empty;


        public static void RedirectToMessagePage( string title, string header, string message, string returnLink )
        {
            Title = title;
            Header = header;
            Message = message;
            ReturnLink = returnLink;

            HttpContext.Current.Response.Redirect( "~/Admin/AdminMessage.aspx" );
        }

    }
}
