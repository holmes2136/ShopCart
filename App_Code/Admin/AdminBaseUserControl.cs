using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebUI.Users;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminBaseUserControl
    /// </summary>
    public class AdminBaseUserControl : Vevo.WebUI.BaseControls.BaseUserControl
    {
        protected bool IsAdminModifiable()
        {
            AdminHelper admin = AdminHelper.LoadByUserName( Page.User.Identity.Name );
            return admin.CanModifyPage( Request.Url.AbsoluteUri );
        }

    }
}
