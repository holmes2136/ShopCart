using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;

namespace Vevo
{
    /// <summary>
    /// Summary description for UrlQuery
    /// </summary>
    public class UrlQuery
    {
        private string _queryString = String.Empty;


        private string GetArrayItem( string[] array, int index )
        {
            if (array.Length <= index)
                return String.Empty;

            return ConvertUtilities.ToString( array[index] );
        }


        public string RawQueryString
        {
            get { return _queryString; }
        }

        public NameValueCollection QueryString
        {
            get
            {
                NameValueCollection collection = new NameValueCollection();

                string[] queryArray = _queryString.Split( '&' );

                foreach (string item in queryArray)
                {
                    string[] pair = item.Split( '=' );
                    collection.Add( GetArrayItem( pair, 0 ), GetArrayItem( pair, 1 ) );
                }
                return collection;
            }
        }


        public UrlQuery()
        {
        }

        public void AddQuery( string parameter, string value )
        {
            if (!String.IsNullOrEmpty( parameter ))
                _queryString = 
                    WebUtilities.UrlAddQueryVariable( _queryString, parameter, value ).Replace( "?", "" );
        }

        public void AddQuery( NameValueCollection queries )
        {
            for (int i = 0; i < queries.Count; i++)
            {
                AddQuery( queries.GetKey( i ), queries.Get( i ) );
            }
        }

        public void RemoveQuery( string parameter )
        {
            _queryString = WebUtilities.UrlRemoveQueryVariable( _queryString, parameter ).Replace( "?", "" );
        }

    }
}
