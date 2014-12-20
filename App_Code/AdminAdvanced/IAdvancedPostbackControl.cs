using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;

namespace Vevo
{
    /// <summary>
    /// Summary description for IAdvancedPostbackControl
    /// </summary>
    public interface IAdvancedPostbackControl
    {
        void UpdateBrowseQuery( UrlQuery urlQuery );
    }
}
