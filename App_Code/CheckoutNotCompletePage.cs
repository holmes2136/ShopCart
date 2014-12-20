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
    /// Summary description for CheckoutNotCompletePage
    /// </summary>
    public static class CheckoutNotCompletePage
    {
        public static string TitlePage = String.Empty;
        public static string DescriptionPage = String.Empty;
        public static string Link = String.Empty;
        public static string LinkName = String.Empty;
        public static string PageName = String.Empty;
        public static string QueryString = String.Empty;

        public static void RedirectToPage(
            string titlePage, string description, string urlLink, string linkName )
        {
            TitlePage = titlePage;
            DescriptionPage = description;
            Link = urlLink;
            LinkName = linkName;

            if (UrlManager.IsMobile())
            {
                HttpContext.Current.Response.Redirect( "~/" + UrlManager.MobileFolder + "/CheckoutNotComplete.aspx" );
            }
            else
            {
                HttpContext.Current.Response.Redirect( "~/CheckoutNotComplete.aspx" );
            }
        }

        public static void RedirectToPage(
           string titlePage, string description, string pageName, string queryString, string linkName, string redirectUrl )
        {
            TitlePage = titlePage;
            DescriptionPage = description;
            PageName = pageName;
            QueryString = queryString;
            LinkName = linkName;
            
            HttpContext.Current.Response.Redirect( redirectUrl ); ;
        }
    }
}