using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain.Payments;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminAdvancedBaseGatewayUserControl
    /// </summary>
    public abstract class AdminAdvancedBaseGatewayUserControl : AdminAdvancedBaseUserControl
    {
        public string CultureID
        {
            get
            {
                if (ViewState["CultureID"] == null)
                    return AdminUtilities.DefaultCultureID;
                else
                    return ViewState["CultureID"].ToString();
            }
            set
            {
                ViewState["CultureID"] = value;
            }
        }


        public abstract void Refresh();

        public abstract void Save();

    }
}
