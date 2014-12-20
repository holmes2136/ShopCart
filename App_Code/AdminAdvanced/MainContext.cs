using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using nStuff.UpdateControls;
using Vevo.Shared.Utilities;
using Vevo.WebUI.Users;

namespace Vevo
{
    /// <summary>
    /// Summary description for ControlModule
    /// </summary>
    [Serializable]
    public class MainContext
    {
        private AdminAdvancedBasePage _page;


        private string[] QueryStringArray
        {
            get { return RawQueryString.Split( '&' ); }
        }

        private string GetQueryStringValueByName( string name )
        {
            string[] tmpItem;
            foreach (string item in QueryStringArray)
            {
                tmpItem = item.Split( '=' );
                if (tmpItem[0].ToLower() == name.ToLower())
                    return tmpItem[1];
            }
            return string.Empty;
        }

        private string GetArrayItem( string[] array, int index )
        {
            if (array.Length <= index)
                return String.Empty;

            return ConvertUtilities.ToString( array[index] );
        }

        private void AddHistoryEntry( string pageString, string queryString )
        {
            Regex regex = new Regex( ".ascx", RegexOptions.IgnoreCase );
            UpdateHistory updateHistory = (UpdateHistory) _page.FindControl( "uxUpdateHistory" );
            updateHistory.AddEntry( regex.Replace( pageString, "," ) + queryString );
        }

        private void PrivateLoadMainControl()
        {
            AdminHelper admin = AdminHelper.LoadByUserName( _page.User.Identity.Name );
            if (!admin.CanViewPage( _page.LastControl ))
            {
                AdminErrorMainControl.Header = "AdminError";
                AdminErrorMainControl.Message =
                    String.Format( "You do not have permission to access &quot;{0}&quot;", _page.LastControl );
                _page.LastControl = "AdminError.ascx";
                _page.QueryString = "";
                _page.IsControlPostback = false;
            }

            PlaceHolder myPanel = (PlaceHolder) _page.FindControl( "uxContentPlaceHolder" );
            myPanel.Controls.Clear();
            if (!String.IsNullOrEmpty( _page.LastControl ))
            {
                UserControl uc = (UserControl) _page.LoadControl(
                    String.Format( "MainControls/{0}", _page.LastControl ) );
                uc.ID = "dummyControlID";
                myPanel.Controls.Add( uc );
            }

            AddHistoryEntry( _page.LastControl, _page.QueryString );
        }

        public string LastControl
        {
            get
            {
                return _page.LastControl;
            }
            set { _page.LastControl = value; }
        }

        public string RawQueryString
        {
            get { return _page.QueryString; }
            set { _page.QueryString = value; }
        }

        public bool IsPostBack
        {
            get { return _page.IsControlPostback; }
        }

        public NameValueCollection QueryString
        {
            get
            {
                NameValueCollection collection = new NameValueCollection();

                foreach (string item in QueryStringArray)
                {
                    string[] pair = item.Split( '=' );
                    collection.Add( GetArrayItem( pair, 0 ), GetArrayItem( pair, 1 ) );
                }
                return collection;
            }
        }


        public MainContext( AdminAdvancedBasePage page )
        {
            _page = page;
        }

        public void RedirectMainControl( string pageString )
        {
            RedirectMainControl( pageString, String.Empty );
        }

        public void RedirectMainControl( string pageString, string queryString )
        {
            _page.LastControl = pageString; //Clear Control before load new one
            _page.QueryString = queryString;
            _page.IsControlPostback = false;

            PrivateLoadMainControl();
            //_page.Response.End();
        }

        public void AddBrowseHistory( string pageString, string queryString )
        {
            _page.LastControl = pageString;
            _page.QueryString = queryString;

            AddHistoryEntry( pageString, queryString );
        }

        public void LoadMainControl()
        {
            _page.IsControlPostback = true;

            PrivateLoadMainControl();
        }
    }
}

