using System;
using System.Data;
using System.Configuration;

namespace Vevo
{
    /// <summary>
    /// Summary description for BrowseHistoryAddEventArgs
    /// </summary>
    public class BrowseHistoryAddEventArgs : EventArgs
    {
        private UrlQuery _browseHistoryQuery;


        public BrowseHistoryAddEventArgs( UrlQuery browseHistoryQuery )
        {
            _browseHistoryQuery = browseHistoryQuery;
        }

        public UrlQuery BrowseHistoryQuery
        {
            get { return _browseHistoryQuery; }
            set { _browseHistoryQuery = value; }
        }
    }
}
