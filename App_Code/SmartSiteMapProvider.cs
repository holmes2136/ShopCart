using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace Vevo
{
    /// <summary>
    /// Summary description for SmartSiteMapProvider
    /// </summary>
    public class SmartSiteMapProvider : XmlSiteMapProvider
    {
        //	Avoid running the file web.sitemap.
        public override SiteMapNode FindSiteMapNode( string rawUrl )
        {
            return null;
        }
    }
}