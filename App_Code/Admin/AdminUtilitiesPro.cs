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
    /// Summary description for AdminUtilitiesPro
    /// </summary>
    public static partial class AdminUtilities
    {
        public static void ClearCurrencyCache()
        {
            CurrencyUtilities.ClearCache();
        }
    }
}
