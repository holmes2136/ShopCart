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
    /// Summary description for AdminAdvancedBasePage
    /// </summary>
    public class AdminAdvancedBasePage : AdminBasePage
    {
        private MainContext _mainContext;

        public AdminAdvancedBasePage()
        {
            _mainContext = new MainContext( this );
            PreInit += new EventHandler( AdminAdvancedBasePage_PreInit );
            Load += new EventHandler( AdminAdvancedBasePage_Load );
        }

        private void AdminAdvancedBasePage_PreInit( object sender, EventArgs e )
        {
            Page.Theme = AdminTheme;
        }

        private HtmlLink CreateLink( string href )
        {
            HtmlLink link = new HtmlLink();
            link.Href = href;
            link.Attributes.Add( "rel", "stylesheet" );
            link.Attributes.Add( "type", "text/css" );
            return link;
        }

        private Literal CreateLiteral( string text )
        {
            Literal literal = new Literal();
            literal.Text = text;
            return literal;
        }

        private void AdminAdvancedBasePage_Load( object sender, EventArgs e )
        {
            String browserName = Request.Browser.Browser.ToLower();
            Page.Header.Controls.Add( CreateLink( "~/App_Themes/AdminCommon/AdvancedAdminCommon.css" ) );
            
            if (browserName == "firefox" || browserName == "applemac-safari")
            {
                Page.Header.Controls.Add( CreateLink( "~/App_Themes/AdminCommon/AdvanceAdminFixFireFox.css" ) );
            }
            else
            {
                Page.Header.Controls.Add( CreateLink( "~/App_Themes/AdminCommon/AdvanceAdminFixIE.css" ) );

                Page.Header.Controls.Add( CreateLiteral( "<!--[if IE 6]>\n" ) );
                Page.Header.Controls.Add( CreateLink( "~/App_Themes/AdminCommon/AdvanceAdminFixIE6.css" ) );
                Page.Header.Controls.Add( CreateLiteral( "<![endif]-->" ) );
            }

            RegisterScriptaculousJavaScript();
        }

        private void RegisterScriptaculousJavaScript()
        {
            RegisterAdminScriptInclude( "ClientScripts/UtilScript.js", "JavaScriptUtilities" );
        }

        private void SetAdminThemeCookie( string theme )
        {
            Response.Cookies["AdminTheme"].Value = theme;
            Response.Cookies["AdminTheme"].Expires = DateTime.Now.AddYears( 1 );
        }


        protected string AdminTheme
        {
            get
            {
                if (Request.Cookies["AdminTheme"] == null ||
                    String.IsNullOrEmpty( Request.Cookies["AdminTheme"].Value ))
                {
                    SetAdminThemeCookie( "AdminBlueTheme" );

                    return "AdminBlueTheme";
                }
                else
                {
                    return Request.Cookies["AdminTheme"].Value;
                }
            }
            set
            {
                SetAdminThemeCookie( value );
            }
        }


        public MainContext MainContext
        {
            get { return _mainContext; }
        }

        public string LastControl
        {
            get
            {
                if (ViewState["LastControl"] == null)
                    ViewState["LastControl"] = String.Empty;

                return ViewState["LastControl"].ToString();
            }
            set { ViewState["LastControl"] = value; }
        }

        public string LastControlCheckPostback
        {
            get
            {
                if (ViewState["LastControlCheckPostback"] == null)
                    ViewState["LastControlCheckPostback"] = String.Empty;

                return ViewState["LastControlCheckPostback"].ToString();
            }
            set { ViewState["LastControlCheckPostback"] = value; }
        }

        public string QueryString
        {
            get
            {
                if (ViewState["QueryString"] == null)
                    ViewState["QueryString"] = String.Empty;

                return ViewState["QueryString"].ToString();
            }
            set
            {
                ViewState["QueryString"] = HttpContext.Current.Server.UrlDecode( value );
            }
        }

        public string Message
        {
            get
            {
                if (ViewState["Message"] == null)
                    ViewState["Message"] = String.Empty;

                return ViewState["Message"].ToString();
            }
            set { ViewState["Message"] = value; }
        }

        public bool IsControlPostback
        {
            get
            {
                if (ViewState["IsControlPostback"] == null)
                    ViewState["IsControlPostback"] = true;

                return (bool) ViewState["IsControlPostback"];
            }
            set { ViewState["IsControlPostback"] = value; }
        }
    }
}
