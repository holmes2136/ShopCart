using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;


namespace Vevo
{
    /// <summary>
    /// Summary description for DynamicPageElement
    /// </summary>
    public class DynamicPageElement
    {
        private Page _page;

        private void AddMeta( string metaName, string content )
        {
            WebUtilities.PageAddMeta( _page, metaName, content );

        }

        private void AddTitle( string name )
        {
            _page.Title = name;
        }

        public DynamicPageElement( Page page )
        {
            _page = page;
        }

        public void SetUpTitleAndMetaTags( string name, string description )
        {
            SetUpTitleAndMetaTags( name, description, name );
        }

        public void SetUpTitleAndMetaTags( string name, string description, string keywords )
        {
            AddMeta( "description", description );
            AddMeta( "keywords", keywords );
            AddTitle( name );
        }
    }
}
